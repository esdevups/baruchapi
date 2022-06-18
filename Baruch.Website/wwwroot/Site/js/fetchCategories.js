const url = 'https://localhost:7196/api'; //TODO: change this before publishing!!!!!


function popoverStart() {
    const nav = document.getElementById('nav');
    const modalBtns = document.querySelectorAll('.modal-btn');
    const modal = document.getElementById('cat-modal');
    const backdrop = document.querySelector('.popover-backdrop');
    const mobileBtns = document.getElementsByClassName('mobile-btn')
    for (btn of modalBtns) {
        const btnTarget = btn.getAttribute('data-target');
        const catId = btn.getAttribute('data-id');
        btn.addEventListener('click', (e) => { e.preventDefault() });
        btn.addEventListener('mouseover', () => {
            openModal(btnTarget);
            loadSubCats(catId);
        });
    }
    for (btn of mobileBtns) {
        const btnTarget = btn.getAttribute('data-target');
        const catId = btn.getAttribute('data-id');
        btn.addEventListener('click', (event) => {
            event.preventDefault();
            openMenu(event);
            loadSubCats(catId);
            
            
        });

        
    }



    backdrop.addEventListener('mouseover', () => {

        closeModal()
    });





    function openModal(target) {
        const targetModal = document.getElementById(target);
        targetModal.classList.replace('hidden', 'flex');



        backdrop.classList.replace('hidden', 'block')


    }

    function closeModal() {
        console.log('close')
        modal.classList.replace('flex', 'hidden');


        backdrop.classList.replace('block', 'hidden')


    }

}




const navList = document.getElementById('nav-list');


async function fetchCategories () {
  


    const result = await fetch(`${url}/categories`, {
        method: 'GET'
    });
    const data = await result.json();
    
    const htmlData = data.map((category) => `<li  class="mx-3 hover:translate-y-1 transition duration-100"><a data-id="${category.id}" data-target="cat-modal" class="modal-btn hover:text-black" href="">${category.title}<i class="zmdi zmdi-chevron-down"></i></a></li>`)
    
    const mobileHtml = data.map((category) => {
    

       return ` <li class="mx-3 py-3 border-b transition duration-100">
                    <a
                    
                       
                        class="relative block hover:text-black"
                        href=""
                    >
                        ${category.title}
                        <i class="zmdi zmdi-chevron-down"></i>
                        <div
                        data-id="${category.id}" class="mobile-btn  rounded-md bg-opacity-setter  absolute w-full h-full top-0 left-0"></div>
                    </a>
                    <ul id="${category.id}" class="sublist h-zero scale-zero">
                    
                    </ul>
                </li>`}

       )
    
    for (Element of htmlData) {
       
        navList.innerHTML += Element;
               
    }
    const mobileList = document.getElementById('mobile-list')
    for (Element of mobileHtml) {

        mobileList.innerHTML += Element;

    }
   

    startMobileMenu();
    popoverStart();
}

fetchCategories()

let subCats = [];

async function loadSubCats(id) {
    
    const list = document.getElementById('popover-list');
    const mobileList = document.getElementById(id);
    
    if (subCats.length === 0) {
        const result = await fetch(`${url}/subcategories`);
        const data = await result.json();
        subCats = data;
        
        
    }
    list.innerHTML = '';
    mobileList.innerHTML = '';
    const filteredSubCats = subCats.filter(item => item.categoryID.toString() === id);
    
    filteredSubCats.forEach(item => {
        list.innerHTML += `<li class="flex">
                        <a class="cat-list-item"
                           href="https://baruch.ir/products?id=${item.id}">${item.title}</a>
                    </li>`;
        mobileList.innerHTML += `<li class="flex">
                        <a class="cat-list-item"
                           href="https://baruch.ir/products?id=${item.id}">${item.title}</a>
                    </li>`;
       
    });

 
 
}



function startMobileMenu() {

const mobileCatBtn = document.getElementsByClassName('mobile-cat-btn');
const subLists = document.getElementsByClassName('sublist');
const apiaryCard = document.getElementById('apiary-card');

for (btn of mobileCatBtn) {
    btn.addEventListener('click', openMenu)
}

}


function openMenu(event) {
    console.log('open menu')
    

    const element = event.target;

    element.classList.toggle('bg-black');
    const sublistId = element.getAttribute('data-id');
    const buttonId = element.id;
    const closestSublist = document.getElementById(sublistId)



    if (closestSublist.classList.contains('h-zero')) {
        closestSublist.classList.replace('h-zero', 'h-auto');
        closestSublist.classList.replace('scale-zero', 'scale-100');
        closestSublist.scrollBy({
            top: 70,

            behavior: 'smooth'
        });
    } else {
        closestSublist.classList.replace('h-auto', 'h-zero');
        closestSublist.classList.replace('scale-100', 'scale-zero');
    }
}

