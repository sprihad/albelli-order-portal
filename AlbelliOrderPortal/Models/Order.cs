using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlbelliOrderPortal.Models
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 OrderId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
