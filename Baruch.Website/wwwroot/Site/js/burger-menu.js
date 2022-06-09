const burgerMenuBtn = document.getElementById('burger-menu-btn');
const burgerMenu = document.getElementById('burger-menu');
const burgerBackdrop = document.querySelector('.burger-backdrop');
const burgerClose = document.getElementById('burger-close');
burgerMenuBtn.addEventListener('click' , () => {
    if (burgerMenu.classList.contains('translate-x-80')) {
        burgerMenu.classList.replace('translate-x-80', 'translate-x-0');
        burgerBackdrop.classList.replace('hidden', 'block')
    }
    
})


burgerClose.addEventListener('click', closeMenu)
burgerBackdrop.addEventListener('click', closeMenu)

function closeMenu () {
    if (burgerMenu.classList.contains('translate-x-0')) {
        burgerMenu.classList.replace('translate-x-0', 'translate-x-80');
        burgerBackdrop.classList.replace('block', 'hidden')
    }
}