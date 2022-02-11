import {
    blackBackground,
    confirmStore,
    addClassificationInp,
    textInValidClassification,
    addedClassificationsContainer,
    classificationsNames,
    storeInfo
} from "./app.js";


import { validationClassificationForm } from "./helpers.js";
import { confirmRoleRequest } from "./httpRequests.js"
import services from "../../../../globalServices.js";



class UserDeailsEvents {

    roleBtnEventHanlar(event) {
        const userId = new URLSearchParams(location.search).get("userId");
        const role = event.target.dataset.role;



        if (role == "Customer") {

            const btn = event.target;

            services.startButtonSpinner(btn);

            confirmRoleRequest(userId, role).catch(err => console.error(err.message)).finally(() => services.stopButtonSpinner(btn));
        }





        if (role === "Store") {

            if (storeInfo) {

                confirmRoleRequest(userId, role).catch(errr => console.error(err.messgae));

                return;
            }

        



            //Display confirm store
            blackBackground.classList.remove('d-none');
            confirmStore.classList.remove('d-none');
            confirmStore.style = `animation:show-confirm-store .7s ease-in-out forwards;
                                  -webkit-animation: show-confirm-store .7s ease-in-out forwards;
                                  -o-animation: show-confirm-store .7s ease-in-out forwards;
                                  -moz-animation: show-confirm-store .7s ease-in-out forwards;`



        }
    }





    submitAddClassificationFormHandlar(event) {
        event.preventDefault();

        //Validation
        const valid = validationClassificationForm(addClassificationInp, textInValidClassification);

        if (!valid) return;
        

        const value = addClassificationInp.value;

        addClassificationInp.classList.remove('is-invalid');
        addClassificationInp.value = "";
        textInValidClassification.innerHTML = "";

        const html = `
                     <div class="row d-flex justify-content-center m-1">
                        <div class="col-8 col-md-4 text-center m-3 class-item p-1">
                            ${value}
                            <button class="btn-close"></button>
                        </div>
                    </div>
                    `

        addedClassificationsContainer.insertAdjacentHTML('beforeend', html);

        addedClassificationsContainer.querySelectorAll('.btn-close').forEach(btn => btn.addEventListener('click', (e) => {
            e.target.closest('.row').remove();
        }));

    }





    hidenConfirmStoreHaanlar() {

        confirmStore.style = `animation: hide-confirm-store .7s ease-in-out forwards;
                              -webkit-animation: hide-confirm-store .7s ease-in-out forwards;
                              -o-animation: hide-confirm-store .7s ease-in-out forwards;
                              -moz-animation: hide-confirm-store .7s ease-in-out forwards;`


        setTimeout(() => {
            confirmStore.classList.add('d-none');
            blackBackground.classList.add('d-none');


            addClassificationInp.classList.remove('is-invalid');
            addClassificationInp.value = "";
            textInValidClassification.innerHTML = "";
            addedClassificationsContainer.innerHTML = "";

        }, 700);



    }



    confirmToStoreHanldar(event) {

        const btn = event.target;

        const userId = new URLSearchParams(location.search).get("userId");

        

        const classificationsEl = addedClassificationsContainer.querySelectorAll('.class-item');

        if (classificationsEl.length === 0) {
            alert("الرجاء إضافة التصنيف");
            return;
        }

        let classifications = [];

        classificationsEl.forEach(el => {

            const text = el.textContent.trim();

            const cl = {
                title: text.substring(0, text.indexOf('-')),
                name: text.substring(text.indexOf('-')+1)
            }

            classifications.push(cl);
        });


        services.startButtonSpinner(btn);

        confirmRoleRequest(userId, classifications).catch(err => console.error(err.message)).finally(() => services.stopButtonSpinner(btn));
    }


}

export default new UserDeailsEvents();