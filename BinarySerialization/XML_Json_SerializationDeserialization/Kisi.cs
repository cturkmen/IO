using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_Json_SerializationDeserialization
{
    public class Kisi
    {
        #region Fields
        private string _ad, _soyad, _TCKN, _meslek;
        private int _boy, _kilo, _yas;
        private DateTime _dogumTarihi;
        private Cinsiyetler _cinsiyet;
        private MedeniDurumlar _medeniDurum;
        #endregion

        #region Properties
        public string Ad
        {
            get { return _ad; }
            set { _ad = AdSoyadHazirla(value); }
        }
        public string Soyad
        {
            get { return _soyad; }
            set { _soyad = AdSoyadHazirla(value); }
        }
        /// <summary>
        /// Boy değeri santimetre tipindedir.
        /// </summary>
        public int Boy
        {
            get { return _boy; }
            set { _boy = value; }
        }
        /// <summary>
        /// Kilogram cinsinden yazılmalıdır
        /// </summary>
        public int Kilo
        {
            get { return _kilo; }
            set { _kilo = value; }
        }
        public int Yas
        {
            get { return _yas; }
        }
        public DateTime DogumTarihi
        {
            get { return _dogumTarihi; }
            set
            {
                TimeSpan aralik = DateTime.Now - value;
                double yil = aralik.TotalDays / 365;
                _yas = (int)yil;
                if (yil > 17)
                {
                    _dogumTarihi = value;
                }
                else
                {
                    throw new Exception("Sisteme 18 yaşından gün almamış kişileri kaydedemiyoruz!");
                }
            }
        }
        public string TCKN
        {
            get { return _TCKN; }
            set { _TCKN = TCKNKontrol(value); }
        }
        public Cinsiyetler Cinsiyet
        {
            get { return _cinsiyet; }
            set { _cinsiyet = value; }
        }
        public MedeniDurumlar MedeniDurum
        {
            get { return _medeniDurum; }
            set { _medeniDurum = value; }
        }
        public string Meslek
        {
            get { return _meslek; }
            set { _meslek = value; }
        }

        public byte[] Fotograf { get; set;}
        #endregion

        #region Methods
        private string TCKNKontrol(string tckn)
        {
            if (tckn.Length != 11)
                throw new Exception("TCKN 11 haneli olmalıdır");
            if (tckn[0] == '0')
                throw new Exception("TCKN '0' ile başlayamaz");
            foreach (char harf in tckn)
                if (!char.IsDigit(harf))
                    throw new Exception("TCKN içerisinde sadece rakam bulunmalıdır");
            //int toplam = 0;
            //for (int i = 0; i < 10; i++)
            //{
            //    toplam += Convert.ToInt32(tckn[i]);
            //}
            //if (toplam % 10 != Convert.ToInt32(tckn[10]))
            //{
            //    throw new Exception("TCKN numaranız geçersizdir.");
            //}
            return tckn;
        }
        private string AdSoyadHazirla(string kelime)
        {
            foreach (char harf in kelime)
            {
                if (!(char.IsLetter(harf) || char.IsWhiteSpace(harf)))
                    throw new Exception("Ad veya Soyad içerisinde geçersiz karakter bulunmaktadır.");
            }
            if (kelime.Trim().Length < 3)
                throw new Exception("Ad veya Soyad en az 3 karakter olmalı");
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(kelime);
        }

        public static List<Kisi> Ara(List<Kisi> kisiler, string kelime)
        {
            List<Kisi> sonuclar = new List<Kisi>();
            //foreach (Kisi item in kisiler)
            //{
            //    if (item.Ad.ToLower().Contains(kelime.ToLower()) || item.Soyad.ToLower().Contains(kelime.ToLower()) || item.TCKN.ToLower().Contains(kelime.ToLower()))
            //        sonuclar.Add(item);
            //}
            sonuclar = kisiler.Where(item => item.Ad.ToLower().Contains(kelime.ToLower()) || item.Soyad.ToLower().Contains(kelime.ToLower()) || item.TCKN.ToLower().Contains(kelime.ToLower())).ToList();
            return sonuclar;
        }
        public override string ToString()
        {
            return $"{this.Ad} {this.Soyad} - {this.Yas} {this.Cinsiyet}";
        }
        #endregion
    }
    public enum Cinsiyetler
    {
        Erkek,
        Kadın,
        Belirsiz
    }
    public enum MedeniDurumlar
    {
        Evli,
        Bekar,
        Dul
    }
}

