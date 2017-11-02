using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XML_Json_SerializationDeserialization
{
    public static class DovizFactory
    {
        private static List<Doviz> _dovizKurlari;
        public static List<Doviz> DovizKurlari
        {
            get
            {
                _dovizKurlari = new List<Doviz>();// daha önceden geldiyse her seferinde sıfırla

                XElement xElement = XElement.Load("http://www.tcmb.gov.tr/kurlar/today.xml"); //web deki xml dosyasını alır.

                List<XElement> kurlar = (from k in xElement.Elements() where k.Element("CurrencyName") != null && k.Attribute("Kod").Value!= "XDR" select k).ToList();
                //webdeki xml dosyasını seçince hepsi geliyo. burada sadece CurrencyName'leri istiyoruz.
                //xelement içindeki bütün elementleri gez. her k elementinin CurrencyName'i null olmayanları ve her k attributenun Kod değeri XDR olmayanları getir 

                //List<XElement> kurlar2 = xElement.Elements().Where(k => k.Element("CurrencyName") != null && k.Attribute("Kod").Value != "XDR").ToList();
                //yukardakinin aynısı

                //xml dosyasında bordo renkli olanlar attribute, mor olanlar Element
                foreach (XElement item in kurlar)
                {
                    _dovizKurlari.Add(new Doviz()
                    {
                        KurAdi = item.Element("Isim").Value,
                        KurKodu = item.Attribute("CurrencyCode").Value,
                        Birim = int.Parse(item.Element("Unit").Value),
                        AlisKuru = decimal.Parse(item.Element("ForexBuying").Value.Replace(".", ",")),
                        SatisKuru = decimal.Parse(item.Element("ForexSelling").Value.Replace(".", ","))


                });
                }
                return _dovizKurlari;
            }
        }
    }
}
