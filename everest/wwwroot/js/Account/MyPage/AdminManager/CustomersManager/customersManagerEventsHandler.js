import { searchInp, users, pagesNumbers } from "./app.js";
import { getUsersRequest } from "./requests.js"


class CustomersManagerEvents {

    submitSearchFormEvent(event) {
        event.preventDefault();


        const userParams = {
            search: searchInp.value.trim(),
            pageNumber: 1
        };

        getUsersRequest(userParams);
    }


    navigationBtnEvent(event) {
        const navBtn = event.target.closest('.nav-btn');

        const userParams = {
            search: searchInp.value.trim(),
            pageNumber : navBtn.textContent
        };


        getUsersRequest(userParams);
    }


    nextPageEvent(event) {
        const btn = event.target.closest('#next');

        const userParams = {
            search: searchInp.value.trim(),
            pageNumber: +btn.dataset.currentpage + 1
        };

        getUsersRequest(userParams);
    }


    prevPageEvent(event) {
        const btn = event.target.closest('#previous');

        const userParams = {
            search: searchInp.value.trim(),
            pageNumber: +btn.dataset.currentpage - 1
        };

        getUsersRequest(userParams);

    }


    goToUserDetailsEvent(event) {
        const userId = event.target.closest('tr').dataset.userid;

        location.href = `/Identity/Account/MyProfile/AdminManager/UserDetails?userId=${userId}`;
    }

}


export default new CustomersManagerEvents();