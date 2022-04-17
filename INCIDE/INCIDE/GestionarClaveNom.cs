using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using INCIDE.ControlEntidades;
using INCIDE.ServiceReference1;

namespace INCIDE
{
    public partial class GestionarClaveNom : Form
    {
        INCIDEControl icontrol;
        List<Clave_nom> Claves;
        Clave_nom ClaveModificar;

        public GestionarClaveNom(INCIDEControl pIcontrol)
        {
            InitializeComponent();
            icontrol = pIcontrol;
            Cargar_Lista_Claves();

        }
        private void btnReporte_Click(object sender, EventArgs e)
        {
            int activo = Utils.NO;
            int planificable = Utils.NO;
            int descuenta = Utils.NO;
            if (edtCod.Text != "" && edtDescripcion.Text != "")
            {
                if (edtCod.Text.Count() <= 4)
                {
                    if (checkActivo.Checked) { activo = Utils.SI; }
                    if (checkPlnificable.Checked) { planificable = Utils.SI; }
                    if (checkDescuenta.Checked) { descuenta = Utils.SI; }

                    Clave_nom clave = icontrol.CrearNuevaClave(edtCod.Text.Trim(), edtDescripcion.Text.Trim(), activo, planificable, descuenta);
                    if (!icontrol.Econtrar_Clave(clave))
                    {
                        if (icontrol.AdicionarClave(clave))
                        {
                            edtCod.Text = edtDescripcion.Text = "";
                            checkDescuenta.Checked = checkPlnificable.Checked = checkActivo.Checked = false;
                            MessageBox.Show("Se guardó correctamente la clave en la base de datos.", "Información:");
                            Cargar_Lista_Claves();
                            
                        }
                        else
                        {
                            MessageBox.Show("Ocurrió un error insertando la clave en la base de datos.", "Alerta:");
                        }
                    }
                    else
                    {
                        edtCod.Text = edtDescripcion.Text = "";
                        checkDescuenta.Checked = checkPlnificable.Checked = checkActivo.Checked = false;
                        
                        MessageBox.Show("Ya existe una clave con ese código.", "Alerta:");
                    }
                    
                }
                else
                {
                    MessageBox.Show("El código para identificar la Clave puede tener hasta 4 caractéres.", "Alerta:");
                }
                
            }
            else
            {
                MessageBox.Show("Debe llenar los campos de descripción y código.", "Alerta:");
            }
        }
        public void Cargar_Lista_Claves()
        {
            Claves = icontrol.Listado_Claves();
            Claves.OrderBy(t => t.descripcion);
            cbxDescripcion.DataSource = null;
            cbxDescripcion.Text = "";
            LLenarComboClaves(cbxDescripcion, Claves);
        }
        public void LLenarComboClaves(ComboBox combo, List<Clave_nom> Claves)
        {
            Claves.OrderBy(t => t.descripcion);
            combo.DataSource = Claves;
            combo.DisplayMember = "descripcion";
            combo.ValueMember = "id_clave";
        }
        private void cbxDescripcion_SelectedValueChanged(object sender, EventArgs e)
        {
            this.ClaveModificar = (Clave_nom)this.cbxDescripcion.SelectedItem;
            this.SetearMiembrosClaves(this.ClaveModificar);
        }
        private void SetearMiembrosClaves(Clave_nom clave_nom)
        {
            if (clave_nom != null)
            {
                    edtDescripcionM.Text = clave_nom.descripcion.ToString();
                    edtCodigoM.Text = clave_nom.codigo.ToString();
                    if (clave_nom.activo == Utils.SI) { checkA.Checked = true; } else { checkA.Checked = false; }
                    if (clave_nom.planificable == Utils.SI) { checkP.Checked = true; } else { checkP.Checked = false; }
                    if (clave_nom.descuenta == Utils.SI) { checkD.Checked = true; } else { checkD.Checked = false; }
                    if (clave_nom.codigo == "VC" || clave_nom.codigo == "CM" || clave_nom.codigo == "SI" || clave_nom.codigo == "AI" || clave_nom.codigo == "TI")
                    {
                        edtCodigoM.Enabled = edtDescripcionM.Enabled = checkA.Enabled = checkD.Enabled = checkP.Enabled = false;
                    }
                    else
                    {
                        edtCodigoM.Enabled = edtDescripcionM.Enabled = checkA.Enabled = checkD.Enabled = checkP.Enabled = true;
                    }
                
            }
            else
            {
                if (cbxDescripcion.DataSource != null)
                {
                    MessageBox.Show("No se pudo mostrar los datos de la clave.", "Alerta:");
                } 
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (edtCodigoM.Text != "" && edtDescripcionM.Text != "")
            {
                if (edtCod.Text.Count() <= 4)
                {
                    if (checkA.Checked) { this.ClaveModificar.activo = Utils.SI; } else { this.ClaveModificar.activo = Utils.NO; }
                    if (checkP.Checked) { this.ClaveModificar.planificable = Utils.SI; } else { this.ClaveModificar.planificable = Utils.NO; }
                    if (checkD.Checked) { this.ClaveModificar.descuenta = Utils.SI; } else { this.ClaveModificar.descuenta = Utils.NO; }
                    this.ClaveModificar.descripcion = edtDescripcionM.Text.Trim();
                    this.ClaveModificar.codigo = edtCodigoM.Text.Trim();

                    
                    
                        if (icontrol.Modificar_Clave(this.ClaveModificar))
                        {
                            edtCodigoM.Text = edtDescripcionM.Text = cbxDescripcion.Text = "";
                            checkD.Checked = checkP.Checked = checkA.Checked = false;
                            MessageBox.Show("Se Modificó correctamente la clave en la base de datos.", "Información:");
                        }
                        else
                        {
                            MessageBox.Show("Ocurrió un error modificando la clave en la base de datos.", "Alerta:");

                        }
                    
                }
                else
                {
                    MessageBox.Show("El código para identificar la Clave puede tener hasta 4 caractéres.", "Alerta:");
                }


            }
            else
            {
                MessageBox.Show("Debe llenar los campos de descripción y código.", "Alerta:");
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

       

        

    }
}
