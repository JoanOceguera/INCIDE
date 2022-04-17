using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using INCIDE;
using System.Globalization;
using INCIDE.ControlEntidades;

namespace INCIDE
{
    public partial class AdminForm : Form
    {
        INCIDEControl icontrol;
        public AdminForm(INCIDEControl icontrol)
        {
            InitializeComponent();
            this.icontrol = icontrol;
            this.edt_ano.Text = DateTime.Now.Year.ToString();
            //this.txtAno.Text = DateTime.Now.Year.ToString();
            this.chb_cerrarMes.Text += ": " + Utils.GetMesesAno()[DateTime.Now.AddMonths(-1).Month - 1];
            this.LLenarComboMeses();
            //this.LLenaComboAreas(cbxArea, icontrol.ObtenerAreasTodas());
            this.SetearChbCerrarMes();
        }

       
        public void LLenarComboMeses()
        {
            String[] Meses = Utils.GetMesesAno();
            for (int i = 0; i < Meses.Count(); i++)
            {
                Meses[i] = Meses[i].ToUpper();
            }
            cmb_mes.Items.AddRange(Meses);
           // cbxMes1.Items.AddRange(Meses);
        }

        public void LLenaComboAreas(ComboBox combo, List<Area> areas)
        {
            areas.OrderBy(t => t.descripcion);
            combo.DataSource = areas;
            combo.DisplayMember = "descripcion";
            combo.ValueMember = "id_area";
        }

        private void SetearChbCerrarMes()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 1);
            this.chb_cerrarMes.Checked = this.icontrol.EstaCerrado(dt);
        }
     
        private void btnGenerarNomina_Click(object sender, EventArgs e)
        {

            if (this.cmb_mes.Text != string.Empty && this.edt_ano.Text != string.Empty && this.edt_ano.Text.Length == 4)
            {
                try
                {
                    int ano = Convert.ToInt32(this.edt_ano.Text);
                    int mes = this.cmb_mes.SelectedIndex + 1;
                    //if (icontrol.EstaCerrado(new DateTime(ano, mes, Utils.SI)))
                    //{
                    if (this.icontrol.ExistenIncidenciasMes(mes, ano))
                    {
                        List<int> param = new List<int>() { mes, ano };
                        panel4.Visible = true;
                        this.worker_Prenomina.RunWorkerAsync(param);
                    }
                    else
                        MessageBox.Show("El mes seleccionado no tiene incidencias.");
                    //}
                    //else
                    //    MessageBox.Show("El mes seleccionado no ha sido cerrado.");//Senia solicitó que no le cumpliera esta condición para poder generar la prenomina en cuanlquier momento.
                }
                catch (Exception msg)
                {
                    MessageBox.Show("Ocurrió un error. Es posible que no haya escrito un valor numérico de 4 dígitos en el campo año."
                                    + "\n" + "Si el valor en el campo año es correcto, comunique el error siguiente al personal de informática."
                                    + "\n" + "Error: " + msg.Message);
                    this.edt_ano.Text = DateTime.Now.Year.ToString();
                }
            }
            else MessageBox.Show("Debe llenar los campos mes y año. Tenga en cuenta que el año debe estar escrito en 4 cifras.");
        }

        private void chb_cerrarMes_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chb_cerrarMes.Checked)
            {
                DateTime dt;
                if (DateTime.Now.Month == 1)
                {
                    dt = new DateTime(DateTime.Now.Year-1, 12, 1);
                }
                else
                {
                    dt = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 1);
                    
                }
                this.chb_cerrarMes.Checked = this.icontrol.CerrarMes(dt);
            }
            else
            {
                DateTime dt;
                if (DateTime.Now.Month == 1)
                {
                    dt = new DateTime(DateTime.Now.Year - 1, 12, 1);
                }
                else
                {
                    dt = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 1);

                }
                this.chb_cerrarMes.Checked = !this.icontrol.AbrirMes(dt);
            }
        }

        private void worker_Prenomina_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<int> parms = e.Argument as List<int>;
               
                this.icontrol.GenerarExcel(parms[0], parms[1]);
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void worker_Prenomina_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            panel4.Visible = false;
        }
               
        

       
    }
}
