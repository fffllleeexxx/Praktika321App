using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika321App
{
    public class TeacherTableClass
    {
        public int Tab_Number { get; set; }
        public string Rank { get; set; }
        public string Degree { get; set; }

        public override string ToString()
        {
            return $"Tab_Number: {Tab_Number}, Rank: {Rank}, Degree: {Degree}";
        }
    }
}
