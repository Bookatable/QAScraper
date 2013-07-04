namespace QAScraper.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Web;

    using QAScraper.Data;
    using QAScraper.Models;

    public class FileHelper
    {
        private const string PATH = "~/Sites";

        public static Dictionary<string, string> Sites = new Dictionary<string, string>
            {
                { "Aquavit", "http://www.aquavit.org/restaurant/newyork/index.asp" },
                { "Tea At The Ritz", "http://www.theritzlondon.com/tea/reservation-en.html"},
                { "Bookatable", "http://www.bookatable.com/uk/112101/prezzo-thame"}
            };

        public static IEnumerable<string> GetFiles()
        {
            try
            {
                var path = HttpContext.Current.Server.MapPath(PATH);
                var files = Directory.EnumerateFiles(path);

                var filenames = files.Select(Path.GetFileName).ToList();

                return filenames;
            }
            catch(Exception ex)
            {
                return new List<string>();
            }
        }

        public static void UpdateSites()
        {
            var sitesContext = new SitesDB();

            var sites = sitesContext.Sites.ToList();

            //DeleteAll(new DirectoryInfo(PATH));

            foreach (var site in sites)
            {
                var request = (HttpWebRequest)WebRequest.Create(site.Url);

                request.Accept= "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.116 Safari/537.36";

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        if (stream != null)
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                var path =
                                    HttpContext.Current.Server.MapPath(string.Format("{0}/{1}.html", PATH, site.Name));
                                var page = reader.ReadToEnd();

                                var baseUri = new Uri(site.Url); 

                                var absolutePage = Regex.Replace(
                                    page,
                                    @"(?:(?:href)|(?:src))=""(?:/|(?:\.\./)+)",
                                    delegate(Match match)
                                        {
                                            
                                            var matchString = match.ToString();
                                                
                                                if (matchString.StartsWith("href"))
                                                {
                                                    matchString = matchString.Replace("href=\"", "");
                                                    return string.Format("href=\"{0}", new Uri(baseUri, matchString));
                                                }

                                            matchString = matchString.Replace("src=\"", "");
                                            return string.Format("src=\"{0}", new Uri(baseUri, matchString));
                                        });

                                File.WriteAllText(path, absolutePage);
                            }
                        }
                    }

                }
            }
        }

        private static void DeleteAll(DirectoryInfo directory)
        {
            foreach (var file in directory.GetFiles())
            {
                file.Delete();
            }
        }
    }
}