namespace ClothingStore.Model
{
    public class ClothQuantity
    {
        public int ClothQuantityId { get; set; }
        public virtual Cloth Cloth { get; set; }
        public string ClothSize { get; set; }
        public int ClothSizeQuantity { get; set; }
    }
}
