namespace INCIDE
{
    partial class MenuAdministracion
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
            this.btnGestionarPersona = new System.Windows.Forms.Button();
            this.btnAbrirCerrarMesPrenom = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnGestionarArea = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pnl_dbf = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.btnGenerarXml = new System.Windows.Forms.Button();
            this.panel14 = new System.Windows.Forms.Panel();
            this.pnl_prenomina = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnl_persona = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button5 = new System.Windows.Forms.Button();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnl_dbf.SuspendLayout();
            this.panel13.SuspendLayout();
            this.pnl_prenomina.SuspendLayout();
            this.pnl_persona.SuspendLayout();
            this.panel15.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGestionarPersona
            // 
            this.btnGestionarPersona.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btnGestionarPersona.FlatAppearance.BorderSize = 0;
            this.btnGestionarPersona.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionarPersona.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionarPersona.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnGestionarPersona.Location = new System.Drawing.Point(31, 1);
            this.btnGestionarPersona.Name = "btnGestionarPersona";
            this.btnGestionarPersona.Size = new System.Drawing.Size(210, 30);
            this.btnGestionarPersona.TabIndex = 11;
            this.btnGestionarPersona.Text = "Gestionar persona";
            this.btnGestionarPersona.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.btnGestionarPersona, "Esta opción permite crear una persona y modificar sus datos. Una persona no se el" +
        "imina, se desactiva.");
            this.btnGestionarPersona.UseVisualStyleBackColor = false;
            this.btnGestionarPersona.Click += new System.EventHandler(this.btnGestionarPersona_Click);
            // 
            // btnAbrirCerrarMesPrenom
            // 
            this.btnAbrirCerrarMesPrenom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btnAbrirCerrarMesPrenom.FlatAppearance.BorderSize = 0;
            this.btnAbrirCerrarMesPrenom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirCerrarMesPrenom.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrirCerrarMesPrenom.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnAbrirCerrarMesPrenom.Location = new System.Drawing.Point(31, 1);
            this.btnAbrirCerrarMesPrenom.Name = "btnAbrirCerrarMesPrenom";
            this.btnAbrirCerrarMesPrenom.Size = new System.Drawing.Size(393, 30);
            this.btnAbrirCerrarMesPrenom.TabIndex = 12;
            this.btnAbrirCerrarMesPrenom.Text = "Abrir-Cerrar mes y generar reporte de incidencias";
            this.btnAbrirCerrarMesPrenom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.btnAbrirCerrarMesPrenom, "Esta opción permite cerrar el mes anterior. También genera la prenómina de un mes" +
        " que ya está cerrado.");
            this.btnAbrirCerrarMesPrenom.UseVisualStyleBackColor = false;
            this.btnAbrirCerrarMesPrenom.Click += new System.EventHandler(this.btnAbrirCerrarMesPrenom_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.panel11);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.pnl_dbf);
            this.panel1.Controls.Add(this.panel13);
            this.panel1.Controls.Add(this.pnl_prenomina);
            this.panel1.Controls.Add(this.pnl_persona);
            this.panel1.Location = new System.Drawing.Point(10, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(432, 354);
            this.panel1.TabIndex = 13;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.button4);
            this.panel11.Controls.Add(this.panel12);
            this.panel11.Location = new System.Drawing.Point(0, 124);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(288, 29);
            this.panel11.TabIndex = 21;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.Gainsboro;
            this.button4.Location = new System.Drawing.Point(32, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(347, 30);
            this.button4.TabIndex = 12;
            this.button4.Text = "Gestionar Horarios de trabajo";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.button4, "Permite crear y modificar una agrupación.");
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel12.Location = new System.Drawing.Point(1, 1);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(7, 29);
            this.panel12.TabIndex = 14;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.button3);
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Location = new System.Drawing.Point(0, 50);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(242, 29);
            this.panel9.TabIndex = 20;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Gainsboro;
            this.button3.Location = new System.Drawing.Point(32, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(210, 30);
            this.button3.TabIndex = 12;
            this.button3.Text = "Gestionar Agrupaciones";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.button3, "Permite crear y modificar una agrupación.");
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Magenta;
            this.panel10.Location = new System.Drawing.Point(1, 1);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(7, 29);
            this.panel10.TabIndex = 14;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.button2);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Location = new System.Drawing.Point(0, 87);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(242, 29);
            this.panel7.TabIndex = 19;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Gainsboro;
            this.button2.Location = new System.Drawing.Point(32, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(210, 30);
            this.button2.TabIndex = 12;
            this.button2.Text = "Gestionar Claves";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.button2, "Permite crear y modificar una clave para las ausencias.");
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.panel8.Location = new System.Drawing.Point(1, 1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(7, 29);
            this.panel8.TabIndex = 14;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnGestionarArea);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Location = new System.Drawing.Point(0, 161);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(261, 29);
            this.panel5.TabIndex = 19;
            // 
            // btnGestionarArea
            // 
            this.btnGestionarArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btnGestionarArea.FlatAppearance.BorderSize = 0;
            this.btnGestionarArea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionarArea.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionarArea.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnGestionarArea.Location = new System.Drawing.Point(32, 0);
            this.btnGestionarArea.Name = "btnGestionarArea";
            this.btnGestionarArea.Size = new System.Drawing.Size(231, 30);
            this.btnGestionarArea.TabIndex = 12;
            this.btnGestionarArea.Text = "Gestionar Áreas del Centro";
            this.btnGestionarArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.btnGestionarArea, "Permite crear un área de trabajo y asignar los responsables de la misma.");
            this.btnGestionarArea.UseVisualStyleBackColor = false;
            this.btnGestionarArea.Click += new System.EventHandler(this.btnGestionarArea_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Orange;
            this.panel6.Location = new System.Drawing.Point(1, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(7, 29);
            this.panel6.TabIndex = 14;
            // 
            // pnl_dbf
            // 
            this.pnl_dbf.Controls.Add(this.button1);
            this.pnl_dbf.Controls.Add(this.panel4);
            this.pnl_dbf.Location = new System.Drawing.Point(0, 236);
            this.pnl_dbf.Name = "pnl_dbf";
            this.pnl_dbf.Size = new System.Drawing.Size(312, 30);
            this.pnl_dbf.TabIndex = 18;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Gainsboro;
            this.button1.Location = new System.Drawing.Point(31, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(280, 30);
            this.button1.TabIndex = 14;
            this.button1.Text = "Generar Excel - horas no laboradas";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.button1, "Genera un fichero dbf con todos los números de expedientes y la cantidad de horas" +
        " que han trabajado.");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.IndianRed;
            this.panel4.Location = new System.Drawing.Point(1, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(7, 30);
            this.panel4.TabIndex = 15;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.btnGenerarXml);
            this.panel13.Controls.Add(this.panel14);
            this.panel13.Location = new System.Drawing.Point(0, 274);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(356, 30);
            this.panel13.TabIndex = 17;
            // 
            // btnGenerarXml
            // 
            this.btnGenerarXml.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btnGenerarXml.FlatAppearance.BorderSize = 0;
            this.btnGenerarXml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarXml.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarXml.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnGenerarXml.Location = new System.Drawing.Point(31, 1);
            this.btnGenerarXml.Name = "btnGenerarXml";
            this.btnGenerarXml.Size = new System.Drawing.Size(324, 30);
            this.btnGenerarXml.TabIndex = 12;
            this.btnGenerarXml.Text = "Generar xml para importar al SISCONT";
            this.btnGenerarXml.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarXml.UseVisualStyleBackColor = false;
            this.btnGenerarXml.Click += new System.EventHandler(this.btnGenerarXml_Click);
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(197)))), ((int)(((byte)(17)))));
            this.panel14.Location = new System.Drawing.Point(1, 2);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(7, 29);
            this.panel14.TabIndex = 14;
            // 
            // pnl_prenomina
            // 
            this.pnl_prenomina.Controls.Add(this.btnAbrirCerrarMesPrenom);
            this.pnl_prenomina.Controls.Add(this.panel3);
            this.pnl_prenomina.Location = new System.Drawing.Point(0, 312);
            this.pnl_prenomina.Name = "pnl_prenomina";
            this.pnl_prenomina.Size = new System.Drawing.Size(427, 30);
            this.pnl_prenomina.TabIndex = 17;
            this.pnl_prenomina.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Teal;
            this.panel3.Location = new System.Drawing.Point(1, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(7, 29);
            this.panel3.TabIndex = 14;
            // 
            // pnl_persona
            // 
            this.pnl_persona.Controls.Add(this.btnGestionarPersona);
            this.pnl_persona.Controls.Add(this.panel2);
            this.pnl_persona.Location = new System.Drawing.Point(0, 12);
            this.pnl_persona.Name = "pnl_persona";
            this.pnl_persona.Size = new System.Drawing.Size(242, 30);
            this.pnl_persona.TabIndex = 16;
            this.pnl_persona.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.YellowGreen;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(7, 30);
            this.panel2.TabIndex = 13;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.Gainsboro;
            this.button5.Location = new System.Drawing.Point(32, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(347, 30);
            this.button5.TabIndex = 12;
            this.button5.Text = "Gestionar Grupos de Evaluación del Centro";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.button5, "Permite crear un grupo de avaluación.");
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.LightGray;
            this.panel15.Controls.Add(this.button5);
            this.panel15.Controls.Add(this.panel16);
            this.panel15.Location = new System.Drawing.Point(10, 210);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(379, 29);
            this.panel15.TabIndex = 20;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.Chocolate;
            this.panel16.Location = new System.Drawing.Point(1, 1);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(7, 29);
            this.panel16.TabIndex = 14;
            // 
            // MenuAdministracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(453, 377);
            this.Controls.Add(this.panel15);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MenuAdministracion";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panel de administración";
            this.Shown += new System.EventHandler(this.MenuAdministracion_Shown);
            this.panel1.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.pnl_dbf.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.pnl_prenomina.ResumeLayout(false);
            this.pnl_persona.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGestionarPersona;
        private System.Windows.Forms.Button btnAbrirCerrarMesPrenom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnl_persona;
        private System.Windows.Forms.Panel pnl_dbf;
        private System.Windows.Forms.Panel pnl_prenomina;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnGestionarArea;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Button btnGenerarXml;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel panel16;
    }
}