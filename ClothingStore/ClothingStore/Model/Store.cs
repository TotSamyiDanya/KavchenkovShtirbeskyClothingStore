namespace ClothingStore.Model
{
    public class Store
    {
        public int StoreId { get; set; }
        public string StoreLocation { get; set; }
        public virtual ICollection<ClothQuantity> Clothes { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
