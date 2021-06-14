using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierAlgorithm
{
    public interface ISchedule
    {
        public List<DateTime> GetWorkingDays();
        public string Generate(int coordinatorId, DateTime date);

    }
}
