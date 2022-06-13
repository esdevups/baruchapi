const url = 'https://localhost:7196/api'; //TODO: change this before publishing!!!!!


function popoverStart() {
    
    const modalBtns = document.querySelectorAll('.modal-btn');
    const modal = document.getElementById('cat-modal');
    const backdrop = document.querySelector('.popover-backdrop');

    for (btn of modalBtns) {
        const btnTarget = btn.getAttribute('data-target');
        const catId = btn.getAttribute('data-id');
        btn.addEventListener('click', (e) => { e.preventDefault() });
        btn.addEventListener('mouseover', () => {
            openModal(btnTarget);
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

    for (Element of htmlData) {
        navList.innerHTML += Element;
        
    }
    popoverStart();
}

fetchCategories()

let subCats = [];

async function loadSubCats(id) {
    
    const list = document.getElementById('popover-list');

    if (subCats.length === 0) {
        const result = await fetch(`${url}/subcategories`);
        const data = await result.json();
        subCats = data;
        
        
    }
    list.innerHTML = '';
    const filteredSubCats = subCats.filter(item => item.categoryID.toString() === id);
   
    filteredSubCats.forEach(item => {
        list.innerHTML += `<li class="flex">
                        <a class="cat-list-item"
                           href="https://baruch.ir/products?id=${item.id}">${item.title}</a>
                    </li>`;
    })

 
}


