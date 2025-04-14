using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.DAL.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Column(TypeName ="Date")]
        public DateTime date { get; set; }

    }
}
