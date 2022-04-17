using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlIncidenciaFuera;
using ControlIncidenciaFuera.Clases;
using INCIDE.ClasesNegocio;
using INCIDE.ServiceReference1;
using libreria_Incidencia_Dentro.Clases;

namespace INCIDE.ControlesVisuales
{
    public partial class ControlIncidencia : UserControl
    {        
        PersonaNegocio personaN;
        INCIDEControl icontrol;
        List<String> claves;
        Button btn_focus = new Button();

        public ControlIncidencia(INCIDEControl icontrol)
        {
            InitializeComponent();

            this.icontrol = icontrol;
            this.personaN = new PersonaNegocio();
            this.claves = this.icontrol.Obtener_ClavesActivas();
            this.btn_focus.Name = "btn_focus";
            this.btn_focus.Width = 1;
            this.btn_focus.Height = 1;
        }

        public ControlIncidenciaFuera.ControlJornada Crear_Jornada(string Nombre, Jornada nuevaJornada,List<String> claves)
        {
            
            
            ControlIncidenciaFuera.ControlJornada nuevoControl = new ControlJornada();
            nuevoControl.AutoScroll = true;
            nuevoControl.Location = new System.Drawing.Point(0, 0);
            nuevoControl.Name = Nombre;
            nuevoControl.PosJornadaLista = 0;
            nuevoControl.Size = new System.Drawing.Size(809, 103);
            nuevoControl.TabIndex = 0;
            nuevoControl.onAnyClaveChange += new ControlJornada.ManipuladorEventoOnAnyClaveChange(nuevoControl_onAnyClaveChange);
            nuevoControl.onObservacionesChange += new ControlJornada.ManipuladorEventoOnObservacionesChange(nuevoControl_onObservacionesChange);
            if (nuevaJornada.ListaTraza.Count >0 )
            {
                //List<string> lista1 = new List<string>() { "TT", "TT", "TT", "TT", "TT", "TT" };// ESTA ES LA LISTA DE CLAVES QUE SE INSERTAN EN EL COMBO CLAVES
                nuevoControl.setValoresIniciales(nuevaJornada.ListaTraza, claves);
                nuevoControl.Width = nuevaJornada.ListaTraza.Count * 100;
            }          
                      
            return nuevoControl;
        }
        void nuevoControl_onObservacionesChange(object sender, EventArgs arg)
        {
            int posJornada = Convert.ToInt32(((ArgumentClaveChange)arg).PosJornadaLista);
            int posTraza = Convert.ToInt32(((ArgumentClaveChange)arg).PosTrazaLista);
            String observ = ((ArgumentClaveChange)arg).Observacion.ToString();

            if (posJornada != -1 && posTraza != -1)
                this.personaN.ListaJornada[posJornada].ListaTraza[posTraza].Observaciones = observ;  
        }
        void nuevoControl_onAnyClaveChange(object sender, EventArgs arg)
        {
            int posJornada = Convert.ToInt32(((ArgumentClaveChange)arg).PosJornadaLista);
            int posTraza = Convert.ToInt32(((ArgumentClaveChange)arg).PosTrazaLista);
            String clave = ((ArgumentClaveChange)arg).Clave.ToString();

            if (posJornada != -1 && posTraza != -1 && clave != String.Empty)
                this.personaN.ListaJornada[posJornada].ListaTraza[posTraza].Clave = clave;          
        }
        public void PonerNumeroExp(String exp)
        {
            if (exp != "")
            {
                this.edit_edtk.Text = exp;
                this.edit_edtk.ReadOnly = true;
                PersonalRH perso = this.icontrol.GetPersonalRHPersonaFromService(exp);
                if (perso != null)
                    this.edtNombre.Text = perso.Nombre + " " + perso.Apellido1 + " " + perso.Apellido2;
               // else this.edtNombre.Text = String.Empty;
            }
        }

        private void edit_edtk_TextChanged(object sender, EventArgs e)
        {
            this.InitLoadIncidencias();
        }

        private void InitLoadIncidencias()
        {
            if (!this.worker_cargarIncidencias.IsBusy)
            {
                this.MostrarLoading();
                this.worker_cargarIncidencias.RunWorkerAsync();
            }
        }
        
        public RegistroESpejo[] GetTrazasRangoXListadoExpediente(DateTime fechaInicio, DateTime fechaFin, int exp)
        {
            return this.icontrol.GetTrazasRangoXListadoExpediente(fechaInicio, fechaFin, exp);
            /*
            RegistroESpejo[] trazasIncidencias; //se obtiene del uso del servicio web
                        
            var serv = new Service1Client();
            int[] exped = new int[] { exp };
            trazasIncidencias = serv.TrazasEnRangoxListadoExp(fechaInicio, fechaFin, exped);
            return trazasIncidencias;  */          
        }

        public void CargarIncidencias(int exp)
        {
            DateTime fechaInicio = new DateTime();
            DateTime fechaFin = new DateTime();

            DateTime cerrado = DateTime.Now.AddMonths(-1);
            cerrado = new DateTime(cerrado.Year, cerrado.Month, 1);

            if (!icontrol.EstaCerrado(cerrado))//sino esta cerrado el mes anterior
            {
                fechaInicio = cerrado;
                fechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1,23,59,59).AddDays(-1);
            }
            else            
                if (!icontrol.EstaCerrado(DateTime.Now))// si no esta cerrado el actual
                {
                    fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    if (DateTime.Now.Day == 1)
                        fechaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    else
                    {
                        DateTime fin = DateTime.Now.AddDays(-1);
                        fechaFin = new DateTime(fin.Year, fin.Month, fin.Day, 23, 59, 59);
                    }
                }
                else MessageBox.Show("No existen incidencias para el expediente " + exp.ToString());
            
            if (fechaInicio <= fechaFin)
            {
                PintarFechaInicio(fechaInicio.Day + "/" + fechaInicio.Month + "/" + fechaInicio.Year);
                PintarFechaFin(fechaFin.Day + "/" + fechaFin.Month + "/" + fechaFin.Year);
                RegistroESpejo[] trazasIncidencias = this.GetTrazasRangoXListadoExpediente(fechaInicio, fechaFin, exp);
                this.personaN = this.icontrol.CargarIncidencias(exp, fechaInicio, fechaFin, trazasIncidencias.ToList());
                if (this.personaN != null)
                {
                   // this.personaN.ListaJornada.RemoveAt(personaN.ListaJornada.Count-1);                
                    this.PintarJornada(this.personaN.ListaJornada, this.claves);
                }
                else MessageBox.Show("Ocurrió un error cargando la información de la persona con expediente: " + exp.ToString());
            }
            else MessageBox.Show("La fecha de inicio debe ser menor a la fecha final");           
        }

        public void PintarJornada(List<Jornada> listaJornada,List<String> claves)
        {            
            this.PintarJornada_pnl_contenedorJornadas(listaJornada, claves);
        }


        delegate void PintarFechaInicio_delegado(string fecha);
        public void PintarFechaInicio(string fecha)
        {
            if (this.lblFechaI.InvokeRequired)
            {
                PintarFechaInicio_delegado d = new PintarFechaInicio_delegado(PintarFechaInicio);
                this.lblFechaI.Invoke(d, new object[] { fecha });
            }
            else
            {
                lblFechaI.Text = fecha;
            }
        }
        delegate void PintarFechaFin_delegado(string fecha);
        public void PintarFechaFin(string fecha)
        {
            if (this.lblFechaI.InvokeRequired)
            {
                PintarFechaFin_delegado d = new PintarFechaFin_delegado(PintarFechaFin);
                this.lblFechafin.Invoke(d, new object[] { fecha });
            }
            else
            {
                lblFechafin.Text = fecha;
            }
        }


        delegate void PintarJornada_delegado(List<Jornada> listaJornada, List<String> claves);
        public void PintarJornada_pnl_contenedorJornadas(List<Jornada> listaJornada, List<String> claves)
        {
            if (this.pnl_contenedorJornadas.InvokeRequired)
            {
                PintarJornada_delegado d = new PintarJornada_delegado(PintarJornada_pnl_contenedorJornadas);
                this.pnl_contenedorJornadas.Invoke(d, new object[] { listaJornada, claves });
            }
            else
            {
                this.pnl_contenedorJornadas.Controls.Clear();
                for (int i = 0; i < listaJornada.Count; i++)
                {
                    ControlIncidenciaFuera.ControlJornada Control = Crear_Jornada("nuevo", listaJornada[i], claves);
                    Control.PosJornadaLista = i;
                    Control.Location = new Point(0, Control.Size.Height * this.pnl_contenedorJornadas.Controls.Count);
                    this.pnl_contenedorJornadas.Controls.Add(Control);
                }
                          
                this.pnl_contenedorJornadas.Controls.Add(this.btn_focus);
            }
        }

        private void worker_cargarIncidencias_DoWork(object sender, DoWorkEventArgs e)
        {
            if(this.edit_edtk.Text != String.Empty)
                this.CargarIncidencias(Convert.ToInt32(this.edit_edtk.Text));
        }

        private void OcultarLoading()
        {
            this.pnl_loading.Visible = false;
        }
        private void MostrarLoading()
        {
            this.pnl_loading.Visible = true;
        }

        private void worker_cargarIncidencias_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.OcultarLoading();
        }

        private void btn_filtrar_Click(object sender, EventArgs e)
        {
            this.InitLoadIncidencias();
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            bool salvadoOk = false;
            try
            {
                if (this.personaN != null)
                    salvadoOk = this.icontrol.SalvarIncidencias(this.personaN);
                else MessageBox.Show("Ocurrió un problema intentando salvar la información. La información no será salvada.");
                if (salvadoOk) 
                    MessageBox.Show("Los datos fueron salvados satisfactoriamente.");
                else MessageBox.Show("Ocurrió un error salvando los datos.");
                this.InitLoadIncidencias();
            }
            catch (Exception msg)
            {
                MessageBox.Show("Ocurrió un problema intentando salvar la información. " + msg.Message);                
            }
        }

        private void pnl_contenedorJornadas_Scroll(object sender, ScrollEventArgs e)
        {
            this.btn_focus.Focus();
        }

        private void pnl_contenedorJornadas_Click(object sender, EventArgs e)
        {
            this.btn_focus.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TrazasHoy thform = new TrazasHoy(this.icontrol, Convert.ToInt32(this.edit_edtk.Text), this.edtNombre.Text);
            thform.ShowDialog();
        }
    }
}
