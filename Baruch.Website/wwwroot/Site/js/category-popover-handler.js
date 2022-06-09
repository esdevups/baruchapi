
const modalBtns = document.querySelectorAll('.modal-btn');
const modal = document.getElementById('cat-modal');
const backdrop = document.querySelector('.popover-backdrop');

for (btn of modalBtns) {
    const btnTarget = btn.getAttribute('data-target');
    btn.addEventListener('click', (e) => { e.preventDefault() });
    btn.addEventListener('mouseover', () => {
        openModal(btnTarget)
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
    console.log('close modal func')
    modal.classList.replace('flex', 'hidden');


    backdrop.classList.replace('block', 'hidden')


}