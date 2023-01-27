using HtmlAgilityPack;

namespace ClothingStoreParser.Core
{
    internal class Cloth
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public double Sale { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public string[] Sizes
        {
            get
            {
                string[] sizes = { "XS", "S", "M", "L", "XL", "XXL", "XXXL" };
                return sizes;
            }
        }
        public override string ToString() => $"Картинка: {Image} \n +" +
            $"Название: {Name} \n" +
            $"Брэнд: {Brand} \n" +
            $"Цена: {Price.ToString()} \n" +
            $"Скидка: {Sale.ToString()}" +
            $"---------------------------------";

        public string CreateUrl(string url)
        {
            return "https://stockmann.ru" + url.Substring(0, url.Length - 3) + "jpg";
        }
        public int isCyrillic(string text)
        {
            string pattern = @"[абвгдеёжзийклмнопрстуфхэюяьъыцщш]";
            char[] textArray = text.ToLower().ToCharArray();
            for (int i = 0; i < textArray.Length; i++)
                if (pattern.Contains(textArray[i]))
                    return i;
            return -1;
        }
        public string FindName(string text)
        {
            string result = "";
            int cirilic = isCyrillic(text);
            int i = cirilic;
            if (cirilic != -1)
                while (i < text.Length)
                {
                    result += text[i];
                    i++;
                }
            return result;
        }
        public string FindBrand(string text)
        {
            string result = "";
            int i = 0;
            int cirilic = isCyrillic(text);
            if (cirilic != -1)
                while (i < cirilic)
                {
                    result += text[i];
                    i++;
                }
            return result;
        }
        public double FindPrice(HtmlNode html)
        {
            string result = html.SelectSingleNode("//li[contains(@class,'card_price')]").InnerText;
            return Convert.ToDouble(result);
        }
        public double FindSale(HtmlNode html)
        {
            string result = html.SelectSingleNode("//li[contains(@class,'card_discount')]").InnerText;
            return Convert.ToDouble(result);
        }
    }
}
