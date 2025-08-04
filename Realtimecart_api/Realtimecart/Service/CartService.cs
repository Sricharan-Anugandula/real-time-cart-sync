using Realtimecart.Models;

namespace Realtimecart.Service
{
    public class CartService
    {
        private static readonly Dictionary<string, List<CartItem>> _cart = new();

        public List<CartItem> GetCart(string userId)
        {
            _cart.TryGetValue(userId, out var items);
            return items ?? new List<CartItem>();
        }

        public void AddToCart(CartItem item)
        {
            if (!_cart.ContainsKey(item.UserId))
                _cart[item.UserId] = new List<CartItem>();

            _cart[item.UserId].Add(item);
        }
    }
}
