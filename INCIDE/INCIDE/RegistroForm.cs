using INCIDE.ControlEntidades;
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
    public partial class RegistroForm : Form
    {
        INCIDEControl icontrol;
        public RegistroForm(INCIDEControl icontrol)
        {
            InitializeComponent();
            this.icontrol = icontrol;
            this.LlenarComboAreas();
            date_fecha.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        private void LlenarComboAreas()
        {
            List<Area> areas = this.icontrol.ObtenerAreasControlaPersona(this.icontrol.Logueado).OrderBy(p => p.descripcion).ToList();
            if(areas.Count > 1)
            {
                Area area = new Area();
                area.descripcion = "Todos";
                area.id_area = 0;
                areas.Add(area);
            }            
            cmb_areas.DataSource = areas;
            cmb_areas.DisplayMember = "descripcion";
            cmb_areas.ValueMember = "id_area";
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cmb_areas.Text != String.Empty && this.date_fecha.Value != null)
                {
                    DateTime inicio = this.date_fecha.Value;
                    List<object> param = new List<object>();
                    List<Area> areas;
                    if (cmb_areas.Text == "Todos")
                    {
                        areas = this.icontrol.ObtenerAreasControlaPersona(this.icontrol.Logueado);
                    }
                    else
                    {
                        Area area = icontrol.AreaC.GetArea(Convert.ToInt32(cmb_areas.SelectedValue));
                        areas = new List<Area>();
                        areas.Add(area);
                    }
                    if (areas.Count > 0)
                    {
                        List<int> idAreas = new List<int>();
                        foreach (Area area in areas)
                            idAreas.Add(area.id_area);

                        param.Add(idAreas);
                        param.Add(inicio);
                        this.worker_registrosPorArea.RunWorkerAsync(param);
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

        private void worker_registrosPorArea_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> parms = e.Argument as List<object>;
            List<int> areas=(List<int>)parms[0];
            DateTime inicio = (DateTime)parms[1];
            this.icontrol.GenerarExcelxAreaRegistros(areas, inicio);
        }

        private void worker_registrosPorArea_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.pnl_wait.Visible = false;
        }
    }
}
