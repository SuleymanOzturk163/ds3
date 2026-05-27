function showBox(number) {

  const allBoxes = document.querySelectorAll(".container-buildler");

  allBoxes.forEach(box => {
    box.classList.remove("active");
  });

  document.getElementById("box" + number).classList.add("active");
}



