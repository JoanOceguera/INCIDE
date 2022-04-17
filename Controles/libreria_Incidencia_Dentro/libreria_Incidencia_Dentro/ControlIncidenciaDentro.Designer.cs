namespace libreria_Incidencia_Dentro
{
    partial class ControlTraza
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
            this.pnl_inferior = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl_superior = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmb_claves = new System.Windows.Forms.ComboBox();
            this.lbl_hora = new System.Windows.Forms.Label();
            this.lbl_fecha = new System.Windows.Forms.Label();
            this.lbl_evento = new System.Windows.Forms.Label();
            this.pnl_eventoFondo = new System.Windows.Forms.Panel();
            this.pnl_inferior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnl_superior.SuspendLayout();
            this.pnl_eventoFondo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_inferior
            // 
            this.pnl_inferior.BackColor = System.Drawing.Color.DimGray;
            this.pnl_inferior.Controls.Add(this.pictureBox1);
            this.pnl_inferior.Controls.Add(this.panel1);
            this.pnl_inferior.Controls.Add(this.pnl_superior);
            this.pnl_inferior.Controls.Add(this.lbl_hora);
            this.pnl_inferior.Controls.Add(this.lbl_fecha);
            this.pnl_inferior.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_inferior.Location = new System.Drawing.Point(0, 0);
            this.pnl_inferior.Name = "pnl_inferior";
            this.pnl_inferior.Size = new System.Drawing.Size(100, 95);
            this.pnl_inferior.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::libreria_Incidencia_Dentro.Properties.Resources.observIco2;
            this.pictureBox1.Location = new System.Drawing.Point(80, 76);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(98, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 53);
            this.panel1.TabIndex = 6;
            // 
            // pnl_superior
            // 
            this.pnl_superior.BackColor = System.Drawing.Color.Gray;
            this.pnl_superior.Controls.Add(this.panel2);
            this.pnl_superior.Controls.Add(this.cmb_claves);
            this.pnl_superior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_superior.Location = new System.Drawing.Point(0, 0);
            this.pnl_superior.Name = "pnl_superior";
            this.pnl_superior.Size = new System.Drawing.Size(100, 42);
            this.pnl_superior.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(98, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2, 42);
            this.panel2.TabIndex = 7;
            // 
            // cmb_claves
            // 
            this.cmb_claves.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cmb_claves.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_claves.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_claves.ForeColor = System.Drawing.Color.DimGray;
            this.cmb_claves.FormattingEnabled = true;
            this.cmb_claves.Location = new System.Drawing.Point(35, 6);
            this.cmb_claves.Name = "cmb_claves";
            this.cmb_claves.Size = new System.Drawing.Size(52, 23);
            this.cmb_claves.TabIndex = 2;
            this.cmb_claves.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_claves_KeyPress);
            // 
            // lbl_hora
            // 
            this.lbl_hora.AutoSize = true;
            this.lbl_hora.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_hora.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_hora.Location = new System.Drawing.Point(3, 52);
            this.lbl_hora.Name = "lbl_hora";
            this.lbl_hora.Size = new System.Drawing.Size(79, 18);
            this.lbl_hora.TabIndex = 4;
            this.lbl_hora.Text = "08:05 am";
            // 
            // lbl_fecha
            // 
            this.lbl_fecha.AutoSize = true;
            this.lbl_fecha.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fecha.ForeColor = System.Drawing.Color.LightGray;
            this.lbl_fecha.Location = new System.Drawing.Point(2, 72);
            this.lbl_fecha.Name = "lbl_fecha";
            this.lbl_fecha.Size = new System.Drawing.Size(73, 14);
            this.lbl_fecha.TabIndex = 3;
            this.lbl_fecha.Text = "10/09/2015";
            // 
            // lbl_evento
            // 
            this.lbl_evento.AutoSize = true;
            this.lbl_evento.BackColor = System.Drawing.Color.YellowGreen;
            this.lbl_evento.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_evento.ForeColor = System.Drawing.Color.Ivory;
            this.lbl_evento.Location = new System.Drawing.Point(3, 2);
            this.lbl_evento.Name = "lbl_evento";
            this.lbl_evento.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_evento.Size = new System.Drawing.Size(27, 25);
            this.lbl_evento.TabIndex = 0;
            this.lbl_evento.Text = "E";
            // 
            // pnl_eventoFondo
            // 
            this.pnl_eventoFondo.BackColor = System.Drawing.Color.YellowGreen;
            this.pnl_eventoFondo.Controls.Add(this.lbl_evento);
            this.pnl_eventoFondo.Location = new System.Drawing.Point(0, 0);
            this.pnl_eventoFondo.Name = "pnl_eventoFondo";
            this.pnl_eventoFondo.Size = new System.Drawing.Size(30, 29);
            this.pnl_eventoFondo.TabIndex = 6;
            // 
            // ControlTraza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnl_eventoFondo);
            this.Controls.Add(this.pnl_inferior);
            this.Name = "ControlTraza";
            this.Size = new System.Drawing.Size(100, 95);
            this.pnl_inferior.ResumeLayout(false);
            this.pnl_inferior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnl_superior.ResumeLayout(false);
            this.pnl_eventoFondo.ResumeLayout(false);
            this.pnl_eventoFondo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnl_inferior;
        public System.Windows.Forms.ComboBox cmb_claves;
        public System.Windows.Forms.Label lbl_evento;
        public System.Windows.Forms.Label lbl_hora;
        public System.Windows.Forms.Label lbl_fecha;
        public System.Windows.Forms.Panel pnl_superior;
        public System.Windows.Forms.Panel pnl_eventoFondo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
