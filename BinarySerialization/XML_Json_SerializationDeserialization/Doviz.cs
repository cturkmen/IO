using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_Json_SerializationDeserialization
{
    public class Doviz
    {
        public string KurAdi { get; set; }
        public string KurKodu { get; set; }
        public int Birim { get; set; }
        public decimal AlisKuru { get; set; }
        public decimal SatisKuru { get; set; }

        public override string ToString() => $"{Birim} {KurAdi}({KurKodu}) Alış: {AlisKuru:c4} Satış: {SatisKuru:c4}";


    }
}
