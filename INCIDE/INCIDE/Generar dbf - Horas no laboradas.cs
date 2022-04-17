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
    public partial class GenerarDBF : Form
    {
        INCIDEControl icontrol;
        public GenerarDBF(INCIDEControl icontrol)
        {
            InitializeComponent();
            this.icontrol = icontrol;
            this.edt_ano_dbf.Text = DateTime.Now.Year.ToString();
            this.LLenarComboMeses();
        }
        public void LLenarComboMeses()
        {
            String[] Meses = INCIDE.ControlEntidades.Utils.GetMesesAno();
            for (int i = 0; i < Meses.Count(); i++)
            {
                Meses[i] = Meses[i].ToUpper();
            }
            cmb_mes_dbf.Items.AddRange(Meses);
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (this.cmb_mes_dbf.Text != string.Empty && this.edt_ano_dbf.Text != string.Empty)
            {
                if (this.edt_ano_dbf.Text.Count() == 4 && Utils.TodosNumeros(this.edt_ano_dbf.Text) && Utils.EsNumeroEntero32(this.edt_ano_dbf.Text))
                {
                    int ano = Convert.ToInt32(this.edt_ano_dbf.Text);
                    int mes = this.cmb_mes_dbf.SelectedIndex + 1;
/*
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "dbf format|*.dbf";
                    saveFileDialog1.Title = "Salvar fichero dbf";
                    saveFileDialog1.FileName = mes.ToString() + "-" + DateTime.Now.Year ;
                    DialogResult dr = saveFileDialog1.ShowDialog();
                    if (dr == DialogResult.OK)
                        if (saveFileDialog1.FileName != "")
                        { 
                            List<Object> param = new List<object>() { mes, ano, saveFileDialog1.FileName };
                            pnlwait.Visible = true;
                            this.worker_generardbf.RunWorkerAsync(param);
                        }
 */
                    List<Object> param = new List<object>() { mes, ano };
                    pnlwait.Visible = true;
                    this.worker_generardbf.RunWorkerAsync(param);
                }
                else
                {
                    MessageBox.Show("Debe escribir un valor numérico de 4 digitos para el año.");
                    this.edt_ano_dbf.Focus();
                }
            }
            else
                MessageBox.Show("Debe seleccionar un mes y escribir un año");
        }

        private void worker_generardbf_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<object> parms = e.Argument as List<object>;
               
                this.icontrol.GenerarExcelLucas(Convert.ToInt32(parms[0]), Convert.ToInt32(parms[1]));
            }
            catch (Exception){}
        }

        private void worker_generardbf_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnlwait.Visible = false;
        }
    }
}
