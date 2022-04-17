namespace INCIDE
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnReporte = new System.Windows.Forms.Button();
            this.btn_funcionesAdministracion = new System.Windows.Forms.Button();
            this.btn_nolaborables = new System.Windows.Forms.Button();
            this.btn_planificar = new System.Windows.Forms.Button();
            this.btn_incidencias = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnl_lateralizq = new System.Windows.Forms.Panel();
            this.pnl_loadingPrincipal = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.worker_listarpersonas = new System.ComponentModel.BackgroundWorker();
            this.worker_listarGrupos = new System.ComponentModel.BackgroundWorker();
            this.hint = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnl_loadingPrincipal.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnReporte);
            this.panel1.Controls.Add(this.btn_funcionesAdministracion);
            this.panel1.Controls.Add(this.btn_nolaborables);
            this.panel1.Controls.Add(this.btn_planificar);
            this.panel1.Controls.Add(this.btn_incidencias);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(963, 61);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(725, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(35, 61);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Silver;
            this.panel4.Location = new System.Drawing.Point(19, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 61);
            this.panel4.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightSlateGray;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(21, 61);
            this.panel3.TabIndex = 2;
            // 
            // btnReporte
            // 
            this.btnReporte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btnReporte.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnReporte.Enabled = false;
            this.btnReporte.FlatAppearance.BorderSize = 0;
            this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporte.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporte.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnReporte.Location = new System.Drawing.Point(580, 0);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(145, 61);
            this.btnReporte.TabIndex = 2;
            this.btnReporte.Text = "Reportes";
            this.hint.SetToolTip(this.btnReporte, "Accede a un menú que posibilita generar distintos reportes.");
            this.btnReporte.UseVisualStyleBackColor = false;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // btn_funcionesAdministracion
            // 
            this.btn_funcionesAdministracion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btn_funcionesAdministracion.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_funcionesAdministracion.Enabled = false;
            this.btn_funcionesAdministracion.FlatAppearance.BorderSize = 0;
            this.btn_funcionesAdministracion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_funcionesAdministracion.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_funcionesAdministracion.ForeColor = System.Drawing.Color.Gainsboro;
            this.btn_funcionesAdministracion.Location = new System.Drawing.Point(435, 0);
            this.btn_funcionesAdministracion.Name = "btn_funcionesAdministracion";
            this.btn_funcionesAdministracion.Size = new System.Drawing.Size(145, 61);
            this.btn_funcionesAdministracion.TabIndex = 0;
            this.btn_funcionesAdministracion.Text = "Funciones de administración";
            this.hint.SetToolTip(this.btn_funcionesAdministracion, "Accede a un menú que posibilita ejecutar funciones de administración.");
            this.btn_funcionesAdministracion.UseVisualStyleBackColor = false;
            this.btn_funcionesAdministracion.Visible = false;
            this.btn_funcionesAdministracion.Click += new System.EventHandler(this.btnGenerarNomina_Click);
            // 
            // btn_nolaborables
            // 
            this.btn_nolaborables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btn_nolaborables.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_nolaborables.Enabled = false;
            this.btn_nolaborables.FlatAppearance.BorderSize = 0;
            this.btn_nolaborables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_nolaborables.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_nolaborables.ForeColor = System.Drawing.Color.Gainsboro;
            this.btn_nolaborables.Location = new System.Drawing.Point(290, 0);
            this.btn_nolaborables.Name = "btn_nolaborables";
            this.btn_nolaborables.Size = new System.Drawing.Size(145, 61);
            this.btn_nolaborables.TabIndex = 0;
            this.btn_nolaborables.Text = "Planificar días no laborables";
            this.hint.SetToolTip(this.btn_nolaborables, "Permite planificar días no laborables como 10 de Octubre y 25 de Diciembre.");
            this.btn_nolaborables.UseVisualStyleBackColor = false;
            this.btn_nolaborables.Visible = false;
            this.btn_nolaborables.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_planificar
            // 
            this.btn_planificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btn_planificar.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_planificar.Enabled = false;
            this.btn_planificar.FlatAppearance.BorderSize = 0;
            this.btn_planificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_planificar.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_planificar.ForeColor = System.Drawing.Color.Gainsboro;
            this.btn_planificar.Location = new System.Drawing.Point(145, 0);
            this.btn_planificar.Name = "btn_planificar";
            this.btn_planificar.Size = new System.Drawing.Size(145, 61);
            this.btn_planificar.TabIndex = 0;
            this.btn_planificar.Text = "Planificar\r\nausencias";
            this.hint.SetToolTip(this.btn_planificar, "Permite planificar futuros períodos de ausencia de un trabajador\r\ncomo vacaciones" +
        " y licencias de maternidad.");
            this.btn_planificar.UseVisualStyleBackColor = false;
            this.btn_planificar.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_incidencias
            // 
            this.btn_incidencias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btn_incidencias.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_incidencias.Enabled = false;
            this.btn_incidencias.FlatAppearance.BorderSize = 0;
            this.btn_incidencias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_incidencias.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_incidencias.ForeColor = System.Drawing.Color.Gainsboro;
            this.btn_incidencias.Location = new System.Drawing.Point(0, 0);
            this.btn_incidencias.Name = "btn_incidencias";
            this.btn_incidencias.Size = new System.Drawing.Size(145, 61);
            this.btn_incidencias.TabIndex = 0;
            this.btn_incidencias.Text = " Incidencias";
            this.hint.SetToolTip(this.btn_incidencias, "Permite poner las claves a los trabajadores según su estancia\r\nen el centro.");
            this.btn_incidencias.UseVisualStyleBackColor = false;
            this.btn_incidencias.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.DimGray;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 61);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer1.Panel1.Controls.Add(this.pnl_lateralizq);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Size = new System.Drawing.Size(963, 390);
            this.splitContainer1.SplitterDistance = 195;
            this.splitContainer1.TabIndex = 1;
            // 
            // pnl_lateralizq
            // 
            this.pnl_lateralizq.AutoScroll = true;
            this.pnl_lateralizq.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_lateralizq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_lateralizq.Location = new System.Drawing.Point(0, 0);
            this.pnl_lateralizq.Name = "pnl_lateralizq";
            this.pnl_lateralizq.Size = new System.Drawing.Size(195, 390);
            this.pnl_lateralizq.TabIndex = 2;
            // 
            // pnl_loadingPrincipal
            // 
            this.pnl_loadingPrincipal.BackColor = System.Drawing.Color.White;
            this.pnl_loadingPrincipal.Controls.Add(this.panel5);
            this.pnl_loadingPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_loadingPrincipal.Location = new System.Drawing.Point(0, 61);
            this.pnl_loadingPrincipal.Name = "pnl_loadingPrincipal";
            this.pnl_loadingPrincipal.Size = new System.Drawing.Size(963, 390);
            this.pnl_loadingPrincipal.TabIndex = 2;
            this.pnl_loadingPrincipal.Visible = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gainsboro;
            this.panel5.Controls.Add(this.pictureBox1);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Location = new System.Drawing.Point(28, 34);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(451, 128);
            this.panel5.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Silver;
            this.pictureBox1.Image = global::INCIDE.Properties.Resources.RelojWait;
            this.pictureBox1.Location = new System.Drawing.Point(20, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(93, 95);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(135, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Espere un instante, por favor...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(135, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Recopilando la información necesaria.";
            // 
            // worker_listarpersonas
            // 
            this.worker_listarpersonas.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_listarpersonas_DoWork_1);
            this.worker_listarpersonas.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_listarpersonas_RunWorkerCompleted);
            // 
            // worker_listarGrupos
            // 
            this.worker_listarGrupos.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_listarGrupos_DoWork);
            this.worker_listarGrupos.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_listarGrupos_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 451);
            this.Controls.Add(this.pnl_loadingPrincipal);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INCIDE - Control de disciplina laboral";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnl_loadingPrincipal.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_nolaborables;
        private System.Windows.Forms.Button btn_planificar;
        private System.Windows.Forms.Button btn_incidencias;
        private System.ComponentModel.BackgroundWorker worker_listarpersonas;
        private System.Windows.Forms.Panel pnl_lateralizq;
        private System.ComponentModel.BackgroundWorker worker_listarGrupos;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_funcionesAdministracion;
        private System.Windows.Forms.ToolTip hint;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.Panel pnl_loadingPrincipal;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}

