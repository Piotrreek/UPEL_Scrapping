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
    class GetPpoGrades
    {
        public static int Choose()
        {
            Console.WriteLine(@$"{Environment.NewLine}What do you want to do on the website ? {Environment.NewLine}1. Get grades and average grade(%) from Basics of Object Oriented Programming {Environment.NewLine}2. ... { Environment.NewLine}5. Exit");
            Console.Write("What is your choice ?: ");
            return int.Parse(Console.ReadLine());
        }
        public static async Task PpoGrades(HttpClient httpClient)
        {
            var PpoCourse = await httpClient.GetAsync("https://upel2.cel.agh.edu.pl/wfiis/course/user.php?mode=grade&id=464&user=2451");
            var PpoCourseStr = await PpoCourse.Content.ReadAsStringAsync();

            PpoCourseStr = WebUtility.HtmlDecode(PpoCourseStr);
            HtmlDocument PpoCourseHtml = new HtmlDocument();
            PpoCourseHtml.LoadHtml(PpoCourseStr);

            List<LabGrade> LabGradeList = new List<LabGrade>();
            var AllTr = PpoCourseHtml.QuerySelectorAll("tr");
            Console.WriteLine();
            foreach (var tr in AllTr)
            {
                AddToList(LabGradeList, tr);
            }

            foreach (var LabGrade in LabGradeList)
            {
                Console.WriteLine($"Name of lab: {LabGrade.LabName}, Points: {LabGrade.Points}, Grade: {LabGrade.Grade}");
            }
            LabGrade FinalGrades = CalculateFinalGrade(AllTr);
            Console.Write($"{Environment.NewLine}Sum of points : {FinalGrades.SumPoints}, Final grade: {FinalGrades.FinalGrade}{Environment.NewLine}");
        }
        public static LabGrade CalculateFinalGrade(IList<HtmlNode> allTr)
        {
            LabGrade FinalGrades = new LabGrade();
            var SumOfPoints = allTr[allTr.Count - 1].QuerySelectorAll("td")[1].InnerText.Split("(")[1];
            SumOfPoints = SumOfPoints.Remove(SumOfPoints.Length - 1).Replace(",", ".");
            var FinalGrade = allTr[allTr.Count - 1].QuerySelectorAll("td")[4].InnerText;
            FinalGrades.FinalGrade = FinalGrade;
            FinalGrades.SumPoints = SumOfPoints;
            return FinalGrades;
        }
        public static void AddToList(List<LabGrade> LabGradeList, HtmlNode tr)
        {
            if (tr.InnerText.Contains("Gr. 4 - ") && tr.QuerySelectorAll("td")[1].InnerText != "")
            {
                var SearchTh = tr.QuerySelector("th");
                var SearchTd = tr.QuerySelectorAll("td");
                var PointsTemp = SearchTd[1].InnerText;
                var PointsTemp_2 = PointsTemp.Split("(");
                var LabName = SearchTh.InnerText;
                var Points = PointsTemp_2[1].Remove(PointsTemp_2[1].Length - 1).Replace(",", ".");
                var Grade = SearchTd[4].InnerText;
                LabGradeList.Add(new LabGrade(LabName, Grade, Points));
            }
        }
    }
}
