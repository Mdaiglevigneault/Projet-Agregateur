using System;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace agregateurMetier
{
    public class RedditPostDAO
    {
        private string m_CurrentImgUrl;

        public List<RedditPost> GetRedditPosts(string url)
        {
            XmlDocument t_doc = new RssReader().GetXMLDocumentFromUrl(url);

            if (t_doc.DocumentElement.LastChild.InnerText == "résultats de la recherche" || t_doc.DocumentElement.LastChild.InnerText == "search results")
                throw new Exception("not a sred");

            XmlNodeList t_XmlEntries = t_doc.DocumentElement.ChildNodes;

            List<RedditPost> t_posts = new List<RedditPost>();

            foreach (XmlNode t_XmlEntry in t_XmlEntries) {
                //pour chaque entry dans le XML fournit
                if (t_XmlEntry.Name.Equals("entry")) {     
                    //pour chaque content dans les entry
                    XmlNode item = t_XmlEntry.ChildNodes[2];

                    bool t_asImage = GetImageUrl(item.InnerText);
                    //si cest un meme (image)
                    if (t_asImage) {
                        RedditPost t_post = new RedditPost();
                        t_post.Titre = t_XmlEntry.ChildNodes[6].InnerText;
                        t_post.PostLink = t_XmlEntry.ChildNodes[4].Attributes["href"].Value;
                        t_post.Img = new BitmapImage(new Uri(m_CurrentImgUrl, UriKind.Absolute));
                        t_posts.Add(t_post);
                    }
                }
            }
            return t_posts;
        }

        private bool GetImageUrl(string a_EntryContent)
        {
            //si c'est pas un meme
            if (a_EntryContent.IndexOf("<!-- SC_OFF -->") != -1)
                return false;

            //couper jusqu'au début de l'url de la potentiel image
            int t_indexof = a_EntryContent.IndexOf("<span><a href=\"https://") + 15;
            a_EntryContent = a_EntryContent.Substring(t_indexof, (a_EntryContent.Length - t_indexof));

            //vérifie si l'url est une url d'image
            if ((a_EntryContent.IndexOf("https://i.") == -1) && (a_EntryContent.IndexOf("https://imgur.") == -1))
                return false;

            //coupe a la fin de l'url
            a_EntryContent = a_EntryContent.Substring(0, a_EntryContent.IndexOf(">[link]") - 1);
            //ajoute [.gif] au url ne comprenant pas d'extension de fichier (.png/.jpg/.gif) a la fin
            if ((a_EntryContent.IndexOf(".png") == -1) && (a_EntryContent.IndexOf(".jpg") == -1) && (a_EntryContent.IndexOf(".gif") == -1))
                a_EntryContent = string.Format("{0}.jpg", a_EntryContent);//TODO
            m_CurrentImgUrl = a_EntryContent;
            return true;
        }
    }
}
