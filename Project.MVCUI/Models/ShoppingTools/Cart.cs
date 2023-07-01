using Newtonsoft.Json;

namespace Project.MVCUI.Models.ShoppingTools
{
    [Serializable]
    public class Cart
    {
        [JsonProperty]
        private readonly Dictionary<int, CartItem> _basket;

        public Cart()
        {
            _basket = new Dictionary<int, CartItem>();
        }

        [JsonProperty]
        public ICollection<CartItem> Basket { get { return _basket.Values; } }

        [JsonProperty]
        public decimal TotalPrice { get { return _basket.Sum(x => x.Value.SubTotal); } }

        [JsonProperty]
        public int TotalItemCount => _basket.Sum(x => x.Value.Amount);

        public void AddToBasket(CartItem item)
        {
            if (_basket.ContainsKey(item.ID))
            {
                _basket[item.ID].Amount += 1;
                return;
            }

            _basket.Add(item.ID, item);
        }

        public void RemoveFromBasket(int id)
        {
            if (_basket[id].Amount > 1)
            {
                _basket[id].Amount -= 1;
                return;
            }

            _basket.Remove(id);
        }

        public void RemoveItemWithAllAmountFromBasket(int id)
        {
            _basket.Remove(id);
        }
    }
}
