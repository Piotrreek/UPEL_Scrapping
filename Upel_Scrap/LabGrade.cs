using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upel_Scrap
{
    class LabGrade
    {
        public LabGrade(string labname, string grade, string points)
        {
            LabName = labname;
            Grade = grade;
            Points = points;
        }
        public LabGrade() { }

        public string LabName { get; set; }
        public string Grade { get; set; }
        public string Points { get; set; }
        public string SumPoints { get; set; }
        public string FinalGrade { get; set; }
    }
}
