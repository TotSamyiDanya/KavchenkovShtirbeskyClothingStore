using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreDbLoader.Model
{
    public class OrderCloth
    {
        public int OrderClothId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Cloth Cloth { get; set; }
        public string ClothSize { get; set; }
        public int OrderClothQuantity { get; set; }
    }
}
