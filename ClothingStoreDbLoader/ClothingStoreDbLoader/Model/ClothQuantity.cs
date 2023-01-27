using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreDbLoader.Model
{
    public class ClothQuantity
    {
        public int ClothQuantityId { get; set; }
        public virtual Cloth Cloth { get; set; }
        public string ClothSize { get; set; }
        public int ClothSizeQuantity { get; set; }
    }
}
