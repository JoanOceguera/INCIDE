using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace libreria_Incidencia_Dentro
{
    public partial class ControlTraza : UserControl
    {
        private int posTrazaLista; // posicion de la Traza en la lista dentro del componente Jornada

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
            Observaciones = String.Empty;
        }

        private void cmb_claves_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

     

        private void lblobserv_Click(object sender, EventArgs e)
        {
            Observaciones forma = new Observaciones();
            forma.setObservaciones(this.Observaciones);
            forma.ShowDialog();
        }
    }
}
