using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using HtmlAgilityPack;


namespace Upel_Scrap
{

    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            using (var httpClient = new HttpClient())
            {
                var html = await httpClient.GetAsync("https://upel2.cel.agh.edu.pl/wfiis/login/index.php");
                var html_string = await html.Content.ReadAsStringAsync();

                var token = GetToken.GetSecurityToken(html_string);
                Console.Write("Insert Login: ");
                string login = Console.ReadLine();
                Console.Write("Insert Password: ");
                string password = Console.ReadLine();

                var values = new Dictionary<string, string>
                {
                {"anchor", "" },
                {"logintoken", token },
                {"username", login },
                {"password", password }
                };
                var content = new FormUrlEncodedContent(values);

                var response = await httpClient.PostAsync("https://upel2.cel.agh.edu.pl/wfiis/login/index.php", content);
                var response_text = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response_text);

            }
        }
    }
}

