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
    public partial class PersonaCategorizacionForm : Form
    {
        INCIDEControl icontrol;
        public PersonaCategorizacionForm(INCIDEControl controlI)
        {
            InitializeComponent();
            this.icontrol = controlI;
        }

        private void btnGenerarNomina_Click(object sender, EventArgs e)
        {
            this.worker_listarPersonaCategorizacion.RunWorkerAsync();
            pnlSms.Visible = true;
        }

        private void worker_listarCategorizacion_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.icontrol.GenerarExcelPersonaCategorizacion();
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
