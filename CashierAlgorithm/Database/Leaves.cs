using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierAlgorithm.Database
{
    public class Leaves
    {
        [Key]
        public int Id { get; set; }
        public DateTime LeaveStart { get; set; }
        public DateTime LeaveEnd { get; set; }
        public string LeaveName { get; set; }
        public int WorkerId { get; set; }
        [ForeignKey("WorkerId")]
        public Workers Worker { get; set; }
    }
}
