let sliderImages = [];
const slider = document.getElementById('index-slider');
 async function fetchImages () {
    const response = await fetch("https://localhost:7196/api/ui");
    const data = await response.json();
    console.log(data);
    sliderImages = data;
    console.log(sliderImages)

    for (image of sliderImages) {
        slider.innerHTML += `<div class="slide">
            <img src="/Site/img/laura-adai-4tcF4Fe9HiI-unsplash_2.jpg"
                 alt="" />
        </div>`
    }
};

fetchImages();



// Select all slides
const slides = document.querySelectorAll(".slide");

// loop through slides and set each slides translateX property to index * 100% 
slides.forEach((slide, indx) => {
  slide.style.transform = `translateX(${indx * 100}%)`;
});

// select next slide button
const nextSlide = document.querySelector(".btn-next");

// current slide counter
let curSlide = 0;
// maximum number of slides
let maxSlide = slides.length - 1;

// add event listener and navigation functionality

nextSlide.addEventListener("click", nextSlideHandler);

function nextSlideHandler () {
    // check if current slide is the last and reset current slide
    if (curSlide === maxSlide) {
      curSlide = 0;
    } else {
      curSlide++;
    }
  
  //   move slide by -100%
    slides.forEach((slide, indx) => {
      slide.style.transform = `translateX(${100 * (indx - curSlide)}%)`;
    });
  }
// select prev slide button
const prevSlide = document.querySelector(".btn-prev");

// add event listener and navigation functionality
prevSlide.addEventListener("click", function () {
  // check if current slide is the first and reset current slide to last
  if (curSlide === 0) {
    curSlide = maxSlide;
  } else {
    curSlide--
  }

  //   move slide by 100%
  slides.forEach((slide, indx) => {
    slide.style.transform = `translateX(${100 * (indx - curSlide)}%)`;
  });
});


//   automatic movement 

let automove = setInterval(nextSlideHandler, 4000)


//   stopping the interval if mouse is on slider 

for(slide of slides) {

    slide.addEventListener('mouseenter', () => {
        
        clearInterval(automove)
    })

    slide.addEventListener('mouseleave', () => {
        
        automove = setInterval(nextSlideHandler, 4000)
    })
}

