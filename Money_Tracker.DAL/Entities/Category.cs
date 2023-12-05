using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Category_Name { get; set; } = string.Empty;
    }
}
