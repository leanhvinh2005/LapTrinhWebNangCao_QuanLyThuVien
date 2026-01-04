export function initializeComponent(id) {
    console.log(id);
    const track = document.querySelector(`#${id} .carouseltrack_home`);
    const items = Array.from(document.querySelectorAll(`#${id} .carouselitem_home`));
    const prevBtn = document.querySelector(`#${id} .carouselprev_home`);
    const nextBtn = document.querySelector(`#${id} .carouselnext_home`);

    let currentIndex = 0;

    function getVisibleItemsCount() {
        const carouselWidth = document.querySelector(`#${id} .carousel_home`).offsetWidth;
        const itemWidth = items[0].offsetWidth + 20;
        return Math.floor(carouselWidth / itemWidth);
    }

    function updateCarousel() {
        const itemWidth = items[0].offsetWidth + 20;
        track.style.transform = "translateX(-${currentIndex * itemWidth}px)";
    }

    nextBtn.addEventListener("click", () => {
        const visibleCount = getVisibleItemsCount();
        currentIndex += 1;
        if (currentIndex > items.length - visibleCount) {
            currentIndex = 0;
        }
        updateCarousel();
    });

    prevBtn.addEventListener("click", () => {
        const visibleCount = getVisibleItemsCount();
        currentIndex -= 1;
        if (currentIndex < 0) {
            currentIndex = items.length - visibleCount;
        }
        updateCarousel();
    });
}


