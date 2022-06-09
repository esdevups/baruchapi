const buttonRights = document.getElementsByClassName('slide-right');
const buttonLefts = document.getElementsByClassName('slide-left');
for(leftButton of buttonLefts) {
    const carouselId = leftButton.getAttribute('data-carousel')
    const carousel = document.getElementById(carouselId);
    
    leftButton.addEventListener('click', function(){
        carousel.scrollBy({
                
                left: -250,
                behavior: 'smooth'
              });
    })
}

for(rightButton of buttonRights) {
    const carouselId = rightButton.getAttribute('data-carousel')
    const carousel = document.getElementById(carouselId);
    
    rightButton.addEventListener('click', function(){
        carousel.scrollBy({
                
                left: 250,
                behavior: 'smooth'
              });
    })
}


// buttonRight.addEventListener('click', function(){
//     slider.scrollBy({
            
//         left: 800,
//         behavior: 'smooth'
//       });
// })


// alert(maxScrollLeft);
// alert("Left Scroll:" + slider.scrollLeft);

//AUTO PLAY THE SLIDER 
// function autoPlay() {
    
        

    
//     if (slider.scrollLeft > (maxScrollLeft - 1)) {
//         slider.scrollLeft -= maxScrollLeft;
//     } else {
//         slider.scrollLeft += 800;
//     }
// }
// let play = setInterval(autoPlay, 6000);

// // PAUSE THE SLIDE ON HOVER
// for (var i=0; i < thumbnails.length; i++){

// thumbnails[i].addEventListener('mouseover', function() {
//     clearInterval(play);
// });

// thumbnails[i].addEventListener('mouseout', function() {
//     return play = setInterval(autoPlay, 6000);
// });
// }