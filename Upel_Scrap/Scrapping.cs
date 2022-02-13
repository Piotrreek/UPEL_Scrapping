using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using HtmlAgilityPack;


namespace Upel_Scrap
{
    class Scrapping
    {
        public static int Choose()
        {
            Console.WriteLine(@$"{Environment.NewLine}What do you want to do on the website ? {Environment.NewLine}1. Get grades and average grade(%) from Basics of Object Oriented Programming {Environment.NewLine}2. ... { Environment.NewLine}5. Exit");
            Console.Write("What is your choice ?: ");
            return int.Parse(Console.ReadLine());
        }
        public static async Task PpoGrades(HttpClient httpClient)
        {
            var PpoCourse = await httpClient.GetAsync("https://upel2.cel.agh.edu.pl/wfiis/grade/report/user/index.php?id=464");
            var PpoCourseStr = await PpoCourse.Content.ReadAsStringAsync();

            PpoCourseStr = WebUtility.HtmlDecode(PpoCourseStr);
            HtmlDocument PpoCourseHtml = new HtmlDocument();
            PpoCourseHtml.LoadHtml(PpoCourseStr);

            Console.WriteLine(PpoCourseStr);
        }
    }
}
