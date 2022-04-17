namespace INCIDE
{
    partial class TrabajadoresForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlSms = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGenerarNomina = new System.Windows.Forms.Button();
            this.cbxAreas = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.worker_listarPorAreas = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlSms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(5, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(412, 203);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 199);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(412, 4);
            this.panel3.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.pnlSms);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnGenerarNomina);
            this.panel2.Controls.Add(this.cbxAreas);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(11, 11);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(390, 180);
            this.panel2.TabIndex = 5;
            // 
            // pnlSms
            // 
            this.pnlSms.BackColor = System.Drawing.Color.White;
            this.pnlSms.Controls.Add(this.pictureBox1);
            this.pnlSms.Controls.Add(this.label2);
            this.pnlSms.Location = new System.Drawing.Point(2, 2);
            this.pnlSms.Name = "pnlSms";
            this.pnlSms.Size = new System.Drawing.Size(387, 177);
            this.pnlSms.TabIndex = 11;
            this.pnlSms.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::INCIDE.Properties.Resources.loading11;
            this.pictureBox1.Location = new System.Drawing.Point(55, 78);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(235, 30);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(52, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 30);
            this.label2.TabIndex = 10;
            this.label2.Text = "Este proceso puede tardar varios minutos.\r\nEspere por favor.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(28, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(284, 30);
            this.label3.TabIndex = 10;
            this.label3.Text = "Deberá seleccionar el área para generar el listado \r\nde trabajadores de la misma";
            // 
            // btnGenerarNomina
            // 
            this.btnGenerarNomina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btnGenerarNomina.FlatAppearance.BorderSize = 0;
            this.btnGenerarNomina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarNomina.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarNomina.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnGenerarNomina.Location = new System.Drawing.Point(273, 90);
            this.btnGenerarNomina.Name = "btnGenerarNomina";
            this.btnGenerarNomina.Size = new System.Drawing.Size(102, 42);
            this.btnGenerarNomina.TabIndex = 7;
            this.btnGenerarNomina.Text = "Generar listado";
            this.btnGenerarNomina.UseVisualStyleBackColor = false;
            this.btnGenerarNomina.Click += new System.EventHandler(this.btnGenerarNomina_Click);
            // 
            // cbxAreas
            // 
            this.cbxAreas.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAreas.FormattingEnabled = true;
            this.cbxAreas.Location = new System.Drawing.Point(34, 110);
            this.cbxAreas.Name = "cbxAreas";
            this.cbxAreas.Size = new System.Drawing.Size(224, 24);
            this.cbxAreas.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(34, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Áreas";
            // 
            // worker_listarPorAreas
            // 
            this.worker_listarPorAreas.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_listarPorAreas_DoWork);
            this.worker_listarPorAreas.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_listarPorAreas_RunWorkerCompleted);
            // 
            // TrabajadoresForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 215);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrabajadoresForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar Reporte de Trabajadores";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlSms.ResumeLayout(false);
            this.pnlSms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlSms;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGenerarNomina;
        private System.Windows.Forms.ComboBox cbxAreas;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker worker_listarPorAreas;
    }
}