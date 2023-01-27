using System.Net;
using Newtonsoft.Json;
namespace ClothingStoreParser.Core
{
    internal class Parser
    {
        public void ParsePortions(List<Url> urls)
        {
            List<Cloth> result = new();
            foreach (Url url in urls)
            {
                List<Cloth> clothes = Parse(url.UrlValue, url.UrlGender, url.UrlCategory);
                if (clothes != null)
                    foreach (Cloth cloth in clothes)
                        result.Add(cloth);
            }
            List<Url> imagesUrl = new();
            if (result != null)
            {
                foreach (Cloth cloth in result)
                {
                    Url url = new()
                    {
                        UrlValue = cloth.Image,
                        UrlCategory = cloth.Name,
                        UrlGender = cloth.Brand
                    };
                    imagesUrl.Add(url);
                    cloth.Image = $"..\\..\\..\\Content\\Images\\{cloth.Name}_{cloth.Brand}.jpg";
                }
                DownloadImages(imagesUrl);
                File.WriteAllText($"..\\..\\..\\Content\\Clothes.json", JsonConvert.SerializeObject(result));
            }
        }
        public List<Cloth> Parse(string url, string gender, string category)
        {
            using HttpResponseMessage response = new HttpClient().GetAsync(url).Result;
            List<Cloth> clothes = new();
            if (response.IsSuccessStatusCode)
            {
                var html = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(html))
                {
                    HtmlAgilityPack.HtmlDocument doc = new();
                    doc.LoadHtml(html);

                    var cards = doc.DocumentNode.SelectNodes("//ul[contains(@class,'category-products')]//li");
                    if (cards != null)
                    {
                        var cardInfo = doc.DocumentNode.SelectNodes("//div[contains(@class,'card_body')] | //div[contains(@class,'card_footer')] | //img[contains(@class,'card_image')]");
                        if (cardInfo != null)
                        {
                            for (int i = 0; i < cardInfo.Count; i = i + 4)
                            {
                                Cloth cloth = new();
                                cloth.Id = i / 4;
                                cloth.Image = cloth.CreateUrl(cardInfo[i + 1].Attributes["src"].Value);
                                cloth.Brand = cloth.FindBrand(cardInfo[i + 2].InnerText);
                                cloth.Name = cloth.FindName(cardInfo[i + 2].InnerText);
                                cloth.Price = cloth.FindPrice(cardInfo[i + 3]);
                                cloth.Sale = cloth.FindSale(cardInfo[i + 3]);
                                cloth.Gender = gender;
                                cloth.Category = category;
                                clothes.Add(cloth);
                            }
                        }
                    }
                }
            }
            return clothes;
        }
        public void DownloadImages(List<Url> imagesUrl)
        {
            using var client = new WebClient();
            foreach (Url imageUrl in imagesUrl)
            {
                int i = 1;
                if (File.Exists($"..\\..\\..\\Content\\Images\\{imageUrl.UrlCategory}_{imageUrl.UrlGender}.jpg"))
                {
                    client.DownloadFile(imageUrl.UrlValue, $"..\\..\\..\\Content\\Images\\{imageUrl.UrlCategory}_{imageUrl.UrlGender}{i}.jpg");
                    Console.WriteLine($"Файл {imageUrl.UrlCategory}_{imageUrl.UrlGender}{i}.jpg скачан!");
                    i++;
                }
                else
                {
                    client.DownloadFile(imageUrl.UrlValue, $"..\\..\\..\\Content\\Images\\{imageUrl.UrlCategory}_{imageUrl.UrlGender}.jpg");
                    Console.WriteLine($"Файл {imageUrl.UrlCategory}_{imageUrl.UrlGender}.jpg скачан!");
                }
            }
        }
    }
}
