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
    public partial class GestionarAreas : Form
    {
        INCIDEControl icontrol;
        List<Agrupacion_nom> Agrupaciones;
        List<Persona> Personas;
        List<Area> areas;
        Area areaSeleccionada;
        PasswordControl passwordC;

        public GestionarAreas(INCIDEControl icontrol)
        {
            InitializeComponent();
            this.icontrol = icontrol;
            this.Init();
            this.passwordC = new PasswordControl();
        }
        public void Init()
        {
            Agrupaciones = this.icontrol.ObtenerListaAgrupaciones();
            Personas = this.icontrol.ObtenerListaPersonas();
            areas = this.icontrol.ObtenerAreasTodas();
            this.LLenarComboAgrupaciones(this.cbxAgrup, Agrupaciones);
            this.LLenarComboPersona(this.cbxJefe, Personas);
            this.LLenarlistBoxPersona(this.list_listaPersonas, this.icontrol.ObtenerListaPersonas());
            if (areas != null)
            {
                this.LLenarComboAreas(this.cmb_areas, areas);
            }
        }
        public void LLenarComboAgrupaciones(ComboBox combo, List<Agrupacion_nom> agrupaciones)
        {
            agrupaciones.OrderBy(t => t.descripcion);
            combo.DataSource = agrupaciones;
            combo.DisplayMember = "descripcion";
            combo.ValueMember = "id_Agrup";
        }
        public void LLenarlistBoxPersona(ListBox list, List<Persona> personas)
        {
            personas.OrderBy(t => t.exp);
            list.DataSource = personas;
            list.DisplayMember = "exp";
            list.ValueMember = "id_Persona";

        }
        public void LLenarComboPersona(ComboBox combo, List<Persona> personas)
        {
            personas.OrderBy(t => t.exp);
            combo.DataSource = personas;
            combo.DisplayMember = "exp";
            combo.ValueMember = "id_Persona";
        }
        public void LLenarComboAreas(ComboBox combo, List<Area> areas)
        {
            areas.OrderBy(t => t.descripcion);
            combo.DataSource = areas;
            combo.DisplayMember = "descripcion";
            combo.ValueMember = "id_Area";
        }
        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (this.cbxAgrup.Text != String.Empty && this.edtNombreArea.Text != String.Empty && this.cbxJefe.Text != String.Empty)
            {
                if (this.listBox_Escogidos.Items.Count > 0)
                {
                    int idAgrup = Convert.ToInt32(cbxAgrup.SelectedValue.ToString());
                    int idPersonaJefe = Convert.ToInt32(cbxJefe.SelectedValue.ToString());
                    Agrupacion_nom Agrupacion = icontrol.ObtenerAgrupacionId(idAgrup);
                    Persona Persona = icontrol.ObtenerPersonaId(idPersonaJefe);
                    if (Agrupacion != null && Persona != null)
                    {
                        Area nuevaArea = icontrol.CrearNuevaArea(Agrupacion, Persona, edtNombreArea.Text.Trim());
                        icontrol.AdicionarArea(nuevaArea);

                        Persona personaI = new Persona();
                        List<Persona> listaPersona = new List<Persona>();
                        for (int i = 0; i < list_listaPersonas.SelectedItems.Count; i++)
                        {
                            Persona person = (Persona)list_listaPersonas.SelectedItems[i];
                            listaPersona.Add(person);
                        }

                        bool sillenoLista = icontrol.llenar_Lista_personas_Controlan_Area(nuevaArea, listaPersona);

                        cbxAgrup.Text = cbxJefe.Text = edtNombreArea.Text = "";
                        MessageBox.Show("Se guardó correctamente el área en la base de datos.", "Información:");
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error insertando el área en la base de datos. Puede que la agrupación o la persona no existan.", "Alerta:");
                    }
                }
                else MessageBox.Show("Debe seleccionar al menos una persona responsable de área");
            }
            else
                MessageBox.Show("Debe llenar todos los campos.", "Tener en cuenta:");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var listItem in this.list_listaPersonas.SelectedItems)
            {
                if (!this.listBox_Escogidos.Items.Contains(listItem))
                    listBox_Escogidos.Items.Add(listItem);
            }

            listBox_Escogidos.DisplayMember = "exp";
            listBox_Escogidos.ValueMember = "id_Persona";
            this.list_listaPersonas.ClearSelected();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(this.listBox_Escogidos);
            selectedItems = this.listBox_Escogidos.SelectedItems;

            if (this.listBox_Escogidos.SelectedIndex != -1)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                    this.listBox_Escogidos.Items.Remove(selectedItems[i]);
            }
        }

        private void cmb_areas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.areaSeleccionada = (Area)this.cmb_areas.SelectedItem;
            this.SetearMiembrosArea(this.areaSeleccionada);
        }

        public void SetearMiembrosArea(Area area)
        {
            List<Agrupacion_nom> agrups = this.icontrol.ObtenerListaAgrupaciones();
            LLenarComboAgrupaciones(cmb_agrup, agrups);
            this.cmb_agrup.SelectedValue = area.id_agrup_nom;
            this.edt_nombreArea.Text = area.descripcion;

            List<Persona> persons = this.icontrol.ObtenerListaPersonas();
            LLenarComboPersona(cmb_jefe, persons);
            this.cmb_jefe.SelectedValue = area.Persona.id_Persona;
            List<Persona> perss = this.icontrol.ObtenerListaPersonas();
            this.LLenarlistBoxPersona(this.list_personas, perss);

            this.list_responsables.Items.Clear();
            foreach (var item in this.icontrol.ObtenerPersonasControlanArea(area))
            {
                this.list_responsables.Items.Add((Persona)item);
            }
            list_responsables.DisplayMember = "exp";
            list_responsables.ValueMember = "id_Persona";
            this.list_personas.ClearSelected();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.cmb_areas.Text != String.Empty && this.cmb_agrup.Text != String.Empty && this.edt_nombreArea.Text != String.Empty && this.cmb_jefe.Text != String.Empty
                && this.list_responsables.Items.Count > 0)
            {
                if (this.areaSeleccionada != null)
                {
                    this.areaSeleccionada.descripcion = this.edt_nombreArea.Text;
                    this.areaSeleccionada.Agrupacion_nom = (Agrupacion_nom)this.cmb_agrup.SelectedItem;
                    this.icontrol.SetearJefeArea(this.areaSeleccionada, (Persona)this.cmb_jefe.SelectedItem);
                    List<Persona> responsables = new List<Persona>();
                    foreach (Persona item in this.list_responsables.Items) { responsables.Add(item); }
                    this.icontrol.Update_PersonasControlarArea(this.areaSeleccionada, responsables);
                    if (this.icontrol.EditarArea(this.areaSeleccionada))
                        MessageBox.Show("Datos salvados satisfactoriamente.");
                    foreach (Persona resp in responsables)
                    {
                        Password password = this.passwordC.GetPasswordDePersona(resp);
                        if (password == null)
                        {
                            String hmac = INCIDEControl.GetHMACstatic(resp.exp.ToString());
                            password = new Password();
                            password.id_persona = resp.id_Persona;
                            //password.id_rol = 0;//Se asume que el usuario que se le inserta la contraseña no es administrador
                            password.password1 = hmac;
                            passwordC.Adicionar(password);
                        }
                    }
                }
                else MessageBox.Show("La operacion no pudo ser completada con exito. Lo sentimos");
            }
            else MessageBox.Show("Debe llenar todos los campos.", "Tener en cuenta:");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (var listItem in this.list_personas.SelectedItems)
            {
                if (!this.list_responsables.Items.Contains(listItem))
                    list_responsables.Items.Add(listItem);
            }

            list_responsables.DisplayMember = "exp";
            list_responsables.ValueMember = "id_Persona";
            this.list_personas.ClearSelected();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItemss = new ListBox.SelectedObjectCollection(this.list_responsables);
            selectedItemss = this.list_responsables.SelectedItems;
            if (this.list_responsables.SelectedIndex != -1)
            {
                for (int i = selectedItemss.Count - 1; i >= 0; i--)
                    this.list_responsables.Items.Remove(selectedItemss[i]);
            }
        }

    }
}
