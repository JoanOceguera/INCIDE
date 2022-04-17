using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace INCIDE
{
    public partial class GestionarAgrupacion : Form
    {
        public INCIDEControl icontrol;
        public Agrupacion_nom AgrupModificar;
        public List<Agrupacion_nom> lista_Agrupacion;
        public GestionarAgrupacion(INCIDEControl picontrol)
        {
            InitializeComponent();
            icontrol = picontrol;
            Cargar_Combo_Agrupaciones();
        }



        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (edtCodigoA.Text != "" && edtDescripcion.Text != "")
            {
                Agrupacion_nom nueva = icontrol.CrearAgrupacion(edtDescripcion.Text.Trim(), edtCodigoA.Text.Trim());
                if (icontrol.Insertar_Agrupacion(nueva))
                {
                    edtCodigoA.Text = "";
                    edtDescripcion.Text = "";
                    MessageBox.Show("Se guardó correctamente la agrupación en la base de datos.", "Información:");
                    Cargar_Combo_Agrupaciones();

                }
                else
                {
                    MessageBox.Show("Ocurrió un error insertando la agrupación en la base de datos.", "Alerta:");
                }
            }

        }
        public void Cargar_Combo_Agrupaciones()
        {
            lista_Agrupacion = icontrol.ObtenerListaAgrupaciones();
            if (lista_Agrupacion != null)
            {
                cbxAgrupaciones.DataSource = null;
                cbxAgrupaciones.Text = "";
                LLenarComboAgrupaciones(cbxAgrupaciones, lista_Agrupacion);
            }
        }

        private void LLenarComboAgrupaciones(ComboBox cbxAgrupaciones, List<Agrupacion_nom> lista_Agrupacion)
        {
            
            cbxAgrupaciones.DataSource = lista_Agrupacion;
            cbxAgrupaciones.DisplayMember = "descripcion";
            cbxAgrupaciones.ValueMember = "id_Agrup";
        }

        private void cbxAgrupaciones_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbxAgrupaciones.Text != "")
            {
                this.AgrupModificar = (Agrupacion_nom)this.cbxAgrupaciones.SelectedItem;
                this.SetearMiembrosAgrupacion(this.AgrupModificar);
            }
        }
        private void SetearMiembrosAgrupacion(Agrupacion_nom Agrup)
        {
            if (Agrup != null)
            {
                edtDescripcionM.Text = Agrup.descripcion.ToString();
                edtCodigoM.Text = Agrup.codigo.ToString();

            }
            else
            {
                if (cbxAgrupaciones.DataSource != null)
                {
                    MessageBox.Show("No se pudo mostrar los datos de la agrupación.", "Alerta:");
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (edtCodigoM.Text != "" && edtDescripcionM.Text != "")
            {
                AgrupModificar.codigo = edtCodigoM.Text.Trim();
                AgrupModificar.descripcion = edtDescripcionM.Text.Trim();
                if (icontrol.Editar_Agrupacion(AgrupModificar))
                {
                    edtCodigoM.Text = edtDescripcionM.Text = cbxAgrupaciones.Text ="";
                    Cargar_Combo_Agrupaciones();

                    MessageBox.Show("Se Modificó correctamente la agrupación en la base de datos.", "Información:");
                }
                else
                {
                    MessageBox.Show("Ocurrió un error modificando la agrupación en la base de datos.", "Alerta:");

                }


            }
        }

    }
}
