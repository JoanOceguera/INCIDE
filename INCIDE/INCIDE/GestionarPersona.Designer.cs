namespace INCIDE
{
    partial class GestionarPersona
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.combo_gruposEvaluacion = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReporte = new System.Windows.Forms.Button();
            this.cmb_areas = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_grupos = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_ci = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_exp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.editcombo_gruposEvaluacion = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.edt_nombre_edit = new System.Windows.Forms.TextBox();
            this.check_activa_editar = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmb_area_editar = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_ci_editar = new System.Windows.Forms.ComboBox();
            this.cmb_grupos_editar = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_exp_editar = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.hint = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(9, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(456, 392);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.combo_gruposEvaluacion);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.btnReporte);
            this.tabPage1.Controls.Add(this.cmb_areas);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.cmb_grupos);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txt_ci);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txt_exp);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(448, 363);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Insertar";
            // 
            // combo_gruposEvaluacion
            // 
            this.combo_gruposEvaluacion.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combo_gruposEvaluacion.ForeColor = System.Drawing.Color.DimGray;
            this.combo_gruposEvaluacion.FormattingEnabled = true;
            this.combo_gruposEvaluacion.Location = new System.Drawing.Point(20, 284);
            this.combo_gruposEvaluacion.Name = "combo_gruposEvaluacion";
            this.combo_gruposEvaluacion.Size = new System.Drawing.Size(203, 24);
            this.combo_gruposEvaluacion.TabIndex = 17;
            this.combo_gruposEvaluacion.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DimGray;
            this.label11.Location = new System.Drawing.Point(17, 241);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(247, 32);
            this.label11.TabIndex = 16;
            this.label11.Text = "Seleccione el grupo de evaluación al cual \r\npertenecerá esta persona";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.YellowGreen;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 356);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(442, 4);
            this.panel2.TabIndex = 15;
            // 
            // btnReporte
            // 
            this.btnReporte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.btnReporte.FlatAppearance.BorderSize = 0;
            this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporte.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporte.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnReporte.Location = new System.Drawing.Point(321, 284);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(106, 45);
            this.btnReporte.TabIndex = 10;
            this.btnReporte.Text = "Insertar";
            this.btnReporte.UseVisualStyleBackColor = false;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // cmb_areas
            // 
            this.cmb_areas.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_areas.ForeColor = System.Drawing.Color.DimGray;
            this.cmb_areas.FormattingEnabled = true;
            this.cmb_areas.Location = new System.Drawing.Point(20, 199);
            this.cmb_areas.Name = "cmb_areas";
            this.cmb_areas.Size = new System.Drawing.Size(243, 24);
            this.cmb_areas.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(17, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(302, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Seleccione área a la cual pertenecerá esta persona";
            // 
            // cmb_grupos
            // 
            this.cmb_grupos.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_grupos.ForeColor = System.Drawing.Color.DimGray;
            this.cmb_grupos.FormattingEnabled = true;
            this.cmb_grupos.Location = new System.Drawing.Point(20, 142);
            this.cmb_grupos.Name = "cmb_grupos";
            this.cmb_grupos.Size = new System.Drawing.Size(243, 24);
            this.cmb_grupos.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(17, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Seleccione el grupo de trabajo";
            // 
            // txt_ci
            // 
            this.txt_ci.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ci.ForeColor = System.Drawing.Color.DimGray;
            this.txt_ci.Location = new System.Drawing.Point(20, 89);
            this.txt_ci.Name = "txt_ci";
            this.txt_ci.Size = new System.Drawing.Size(134, 22);
            this.txt_ci.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(17, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Número de identidad";
            // 
            // txt_exp
            // 
            this.txt_exp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_exp.ForeColor = System.Drawing.Color.DimGray;
            this.txt_exp.Location = new System.Drawing.Point(20, 39);
            this.txt_exp.Name = "txt_exp";
            this.txt_exp.Size = new System.Drawing.Size(103, 22);
            this.txt_exp.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Número de expediente";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightGray;
            this.tabPage2.Controls.Add(this.editcombo_gruposEvaluacion);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.edt_nombre_edit);
            this.tabPage2.Controls.Add(this.check_activa_editar);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.cmb_area_editar);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cmb_ci_editar);
            this.tabPage2.Controls.Add(this.cmb_grupos_editar);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txt_exp_editar);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(448, 363);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Editar";
            // 
            // editcombo_gruposEvaluacion
            // 
            this.editcombo_gruposEvaluacion.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editcombo_gruposEvaluacion.ForeColor = System.Drawing.Color.DimGray;
            this.editcombo_gruposEvaluacion.FormattingEnabled = true;
            this.editcombo_gruposEvaluacion.Location = new System.Drawing.Point(20, 277);
            this.editcombo_gruposEvaluacion.Name = "editcombo_gruposEvaluacion";
            this.editcombo_gruposEvaluacion.Size = new System.Drawing.Size(243, 24);
            this.editcombo_gruposEvaluacion.TabIndex = 27;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DimGray;
            this.label12.Location = new System.Drawing.Point(17, 239);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(247, 32);
            this.label12.TabIndex = 26;
            this.label12.Text = "Seleccione el grupo de evaluación al cual \r\npertenece esta persona";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.YellowGreen;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 356);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(442, 4);
            this.panel3.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DimGray;
            this.label10.Location = new System.Drawing.Point(180, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 16);
            this.label10.TabIndex = 24;
            this.label10.Text = "Nombre";
            // 
            // edt_nombre_edit
            // 
            this.edt_nombre_edit.BackColor = System.Drawing.Color.Silver;
            this.edt_nombre_edit.Enabled = false;
            this.edt_nombre_edit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edt_nombre_edit.ForeColor = System.Drawing.Color.DimGray;
            this.edt_nombre_edit.Location = new System.Drawing.Point(182, 91);
            this.edt_nombre_edit.Name = "edt_nombre_edit";
            this.edt_nombre_edit.Size = new System.Drawing.Size(212, 22);
            this.edt_nombre_edit.TabIndex = 23;
            // 
            // check_activa_editar
            // 
            this.check_activa_editar.AutoSize = true;
            this.check_activa_editar.ForeColor = System.Drawing.Color.DimGray;
            this.check_activa_editar.Location = new System.Drawing.Point(20, 307);
            this.check_activa_editar.Name = "check_activa_editar";
            this.check_activa_editar.Size = new System.Drawing.Size(64, 20);
            this.check_activa_editar.TabIndex = 22;
            this.check_activa_editar.Text = "Activa";
            this.check_activa_editar.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Gainsboro;
            this.button2.Location = new System.Drawing.Point(131, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 21);
            this.button2.TabIndex = 21;
            this.button2.Text = "..";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(164, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(276, 26);
            this.label9.TabIndex = 20;
            this.label9.Text = "Escriba un número de expediente y presione el pequeño\r\nbotón a la derecha del cua" +
    "dro de texto.";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(223)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Gainsboro;
            this.button1.Location = new System.Drawing.Point(321, 284);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 45);
            this.button1.TabIndex = 19;
            this.button1.Text = "Editar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmb_area_editar
            // 
            this.cmb_area_editar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_area_editar.ForeColor = System.Drawing.Color.DimGray;
            this.cmb_area_editar.FormattingEnabled = true;
            this.cmb_area_editar.Location = new System.Drawing.Point(20, 199);
            this.cmb_area_editar.Name = "cmb_area_editar";
            this.cmb_area_editar.Size = new System.Drawing.Size(243, 24);
            this.cmb_area_editar.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(17, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Area a la cual pertenece esta persona";
            // 
            // cmb_ci_editar
            // 
            this.cmb_ci_editar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_ci_editar.ForeColor = System.Drawing.Color.DimGray;
            this.cmb_ci_editar.FormattingEnabled = true;
            this.cmb_ci_editar.Location = new System.Drawing.Point(20, 91);
            this.cmb_ci_editar.Name = "cmb_ci_editar";
            this.cmb_ci_editar.Size = new System.Drawing.Size(143, 24);
            this.cmb_ci_editar.TabIndex = 16;
            this.cmb_ci_editar.SelectedIndexChanged += new System.EventHandler(this.cmb_ci_editar_SelectedIndexChanged);
            // 
            // cmb_grupos_editar
            // 
            this.cmb_grupos_editar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_grupos_editar.ForeColor = System.Drawing.Color.DimGray;
            this.cmb_grupos_editar.FormattingEnabled = true;
            this.cmb_grupos_editar.Location = new System.Drawing.Point(20, 142);
            this.cmb_grupos_editar.Name = "cmb_grupos_editar";
            this.cmb_grupos_editar.Size = new System.Drawing.Size(243, 24);
            this.cmb_grupos_editar.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(17, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(285, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Grupo de trabajo al cual pertenece esta persona";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(17, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Número de identidad";
            // 
            // txt_exp_editar
            // 
            this.txt_exp_editar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_exp_editar.ForeColor = System.Drawing.Color.DimGray;
            this.txt_exp_editar.Location = new System.Drawing.Point(20, 39);
            this.txt_exp_editar.Name = "txt_exp_editar";
            this.txt_exp_editar.Size = new System.Drawing.Size(103, 22);
            this.txt_exp_editar.TabIndex = 12;
            this.txt_exp_editar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_exp_editar_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(17, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(137, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "Número de expediente";
            // 
            // GestionarPersona
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(474, 413);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GestionarPersona";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestionar  persona";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txt_exp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ci;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_grupos;
        private System.Windows.Forms.ComboBox cmb_areas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmb_area_editar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_grupos_editar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_exp_editar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolTip hint;
        private System.Windows.Forms.CheckBox check_activa_editar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox edt_nombre_edit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cmb_ci_editar;
        private System.Windows.Forms.ComboBox combo_gruposEvaluacion;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox editcombo_gruposEvaluacion;
        private System.Windows.Forms.Label label12;
    }
}