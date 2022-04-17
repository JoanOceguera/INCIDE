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
    public partial class GestionarGrupoEvaluacion : Form
    {
        INCIDEControl icontrol;
        List<GrupoEvaluacion> Grupos;
        GrupoEvaluacion GrupoModificar;


        public GestionarGrupoEvaluacion(INCIDEControl pIcontrol)
        {
            InitializeComponent();
            icontrol = pIcontrol;
            Cargar_Lista_Grupos();
        }

        private void GestionarGrupoEvaluacion_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void Cargar_Lista_Grupos()
        {
            Grupos = icontrol.Listado_Grupos();
            Grupos.OrderBy(t => t.descripcion);
            cmb_grupos.DataSource = null;
            cmb_grupos.Text = "";
            LLenarComboGrupos(cmb_grupos, Grupos);
        }

        public void LLenarComboGrupos(ComboBox combo, List<GrupoEvaluacion> Grupos)
        {
            Grupos.OrderBy(t => t.descripcion);
            combo.DataSource = Grupos;
            combo.DisplayMember = "descripcion";
            combo.ValueMember = "idGrupoEvaluacion";
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (insNombreGrupo.Text != "" && insCDSnumericUpDown.Value != 0)
            {
                GrupoEvaluacion grupo = icontrol.CrearNuevoGrupoEvaluacion(insNombreGrupo.Text, insCDSnumericUpDown.Value);
                if (icontrol.AdicionarGrupoEvaluacion(grupo))
                {
                    insNombreGrupo.Text = "";
                    insCDSnumericUpDown.Value = 0;
                    MessageBox.Show("Se guardó correctamente el grupo en la base de datos.", "Información:");
                    Cargar_Lista_Grupos();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error insertando el grupo en la base de datos.", "Alerta:");
                }
            }
            else
            {
                MessageBox.Show("Debe llenar los campos de Nombre y CDS.", "Alerta:");
            }

        }

        private void cmb_grupos_SelectedValueChanged (object sender, EventArgs e)
        {
            this.GrupoModificar = (GrupoEvaluacion)this.cmb_grupos.SelectedItem;
            this.SetearMiembrosClaves(this.GrupoModificar);
        }

        private void SetearMiembrosClaves(GrupoEvaluacion GrupoModificar)
        {
            if (GrupoModificar != null)
            {
                numericUpDown2.Value = GrupoModificar.cdsReferencial;
                edtNombreGrupo.Text = GrupoModificar.descripcion;
            }
            else
            {
                if (cmb_grupos.DataSource != null)
                {
                    MessageBox.Show("No se pudo mostrar los datos de la clave.", "Alerta:");
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (edtNombreGrupo.Text != "" && numericUpDown2.Value != 0)
            {
                this.GrupoModificar.descripcion = edtNombreGrupo.Text;
                this.GrupoModificar.cdsReferencial = numericUpDown2.Value;

                if (icontrol.Modificar_GrupoEvaluacion(GrupoModificar))
                {
                    edtNombreGrupo.Text = insNombreGrupo.Text = "";
                    insCDSnumericUpDown.Value = numericUpDown2.Value = 0;
                    MessageBox.Show("Se modificó correctamente el grupo en la base de datos.", "Información:");
                    Cargar_Lista_Grupos();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error modificando el grupo en la base de datos.", "Alerta:");
                }
            }
            else
            {
                MessageBox.Show("Debe llenar los campos de Nombre y CDS.", "Alerta:");
            }

        }
    }
}
