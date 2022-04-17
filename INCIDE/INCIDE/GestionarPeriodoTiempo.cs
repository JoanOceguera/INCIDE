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
    public partial class GestionarPeriodoTiempo : Form
    {

        Periodo_tiempo periodo = null;
        INCIDEControl icontrol;

        public Periodo_tiempo Periodo
        {
            get { return periodo; }
            set { periodo = value; }
        }
        public void Validar_Hora()
        {
            if (edtHora.Text != "")
            {
                string horas = edtHora.Text;
                if (Convert.ToInt32(horas) > 12)
                {
                    edtHora.Text = "";
                    MessageBox.Show("La hora a escribir tiene que ser menor o igual que 12.", "Alerta:");
                }
            }
            if (edtMinutos.Text != "")
            {
                string minutos = edtMinutos.Text;

                if (Convert.ToInt32(minutos) > 59)
                {
                    edtMinutos.Text = "";
                    MessageBox.Show("Los minutos a escribir tienen que ser menor o igual que 59.", "Alerta:");
                }
            }
                
            
        }
        public GestionarPeriodoTiempo(INCIDEControl pIcontrol, Periodo_tiempo pperiodo, bool editable)
        {
            InitializeComponent();
            if (pperiodo == null)
            {
                this.icontrol = pIcontrol;
            }
            else //si el periodo distinto de null entonces cargar en componentes los valores del periodo
            {
                Periodo = pperiodo;
                edtNombre.Text = Periodo.descripcion;
                edtFrecuencia.Text = Periodo.dias_periodo.ToString();
                edtHora.Text = Periodo.hora_entrada.ToString().Split(":".ToCharArray())[0];
                edtMinutos.Text = edtMinutos.Text = Periodo.hora_entrada.ToString().Split(":".ToCharArray())[1];
                dateFecha.Text = Periodo.fecha_inicio.ToString();
                edtMinuto.Text = Periodo.minuto_almuerzo.ToString();
                edtCanTJornada.Text = Periodo.cantidad_horas.ToString();
                if (Periodo.activo == 1) { checkActivoM.Checked = true; } else { checkActivoM.Checked = false; }

            }
            if (!editable)
            {
                edtNombre.Enabled = false;
                edtFrecuencia.Enabled = false;
                edtHora.Enabled = false;
                edtMinutos.Enabled = false;
                dateFecha.Enabled = false;
                edtMinuto.Enabled = false;
                edtCanTJornada.Enabled = false;
                checkActivoM.Enabled = false;
            }

        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (edtNombre.Text != "" && edtFrecuencia.Text != "" && edtHora.Text != "" && edtMinutos.Text != "" && dateFecha.Value != null && edtMinuto.Text != "" && edtCanTJornada.Text != "")
            {
                periodo = new Periodo_tiempo();
                periodo.descripcion = edtNombre.Text.Trim();
                periodo.dias_periodo = Convert.ToInt32(edtFrecuencia.Text);
                periodo.hora_entrada = TimeSpan.Parse(edtHora.Text + ":" + edtMinutos.Text + ":" + "00");
                periodo.fecha_inicio = dateFecha.Value;
                periodo.minuto_almuerzo = Convert.ToInt32(edtMinuto.Text);
                periodo.cantidad_horas = Convert.ToInt32(edtCanTJornada.Text);
                if (checkActivoM.Checked) { periodo.activo = Utils.SI; } else { periodo.activo = Utils.NO; }
                Periodo = periodo;
                this.Close();

            }
            else
            {
                MessageBox.Show("Debe llenar obligatoriamente todos los campos menos el activo.", "Alerta:");
            }
        }

        private void edtHora_TextChanged(object sender, EventArgs e)
        {
            Validar_Hora();
        }

     


    }
}
