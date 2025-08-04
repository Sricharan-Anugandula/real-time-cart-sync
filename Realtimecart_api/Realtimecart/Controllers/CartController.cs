using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Realtimecart.Hubs;
using Realtimecart.Models;
using Realtimecart.Service;

namespace Realtimecart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _service;
        private readonly IHubContext<CartHub> _hub;

        public CartController(CartService service, IHubContext<CartHub> hub)
        {
            _service = service;
            _hub = hub;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartItem item)
        {
            _service.AddToCart(item);
            var updatedCart = _service.GetCart(item.UserId);

            await _hub.Clients.Group(item.UserId).SendAsync("CartUpdated", updatedCart);
            return Ok(updatedCart);
        }

        [HttpGet("{userId}")]
        public IActionResult GetCart(string userId)
        {
            var cart = _service.GetCart(userId);
            return Ok(cart);
        }
    }
}
