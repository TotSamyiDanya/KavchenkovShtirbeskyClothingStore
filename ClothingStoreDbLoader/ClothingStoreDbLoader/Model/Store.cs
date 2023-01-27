using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreDbLoader.Model
{
    public class Store
    {
        public int StoreId { get; set; }
        public string StoreLocation { get; set; }
        public virtual ICollection<ClothQuantity> Clothes { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
