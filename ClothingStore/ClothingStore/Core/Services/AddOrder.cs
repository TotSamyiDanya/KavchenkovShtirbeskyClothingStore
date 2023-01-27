namespace ClothingStore.Core.Services
{
    public class AddOrder
    {
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDestination { get; set; }
        public int OrderPrice { get; set; }
        public string OrderClothes { get; set; }
    }
}
