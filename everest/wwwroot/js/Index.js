//constans
const bgSidenav = document.querySelector(".bg-sidenav");
const sidenav = document.querySelector(".sidenav");
const btnCloseSidenav = document.querySelector(".btn-close-sidenav");
const btnSidenav = document.querySelector(".btn-sidenav");

//functions
const closeSidenav = function () {
    bgSidenav.classList.add("d-none");
    sidenav.style.width = "0px";
};

const openSidenav = function () {
    bgSidenav.classList.remove("d-none");
    sidenav.style.width = "250px";
};

//events
btnCloseSidenav.addEventListener("click", closeSidenav);
btnSidenav.addEventListener("click", openSidenav);
