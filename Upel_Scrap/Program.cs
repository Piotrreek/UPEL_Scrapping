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
            while (!await SignToWebsite.SignInFun(httpClient)) {}
            int choice = Welcome.Choose();
            do
            {
                switch (choice)
                {
                    case 1:            
                        await GetPpoGrades.PpoGrades(httpClient);
                        break;
                    case 2:
                        List<string> URLs = await TOiSDownloadPDF.GetURLs(httpClient);
                        await TOiSDownloadPDF.DownloadPdf(httpClient, URLs);
                        break;
                    case 5:
                        Console.WriteLine("Logging out ...");
                        await LogOut.LogOutFromWebsite(httpClient);
                        return;
                    default:
                        Console.WriteLine("Bad input, try again");
                        break;
                }
                choice = Welcome.Choose();
            } while (true);
        }
    }
}

