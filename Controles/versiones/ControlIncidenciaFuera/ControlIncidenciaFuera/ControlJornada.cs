using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlIncidenciaFuera.Clases;
using System.Globalization;
namespace ControlIncidenciaFuera
{
    public partial class ControlJornada : UserControl
    {
        public delegate void ManipuladorEventoOnAnyClaveChange(object sender, EventArgs arg);
        public event ManipuladorEventoOnAnyClaveChange onAnyClaveChange;

        public List<Traza> listaTraza;
        public List<string> listaClaves = new List<string>();
        private int posJornadaLista; // posicion de la Jornada en la lista 

        public int PosJornadaLista
        {
            get { return posJornadaLista; }
            set { posJornadaLista = value; }
        }

        public ControlJornada()
        {
            InitializeComponent();
        }
        public ControlJornada(List<Traza> plistaTraza, List<string> plistaClaves)
        {
            InitializeComponent();
            listaTraza = plistaTraza;
            listaClaves = plistaClaves;
            Pintar_Listado_Trazas();
            
        }
        public ControlJornada(List<Traza> plistaTraza, List<string> plistaClaves, int pPosListaTraza, int pPosListaJornada )
        {
            InitializeComponent();
            listaTraza = plistaTraza;
            listaClaves = plistaClaves;
            PosJornadaLista = pPosListaJornada;
            Pintar_Listado_Trazas();

        }
        public void setValoresIniciales(List<Traza> plistaTraza, List<string> plistaClaves)
        {
            listaTraza = plistaTraza;
            listaClaves = plistaClaves;
            Pintar_Listado_Trazas();
        }

        #region

        public libreria_Incidencia_Dentro.ControlTraza Crear_Traza_Control(string nombre, Traza traza)
        {
            libreria_Incidencia_Dentro.ControlTraza controlTraza = new libreria_Incidencia_Dentro.ControlTraza();
            
            controlTraza.AutoScroll = true;
            controlTraza.BackColor = System.Drawing.Color.White;
            controlTraza.Dock = System.Windows.Forms.DockStyle.Left;
            controlTraza.Location = new System.Drawing.Point(100, 0);
            controlTraza.Name = nombre;
            controlTraza.Size = new System.Drawing.Size(100, 95);
            controlTraza.TabIndex = 3;
            this.Pintar_Claves(controlTraza.cmb_claves, this.listaClaves);
         //   controlTraza.cmb_claves.Click += new EventHandler(Pintar_Claves);
            controlTraza.lbl_fecha.Text = traza.Fecha.Day + "/" + traza.Fecha.Month + "/" + traza.Fecha.Year;
            controlTraza.lbl_hora.Text =  DateTime.Today.Add(traza.Hora).ToString("hh:mm tt",CultureInfo.InvariantCulture);
            controlTraza.cmb_claves.Text = traza.Clave;
            controlTraza.cmb_claves.TextChanged += new EventHandler(cmb_claves_TextChanged);
        
            controlTraza.Observaciones = traza.Observaciones;
            
            controlTraza.lbl_evento.Text = traza.Tipo.ToString();
            
            if (traza.Tipo == TipoTraza.N)
            {
                controlTraza.pnl_eventoFondo.BackColor = Color.OrangeRed;
                controlTraza.lbl_evento.BackColor = Color.OrangeRed;
                controlTraza.lbl_hora.Visible = false;
            }
            if (traza.Tipo == TipoTraza.E)
            {
                controlTraza.pnl_eventoFondo.BackColor = Color.YellowGreen;
                controlTraza.lbl_evento.BackColor = Color.YellowGreen;
                controlTraza.lbl_hora.Visible = true;
            }
            if (traza.Tipo == TipoTraza.S)
            {
                controlTraza.pnl_eventoFondo.BackColor = Color.DarkTurquoise;
                controlTraza.lbl_evento.BackColor = Color.DarkTurquoise;
                controlTraza.lbl_hora.Visible = true;
            }
            if (traza.Clave == "B")
            {
                controlTraza.cmb_claves.Enabled = false;
            }
            return controlTraza;
        }
      
        void cmb_claves_TextChanged(object sender, EventArgs e)
        {
            libreria_Incidencia_Dentro.ControlTraza control;
            if (onAnyClaveChange != null)
            {
                control = (libreria_Incidencia_Dentro.ControlTraza)((ComboBox)sender).Parent.Parent.Parent;
                onAnyClaveChange(this, new ArgumentClaveChange() { Clave = ((ComboBox)sender).Text, PosTrazaLista = control.PosTrazaLista, PosJornadaLista = this.PosJornadaLista });
            }
        }
        public void Pintar_Claves(ComboBox combo, List<String> claves)
        {
            combo.Items.Clear();
            combo.Items.AddRange(claves.ToArray());
            //if (listaClaves.Count > 0)
            //{
            //    ((ComboBox)sender).Items.Clear();
            //    foreach (string item in listaClaves)
            //    {
            //        if (item != null)
            //        {
            //            ((ComboBox)sender).Items.Add(item.Trim());
            //        }
            //    }
            //}
        }

        public void Pintar_Listado_Trazas()
        {
            if (listaTraza.Count >0)
            {       
                for (int k = listaTraza.Count - 1; k >= 0; k--)
                {
                    libreria_Incidencia_Dentro.ControlTraza control = Crear_Traza_Control("nombre", listaTraza[k]);
                    control.PosTrazaLista = k;
                    control.Location = new Point(0, control.Size.Height);
                    panel1.Controls.Add(control);
                    this.Pintar_Claves(control.cmb_claves, this.listaClaves);
                }
            }
        }
        #endregion
    }
}
