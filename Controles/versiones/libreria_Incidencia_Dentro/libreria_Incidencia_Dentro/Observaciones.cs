using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace libreria_Incidencia_Dentro
{
    public partial class Observaciones : Form
    {
        public Observaciones()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Guardar las observaciones
            this.Close();
        }

        public void setObservaciones(string observaciones)
        {
            if(observaciones != null && observaciones != String.Empty )
                this.edtObservaciones.Text = observaciones;
        }
    }
}
