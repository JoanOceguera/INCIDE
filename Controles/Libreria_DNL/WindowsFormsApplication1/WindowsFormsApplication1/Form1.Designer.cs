﻿namespace WindowsFormsApplication1
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
            this.userControl11 = new Prueba.UserControl1();
            this.userControl12 = new Prueba.UserControl1();
            this.userControl13 = new Prueba.UserControl1();
            this.SuspendLayout();
            // 
            // userControl11
            // 
            this.userControl11.Dock = System.Windows.Forms.DockStyle.Top;
            this.userControl11.Location = new System.Drawing.Point(0, 0);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(284, 76);
            this.userControl11.TabIndex = 0;
            // 
            // userControl12
            // 
            this.userControl12.Dock = System.Windows.Forms.DockStyle.Top;
            this.userControl12.Location = new System.Drawing.Point(0, 76);
            this.userControl12.Name = "userControl12";
            this.userControl12.Size = new System.Drawing.Size(284, 76);
            this.userControl12.TabIndex = 1;
            // 
            // userControl13
            // 
            this.userControl13.Dock = System.Windows.Forms.DockStyle.Top;
            this.userControl13.Location = new System.Drawing.Point(0, 152);
            this.userControl13.Name = "userControl13";
            this.userControl13.Size = new System.Drawing.Size(284, 76);
            this.userControl13.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.userControl13);
            this.Controls.Add(this.userControl12);
            this.Controls.Add(this.userControl11);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Prueba.UserControl1 userControl11;
        private Prueba.UserControl1 userControl12;
        private Prueba.UserControl1 userControl13;
    }
}

