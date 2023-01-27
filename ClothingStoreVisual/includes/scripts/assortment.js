let loc = '';
setStoreLocation();
getLinks();
createUnderline();
getAssortment();
createNavRow();
document.getElementById('header_stores_select').onchange = function () {
	clearLinksAndCards();
	getLinks();
	createUnderline();
	getAssortment();
	createNavRow();
}
async function getAssortment() {
	let params = (new URL(document.location)).searchParams;

	if(params.get('category') == null) {
		getAssortmentGender();
	}
	else {
		getAssortmentGenderCategory();
	}
}
async function getAssortmentGender() {
	let params = (new URL(document.location)).searchParams;
	let gender = params.get('gender');
    let url = `https://localhost:7073/Client/GenderAssortment?location=${getStoreLocation()}&gender=${gender}`;

	let responce = await fetch(url);
	let content =  await responce.text();
	let jsoncontent = JSON.parse(content);

	let main = document.getElementById('main_assortment');
	
	for (let i = 0; i < jsoncontent.length / 4; i++) {
		var cardGroup = document.createElement('div');
		cardGroup.className = 'card-group';

		for (let j = i * 4; j < 4 * (i + 1); j++){
			var a = document.createElement('a');
			a.href = `card.html?location=${getStoreLocationLink()}&clothid=${jsoncontent[j]['ClothId']}`;
			a.style = "text-decoration: none;"

			var card = document.createElement('div');
			card.innerHTML = `<div class="card"><img src="https://localhost:7073/Client/Image?clothId=${jsoncontent[j]['ClothId']}" class="card-img-top" alt="..."><div class="card-body" style="padding-left: 0px; "><h5 class="card-title" style="font-size: 1.0vw; font-weight: 400;">${jsoncontent[j]['ClothBrand']}</h5><h6 class="card-subtitle mb-2" style="font-size: 0.8vw; color: white; font-weight: 300;">${jsoncontent[j]['ClothName']}</h6><a id="sale" href="#" class="card-link" style="font-size: 0.9vw; color: red; font-weight: 500;">${jsoncontent[j]['ClothSale']}₽</a><a id="price" href="#" class="card-link" style="font-size: 0.9vw; color: white; font-weight: 300;">${jsoncontent[j]['ClothPrice']}₽</a></div></div>`;

			a.appendChild(card);
			cardGroup.appendChild(a);
		}
		main.appendChild(cardGroup);
	}
}
async function getAssortmentGenderCategory(){
	let params = (new URL(document.location)).searchParams;
	let gender = params.get('gender');
	let category = params.get('category');
    let url = `https://localhost:7073/Client/GenderCategoryAssortment?location=${getStoreLocationLink()}&gender=${gender}&category=${category}`;

	let responce = await fetch(url);
	let content =  await responce.text();
	let jsoncontent = JSON.parse(content);

	let main = document.getElementById('main_assortment');
	
	for (let i = 0; i < jsoncontent.length / 4; i++) {
		var cardGroup = document.createElement('div');
		cardGroup.className = 'card-group';

		for (let j = i*4; j < 4*(i+1); j++){
			var a = document.createElement('a');
			a.href = `card.html?location=${getStoreLocationLink()}&clothid=${jsoncontent[j]['ClothId']}`;
			a.style = "text-decoration: none;"

			var card = document.createElement('div');
			card.innerHTML = `<div class="card"><img src="https://localhost:7073/Client/Image?clothId=${jsoncontent[j]['ClothId']}" class="card-img-top" alt="..."><div class="card-body" style="padding-left: 0px; "><h5 class="card-title" style="font-size: 1.0vw; font-weight: 400;">${jsoncontent[j]['ClothBrand']}</h5><h6 class="card-subtitle mb-2" style="font-size: 0.8vw; color: white; font-weight: 300;">${jsoncontent[j]['ClothName']}</h6><a id="sale" href="#" class="card-link" style="font-size: 0.9vw; color: red; font-weight: 500;">${jsoncontent[j]['ClothSale']}₽</a><a id="price" href="#" class="card-link" style="font-size: 0.9vw; color: white; font-weight: 300;">${jsoncontent[j]['ClothPrice']}₽</a></div></div>`;

			a.appendChild(card);
			cardGroup.appendChild(a);
		}
		main.appendChild(cardGroup);
	}
}
function getStoreLocation() {
	let params = (new URL(document.location)).searchParams;
	return params.get('location');
}
function getStoreLocationLink() {
	let select = document.getElementById('header_stores_select');
	return select.value;
}
function getLinks(){
	let params = (new URL(document.location)).searchParams;
	let gender = params.get('gender');

	let manCategory = ['Новинки','Верхняя одежда','Брюки','Толстовки','Футболки','Обувь'];
	let womanCategory = ['Новинки','Верхняя одежда','Брюки','Толстовки','Платья','Обувь'];

	if(gender == 'Мужчина') {
		let nav = document.getElementById('assortmentNav');

		for(let i = 0; i < 6; i++) {
			let li = document.createElement('li');
			li.className = 'nav-item';

			let a = document.createElement('a');
			a.href = `assortment.html?location=${getStoreLocationLink()}&gender=${gender}&category=${manCategory[i]}`;
			a.innerText = manCategory[i];
			a.className = 'nav-link';
			a.style = "color: white;"

			li.appendChild(a);
			nav.appendChild(li);
		}
	}
	if(gender == 'Женщина') {
		let nav = document.getElementById('assortmentNav');

		for(let i = 0; i < 6; i++) {
			let li = document.createElement('li');
			li.className = 'nav-item';

			let a = document.createElement('a');
			a.href = `assortment.html?location=${getStoreLocationLink()}&gender=${gender}&category=${womanCategory[i]}`;
			a.innerText = womanCategory[i];
			a.className = 'nav-link';
			a.style = "color: white;"

			li.appendChild(a);
			nav.appendChild(li);
		}
	}

	let logo = document.getElementById('logo');
	let a = document.createElement('a');

	a.href = `index.html?location=${getStoreLocationLink()}`;
	a.innerText = 'LOGO';
	logo.innerText = '';

	logo.appendChild(a);

	let manLink = document.getElementById('assortment_man_link');
	let womanLink = document.getElementById('assortment_woman_link');

	manLink.href = `assortment.html?location=${getStoreLocationLink()}&gender=Мужчина`;
	womanLink.href = `assortment.html?location=${getStoreLocationLink()}&gender=Женщина`;
}
function createNavRow() {
	let result = '';
	let row = document.getElementById('navigation');
	let params = (new URL(document.location)).searchParams;

	let a = document.createElement('a');
	a.href = `index.html?location=${getStoreLocation}`;
	a.innerText = 'Главная / ';
	a.style = "text-decoration: none; color: white;";

	row.appendChild(a);

	if(params.get('gender') != null) {
		let a1 = document.createElement('a');
		a1.href = `assortment.html?gender=${params.get('gender')}`;
		a1.innerText = `${params.get('gender')} /`;
		a1.style = "text-decoration: none; color: white;";

		row.appendChild(a1);
	}

	if (params.get('category') != null) {
		let a2 = document.createElement('a');
		a2.href = `assortment.html?gender=${params.get('gender')}&category=${params.get('category')}`;
		a2.innerText = ` ${params.get('category')}`;
		a2.style = "text-decoration: none; color: white;";

		row.appendChild(a2);
	}
}
function createUnderline() {
	let params = (new URL(document.location)).searchParams;
	let categories = document.getElementById('header_logo_genders');
	let gender = params.get('gender');

	if(gender == "Мужчина") {
		let a = document.getElementById('assortment_man_link');
		a.style = 'text-decoration: underline 0.1vw solid white; text-underline-offset: 0.3vw;';
	}
	if (gender == "Женщина") {
		let a = document.getElementById('assortment_woman_link');
		a.style = 'text-decoration: underline 0.1vw solid white; text-underline-offset: 0.3vw;';
	}
}
function setStoreLocation(){
	let loc = getStoreLocation();
	document.getElementById('header_stores_select').value = (`${loc}`);
}
function clearLinksAndCards(){
	let cards = document.getElementById('main_assortment');
	let links = document.getElementById('assortmentNav');
	let row = document.getElementById('navigation');

	cards.innerHTML = '';
	links.innerHTML = '';
	row.innerText = '';
}