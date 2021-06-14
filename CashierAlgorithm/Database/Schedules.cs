using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierAlgorithm.Database
{
    public class Schedules
    {
        [Key]
        public int Id { get; set; }
        public int CoordinatorId { get; set; }
        public DateTime ScheduleMonth { get; set; }
        [ForeignKey("CoordinatorId")]
        public Workers Worker { get; set; }
        public string Name { get; set; }
        public string ScheduleInJSON { get; set; }
    }
}
