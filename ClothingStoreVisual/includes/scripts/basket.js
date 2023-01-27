let jsonArray = [];
getCookies();
getBasket();
getSizes();
countOrder();
let qrCode = 0;
function getCookies(){
	let cookies = document.cookie;
	let arrCookie = cookies.split(/;\s*/);

	for(let i = 0; i < arrCookie.length; i++) {
		var arr = arrCookie[i].split("=");
      	var name = arr.shift();
      	var value = arr.join("=");
      	jsonArray[i] = JSON.parse(value);
	}
}
async function getBasket() {
	let clothes = document.getElementById('main_basket_clothes');

	for (let i = 0; i < jsonArray.length; i++) {
		let cloth = document.createElement('div');
		cloth.className = 'card mb-3';
		cloth.id = 'card';
		cloth.innerHTML = `<div class="row g-0"><div class="cardId" style="display: none">${jsonArray[i]['ClothId']}</div><div class="col-md-4" id="main_card_image"><img src="https://localhost:7073/Client/Image?clothId=${jsonArray[i]['ClothId']}" id="image" alt="..."></div><div class="col-md-8" id="main_card_body"><div class="card-body" name="${jsonArray[i]['ClothId']}"><h5 class="card-title" id="brand">${jsonArray[i]['ClothBrand']}</h5><p class="card-text" id="name">${jsonArray[i]['ClothName']}</p><div id="main_card_body_prices"><p id="sale">${jsonArray[i]['ClothSale']}</p><p id="price">${jsonArray[i]['ClothPrice']}</p></div></div></div></div>`;
		clothes.appendChild(cloth);
	}
}
async function getImages() {
	let clothes = document.getElementsByClassName('col-md-4');

	for(let i = 0; i < jsonArray.length; i++){
		let img = document.createElement('img');
		img.innerHTML = `<img src="https://localhost:7073/Client/Image?clothId=${jsonArray[i]['ClothId']}" id="image" alt="...">`;
		clothes[i].appendChild(img);
	}
}
function getSizes(){
	let cards = document.getElementsByClassName('card-body');

	for (let i = 0; i < jsonArray.length; i++) {
		let div = document.createElement('div');
		div.className = 'sizes';

		let labelM = document.createElement('label'); let labelL = document.createElement('label'); let labelXL = document.createElement('label'); let labelXXL = document.createElement('label');
		labelM.innerText = 'M'; labelL.innerText = 'L'; labelXL.innerText = 'XL'; labelXXL.innerText = 'XXL';
		let inputM = document.createElement('input'); let inputL = document.createElement('input'); let inputXL = document.createElement('input'); let inputXXL = document.createElement('input');
		inputM.type = 'number'; inputL.type = 'number'; inputXL.type = 'number'; inputXXL.type = 'number';
		inputM.value = 0; inputL.value = 0; inputXL.value = 0; inputXXL.value = 0;
		inputM.max = Number(jsonArray[i]['ClothM']); inputL.max = Number(jsonArray[i]['ClothL']); inputXL.max = Number(jsonArray[i]['ClothXL']); inputXXL.max = Number(jsonArray[i]['ClothXXL']);
		inputM.min = 0; inputL.min = 0; inputXL.min = 0; inputXXL.min = 0;
		inputM.className = 'sizeInput'; inputL.className = 'sizeInput'; inputXL.className = 'sizeInput'; inputXXL.className = 'sizeInput';
 		inputM.oninput = (event) => { countOrder() }; inputL.oninput = (event) => { countOrder() }; inputXL.oninput = (event) => { countOrder() }; inputXXL.oninput = (event) => { countOrder() };


		div.appendChild(labelM); div.appendChild(inputM);
		div.appendChild(labelL); div.appendChild(inputL);
		div.appendChild(labelXL); div.appendChild(inputXL);
		div.appendChild(labelXXL); div.appendChild(inputXXL);
		cards[i].appendChild(div);
	}
}
document.getElementsByClassName('sizeInput').oninput = function(){
	countOrder();
}
function countOrder(){
	let totalPrice = 0;
	let totalSale = 0;
	let sizes = document.getElementsByClassName('sizes');

	for (let i = 0; i < sizes.length; i++) {
		let inputs = sizes[i].getElementsByClassName('sizeInput');

		for (let j = 0; j < inputs.length; j++) {
			totalPrice = totalPrice + Number(jsonArray[i]['ClothPrice'].slice(0,jsonArray[i]['ClothPrice'].length - 1))*Number(inputs[j].value);
			totalSale = totalSale + Number(jsonArray[i]['ClothSale'].slice(0,jsonArray[i]['ClothSale'].length - 1))*Number(inputs[j].value);
		}
	}

	orderPrice = document.getElementById('order_price');
	orderSale = document.getElementById('order_sale');
	orderFinal = document.getElementById('order_price_final_value');

	orderPrice.innerText = totalPrice + "₽";
	orderSale.innerText = totalPrice - totalSale + "₽";
	orderFinal.innerText = totalSale + "₽";
}
function sendOrder(){
	var form = document.getElementById('main_basket_price_form');
	form.addEventListener('submit', getOrder);
}
async function getOrder(e) {
	e.preventDefault();
	var form = document.getElementById('main_basket_price_form');

	var name = form.querySelector('[name="name"]');
    var email = form.querySelector('[name="email"]');
    var destination = form.querySelector('[name="destination"]');
    var price = form.querySelector('[name="price"]');

    let empty = checkEmpty();
    if (empty == false) {
    	clothes = JSON.stringify(checkCards());
    	console.log(clothes);
    	let shit = {
    		customerEmail : String(email.value),
        	customerName : String(name.value),
        	customerDestination: String(destination.value),
        	orderPrice: Number(price.innerText.slice(0,price.innerText.length - 1)),
        	orderClothes: clothes
    	};
    	let result = await sendReques("POST", "https://localhost:7073/Client/Order", shit);
    	document.location.href = await`order.html?orderid=${result}`;
    }
}
function checkEmpty(){
	var clothes = document.getElementsByClassName('card-body');

    for (let i = 0; i < clothes.length; i++) {
    	let sizes = clothes[i].getElementsByClassName('sizes');
    	let inputs = sizes[0].getElementsByTagName('input');

    	for (let j = 0; j < inputs.length; j++) {
    		if(inputs[j].value != 0) {
    			return false;
    		}
    	}
    }
    return true;
}
function checkCards(){
	var clothes = document.getElementsByClassName('card');
	var result = [];

    for (let i = 0; i < clothes.length; i++) {
    	let sizes = clothes[i].getElementsByClassName('sizes');
    	var id = clothes[i].getElementsByClassName('cardId');
    	let inputs = sizes[0].getElementsByTagName('input');
    	let labels = sizes[0].getElementsByTagName('label');

    	for (let j = 0; j < inputs.length; j++) {
    		if (inputs[j].value != 0) {
    			var cloth = {
    				ClothId : Number(id[0].innerText),
    				ClothSize : labels[j].innerText,
    				ClothQuantity : Number(inputs[j].value)
    			};
    			result.push(cloth);
    		}
    	}
    }
    return result;
}
function sendReques(method, url, body = null) {
	const headers = {
		'Content-Type':'application/json'
	};
	return fetch(url, {
		method: method,
		body: JSON.stringify(body),
		headers : headers
	}).then (response => {
		qrCode = Number(response);
		return response.json()
	})
}