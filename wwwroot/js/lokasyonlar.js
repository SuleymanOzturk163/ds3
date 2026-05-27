const searchForm = document.querySelector(".arama-formu");
const searchBox = document.getElementById("search-box");
const searchBtn = document.querySelector("#search-btn");

const cards = document.querySelectorAll("div[data-title]");

searchBtn.addEventListener("click", function () {
  searchForm.classList.toggle("active");
});

document.addEventListener("click", function (e) {
  if (!searchForm.contains(e.target) && !searchBtn.contains(e.target)) {
    searchForm.classList.remove("active");
  }
});

searchBox.addEventListener("keyup", function () {
  const searchValue = searchBox.value.toLowerCase();

  cards.forEach((card) => {
    const title = card.dataset.title.toLowerCase();

    if (title.includes(searchValue)) {
      card.style.display = "block";
    } else {
      card.style.display = "none";
    }
  });
});

cards.forEach((card) => {
  card.addEventListener("click", function (e) {
    if (e.target.classList.contains("kapat-btn")) {
      this.classList.remove("active");

      e.stopPropagation();
      return;
    }

    if (this.classList.contains("active")) {
      this.classList.remove("active");
      return;
    }

    cards.forEach((otherCard) => {
      if (otherCard !== this) {
        otherCard.classList.remove("active");
      }
    });

    this.classList.add("active");
  });
});
