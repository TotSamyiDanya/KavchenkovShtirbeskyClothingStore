getLinks();
getNewClothes();
getPopularClothes();
document.getElementById('header_stores_select').onchange = function () {
	clearCarousels();
	getLinks();
	getNewClothes();
	getPopularClothes();
}
async function getNewClothes(){
	let location = getStoreLocation();
    let url = `https://localhost:7073/Client/New?location=${location}`;

	let responce = await fetch(url);
	let content =  await responce.text();
	let jsoncontent = JSON.parse(content);

	var cards = document.getElementById('main_new_carousel');

	for (let i = 0; i < 2; i++) {
		var carouselItem = document.createElement('div');

		var cardGroup = document.createElement('div');
		cardGroup.className = 'card-group';

		if (i == 0) {
			carouselItem.className = 'carousel-item active';
		}
		else {
			carouselItem.className = 'carousel-item';
		}

		for (let j = i * 4; j < 4 * (i + 1); j++){
			var a = document.createElement('a');
			a.href = `card.html?location=${getStoreLocation()}&clothid=${jsoncontent[j]['ClothId']}`;
			a.style = 'color: white; text-decoration: none';

			var card = document.createElement('div');
			card.innerHTML = `<div class="card"><img src="https://localhost:7073/Client/Image?clothId=${jsoncontent[j]['ClothId']}" class="card-img-top" alt="..."><div class="card-body" style="padding-left: 0px; "><h5 class="card-title" style="font-size: 1.3vw; font-weight: 400;">${jsoncontent[j]['ClothBrand']}</h5><h6 class="card-subtitle mb-2" style="font-size: 1vw; color: white; font-weight: 300;">${jsoncontent[j]['ClothName']}</h6><a id="sale" href="#" class="card-link" style="font-size: 1.1vw; color: red; font-weight: 500;">${jsoncontent[j]['ClothSale']}₽</a><a id="price" href="#" class="card-link" style="font-size: 1.1vw; color: white; font-weight: 300;">${jsoncontent[j]['ClothPrice']}₽</a></div></div>`;

			a.appendChild(card);
			cardGroup.appendChild(a);
		}

		carouselItem.appendChild(cardGroup);
		cards.appendChild(carouselItem);
	}
}
async function getPopularClothes(){
	let location = getStoreLocation();
    let url = `https://localhost:7073/Client/Popular?location=${location}`;

	let responce = await fetch(url);
	let content =  await responce.text();
	let jsoncontent = JSON.parse(content);

	var cards = document.getElementById('main_popular_carousel');

	for (let i = 0; i < 2; i++){
		var carouselItem = document.createElement('div');

		var cardGroup = document.createElement('div');
		cardGroup.className = 'card-group';

		if (i == 0) {
			carouselItem.className = 'carousel-item active';
		}
		else {
			carouselItem.className = 'carousel-item';
		}

		for (let j = i * 4; j < 4 * (i + 1); j++){
			var a = document.createElement('a');
			a.href = `card.html?location=${getStoreLocation()}&clothid=${jsoncontent[j]['ClothId']}`;
			a.style = 'color: white; text-decoration: none';

			var card = document.createElement('div');
			card.innerHTML = `<div class="card"><img src="https://localhost:7073/Client/Image?clothId=${jsoncontent[j]['ClothId']}" class="card-img-top" alt="..."><div class="card-body" style="padding-left: 0px; "><h5 class="card-title" style="font-size: 1.3vw; font-weight: 400;">${jsoncontent[j]['ClothBrand']}</h5><h6 class="card-subtitle mb-2" style="font-size: 1vw; color: white; font-weight: 300;">${jsoncontent[j]['ClothName']}</h6><a id="sale" href="#" class="card-link" style="font-size: 1.1vw; color: red; font-weight: 500;">${jsoncontent[j]['ClothSale']}₽</a><a id="price" href="#" class="card-link" style="font-size: 1.1vw; color: white; font-weight: 300;">${jsoncontent[j]['ClothPrice']}₽</a></div></div>`;

			a.appendChild(card);
			cardGroup.appendChild(a);
		}

		carouselItem.appendChild(cardGroup);
		cards.appendChild(carouselItem);
	}
}
function clearCarousels(){
	var news = document.getElementById('main_new_carousel');
	var popular = document.getElementById('main_popular_carousel');

	news.innerHTML = '';
	popular.innerHTML = '';
}
function getStoreLocation() {
	let select = document.getElementById('header_stores_select');
	return select.value.substring(2,select.value.length);
}
function getLinks(){
	var links = document.getElementsByClassName('nav-link');

	for (let i = 0; i < links.length; i++) {
		if (links[i].innerText == 'Топы') {
			links[i].href = `assortment.html?location=${getStoreLocation()}&category=Толстовки&gender=Женщина`;
		}
		else {
			links[i].href = `assortment.html?location=${getStoreLocation()}&category=${links[i].innerText}&gender=Женщина`;
		}
	}

	var manImage = document.getElementById('man_category_img');
	var womanImage = document.getElementById('woman_category_img')
	var newImage = document.getElementById('new_category_img');
	var saleImage = document.getElementById('sale_category_img');

	manImage.href = `assortment.html?location=${getStoreLocation()}&gender=Мужчина`;
	womanImage.href = `assortment.html?location=${getStoreLocation()}&gender=Женщина`;
	newImage.href = `assortment.html?location=${getStoreLocation()}&gender=Женщина`;
	saleImage.href = `assortment.html?location=${getStoreLocation()}&gender=Женщина`;

	var manTitle = document.getElementById('man_title');
	var womanTitle = document.getElementById('woman_title');

	manTitle.href = `assortment.html?location=${getStoreLocation()}&gender=Мужчина`;
	womanTitle.href = `assortment.html?location=${getStoreLocation()}&gender=Женщина`;
}