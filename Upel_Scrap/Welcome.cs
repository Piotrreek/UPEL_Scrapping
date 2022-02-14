using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upel_Scrap
{
    class Welcome
    {
        public static int Choose()
        {
            Console.WriteLine(@$"{Environment.NewLine}What do you want to do on the website ? {Environment.NewLine}1. Get grades and average grade(%) from Basics of Object Oriented Programming {Environment.NewLine}2. Download Lectures in PDF format from TOiS course{ Environment.NewLine}5. Exit");
            Console.Write("What is your choice ?: ");
            return int.Parse(Console.ReadLine());
        }
    }
}
