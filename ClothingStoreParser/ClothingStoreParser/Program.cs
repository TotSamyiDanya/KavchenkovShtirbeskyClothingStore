using ClothingStoreParser.Core;

Console.WriteLine("Hello, World!");
Console.WriteLine("Будем парсить!");
Parser parser = new();
#region init
List<Url> urls = new();
Url url1 = new()
{
    UrlValue = "https://stockmann.ru/category/255-muzhskie-bryuki/",
    UrlCategory = "Брюки",
    UrlGender = "Мужчина"
};
Url url2 = new()
{
    UrlValue = "https://stockmann.ru/category/255-muzhskie-bryuki/?page=2",
    UrlCategory = "Брюки",
    UrlGender = "Мужчина"
};
Url url3 = new()
{
    UrlValue = "https://stockmann.ru/category/255-muzhskie-bryuki/?page=3",
    UrlCategory = "Брюки",
    UrlGender = "Мужчина"
};
Url url4 = new()
{
    UrlValue = "https://stockmann.ru/category/254-muzhskaya-verkhnyaya-odezhda/",
    UrlCategory = "Верхняя одежда",
    UrlGender = "Мужчина"
};
Url url5 = new()
{
    UrlValue = "https://stockmann.ru/category/254-muzhskaya-verkhnyaya-odezhda/?page=2",
    UrlCategory = "Верхняя одежда",
    UrlGender = "Мужчина"
};
Url url6 = new()
{
    UrlValue = "https://stockmann.ru/category/254-muzhskaya-verkhnyaya-odezhda/?page=3",
    UrlCategory = "Верхняя одежда",
    UrlGender = "Мужчина"
};
Url url7 = new()
{
    UrlValue = "https://stockmann.ru/category/261-muzhskie-tolstovki-i-svitshoty/",
    UrlCategory = "Толстовки",
    UrlGender = "Мужчина"
};
Url url8 = new()
{
    UrlValue = "https://stockmann.ru/category/261-muzhskie-tolstovki-i-svitshoty/?page=2",
    UrlCategory = "Толстовки",
    UrlGender = "Мужчина"
};
Url url9 = new()
{
    UrlValue = "https://stockmann.ru/category/261-muzhskie-tolstovki-i-svitshoty/?page=3",
    UrlCategory = "Толстовки",
    UrlGender = "Мужчина"
};
Url url10 = new()
{
    UrlValue = "https://stockmann.ru/category/260-muzhskie-futbolki-i-polo/",
    UrlCategory = "Футболки",
    UrlGender = "Мужчина"
};
Url url11 = new()
{
    UrlValue = "https://stockmann.ru/category/260-muzhskie-futbolki-i-polo/?page=2",
    UrlCategory = "Футболки",
    UrlGender = "Мужчина"
};
Url url12 = new()
{
    UrlValue = "https://stockmann.ru/category/260-muzhskie-futbolki-i-polo/?page=3",
    UrlCategory = "Футболки",
    UrlGender = "Мужчина"
};
Url url13 = new()
{
    UrlValue = "https://stockmann.ru/category/267-muzhskaya-obuv/",
    UrlCategory = "Обувь",
    UrlGender = "Мужчина"
};
Url url14 = new()
{
    UrlValue = "https://stockmann.ru/category/267-muzhskaya-obuv/?page=2",
    UrlCategory = "Обувь",
    UrlGender = "Мужчина"
};
Url url15 = new()
{
    UrlValue = "https://stockmann.ru/category/267-muzhskaya-obuv/?page=3",
    UrlCategory = "Обувь",
    UrlGender = "Мужчина"
};
urls.Add(url1); urls.Add(url2); urls.Add(url3); urls.Add(url4); urls.Add(url5);
urls.Add(url6); urls.Add(url7); urls.Add(url8); urls.Add(url9); urls.Add(url10);
urls.Add(url11); urls.Add(url12); urls.Add(url13); urls.Add(url14); urls.Add(url15);
Url url16 = new()
{
    UrlValue = "https://stockmann.ru/category/58-zhenskaya-verkhnyaya-odezhda/",
    UrlCategory = "Верхняя одежда",
    UrlGender = "Женщина"
};
Url url17 = new()
{
    UrlValue = "https://stockmann.ru/category/58-zhenskaya-verkhnyaya-odezhda/?page=2",
    UrlCategory = "Верхняя одежда",
    UrlGender = "Женщина"
};
Url url18 = new()
{
    UrlValue = "https://stockmann.ru/category/58-zhenskaya-verkhnyaya-odezhda/?page=3",
    UrlCategory = "Верхняя одежда",
    UrlGender = "Женщина"
};
Url url19 = new()
{
    UrlValue = "https://stockmann.ru/category/183-zhenskie-bryuki/",
    UrlCategory = "Брюки",
    UrlGender = "Женщина"
};
Url url20 = new()
{
    UrlValue = "https://stockmann.ru/category/183-zhenskie-bryuki/?page=2",
    UrlCategory = "Брюки",
    UrlGender = "Женщина"
};
Url url21 = new()
{
    UrlValue = "https://stockmann.ru/category/183-zhenskie-bryuki/?page=3",
    UrlCategory = "Брюки",
    UrlGender = "Женщина"
};
Url url22 = new()
{
    UrlValue = "https://stockmann.ru/category/197-zhenskie-platya-i-sarafany/",
    UrlCategory = "Платья",
    UrlGender = "Женщина"
};
Url url23 = new()
{
    UrlValue = "https://stockmann.ru/category/197-zhenskie-platya-i-sarafany/?page=2",
    UrlCategory = "Платья",
    UrlGender = "Женщина"
};
Url url24 = new()
{
    UrlValue = "https://stockmann.ru/category/197-zhenskie-platya-i-sarafany/?page=3",
    UrlCategory = "Платья",
    UrlGender = "Женщина"
};
Url url25 = new()
{
    UrlValue = "https://stockmann.ru/category/203-zhenskie-tolstovki-i-svitshoty/",
    UrlCategory = "Толстовки",
    UrlGender = "Женщина"
};
Url url26 = new()
{
    UrlValue = "https://stockmann.ru/category/203-zhenskie-tolstovki-i-svitshoty/?page=2",
    UrlCategory = "Толстовки",
    UrlGender = "Женщина"
};
Url url27 = new()
{
    UrlValue = "https://stockmann.ru/category/203-zhenskie-tolstovki-i-svitshoty/?page=3",
    UrlCategory = "Толстовки",
    UrlGender = "Женщина"
};
Url url28 = new()
{
    UrlValue = "https://stockmann.ru/category/182-zhenskaya-obuv/",
    UrlCategory = "Обувь",
    UrlGender = "Женщина"
};
Url url29 = new()
{
    UrlValue = "https://stockmann.ru/category/182-zhenskaya-obuv/?page=2",
    UrlCategory = "Обувь",
    UrlGender = "Женщина"
};
Url url30 = new()
{
    UrlValue = "https://stockmann.ru/category/182-zhenskaya-obuv/?page=3",
    UrlCategory = "Обувь",
    UrlGender = "Женщина"
};
urls.Add(url16); urls.Add(url17); urls.Add(url18); urls.Add(url19); urls.Add(url20);
urls.Add(url21); urls.Add(url22); urls.Add(url23); urls.Add(url24); urls.Add(url25);
urls.Add(url26); urls.Add(url27); urls.Add(url28); urls.Add(url29); urls.Add(url30);
#endregion
//parser.ParsePortions(urls);