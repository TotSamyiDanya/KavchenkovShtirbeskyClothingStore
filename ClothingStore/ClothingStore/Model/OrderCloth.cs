using Newtonsoft.Json;

namespace ClothingStore.Model
{
    public class OrderCloth
    {
        public int OrderClothId { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
        public virtual Cloth Cloth { get; set; }
        public string ClothSize { get; set; }
        public int OrderClothQuantity { get; set; }
    }
}
