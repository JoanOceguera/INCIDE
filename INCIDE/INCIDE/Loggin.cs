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
    public partial class Loggin : Form
    {
        INCIDEControl icontrol;
        public Loggin()
        {
            this.icontrol = new INCIDEControl();            
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.txt_exp.Text != String.Empty && this.txt_pass.Text != String.Empty)
            {
                try
                {
                    try{Convert.ToInt32(this.txt_exp.Text);}
                    catch (Exception){throw new Exception("El campo para el número de expediente debe contener solo caracteres numéricos.");}

                    if (this.icontrol.CheckearCredenciales(Convert.ToInt32(this.txt_exp.Text), this.txt_pass.Text))//si coinciden los datos con lo almacenado en BD
                    {
                        this.Hide();
                        this.icontrol.SetLogginUsuario(Convert.ToInt32(this.txt_exp.Text));
                        Form1 formP = new Form1(this.icontrol);
                        formP.ShowDialog();                        
                    }
                    else
                    {
                        MessageBox.Show("Contraseña incorrecta. Escriba nuevamente su contraseña");
                        this.txt_pass.Text = String.Empty;
                        this.txt_pass.Focus();
                    }
                }
                catch (Exception msg)
                {
                    MessageBox.Show(msg.Message);                    
                }
            }
            else MessageBox.Show("Debe rellenar todos los campos.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnl_cambiarpass.Visible = false;
            pnl_loggin.Visible = true;
            this.LimpiarCamposPass();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (edt_contranuev1.Text != "" && edt_contranueva2.Text != ""
                    && this.edt_expPass.Text != string.Empty && this.edt_expPass.Text != string.Empty)
                {
                    try { Convert.ToInt32(this.edt_expPass.Text); }
                    catch (Exception) { throw new Exception("El campo para el número de expediente debe contener solo caracteres numéricos."); }

                    if (this.icontrol.CheckearCredenciales(Convert.ToInt32(this.edt_expPass.Text), this.edt_contraActual.Text))
                    {
                        if (this.edt_contranuev1.Text == this.edt_contranueva2.Text)
                        {
                            if (this.icontrol.CambiarContrasena(Convert.ToInt32(this.edt_expPass.Text), this.edt_contranuev1.Text))
                            {
                                MessageBox.Show("Contraseña cambiada satisfactoriamente");
                                this.pnl_cambiarpass.Visible = false;
                                this.LimpiarCamposPass();
                                //aki hay ke ocultar campos o limpiarlos etc... ver mañana con ailin
                            }
                            else MessageBox.Show("Ocurrió un error cambiando la contraseña.");
                        }
                        else MessageBox.Show("Rectifique las contraseña nueva"); //ailin debe poner otro cartel mas tocao aki
                    }
                    else MessageBox.Show("La contraseña actual es incorrecta.");               
                }
                else MessageBox.Show("Debe rellenar todos los campos.");
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }
        }

        private void LimpiarCamposPass()
        {
            edt_expPass.Text = string.Empty;
            edt_contranueva2.Text = string.Empty;
            edt_contranuev1.Text = string.Empty;
            edt_contraActual.Text = string.Empty;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.pnl_cambiarpass.Visible = true;
            this.pnl_loggin.Visible = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            List<int> expds = new List<int> {5};
            //icontrol.GenerarExcelxArea(9,2015,expds);
        }
    }
}
