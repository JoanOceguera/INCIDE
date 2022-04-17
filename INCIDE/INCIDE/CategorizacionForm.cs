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
    public partial class CategorizacionForm : Form
    {
        INCIDEControl icontrol;
        public CategorizacionForm(INCIDEControl controlI)
        {
            InitializeComponent();
            this.icontrol = controlI;
        }

        private void btnGenerarNomina_Click(object sender, EventArgs e)
        {
            this.worker_listarCategorizacion.RunWorkerAsync();
            pnlSms.Visible = true;
        }

        private void worker_listarCategorizacion_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.icontrol.GenerarExcelCategorizacion();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void worker_listarCategorizacion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnlSms.Visible = false;
        }
    }
}
