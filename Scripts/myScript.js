
$('.dialog-wrapper').on('click', (e) => {
    if (e.target == e.currentTarget) {
        $(e.target).addClass('hidden');
    }
})