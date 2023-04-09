
//product search form
var pageNumEl = $('#product-page-number');
function searchProduct() {
    $('#product-frm').submit();
}
//end product search form

//slider
var sliderCount = $('.slider-item').length;
var sliderInd = 0;
const sliderLim = $('.slider-container').data('limit');

$('.slider-container .prev-slide').on('click', () => {
    moveSlide(-1);
})
$('.slider-container .next-slide').on('click', () => {
    moveSlide(1);
})

function moveSlide(i) {
    if (sliderCount <= sliderLim) return;

    var sliderContainer = document.querySelector('.slider-container');
    var width = getComputedStyle(sliderContainer).getPropertyValue('--slide-width');
    sliderInd += i;
    if (sliderInd < 0) sliderInd = sliderCount - sliderLim;
    if (sliderInd > sliderCount - sliderLim) sliderInd = 0;
    $('.slider').css('transform', `translateX(calc( ${-sliderInd} * ${width}))`);
}
//end slider

//dialog
$('.dialog-wrapper').on('click', (e) => {
    if (e.target == e.currentTarget) {
        $(e.target).addClass('hidden');
    }
})
//end dialog

//numeric input
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
//end numeric input