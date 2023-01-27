using ClothingStoreDbLoader.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.VisualBasic.FileIO;

namespace ClothingStoreDbLoader.Core
{
    internal class DbLoader
    {
        private void LoadClothes()
        {
            using ClothingStoreDbContext db = new();
            string json = File.ReadAllText($"..\\..\\..\\Content\\Clothes.json");
            JArray jArray = JArray.Parse(json);
            Random random = new();
            var stores = db.Stores.ToList();
            List<Cloth> clothes = new();
            foreach (var item in jArray)
            {
                Cloth cloth = new()
                {
                    ClothName = item["Name"].Value<string>(),
                    ClothBrand = item["Brand"].Value<string>(),
                    ClothCategory = item["Category"].Value<string>(),
                    ClothGender = item["Gender"].Value<string>(),
                    ClothPrice = item["Price"].Value<double>(),
                    ClothSale = item["Sale"].Value<double>(),
                    ClothSaleEnding = DateTime.Now.AddDays(random.Next(2, 10)),
                    ClothImage = item["Image"].Value<string>()
                };
                clothes.Add(cloth);
            }
            var results = clothes.GroupBy(x => new { x.ClothName, x.ClothBrand }).Select(x => x.First()).ToList();
            foreach (var cloth in results)
            {
                foreach (var store in stores)
                {
                    ClothQuantity cqM = new() { ClothSize = "M", Cloth = cloth, ClothSizeQuantity = random.Next(5, 15) };
                    ClothQuantity cqL = new() { ClothSize = "L", Cloth = cloth, ClothSizeQuantity = random.Next(5, 15) };
                    ClothQuantity cqXL = new() { ClothSize = "XL", Cloth = cloth, ClothSizeQuantity = random.Next(5, 15) };
                    ClothQuantity cqXXL = new() { ClothSize = "XXL", Cloth = cloth, ClothSizeQuantity = random.Next(5, 15) };
                    store.Clothes.Add(cqM);
                    store.Clothes.Add(cqL);
                    store.Clothes.Add(cqXL);
                    store.Clothes.Add(cqXXL);
                }
            }
            db.SaveChanges();
        }
        private void LoadStores()
        {
            using ClothingStoreDbContext db = new();
            Store store1 = new() { StoreLocation = "Москва" };
            Store store2 = new() { StoreLocation = "Санкт-Петербург" };
            Store store3 = new() { StoreLocation = "Смоленск" };
            Store store4 = new() { StoreLocation = "Тула" };
            Store store5 = new() { StoreLocation = "Краснодар" };
            db.Stores.AddRange(store1, store2, store3, store4, store5);
            db.SaveChanges();
        }
        public void SelectCategories()
        {
            using ClothingStoreDbContext db = new();
            List<string> resultMan = new();
            List<string> resultWoman = new();
            var clothesMan = db.Clothes.Where(c => c.ClothGender == "Мужчина").ToList();
            var clothesWoman = db.Clothes.Where(c => c.ClothGender == "Женщина").ToList();
            foreach (var cloth in clothesMan)
            {
                if (resultMan.Any(c => c == cloth.ClothCategory)) { }
                else
                    resultMan.Add(cloth.ClothCategory);
            }
            foreach (var cloth in clothesWoman)
            {
                if (resultWoman.Any(c => c == cloth.ClothCategory)) { }
                else
                    resultWoman.Add(cloth.ClothCategory);
            }
            File.WriteAllText($"..\\..\\..\\Content\\man_categories.json", JsonConvert.SerializeObject(resultMan));
            File.WriteAllText($"..\\..\\..\\Content\\woman_categories.json", JsonConvert.SerializeObject(resultWoman));
        }
        private void RenameImages()
        {
            using ClothingStoreDbContext db = new();
            var clothes = db.Clothes.ToList();
            for (int i = 0; i < clothes.Count; i++)
            {
                FileSystem.RenameFile($"..\\..\\..\\Content\\Images\\{clothes[i].ClothName}_{clothes[i].ClothBrand}.jpg", $"{clothes[i].ClothId}.jpg");
                clothes[i].ClothImage = $"..\\..\\..\\Content\\Images\\{clothes[i].ClothId}.jpg";
                db.Update(clothes[i]);
            }
            db.SaveChanges();
        }
        public void LoadClothesInDb()
        {
            LoadStores();
            LoadClothes();
            RenameImages();
        }
    }
}
