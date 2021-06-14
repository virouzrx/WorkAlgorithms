using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierAlgorithm.Database
{
    public class ShiftInfo
    {
        public DateTime ShiftBegin { get; set; }
        public DateTime ShiftEnd { get; set; }

        public ShiftInfo(DateTime ShiftBegin, DateTime ShiftEnd)
        {
            this.ShiftBegin = ShiftBegin;
            this.ShiftEnd = ShiftEnd;
        }
    }
}
