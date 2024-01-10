const elementMapping = {
    dropdownElements: document.querySelectorAll('.dropdown-wrapper'),
    scrollSideMenu: document.querySelector('.side-menu ul:nth-child(even)')
}

elementMapping.dropdownElements.forEach(element => {
    const container = element.querySelector('.dropdown-container');
    const content = element.querySelector('.dropdown-content');
    container.addEventListener('transitionend', () => {

        if (container.clientHeight >= 298) {
            content.style.overflowY = 'scroll';
        }
    })
});

function checkContent(){
    if(elementMapping.scrollSideMenu.scrollHeight < elementMapping.scrollSideMenu.clientHeight){
        elementMapping.scrollSideMenu.style.overflowY = "hidden"
    }
}
