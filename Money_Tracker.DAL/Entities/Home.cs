using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Entities
{
    public class Home
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string Name_Home { get; set; } = string.Empty;
    }
}
