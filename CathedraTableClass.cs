using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika321App
{
    public class CathedraTableClass
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Faculty { get; set; }

        public override string ToString()
        {
            return $"Code: {Code}, Name: {Name}, Faculty: {Faculty}";
        }
    }
}
