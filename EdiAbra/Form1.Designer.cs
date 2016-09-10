namespace EdiAbra
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Wymagana metoda obsługi projektanta — nie należy modyfikować 
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.listBoxPlikiEdi = new System.Windows.Forms.ListBox();
            this.buttonGenerujDokumenty = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(520, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Wczytaj pliki EDI";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBoxPlikiEdi
            // 
            this.listBoxPlikiEdi.FormattingEnabled = true;
            this.listBoxPlikiEdi.Location = new System.Drawing.Point(12, 12);
            this.listBoxPlikiEdi.Name = "listBoxPlikiEdi";
            this.listBoxPlikiEdi.Size = new System.Drawing.Size(415, 472);
            this.listBoxPlikiEdi.TabIndex = 1;
            // 
            // buttonGenerujDokumenty
            // 
            this.buttonGenerujDokumenty.Location = new System.Drawing.Point(520, 84);
            this.buttonGenerujDokumenty.Name = "buttonGenerujDokumenty";
            this.buttonGenerujDokumenty.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerujDokumenty.TabIndex = 2;
            this.buttonGenerujDokumenty.Text = "button2";
            this.buttonGenerujDokumenty.UseVisualStyleBackColor = true;
            this.buttonGenerujDokumenty.Click += new System.EventHandler(this.buttonGenerujDokumenty_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(520, 129);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "LoginXl";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 494);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonGenerujDokumenty);
            this.Controls.Add(this.listBoxPlikiEdi);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "AbraEDI";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBoxPlikiEdi;
        private System.Windows.Forms.Button buttonGenerujDokumenty;
        private System.Windows.Forms.Button button2;
    }
}

