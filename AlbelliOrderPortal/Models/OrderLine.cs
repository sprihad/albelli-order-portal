using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlbelliOrderPortal.Models
{
    public class OrderLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [Required]
        public Int64 OrderId { get; set; }

        [StringLength(30)]
        [Required]
        public string ProductType { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double RequiredBinWidth { get; set; }
        public Order Order { get; set; }
    }
}
