using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIntranet.Models;

namespace MyIntranet.Utilities
{
    public class Hr
    {
        public static double CountDays(List<MyIntranet.Models.LeaveRequest> days)
        {
            var count = days.Sum(x => x.Days);

            return (double)count;
        }
    }
}