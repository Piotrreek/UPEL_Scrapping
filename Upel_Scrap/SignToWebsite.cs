using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;


namespace Upel_Scrap
{
    class SignToWebsite
    {
        public static string GetSecurityToken(string document)
        {
            List<string> List = document.Split(">").ToList();
            var Str = List.FirstOrDefault(s => s.Contains("name=\"logintoken\""));
            Str = Str.Trim();
            Str = Str.Substring(Str.Length - 33);
            Str = Str.Remove(Str.Length - 1);
            return Str;
        }

        public static List<string> GetLoginAndPassword()
        {
            Console.Write("Insert Login: ");
            string Login = Console.ReadLine();
            // inserted password will not be seen in the console
            Console.Write("Insert Password: ");
            string Password = null;
            while (true)
            {
                var Key = Console.ReadKey(true);
                if (Key.Key == ConsoleKey.Enter)
                    break;
                Password += Key.KeyChar;
            }
            List<string> Data = new List<string> { Login, Password };
            return Data;
        }

        public static async Task<HttpResponseMessage> SignIn(string token, List<string> login_password,  HttpClient httpClient)
        {
            var Values = new Dictionary<string, string>
                {
                {"anchor", "" },
                {"logintoken", token },
                {"username", login_password[0] },
                {"password", login_password[1] }
                };
            var Content = new FormUrlEncodedContent(Values);
            return  await httpClient.PostAsync("https://upel2.cel.agh.edu.pl/wfiis/login/index.php", Content);
        }

        public static async Task<bool> SignInFun(HttpClient httpClient)
        {
            var Html = await httpClient.GetAsync("https://upel2.cel.agh.edu.pl/wfiis/login/index.php");
            var HtmlString = await Html.Content.ReadAsStringAsync();
            var Token = SignToWebsite.GetSecurityToken(HtmlString);
            var LoginAndPassword = SignToWebsite.GetLoginAndPassword();
            var Response = await SignToWebsite.SignIn(Token, LoginAndPassword, httpClient);
            return await CheckIfLoggedIn(httpClient);
        }
        public static async Task<bool> CheckIfLoggedIn(HttpClient httpClient)
        {
            var Html = await httpClient.GetStringAsync("https://upel2.cel.agh.edu.pl/wfiis/");
            if (Html.Contains("Dostępne kursy"))
            {
                Console.WriteLine();
                Console.WriteLine("You are logged in");
                return true;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Wrong login or password, try again");
                return false;
            }

        }
    }
}
