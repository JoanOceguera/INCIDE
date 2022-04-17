namespace INCIDE.Controles_Visuales
{
    partial class ControlPlanificacion
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.edtNombrePlan = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.edit_edtk = new System.Windows.Forms.TextBox();
            this.labelExp = new System.Windows.Forms.Label();
            this.lblEXP = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPlanificar = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(230)))), ((int)(((byte)(7)))));
            this.panel1.Controls.Add(this.edtNombrePlan);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.edit_edtk);
            this.panel1.Controls.Add(this.labelExp);
            this.panel1.Controls.Add(this.lblEXP);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 30);
            this.panel1.TabIndex = 0;
            // 
            // edtNombrePlan
            // 
            this.edtNombrePlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(230)))), ((int)(((byte)(7)))));
            this.edtNombrePlan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.edtNombrePlan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.edtNombrePlan.ForeColor = System.Drawing.Color.Gray;
            this.edtNombrePlan.Location = new System.Drawing.Point(448, 6);
            this.edtNombrePlan.Name = "edtNombrePlan";
            this.edtNombrePlan.Size = new System.Drawing.Size(349, 19);
            this.edtNombrePlan.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(300, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 18);
            this.label6.TabIndex = 4;
            this.label6.Text = "Nombre y Apellidos: ";
            // 
            // edit_edtk
            // 
            this.edit_edtk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(230)))), ((int)(((byte)(7)))));
            this.edit_edtk.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.edit_edtk.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.edit_edtk.ForeColor = System.Drawing.Color.Gray;
            this.edit_edtk.Location = new System.Drawing.Point(227, 8);
            this.edit_edtk.Name = "edit_edtk";
            this.edit_edtk.Size = new System.Drawing.Size(67, 19);
            this.edit_edtk.TabIndex = 1;
            this.edit_edtk.TextChanged += new System.EventHandler(this.edit_edtk_TextChanged);
            // 
            // labelExp
            // 
            this.labelExp.AutoSize = true;
            this.labelExp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelExp.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExp.ForeColor = System.Drawing.Color.DimGray;
            this.labelExp.Location = new System.Drawing.Point(238, 7);
            this.labelExp.Name = "labelExp";
            this.labelExp.Size = new System.Drawing.Size(0, 18);
            this.labelExp.TabIndex = 0;
            // 
            // lblEXP
            // 
            this.lblEXP.AutoSize = true;
            this.lblEXP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblEXP.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEXP.ForeColor = System.Drawing.Color.DimGray;
            this.lblEXP.Location = new System.Drawing.Point(189, 7);
            this.lblEXP.Name = "lblEXP";
            this.lblEXP.Size = new System.Drawing.Size(43, 18);
            this.lblEXP.TabIndex = 0;
            this.lblEXP.Text = "Exp: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Planificar ausencias";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkGray;
            this.panel2.Controls.Add(this.btnPlanificar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 30);
            this.panel2.TabIndex = 1;
            // 
            // btnPlanificar
            // 
            this.btnPlanificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(118)))), ((int)(((byte)(208)))));
            this.btnPlanificar.FlatAppearance.BorderSize = 0;
            this.btnPlanificar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnPlanificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlanificar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlanificar.ForeColor = System.Drawing.Color.White;
            this.btnPlanificar.Location = new System.Drawing.Point(0, 1);
            this.btnPlanificar.Name = "btnPlanificar";
            this.btnPlanificar.Size = new System.Drawing.Size(189, 30);
            this.btnPlanificar.TabIndex = 0;
            this.btnPlanificar.Text = "Crear nueva planificación";
            this.btnPlanificar.UseVisualStyleBackColor = false;
            this.btnPlanificar.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.ForeColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(0, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 511);
            this.panel3.TabIndex = 2;
            // 
            // ControlPlanificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ControlPlanificacion";
            this.Size = new System.Drawing.Size(800, 571);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPlanificar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblEXP;
        private System.Windows.Forms.Label labelExp;
        private System.Windows.Forms.TextBox edit_edtk;
        public System.Windows.Forms.TextBox edtNombrePlan;
        private System.Windows.Forms.Label label6;
    }
}
