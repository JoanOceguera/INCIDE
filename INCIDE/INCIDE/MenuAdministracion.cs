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
    public partial class MenuAdministracion : Form
    {
        INCIDEControl icontrol;
        public MenuAdministracion(INCIDEControl icontrol)
        {
            InitializeComponent();
            this.icontrol = icontrol;            
        }

        private void ShowBotonesSegunRol()
        {
            if (this.icontrol.Logueado.Password.First().Role_nom.codigo == (int)Rol.Administrador)
                this.pnl_persona.Visible = this.pnl_prenomina.Visible = true;

        }
        private void btnGestionarPersona_Click(object sender, EventArgs e)
        {
            GestionarPersona gestpf = new GestionarPersona(this.icontrol);
            gestpf.ShowDialog();
        }

        private void btnAbrirCerrarMesPrenom_Click(object sender, EventArgs e)
        {
            AdminForm adf = new AdminForm(this.icontrol);
            adf.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerarDBF adf = new GenerarDBF(this.icontrol);
            adf.ShowDialog();
        }

        private void MenuAdministracion_Shown(object sender, EventArgs e)
        {
            this.ShowBotonesSegunRol();
        }

        private void btnGestionarArea_Click(object sender, EventArgs e)
        {
            GestionarAreas forma = new GestionarAreas(this.icontrol);
            forma.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GestionarClaveNom forma = new GestionarClaveNom(icontrol);
            forma.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GestionarAgrupacion forma = new GestionarAgrupacion(icontrol);
            forma.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GestinarGruposPeriodosTiempo forma = new GestinarGruposPeriodosTiempo(icontrol);
            forma.ShowDialog();
        }

        private void btnGenerarXml_Click(object sender, EventArgs e)
        {
            GenerarXml gxml = new GenerarXml(this.icontrol);
            gxml.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GestionarGrupoEvaluacion grupoEvaluacion = new GestionarGrupoEvaluacion(this.icontrol);
            grupoEvaluacion.ShowDialog();
        }

        private void btnGenerarXmlUtilidades_Click(object sender, EventArgs e)
        {
            GenerarXmlUtilidades gxml = new GenerarXmlUtilidades(this.icontrol);
            gxml.ShowDialog();
        }
    }
}
