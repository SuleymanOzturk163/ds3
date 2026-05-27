const Giris = document.querySelector(".Giris");
const Loginlink = document.querySelector(".giris-link");
const Registerlink = document.querySelector(".kayit-link");


Registerlink.addEventListener('click', () => {
    Giris.classList.add('active');
});

Loginlink.addEventListener('click', () => {
    Giris.classList.remove('active');
});



