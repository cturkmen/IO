using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace XML_Json_SerializationDeserialization
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Kisi> kisiler = new List<Kisi>();
        int bufferSize = 64;
        byte[] resimArray = new byte[64];
        MemoryStream memoryStream = new MemoryStream();
        private void Form1_Load(object sender, EventArgs e)
        {
            cmbCinsiyet.Items.AddRange(Enum.GetNames(typeof(Cinsiyetler)));
            cmbMedeniDurum.Items.AddRange(Enum.GetNames(typeof(MedeniDurumlar)));
            DialogResult cevap = MessageBox.Show("uygulamanıza dışarıdan veri eklemek istermisini?", "İçeri aktar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                içerAktarToolStripMenuItem.PerformClick();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                Kisi yeniKisi = new Kisi() //Object initializer
                {
                    Ad = txtAd.Text,
                    Boy = int.Parse(txtBoy.Text),
                    Cinsiyet = (Cinsiyetler)Enum.Parse(typeof(Cinsiyetler), cmbCinsiyet.SelectedItem.ToString()),
                    DogumTarihi = dtpDogumTarihi.Value,
                    Kilo = int.Parse(txtKilo.Text),
                    MedeniDurum = (MedeniDurumlar)Enum.Parse(typeof(MedeniDurumlar), cmbMedeniDurum.SelectedItem.ToString()),
                    Meslek = txtMeslek.Text,
                    Soyad = txtSoyad.Text,
                    TCKN = txtTCKN.Text
                };

                if (memoryStream.Length > 0)
                {
                    yeniKisi.Fotograf = memoryStream.ToArray();//Base64 coding olarak kaydeder xml e
                }
                kisiler.Add(yeniKisi);
                //memoryStream = new MemoryStream();//memorystream temizlendi
                ListeyiDoldur();
                FormuTemizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        void FormuTemizle()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                    item.Text = string.Empty;
                else if (item is ComboBox)
                    (item as ComboBox).SelectedIndex = 0;
                else if (item is DateTimePicker)
                    (item as DateTimePicker).Value = DateTime.Now;
                else if (item is PictureBox)
                    (item as PictureBox).Image = null;
            }
        }
        void ListeyiDoldur()
        {
            lstKisi.Items.Clear();
            foreach (Kisi item in kisiler)
            {
                lstKisi.Items.Add(item);
            }
        }
        Kisi seciliKisi;
        private void lstKisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstKisi.SelectedIndex == -1) return;
            seciliKisi = lstKisi.SelectedItem as Kisi;
            txtAd.Text = seciliKisi.Ad;
            txtSoyad.Text = seciliKisi.Soyad;
            txtBoy.Text = seciliKisi.Boy.ToString();
            txtKilo.Text = seciliKisi.Kilo.ToString();
            txtMeslek.Text = seciliKisi.Meslek;
            txtTCKN.Text = seciliKisi.TCKN;
            cmbCinsiyet.SelectedIndex = (int)seciliKisi.Cinsiyet;
            cmbMedeniDurum.SelectedIndex = (int)seciliKisi.MedeniDurum;
            dtpDogumTarihi.Value = seciliKisi.DogumTarihi;
            if (seciliKisi.Fotograf.Length > 0 || seciliKisi.Fotograf==null)
            {
                MemoryStream hamResim = new MemoryStream(seciliKisi.Fotograf);
                pbResim.Image = new Bitmap(hamResim);
            }
            else
                pbResim.Image = null;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (seciliKisi == null)
            {
                MessageBox.Show("Neyi güncelleyeyim?");
                return;
            }
            DialogResult cevap = MessageBox.Show($"{seciliKisi.Ad} adlı kişiyi güncellemek istiyor musunuz?", "Kişi güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                try
                {
                    seciliKisi = kisiler.Where(item => item.TCKN == seciliKisi.TCKN).FirstOrDefault();
                    seciliKisi.Ad = txtAd.Text;
                    seciliKisi.Soyad = txtSoyad.Text;
                    seciliKisi.DogumTarihi = dtpDogumTarihi.Value;
                    seciliKisi.Meslek = txtMeslek.Text;
                    ListeyiDoldur();
                    FormuTemizle();
                    seciliKisi = null;
                    MessageBox.Show("Güncelleme Başarılı");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstKisi.SelectedItem == null)
            {
                MessageBox.Show("Neyi sileyim?");
                return;
            }
            seciliKisi = lstKisi.SelectedItem as Kisi;

            DialogResult cevap = MessageBox.Show($"{seciliKisi.Ad} adlı kişiyi silmek istiyor musunuz?", "Kişi silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                kisiler.Remove(seciliKisi);
                ListeyiDoldur();
                FormuTemizle();
                seciliKisi = null;
                MessageBox.Show("Kişi silindi");
            }
        }
        void ListeyiDoldur(List<Kisi> aramasonucu)
        {
            lstKisi.Items.Clear();
            foreach (Kisi item in aramasonucu)
            {
                lstKisi.Items.Add(item);
            }
        }
        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            List<Kisi> aramasonucu = Kisi.Ara(kisiler, txtArama.Text);
            ListeyiDoldur(aramasonucu);
        }

        private void dışarıAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (kisiler.Count == 0)
            {
                MessageBox.Show("kimse kayıtlı değil");
                return;
            }
            DosyaKaydet.Title = $"{kisiler.Count} adet kişi dışarı aktarılacak";
            DosyaKaydet.Filter = "XML Format | *.xml";
            DosyaKaydet.FileName = string.Empty;
            DosyaKaydet.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (DosyaKaydet.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Kisi>));
                TextWriter writer = new StreamWriter(DosyaKaydet.FileName);
                xmlSerializer.Serialize(writer, kisiler);
                writer.Close();
                writer.Dispose();
                MessageBox.Show("kişiler dışarıya aktarıldı");
            }
        }

        private void içerAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DosyaAc.Title = "Bir kişi XML dosyasını seçiniz";
            DosyaAc.Filter = "XML Format | *.xml";
            DosyaAc.Multiselect = false;
            DosyaAc.FileName = string.Empty;
            DosyaAc.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (DosyaAc.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Kisi>));
                TextReader reader = new StreamReader(DosyaAc.FileName);
                kisiler = xmlSerializer.Deserialize(reader) as List<Kisi>;
                ListeyiDoldur();
                reader.Close();
                reader.Dispose();
                MessageBox.Show($"{kisiler.Count} adet kişi aktarıldı.");
            }
        }

        private void dışaAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (kisiler.Count == 0)
            {
                MessageBox.Show("kimse kayıtlı değil");
                return;
            }
            DosyaKaydet.Title = "bir JSON dosyası dışarı aktarılacak";
            DosyaKaydet.Filter = "(*.json) | *.json";
            DosyaKaydet.FileName = string.Empty;
            DosyaKaydet.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (DosyaKaydet.ShowDialog() == DialogResult.OK)
            {
                FileStream dosya = File.Open(DosyaKaydet.FileName, FileMode.OpenOrCreate);
                StreamWriter writer = new StreamWriter(dosya);
                writer.Write(JsonConvert.SerializeObject(kisiler));
                writer.Close();
                writer.Dispose();
                MessageBox.Show($"{kisiler.Count} adet kişi JSON olarak dışarıya aktarıldı");
            }
        }
        private void içeAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DosyaAc.Title = "Bir kişi JSON dosyasını seçiniz";
            DosyaAc.Filter = "(*.json) | *.json";
            DosyaAc.Multiselect = false;
            DosyaAc.FileName = string.Empty;
            DosyaAc.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (DosyaAc.ShowDialog() == DialogResult.OK)
            {
                FileStream dosya = File.OpenRead(DosyaAc.FileName);
                StreamReader reader = new StreamReader(dosya);
                string dosyaIcerigi = reader.ReadToEnd();
                reader.Close();
                dosya.Close();
                //kisiler = JsonConvert.DeserializeObject(dosyaIcerigi) as List<Kisi>;
                kisiler = JsonConvert.DeserializeObject<List<Kisi>>(dosyaIcerigi);

                ListeyiDoldur();
                MessageBox.Show($"{kisiler.Count} adet kişi JSON'dan aktarıldı.");
            }
        }

        
        private void pbResim_Click(object sender, EventArgs e)
        {
            DosyaAc.Title = "Bir fotoğraf seçiniz";
            DosyaAc.Filter = "JPG | *.jpg";
            DosyaAc.Multiselect = false;
            DosyaAc.FileName = string.Empty;
            DosyaAc.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (DosyaAc.ShowDialog() == DialogResult.OK)
            {
                FileStream dosya = File.Open(DosyaAc.FileName, FileMode.Open);
                while (dosya.Read(resimArray, 0, bufferSize) != 0)
                {
                    memoryStream.Write(resimArray, 0, resimArray.Length);
                }
                dosya.Close();
                dosya.Dispose();
                pbResim.Image = new Bitmap(memoryStream);
            }
        }

        private void btnResmiDisariAktar_Click(object sender, EventArgs e)
        {
            if (pbResim.Image == null) return;
            //MemoryStream stream = new MemoryStream(seciliKisi.Fotograf);

            if (seciliKisi.Fotograf.Length == 0)
            {
                MessageBox.Show("RAM'de dosya bulunmamaktadır");
                return;
            }
            DosyaKaydet.Title = "Bir resim dosyası seçiniz";
            DosyaKaydet.Filter = "JPG Dosyaları | *.jpg";
            DosyaKaydet.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DosyaKaydet.FileName = string.Empty;
            if (DosyaKaydet.ShowDialog() == DialogResult.OK)
            {
                FileStream dosya = File.Create(DosyaKaydet.FileName);
                memoryStream.Seek(0, SeekOrigin.Begin);
                while (memoryStream.Read(resimArray, 0, bufferSize) != 0)
                {
                    dosya.Write(resimArray, 0, resimArray.Length);

                }
                dosya.Close();
                dosya.Dispose();
                MessageBox.Show("resim aktarıldı");
            }
        }

        //json da veri kaybı olabiliyor. xml daha güvenli. büyük boyutlu dosyalar için xml kullanmak dah güvenli olur. tag lar daha keskin
    }
}
