using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using INCIDE.ServiceReference1;
using System.Globalization;

namespace INCIDE
{
    public partial class TrazasHoy : Form
    {
        int totalHorasARestar = 3;
        String nombre;
        INCIDEControl icontrol;
        int expSeleccionado;
        public TrazasHoy(INCIDEControl icontrol, int exp, String nombre)
        {            
            InitializeComponent();
            this.expSeleccionado = exp;
            this.icontrol = icontrol;
            this.nombre = nombre;
            this.lbl_nombre.Text = this.nombre + ". Exp: " + this.expSeleccionado;
            this.PintarTrazas(this.GetTrazasJornadaActual(this.expSeleccionado));
        }
        public List<RegistroESpejo> GetTrazasJornadaActual(int exp)
        {
            DateTime fechaHoraInicioJornada = this.icontrol.SaberSiTocaTrabajar(exp,DateTime.Now);
            if(fechaHoraInicioJornada.Year == 1)//sino esta comprenddida entonces no esta trabajando actualmente            
                return new List<RegistroESpejo>();

            RegistroESpejo[] trazas = this.icontrol.GetTrazasRangoXListadoExpediente(fechaHoraInicioJornada.AddHours(totalHorasARestar * -1), DateTime.Now, exp);
            if (trazas != null && trazas.Count() > 0)
                return trazas.ToList();
            
            return new List<RegistroESpejo>();    
        }
        public void PintarTrazas(List<RegistroESpejo> trazas)
        {
            for (int i = trazas.Count - 1; i >= 0; i--)
            {
                TrazahoyComponent.Trazahoy traza_ = this.ConstruirTrazasHoyComponent(trazas[i].Fecha, trazas[i].Tipo);
                this.pnl_content.Controls.Add(traza_);
            }          
        }

        public TrazahoyComponent.Trazahoy ConstruirTrazasHoyComponent(DateTime fechaHora, String evento)
        {
            TrazahoyComponent.Trazahoy trazahoy1 = new TrazahoyComponent.Trazahoy();
            trazahoy1.Dock = System.Windows.Forms.DockStyle.Left;
            trazahoy1.Location = new System.Drawing.Point(0, 0);
            trazahoy1.Name = "trazahoy1";
            
            trazahoy1.TabIndex = 0;

            if (evento.ToUpper() == "E")
            {
                trazahoy1.lbl_evento.Text = "E";
                trazahoy1.pnl_evento.BackColor = Color.FromName("YellowGreen");
            }
            else
            {
                trazahoy1.lbl_evento.Text = "S";
                trazahoy1.pnl_evento.BackColor = Color.FromName("MediumTurquoise");
            }

            trazahoy1.lbl_fecha.Text = fechaHora.Day + "/" + fechaHora.Month + "/" + fechaHora.Year;
            trazahoy1.lbl_hora.Text = fechaHora.ToString("hh:mm:ss tt",CultureInfo.InvariantCulture);

            return trazahoy1;
        }


    }
}
