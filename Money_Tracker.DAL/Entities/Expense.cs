using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public int Category_Id { get; set; }
        public int User_Id { get; set; }
        public int Home_Id { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date_Expense { get; set; }

    }
}
