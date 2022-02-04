import services from "../../../../globalServices.js";
import events from "./customersManagerEventsHandler.js";

//HTML Elements
export const form = document.querySelector('#search-form');
export const searchInp = document.querySelector('#search');
export const users = document.querySelector('#users-section tbody');
export const usersSection = document.querySelector('#users-section');
export const usersRows = document.querySelectorAll('tbody tr');
export const pagesNumbers = document.querySelector("#pages-numbers");
export const nextPageBtn = document.querySelector('#next');
export const prevPageBtn = document.querySelector('#previous');
export const navBtns = document.querySelectorAll(".nav-btn");

//Add Events
form.addEventListener("submit", events.submitSearchFormEvent);



prevPageBtn.addEventListener('click', events.prevPageEvent);
nextPageBtn.addEventListener('click', events.nextPageEvent);

usersRows.forEach(row => row.addEventListener('click', events.goToUserDetailsEvent));

navBtns.forEach(btn => {
    btn.addEventListener('click', events.navigationBtnEvent);
})
