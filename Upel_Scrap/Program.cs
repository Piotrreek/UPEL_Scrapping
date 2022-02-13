using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Upel_Scrap
{
    class Program
    {
        private static HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            using var httpClient = new HttpClient();
            var Html = await httpClient.GetAsync("https://upel2.cel.agh.edu.pl/wfiis/login/index.php");
            var HtmlString = await Html.Content.ReadAsStringAsync();
            var Token = SignToWebsite.GetSecurityToken(HtmlString);
            var LoginAndPassword = SignToWebsite.GetLoginAndPassword();
            var Response = await SignToWebsite.SignIn(Token, LoginAndPassword, httpClient);

            int choice = Scrapping.Choose();
            do
            {
                switch (choice)
                {
                    case 1:
                        await Scrapping.PpoGrades(httpClient);
                        break;
                    case 2:

                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Bad input, try again");
                        break;
                }
                choice = Scrapping.Choose();
            } while (true);
        }
    }
}

