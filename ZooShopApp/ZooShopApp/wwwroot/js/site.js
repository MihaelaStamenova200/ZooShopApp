let index = 0;

function startSlider() {
    const slides = document.querySelectorAll(".slide");

    if (slides.length === 0) return;

    setInterval(() => {
        slides[index].classList.remove("active");

        index = (index + 1) % slides.length;

        slides[index].classList.add("active");
    }, 3000);
}

// чака страницата да се зареди
document.addEventListener("DOMContentLoaded", startSlider);