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
        private String observaciones = String.Empty;

        public Observaciones()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Guardar las observaciones
            this.observaciones = this.edtObservaciones.Text;
            this.Close();
        }

        public void setObservaciones(string observacion)
        {
            if (observacion != null)
            {
                this.observaciones = observacion;
                this.edtObservaciones.Text = this.observaciones;
            }
        }
        public string getObservaciones()
        {
            return this.observaciones;
        }
    }
}
