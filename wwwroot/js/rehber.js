// Sayfa yüklendiğinde çalıştır
document.addEventListener("DOMContentLoaded", () => {
    const sliderTabs = document.querySelectorAll(".slider-tab");
    const sliderIndicator = document.querySelector(".slider-indicator");
    const sliderControls = document.querySelector(".slider-controls");

    // 1. Swiper'ı başlat (Her sayfada çalışmalı)
    const swiper = new Swiper(".slider-container", {
        effect: "fade",
        speed: 1300,
        navigation: {
            prevEl: "#slide-prev",
            nextEl: "#slide-next"
        },
        on: {
            slideChange: () => {
                // Sadece sekmeler varsa çalıştır (Hata almamak için kontrol)
                if (sliderTabs.length > 0 && sliderTabs[swiper.activeIndex]) {
                    const currentTabIndex = [...sliderTabs].indexOf(sliderTabs[swiper.activeIndex]);
                    updateIndicator(sliderTabs[swiper.activeIndex], currentTabIndex);
                }
            }
        }
    });

    // 2. Sekmeler (Tablar) varsa çalıştır
    if (sliderTabs.length > 0 && sliderIndicator && sliderControls) {
        
        const updateIndicator = (tab, index) => {
            sliderIndicator.style.transform = `translateX(${tab.offsetLeft - 20}px)`;
            const scrollLeft = sliderTabs[index].offsetLeft - sliderControls.offsetWidth / 2 + sliderTabs[index].offsetWidth / 2;
            sliderControls.scrollTo({ left: scrollLeft, behavior: "smooth" });
        };

        sliderTabs.forEach((tab, index) => {
            tab.addEventListener("click", () => {
                swiper.slideTo(index);
                updateIndicator(tab, index);
            });
        });

        // Başlangıç ayarı
        updateIndicator(sliderTabs[0], 0);
        window.addEventListener("resize", () => updateIndicator(sliderTabs[swiper.activeIndex], 0));
    }
});