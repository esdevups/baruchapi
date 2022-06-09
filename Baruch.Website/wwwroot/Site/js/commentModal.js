const commentBtn = document.getElementById('comment-btn');
const commentModal = document.getElementById('comment-modal')
const commentBackdrop = document.getElementById('comment-backdrop');
commentBtn.addEventListener('click' , () => {
    commentModal.classList.replace('hidden', 'flex');
});

commentBackdrop.addEventListener('click', () => {
    commentModal.classList.replace('flex', 'hidden')
})