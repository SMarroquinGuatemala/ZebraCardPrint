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
         this.checkBox1 = new System.Windows.Forms.CheckBox();
         this.button1 = new System.Windows.Forms.Button();
         this.pictureBox2 = new System.Windows.Forms.PictureBox();
         this.pictureBox1 = new System.Windows.Forms.PictureBox();
         this.btnImprimir = new System.Windows.Forms.Button();
         this.rdoAmbasCaras = new System.Windows.Forms.RadioButton();
         this.rdoFrontal = new System.Windows.Forms.RadioButton();
         this.rdoReverso = new System.Windows.Forms.RadioButton();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         this.SuspendLayout();
         // 
         // txtNumeroDeEmpleado
         // 
         this.txtNumeroDeEmpleado.BackColor = System.Drawing.Color.LemonChiffon;
         this.txtNumeroDeEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.txtNumeroDeEmpleado.Location = new System.Drawing.Point(8, 25);
         this.txtNumeroDeEmpleado.MaxLength = 7;
         this.txtNumeroDeEmpleado.Name = "txtNumeroDeEmpleado";
         this.txtNumeroDeEmpleado.Size = new System.Drawing.Size(112, 29);
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
         // checkBox1
         // 
         this.checkBox1.AutoSize = true;
         this.checkBox1.Checked = true;
         this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
         this.checkBox1.Location = new System.Drawing.Point(472, 25);
         this.checkBox1.Name = "checkBox1";
         this.checkBox1.Size = new System.Drawing.Size(80, 17);
         this.checkBox1.TabIndex = 3;
         this.checkBox1.Text = "checkBox1";
         this.checkBox1.UseVisualStyleBackColor = true;
         this.checkBox1.Visible = false;
         // 
         // button1
         // 
         this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.button1.Image = global::ZebraCardPrint.Properties.Resources.Id_Card_48;
         this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.button1.Location = new System.Drawing.Point(126, 1);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(131, 53);
         this.button1.TabIndex = 1;
         this.button1.Text = "Visualizar";
         this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.button1.UseVisualStyleBackColor = true;
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // pictureBox2
         // 
         this.pictureBox2.Location = new System.Drawing.Point(5, 357);
         this.pictureBox2.Name = "pictureBox2";
         this.pictureBox2.Size = new System.Drawing.Size(547, 295);
         this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.pictureBox2.TabIndex = 5;
         this.pictureBox2.TabStop = false;
         // 
         // pictureBox1
         // 
         this.pictureBox1.Location = new System.Drawing.Point(5, 60);
         this.pictureBox1.Name = "pictureBox1";
         this.pictureBox1.Size = new System.Drawing.Size(547, 295);
         this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.pictureBox1.TabIndex = 3;
         this.pictureBox1.TabStop = false;
         // 
         // btnImprimir
         // 
         this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.btnImprimir.Image = global::ZebraCardPrint.Properties.Resources.Printer_48;
         this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.btnImprimir.Location = new System.Drawing.Point(263, 1);
         this.btnImprimir.Name = "btnImprimir";
         this.btnImprimir.Size = new System.Drawing.Size(131, 53);
         this.btnImprimir.TabIndex = 2;
         this.btnImprimir.Text = "Imprimir";
         this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.btnImprimir.UseVisualStyleBackColor = true;
         this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
         // 
         // rdoAmbasCaras
         // 
         this.rdoAmbasCaras.AutoSize = true;
         this.rdoAmbasCaras.Checked = true;
         this.rdoAmbasCaras.Location = new System.Drawing.Point(400, 2);
         this.rdoAmbasCaras.Name = "rdoAmbasCaras";
         this.rdoAmbasCaras.Size = new System.Drawing.Size(87, 17);
         this.rdoAmbasCaras.TabIndex = 6;
         this.rdoAmbasCaras.TabStop = true;
         this.rdoAmbasCaras.Text = "Ambas Caras";
         this.rdoAmbasCaras.UseVisualStyleBackColor = true;
         // 
         // rdoFrontal
         // 
         this.rdoFrontal.AutoSize = true;
         this.rdoFrontal.Location = new System.Drawing.Point(400, 19);
         this.rdoFrontal.Name = "rdoFrontal";
         this.rdoFrontal.Size = new System.Drawing.Size(57, 17);
         this.rdoFrontal.TabIndex = 7;
         this.rdoFrontal.Text = "Frontal";
         this.rdoFrontal.UseVisualStyleBackColor = true;
         // 
         // rdoReverso
         // 
         this.rdoReverso.AutoSize = true;
         this.rdoReverso.Location = new System.Drawing.Point(400, 36);
         this.rdoReverso.Name = "rdoReverso";
         this.rdoReverso.Size = new System.Drawing.Size(65, 17);
         this.rdoReverso.TabIndex = 8;
         this.rdoReverso.Text = "Reverso";
         this.rdoReverso.UseVisualStyleBackColor = true;
         // 
         // FrmCardPrint
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(558, 658);
         this.Controls.Add(this.rdoReverso);
         this.Controls.Add(this.rdoFrontal);
         this.Controls.Add(this.rdoAmbasCaras);
         this.Controls.Add(this.button1);
         this.Controls.Add(this.pictureBox2);
         this.Controls.Add(this.checkBox1);
         this.Controls.Add(this.pictureBox1);
         this.Controls.Add(this.btnImprimir);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.txtNumeroDeEmpleado);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Name = "FrmCardPrint";
         this.Text = "Impresion Carnet";
         this.Load += new System.EventHandler(this.Form1_Load);
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

        #endregion

        private System.Windows.Forms.TextBox txtNumeroDeEmpleado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rdoAmbasCaras;
        private System.Windows.Forms.RadioButton rdoFrontal;
        private System.Windows.Forms.RadioButton rdoReverso;
    }
}

