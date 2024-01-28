var checkbox = document.getElementById('aside-toggle');

checkbox.checked = localStorage.getItem('checkboxState') === 'true';

checkbox.addEventListener('click', function () {
    localStorage.setItem('checkboxState', checkbox.checked);
});