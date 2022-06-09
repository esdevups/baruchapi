const mobileCatBtn = document.getElementsByClassName('mobile-cat-btn');
const subLists = document.getElementsByClassName('sublist');
const apiaryCard = document.getElementById('apiary-card');

for (btn of mobileCatBtn) {
    btn.addEventListener('click', openMenu)
}



function openMenu(event) {
    console.log('open menu')
    event.preventDefault();
    
    const element = event.target;
    
    element.classList.toggle('bg-black');
    const sublistId = element.getAttribute('data-target');
    const buttonId = element.id;
    const closestSublist = document.querySelector(`.sublist#${sublistId}`)
    
    
    
    if (closestSublist.classList.contains('h-0')) {
        closestSublist.classList.replace('h-0', 'h-auto');
        closestSublist.classList.replace('scale-0', 'scale-100');
        closestSublist.scrollBy({
            top: 70,

            behavior: 'smooth'
        });
    } else {
        closestSublist.classList.replace('h-auto', 'h-0');
        closestSublist.classList.replace('scale-100', 'scale-0');
    }
}
