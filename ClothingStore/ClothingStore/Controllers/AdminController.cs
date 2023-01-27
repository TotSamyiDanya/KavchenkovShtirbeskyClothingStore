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
            //Response.WriteAsync(cloth);
            return Ok();
        }
        [HttpGet]
        [Route("GetClothes")]
        public IActionResult GetClothesInStore(string location)
        {
            ClothingStoreDbContext db = new();
            var store = db.Stores.Where(s => s.StoreLocation == location).FirstOrDefault();
            var clothes = store.Clothes.GroupBy(c => c.Cloth).ToList();
            string json = JsonConvert.SerializeObject(clothes);
            if (!string.IsNullOrEmpty(json))
                return Ok(json);
            else
                return NotFound();
        }
    }
}
