namespace INCIDE
{
    partial class AdminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.img_loadingPrenom = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnGenerarNomina = new System.Windows.Forms.Button();
            this.edt_ano = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.llbl_mesAño = new System.Windows.Forms.Label();
            this.cmb_mes = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chb_cerrarMes = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.worker_Prenomina = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_loadingPrenom)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(9, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(370, 255);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.img_loadingPrenom);
            this.panel4.Location = new System.Drawing.Point(9, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(370, 252);
            this.panel4.TabIndex = 5;
            this.panel4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(51, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(240, 30);
            this.label3.TabIndex = 6;
            this.label3.Text = "Este Proceso puede tardar varios minutos. \r\nEspere por favor.";
            // 
            // img_loadingPrenom
            // 
            this.img_loadingPrenom.Image = global::INCIDE.Properties.Resources.loading11;
            this.img_loadingPrenom.Location = new System.Drawing.Point(54, 141);
            this.img_loadingPrenom.Name = "img_loadingPrenom";
            this.img_loadingPrenom.Size = new System.Drawing.Size(218, 26);
            this.img_loadingPrenom.TabIndex = 5;
            this.img_loadingPrenom.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnGenerarNomina);
            this.panel6.Controls.Add(this.edt_ano);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.llbl_mesAño);
            this.panel6.Controls.Add(this.cmb_mes);
            this.panel6.Location = new System.Drawing.Point(15, 74);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(325, 100);
            this.panel6.TabIndex = 18;
            // 
            // btnGenerarNomina
            // 
            this.btnGenerarNomina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btnGenerarNomina.FlatAppearance.BorderSize = 0;
            this.btnGenerarNomina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarNomina.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarNomina.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnGenerarNomina.Location = new System.Drawing.Point(211, 29);
            this.btnGenerarNomina.Name = "btnGenerarNomina";
            this.btnGenerarNomina.Size = new System.Drawing.Size(102, 57);
            this.btnGenerarNomina.TabIndex = 2;
            this.btnGenerarNomina.Text = "Generar Reporte de Incidencias";
            this.btnGenerarNomina.UseVisualStyleBackColor = false;
            this.btnGenerarNomina.Click += new System.EventHandler(this.btnGenerarNomina_Click);
            // 
            // edt_ano
            // 
            this.edt_ano.Location = new System.Drawing.Point(149, 30);
            this.edt_ano.Name = "edt_ano";
            this.edt_ano.Size = new System.Drawing.Size(51, 20);
            this.edt_ano.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(148, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Año";
            // 
            // llbl_mesAño
            // 
            this.llbl_mesAño.AutoSize = true;
            this.llbl_mesAño.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llbl_mesAño.ForeColor = System.Drawing.Color.DimGray;
            this.llbl_mesAño.Location = new System.Drawing.Point(11, 12);
            this.llbl_mesAño.Name = "llbl_mesAño";
            this.llbl_mesAño.Size = new System.Drawing.Size(34, 16);
            this.llbl_mesAño.TabIndex = 1;
            this.llbl_mesAño.Text = "Mes";
            // 
            // cmb_mes
            // 
            this.cmb_mes.FormattingEnabled = true;
            this.cmb_mes.Location = new System.Drawing.Point(14, 29);
            this.cmb_mes.Name = "cmb_mes";
            this.cmb_mes.Size = new System.Drawing.Size(121, 21);
            this.cmb_mes.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Teal;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 251);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(370, 4);
            this.panel5.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.chb_cerrarMes);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(15, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(325, 72);
            this.panel2.TabIndex = 7;
            // 
            // chb_cerrarMes
            // 
            this.chb_cerrarMes.AutoSize = true;
            this.chb_cerrarMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_cerrarMes.ForeColor = System.Drawing.Color.DimGray;
            this.chb_cerrarMes.Location = new System.Drawing.Point(21, 41);
            this.chb_cerrarMes.Name = "chb_cerrarMes";
            this.chb_cerrarMes.Size = new System.Drawing.Size(93, 20);
            this.chb_cerrarMes.TabIndex = 5;
            this.chb_cerrarMes.Text = "Cerrar mes";
            this.chb_cerrarMes.UseVisualStyleBackColor = true;
            this.chb_cerrarMes.CheckedChanged += new System.EventHandler(this.chb_cerrarMes_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(2, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Usted podrá cerrar el mes vencido.";
            // 
            // worker_Prenomina
            // 
            this.worker_Prenomina.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_Prenomina_DoWork);
            this.worker_Prenomina.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_Prenomina_RunWorkerCompleted);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(389, 276);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdminForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Abrir-Cerrar mes y generar reporte de incidencias";
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_loadingPrenom)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chb_cerrarMes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.ComponentModel.BackgroundWorker worker_Prenomina;
        private System.Windows.Forms.PictureBox img_loadingPrenom;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnGenerarNomina;
        private System.Windows.Forms.TextBox edt_ano;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label llbl_mesAño;
        private System.Windows.Forms.ComboBox cmb_mes;
    }
}