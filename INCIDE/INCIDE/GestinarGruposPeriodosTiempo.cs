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
    public partial class GestinarGruposPeriodosTiempo : Form
    {
        public Periodo_tiempo nuevoP;
        public  List<Grupo> listaGrupos;
        public Grupo grupoModificar;
        public INCIDEControl icontrol;
        Periodo_tiempoControl periodoControl;

        public GestinarGruposPeriodosTiempo(INCIDEControl pIcontrol)
        {
            InitializeComponent();
            this.icontrol = pIcontrol;
            this.periodoControl = new Periodo_tiempoControl();
            LlenarlistPTATodos(periodoControl.Periodos_tiempo);
        }

        public void Cargar_Combo(ComboBox combo, List<Grupo> Grupos)
        {

            combo.DataSource = Grupos;
            combo.DisplayMember = "descripcion";
            combo.ValueMember = "id_Grupo";
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<Periodo_tiempo> pt = new List<Periodo_tiempo>();
            if (edtNombreGrupo.Text != "" && listPTA.Items.Count > 0)
            {
                pt = this.GetPeriodosTFromListb(listPTA);
                if (pt.Count > 0)
                    if (icontrol.AdicionarGrupo(edtNombreGrupo.Text, Convert.ToInt32(checkActivo.Checked), pt))
                    {
                        MessageBox.Show("Los datos fueron adicionados correctamente.");
                        this.edtNombreGrupo.Text = string.Empty;
                        listPTA.Items.Clear();
                        this.checkActivo.Checked = false;
                    }
                    else MessageBox.Show("Debe adicionar al menos un periodo de tiempo al grupo que está construyendo.");
            }
            else
            {
                MessageBox.Show("Debe llenar obligatoriamente el nombre del grupo y agregar al menos un período de tiempo.", "Alerta:");
            }

        }

        private List<Periodo_tiempo> GetPeriodosTFromListb(ListBox listb)
        {
            List<Periodo_tiempo> pt = new List<Periodo_tiempo>();
            foreach (Periodo_tiempo item in listb.Items)
            {
                pt.Add(item);
            }
            return pt;
        }

        private void LLenarCmbGrupos()
        {
            this.cmb_grupos.Items.Clear();
            foreach (Grupo grup in this.icontrol.ObtenerGruposTodos())
            {
                this.cmb_grupos.Items.Add(grup);
            }
            this.cmb_grupos.ValueMember = "id_grupo";
            this.cmb_grupos.DisplayMember = "nombre";
            if(this.cmb_grupos.Items.Count > 0)
                this.cmb_grupos.SelectedIndex = 0;
        }
        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (edt_nombreGrupo.Text != "" && listPT.Items.Count > 0)
            {
                if (this.icontrol.EditarGrupo(this.edt_nombreGrupo.Text, Convert.ToInt32(this.checkActivoM.Checked), this.GetPeriodosTFromListb(this.listPT), (Grupo)this.cmb_grupos.SelectedItem))
                {
                    MessageBox.Show("Los datos fueron editados correctamente.");
                } 
            }
            else
            {
                MessageBox.Show("Debe llenar obligatoriamente el nombre del grupo y agregar al menos un período de tiempo.", "Alerta:");
            }
        }

        private void btnCrearP_Click(object sender, EventArgs e)
        {
            Periodo_tiempo periodo = null;
            using (var formp = new GestionarPeriodoTiempo(this.icontrol, periodo, true))
            {
                formp.ShowDialog();
                if (formp.Periodo != null)
                {
                    this.listPTA.Items.Add(formp.Periodo);
                    this.listPTA.DisplayMember = "descripcion";
                }

            }
        }

        private void btnEditarP_Click(object sender, EventArgs e)
        {
            if (listPTA.SelectedIndex >=0)
            {
                nuevoP = (Periodo_tiempo)listPTA.SelectedItem;
                Periodo_tiempo per = AbrirEditPeriodo(nuevoP);
                if(per != null)
                {
                    nuevoP.activo = per.activo;
                    nuevoP.cantidad_horas = per.cantidad_horas;
                    nuevoP.descripcion = per.descripcion;
                    nuevoP.dias_periodo = per.dias_periodo;
                    nuevoP.fecha_inicio = per.fecha_inicio;
                    nuevoP.hora_entrada = per.hora_entrada;
                    nuevoP.minuto_almuerzo = per.minuto_almuerzo;
                    periodoControl.Editar(nuevoP);
                    this.listPTA.Items.Remove(listPTA.SelectedItem);
                    this.listPTA.Items.Add(nuevoP);
                    this.listPTA.DisplayMember = "descripcion";

                }
            }            
        }
        public Periodo_tiempo AbrirEditPeriodo(Periodo_tiempo nuevoP)
        {
            using (var formp = new GestionarPeriodoTiempo(this.icontrol, nuevoP, true))
            {
                formp.ShowDialog();
                return formp.Periodo;
            }            
        }

        public Periodo_tiempo AbrirVerPeriodo(Periodo_tiempo nuevoP)
        {
            using (var formp = new GestionarPeriodoTiempo(this.icontrol, nuevoP, false))
            {
                
                formp.ShowDialog();
                return formp.Periodo;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((TabControl)sender).SelectedIndex == 1)
                this.LLenarCmbGrupos();
        }

        private void cmb_grupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LLenarCamposGrupo((Grupo)this.cmb_grupos.SelectedItem);
        }

        public void LLenarCamposGrupo(Grupo grupo)
        {
            this.edt_nombreGrupo.Text = grupo.nombre;
            checkActivoM.Checked = (grupo.activo == Utils.SI) ? true : false;
            List<Periodo_tiempo> dnl = this.icontrol.GetPeriodosDeTiempoDeGrupo(grupo.id_Grupo);
            List<Periodo_tiempo> periodosNoGrupo = this.icontrol.GetPeriodosDeTiempoNoDeGrupo(grupo.id_Grupo);
            this.LlenarlistPT(dnl);
            this.LlenarlistPTTodos(periodosNoGrupo);
        }

        public void LlenarlistPT(List<Periodo_tiempo> periodos)
        {
            this.listPT.Items.Clear();
            foreach (Periodo_tiempo periodo in periodos)
            {
                this.listPT.Items.Add(periodo);
            }
            this.listPT.DisplayMember = "descripcion";
        }

        public void LlenarlistPTTodos(List<Periodo_tiempo> periodos)
        {
            this.listPTTodos.Items.Clear();
            foreach (Periodo_tiempo periodo in periodos)
            {
                this.listPTTodos.Items.Add(periodo);
            }
            this.listPTTodos.DisplayMember = "descripcion";
        }

        public void LlenarlistPTATodos(List<Periodo_tiempo> periodos)
        {
            this.listPTATodos.Items.Clear();
            foreach (Periodo_tiempo periodo in periodos)
            {
                this.listPTATodos.Items.Add(periodo);
            }
            this.listPTATodos.DisplayMember = "descripcion";
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (listPT.SelectedIndex >= 0)
            {
                Periodo_tiempo nuevoPer = (Periodo_tiempo)listPT.SelectedItem;
                Periodo_tiempo per = AbrirEditPeriodo(nuevoPer);
                if (per != null)
                {
                    nuevoPer.activo = per.activo;
                    nuevoPer.cantidad_horas = per.cantidad_horas;
                    nuevoPer.descripcion = per.descripcion;
                    nuevoPer.dias_periodo = per.dias_periodo;
                    nuevoPer.fecha_inicio = per.fecha_inicio;
                    nuevoPer.hora_entrada = per.hora_entrada;
                    nuevoPer.minuto_almuerzo = per.minuto_almuerzo;
                    periodoControl.Editar(nuevoPer);
                    this.listPT.Items.Remove(listPT.SelectedItem);
                    this.listPT.Items.Add(nuevoPer);
                    this.listPT.DisplayMember = "descripcion";
                }
            }  
        }

        private void btnBorrarP_Click(object sender, EventArgs e)
        {
            this.listPTA.Items.Remove(this.listPTA.SelectedItem);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            this.listPT.Items.Remove(this.listPT.SelectedItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Periodo_tiempo periodo = null;
            using (var formp = new GestionarPeriodoTiempo(this.icontrol, periodo, true))
            {
                formp.ShowDialog();
                if (formp.Periodo != null)
                {
                    this.listPT.Items.Add(formp.Periodo);
                    this.listPT.DisplayMember = "descripcion";
                }

            }
        }

        private void listPTATodosFocus(object sender, EventArgs e)
        {
            btnCrearP.Enabled = false;
            btnEditarP.Enabled = false;
            btnBorrarP.Enabled = false;
        }

        private void listPTATodosNoFocus(object sender, EventArgs e)
        {
            btnCrearP.Enabled = true;
            btnEditarP.Enabled = true;
            btnBorrarP.Enabled = true;
        }

        private void listPTTodosFocus(object sender, EventArgs e)
        {
            button1.Enabled = false;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
        }

        private void listPTTodosNoFocus(object sender, EventArgs e)
        {
            button1.Enabled = true;
            btnModificar.Enabled = true;
            btnBorrar.Enabled = true;
        }

        private void LefttoRightEditar_Click(object sender, EventArgs e)
        {
            if (listPT.SelectedIndex >= 0)
            {
                this.listPTTodos.Items.Add(listPT.SelectedItem);
                listPT.Items.Remove(listPT.SelectedItem);
                this.listPT.DisplayMember = "descripcion";
                this.listPTTodos.DisplayMember = "descripcion";
            }
        }

        private void RighttoLeftEditar_Click(object sender, EventArgs e)
        {
            if (listPTTodos.SelectedIndex >= 0)
            {
                this.listPT.Items.Add(listPTTodos.SelectedItem);
                listPTTodos.Items.Remove(listPTTodos.SelectedItem);
                this.listPT.DisplayMember = "descripcion";
                this.listPTTodos.DisplayMember = "descripcion";
            }
        }

        private void LefttoRight_Click(object sender, EventArgs e)
        {
            if (listPTA.SelectedIndex >= 0)
            {
                this.listPTATodos.Items.Add(listPTA.SelectedItem);
                listPTA.Items.Remove(listPTA.SelectedItem);
                this.listPTA.DisplayMember = "descripcion";
                this.listPTATodos.DisplayMember = "descripcion";
            }
        }

        private void RighttoLeft_Click(object sender, EventArgs e)
        {
            if (listPTATodos.SelectedIndex >= 0)
            {
                this.listPTA.Items.Add(listPTATodos.SelectedItem);
                listPTATodos.Items.Remove(listPTATodos.SelectedItem);
                this.listPTA.DisplayMember = "descripcion";
                this.listPTATodos.DisplayMember = "descripcion";
            }
        }

        private void btnVerP_Click(object sender, EventArgs e)
        {
            if (listPTA.SelectedIndex >= 0 || listPTATodos.SelectedIndex >= 0)
            {
                if (listPTA.SelectedIndex >= 0)
                {
                    nuevoP = (Periodo_tiempo)listPTA.SelectedItem;
                    Periodo_tiempo per = AbrirVerPeriodo(nuevoP);
                    listPTA.SelectedItem = null;
                }
                else
                {
                    nuevoP = (Periodo_tiempo)listPTATodos.SelectedItem;
                    Periodo_tiempo per = AbrirVerPeriodo(nuevoP);
                    listPTATodos.SelectedItem = null;
                }
                
            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (listPT.SelectedIndex >= 0 || listPTTodos.SelectedIndex >= 0)
            {
                if (listPT.SelectedIndex >= 0)
                {
                    nuevoP = (Periodo_tiempo)listPT.SelectedItem;
                    Periodo_tiempo per = AbrirVerPeriodo(nuevoP);
                    listPT.SelectedItem = null;
                }
                else
                {
                    nuevoP = (Periodo_tiempo)listPTTodos.SelectedItem;
                    Periodo_tiempo per = AbrirVerPeriodo(nuevoP);
                    listPTTodos.SelectedItem = null;
                }

            }
        }
    }
}
