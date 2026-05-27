const loader = document.getElementById("dsLoader");

// Site açıldığında
window.addEventListener("load", () => {
  setTimeout(() => {
    loader.classList.add("hide");
  }, 1000); // loading süresi
});

// Link tıklanınca loading tekrar göster
document.querySelectorAll("a").forEach((link) => {
  link.addEventListener("click", function (e) {
    const target = this.getAttribute("href");

    if (!target.includes("http") && !target.startsWith("#")) {
      e.preventDefault();

      loader.classList.remove("hide");

      setTimeout(() => {
        window.location.href = target;
      }, 2000);
    }
  });
});
