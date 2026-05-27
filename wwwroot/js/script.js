
function slowScroll(targetY, duration = 1500) {
    const startY = window.scrollY;
    const distance = targetY - startY;
    let startTime = null;

    function animation(currentTime) {
        if (!startTime) startTime = currentTime;
        const progress = Math.min((currentTime - startTime) / duration, 1);

        const ease = Math.pow(progress, 3);

        window.scrollTo(0, startY + distance * ease);

        if (progress < 1) requestAnimationFrame(animation);
    }

    requestAnimationFrame(animation);
}

const fadeLayer = document.getElementById("fadeLayer");

document.querySelectorAll(".link").forEach(link => {
    link.addEventListener("click", () => {
        const targetId = link.getAttribute("data-target");
        const targetElement = document.getElementById(targetId);

        fadeLayer.style.opacity = "1";

        setTimeout(() => {
            slowScroll(targetElement.offsetTop);
        }, 150);

        setTimeout(() => {
            fadeLayer.style.opacity = "0";
        }, 900);
    });
});
