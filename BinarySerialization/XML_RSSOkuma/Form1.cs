using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XML_RSSOkuma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Haber> haberler;
        private void btnYenile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKanal.Text.Trim()))
            {
                haberler = RSSFactory.Haberler(txtKanal.Text.Trim());
                cmbBaslik.DataSource = haberler;
                cmbBaslik.DisplayMember = "Baslik";

            }
        }

        private void cmbBaslik_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBaslik.SelectedItem == null) return;
            Haber seciliHaber = cmbBaslik.SelectedItem as Haber;
            llHaber.Text = seciliHaber.Baslik;
            txtAciklama.Text = seciliHaber.Aciklama;
        }

        private void llHaber_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cmbBaslik.SelectedItem == null) return;
            Haber seciliHaber = cmbBaslik.SelectedItem as Haber;
            Process.Start(seciliHaber.Link);
        }
    }
}
