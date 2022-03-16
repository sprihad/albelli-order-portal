using AlbelliOrderPortal.Helpers;
using AlbelliOrderPortal.Models;
using AlbelliOrderPortal.ViewModel;
using System;
using System.Collections.Generic;
using Moq;
using AlbelliOrderPortal.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Assert = Xunit.Assert;
using System.Linq;
using AlbelliOrderPortal.DTO;
using AlbelliOrderPortal.Service;

namespace AlbelliOrderPortalTest
{
    public class OrderControllerTest
    {
        private readonly Mock<IOrderService> _mockOrderService;

        public OrderControllerTest()
        {
            _mockOrderService = new Mock<IOrderService>();
        }

        [Fact]
        public void GetOrderByID_GetOrderDetails_WhenOrderIDExists()
        {
            //Arrange
            var allOrders = GetOrderDetails(1);
            _mockOrderService.Setup(r => r.GetOrderById(1)).Returns(allOrders);
            var controller = new OrderController(_mockOrderService.Object);

            //Act
            IActionResult actionResult = controller.GetOrderById(1);
            var returnedContent = actionResult as Microsoft.AspNetCore.Mvc.OkObjectResult;

            //Assert
            Assert.NotNull(returnedContent);
            Assert.Equal(allOrders, returnedContent.Value);
        }

        [Fact]
        public void GetOrderByID_ReturnsNotFound_WhenOrderIDDoesNotExist()
        {
            //Arrange
            var controller = new OrderController(_mockOrderService.Object);

            //Act
            IActionResult actionResult = controller.GetOrderById(4);

            //Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundObjectResult>(actionResult);
        }

        [Fact]
        public void CreateOrder_CreatedAtLocation_PassingOrderItem()
        {
            //Arrange
            var productType = (ProductType)2;
            var quantity = 3;

            var createOrderModel = new OrderViewModel[] { new OrderViewModel { ProductType = productType, Quantity = quantity } };
            var controller = new OrderController(_mockOrderService.Object);

            //Act
            var actionResult = controller.PostOrder(createOrderModel);
            var response = actionResult.Result;

            //Assert
            Assert.IsType<CreatedAtActionResult>(response);
        }

        private OrderLineViewModel GetOrderDetails(Int64 OrderID)
        {
            List<OrderLine> allOrderItems = new List<OrderLine>
            {
                new OrderLine{Id = 1, ProductType = "PhotoBook", Quantity = 2, RequiredBinWidth = 38, OrderId = 1},

                new OrderLine{Id = 2, ProductType = "Cards", Quantity = 2, RequiredBinWidth = 9.4, OrderId = 1},

                new OrderLine{Id = 3, ProductType = "Calendar", Quantity = 5, RequiredBinWidth = 50, OrderId = 2}
            };

            List<OrderDTO> orderDetails = new List<OrderDTO>();

            foreach (OrderLine item in allOrderItems.Where(i => i.OrderId == OrderID))
            {
                orderDetails.Add(new OrderDTO { ProductType = item.ProductType, Quantity = item.Quantity });
            }

            var requiredBinWidth = allOrderItems.Where(i => i.OrderId == OrderID).Sum(b => b.RequiredBinWidth) + "mm";

            return allOrderItems.Select(a => new OrderLineViewModel { Orders = orderDetails, RequiredBinWidth = requiredBinWidth }).FirstOrDefault();


        }
    }
}
