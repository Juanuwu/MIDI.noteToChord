namespace midi
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.notaSalida = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // notaSalida
            // 
            this.notaSalida.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notaSalida.Location = new System.Drawing.Point(163, 47);
            this.notaSalida.Name = "notaSalida";
            this.notaSalida.Size = new System.Drawing.Size(852, 60);
            this.notaSalida.TabIndex = 0;
            this.notaSalida.Text = "label1";
            this.notaSalida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(21, 122);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1181, 312);
            this.panel1.TabIndex = 1;
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 446);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.notaSalida);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label notaSalida;
        private System.Windows.Forms.Panel panel1;
    }
}

