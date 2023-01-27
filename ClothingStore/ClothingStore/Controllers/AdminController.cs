using ClothingStore.Core.Services;
using ClothingStore.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClothingStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpPost]
        [Route("AddCloth")]
        public IActionResult AddCloth(AddCloth cloth)
        {
            string dataUri = cloth.ClothImage;
            string base64str = dataUri.Substring(dataUri.IndexOf(',') + 1);
            byte[] bytes = Convert.FromBase64String(base64str);
            System.IO.File.WriteAllBytes("chupepa.png", bytes);
            return Ok();
        }
        [HttpPost]
        [Route("UpdateCloth")]
        public IActionResult UpdateCloth(UpdateCloth cloth)
        {
            using ClothingStoreDbContext db = new();
            var clothDb = db.Clothes.Where(c => c.ClothName == cloth.ClothName && c.ClothBrand == cloth.ClothBrand && c.ClothCategory == cloth.ClothCategory && c.ClothGender == cloth.ClothGender).FirstOrDefault();
            if (clothDb == null)
            {

            }
            else
            {
                var store = db.Stores.Where(s => s.StoreLocation == cloth.ClothStore).FirstOrDefault();
                var sizes = store.Clothes.Where(cq => cq.Cloth.ClothName == cloth.ClothName && cq.Cloth.ClothBrand == cloth.ClothBrand && cq.Cloth.ClothCategory == cloth.ClothCategory && cq.Cloth.ClothGender == cloth.ClothGender).ToList();
                foreach(var size in sizes)
                {
                    if (size.ClothSize == "M")
                        size.ClothSizeQuantity = cloth.ClothSizeM;
                    if (size.ClothSize == "L")
                        size.ClothSizeQuantity = cloth.ClothSizeL;
                    if (size.ClothSize == "XL")
                        size.ClothSizeQuantity = cloth.ClothSizeXL;
                    if (size.ClothSize == "XXL")
                        size.ClothSizeQuantity = cloth.ClothSizeXXL;
                }
                db.Update(store);
                db.SaveChanges();
            }
            return Ok();
        }
        [HttpGet]
        [Route("GetClothes")]
        public IActionResult GetClothesInStore(string location)
        {
            using ClothingStoreDbContext db = new();
            var store = db.Stores.Where(s => s.StoreLocation == location).FirstOrDefault();
            var clothes = store.Clothes.GroupBy(c => c.Cloth).ToList();
            string json = JsonConvert.SerializeObject(clothes);
            if (!string.IsNullOrEmpty(json))
                return Ok(json);
            else
                return NotFound();
        }
        [HttpGet]
        [Route("GetOrders")]
        public IActionResult GetOrdersInStore(string location)
        {
            using ClothingStoreDbContext db = new();
            var store = db.Stores.Where(s => s.StoreLocation == location).FirstOrDefault();
            var orders = store.Orders.ToList();
            string json = JsonConvert.SerializeObject(orders);
            if (!string.IsNullOrEmpty(json))
                return Ok(json);
            else
                return NotFound();
        }
    }
}
