using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace INCIDE.ControlesVisuales
{
    public partial class Control_Planificar_DNL : UserControl
    {
        INCIDEControl incideControl;
        public Control_Planificar_DNL(INCIDEControl incideControl)
        {
            this.incideControl = incideControl;
            InitializeComponent();
        }
        #region Funciones

        
        public ControlLibrary_DNL.User_Control_DNL  Crear_Control(string NombreControl)
        {
           ControlLibrary_DNL.User_Control_DNL userControl  = new ControlLibrary_DNL.User_Control_DNL();
            userControl.Name = NombreControl;
            userControl.Size = new System.Drawing.Size(727, 74);
            userControl.TabIndex = 1;
            userControl.Dock = DockStyle.Top;
            userControl.btnEliminar.Click += new EventHandler(btnEliminar_Click);
            userControl.btnInsertar.Click += new EventHandler(btnInsertar_Click);
            return userControl;
        }
        #endregion

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                ControlLibrary_DNL.User_Control_DNL control = new ControlLibrary_DNL.User_Control_DNL();
                control = ((ControlLibrary_DNL.User_Control_DNL)((Button)sender).Parent.Parent.Parent);
                if (control.dateFecha.Text != "")
                {
                    Dia_no_laborable_nom dnl = this.incideControl.CrearNuevoDiaNoLaborable(control.dateFecha.Value, control.edtDescripcion.Text);
                    int idDnl = Insertar_DiaNoLaborable(dnl, Convert.ToInt32(this.txt_idGrupo.Text));
                    Modificar_Control(ref control, idDnl);
                }
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);                
            }

        }
        public void Modificar_Control(ref ControlLibrary_DNL.User_Control_DNL userControl, int idDnl)
        {
            userControl.Name = idDnl.ToString();
            userControl.btnInsertar.Visible = false;
            userControl.dateFecha.Enabled = false;

            userControl.edtDescripcion.Enabled = false;
        }
        int Insertar_DiaNoLaborable(Dia_no_laborable_nom dnl, int idGrupo)
        {
            return incideControl.Insertar_DiaNoLaborableEnGrupo(idGrupo, dnl);
        }
        private void btnPlanificar_Click(object sender, EventArgs e)
        {
            ControlLibrary_DNL.User_Control_DNL control = Crear_Control("NuevoControl");
            control.Location = new Point(0, control.Size.Height);
            panel3.Controls.Add(control);
        }
        void btnEliminar_Click(object sender, EventArgs e)
        {
            ControlLibrary_DNL.User_Control_DNL control = new ControlLibrary_DNL.User_Control_DNL();

            control = ((ControlLibrary_DNL.User_Control_DNL)((Button)sender).Parent.Parent.Parent);
            Eliminar_Control(control);
        }

        public void Eliminar_Control(ControlLibrary_DNL.User_Control_DNL Control)
        {
            try
            {
                if (Control.Name != "NuevoControl")
                {
                    if (MessageBox.Show("Esta seguro que desea Eliminar este dia no laborable.", "Eliminar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Dia_no_laborable_nom dnl = incideControl.Obtener_DiaNoLaborable(Convert.ToInt32(Control.Name));

                        incideControl.Eliminar_DiaNoLaborableDelGrupoConID(Convert.ToInt32(this.txt_idGrupo.Text), dnl);
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
        public void PonerNombreIdGrupoDNL(string nombreGrupo, int idGrupo)
        {
            if (nombreGrupo != "")
            {
                this.txt_idGrupo.Text = idGrupo.ToString();
                this.edit_edtkDNL.Text = nombreGrupo;
                this.edit_edtkDNL.ReadOnly = true;
            }
        }

        private void CargarDiasNoLaborables(int idGrupo)
        {
            if (this.txt_idGrupo.Text != "")
            {
                panel3.Controls.Clear();
                int idG = idGrupo;
                List<Dia_no_laborable_nom> lista = incideControl.Obtener_DiasNolaborablesDeGrupo(idG);
                ControlLibrary_DNL.User_Control_DNL control;
                foreach (Dia_no_laborable_nom item in lista)
                {

                    //Clave_nom clave = incideControl.Devolver_Clave(Convert.ToInt32(item.id_clave));
                    //WindowsFormsControlLibrary1.UserControl1 control = Poner_Control(Convert.ToInt32(item.id_planificacion), clave.codigo.ToString(), Convert.ToDateTime(item.fecha_inicio), Convert.ToDateTime(item.fecha_fin));
                    //control.Location = new Point(0, control.Size.Height);
                    //panel3.Controls.Add(control);
                    control = this.Poner_Control(item.id_Dia_no_lab, item.descripcion, item.fecha);
                    this.panel3.Controls.Add(control);
                }
            }
        }

        public ControlLibrary_DNL.User_Control_DNL Poner_Control(int idDiaNoLaborable, string descripcion, DateTime fi)
        {
            ControlLibrary_DNL.User_Control_DNL userControl = new ControlLibrary_DNL.User_Control_DNL();
            userControl.Name = idDiaNoLaborable.ToString();
            userControl.Size = new System.Drawing.Size(727, 74);
            userControl.TabIndex = 1;
            userControl.Dock = DockStyle.Top;
            userControl.btnEliminar.Click += new EventHandler(btnEliminar_Click);
            userControl.dateFecha.Value = fi;            
            userControl.btnInsertar.Visible = false;
            userControl.edtDescripcion.Text = descripcion;
            userControl.dateFecha.Enabled = false;
            userControl.edtDescripcion.Enabled = false;

            //userControl.btnInsertar.Click += new EventHandler(btnInsertar_Click);
            return userControl;
        }


        private void txt_idGrupo_TextChanged(object sender, EventArgs e)
        {
            this.CargarDiasNoLaborables(Convert.ToInt32(this.txt_idGrupo.Text));
        }
        
    }
}
