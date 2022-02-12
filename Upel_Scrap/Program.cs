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
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            using (var httpClient = new HttpClient())
            {
                var html = await httpClient.GetAsync("https://upel2.cel.agh.edu.pl/wfiis/login/index.php");
                var html_string = await html.Content.ReadAsStringAsync();

                // getting security token to log in to the website
                var token = GetToken.GetSecurityToken(html_string);

                Console.Write("Insert Login: ");
                string login = Console.ReadLine();

                // inserted password will not be seen in the console
                Console.Write("Insert Password: ");
                string password = null;
                while (true)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    password += key.KeyChar;
                }
                // values to post to the webiste
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

                // string containing html code of the website after being logged in
                // Console.WriteLine(response_text);

                // getting code of PPO_COURSE Grades
                var ppo_course = await httpClient.GetAsync("https://upel2.cel.agh.edu.pl/wfiis/grade/report/user/index.php?id=464");
                var ppo_course_str = await ppo_course.Content.ReadAsStringAsync();

                // creating new HtmlDocument from ppo_course_str
                ppo_course_str = WebUtility.HtmlDecode(ppo_course_str);
                HtmlDocument ppo_course_html = new HtmlDocument();
                ppo_course_html.LoadHtml(ppo_course_str);

                // now scrapping ... 


                 
                

                

            }
        }
    }
}

