namespace INCIDE
{
    partial class GestionarAgrupacion
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.edtCodigo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.edtCodigoA = new System.Windows.Forms.TextBox();
            this.edtDescripcion = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReporte = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.edtCodigoM = new System.Windows.Forms.TextBox();
            this.edtDescripcionM = new System.Windows.Forms.TextBox();
            this.cbxAgrupaciones = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(7, 11);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(392, 248);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.edtCodigo);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.edtCodigoA);
            this.tabPage1.Controls.Add(this.edtDescripcion);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.btnReporte);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(384, 219);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Insertar";
            // 
            // edtCodigo
            // 
            this.edtCodigo.AutoSize = true;
            this.edtCodigo.ForeColor = System.Drawing.Color.DimGray;
            this.edtCodigo.Location = new System.Drawing.Point(21, 72);
            this.edtCodigo.Name = "edtCodigo";
            this.edtCodigo.Size = new System.Drawing.Size(157, 16);
            this.edtCodigo.TabIndex = 18;
            this.edtCodigo.Text = "Código de la Agrupación";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(21, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Nombre de la agrupación a Insertar";
            // 
            // edtCodigoA
            // 
            this.edtCodigoA.ForeColor = System.Drawing.Color.DimGray;
            this.edtCodigoA.Location = new System.Drawing.Point(24, 91);
            this.edtCodigoA.Name = "edtCodigoA";
            this.edtCodigoA.Size = new System.Drawing.Size(154, 22);
            this.edtCodigoA.TabIndex = 1;
            // 
            // edtDescripcion
            // 
            this.edtDescripcion.ForeColor = System.Drawing.Color.DimGray;
            this.edtDescripcion.Location = new System.Drawing.Point(21, 44);
            this.edtDescripcion.Name = "edtDescripcion";
            this.edtDescripcion.Size = new System.Drawing.Size(220, 22);
            this.edtDescripcion.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Magenta;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 212);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(378, 4);
            this.panel2.TabIndex = 15;
            // 
            // btnReporte
            // 
            this.btnReporte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btnReporte.FlatAppearance.BorderSize = 0;
            this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporte.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporte.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnReporte.Location = new System.Drawing.Point(259, 156);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(106, 45);
            this.btnReporte.TabIndex = 10;
            this.btnReporte.Text = "Insertar";
            this.btnReporte.UseVisualStyleBackColor = false;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightGray;
            this.tabPage2.Controls.Add(this.edtCodigoM);
            this.tabPage2.Controls.Add(this.edtDescripcionM);
            this.tabPage2.Controls.Add(this.cbxAgrupaciones);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.ForeColor = System.Drawing.Color.DimGray;
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(384, 219);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Editar";
            // 
            // edtCodigoM
            // 
            this.edtCodigoM.ForeColor = System.Drawing.Color.DimGray;
            this.edtCodigoM.Location = new System.Drawing.Point(23, 141);
            this.edtCodigoM.Name = "edtCodigoM";
            this.edtCodigoM.Size = new System.Drawing.Size(152, 22);
            this.edtCodigoM.TabIndex = 2;
            // 
            // edtDescripcionM
            // 
            this.edtDescripcionM.ForeColor = System.Drawing.Color.DimGray;
            this.edtDescripcionM.Location = new System.Drawing.Point(23, 95);
            this.edtDescripcionM.Name = "edtDescripcionM";
            this.edtDescripcionM.Size = new System.Drawing.Size(227, 22);
            this.edtDescripcionM.TabIndex = 1;
            // 
            // cbxAgrupaciones
            // 
            this.cbxAgrupaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxAgrupaciones.ForeColor = System.Drawing.Color.DimGray;
            this.cbxAgrupaciones.FormattingEnabled = true;
            this.cbxAgrupaciones.Location = new System.Drawing.Point(23, 47);
            this.cbxAgrupaciones.Name = "cbxAgrupaciones";
            this.cbxAgrupaciones.Size = new System.Drawing.Size(227, 24);
            this.cbxAgrupaciones.TabIndex = 0;
            this.cbxAgrupaciones.SelectedValueChanged += new System.EventHandler(this.cbxAgrupaciones_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(23, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 16);
            this.label2.TabIndex = 32;
            this.label2.Text = "Código de la Agrupación";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(23, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(287, 16);
            this.label4.TabIndex = 33;
            this.label4.Text = "Seleccione la agrupación que desea modificar";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(23, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 16);
            this.label3.TabIndex = 33;
            this.label3.Text = "Nombre de la agrupación";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Gainsboro;
            this.button2.Location = new System.Drawing.Point(259, 156);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 45);
            this.button2.TabIndex = 26;
            this.button2.Text = "Editar";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Magenta;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 212);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(378, 4);
            this.panel3.TabIndex = 25;
            // 
            // GestionarAgrupacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 266);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GestionarAgrupacion";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestionar Agrupación";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label edtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtCodigoA;
        private System.Windows.Forms.TextBox edtDescripcion;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox edtCodigoM;
        private System.Windows.Forms.TextBox edtDescripcionM;
        private System.Windows.Forms.ComboBox cbxAgrupaciones;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel3;
    }
}