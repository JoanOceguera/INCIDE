using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using INCIDE.ControlEntidades;
using INCIDE.ServiceReference1;

namespace INCIDE
{
    public partial class GestionarPersona : Form
    {
        INCIDEControl icontrol;
        List<Grupo> grupos;
        List<Area> areas;
        List<GrupoEvaluacion> gruposEvaluacion;
        Persona personaEditar;

        public GestionarPersona(INCIDEControl icontrol)
        {
            InitializeComponent();
            this.icontrol = icontrol;
            this.Init();
        }

        public void Init()
        {
            grupos = this.icontrol.ObtenerGruposTodos();
            areas = this.icontrol.ObtenerAreasTodas();
            gruposEvaluacion = this.icontrol.ObtenerGruposEvaluacionTodos();
            this.LLenarComboGrupos(this.cmb_grupos,grupos);
            this.LLenarComboGruposEvaluacion(this.combo_gruposEvaluacion, gruposEvaluacion);
            this.LLenarComboAreas(this.cmb_areas,areas);
        }

        public void LLenarComboGrupos(ComboBox combo,List<Grupo> grupos)
        {
            combo.DataSource = grupos;
            combo.DisplayMember = "nombre";
            combo.ValueMember = "id_Grupo";
        }
        public void LLenarComboAreas(ComboBox combo,List<Area> areas)
        {
            combo.DataSource = areas;
            combo.DisplayMember = "descripcion";
            combo.ValueMember = "id_area";
        }

        public void LLenarComboGruposEvaluacion(ComboBox combo, List<GrupoEvaluacion> grupos)
        {
            combo.DataSource = grupos;
            combo.DisplayMember = "descripcion";
            combo.ValueMember = "idGrupoEvaluacion";
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (this.txt_exp.Text != String.Empty && this.txt_ci.Text != string.Empty &&
               this.cmb_areas.Text != string.Empty && this.cmb_grupos.Text != string.Empty &&
               this.txt_ci.Text.Count() == 11 && Utils.EsNumeroEntero32(this.txt_exp.Text) && Utils.TodosNumeros(this.txt_ci.Text))
            {
                int exp = Convert.ToInt32(this.txt_exp.Text);
                Persona person = this.icontrol.ObtenerPersonaPorExp(exp);
                if (person != null)//si existe ya una persona con el expediente ke se pretende utilizar 
                {
                    this.txt_exp.Clear();
                    this.txt_exp.Focus();
                    MessageBox.Show("El número de expediente " + this.txt_exp.Text + " ya está actualmente en uso." + "\n" + "Escriba otro número de expediente.");
                }
                else
                {
                    Grupo grup = this.icontrol.ObtenerGrupoPorId(Convert.ToInt32(this.cmb_grupos.SelectedValue.ToString()));
                    Area area = this.icontrol.ObtenerAreaPorId(Convert.ToInt32(this.cmb_areas.SelectedValue.ToString()));
                    GrupoEvaluacion grupoEvaluacion = this.icontrol.ObtenerGrupoEvaluacionPorId((int)this.combo_gruposEvaluacion.SelectedValue);

                    if (grup != null && area != null && grupoEvaluacion != null)
                    {
                        if (this.icontrol.InsertarPersona(grup, exp, this.txt_ci.Text, area, grupoEvaluacion))
                        {
                            MessageBox.Show("La persona con expediente número " + this.txt_exp.Text + " fue adicionada satisfactoriamente." + "\n" + "Puede adicionar otra persona.");
                            this.txt_exp.Text = String.Empty;
                            this.txt_ci.Text = String.Empty;
                            this.txt_exp.Focus();
                        }
                        else MessageBox.Show("Ocurrió un error insertando la persona en la base de datos. Es posible que no se haya podido insertar satisfactoriamente.");
                    }
                    else
                        MessageBox.Show("Ocurrió un error adicionando la persona en la base de datos." + "\n" + "No existe el area,grupo o grupo de evaluación seleccionado");
                }
            }
            else MessageBox.Show("Debe llenar todos los campos y asegurarse de escribir valores numéricos en los cuadros de expediente y número de identidad." + "\n" + 
                                 "No olvide que el número de identidad debe tener 11 dígitos.");

        }

        public void CargarInfoPersona()
        {
            if (this.txt_exp_editar.Text != String.Empty)
            {
                if (Utils.EsNumeroEntero32(this.txt_exp_editar.Text))
                {
                    int exp = Convert.ToInt32(this.txt_exp_editar.Text);
                    List<Persona> personas = this.icontrol.ObtenerListPersonasPorExp_Activa_o_noAct(exp);
                   
                    if (personas != null && personas.Count > 0) //si se encontro una o mas personas en la bd con el exp pasado por parametro
                    {
                        this.personaEditar = personas[0];
                        this.LLenarComboCI(this.cmb_ci_editar, personas);
                    }
                    else
                    {
                        MessageBox.Show("No existe una persona con el expediente: " + this.txt_exp_editar.Text);
                        //this.HacerSeleccionEnTextBox(this.txt_exp_editar);
                        this.txt_exp_editar.Clear();
                        this.txt_exp_editar.Focus();
                        this.edt_nombre_edit.Text = String.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("Debe escribir un valor numérico en el campo expediente.");
                    this.HacerSeleccionEnTextBox(this.txt_exp_editar);
                }
            }
            else MessageBox.Show("Debe escribir un número de expediente.");
        }
        public void HacerSeleccionEnTextBox(TextBox tb)
        {
            tb.SelectionStart = 0;
            tb.SelectionLength = this.txt_exp_editar.Text.Length;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.CargarInfoPersona();
        }

        private void txt_exp_editar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.CargarInfoPersona();
        }
        public void LimpiarCamposEditarPersona()
        {
            this.txt_exp_editar.Text = String.Empty;            
            this.edt_nombre_edit.Text = String.Empty;
            this.check_activa_editar.Checked = false;
            this.cmb_ci_editar.DataSource = null;
            this.cmb_ci_editar.Items.Clear();
            if(this.cmb_area_editar.Items.Count > 0)
                this.cmb_area_editar.SelectedIndex = 0;
            if (this.cmb_grupos_editar.Items.Count > 0)
                this.cmb_grupos_editar.SelectedIndex = 0;
            if (this.editcombo_gruposEvaluacion.Items.Count > 0)
                this.editcombo_gruposEvaluacion.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.txt_exp_editar.Text != String.Empty && this.cmb_ci_editar.Text != String.Empty && this.cmb_area_editar.Text != string.Empty && this.cmb_grupos.Text != String.Empty)
            {
                if (Utils.EsNumeroEntero32(this.txt_exp_editar.Text))
                {
                    if(Utils.TodosNumeros(this.cmb_ci_editar.Text) && this.cmb_ci_editar.Text.Count() == 11)
                    {
                        if (this.personaEditar != null)
                        {
                            Grupo grup = this.icontrol.ObtenerGrupoPorId(Convert.ToInt32(this.cmb_grupos_editar.SelectedValue.ToString()));
                            Area area = this.icontrol.ObtenerAreaPorId(Convert.ToInt32(this.cmb_area_editar.SelectedValue.ToString()));
                            GrupoEvaluacion grupoEvaluacion= this.icontrol.ObtenerGrupoEvaluacionPorId((int) editcombo_gruposEvaluacion.SelectedValue);

                            if (grup != null && area != null)
                            {
                                this.personaEditar.ci = this.cmb_ci_editar.Text;
                                this.personaEditar.Area1 = area;
                                this.personaEditar.Grupo = grup;
                                this.personaEditar.GrupoEvaluacion = grupoEvaluacion;
                                this.personaEditar.activo = Convert.ToInt32(this.check_activa_editar.Checked);
                                if (this.icontrol.EditarPersona(this.personaEditar))
                                    MessageBox.Show("La persona fue editada satisfactoriamente");
                                else MessageBox.Show("Ocurrió un error editando a la persona. Comunique este error al personal de informática.");                                
                                this.LimpiarCamposEditarPersona();
                            }
                            else MessageBox.Show("No se pudo editar a la persona con expediente:" + "\n" +". Lo sentimos. Contacte al personal de informática");
                        }
                        else MessageBox.Show("No se pudo editar a la persona. Lo sentimos. Contacte al personal de informática");
                    }
                    else
                    {
                        MessageBox.Show("Debe escribir un valor válido en el campo número de identidad. Solo valores numéricos y un total de 11 caracteres");
                        //this.HacerSeleccionEnTextBox(this.txt_ci_editar);
                    }
                }
                else
                {
                    MessageBox.Show("Debe escribir un valor numérico en el campo expediente.");
                    this.HacerSeleccionEnTextBox(this.txt_exp_editar);
                }
            }
            else MessageBox.Show("Debe llenar todos los campos");
        }

        public void LLenarComboCI(ComboBox combo, List<Persona> person)
        {
            combo.DataSource = person;
            combo.DisplayMember = "ci";
            combo.ValueMember = "id_persona";
        }

        private void cmb_ci_editar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmb_ci_editar.DataSource != null)
            {
                this.personaEditar = (Persona)((ComboBox)sender).SelectedItem;
                this.LLenarComboGrupos(this.cmb_grupos_editar, grupos);
                this.LLenarComboAreas(this.cmb_area_editar, areas);
                this.LLenarComboGruposEvaluacion(this.editcombo_gruposEvaluacion, gruposEvaluacion);
                this.cmb_grupos_editar.SelectedValue = this.personaEditar.Grupo.id_Grupo;
                this.cmb_area_editar.SelectedValue = this.personaEditar.Area1.id_area;
                if (this.personaEditar.GrupoEvaluacion != null)
                {
                    this.editcombo_gruposEvaluacion.SelectedValue = this.personaEditar.GrupoEvaluacion.idGrupoEvaluacion;
                }
                
                // PersonalRH person = this.icontrol.GetPersonalRHPersonaFromService(this.personaEditar.exp.ToString());
                PersonalRH person = this.icontrol.GetPersonalRHPersonaFromServiceCI(this.personaEditar.ci);
             //   if (person != null && person.CarneId != this.personaEditar.ci)
             //       MessageBox.Show("Debe tener en cuenta que la persona con CI " + this.personaEditar.ci + " no se encuentra en nuestro centro.");
                if (person != null && person.Nombre != null && person.Apellido1 != null)
                    this.edt_nombre_edit.Text = person.Nombre + " " + person.Apellido1;
                if(person == null)
                    this.edt_nombre_edit.Text = String.Empty;
                this.check_activa_editar.Checked = (this.personaEditar.activo == 1) ? true : false;
                this.HacerSeleccionEnTextBox(this.txt_exp_editar);
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
