using AlbelliOrderPortal.ViewModel;
using System;
using System.Threading.Tasks;

namespace AlbelliOrderPortal.Service
{
    public interface IOrderService
    {
        OrderLineViewModel GetOrderById(Int64 OrderId);
        Task<Int64> CreateOrder(OrderViewModel[] order);
    }
}
