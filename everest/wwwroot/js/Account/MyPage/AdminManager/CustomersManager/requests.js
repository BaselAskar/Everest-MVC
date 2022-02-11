import { users, pagesNumbers,nextPageBtn,prevPageBtn,usersSection } from "./app.js";
import events from "./customersManagerEventsHandler.js";
import servises from "../../../../globalServices.js";

export const getUsersRequest = async (userParams) => {
    try {

        servises.stratSectionSpinner(usersSection);



        const params = new URLSearchParams(userParams).toString();

        const res = await fetch(`/api/users?${params}`);

        if (!res.ok) throw new Error("Field to get users");

        const header = JSON.parse(res.headers.get("Pagination"));

        console.log(header);

        const usersData = await res.json();

        console.log(usersData);



        users.innerHTML = "";

        for (const user of usersData) {



            const html = `
                     <tr data-userid="${user.id}">
                        <td>${user.roleName}</td>
                        <td><img class="user-img" src="${location.origin}/assets/user.png" alt="user photo"/></td>
                        <td>${user.userName}</td>
                        <td>${user.created.substring(0, 10)}</td>
                    </tr>
                     `

            users.insertAdjacentHTML("beforeend", html);
        }

        const usersRows = document.querySelectorAll('tbody tr');

        usersRows.forEach(row => row.addEventListener('click', events.goToUserDetailsEvent));

        pagesNumbers.innerHTML = "";

        console.log(header.totalPages);

        for (let n = 1; n <= header.totalPages; n++) {

            let html;


            if (n === header.currentPage) {
                html = `<button class="btn btn-primary" disabled>${n}</button>`
            } else {
                html = `<button class="btn btn-outline-primary nav-btn">${n}</button>`
            }

            pagesNumbers.insertAdjacentHTML("beforeend", html);
        }

        const navBtns = document.querySelectorAll('.nav-btn');

        navBtns.forEach(btn => {
            btn.addEventListener('click', events.navigationBtnEvent);
        });

        prevPageBtn.disabled = header.currentPage === 1;
        prevPageBtn.dataset.currentpage = header.currentPage;

        nextPageBtn.disabled = header.currentPage === header.totalPages;
        nextPageBtn.dataset.currentpage = header.currentPage;

    } catch (err) {
        console.error(err.Message);
    } finally {
        servises.closeSectionSpinner(usersSection);
    }

}