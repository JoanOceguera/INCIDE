using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libreria_Incidencia_Dentro.Clases;

namespace libreria_Incidencia_Dentro
{


    public partial class ControlTraza : UserControl
    {
        private int posTrazaLista; // posicion de la Traza en la lista dentro del componente Jornada
        public delegate void ManipuladorEventoOnObservacionesChange(object sender, EventArgs arg);
        public event ManipuladorEventoOnObservacionesChange onObservacionesChange;

        public int PosTrazaLista
        {
            get { return posTrazaLista; }
            set { posTrazaLista = value; }
        }
        private string observaciones;

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
        
        public ControlTraza()
        {
            InitializeComponent();
        }

        private void cmb_claves_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }     

        private void OpenObservaciones()
        {
            Observaciones forma = new Observaciones();
            forma.setObservaciones(this.observaciones);
            Point punto = new Point(Cursor.Position.X + 10,Cursor.Position.Y - 56);
            forma.Location = punto;
            
            forma.ShowDialog();
            this.Observaciones = forma.getObservaciones();

            if (onObservacionesChange != null)
            {
                onObservacionesChange(this, new ArgumentClaveChange() { Observacion = this.Observaciones });
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.OpenObservaciones();
        }
    }
}
