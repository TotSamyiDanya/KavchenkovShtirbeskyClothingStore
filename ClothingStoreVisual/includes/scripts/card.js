getCard();
getLinks();
setStoreLocation();
document.getElementById('header_stores_select').onchange = function () {
	getCard();
	clearCard();
	getLinks();
	getStoreLocationLink();
}
let sizeM = 0; let sizeL = 0; let sizeXL = 0; let sizeXXL = 0;
let sizes = [];
async function getCard() {
	let params = (new URL(document.location)).searchParams;
	let id = params.get('clothid');
    let url = `https://localhost:7073/Client/Card?location=${getStoreLocation()}&clothid=${id}`;

    let responce = await fetch(url);
	let content =  await responce.text();
	let jsoncontent = JSON.parse(content);

	let image = document.getElementById('image');
	let brand = document.getElementById('brand');
	let name = document.getElementById('name');
	let price = document.getElementById('price');
	let sale = document.getElementById('sale');

	image.src =  `https://localhost:7073/Client/Image?clothId=${jsoncontent[0][0]['ClothId']}`;
	brand.innerText = jsoncontent[0][0]['ClothBrand'];
	name.innerText = jsoncontent[0][0]['ClothName'];
	price.innerText = jsoncontent[0][0]['ClothPrice'] + '₽';
	sale.innerText = jsoncontent[0][0]['ClothSale'] + '₽';

	let sizes = document.getElementById('main_card_body_form_sizes');

	for(let i = 0; i < jsoncontent[1].length; i++){
		sizes[i] = Number(jsoncontent[1][i]['ClothSizeQuantity']);
		if(Number(jsoncontent[1][i]['ClothSizeQuantity']) > 0){
			let button = document.getElementById(`${jsoncontent[1][i]['ClothSize']}`);
			button.name = Number(jsoncontent[1][i]['ClothSizeQuantity']);
			button.style = 'background-color: white; color: black;';
		}
	}

	sizeM = Number(jsoncontent[1][0]['ClothSizeQuantity']);
	sizeL = Number(jsoncontent[1][1]['ClothSizeQuantity']);
	sizeXL = Number(jsoncontent[1][2]['ClothSizeQuantity']);
	sizeXXL = Number(jsoncontent[1][3]['ClothSizeQuantity'])

	let message = document.createElement('span');
	message.className = 'message';
	message.innerText = '';
	sizes.appendChild(message);

	getSameClothes(jsoncontent[0][0]['ClothCategory'],jsoncontent[0][0]['ClothGender']);
}
async function getSameClothes(category, gender){
	let location = getStoreLocation();
    let url = `https://localhost:7073/Client/SameClothes?location=${location}&category=${category}&gender=${gender}`;

	let responce = await fetch(url);
	let content =  await responce.text();
	let jsoncontent = JSON.parse(content);

	var cards = document.getElementById('main_new_carousel');

	for (let i = 0; i < 2; i++) {
		var carouselItem = document.createElement('div');
		carouselItem.style = "padding-top: 2vw;"
		var cardGroup = document.createElement('div');
		cardGroup.className = 'card-group';
		if (i == 0) {
			carouselItem.className = 'carousel-item active';
		}
		else {
			carouselItem.className = 'carousel-item';
		}
		for (let j = i*4; j < 4*(i+1); j++) {
			var a = document.createElement('a');
			a.href = `card.html?location=${location}&clothid=${jsoncontent[j]['ClothId']}&gender=${gender}`;
			a.style = "text-decoration: none;"
			var card = document.createElement('div');
			card.innerHTML = `<div class="card"><img src="https://localhost:7073/Client/Image?clothId=${jsoncontent[j]['ClothId']}" class="card-img-top" alt="..."><div class="card-body" style="padding-left: 0px; "><h5 class="card-title" style="font-size: 1.0vw; font-weight: 400;">${jsoncontent[j]['ClothBrand']}</h5><h6 class="card-subtitle mb-2" style="font-size: 0.8vw; color: white; font-weight: 300;">${jsoncontent[j]['ClothName']}</h6><a id="sale" href="#" class="card-link" style="font-size: 0.9vw; color: red; font-weight: 500;">${jsoncontent[j]['ClothSale']}₽</a><a id="price" href="#" class="card-link" style="font-size: 0.9vw; color: white; font-weight: 300;">${jsoncontent[j]['ClothPrice']}₽</a></div></div>`;
			a.appendChild(card);
			cardGroup.appendChild(a);
		}
		carouselItem.appendChild(cardGroup);
		cards.appendChild(carouselItem);
	}
}
function getLinks(){
	let params = (new URL(document.location)).searchParams;
	let gender = params.get('gender');

	let manCategory = ['Новинки','Верхняя одежда','Брюки','Толстовки','Футболки','Обувь'];
	let womanCategory = ['Новинки','Верхняя одежда','Брюки','Толстовки','Платья','Обувь'];

	let nav = document.getElementById('assortmentNav');

		for(let i = 0; i < 6; i++){
			let li = document.createElement('li');
			li.className = 'nav-item';

			let a = document.createElement('a');
			a.href = `assortment.html?gender=Женщина&category=${womanCategory[i]}&location=${getStoreLocationLink()}`;
			a.innerText = womanCategory[i];
			a.className = 'nav-link';
			a.style = "color: white;"

			li.appendChild(a);
			nav.appendChild(li);
		}

	let logo = document.getElementById('logo');
	let a = document.createElement('a');

	a.href = `index.html?location=${getStoreLocation()}`;
	a.innerText = 'LOGO';
	logo.innerText = '';

	logo.appendChild(a);

	let manLink = document.getElementById('assortment_man_link');
	let womanLink = document.getElementById('assortment_woman_link');

	manLink.href = `assortment.html?location=${getStoreLocationLink()}&gender=Мужчина`;
	womanLink.href = `assortment.html?location=${getStoreLocationLink()}&gender=Женщина`;
}
function getStoreLocation() {
	let params = (new URL(document.location)).searchParams;
	return params.get('location');
}
function getStoreLocationLink() {
	let select = document.getElementById('header_stores_select');
	return select.value;
}
function addInBasket(e) {
	var form = document.getElementById('main_card_body_form');
	form.addEventListener('submit', getFormValue);

	let message = document.getElementsByClassName('message');
	message[0].innerText = 'Товар добавлен в корзину';
	message[0].style = 'color: white; font-size: 1.2vw;'
}
function getFormValue(e){
	e.preventDefault();

	let params = (new URL(document.location)).searchParams;
	let image = document.getElementById('image');
	let brand = document.getElementById('brand');
	let name = document.getElementById('name');
	let price = document.getElementById('price');
	let sale = document.getElementById('sale');

	var cloth = {
		ClothId : params.get('clothid'),
		ClothName : name.innerText,
		ClothBrand : brand.innerText,
		ClothPrice : price.innerText,
		ClothSale : sale.innerText,
		ClothM : sizeM,
		ClothL : sizeL,
		ClothXL : sizeXL,
		ClothXXL : sizeXXL
	}

	document.cookie = `clothNumber${params.get('clothid')}=${JSON.stringify(cloth)}`;	
}
function setStoreLocation(){
	let loc = getStoreLocation();
	document.getElementById('header_stores_select').value = (`${loc}`);
}
function clearCard() {
	let links = document.getElementById('assortmentNav');
	let carousel = document.getElementById('main_new_carousel');

	links.innerHTML = '';
	carousel.innerHTML = '';
}