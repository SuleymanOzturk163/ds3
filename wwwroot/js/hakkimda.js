
const dahaFazlasiButon = document.getElementById("daha-fazlasi-buton");
const anaEkran = document.getElementById("ana-ekran"); 
const detayliHakkimda = document.getElementById("detayli-hakkimda"); 


dahaFazlasiButon.addEventListener("click", function () {

  anaEkran.style.display = "none";

  detayliHakkimda.style.display = "block";

  window.scrollTo(0, 0);
  
});
