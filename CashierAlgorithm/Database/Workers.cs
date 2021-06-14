using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierAlgorithm.Database
{
    public class Workers
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SpecialInfo { get; set; }
        public int DepartmentId { get; set; }
        public int Role { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Departments Department { get; set; }
        public virtual ICollection<Leaves> Leaves { get; set; }

    }
}
