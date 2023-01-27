using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreDbLoader.Model
{
    public class Cloth
    {
        public int ClothId { get; set; }
        public string ClothName { get; set; }
        public string ClothBrand { get; set; }
        public string ClothCategory { get; set; }
        public string ClothGender { get; set; }
        public double ClothPrice { get; set; }
        public double? ClothSale { get; set; }
        public DateTime? ClothSaleEnding { get; set; }
        public string ClothImage { get; set; }
    }
}
