using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XML_Json_SerializationDeserialization
{
    public partial class DovizForm : Form
    {
        public DovizForm()
        {
            InitializeComponent();
        }
        List<Doviz> dovizler = DovizFactory.DovizKurlari;

        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = dovizler[sayac % dovizler.Count].ToString();
            sayac++;
        }

        private void DovizForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
