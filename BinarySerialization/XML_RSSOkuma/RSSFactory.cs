using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XML_RSSOkuma
{
    public static class RSSFactory
    {
        private static List<Haber> _haberler;
        public static List<Haber> Haberler (string kanal)
        {

            WebRequest request = WebRequest.Create(kanal);
            WebResponse response = request.GetResponse();
            Stream rssStream = response.GetResponseStream();
            
            XmlDocument rssDocument = new XmlDocument();
            rssDocument.Load(rssStream);
            XmlNodeList rssItems = rssDocument.SelectNodes("rss/channel/item");
            _haberler = new List<Haber>();

            for (int i = 0; i < rssItems.Count; i++)
            {
                Haber gelenHaber = new Haber();
                XmlNode node;
                node = rssItems.Item(i).SelectSingleNode("title");
                if (node!=null)
                 gelenHaber.Baslik = node.InnerText;
                node = rssItems.Item(i).SelectSingleNode("description");
                if (node != null)
                    gelenHaber.Aciklama = node.InnerText;
                node = rssItems.Item(i).SelectSingleNode("link");
                if (node != null)
                    gelenHaber.Link = node.InnerText;
                _haberler.Add(gelenHaber);

            }

            return _haberler;
        }
    }
}
