var checkbox = document.getElementById('aside-toggle');
var cartInput = document.querySelector('#show-cart.unfolded');
var coverInfo = document.querySelector('#edit input[data-info="icon-input"]')
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
        if(coverInfo.value.length === 0 || coverInfo.value === ''){
            img.src = '/images/icon-canvas.svg'
        }
        else{
            img.src = coverInfo.value;
        }
    })
}