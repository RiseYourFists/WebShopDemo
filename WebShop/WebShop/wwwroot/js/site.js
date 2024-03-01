var checkbox = document.getElementById('aside-toggle');
var cartInput = document.querySelector('#show-cart.unfolded');

checkbox.checked = localStorage.getItem('checkboxState') === 'true';

checkbox.addEventListener('click', function () {
    localStorage.setItem('checkboxState', checkbox.checked);
});

if (cartInput) {
    setTimeout(() => {
        cartInput.checked = false;
    }, 300);
}