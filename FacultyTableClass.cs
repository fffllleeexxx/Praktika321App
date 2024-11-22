using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika321App
{
    public class FacultyTableClass
    {
        public string abbreviation { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"abbreviation: {abbreviation}, Name: {Name}";
        }
    }
}
