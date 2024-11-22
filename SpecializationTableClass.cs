using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika321App
{
    public class SpecializationTableClass
    {
        public string Number { get; set; }
        public string Name_Specialization { get; set; }
        public string Code { get; set; }

        public override string ToString()
        {
            return $"Number: {Number}, Name_Specialization: {Name_Specialization}, Code: {Code}";
        }
    }
}
