using System;
using System.Threading.Tasks;
using AlbelliOrderPortal.Service;
using AlbelliOrderPortal.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlbelliOrderPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
       private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet(template: "{OrderID}", Name = "OrderID")]
        public IActionResult GetOrderById(Int64 OrderID)
        {
            if (OrderID < 0)
                return BadRequest(new { message = "Invalid Order" });

            var orderReponse = _orderService.GetOrderById(OrderID);

            if (orderReponse == null)
                return NotFound(new { message = "Order does not exist" });
            return Ok(orderReponse);
        }

        [HttpPost]
        [Route("PlaceOrder")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<IActionResult> PostOrder(OrderViewModel[] order)
        {
            if (ModelState.IsValid)
            {
                var orderId = await _orderService.CreateOrder(order);
                if (orderId == -1)
                    return UnprocessableEntity(new { message = "Please choose Product type between 0-4 and Quantity cannnot be 0." });

                if (orderId == -2)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");

                return CreatedAtAction(nameof(GetOrderById), new { OrderID = orderId }, order);
            }
            return BadRequest();
        }
    }
}
