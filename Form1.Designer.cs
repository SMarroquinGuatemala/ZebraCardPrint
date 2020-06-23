namespace ZebraCardPrint
{
   partial class FrmCardPrint
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
         this.txtNumeroDeEmpleado = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.btnImprimir = new System.Windows.Forms.Button();
         this.pictureBox1 = new System.Windows.Forms.PictureBox();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         this.SuspendLayout();
         // 
         // txtNumeroDeEmpleado
         // 
         this.txtNumeroDeEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.txtNumeroDeEmpleado.Location = new System.Drawing.Point(8, 25);
         this.txtNumeroDeEmpleado.MaxLength = 7;
         this.txtNumeroDeEmpleado.Name = "txtNumeroDeEmpleado";
         this.txtNumeroDeEmpleado.Size = new System.Drawing.Size(184, 29);
         this.txtNumeroDeEmpleado.TabIndex = 0;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(8, 9);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(112, 13);
         this.label1.TabIndex = 1;
         this.label1.Text = "Numero de Empleado:";
         // 
         // btnImprimir
         // 
         this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.btnImprimir.Location = new System.Drawing.Point(198, 25);
         this.btnImprimir.Name = "btnImprimir";
         this.btnImprimir.Size = new System.Drawing.Size(110, 29);
         this.btnImprimir.TabIndex = 2;
         this.btnImprimir.Text = "Imprimir";
         this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.btnImprimir.UseVisualStyleBackColor = true;
         this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
         // 
         // pictureBox1
         // 
         this.pictureBox1.Location = new System.Drawing.Point(8, 60);
         this.pictureBox1.Name = "pictureBox1";
         this.pictureBox1.Size = new System.Drawing.Size(375, 245);
         this.pictureBox1.TabIndex = 3;
         this.pictureBox1.TabStop = false;
         // 
         // FrmCardPrint
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
         this.Controls.Add(this.pictureBox1);
         this.Controls.Add(this.btnImprimir);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.txtNumeroDeEmpleado);
         this.Name = "FrmCardPrint";
         this.Text = "Impresion Carnet";
         this.Load += new System.EventHandler(this.Form1_Load);
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

        #endregion

        private System.Windows.Forms.TextBox txtNumeroDeEmpleado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

