const albumImages = document.getElementsByClassName('album-img');
const albumModal = document.getElementById('album-modal');
const albumBackdrop = document.getElementById('album-backdrop');
for (let i = 0; i < albumImages.length; i++) {
    console.log('inside the loop')
    albumImages[i].addEventListener('click', openAlbumModal);
}
albumBackdrop.addEventListener('click', closeAlbumModal);

const modalImg = document.getElementById('modal-img');









function openAlbumModal(event) {


    albumModal.classList.replace('hidden', 'flex');
    modalImg.src = event.target.src;

}

function closeAlbumModal(event) {


    albumModal.classList.replace('flex', 'hidden');

}
