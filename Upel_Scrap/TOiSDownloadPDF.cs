using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using HtmlAgilityPack;

namespace Upel_Scrap
{
    class TOiSDownloadPDF
    {
        public static async Task DownloadPdf(HttpClient httpClient, List<string> URLs) 
        {
            Console.WriteLine();
            int index = 0;
            foreach(var URL in URLs)
            {
                byte[] FileBytes = await httpClient.GetByteArrayAsync(URL);
                Console.WriteLine($"Downloading Wykład_{index}");
                File.WriteAllBytes(@$"D:\Semestr 3\TOiS\Wykład_{index++}.pdf", FileBytes);
            }
            Console.WriteLine();
            Console.WriteLine("Download completed, check your folder to see files");
        }

        public static async Task<List<string>> GetURLs(HttpClient httpClient)
        {
            var TOiSCourse = await httpClient.GetAsync("https://upel2.cel.agh.edu.pl/wfiis/course/view.php?id=64");
            var TOiSCourseStr = await TOiSCourse.Content.ReadAsStringAsync();

            TOiSCourseStr = WebUtility.HtmlDecode(TOiSCourseStr);
            HtmlDocument TOiSCourseHtml = new HtmlDocument();
            TOiSCourseHtml.LoadHtml(TOiSCourseStr);

            var aLinks = TOiSCourseHtml.QuerySelectorAll("a").Where(n => n.InnerText.Contains("Wykład ")).Skip(1);
            List<string> URLs = GetURLsList(aLinks);
            return URLs;
        }

        public static List<string> GetURLsList(IEnumerable<HtmlNode> aLinks)
        {
            List<string> URLs = new List<string>();
            foreach(var aLink in aLinks)
            {
                URLs.Add(aLink.GetAttributeValue("href", null));
            }
            return URLs;
        }
    }
}
