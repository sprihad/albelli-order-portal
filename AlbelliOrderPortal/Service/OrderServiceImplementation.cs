using AlbelliOrderPortal.DTO;
using AlbelliOrderPortal.Helpers;
using AlbelliOrderPortal.Models;
using AlbelliOrderPortal.Repository;
using AlbelliOrderPortal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbelliOrderPortal.Service
{
    public class OrderServiceImplementation : IOrderService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderServiceImplementation(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public OrderLineViewModel GetOrderById(Int64 OrderId)
        {
            var allOrderItems = _orderRepo.GetOrderById(OrderId);
            List<OrderDTO> orderDetails = new List<OrderDTO>();

            foreach (OrderLine item in allOrderItems)
            {
                orderDetails.Add(new OrderDTO { ProductType = item.ProductType, Quantity = item.Quantity });
            }

            var requiredBinWidth = allOrderItems.Sum(b => b.RequiredBinWidth) + "mm";
            return allOrderItems.Select(a => new OrderLineViewModel { Orders = orderDetails, RequiredBinWidth = requiredBinWidth }).FirstOrDefault();
        }

        public async Task<Int64> CreateOrder(OrderViewModel[] order)
        {
            if (order.Any(o => !Enum.IsDefined(typeof(ProductType), o.ProductType)) || order.Any(o => o.Quantity <= 0))
                return -1;

            Order newOrder = new Order();
            newOrder.OrderDate = DateTime.Now;
            _orderRepo.AddOrder(newOrder);
            long lastOrderId = _orderRepo.GetLastOrderId();
            List<OrderLine> orderLineList = new List<OrderLine>();
            foreach (var item in order)
            {
                ProductType pType = (ProductType)item.ProductType;
                OrderLine newitem = new OrderLine();
                newitem.ProductType = pType.ToString();
                newitem.RequiredBinWidth = BinWidthCompuation.ComputeBinWidth(item.ProductType, item.Quantity);
                newitem.OrderId = lastOrderId;
                newitem.Quantity = item.Quantity;
                orderLineList.Add(newitem);
            }
            await _orderRepo.Create(orderLineList);
            return lastOrderId;
        }
    }
}
