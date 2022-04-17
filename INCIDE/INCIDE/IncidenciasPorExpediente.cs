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
    public partial class IncidenciasPorExpediente : Form
    {
        INCIDEControl icontrol;
        public IncidenciasPorExpediente(INCIDEControl icontrol)
        {
            InitializeComponent();
            this.icontrol = icontrol;
            this.LlenarComboExp();
            date_inicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            date_fin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day, 23, 59, 59);
        }

        private void LlenarComboExp()
        {
            List<Persona> personas = this.icontrol.ObtenerPersonasBajoControlDe(this.icontrol.Logueado).OrderBy(p=>p.exp).ToList();
            cmb_exp.DataSource = personas;
            cmb_exp.DisplayMember = "exp";
            cmb_exp.ValueMember = "id_Persona";
        }

        private void btnGenerarNomina_Click(object sender, EventArgs e)
        {
            if (this.cmb_exp.Text != String.Empty && Utils.EsNumeroEntero32(this.cmb_exp.Text))
            {
                DateTime inicio = this.date_inicio.Value;
                DateTime fin = this.date_fin.Value;

                this.pnl_wait.Visible = true;
                this.GenerarExcelConsultante();
            }
            else MessageBox.Show("No debe dejar campos vacios. Recuerde que el número de expediente debe ser un valor numérico.");
        }

        public void GenerarExcelConsultante()
        {
            if (this.cmb_exp.Text != String.Empty && Utils.EsNumeroEntero32(this.cmb_exp.Text))
            {
                DateTime inicio = this.date_inicio.Value;
                DateTime fin = this.date_fin.Value;

                List<Object> param = new List<object>() {Convert.ToInt32(this.cmb_exp.Text), inicio, fin};
                this.worker_incidenciasPorExp.RunWorkerAsync(param);
            }
            else MessageBox.Show("No debe dejar campos vacios. Recuerde que el número de expediente debe ser un valor numérico.");
        }

        private void worker_incidenciasPorExp_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> parms = e.Argument as List<object>;
            int exp = Convert.ToInt32(parms[0]);
            DateTime inicio = (DateTime)parms[1];
            DateTime fin = (DateTime)parms[2];

            this.icontrol.GenerarExcelConsultante(exp, inicio, fin);
        }

        private void worker_incidenciasPorExp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.pnl_wait.Visible = false;
        }
    }
}
