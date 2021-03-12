using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace agregateurMetier
{
    public class CsGoUpdateDAO
    {
        private char[] m_Delimiters = { ' ', ':', '-' };

        public List<CsGoUpdate> GetUpdates()
        {
            //url reddit
            string t_json = new RssReader().GetStringFromUrl("https://api.rss2json.com/v1/api.json?rss_url=https%3A%2F%2Fblog.counter-strike.net%2Findex.php%2Ffeed%2F");

            CsGoUpdateitems t_CsItems = JsonConvert.DeserializeObject<CsGoUpdateitems>(t_json);

            foreach (CsGoUpdate t_item in t_CsItems.ListItems) {

                string t_published = t_item.pubDateStr;
                string[] t_Date = t_published.Split(m_Delimiters);
                DateTime t_ReleaseDate = new DateTime(Int32.Parse(t_Date[0]), Int32.Parse(t_Date[1]), Int32.Parse(t_Date[2]), Int32.Parse(t_Date[3]), Int32.Parse(t_Date[4]), Int32.Parse(t_Date[5]));
                t_item.UpdateDate = t_ReleaseDate;
                TimeSpan t_DeltaRelease = DateTime.UtcNow - t_ReleaseDate;
                t_item.PublicatedSince = t_DeltaRelease.Days > 365 ? String.Format("{0} Ans, {1} Mois", (t_DeltaRelease.Days / 365), (t_DeltaRelease.Days / 31) - (t_DeltaRelease.Days / 365) * 12) :
                                t_DeltaRelease.Days > 31 ? String.Format("{0} Mois, {1} Jours", (t_DeltaRelease.Days / 31), t_DeltaRelease.Days - (t_DeltaRelease.Days / 31) * 31) :
                                t_DeltaRelease.Days > 0 ? String.Format("{0} Jours, {1} Heures", t_DeltaRelease.Days, t_DeltaRelease.Hours) :
                                t_DeltaRelease.Hours > 0 ? String.Format("{0} Heures, {1} Minutes", t_DeltaRelease.Hours, t_DeltaRelease.Minutes) :
                                t_DeltaRelease.Minutes > 0 ? String.Format("{0} Minutes, {1} Secondes", t_DeltaRelease.Minutes, t_DeltaRelease.Seconds) :
                                String.Format("{0} Secondes", t_DeltaRelease.Seconds);

            }
            return t_CsItems.ListItems;
        }
    }
}
