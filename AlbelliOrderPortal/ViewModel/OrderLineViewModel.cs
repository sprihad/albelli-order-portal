using AlbelliOrderPortal.DTO;
using System.Collections.Generic;

namespace AlbelliOrderPortal.ViewModel
{
    public class OrderLineViewModel
    {
        public List<OrderDTO> Orders { get; set; }

        public string RequiredBinWidth { get; set; }
    }
}
