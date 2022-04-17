using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using ClassLibrary2;

namespace INCIDE
{
    public partial class GenerarXml : Form

    {
        class StreamFile
        {
            private string url;
            private List<String> lastWritten;
            private FileStream file;
            public StreamFile(string dir)
            {

                this.url = dir;
                this.lastWritten = new List<String>();
            }
            public void SaveToFile(List<String> informationList)
            {


                this.file = new FileStream(this.url, FileMode.Append, FileAccess.Write, FileShare.Read);
                StreamWriter writer = new StreamWriter(this.file);

                foreach (String info in informationList)
                {
                    if (info == @"\n")
                        writer.Write(Environment.NewLine);
                    else
                    {
                        writer.Write(info);
                        writer.Write(Environment.NewLine);
                    }

                }
                this.lastWritten = informationList;
                writer.Close();
                this.file.Close();

            }
            public void SaveToFile(String informationText)
            {


                this.file = new FileStream(this.url, FileMode.Append, FileAccess.Write, FileShare.Read);
                StreamWriter writer = new StreamWriter(this.file);

                if (informationText == @"\n")
                    writer.Write(Environment.NewLine);
                else
                {
                    writer.Write(informationText);
                    writer.Write(Environment.NewLine);
                }

                this.lastWritten.Add(informationText);
                writer.Close();
                this.file.Close();

            }
            public List<String> ReadFromFile()
            {
                this.file = new FileStream(this.url, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Write);
                StreamReader reader = new StreamReader(this.file);
                List<String> data = new List<string>();
                String readed;

                try
                {
                    String line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        readed = line;
                        data.Add(line);
                    }
                }
                catch (IOException)
                {
                    this.file.Close();
                    reader.Close();
                    return null;
                }

                this.file.Close();
                reader.Close();

                return (data.Count == 0) ? new List<String>() : data;
            }
            public void ClearFile()
            {

                this.file = new FileStream(this.url, FileMode.Truncate);
                this.file.Close();
            }
            public List<String> LastWritten
            {
                get { return this.lastWritten; }
            }

        }

        INCIDEControl icontrol;

        public GenerarXml(INCIDEControl pIcontrol)
        {
            InitializeComponent();
            this.icontrol = pIcontrol;
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            //creamos un objeto OpenDialog que es un cuadro de dialogo para buscar archivos
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Archivos de Excel (*.xls;*.xlsx)|*.xls;*.xlsx"; //le indicamos el tipo de filtro en este caso que busque
            //solo los archivos excel

            dialog.Title = "Seleccione el archivo de Excel";//le damos un titulo a la ventana

            dialog.FileName = string.Empty;//inicializamos con vacio el nombre del archivo

            //si al seleccionar el archivo damos Ok
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //el nombre del archivo sera asignado al textbox
                txtArchivo.Text = dialog.FileName;
                //la variable hoja tendra el valor del textbox donde colocamos el nombre de la hoja
                LLenarGrid(txtArchivo.Text, txtHoja.Text); //se manda a llamar al metodo

                //dataGridView1.AutoSizeColumnsMode= Fi; //se ajustan las
                //columnas al ancho del DataGridview para que no quede espacio en blanco (opcional)
            }
        }

        private void LLenarGrid(string archivo, string hoja)
        {
            //declaramos las variables        
            System.Data.OleDb.OleDbConnection conexion = null;
            DataSet dataSet = null;
            System.Data.OleDb.OleDbDataAdapter dataAdapter = null;
            string consultaHojaExcel = "Select * from [" + hoja + "$]";

            //esta cadena es para archivos excel 2007 y 2010
            string cadenaConexionArchivoExcel = "provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + archivo + "';Extended Properties=Excel 12.0;";

            //para archivos de 97-2003 usar la siguiente cadena
            //string cadenaConexionArchivoExcel = "provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + archivo + "';Extended Properties=Excel 8.0;";

            //Validamos que el usuario ingrese el nombre de la hoja del archivo de excel a leer
            if (string.IsNullOrEmpty(hoja))
            {
                MessageBox.Show("No hay una hoja para leer");
            }
            else
            {
                try
                {
                    //Si el usuario escribio el nombre de la hoja se procedera con la busqueda
                    conexion = new System.Data.OleDb.OleDbConnection(cadenaConexionArchivoExcel);//creamos la conexion con la hoja de excel
                    conexion.Open(); //abrimos la conexion
                    dataAdapter = new OleDbDataAdapter(consultaHojaExcel, conexion); //traemos los datos de la hoja y las guardamos en un dataSdapter
                    dataSet = new DataSet(); // creamos la instancia del objeto DataSet
                    dataAdapter.Fill(dataSet, hoja);//llenamos el dataset

                    radGridView1.DataSource = dataSet.Tables[0]; //le asignamos al DataGridView el contenido del dataSet
                    conexion.Close();//cerramos la conexion
                    radGridView1.AllowAddNewRow = false;       //eliminamos la ultima fila del datagridview que se autoagrega
                    if (radGridView1.RowCount > 0)
                    {
                        int cantidadInsertado = radGridView1.RowCount - 1;
                        lblCantidadInsertados.Text += cantidadInsertado.ToString();
                    }
                }
                catch (Exception ex)
                {
                    //en caso de haber una excepcion que nos mande un mensaje de error
                    MessageBox.Show("Error, Verificar el archivo o el nombre de la hoja", ex.Message);
                }
            }
        }


       bool Validar_CI_Repetidos(Dictionary<string, string> listadic,string CI_nuevo)
       {
           bool encontrado = false;
           foreach (var item in listadic)
           {
               string ci = item.Key;
               if (ci==CI_nuevo)
               {
                   encontrado = true;
                   break;
               }           }
           return encontrado;

       }
        Dictionary<string, string> Cargar_Diccionario_Grid()
        {

            string ci = "";
            Dictionary<string, string> listadic = new Dictionary<string, string>();
            foreach (var item in radGridView1.Rows)
            {
                string IMPORTE = "0,00";
                int exp = Convert.ToInt32(item.Cells[0].Value);// Columna 0 del excel
                try
                {
                    ci = icontrol.GetPersonalRHPersonaFromService(exp.ToString()).CarneId;
                }
                catch (Exception)
                {
                    ci = "Causó baja";
                }

                if (item.Cells[7].Value != null)
                    IMPORTE = Math.Round(Convert.ToDecimal(item.Cells[7].Value), 2).ToString();
                if (Math.Round(Convert.ToDecimal(item.Cells[7].Value), 2) > 0)
                {
                    try
                    {
                        if (!Validar_CI_Repetidos(listadic,ci))
                        {
                            listadic.Add(ci, IMPORTE.Replace(',', '.'));
                        }                       
                    }
                    catch (Exception ec)
                    {
                        if (ec.Message.Contains(""))
                        {
                            MessageBox.Show("HAY DUPLICADOS");
                            break;
                        }
                    }
                }

            }
            return listadic;
        }

        private void FUNCION_Genera_XML(Dictionary<string, string> listadic, string dirGuardar)
        {
            StreamFile fs = new StreamFile(dirGuardar);
            if (File.Exists(dirGuardar)) fs.ClearFile();
            fs.SaveToFile("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            fs.SaveToFile("<INCIDENCIAS>");
            int i = 1;
            foreach (var dato in listadic)
            {
                fs.SaveToFile("<INCIDENCIA>");
                fs.SaveToFile("<Identidad>" + dato.Key + "</Identidad>");
                fs.SaveToFile("<TiemTipo>2</TiemTipo>");
                fs.SaveToFile("<TiemConsec>" + i++.ToString() + "</TiemConsec>");
                fs.SaveToFile("<TiemClave>2005</TiemClave>");
                fs.SaveToFile("<TiemTrab></TiemTrab>");
                fs.SaveToFile("<TiemPorCiento>0</TiemPorCiento>");

                fs.SaveToFile("<TiemImporte>" + dato.Value.ToString().Replace(',', '.') + "</TiemImporte>");// se cambio 5/10/2016 porque cuando se importo el xml no se veian los números decimales.
                fs.SaveToFile("<TiemTarifa>0</TiemTarifa>");
                fs.SaveToFile("</INCIDENCIA>");
            }
            fs.SaveToFile("</INCIDENCIAS>");
            MessageBox.Show("Se completo el proceso para " + listadic.Count.ToString() + " trabajadores");

        }

        private void btnGenerarXml_Click(object sender, EventArgs e)
        {

            Dictionary<string, string> listadic = Cargar_Diccionario_Grid();

            SaveFileDialog SFD = new SaveFileDialog();
            SFD.FileName = "Estimulación.xml";
            SFD.Filter = "Archivos Variados (*.xml;*.txt)|*.xml;*.txt"; //le indicamos el tipo de filtro en este caso que busque
            SFD.Title = "Seleccione la carpeta donde desea guardar el fichero";//le damos un titulo a la ventana

            if (SFD.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = SFD.FileName;
                FUNCION_Genera_XML(listadic, SFD.FileName);
                Criptografia cifrador = new Criptografia();
                cifrador.CargarFichero_Encriptar(SFD.FileName);

            }
        }
    }
}
