using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Pages
{
    public class TimeList
    {
        public string TimeSet { get; set; }

        public TimeList(string timeSet)
        {
            TimeSet = timeSet;
        }
    }
}
