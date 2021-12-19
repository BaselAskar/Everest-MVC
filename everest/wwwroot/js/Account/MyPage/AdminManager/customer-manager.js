
    //Constans
    const classForm = document.querySelector(".confirm-form form");
    const classificationGroup = document.querySelector(".classification-group");
    const classificationInput = document.querySelector(".confirm-form input");
    const btnConfirm = document.querySelector(".confirm-btns-group .btn-success");
    const btnCancel = document.querySelector('.confirm-btns-group .btn-danger');
    const rolesBtns = document.querySelectorAll('.btn-role');
    const searchInput = document.querySelector('.search-input');
    const blackBackground = document.querySelector('.black-background');
    const confirmForm = document.querySelector('.confirm-form');



            //Events convert role of user
            rolesBtns.forEach(btn => {
        btn.addEventListener('click', (e) => {
            var userId = e.target.dataset.userid;
            var role = e.target.dataset.role;


            if (role === "Store" || role === "Clinic") {
                blackBackground.classList.remove('d-none');
                confirmForm.classList.add('confirm-form-visible');



                //Event button confirm
                btnConfirm.addEventListener("click", () => {
                    let classifications = [];

                    const classAddedElements = document.querySelectorAll(
                        "div.class-added:not(.d-none)"
                    );

                    classAddedElements.forEach((classAdded) => {
                        classifications.push(classAdded.textContent.trim());
                    });

                    classifications = [...new Set(classifications)];

                    if (classifications.length === 0) alert("الرجاء إدخال تصنيف المتجر");
                    else {
                        const classificationText = classifications.join(",");

                        console.log(classificationText);

                        window.location.href = `/admin/changeRole?userid=${userId}&role=${role}&classifications=${classificationText}`;
                    }
                });

            }

            if ((role === "Customer" || role === "Moderator")
                && confirm("سيتم حذف جميع البيانات لمستخدم نهائيا ... هل ترغب بالاستمرار؟..")) {

                window.location.href = `/admin/changeRole?userid=${userId}&role=${role}`;
            }


        });
            });



            //Search input
            searchInput.addEventListener('input', () => {
                var users = document.querySelectorAll('.user-row');
    if (searchInput.value !== "") {
        users.forEach(user => {
            if (user.querySelector('.user-name').textContent.includes(searchInput.value)) user.classList.remove('d-none');
            else user.classList.add('d-none');
        });
                } else {
        users.forEach(user => user.classList.remove('d-none'));

                }
            });



    //Confirm convert user to client
    const removeClassification = function (e) {
                const classAdded = e.target.closest(".class-added");

    classAdded.classList.add("d-none");
            };

            classForm.addEventListener("submit", (e) => {
        e.preventDefault();

    if (classificationInput.value === "") return;

    const classAddedElements = document.querySelectorAll(
    "div.class-added:not(.d-none)"
    );

    const html = `
    <div class="class-added m-3">
        <span>${classificationInput.value}<button onclick="removeClassification(event)
                    " class="btn-close" aria-label="close"></button></span>
    </div>
    `;

    classificationGroup.insertAdjacentHTML("beforeend", html);

    classificationInput.value = "";
 });

btnCancel.addEventListener('click', () => {
    classificationGroup.innerHTML = "";
    blackBackground.classList.add('d-none');
    confirmForm.classList.remove('confirm-form-visible');
});


