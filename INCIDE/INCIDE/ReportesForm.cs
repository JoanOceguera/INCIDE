using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using INCIDE.ControlEntidades;

namespace INCIDE
{
    public partial class ReportesForm : Form
    {
        INCIDEControl icontrol;
        public ReportesForm(INCIDEControl controlI)
        {
            InitializeComponent();
            this.icontrol = controlI;
            this.edt_ano.Text = DateTime.Now.Year.ToString();
            this.LLenarComboMeses();
            List<Area> listaAreas = this.icontrol.ObtenerAreasControlaPersona(this.icontrol.Logueado);
            if (listaAreas.Count > 1)
            {
                Area area = new Area();
                area.descripcion = "Todos";
                area.id_area = 0;
                listaAreas.Add(area);
            }
            LLenarComboAreas(cbxAreas, listaAreas);
        }


        public void LLenarComboAreas(ComboBox combo, List<Area> areas)
        {
            combo.DataSource = areas;
            combo.DisplayMember = "descripcion";
            combo.ValueMember = "id_area";
        }

        public void LLenarComboMeses()
        {
            String[] Meses = Utils.GetMesesAno();
            for (int i = 0; i < Meses.Count()-1; i++)
            {
                Meses[i] = Meses[i].Substring(0,1).ToUpper() + Meses[i].Substring(1);
            }
            cmb_mes.Items.AddRange(Meses);
            int mesActual = DateTime.Now.Month;
            this.cmb_mes.SelectedIndex = mesActual - 1;
        }

        private void btnGenerarNomina_Click(object sender, EventArgs e)
        {
            if (this.cmb_mes.Text != string.Empty && this.edt_ano.Text != string.Empty && this.edt_ano.Text.Length == 4)
            {
                try
                {

                    int ano = Convert.ToInt32(this.edt_ano.Text);
                    int mes = this.cmb_mes.SelectedIndex + 1;
                    icontrol.LLenarHorasMAX(mes, ano);

                    List<object> param = new List<object>() { mes, ano };
                    List<Area> areas;
                    if (cbxAreas.Text == "Todos")
                    {
                         areas = this.icontrol.ObtenerAreasControlaPersona(this.icontrol.Logueado);
                    }
                    else
                    {
                        Area area = icontrol.AreaC.GetArea(Convert.ToInt32(cbxAreas.SelectedValue));
                        areas = new List<Area>();
                        areas.Add(area);                        
                    }

                    
                    if (areas.Count > 0)
                    {
                        List<int> idAreas = new List<int>();
                        foreach (Area area in areas)
                            idAreas.Add(area.id_area);

                        param.Add(idAreas);
                        this.worker_listarPorAreas.RunWorkerAsync(param);
                    }
                    else MessageBox.Show("No se pudo leer de la base de datos las áreas de las cuales el usuario logueado es responsable.");
                    pnlSms.Visible = true;
                                      


                }
                catch (Exception msg)
                {
                    MessageBox.Show("Ocurrió un error. Es posible que no haya escrito un valor numérico de 4 dígitos en el campo año."
                                    + "\n" + "Si el valor en el campo año es correcto, comunique el error siguiente al personal de informática."
                                    + "\n" + "Error: " + msg.Message);
                    this.edt_ano.Text = DateTime.Now.Year.ToString();
                }
            }
            else MessageBox.Show("Debe llenar los campos año y mes. Tenga en cuenta que el año debe estar escrito en 4 cifras");
        }

        private void worker_listarPorAreas_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<object> parms = e.Argument as List<object>;
                this.icontrol.GenerarExcelxArea((int)parms[0], (int)parms[1], (List<int>)parms[2]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void worker_listarPorAreas_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnlSms.Visible = false;
        }
    }
}
