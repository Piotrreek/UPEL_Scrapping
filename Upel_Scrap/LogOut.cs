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
    class LogOut
    {
        public static async Task<string> FindLogOutSessionKey(HttpClient httpClient)
        {
            var http = await httpClient.GetAsync("https://upel2.cel.agh.edu.pl/wfiis/");
            var httpStr = await http.Content.ReadAsStringAsync();

            httpStr = WebUtility.HtmlDecode(httpStr);
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(httpStr);

            var SessionKeyNode = html.QuerySelectorAll("a").Where(a => a.GetAttributeValue("data-title", null) == "logout,moodle");
            string sessionKey = null;
            foreach(var sessionKeyNode in SessionKeyNode)
            {
                sessionKey = sessionKeyNode.GetAttributeValue("href", null);
            }
            return sessionKey;
        }
        public static async Task LogOutFromWebsite(HttpClient httpClient)
        {
            var sessionKey = await FindLogOutSessionKey(httpClient);
            await httpClient.GetAsync(sessionKey);
        }
    }
}
