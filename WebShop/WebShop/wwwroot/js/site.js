const checkbox = document.getElementById('aside-toggle');
const cartInput = document.querySelector('#show-cart.unfolded');
const coverInfo = document.querySelector('#edit input[data-info="icon-input"]')
const searchBars = document.querySelectorAll('.search-bar-wrapper');
checkbox.checked = localStorage.getItem('checkboxState') === 'true';


checkbox.addEventListener('click', function () {
    localStorage.setItem('checkboxState', checkbox.checked);
});

if (cartInput) {
    setTimeout(() => {
        cartInput.checked = false;
    }, 300);
}

if (coverInfo) {
    const img = document.querySelector('#edit .img-wrapper img');
    coverInfo.addEventListener('keyup', () => {
        if (coverInfo.value.length === 0 || coverInfo.value === '') {
            img.src = '/images/icon-canvas.svg'
        }
        else {
            img.src = coverInfo.value;
        }
    })
}

searchBars.forEach(bar => {
    const typingInterval = 2000; // 2 seconds
    const apiCallback = bar.getAttribute('data-api-callback');
    const resultCallback = bar.getAttribute('data-result-callback')
    const returnRoute = bar.getAttribute('data-return-route')

    if (apiCallback) {
        const apiWrapper = createElement('div', null, bar, ['api-search-result']);
        const resultList = createElement('ul', null, apiWrapper)
        const submitBtn = createElement('button', 'Look for more results', apiWrapper)
        const input = bar.querySelector('input[type="search"]')
        submitBtn.type = 'submit';

        input.addEventListener('keyup', () => {
            if (input.value.length > 0) {
                apiWrapper.style.maxHeight = '700px';
            }
            let typingTimer;
            addLoader(resultList, true);
            clearTimeout(typingTimer);
            typingTimer = setTimeout(fetchData, typingInterval);
        })

        input.addEventListener('input', () => {
            if (input.value.length <= 0) {
                apiWrapper.style.maxHeight = '0px'
            }
        })

        function fetchData() {
            const apiURL = apiCallback + input.value;

            fetch(apiURL)
                .then((result) => result.json())
                .then(data =>{
                    const object = JSON.parse(data);
                    resultList.innerHTML = '';

                    if (object.length > 0) {
                        object.forEach(element => {
                            const address = resultCallback + `?ItemId=${element.Id}` + returnRoute;
                            addSearchResultRow(element.Title, element.BookCover, address, resultList)
                        })
                    }
                    else {
                        addErrorElement('No results found', resultList, true);
                    }
                })
                .catch(() => {
                    addErrorElement('No results found', resultList, true);
                });
        }

        function addErrorElement(msg, parentNode, clearContent) {
            if (clearContent) {
                resultList.innerHTML = '';
            }

            createElement('li', msg, parentNode, ['error'])
        }

        function addLoader(parentElement, clearParentNodes){
            if(clearParentNodes){
                parentElement.innerHTML = '';
            }

            const loaderContainer = createElement('li', null, parentElement, ['loader'])
            createElement('div', null, loaderContainer)
            createElement('span', 'Loading...', loaderContainer)
        }
    }
})

function addSearchResultRow(title, img, targetLink, parentNode) {
    const wrapper = createElement('li', null, parentNode);
    const link = createElement('a', null, wrapper);
    const imgContainer = createElement('div', null, link, ['img-wrapper'])
    const imgCover = createElement('img', null, imgContainer);
    createElement('span', title, link)

    link.href = targetLink;
    imgCover.src = img;
    imgCover.alt = 'cover photo';
}

function createElement(type, content, parentNode, classes, id, useInnerHtml) {
    const element = document.createElement(type);
    if (content && useInnerHtml) {
        element.innerHTML = content;
    }
    else {
        if (content && type !== 'input') {
            element.textContent = content;
        }
        if (content && type === 'input') {
            element.value = content;
        }
    }
    if (classes && classes.length > 0) {
        element.classList.add(...classes);
    }
    if (id) {
        element.setAttribute('id', id);
    }
    if (parentNode) {
        parentNode.appendChild(element);
    }
    return element;
}