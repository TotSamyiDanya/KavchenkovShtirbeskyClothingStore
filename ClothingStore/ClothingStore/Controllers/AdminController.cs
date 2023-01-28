using ClothingStore.Core.Services;
using ClothingStore.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ClothingStore.Model;

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
            using ClothingStoreDbContext db = new();
            var clothDb = db.Clothes.Where(c => c.ClothName == cloth.ClothName && c.ClothBrand == cloth.ClothBrand && c.ClothCategory == cloth.ClothCategory && c.ClothGender == cloth.ClothGender).FirstOrDefault();
            if (clothDb != null)
            {

            }
            else
            {
                var store = db.Stores.Where(s => s.StoreLocation == cloth.ClothStore).FirstOrDefault();

                Cloth clothNew = new();
                clothNew.ClothName = cloth.ClothName;
                clothNew.ClothBrand = cloth.ClothBrand;
                clothNew.ClothGender = cloth.ClothGender;
                clothNew.ClothPrice = cloth.ClothPrice;
                clothNew.ClothSale = cloth.ClothSale;
                clothNew.ClothCategory = cloth.ClothCategory;
                clothNew.ClothSaleEnding = DateTime.Parse(cloth.ClothSaleEnd);
                clothNew.ClothImage = "";

                ClothQuantity cqM = new(); ClothQuantity cqL = new(); ClothQuantity cqXL = new(); ClothQuantity cqXXL = new();
                cqM.ClothSize = "M"; cqM.ClothSizeQuantity = cloth.ClothSizeM; cqM.Cloth = clothNew;
                cqL.ClothSize = "L"; cqL.ClothSizeQuantity = cloth.ClothSizeL; cqL.Cloth = clothNew;
                cqXL.ClothSize = "XL"; cqXL.ClothSizeQuantity = cloth.ClothSizeXL; cqXL.Cloth = clothNew;
                cqXXL.ClothSize = "XXL"; cqXXL.ClothSizeQuantity = cloth.ClothSizeXXL; cqXXL.Cloth = clothNew;

                store.Clothes.Add(cqM); store.Clothes.Add(cqL); store.Clothes.Add(cqXL); store.Clothes.Add(cqXXL);

                db.Update(store);
                db.SaveChanges();

                var clothAdded = db.Clothes.OrderBy(c => c.ClothId).Last();

                string dataUri = cloth.ClothImage;
                string base64str = dataUri.Substring(dataUri.IndexOf(',') + 1);
                byte[] bytes = Convert.FromBase64String(base64str);
                System.IO.File.WriteAllBytes($"Content\\Images\\{clothAdded.ClothId}.jpg", bytes);

                clothAdded.ClothImage = $"Content\\Images\\{clothAdded.ClothId}.jpg";

                db.Update(clothAdded);
                db.SaveChanges();
            }
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
