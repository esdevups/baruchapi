const dropBtn = document.getElementsByClassName('drop-button');
const profileBackdrop = document.getElementById('profile-backdrop');
const dropdown = document.getElementById('profile-drop');
for (let button of dropBtn) {
    const buttonTarget = button.getAttribute('data-drop');
    button.addEventListener('mouseover', function () {
        openDropDown(buttonTarget);
    });
}
profileBackdrop.addEventListener('mouseover', () => {
    
        
    profileBackdrop.classList.replace('block', 'hidden');
    dropdown.classList.replace('flex', 'hidden')
})

function openDropDown(buttonTarget) {

    const dropDown = document.getElementById(buttonTarget);
    dropDown.classList.replace('hidden', 'flex');
    profileBackdrop.classList.replace('hidden', 'block')
}


