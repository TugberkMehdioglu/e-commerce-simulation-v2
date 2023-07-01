using Newtonsoft.Json;

namespace Project.MVCUI.Models.ShoppingTools
{
    [Serializable]
    public class CartItem
    {
        [JsonProperty]
        public int ID { get; set; }

        [JsonProperty]
        public string Name { get; set; } = null!;

        [JsonProperty]
        public short Amount { get; set; }

        [JsonProperty]
        public decimal Price { get; set; }

        [JsonProperty]
        public string? ImagePath { get; set; }

        [JsonProperty]
        public decimal SubTotal
        {
            get
            {
                return Price * Amount;
            }
        }

        public CartItem()
        {
            ++Amount;//For starts with 1
        }
    }
}
