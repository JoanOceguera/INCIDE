namespace Prueba
{
    partial class UserControl1
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
            this.controlIncidenciaDentro1 = new libreria_Incidencia_Dentro.ControlIncidenciaDentro();
            this.SuspendLayout();
            // 
            // controlIncidenciaDentro1
            // 
            this.controlIncidenciaDentro1.AutoScroll = true;
            this.controlIncidenciaDentro1.BackColor = System.Drawing.Color.White;
            this.controlIncidenciaDentro1.Dock = System.Windows.Forms.DockStyle.Left;
            this.controlIncidenciaDentro1.Location = new System.Drawing.Point(0, 0);
            this.controlIncidenciaDentro1.Name = "controlIncidenciaDentro1";
            this.controlIncidenciaDentro1.Size = new System.Drawing.Size(104, 146);
            this.controlIncidenciaDentro1.TabIndex = 0;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.controlIncidenciaDentro1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(675, 146);
            this.ResumeLayout(false);

        }

        #endregion

        private libreria_Incidencia_Dentro.ControlIncidenciaDentro controlIncidenciaDentro1;
    }
}
