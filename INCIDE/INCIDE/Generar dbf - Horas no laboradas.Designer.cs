namespace INCIDE
{
    partial class GenerarDBF
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
            this.label9 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlwait = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnReporte = new System.Windows.Forms.Button();
            this.cmb_mes_dbf = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.llbl_mesAño = new System.Windows.Forms.Label();
            this.edt_ano_dbf = new System.Windows.Forms.TextBox();
            this.worker_generardbf = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.pnlwait.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.pnlwait);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnReporte);
            this.panel1.Controls.Add(this.cmb_mes_dbf);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.llbl_mesAño);
            this.panel1.Controls.Add(this.edt_ano_dbf);
            this.panel1.Location = new System.Drawing.Point(12, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 155);
            this.panel1.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(14, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(260, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Seleccione un mes y año para generar el fichero .dbf";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.IndianRed;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 151);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(360, 4);
            this.panel2.TabIndex = 16;
            // 
            // pnlwait
            // 
            this.pnlwait.BackColor = System.Drawing.Color.White;
            this.pnlwait.Controls.Add(this.pictureBox1);
            this.pnlwait.Controls.Add(this.label2);
            this.pnlwait.Location = new System.Drawing.Point(8, 5);
            this.pnlwait.Name = "pnlwait";
            this.pnlwait.Size = new System.Drawing.Size(346, 143);
            this.pnlwait.TabIndex = 12;
            this.pnlwait.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::INCIDE.Properties.Resources.loading11;
            this.pictureBox1.Location = new System.Drawing.Point(55, 71);
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
            this.label2.Location = new System.Drawing.Point(52, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 30);
            this.label2.TabIndex = 10;
            this.label2.Text = "Este proceso puede tardar varios minutos.\r\nEspere por favor.";
            // 
            // btnReporte
            // 
            this.btnReporte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btnReporte.FlatAppearance.BorderSize = 0;
            this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporte.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporte.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnReporte.Location = new System.Drawing.Point(224, 87);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(106, 45);
            this.btnReporte.TabIndex = 14;
            this.btnReporte.Text = "Generar .dbf";
            this.btnReporte.UseVisualStyleBackColor = false;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // cmb_mes_dbf
            // 
            this.cmb_mes_dbf.FormattingEnabled = true;
            this.cmb_mes_dbf.Location = new System.Drawing.Point(17, 111);
            this.cmb_mes_dbf.Name = "cmb_mes_dbf";
            this.cmb_mes_dbf.Size = new System.Drawing.Size(121, 21);
            this.cmb_mes_dbf.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(143, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Año";
            // 
            // llbl_mesAño
            // 
            this.llbl_mesAño.AutoSize = true;
            this.llbl_mesAño.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llbl_mesAño.ForeColor = System.Drawing.Color.DimGray;
            this.llbl_mesAño.Location = new System.Drawing.Point(14, 93);
            this.llbl_mesAño.Name = "llbl_mesAño";
            this.llbl_mesAño.Size = new System.Drawing.Size(34, 16);
            this.llbl_mesAño.TabIndex = 11;
            this.llbl_mesAño.Text = "Mes";
            // 
            // edt_ano_dbf
            // 
            this.edt_ano_dbf.Location = new System.Drawing.Point(144, 111);
            this.edt_ano_dbf.Name = "edt_ano_dbf";
            this.edt_ano_dbf.Size = new System.Drawing.Size(51, 20);
            this.edt_ano_dbf.TabIndex = 12;
            // 
            // worker_generardbf
            // 
            this.worker_generardbf.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_generardbf_DoWork);
            this.worker_generardbf.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_generardbf_RunWorkerCompleted);
            // 
            // GenerarDBF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(385, 182);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenerarDBF";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar_Excel___Horas_no_laboradas";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlwait.ResumeLayout(false);
            this.pnlwait.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmb_mes_dbf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label llbl_mesAño;
        private System.Windows.Forms.TextBox edt_ano_dbf;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.Panel pnlwait;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker worker_generardbf;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
    }
}