using INCIDE.ControlEntidades;
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
    public partial class AltasBajasForm : Form
    {
        INCIDEControl icontrol;

        public AltasBajasForm(INCIDEControl controlI)
        {
            InitializeComponent();
            this.icontrol = controlI;
            this.edt_ano.Text = DateTime.Now.Year.ToString();
            this.LLenarComboMeses();
        }

        public void LLenarComboMeses()
        {
            String[] Meses = Utils.GetMesesAno();
            for (int i = 0; i < Meses.Count()-1; i++)
            {
                Meses[i] = Meses[i].Substring(0, 1).ToUpper() + Meses[i].Substring(1);
            }
            cmb_mes.Items.Add("Todos");
            cmb_mes.Items.AddRange(Meses);
            int mesActual = DateTime.Now.Month;
            this.cmb_mes.SelectedIndex = mesActual;
        }

        private void btnGenerarListado_Click(object sender, EventArgs e)
        {
            if (this.cmb_mes.Text != string.Empty && this.edt_ano.Text != string.Empty && this.edt_ano.Text.Length == 4)
            {
                try
                {

                    int ano = Convert.ToInt32(this.edt_ano.Text);
                    int mes = this.cmb_mes.SelectedIndex;
                    List<object> param = new List<object>() { ano, mes};
                    this.worker_AltasyBajas.RunWorkerAsync(param);
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

        private void worker_AltasyBajasDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<object> parms = e.Argument as List<object>;
                this.icontrol.GenerarExcelAltasBajas((int)parms[0], (int)parms[1]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void worker_AltasyBajasRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnlSms.Visible = false;
        }
    }
}
