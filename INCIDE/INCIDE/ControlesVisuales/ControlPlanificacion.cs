using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using INCIDE.ServiceReference1;

namespace INCIDE.Controles_Visuales
{
    public partial class ControlPlanificacion : UserControl
    {
        INCIDEControl incideControl;
        List<Planificacion> planificaciones = new List<Planificacion>();
        public ControlPlanificacion(INCIDEControl incideControl)
        {
            InitializeComponent();
            this.incideControl = incideControl;
           // Cargar_planificaciones();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            WindowsFormsControlLibrary1.UserControl1 control = Crear_Control("NuevoControl");
            control.Location = new Point(0, control.Size.Height);
            panel3.Controls.Add(control);

        }
        #region Funciones
        public WindowsFormsControlLibrary1.UserControl1 Crear_Control(string NombreControl)
        {
            WindowsFormsControlLibrary1.UserControl1 userControl = new WindowsFormsControlLibrary1.UserControl1();
            userControl.Name = NombreControl;
            userControl.Size = new System.Drawing.Size(727, 74);
            userControl.TabIndex = 1;
            userControl.Dock = DockStyle.Top;
            userControl.btnEliminar.Click += new EventHandler(btnEliminar_Click);
            userControl.btnInsertar.Click += new EventHandler(btnInsertar_Click);
            userControl.cbxClave.Click += new EventHandler(cbxClave_Items_Add);
            userControl.dateFechaInicio.Value = DateTime.Now;
            userControl.dateFechaFin.Value = DateTime.Now.AddMonths(1);
            return userControl;
        }
        public WindowsFormsControlLibrary1.UserControl1 Poner_Control(int idplan, string Clave, DateTime fi, DateTime ff)
        {
            WindowsFormsControlLibrary1.UserControl1 userControl = new WindowsFormsControlLibrary1.UserControl1();
            userControl.Name = idplan.ToString();
            userControl.Size = new System.Drawing.Size(727, 74);
            userControl.TabIndex = 1;
            userControl.Dock = DockStyle.Top;
            userControl.btnEliminar.Click += new EventHandler(btnEliminar_Click);
            userControl.cbxClave.Click += new EventHandler(cbxClave_Items_Add);
            userControl.dateFechaInicio.Value = fi;
            userControl.dateFechaFin.Value = ff;
            userControl.cbxClave.Text = Clave;
            userControl.btnInsertar.Visible = false;
            userControl.dateFechaFin.Enabled = false;
            userControl.dateFechaInicio.Enabled = false;
            userControl.cbxClave.Enabled = false;
            userControl.btnInsertar.Click += new EventHandler(btnInsertar_Click);
            return userControl;
        }
        public void Modificar_Control(ref WindowsFormsControlLibrary1.UserControl1 userControl, int idplanificacion)
        {
            userControl.Name = idplanificacion.ToString();
            userControl.btnInsertar.Visible = false;
            userControl.dateFechaFin.Enabled = false;
            userControl.dateFechaInicio.Enabled = false;
            userControl.cbxClave.Enabled = false;
        }
        void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                WindowsFormsControlLibrary1.UserControl1 control = new WindowsFormsControlLibrary1.UserControl1();
                control = ((WindowsFormsControlLibrary1.UserControl1)((Button)sender).Parent.Parent.Parent);
                if (control.dateFechaInicio.Text != "" && control.dateFechaFin.Text != "" && control.cbxClave.Text != String.Empty)
                {
                    int idplanificacion = Insertar_Planificacion(control.cbxClave.Text, control.dateFechaInicio.Value, control.dateFechaFin.Value);
                    if (idplanificacion > 0)
                    {
                        Modificar_Control(ref control, idplanificacion);
                    }
                    else
                    {
                        control.dateFechaFin.Text = control.dateFechaInicio.Text = control.cbxClave.Text = "";
                    }
                }
                else MessageBox.Show("Debe llenar todos los campos.", "Tener en cuenta:");
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);                
            }

        }

        int Insertar_Planificacion(string clave, DateTime FI, DateTime FF)
        {
            int exp = Convert.ToInt32(edit_edtk.Text);
            if (!this.incideControl.EstaCerrado(FI) && !this.incideControl.EstaCerrado(FF))
            {
                Planificacion plan = incideControl.Crear_Planificacion(clave, FI, FF, exp);
                if (plan == null)
                    throw new Exception("No se pudo crear la Planificación");
                if (this.SolapadaEn(plan,incideControl.Lista_Planificaciones_Persona(exp)))
                    throw new Exception("Ya tiene alguna planificación anterior en la que existe algún dia de los que intenta adicionar como nueva planificación. No puede haber un mismo dia en dos planificaciones distintas.");
                return incideControl.Insertar_Planificacion_Persona(plan, exp);
            }
            else
            {
                MessageBox.Show("No se pueden insertar planificaciones para un mes que está cerrado.", "Tener en cuenta:");                
            }
            return -1;
        }
        void cbxClave_Items_Add(object sender, EventArgs e)
        {
            List<Clave_nom> Claves = incideControl.ListaClavesPlanificables();
            ((ComboBox)sender).Items.Clear();
            foreach (Clave_nom item in Claves)
            {
                if (item.id_clave != null)
                {
                    ((ComboBox)sender).Items.Add(item.codigo);
                }
            }
        }
        void btnEliminar_Click(object sender, EventArgs e)
        {
            WindowsFormsControlLibrary1.UserControl1 control = new WindowsFormsControlLibrary1.UserControl1();
            control = ((WindowsFormsControlLibrary1.UserControl1)((Button)sender).Parent.Parent.Parent);
            DateTime FI = control.dateFechaInicio.Value;
            DateTime FF = control.dateFechaFin.Value;
            if (!this.incideControl.EstaCerrado(FI) && !this.incideControl.EstaCerrado(FF))
            {
                Eliminar_Control(control);
            }
            else
            {
                MessageBox.Show("No se pueden eliminar planificaciones para un mes que está cerrado.", "Tener en cuenta:");
            }
        }
        public void Eliminar_Control(WindowsFormsControlLibrary1.UserControl1 Control)
        {
            try
            {
                if (Control.Name != "NuevoControl")
                {
                    if (MessageBox.Show("Esta seguro que desea Eliminar la Planicación.", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Planificacion plan = incideControl.Devolver_planificacion(Convert.ToInt32(Control.Name));

                        incideControl.Eliminar_Planificacion_Persona(plan);
                        panel3.Controls.Remove(Control);
                        Control.Dispose();
                    }
                }
                else
                {
                    panel3.Controls.Remove(Control);
                    Control.Dispose();
                }
            }
            catch (Exception msg)
            {
                throw new Exception("No se pudo eliminar el Control" + msg.Message);
            }
        }
        public void PonerNumeroExp(string exp)
        {
            if (exp != "")
            {
                this.edit_edtk.Text = exp;
                this.edit_edtk.ReadOnly = true;
                PersonalRH persona = this.incideControl.GetPersonalRHPersonaFromService(exp);
                if (persona != null)
                    this.edtNombrePlan.Text = persona.Nombre + " " + persona.Apellido1 + " " + persona.Apellido2;
                else this.edtNombrePlan.Text = string.Empty;
            }
        }
        public void Cargar_planificaciones()
        {
            if (edit_edtk.Text != "")
            {
                panel3.Controls.Clear();
                int exp = Convert.ToInt32(edit_edtk.Text);
                planificaciones = incideControl.Lista_Planificaciones_Persona(exp);
                foreach (Planificacion item in planificaciones)
                {
                    Clave_nom clave = incideControl.Devolver_Clave(Convert.ToInt32(item.id_clave));
                    WindowsFormsControlLibrary1.UserControl1 control = Poner_Control(Convert.ToInt32(item.id_planificacion), clave.codigo.ToString(), Convert.ToDateTime(item.fecha_inicio), Convert.ToDateTime(item.fecha_fin));
                    control.Location = new Point(0, control.Size.Height);
                    panel3.Controls.Add(control);
                }
            }
        }

        private bool SolapadaEn(Planificacion planificacion, List<Planificacion> planificacionesExistentes)
        {
            foreach (Planificacion plan in planificacionesExistentes)
            {
                if ((planificacion.fecha_inicio >= plan.fecha_inicio && planificacion.fecha_inicio <= plan.fecha_fin) ||
                    (planificacion.fecha_fin >= plan.fecha_inicio && planificacion.fecha_fin <= plan.fecha_fin))
                    return true;
            }
            return false;
        }

        #endregion
        private void edit_edtk_TextChanged(object sender, EventArgs e)
        {
            Cargar_planificaciones();
        }
    }
}
