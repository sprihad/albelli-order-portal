using AlbelliOrderPortal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbelliOrderPortal.Repository
{
    public class OrderRepositoryImplementation : IOrderRepository
    {
        private readonly OrderDbContext _dbcontext;

        public OrderRepositoryImplementation(OrderDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Int64> Create(List<OrderLine> orderLines)
        {
            using (var transaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {
                    _dbcontext.OrderLine.AddRange(orderLines);
                    await _dbcontext.SaveChangesAsync();

                    transaction.Commit();
                    return 0;
                }
                catch (Exception)
                {
                    //log exception message when logging enabled.

                    transaction.Rollback();
                    return -2;
                }
            }
        }
        public IQueryable<OrderLine> GetOrderById(Int64 OrderId)
        {
            if (!_dbcontext.Order.Any(O => O.OrderId == OrderId))
            {
                return null;
            }
            else
            {
                var allOrderItems = _dbcontext.OrderLine.Include(o => o.Order).Where(i => i.Order.OrderId == OrderId);
                return allOrderItems;
            }
        }

        public void AddOrder(Order newOrder)
        {
            try
            {
                _dbcontext.Order.Add(newOrder);
                _dbcontext.SaveChanges();
            }
            catch (Exception)
            {
                //log exception message when logging enabled.
            }
        }

        public long GetLastOrderId()
        {
            return _dbcontext.Order.OrderBy(o => o.OrderId).ToList().LastOrDefault().OrderId;
        }
    }
}
