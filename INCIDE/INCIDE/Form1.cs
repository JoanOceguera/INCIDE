using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using INCIDE.Controles_Visuales;
using INCIDE.ControlesVisuales;
using INCIDE.ControlEntidades;
using INCIDE.ServiceReference1;


namespace INCIDE
{
    public partial class Form1 : Form
    {
        ControlPlanificacion control;
        Control_Planificar_DNL controlP_DNL;
        ControlIncidencia controlIncide;
        INCIDEControl icontrol;
        String COLORTITULOAREABACK = "DimGray";
        Persona primeroLista;
        Grupo primeroListaGrupo;
        public enum OpcionMenu { Incidencias, Planificar, NoLaborales, PreNomina,SinOpcionActiva };
        OpcionMenu opcionActiva;

        public Form1(INCIDEControl icontrol)
        {
            InitializeComponent();
            this.icontrol = icontrol;
            //this.icontrol.SetLogginUsuario(305);
            this.opcionActiva = OpcionMenu.SinOpcionActiva;
            if (icontrol.Logueado.Password.First().Role_nom != null && icontrol.Logueado.Password.First().Role_nom.codigo == (int)Rol.Administrador)
            {             
                this.btn_nolaborables.Visible = this.btn_funcionesAdministracion.Visible = this.btn_planificar.Visible = true;                
            }
            else if (icontrol.Logueado.Password.First().Role_nom != null && icontrol.Logueado.Password.First().Role_nom.codigo == (int)Rol.Economia)
            {
                this.btn_funcionesAdministracion.Visible = true;
            }
            try
            {
                this.OpcionActiva = OpcionMenu.Incidencias;
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }         
        }

        public OpcionMenu OpcionActiva
        {
            get { return opcionActiva;}
            set { opcionActiva = value; }
        }
        

        #region Funciones
        public void CargarPlanificacionControl()
        {
            this.splitContainer1.Panel2.Controls.Clear(); 
             control = new ControlPlanificacion(icontrol);                    
            control.Location = new Point(0, 0);
            control.Dock = DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Add(control);
          
        }
        public void CargarPlanificacion_DNL()
        {
            this.splitContainer1.Panel2.Controls.Clear();
            this.controlP_DNL = new Control_Planificar_DNL(this.icontrol);
            this.controlP_DNL.Location = new Point(0, 0);
            this.controlP_DNL.Dock = DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Add(this.controlP_DNL);
        }
        #endregion
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.OpcionActiva = OpcionMenu.Planificar;
                this.Listarpersonas();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);                
            }                   
        }
        public void CargarIncidenciaControl()
        {
            this.splitContainer1.Panel2.Controls.Clear();
            controlIncide = new ControlIncidencia(this.icontrol);
            controlIncide.Location = new Point(0, 0);
            controlIncide.Dock = DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Add(controlIncide);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.OpcionActiva = OpcionMenu.Incidencias;
                this.Listarpersonas();    
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);                
            }
                      
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.OpcionActiva = OpcionMenu.NoLaborales;
                if (this.icontrol.ObtenerGruposTodos() != null)
                {
                    CargarPlanificacion_DNL();                
                    this.worker_listarGrupos.RunWorkerAsync();
                }
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }
        }
        delegate void AddAreaTitulo_delegado(List<Area> areas);
        public void AddAreaTitulo(List<Area> areas)
        {
            if (this.pnl_lateralizq.InvokeRequired)
            {
                AddAreaTitulo_delegado d = new AddAreaTitulo_delegado(AddAreaTitulo);
                this.pnl_lateralizq.Invoke(d, new object[] { areas });
            }
            else
            {
                this.pnl_lateralizq.Controls.Clear();
                for (int i = areas.Count - 1; i >= 0; i--)
                {
                    List<Persona> personasPorArea = this.icontrol.AreaC.PersonasActivasQuePertenecenArea(areas[i]);
                    for (int j = personasPorArea.Count - 1; j >= 0; j--)
                    {
                        this.pnl_lateralizq.Controls.Add(this.CrearNuevoBotonMenuIzq(personasPorArea[j].exp.ToString(), personasPorArea[j].exp.ToString()));
                    }

                    this.pnl_lateralizq.Controls.Add(this.CrearNuevoAreaTituloComponente(areas[i].descripcion));
                }
            }
        }
        public Label CrearNuevoAreaTituloComponente(String titulo)
        {
            Label label1 = new Label();
            label1.Name = "label" + titulo;
            label1.AutoSize = true;
            label1.BackColor = Color.FromName(COLORTITULOAREABACK);
            label1.Dock = System.Windows.Forms.DockStyle.Top;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            label1.Padding = new System.Windows.Forms.Padding(5, 5, 30, 5);
            label1.Text = titulo;

            return label1;
        }
        public Button CrearNuevoBotonMenuIzq(String titulo, String identificador)
        {
            Button btn_persona = new Button();

            btn_persona.BackColor = System.Drawing.Color.LightGray;
            btn_persona.Dock = System.Windows.Forms.DockStyle.Top;
            btn_persona.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btn_persona.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            btn_persona.FlatAppearance.BorderSize = 1;
            btn_persona.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            btn_persona.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            btn_persona.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn_persona.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btn_persona.ForeColor = System.Drawing.Color.DimGray;
            btn_persona.Location = new System.Drawing.Point(0, 0);
            btn_persona.Name = identificador;
            btn_persona.Size = new System.Drawing.Size(198, 30);
            btn_persona.TabIndex = 0;
            btn_persona.Text = titulo;
            btn_persona.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            btn_persona.UseVisualStyleBackColor = false;
            btn_persona.Click += new System.EventHandler(btn_persona_Click);

            return btn_persona;
        }
        private void btn_persona_Click(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            string Expp = boton.Name;           
            string Exp = Convert.ToInt32(Expp).ToString();
            
            if (this.OpcionActiva == OpcionMenu.Planificar)                         
                control.PonerNumeroExp(Exp);            
            if (this.opcionActiva == OpcionMenu.NoLaborales)
                this.controlP_DNL.PonerNombreIdGrupoDNL(boton.Text, Convert.ToInt32(boton.Name));
            if (this.opcionActiva == OpcionMenu.Incidencias)
            { 
                this.controlIncide.PonerNumeroExp(Exp);
                PersonalRH persona = icontrol.GetPersonalRHPersonaFromService(Exp);
                if (persona != null)
                {
                    string nombre = persona.Nombre + " " + persona.Apellido1 + " " + persona.Apellido2;
                    this.controlIncide.edtNombre.Text = nombre;
                }
                else this.controlIncide.edtNombre.Text = String.Empty;
            }             
        }      
        private void worker_listarpersonas_DoWork_1(object sender, DoWorkEventArgs e)
        {
            Persona logueado = this.icontrol.Logueado;
            List<Area> areas = this.icontrol.ObtenerAreasControlaPersona(logueado);
            if (areas != null && areas.Count > 0)
                this.primeroLista = this.GetPrimeroActivo(this.icontrol.AreaC.PersonasQuePertenecenArea(areas[0]));
            this.AddAreaTitulo(areas);
        }
        private Persona GetPrimeroActivo(List<Persona> personas)
        {
            Persona person = null;
            foreach (Persona per in personas)
	        {
		        if(per.activo == 1)
                    return per;
	        }
            return person;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void worker_listarpersonas_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.OpcionActiva == OpcionMenu.Planificar)
            {
                CargarPlanificacionControl();
                if (primeroLista != null)
                    control.PonerNumeroExp(primeroLista.exp.ToString());
            }
            if (this.OpcionActiva == OpcionMenu.Incidencias)
            {
                CargarIncidenciaControl();
                if (primeroLista != null)
                    this.controlIncide.PonerNumeroExp(primeroLista.exp.ToString());
            }
            pnl_loadingPrincipal.Visible = false;
            this.setBotonesEnable();
        }

        public void Listarpersonas()
        {
            pnl_loadingPrincipal.Visible = true;
            this.worker_listarpersonas.RunWorkerAsync();
        }

        private void worker_listarGrupos_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Grupo> grupos = this.icontrol.ObtenerGruposTodos();
            if (grupos != null)
            {
                this.primeroListaGrupo = grupos[0];
                this.AddGrupo(grupos);
            }
        }

        delegate void AddGrupo_delegado(List<Grupo> grupos);
        public void AddGrupo(List<Grupo> grupos)
        {
            if (this.pnl_lateralizq.InvokeRequired)
            {
                AddGrupo_delegado d = new AddGrupo_delegado(AddGrupo);
                this.pnl_lateralizq.Invoke(d, new object[] { grupos });
            }
            else
            {
                this.pnl_lateralizq.Controls.Clear();
                for (int i = grupos.Count - 1; i >= 0; i--)
                {
                    this.pnl_lateralizq.Controls.Add(this.CrearNuevoBotonMenuIzq(grupos[i].nombre,grupos[i].id_Grupo.ToString()));
                }
            }
        }

        private void worker_listarGrupos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.OpcionActiva == OpcionMenu.NoLaborales && this.primeroListaGrupo != null)
                this.controlP_DNL.PonerNombreIdGrupoDNL(primeroListaGrupo.nombre, primeroListaGrupo.id_Grupo);             
        }

        private void btnGenerarNomina_Click(object sender, EventArgs e)
        {
            MenuAdministracion madminf = new MenuAdministracion(this.icontrol);
            madminf.ShowDialog();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            MenuAdministracionReports repoForm = new MenuAdministracionReports(this.icontrol);
            repoForm.ShowDialog();            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (this.OpcionActiva == OpcionMenu.Incidencias)
                Listarpersonas();
        }
        private void setBotonesEnable()
        {
           btn_incidencias.Enabled = btnReporte.Enabled = this.btn_nolaborables.Enabled = this.btn_funcionesAdministracion.Enabled = this.btn_planificar.Enabled = true; 
        }

     
    }


}

