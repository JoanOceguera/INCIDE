namespace INCIDE
{
    partial class AltasBajasForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AltasBajasForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlSms = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGenerarListado = new System.Windows.Forms.Button();
            this.cmb_mes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.llbl_mesAño = new System.Windows.Forms.Label();
            this.edt_ano = new System.Windows.Forms.TextBox();
            this.worker_AltasyBajas = new System.ComponentModel.BackgroundWorker();
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
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Violet;
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.pnlSms);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnGenerarListado);
            this.panel2.Controls.Add(this.cmb_mes);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.llbl_mesAño);
            this.panel2.Controls.Add(this.edt_ano);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // pnlSms
            // 
            this.pnlSms.BackColor = System.Drawing.Color.White;
            this.pnlSms.Controls.Add(this.pictureBox1);
            this.pnlSms.Controls.Add(this.label2);
            resources.ApplyResources(this.pnlSms, "pnlSms");
            this.pnlSms.Name = "pnlSms";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::INCIDE.Properties.Resources.loading11;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Name = "label3";
            // 
            // btnGenerarListado
            // 
            this.btnGenerarListado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btnGenerarListado.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnGenerarListado, "btnGenerarListado");
            this.btnGenerarListado.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnGenerarListado.Name = "btnGenerarListado";
            this.btnGenerarListado.UseVisualStyleBackColor = false;
            this.btnGenerarListado.Click += new System.EventHandler(this.btnGenerarListado_Click);
            // 
            // cmb_mes
            // 
            resources.ApplyResources(this.cmb_mes, "cmb_mes");
            this.cmb_mes.FormattingEnabled = true;
            this.cmb_mes.Name = "cmb_mes";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Name = "label1";
            // 
            // llbl_mesAño
            // 
            resources.ApplyResources(this.llbl_mesAño, "llbl_mesAño");
            this.llbl_mesAño.ForeColor = System.Drawing.Color.DimGray;
            this.llbl_mesAño.Name = "llbl_mesAño";
            // 
            // edt_ano
            // 
            resources.ApplyResources(this.edt_ano, "edt_ano");
            this.edt_ano.Name = "edt_ano";
            // 
            // worker_AltasyBajas
            // 
            this.worker_AltasyBajas.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_AltasyBajasDoWork);
            this.worker_AltasyBajas.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_AltasyBajasRunWorkerCompleted);
            // 
            // AltasBajasForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "AltasBajasForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
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
        private System.Windows.Forms.Button btnGenerarListado;
        private System.Windows.Forms.ComboBox cmb_mes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label llbl_mesAño;
        private System.Windows.Forms.TextBox edt_ano;
        private System.ComponentModel.BackgroundWorker worker_AltasyBajas;
    }
}