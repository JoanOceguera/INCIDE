namespace INCIDE.ControlesVisuales
{
    partial class ControlIncidencia
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.edtNombre = new System.Windows.Forms.TextBox();
            this.edit_edtk = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblEXP = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFechaI = new System.Windows.Forms.Label();
            this.lblFechafin = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnGuardarCambios = new System.Windows.Forms.Button();
            this.pnl_contenedorJornadas = new System.Windows.Forms.Panel();
            this.worker_cargarIncidencias = new System.ComponentModel.BackgroundWorker();
            this.pnl_loading = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnl_loading.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(230)))), ((int)(((byte)(7)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.edtNombre);
            this.panel1.Controls.Add(this.edit_edtk);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblEXP);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1085, 30);
            this.panel1.TabIndex = 3;
            // 
            // edtNombre
            // 
            this.edtNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(230)))), ((int)(((byte)(7)))));
            this.edtNombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.edtNombre.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.edtNombre.ForeColor = System.Drawing.Color.Gray;
            this.edtNombre.Location = new System.Drawing.Point(455, 9);
            this.edtNombre.Name = "edtNombre";
            this.edtNombre.Size = new System.Drawing.Size(349, 19);
            this.edtNombre.TabIndex = 3;
            this.edtNombre.TextChanged += new System.EventHandler(this.edit_edtk_TextChanged);
            // 
            // edit_edtk
            // 
            this.edit_edtk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(230)))), ((int)(((byte)(7)))));
            this.edit_edtk.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.edit_edtk.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.edit_edtk.ForeColor = System.Drawing.Color.Gray;
            this.edit_edtk.Location = new System.Drawing.Point(258, 10);
            this.edit_edtk.Name = "edit_edtk";
            this.edit_edtk.Size = new System.Drawing.Size(39, 19);
            this.edit_edtk.TabIndex = 3;
            this.edit_edtk.TextChanged += new System.EventHandler(this.edit_edtk_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(307, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 18);
            this.label6.TabIndex = 1;
            this.label6.Text = "Nombre y Apellidos: ";
            // 
            // lblEXP
            // 
            this.lblEXP.AutoSize = true;
            this.lblEXP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblEXP.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEXP.ForeColor = System.Drawing.Color.DimGray;
            this.lblEXP.Location = new System.Drawing.Point(218, 9);
            this.lblEXP.Name = "lblEXP";
            this.lblEXP.Size = new System.Drawing.Size(43, 18);
            this.lblEXP.TabIndex = 1;
            this.lblEXP.Text = "Exp: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(-1, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Incidencias de Personas";
            // 
            // lblFechaI
            // 
            this.lblFechaI.AutoSize = true;
            this.lblFechaI.BackColor = System.Drawing.Color.Gray;
            this.lblFechaI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFechaI.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaI.ForeColor = System.Drawing.Color.LightGray;
            this.lblFechaI.Location = new System.Drawing.Point(312, 8);
            this.lblFechaI.Name = "lblFechaI";
            this.lblFechaI.Size = new System.Drawing.Size(108, 18);
            this.lblFechaI.TabIndex = 0;
            this.lblFechaI.Text = "fecha de Inicio";
            // 
            // lblFechafin
            // 
            this.lblFechafin.AutoSize = true;
            this.lblFechafin.BackColor = System.Drawing.Color.Gray;
            this.lblFechafin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFechafin.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechafin.ForeColor = System.Drawing.Color.LightGray;
            this.lblFechafin.Location = new System.Drawing.Point(510, 8);
            this.lblFechafin.Name = "lblFechafin";
            this.lblFechafin.Size = new System.Drawing.Size(72, 18);
            this.lblFechafin.TabIndex = 0;
            this.lblFechafin.Text = "fecha Fin";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(430, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 18);
            this.label5.TabIndex = 1;
            this.label5.Text = "Fecha Fin:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(218, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 18);
            this.label4.TabIndex = 1;
            this.label4.Text = "Fecha Inicio: ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkGray;
            this.panel2.Controls.Add(this.lblFechaI);
            this.panel2.Controls.Add(this.btnGuardarCambios);
            this.panel2.Controls.Add(this.lblFechafin);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1085, 30);
            this.panel2.TabIndex = 4;
            // 
            // btnGuardarCambios
            // 
            this.btnGuardarCambios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(118)))), ((int)(((byte)(208)))));
            this.btnGuardarCambios.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnGuardarCambios.FlatAppearance.BorderSize = 0;
            this.btnGuardarCambios.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnGuardarCambios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarCambios.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarCambios.ForeColor = System.Drawing.Color.White;
            this.btnGuardarCambios.Location = new System.Drawing.Point(0, 0);
            this.btnGuardarCambios.Name = "btnGuardarCambios";
            this.btnGuardarCambios.Size = new System.Drawing.Size(147, 30);
            this.btnGuardarCambios.TabIndex = 1;
            this.btnGuardarCambios.Text = "Guardar Cambios";
            this.btnGuardarCambios.UseVisualStyleBackColor = false;
            this.btnGuardarCambios.Click += new System.EventHandler(this.btnGuardarCambios_Click);
            // 
            // pnl_contenedorJornadas
            // 
            this.pnl_contenedorJornadas.AutoScroll = true;
            this.pnl_contenedorJornadas.BackColor = System.Drawing.Color.White;
            this.pnl_contenedorJornadas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_contenedorJornadas.Location = new System.Drawing.Point(0, 60);
            this.pnl_contenedorJornadas.Name = "pnl_contenedorJornadas";
            this.pnl_contenedorJornadas.Size = new System.Drawing.Size(1085, 418);
            this.pnl_contenedorJornadas.TabIndex = 5;
            this.pnl_contenedorJornadas.Scroll += new System.Windows.Forms.ScrollEventHandler(this.pnl_contenedorJornadas_Scroll);
            this.pnl_contenedorJornadas.Click += new System.EventHandler(this.pnl_contenedorJornadas_Click);
            // 
            // worker_cargarIncidencias
            // 
            this.worker_cargarIncidencias.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_cargarIncidencias_DoWork);
            this.worker_cargarIncidencias.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_cargarIncidencias_RunWorkerCompleted);
            // 
            // pnl_loading
            // 
            this.pnl_loading.BackColor = System.Drawing.Color.Silver;
            this.pnl_loading.Controls.Add(this.panel3);
            this.pnl_loading.Location = new System.Drawing.Point(304, 123);
            this.pnl_loading.Name = "pnl_loading";
            this.pnl_loading.Size = new System.Drawing.Size(200, 205);
            this.pnl_loading.TabIndex = 1;
            this.pnl_loading.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(194, 195);
            this.panel3.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(34, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Espere, por favor...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(21, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Recopilando información.";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(118)))), ((int)(((byte)(208)))));
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(896, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(189, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "Ver trazas jornada actual";
            this.toolTip1.SetToolTip(this.button1, "Muestra las trazas para la jornada laboral en curso.");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::INCIDE.Properties.Resources.Preloader_8;
            this.pictureBox1.Location = new System.Drawing.Point(33, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ControlIncidencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_loading);
            this.Controls.Add(this.pnl_contenedorJornadas);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ControlIncidencia";
            this.Size = new System.Drawing.Size(1085, 478);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnl_loading.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblEXP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edit_edtk;
        private System.Windows.Forms.Button btnGuardarCambios;
        private System.Windows.Forms.Panel pnl_contenedorJornadas;
        private System.ComponentModel.BackgroundWorker worker_cargarIncidencias;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnl_loading;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblFechaI;
        private System.Windows.Forms.Label lblFechafin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox edtNombre;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
