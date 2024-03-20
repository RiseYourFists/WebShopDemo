var checkbox = document.getElementById('aside-toggle');
var cartInput = document.querySelector('#show-cart.unfolded');
var coverInfo = document.querySelector('#edit #BookCover')
checkbox.checked = localStorage.getItem('checkboxState') === 'true';

checkbox.addEventListener('click', function () {
    localStorage.setItem('checkboxState', checkbox.checked);
});

if (cartInput) {
    setTimeout(() => {
        cartInput.checked = false;
    }, 300);
}

if(coverInfo){
    const img = document.querySelector('#edit .img-wrapper img');
    coverInfo.addEventListener('keyup', ()=>{
        if(coverInfo.value.length == 0){
            img.src = '/images/book-template-v2.png'
        }
        else{
            img.src = coverInfo.value;
        }
    })
}