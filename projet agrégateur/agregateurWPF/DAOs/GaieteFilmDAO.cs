using System.Xml;
using System.Collections.Generic;

namespace agregateurMetier
{
    public class GaieteFilmDAO
    {
        public List<GaieteFilm> GetRepresentations()
        {
            //url reddit
            XmlDocument t_doc = new RssReader().GetXMLDocumentFromUrl("https://www.cinemagaiete.com/feed/rss/");

            //liste pour CinemaGaiete
            XmlNodeList t_XmlItems = t_doc.DocumentElement.FirstChild.ChildNodes;

            List<GaieteFilm> t_Representations = new List<GaieteFilm>();

            foreach (XmlNode t_XmlItem in t_XmlItems)
            {
                //pour chaque entry dans le XML fournit
                if (t_XmlItem.Name.Equals("item"))
                {
                    GaieteFilm t_Film = new GaieteFilm();

                    t_Film.Title = t_XmlItem.ChildNodes[0].InnerText;
                    string t_desc = t_XmlItem.ChildNodes[1].InnerText;

                    while (t_desc.IndexOf("&#8217;") != -1)
                    {
                        string t_p1 = t_desc.Substring(0, t_desc.IndexOf("&#8217;"));
                        string t_p2 = t_desc.Substring(t_desc.IndexOf("&#8217;")+7, t_desc.Length - (t_desc.IndexOf("&#8217;")+7));
                        t_desc = string.Format("{0}\'{1}", t_p1, t_p2);
                    }

                    t_Film.Desc = t_desc;
                    t_Film.Link = t_XmlItem.ChildNodes[2].InnerText;

                    t_Representations.Add(t_Film);
                }
            }

            return t_Representations;
        }
    }
}
