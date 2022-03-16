using AlbelliOrderPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbelliOrderPortal.Repository
{
    public interface IOrderRepository
    {
        Task<Int64> Create(List<OrderLine> orderLines);
        IQueryable<OrderLine> GetOrderById(Int64 OrderId);
        void AddOrder(Order newOrder);
        long GetLastOrderId();
    }
}
