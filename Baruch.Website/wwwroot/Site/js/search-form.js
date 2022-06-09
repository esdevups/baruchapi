const searchForm = document.getElementById('search-form');
const searchFormButton = document.getElementById('search-form-btn');
const searchFormCloseButton = document.getElementById('search-form-close-btn');
const headerIcons = document.getElementById('header-icons');
const headerLogo = document.getElementById('header-logo');
const searchContainer = document.getElementById('search-container');
searchFormButton.addEventListener('click', () => {
    
    searchForm.classList.replace('search-form-hidden', 'search-form-shown');
    headerIcons.classList.replace('flex', 'hidden');
    headerLogo.classList.replace('block', 'hidden');
    
})

searchFormCloseButton.addEventListener('click', () => {
    
    headerIcons.classList.replace('hidden', 'flex');
    headerLogo.classList.replace('hidden', 'block')
    searchForm.classList.replace('search-form-shown', 'search-form-hidden');

})