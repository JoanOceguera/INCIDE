using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace INCIDE
{
    public partial class AsistenciaForm : Form
    {
        INCIDEControl icontrol;
        public AsistenciaForm(INCIDEControl icontrol)
        {
            InitializeComponent();
            this.icontrol = icontrol;
            date_fecha.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
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


        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.date_fecha.Value != null)
                {
                    DateTime inicio = this.date_fecha.Value;
                    List<object> param = new List<object>();
                    param.Add(inicio);                    

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
                        this.worker_asistencia.RunWorkerAsync(param);
                        this.pnl_wait.Visible = true;
                    }
                    else MessageBox.Show("No se pudo leer de la base de datos las áreas de las cuales el usuario logueado es responsable.");

                }
                else MessageBox.Show("No debe dejar campos vacios.");

                
            }
            catch (Exception msg)
            {
                MessageBox.Show("Error: " + msg.Message);
            }
        }

        private void worker_asistencia_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<object> parms = e.Argument as List<object>;
                DateTime inicio = (DateTime)parms[0];
                this.icontrol.GenerarExcelAsistenciaDia(inicio, (List<int>)parms[1]);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void worker_asistencia_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.pnl_wait.Visible = false;
        }
    }
}
