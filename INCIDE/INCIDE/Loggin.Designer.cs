namespace INCIDE
{
    partial class Loggin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loggin));
            this.txt_pass = new System.Windows.Forms.MaskedTextBox();
            this.pnl_loggin = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btn_entrar = new System.Windows.Forms.Button();
            this.txt_exp = new System.Windows.Forms.TextBox();
            this.lbl_pass = new System.Windows.Forms.Label();
            this.lbl_expediente = new System.Windows.Forms.Label();
            this.pnl_cambiarpass = new System.Windows.Forms.Panel();
            this.edt_contraActual = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.edt_expPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.edt_contranueva2 = new System.Windows.Forms.TextBox();
            this.edt_contranuev1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnl_loggin.SuspendLayout();
            this.pnl_cambiarpass.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_pass
            // 
            this.txt_pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pass.ForeColor = System.Drawing.Color.DimGray;
            this.txt_pass.Location = new System.Drawing.Point(25, 118);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.Size = new System.Drawing.Size(169, 22);
            this.txt_pass.TabIndex = 1;
            this.txt_pass.UseSystemPasswordChar = true;
            // 
            // pnl_loggin
            // 
            this.pnl_loggin.BackColor = System.Drawing.Color.Gainsboro;
            this.pnl_loggin.Controls.Add(this.linkLabel1);
            this.pnl_loggin.Controls.Add(this.btn_entrar);
            this.pnl_loggin.Controls.Add(this.txt_exp);
            this.pnl_loggin.Controls.Add(this.lbl_pass);
            this.pnl_loggin.Controls.Add(this.lbl_expediente);
            this.pnl_loggin.Controls.Add(this.txt_pass);
            this.pnl_loggin.Location = new System.Drawing.Point(31, 34);
            this.pnl_loggin.Name = "pnl_loggin";
            this.pnl_loggin.Size = new System.Drawing.Size(348, 210);
            this.pnl_loggin.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.SlateGray;
            this.linkLabel1.Location = new System.Drawing.Point(24, 175);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(102, 13);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Cambiar Contraseña";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btn_entrar
            // 
            this.btn_entrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btn_entrar.FlatAppearance.BorderSize = 0;
            this.btn_entrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_entrar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_entrar.ForeColor = System.Drawing.Color.White;
            this.btn_entrar.Location = new System.Drawing.Point(232, 42);
            this.btn_entrar.Name = "btn_entrar";
            this.btn_entrar.Size = new System.Drawing.Size(92, 39);
            this.btn_entrar.TabIndex = 2;
            this.btn_entrar.Text = "Entrar";
            this.btn_entrar.UseVisualStyleBackColor = false;
            this.btn_entrar.Click += new System.EventHandler(this.button3_Click);
            // 
            // txt_exp
            // 
            this.txt_exp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_exp.ForeColor = System.Drawing.Color.DimGray;
            this.txt_exp.Location = new System.Drawing.Point(25, 59);
            this.txt_exp.Name = "txt_exp";
            this.txt_exp.Size = new System.Drawing.Size(169, 22);
            this.txt_exp.TabIndex = 0;
            // 
            // lbl_pass
            // 
            this.lbl_pass.AutoSize = true;
            this.lbl_pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pass.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_pass.Location = new System.Drawing.Point(22, 99);
            this.lbl_pass.Name = "lbl_pass";
            this.lbl_pass.Size = new System.Drawing.Size(77, 16);
            this.lbl_pass.TabIndex = 2;
            this.lbl_pass.Text = "Contraseña";
            // 
            // lbl_expediente
            // 
            this.lbl_expediente.AutoSize = true;
            this.lbl_expediente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_expediente.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_expediente.Location = new System.Drawing.Point(22, 40);
            this.lbl_expediente.Name = "lbl_expediente";
            this.lbl_expediente.Size = new System.Drawing.Size(145, 16);
            this.lbl_expediente.TabIndex = 1;
            this.lbl_expediente.Text = "Número de expediente";
            // 
            // pnl_cambiarpass
            // 
            this.pnl_cambiarpass.BackColor = System.Drawing.Color.Gainsboro;
            this.pnl_cambiarpass.Controls.Add(this.edt_contraActual);
            this.pnl_cambiarpass.Controls.Add(this.label4);
            this.pnl_cambiarpass.Controls.Add(this.edt_expPass);
            this.pnl_cambiarpass.Controls.Add(this.label3);
            this.pnl_cambiarpass.Controls.Add(this.button2);
            this.pnl_cambiarpass.Controls.Add(this.button1);
            this.pnl_cambiarpass.Controls.Add(this.edt_contranueva2);
            this.pnl_cambiarpass.Controls.Add(this.edt_contranuev1);
            this.pnl_cambiarpass.Controls.Add(this.label1);
            this.pnl_cambiarpass.Controls.Add(this.label2);
            this.pnl_cambiarpass.Location = new System.Drawing.Point(32, 34);
            this.pnl_cambiarpass.Name = "pnl_cambiarpass";
            this.pnl_cambiarpass.Size = new System.Drawing.Size(348, 210);
            this.pnl_cambiarpass.TabIndex = 1;
            // 
            // edt_contraActual
            // 
            this.edt_contraActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edt_contraActual.ForeColor = System.Drawing.Color.DimGray;
            this.edt_contraActual.Location = new System.Drawing.Point(25, 77);
            this.edt_contraActual.Name = "edt_contraActual";
            this.edt_contraActual.Size = new System.Drawing.Size(169, 22);
            this.edt_contraActual.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(22, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Contraseña actual";
            // 
            // edt_expPass
            // 
            this.edt_expPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edt_expPass.ForeColor = System.Drawing.Color.DimGray;
            this.edt_expPass.Location = new System.Drawing.Point(25, 30);
            this.edt_expPass.Name = "edt_expPass";
            this.edt_expPass.Size = new System.Drawing.Size(169, 22);
            this.edt_expPass.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(22, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Número de expediente";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(231, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 39);
            this.button2.TabIndex = 2;
            this.button2.Text = "Guardar";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(231, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 39);
            this.button1.TabIndex = 2;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // edt_contranueva2
            // 
            this.edt_contranueva2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edt_contranueva2.ForeColor = System.Drawing.Color.DimGray;
            this.edt_contranueva2.Location = new System.Drawing.Point(25, 179);
            this.edt_contranueva2.Name = "edt_contranueva2";
            this.edt_contranueva2.Size = new System.Drawing.Size(169, 22);
            this.edt_contranueva2.TabIndex = 0;
            // 
            // edt_contranuev1
            // 
            this.edt_contranuev1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edt_contranuev1.ForeColor = System.Drawing.Color.DimGray;
            this.edt_contranuev1.Location = new System.Drawing.Point(25, 132);
            this.edt_contranuev1.Name = "edt_contranuev1";
            this.edt_contranuev1.Size = new System.Drawing.Size(169, 22);
            this.edt_contranuev1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(22, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Confirmar Contraseña";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(22, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nueva Contraseña";
            // 
            // Loggin
            // 
            this.AcceptButton = this.btn_entrar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(412, 276);
            this.Controls.Add(this.pnl_loggin);
            this.Controls.Add(this.pnl_cambiarpass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Loggin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loggin";
            this.pnl_loggin.ResumeLayout(false);
            this.pnl_loggin.PerformLayout();
            this.pnl_cambiarpass.ResumeLayout(false);
            this.pnl_cambiarpass.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox txt_pass;
        private System.Windows.Forms.Panel pnl_loggin;
        private System.Windows.Forms.Label lbl_expediente;
        private System.Windows.Forms.Label lbl_pass;
        private System.Windows.Forms.TextBox txt_exp;
        private System.Windows.Forms.Button btn_entrar;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Panel pnl_cambiarpass;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox edt_contranuev1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edt_contranueva2;
        private System.Windows.Forms.TextBox edt_expPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox edt_contraActual;
        private System.Windows.Forms.Label label4;
    }
}