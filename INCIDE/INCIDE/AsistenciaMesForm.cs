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
    public partial class AsistenciaMesForm : Form
    {
        INCIDEControl icontrol;

        public AsistenciaMesForm(INCIDEControl controlI)
        {
            InitializeComponent();
            this.icontrol = controlI;
            this.edt_ano.Text = DateTime.Now.Year.ToString();
            List<Area> listaAreas = this.icontrol.ObtenerAreasControlaPersona(this.icontrol.Logueado);
            if (listaAreas.Count > 1)
            {
                Area area = new Area();
                area.descripcion = "Todos";
                area.id_area = 0;
                listaAreas.Add(area);
            }
            LLenarComboAreas(cmb_areas, listaAreas);
            this.LLenarComboMeses();
        }

        public void LLenarComboAreas(ComboBox combo, List<Area> areas)
        {
            combo.DataSource = areas;
            combo.DisplayMember = "descripcion";
            combo.ValueMember = "id_area";
        }

        public void LLenarComboMeses()
        {
            String[] Meses = Utils.GetMesesAno();
            for (int i = 0; i < Meses.Count() - 1; i++)
            {
                Meses[i] = Meses[i].Substring(0, 1).ToUpper() + Meses[i].Substring(1);
            }
            cmb_mes.Items.AddRange(Meses);
            int mesActual = DateTime.Now.Month;
            this.cmb_mes.SelectedIndex = mesActual-1;
        }

        private void btnGenerarListado_Click(object sender, EventArgs e)
        {
            if (this.cmb_mes.Text != string.Empty && this.edt_ano.Text != string.Empty && this.edt_ano.Text.Length == 4)
            {
                try
                {

                    int ano = Convert.ToInt32(this.edt_ano.Text);
                    int mes = this.cmb_mes.SelectedIndex;
                    List<int> idAreas = new List<int>();
                    List<Area> areas;
                    if (cmb_areas.Text == "Todos")
                    {
                        areas = this.icontrol.ObtenerAreasControlaPersona(this.icontrol.Logueado);
                        foreach (Area area in areas)
                            idAreas.Add(area.id_area);
                    }
                    else
                    {
                        Area area = icontrol.AreaC.GetArea(Convert.ToInt32(cmb_areas.SelectedValue));
                        idAreas.Add(area.id_area);
                    }
                    
                    List<object> param = new List<object>() { ano, mes + 1, idAreas};
                    this.worker_AsistenciaMes.RunWorkerAsync(param);
                    pnlSms.Visible = true;
                }
                catch (Exception msg)
                {
                    MessageBox.Show("Ocurrió un error. Es posible que no haya escrito un valor numérico de 4 dígitos en el campo año."
                                    + "\n" + "Si el valor en el campo año es correcto, comunique el error siguiente al personal de informática."
                                    + "\n" + "Error: " + msg.Message);
                    this.edt_ano.Text = DateTime.Now.Year.ToString();
                }
            }
            else MessageBox.Show("Debe llenar los campos año y mes. Tenga en cuenta que el año debe estar escrito en 4 cifras");
        }

        private void worker_AsistenciaMesDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<object> parms = e.Argument as List<object>;
                this.icontrol.GenerarExcelAsistenciaMes((int)parms[0], (int)parms[1], (List<int>) parms[2]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void worker_AsistenciaMesRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnlSms.Visible = false;
        }
    }
}
