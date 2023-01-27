using Newtonsoft.Json;

namespace ClothingStore.Model
{
    public class Order
    {
        public int OrderID { get; set; }
        public double OrderPrice { get; set; }
        public string OrderCustomerName { get; set; }
        public string OrderCustomerEmail { get; set; }
        public string OrderDestination { get; set; }
        public virtual ICollection<OrderCloth> Clothes { get; set; }
        [JsonIgnore]
        public virtual Store OrderStore { get; set; }
    }
}
