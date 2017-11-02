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

namespace BinarySerialization
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int bufferSize = 64;
        byte[] resimArray = new byte[64];
        MemoryStream memoryStream = new MemoryStream();// byte akışını tutacak. RAM'den aktarıyor

        private void btnAc_Click(object sender, EventArgs e)
        {
            dosyaAc.Title = "Bir resim dosyası seçiniz"; //pencerenin üstündeki yazı
            dosyaAc.Filter = "JPG Dosyaları | *.jpg"; // sadece jpg dosyalarını seçmesi içn filtre.Filtre yazmazsak bütün dosya tiplerinden seçebilir
            dosyaAc.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures); //ilk gelecek path.ilk Resimlerim gelir.
            dosyaAc.FileName = string.Empty; //FileNames propertysi multiselect=true olduğunda kullanılır. 
            dosyaAc.Multiselect = false;//çoklu seçim olmasın
            if (dosyaAc.ShowDialog() == DialogResult.OK)
            {
                FileStream dosya = File.Open(dosyaAc.FileName, FileMode.Open);
                while (dosya.Read(resimArray, 0, bufferSize) != 0)
                {
                    memoryStream.Write(resimArray, 0, resimArray.Length);
                }
                dosya.Close();
                dosya.Dispose();// RAM den uçur
                pbResim.Image = new Bitmap(memoryStream);
                //pbResim.ImageLocation = dosyaAc.FileName;//seçilen dosyanın yolunu tutar.Resim olunca bu ok ama müzik text olunca bunu kullanamayız. stream olarak olumak daha doğru olur.
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (memoryStream.Length == 0) // program ilk açıldığında kaydete basarsa boş gelicek ve memory de birşey yoksa
            {
                MessageBox.Show("RAM'de dosya bulunmamaktadır");
                return;
            }
            dosyaKaydet.Title = "Bir resim dosyası seçiniz";
            dosyaKaydet.Filter = "JPG Dosyaları | *.jpg";
            dosyaKaydet.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dosyaKaydet.FileName = string.Empty;
            if (dosyaKaydet.ShowDialog() == DialogResult.OK)
            {
                FileStream dosya = File.Create(dosyaKaydet.FileName);
                memoryStream.Seek(0, SeekOrigin.Begin);//0'dan başlayıp tarasın
                while (memoryStream.Read(resimArray, 0, bufferSize) != 0)
                {
                    dosya.Write(resimArray, 0, resimArray.Length);
                }
                dosya.Close();
                dosya.Dispose();
            }
        }
    }
}
