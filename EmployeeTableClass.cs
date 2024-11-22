using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika321App
{
    public class EmployeeTableClass
    {
        public int Tab_Number { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public decimal? Salary { get; set; }
        public int? Chief { get; set; }

        public override string ToString()
        {
            return $"Tab_Number: {Tab_Number}, Code: {Code}, FullName: {FullName}, Position: {Position}, Salary: {Salary ?? 0}, Chief: {Chief ?? 0}";
        }
    }
}
