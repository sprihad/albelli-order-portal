using AlbelliOrderPortal.Helpers;
using System.ComponentModel.DataAnnotations;

namespace AlbelliOrderPortal.ViewModel
{
    public class OrderViewModel
    {
        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
