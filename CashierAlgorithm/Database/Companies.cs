using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashierAlgorithm.Database
{
    public class Companies
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public virtual ICollection<Departments> Departments {get; set;}
    }
}
