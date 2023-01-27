using ClothingStore.Core.Services;
using ClothingStore.Core;
using ClothingStore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ClothingStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        [Route("WriteReport")]
        public IActionResult WriteReport()
        {
            return Ok();
        }
        [HttpGet]
        [Route("SameClothes")]
        public IActionResult GetGroupClothes(string location, string category, string gender)
        {
            ClothingStoreDbContext db = new(); Random random = new();
            var store = db.Stores.Where(s => s.StoreLocation == location).FirstOrDefault();
            var clothes = store.Clothes.Where(c => c.Cloth.ClothCategory == category && c.Cloth.ClothGender == gender).Skip(random.Next(0, 5) * 8).Take(32).GroupBy(c => c.Cloth).Where(c => c.Sum(s => s.ClothSizeQuantity) > 0).ToList();

            List<Cloth> clothesJson = new();
            foreach (var cq in clothes)
            {
                clothesJson.Add(cq.Key);
            }

            string json = JsonConvert.SerializeObject(clothesJson);
            if (!string.IsNullOrEmpty(json))
                return Ok(json);
            else
                return NotFound();
        }
        [HttpGet]
        [Route("New")]
        public IActionResult GetNewClothes(string location)
        {
            ClothingStoreDbContext db = new(); Random random = new();
            var store = db.Stores.Where(s => s.StoreLocation == location).FirstOrDefault();
            var clothes = store.Clothes.Skip(4 * random.Next(0, 67)).Take(32).GroupBy(c => c.Cloth).Where(c => c.Sum(s => s.ClothSizeQuantity) > 0).ToList();

            List<Cloth> clothesJson = new();
            foreach (var cq in clothes)
            {
                clothesJson.Add(cq.Key);
            }

            string json = JsonConvert.SerializeObject(clothesJson);
            if (!string.IsNullOrEmpty(json))
                return Ok(json);
            else
                return NotFound();
        }
        [HttpGet]
        [Route("Popular")]
        public IActionResult GetPopularClothes(string location)
        {
            ClothingStoreDbContext db = new(); Random random = new();
            var store = db.Stores.Where(s => s.StoreLocation == location).FirstOrDefault();
            var clothes = store.Clothes.Skip(4 * random.Next(307, 377)).Take(32).GroupBy(c => c.Cloth).Where(c => c.Sum(s => s.ClothSizeQuantity) > 0).ToList();

            List<Cloth> clothesJson = new();
            foreach (var cq in clothes)
            {
                clothesJson.Add(cq.Key);
            }

            string json = JsonConvert.SerializeObject(clothesJson);
            if (!string.IsNullOrEmpty(json))
                return Ok(json);
            else
                return NotFound();
        }
        [HttpGet]
        [Route("GenderAssortment")]
        public IActionResult GetGenderAssortment(string location, string gender, int page)
        {
            ClothingStoreDbContext db = new();
            var store = db.Stores.Where(s => s.StoreLocation == location).FirstOrDefault();
            var clothes = store.Clothes.Where(c => c.Cloth.ClothGender.Contains(gender)).GroupBy(c => c.Cloth).Where(c => c.Sum(s => s.ClothSizeQuantity) > 0).ToList();

            List<Cloth> clothesJson = new();
            foreach (var cq in clothes)
            {
                clothesJson.Add(cq.Key);
            }

            string json = JsonConvert.SerializeObject(clothesJson);
            if (!string.IsNullOrEmpty(json))
                return Ok(json);
            else
                return NotFound();
        }
        [HttpGet]
        [Route("GenderCategoryAssortment")]
        public IActionResult GetGenderCategoryAssortment(string location, string gender, string category, int page)
        {
            ClothingStoreDbContext db = new();
            var store = db.Stores.Where(s => s.StoreLocation == location).FirstOrDefault();
            var clothes = store.Clothes.Where(c => c.Cloth.ClothGender.Contains(gender) && c.Cloth.ClothCategory.Contains(category)).GroupBy(c => c.Cloth).Where(c => c.Sum(s => s.ClothSizeQuantity) > 0).ToList();

            List<Cloth> clothesJson = new();
            foreach (var cq in clothes)
            {
                clothesJson.Add(cq.Key);
            }

            string json = JsonConvert.SerializeObject(clothesJson);
            if (!string.IsNullOrEmpty(json))
                return Ok(json);
            else
                return NotFound();
        }
        [HttpGet]
        [Route("CategoryAssortment")]
        public IActionResult GetCategoryAssortment(string location, string category)
        {
            ClothingStoreDbContext db = new();
            var store = db.Stores.Where(s => s.StoreLocation == location).FirstOrDefault();
            var clothes = store.Clothes.Where(c => c.Cloth.ClothCategory.Contains(category)).GroupBy(c => c.Cloth).Where(c => c.Sum(s => s.ClothSizeQuantity) > 0).ToList();

            List<Cloth> clothesJson = new();
            foreach (var cq in clothes)
            {
                clothesJson.Add(cq.Key);
            }

            string json = JsonConvert.SerializeObject(clothesJson);
            if (!string.IsNullOrEmpty(json))
                return Ok(json);
            else
                return NotFound();
        }
        [HttpGet]
        [Route("Card")]
        public IActionResult GetCloth(string location, int clothid)
        {
            ClothingStoreDbContext db = new();
            var store = db.Stores.Where(s => s.StoreLocation == location).FirstOrDefault();
            var clothSizes = store.Clothes.Where(c => c.Cloth.ClothId == clothid).ToList();
            var cloth = db.Clothes.Where(c => c.ClothId == clothid);

            List<object> clothesJson = new();
            clothesJson.Add(cloth);
            clothesJson.Add(clothSizes);

            string json = JsonConvert.SerializeObject(clothesJson);
            if (!string.IsNullOrEmpty(json))
                return Ok(json);
            else
                return NotFound();
        }
        [HttpPost]
        [Route("Order")]
        public IActionResult SendOrder(AddOrder addOrder)
        {
            using ClothingStoreDbContext db = new();

            Store store = db.Stores.Where(s => s.StoreLocation == "Смоленск").FirstOrDefault();

            Order order = new();
            order.OrderPrice = addOrder.OrderPrice;
            order.OrderCustomerName = addOrder.CustomerName;
            order.OrderCustomerEmail = addOrder.CustomerEmail;
            order.OrderDestination = addOrder.CustomerDestination;

            JArray clothes = JArray.Parse(addOrder.OrderClothes);

            List<OrderCloth> orderClothes = new();
            for (int i = 0; i < clothes.Count; i++)
            {
                Cloth cloth = db.Clothes.Where(c => c.ClothId == clothes[i]["ClothId"].Value<int>()).FirstOrDefault();

                OrderCloth co = new OrderCloth();
                co.Cloth = cloth;
                co.ClothSize = clothes[i]["ClothSize"].Value<string>();
                co.OrderClothQuantity = clothes[i]["ClothQuantity"].Value<int>();

                orderClothes.Add(co);
            }
            order.Clothes = orderClothes;
            store.Orders.Add(order);

            db.Update(store);
            db.SaveChanges();

            var orderQr = db.Orders.OrderBy(o => o.OrderID).Last();
            QrCode qrCode = new();
            qrCode.CreateQrCode($"Номер заказа: {orderQr.OrderID.ToString()}\n Почта заказчика: {orderQr.OrderCustomerEmail} \n Имя заказчика: {orderQr.OrderCustomerName} \n Стоимость заказа: {orderQr.OrderPrice.ToString()}", $"{orderQr.OrderID.ToString()}");
            return Ok(orderQr.OrderID.ToString());
        }
        [HttpGet]
        [Route("Image")]
        public IActionResult GetClothImage(int clothId)
        {
            var file = System.IO.File.OpenRead($"Content\\Images\\{clothId.ToString()}.jpg");
            if (file != null)
                return Ok(file);
            else
                return NotFound();
        }
        [HttpGet]
        [Route("GetQr")]
        public IActionResult GetQrCode(int orderid)
        {
            var file = System.IO.File.OpenRead($"Content\\QrCodes\\{orderid.ToString()}.png");
            if (file != null)
                return Ok(file);
            else
                return NotFound();
        }
    }
}
