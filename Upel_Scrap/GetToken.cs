using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;


namespace Upel_Scrap
{
    class GetToken
    {
        public static string GetSecurityToken(string document)
        {
            List<string> list = document.Split(">").ToList();
            var str = list.FirstOrDefault(s => s.Contains("name=\"logintoken\""));
            str = str.Trim();
            str = str.Substring(str.Length - 33);
            str = str.Remove(str.Length - 1);
            return str;
        }
    }
}
