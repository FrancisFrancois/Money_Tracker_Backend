using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.BLL.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string? Message) : base(Message)
        {

        }
    }
}
