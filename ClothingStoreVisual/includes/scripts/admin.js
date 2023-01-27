let image64 = '';
function getClothes() {
	clearClothesStore();
	var form = document.getElementById('show_clothes_form');
	form.addEventListener('submit', getClothesStore)
}
async function getClothesStore(e) {
	e.preventDefault();

	var form = document.getElementById('show_clothes_form');

	var store = form.querySelector('[name="store"]');
	var url = `https://localhost:7073/Admin/GetClothes?location=${store.value}`;

	var responce = await fetch(url);
	var content =  await responce.text();
	var jsoncontent = JSON.parse(content);

	var table = document.getElementById('table_show');

	for(var i = 0; i < jsoncontent.length; i++){
		var card = document.createElement('div');

		var tr = document.createElement('tr');			
		var tdImage = document.createElement('td');		tdImage.innerHTML = `<img src="https://localhost:7073/Client/Image?clothId=${jsoncontent[i][0]['Cloth']['ClothId']}" class="card-img-top" alt="...">`;
		var tdBrand = document.createElement('td');		tdBrand.innerText = jsoncontent[i][0]['Cloth']['ClothBrand'];
		var tdName = document.createElement('td');		tdName.innerText = jsoncontent[i][0]['Cloth']['ClothName'];
		var tdPrice = document.createElement('td');		tdPrice.innerText = jsoncontent[i][0]['Cloth']['ClothPrice'];
		var tdSale = document.createElement('td');		tdSale.innerText = jsoncontent[i][0]['Cloth']['ClothSale'];
		var tdM = document.createElement('td');			tdM.innerText = jsoncontent[i][0]['ClothSizeQuantity'];
		var tdL = document.createElement('td');			tdL.innerText = jsoncontent[i][1]['ClothSizeQuantity'];
		var tdXL = document.createElement('td');		tdXL.innerText = jsoncontent[i][2]['ClothSizeQuantity'];
		var tdXXL = document.createElement('td');		tdXXL.innerText = jsoncontent[i][3]['ClothSizeQuantity'];


		tr.appendChild(tdImage); tr.appendChild(tdBrand); tr.appendChild(tdName); tr.appendChild(tdPrice); tr.appendChild(tdSale);
		tr.appendChild(tdM); tr.appendChild(tdL); tr.appendChild(tdXL); tr.appendChild(tdXXL);
		table.appendChild(tr);
	}
}
function clearClothesStore(){
	var clothes = document.getElementById('table_show');
	clothes.innerHTML = '';
}
function addCloth() {
	var form = document.getElementById('add_cloth_form');
	form.addEventListener('submit', getFormValueAddCloth);
}
async function getFormValueAddCloth(e) { 
	e.preventDefault();

	var form = document.getElementById('add_cloth_form');

	var name = form.querySelector('[name="name"]');
    var brand = form.querySelector('[name="brand"]');
    var category = form.querySelector('[name="category"]');
    var gender = form.querySelector('[name="gender"]');
    var price = form.querySelector('[name="price"]');
    var sale = form.querySelector('[name="sale"]');
    var sale_end = form.querySelector('[name="sale_end"]');
    var image = form.querySelector('[name="image"]');
	var store = form.querySelector('[name="store"]');
	var sizes = form.getElementsByClassName('add_cloth_size');


	var clothM = Number(sizes[0].getElementsByTagName('input')[0].value);
	var clothL = Number(sizes[1].getElementsByTagName('input')[0].value);
	var clothXL = Number(sizes[2].getElementsByTagName('input')[0].value);
	var clothXXL = Number(sizes[3].getElementsByTagName('input')[0].value);


    let addcloth = {
    	clothName: name.value,
    	clothBrand: brand.value,
    	clothCategory: category.value,
    	clothGender: gender.value,
    	clothPrice: price.value,
    	clothSale: sale.value,
    	clothSaleEnd: sale_end.value,
    	clothStore: store.value,
    	clothSizeM: clothM,
    	clothSizeL: clothL,
    	clothSizeXL: clothXL,
    	clothSizeXXL: clothXXL,
    	clothImage: image64
    };
    console.log(addcloth);
    sendReques("POST", "https://localhost:7073/Admin/AddCloth", addcloth);
}
function updateCloth() {
	var form = document.getElementById('update_cloth_form');
	form.addEventListener('submit', getFormValueUpdateCloth)
}
async function getFormValueUpdateCloth(e) {
	e.preventDefault();

	var form = document.getElementById('update_cloth_form');

	var name = form.querySelector('[name="name"]');
    var brand = form.querySelector('[name="brand"]');
    var category = form.querySelector('[name="category"]');
    var gender = form.querySelector('[name="gender"]');
    var price = form.querySelector('[name="price"]');
    var sale = form.querySelector('[name="sale"]');
    var sale_end = form.querySelector('[name="sale_end"]');
	var store = form.querySelector('[name="store"]');
	var sizes = form.getElementsByClassName('update_cloth_size');


	var clothM = Number(sizes[0].getElementsByTagName('input')[0].value);
	var clothL = Number(sizes[1].getElementsByTagName('input')[0].value);
	var clothXL = Number(sizes[2].getElementsByTagName('input')[0].value);
	var clothXXL = Number(sizes[3].getElementsByTagName('input')[0].value);

    let cloth = {
    	clothName: name.value,
    	clothBrand: brand.value,
    	clothCategory: category.value,
    	clothGender: gender.value,
    	clothPrice: Number(price.value),
    	clothSale: Number(sale.value),
    	clothSaleEnd: sale_end.value,
    	clothStore: store.value,
    	clothSizeM: Number(clothM),
    	clothSizeL: Number(clothL),
    	clothSizeXL: Number(clothXL),
    	clothSizeXXL: Number(clothXXL)
    };

    sendReques("POST", "https://localhost:7073/Admin/UpdateCloth", cloth);
}
function getOrders() {
	clearClothesStore();
	var form = document.getElementById('show_orders_form');
	form.addEventListener('submit', getOrdersStore)
}
async function getOrdersStore(e) {
	e.preventDefault();

	var form = document.getElementById('show_clothes_form');

	var store = form.querySelector('[name="store"]');
	var url = `https://localhost:7073/Admin/GetOrders?location=${store.value}`;

	var responce = await fetch(url);
	var content =  await responce.text();
	var jsoncontent = JSON.parse(content);

	console.log(jsoncontent);

	var table = document.getElementById('table_show_orders');

	for(var i = 0; i < jsoncontent.length; i++){
		var card = document.createElement('div');

		var tr = document.createElement('tr');			
		var tdNumber = document.createElement('td');	tdNumber.innerText = jsoncontent[i]['OrderID'];
		var tdPrice = document.createElement('td');		tdPrice.innerText = jsoncontent[i]['OrderPrice'] + "₽";
		var tdName = document.createElement('td');		tdName.innerText = jsoncontent[i]['OrderCustomerName'];
		var tdEmail = document.createElement('td');		tdEmail.innerText = jsoncontent[i]['OrderCustomerEmail'];
		var tdDest = document.createElement('td');		tdDest.innerText = jsoncontent[i]['OrderDestination'];
		var tdClothes = document.createElement('td');	var clothes = jsoncontent[i]['Clothes'];
		
		for(let j = 0; j < clothes.length; j++){
			tdClothes.innerText += clothes[j]['Cloth']['ClothName'] + ": " + clothes[j]['Cloth']['ClothSale'] + "₽\n " + clothes[j]['ClothSize'] + ": " + clothes[j]['OrderClothQuantity'] + " \n";
		}


		tr.appendChild(tdNumber); tr.appendChild(tdPrice); tr.appendChild(tdName); tr.appendChild(tdEmail); tr.appendChild(tdDest);
		tr.appendChild(tdClothes);
		table.appendChild(tr);
	}
}
function encodeImageFileAsURL(element) {
  var file = element.files[0];
  var reader = new FileReader();
  reader.onloadend = function() {
    console.log('RESULT', reader.result)
    image64 = reader.result;
  }
  reader.readAsDataURL(file);
}
/*

async function getBase64 (file) {

    const reader = new FileReader();

    //reader.addEventListener('onload', () => callback(reader.result));

    await console.log(reader.readAsDataURL(file));
}

async function convertImage(file) {
	getBase64(file, function(base64Data) {
    	clothImage = base64Data;
    	return clothImage;
});
}

*/







/*
async function getAssortmentGender(){
	let params = (new URL(document.location)).searchParams;
	let gender = params.get('gender');
    let url = `https://localhost:7070/Client/GenderAssortment?location=${getStoreLocation()}&gender=${gender}`;

	let responce = await fetch(url);
	let content =  await responce.text();
	let jsoncontent = JSON.parse(content);

	let main = document.getElementById('main_assortment');
	
	for (let i = 0; i < jsoncontent.length / 4; i++){
		var cardGroup = document.createElement('div');
		cardGroup.className = 'card-group';
		for (let j = i*4; j < 4*(i+1); j++){
			var a = document.createElement('a');
			a.href = `card.html?clothid=${jsoncontent[j]['ClothId']}&gender=${gender}`;
			a.style = "text-decoration: none;"
			var card = document.createElement('div');
			card.innerHTML = `<div class="card"><img src="https://localhost:7070/Client/Image?clothId=${jsoncontent[j]['ClothId']}" class="card-img-top" alt="..."><div class="card-body" style="padding-left: 0px; "><h5 class="card-title" style="font-size: 1.0vw; font-weight: 400;">${jsoncontent[j]['ClothBrand']}</h5><h6 class="card-subtitle mb-2" style="font-size: 0.8vw; color: white; font-weight: 300;">${jsoncontent[j]['ClothName']}</h6><a id="sale" href="#" class="card-link" style="font-size: 0.9vw; color: red; font-weight: 500;">${jsoncontent[j]['ClothSale']}₽</a><a id="price" href="#" class="card-link" style="font-size: 0.9vw; color: white; font-weight: 300;">${jsoncontent[j]['ClothPrice']}₽</a></div></div>`;
			a.appendChild(card);
			cardGroup.appendChild(a);
		}
		main.appendChild(cardGroup);
	}
}

*/





















async function send(cloth) {
	alert(cloth);
	try {
    	const res = await fetch(`https://localhost:7070/Admin/AddCloth/cloth`,
    	{
    		method: 'POST',
    		cloth: cloth
    	}
    	);
    	const result = await res.json()
    	this.success = result.success
    	this.id = result.id
    } catch (e) {

    }
}


/*
async function upload(e) {
	e.preventDefault()
    const formData = new FormData()
    formData.append('performer', this.performer)
    formData.append('album', this.album)
    for (let file of this.audio) {
    	formData.append('audio', file)
    }
    formData.append('preview', this.preview[0]);
    try {
    	const res = await fetch(`${keys.BASE_URL}/music/add`,
    	{
    		method: 'POST',
    		body: formData
    	}
    	);
    	const result = await res.json()
    	this.success = result.success
    	this.id = result.id
    } catch (e) {

    }
}
*/

function sendReques(method, url, body = null) {
	const headers = {
		'Content-Type':'application/json'
	};
	return fetch(url, {
		method: method,
		body: JSON.stringify(body),
		headers : headers
	}).then (response => {
		return response.json()
	})
}