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
    public partial class TrabajadoresForm : Form
    {
        INCIDEControl icontrol;
        double horasMaximas;
        public TrabajadoresForm(INCIDEControl controlI)
        {
            InitializeComponent();
            this.icontrol = controlI;
            List<Area> listaAreas = this.icontrol.ObtenerAreasControlaPersona(this.icontrol.Logueado);
            if (listaAreas.Count > 1)
            {
                Area area = new Area();
                area.descripcion = "Todos";
                area.id_area = 0;
                listaAreas.Add(area);
            }
            LLenarComboAreas(cbxAreas, listaAreas);
        }


        public void LLenarComboAreas(ComboBox combo, List<Area> areas)
        {
            combo.DataSource = areas;
            combo.DisplayMember = "descripcion";
            combo.ValueMember = "id_area";
        }


        private void btnGenerarNomina_Click(object sender, EventArgs e)
        {
            try
            {
                List<object> param = new List<object>();
                List<Area> areas;
                if (cbxAreas.Text == "Todos")
                {
                    areas = this.icontrol.ObtenerAreasControlaPersona(this.icontrol.Logueado);
                }
                else
                {
                    Area area = icontrol.AreaC.GetArea(Convert.ToInt32(cbxAreas.SelectedValue));
                    areas = new List<Area>();
                    areas.Add(area);
                }
                if (areas.Count > 0)
                {
                    List<int> idAreas = new List<int>();
                    foreach (Area area in areas)
                        idAreas.Add(area.id_area);

                    param.Add(idAreas);
                    this.worker_listarPorAreas.RunWorkerAsync(param);
                    pnlSms.Visible = true;
                }
                else MessageBox.Show("No se pudo leer de la base de datos las áreas de las cuales el usuario logueado es responsable.");
            }
            catch (Exception msg)
            {
                MessageBox.Show("Error: " + msg.Message);
            }
        }

        private void worker_listarPorAreas_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<object> parms = e.Argument as List<object>;
                this.icontrol.GenerarExcelxAreaTrabajadores((List<int>)parms[0]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void worker_listarPorAreas_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnlSms.Visible = false;
        }


    }
}

