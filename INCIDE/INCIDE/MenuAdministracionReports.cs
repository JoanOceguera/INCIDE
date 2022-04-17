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
    public partial class MenuAdministracionReports : Form
    {
        INCIDEControl icontrol;
        public MenuAdministracionReports(INCIDEControl icontrol)
        {
            InitializeComponent();
            this.icontrol = icontrol;
            if (icontrol.Logueado.Password.First().Role_nom != null && icontrol.Logueado.Password.First().Role_nom.codigo == (int)Rol.Administrador)
            {
                this.panel15.Visible = true;
                this.panel6.Visible = true;
                this.panel9.Visible = true;
                this.panel11.Visible = true;
                this.panel13.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IncidenciasPorExpediente inForm = new IncidenciasPorExpediente(this.icontrol);
            inForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReportesForm reportForm = new ReportesForm(this.icontrol);
            reportForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TrabajadoresForm workersForm = new TrabajadoresForm(this.icontrol);
            workersForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RegistroForm registrosForm = new RegistroForm(this.icontrol);
            registrosForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AltasBajasForm altasBajas = new AltasBajasForm(this.icontrol);
            altasBajas.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CategorizacionForm categorizacion = new CategorizacionForm(this.icontrol);
            categorizacion.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AsistenciaForm asistencia = new AsistenciaForm(this.icontrol);
            asistencia.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AsistenciaMesForm asistencia = new AsistenciaMesForm(this.icontrol);
            asistencia.ShowDialog();
        }
    }
}
