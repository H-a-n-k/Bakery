
$('.dialog-wrapper').on('click', (e) => {
    if (e.target == e.currentTarget) {
        $(e.target).addClass('hidden');
    }
})

function onQuantityKeydown(event) {
	if (event.keyCode != 8 && event.keyCode < 48 || event.keyCode > 57) {
		event.preventDefault();
		return false;
	}
}

function onCartQuantityChange(event, masp) {
	let val = event.target.value;
	if (isNaN(val) || val == "") event.target.value = 1;

	$.post('/giohangs/UpdateQuantity', { masp, sl: event.target.value });
}

function onQuantityChange(event) {
	let val = event.target.value;
	if (isNaN(val) || val == "") event.target.value = 1;
}