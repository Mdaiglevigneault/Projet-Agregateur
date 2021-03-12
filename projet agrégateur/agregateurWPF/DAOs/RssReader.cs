using System.Xml;
using System.IO;
using System.Net;

namespace agregateurMetier
{
    public class RssReader {
        public XmlDocument GetXMLDocumentFromUrl(string url) {

            string t_xmltext = GetStringFromUrl(url);
            XmlDocument t_doc = new XmlDocument();
            t_doc.LoadXml(t_xmltext);
            return t_doc;
        }

        public string GetStringFromUrl(string url)
        {
            WebRequest t_request = WebRequest.Create(url);
            WebResponse t_response = t_request.GetResponse();
            Stream t_responseStream = t_response.GetResponseStream();
            StreamReader t_reader = new StreamReader(t_responseStream);
            return t_reader.ReadToEnd();
        }
    }
}
