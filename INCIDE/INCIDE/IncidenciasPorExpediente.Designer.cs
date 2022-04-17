namespace INCIDE
{
    partial class IncidenciasPorExpediente
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
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.date_fin = new System.Windows.Forms.DateTimePicker();
            this.date_inicio = new System.Windows.Forms.DateTimePicker();
            this.btnGenerarNomina = new System.Windows.Forms.Button();
            this.cmb_exp = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.llbl_mesAño = new System.Windows.Forms.Label();
            this.pnl_wait = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.img_loadingPrenom = new System.Windows.Forms.PictureBox();
            this.worker_incidenciasPorExp = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.pnl_wait.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_loadingPrenom)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.date_fin);
            this.panel1.Controls.Add(this.date_inicio);
            this.panel1.Controls.Add(this.btnGenerarNomina);
            this.panel1.Controls.Add(this.cmb_exp);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.llbl_mesAño);
            this.panel1.Location = new System.Drawing.Point(12, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 180);
            this.panel1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(6, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(357, 45);
            this.label4.TabIndex = 22;
            this.label4.Text = "Seleccione un número de expediente asi como un rango \r\nde fechas y presione el bo" +
                "tón \"Generar reporte\" para obtener un \r\nexcel con las incidencias del usuario se" +
                "leccionado.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(136, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "Fecha de fin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(6, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Fecha de inicio";
            // 
            // date_fin
            // 
            this.date_fin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_fin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.date_fin.Location = new System.Drawing.Point(139, 131);
            this.date_fin.Name = "date_fin";
            this.date_fin.Size = new System.Drawing.Size(116, 22);
            this.date_fin.TabIndex = 19;
            this.date_fin.Value = new System.DateTime(2015, 10, 19, 23, 59, 59, 0);
            // 
            // date_inicio
            // 
            this.date_inicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_inicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.date_inicio.Location = new System.Drawing.Point(9, 131);
            this.date_inicio.Name = "date_inicio";
            this.date_inicio.Size = new System.Drawing.Size(121, 22);
            this.date_inicio.TabIndex = 18;
            this.date_inicio.Value = new System.DateTime(2015, 10, 19, 0, 0, 0, 0);
            // 
            // btnGenerarNomina
            // 
            this.btnGenerarNomina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btnGenerarNomina.FlatAppearance.BorderSize = 0;
            this.btnGenerarNomina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarNomina.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarNomina.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnGenerarNomina.Location = new System.Drawing.Point(279, 123);
            this.btnGenerarNomina.Name = "btnGenerarNomina";
            this.btnGenerarNomina.Size = new System.Drawing.Size(102, 42);
            this.btnGenerarNomina.TabIndex = 2;
            this.btnGenerarNomina.Text = "Generar reporte";
            this.btnGenerarNomina.UseVisualStyleBackColor = false;
            this.btnGenerarNomina.Click += new System.EventHandler(this.btnGenerarNomina_Click);
            // 
            // cmb_exp
            // 
            this.cmb_exp.FormattingEnabled = true;
            this.cmb_exp.Location = new System.Drawing.Point(9, 84);
            this.cmb_exp.Name = "cmb_exp";
            this.cmb_exp.Size = new System.Drawing.Size(121, 21);
            this.cmb_exp.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.MediumPurple;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 176);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(393, 4);
            this.panel5.TabIndex = 17;
            // 
            // llbl_mesAño
            // 
            this.llbl_mesAño.AutoSize = true;
            this.llbl_mesAño.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llbl_mesAño.ForeColor = System.Drawing.Color.DimGray;
            this.llbl_mesAño.Location = new System.Drawing.Point(6, 66);
            this.llbl_mesAño.Name = "llbl_mesAño";
            this.llbl_mesAño.Size = new System.Drawing.Size(76, 16);
            this.llbl_mesAño.TabIndex = 1;
            this.llbl_mesAño.Text = "Expediente";
            // 
            // pnl_wait
            // 
            this.pnl_wait.BackColor = System.Drawing.Color.White;
            this.pnl_wait.Controls.Add(this.label3);
            this.pnl_wait.Controls.Add(this.img_loadingPrenom);
            this.pnl_wait.Location = new System.Drawing.Point(12, 11);
            this.pnl_wait.Name = "pnl_wait";
            this.pnl_wait.Size = new System.Drawing.Size(393, 176);
            this.pnl_wait.TabIndex = 5;
            this.pnl_wait.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(76, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(240, 30);
            this.label3.TabIndex = 6;
            this.label3.Text = "Este Proceso puede tardar varios minutos. \r\nEspere por favor.";
            // 
            // img_loadingPrenom
            // 
            this.img_loadingPrenom.Image = global::INCIDE.Properties.Resources.loading11;
            this.img_loadingPrenom.Location = new System.Drawing.Point(79, 79);
            this.img_loadingPrenom.Name = "img_loadingPrenom";
            this.img_loadingPrenom.Size = new System.Drawing.Size(218, 26);
            this.img_loadingPrenom.TabIndex = 5;
            this.img_loadingPrenom.TabStop = false;
            // 
            // worker_incidenciasPorExp
            // 
            this.worker_incidenciasPorExp.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_incidenciasPorExp_DoWork);
            this.worker_incidenciasPorExp.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_incidenciasPorExp_RunWorkerCompleted);
            // 
            // IncidenciasPorExpediente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 201);
            this.Controls.Add(this.pnl_wait);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IncidenciasPorExpediente";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Incidencias por expediente";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnl_wait.ResumeLayout(false);
            this.pnl_wait.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_loadingPrenom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel pnl_wait;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox img_loadingPrenom;
        private System.Windows.Forms.Button btnGenerarNomina;
        private System.Windows.Forms.ComboBox cmb_exp;
        private System.Windows.Forms.Label llbl_mesAño;
        private System.Windows.Forms.DateTimePicker date_inicio;
        private System.Windows.Forms.DateTimePicker date_fin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker worker_incidenciasPorExp;
    }
}