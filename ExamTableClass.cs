using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika321App
{
    public class ExamTableClass
    {
        public DateTime? Date { get; set; }
        public int? Disciple { get; set; }
        public int? Reg_Number { get; set; }
        public int? Teacher { get; set; }
        public string Room { get; set; }
        public int? Grade { get; set; }
        public int ID_Exam { get; set; }

        public override string ToString()
        {
            return $"ID_Exam: {ID_Exam}, Date: {Date?.ToString("yyyy-MM-dd") ?? "N/A"}, Disciple: {Disciple ?? 0}, Reg_Number: {Reg_Number ?? 0}, Teacher: {Teacher ?? 0}, Room: {Room}, Grade: {Grade ?? 0}";
        }
    }
}
