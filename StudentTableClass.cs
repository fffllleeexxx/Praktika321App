using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika321App
{
    public class StudentTableClass
    {
        public int Reg_Number { get; set; }
        public string Number { get; set; }
        public string Fullname { get; set; }

        public override string ToString()
        {
            return $"Reg_Number: {Reg_Number}, Fullname: {Fullname}, Number: {Number}";
        }
    }
}
