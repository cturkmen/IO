namespace XML_RSSOkuma
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
            this.txtKanal = new System.Windows.Forms.TextBox();
            this.btnYenile = new System.Windows.Forms.Button();
            this.cmbBaslik = new System.Windows.Forms.ComboBox();
            this.pbResim = new System.Windows.Forms.PictureBox();
            this.txtAciklama = new System.Windows.Forms.RichTextBox();
            this.llHaber = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbResim)).BeginInit();
            this.SuspendLayout();
            // 
            // txtKanal
            // 
            this.txtKanal.Location = new System.Drawing.Point(13, 13);
            this.txtKanal.Name = "txtKanal";
            this.txtKanal.Size = new System.Drawing.Size(305, 20);
            this.txtKanal.TabIndex = 0;
            // 
            // btnYenile
            // 
            this.btnYenile.Location = new System.Drawing.Point(324, 13);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(75, 23);
            this.btnYenile.TabIndex = 1;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.UseVisualStyleBackColor = true;
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);
            // 
            // cmbBaslik
            // 
            this.cmbBaslik.FormattingEnabled = true;
            this.cmbBaslik.Location = new System.Drawing.Point(13, 49);
            this.cmbBaslik.Name = "cmbBaslik";
            this.cmbBaslik.Size = new System.Drawing.Size(386, 21);
            this.cmbBaslik.TabIndex = 2;
            this.cmbBaslik.SelectedIndexChanged += new System.EventHandler(this.cmbBaslik_SelectedIndexChanged);
            // 
            // pbResim
            // 
            this.pbResim.Location = new System.Drawing.Point(13, 92);
            this.pbResim.Name = "pbResim";
            this.pbResim.Size = new System.Drawing.Size(386, 175);
            this.pbResim.TabIndex = 3;
            this.pbResim.TabStop = false;
            // 
            // txtAciklama
            // 
            this.txtAciklama.Location = new System.Drawing.Point(13, 274);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(386, 126);
            this.txtAciklama.TabIndex = 4;
            this.txtAciklama.Text = "";
            // 
            // llHaber
            // 
            this.llHaber.AutoSize = true;
            this.llHaber.Location = new System.Drawing.Point(13, 407);
            this.llHaber.Name = "llHaber";
            this.llHaber.Size = new System.Drawing.Size(61, 13);
            this.llHaber.TabIndex = 5;
            this.llHaber.TabStop = true;
            this.llHaber.Text = "Haber Linki";
            this.llHaber.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llHaber_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 451);
            this.Controls.Add(this.llHaber);
            this.Controls.Add(this.txtAciklama);
            this.Controls.Add(this.pbResim);
            this.Controls.Add(this.cmbBaslik);
            this.Controls.Add(this.btnYenile);
            this.Controls.Add(this.txtKanal);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbResim)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtKanal;
        private System.Windows.Forms.Button btnYenile;
        private System.Windows.Forms.ComboBox cmbBaslik;
        private System.Windows.Forms.PictureBox pbResim;
        private System.Windows.Forms.RichTextBox txtAciklama;
        private System.Windows.Forms.LinkLabel llHaber;
    }
}

