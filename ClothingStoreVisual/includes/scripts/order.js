getQrCode();
function getQrCode() {
	let params = (new URL(document.location)).searchParams;
	let id = params.get('orderid');
	let img = document.getElementById('qrCode');
	img.src = `https://localhost:7073/Client/GetQr?orderid=${id}`;
}