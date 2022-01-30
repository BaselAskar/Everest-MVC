//Constans
const form = document.querySelector(".form-edit-product");
const makeMainBtns = document.querySelectorAll(".btns-group .btn-warning");
const removeImgBtns = document.querySelectorAll(".btns-group .btn-danger");
const imageInputs = document.querySelectorAll(".file-upload input");
const submitBtn = document.querySelector('.submit-btn');
const cancelChangesBtn = document.querySelector('.cancel-changes-btn');
const removeProductBtn = document.querySelector('.remove-product-btn');
const confirmRemoving = document.querySelector('.confirm-removing');
const blackBg = document.querySelector('.black-background');
const deleteProductButton = document.querySelector('.confirm-btns-group .btn-warning');
const cancelRemovingProduct = document.querySelector('.confirm-btns-group .btn-success');

//Input constans
const nameInput = document.querySelector('#Input_Name');
const descriptionInput = document.querySelector('#Input_Description');
const classificationInput = document.querySelector('#Input_Classification');
const priceInput = document.querySelector('#Input_Price');
const currencyInput = document.querySelector('#Input_Currency');
const isAllowedInput = document.querySelector('#Input_IsAllowed');


//Functions

const startSpinnerBtn = function (btnElement) {
    const spinner = btnElement.querySelector('.spinner-border');
    const content = btnElement.querySelector('.btn-content');

    content.classList.add('d-none');
    spinner.classList.remove('d-none');
}


const closeSpinnerBtn = function (btnElement) {

    const spinner = btnElement.querySelector('.spinner-border');
    const content = btnElement.querySelector('.btn-content');

    spinner.classList.add('d-none');
    content.classList.remove('d-none');
}




const makingMainRequest = async function (btnElement,productId, id) {
    try {

        startSpinnerBtn(btnElement);

            const res = await fetch(`/api/StoreApi/make-photo-main?productId=${productId}&id=${id}`, {
                method: "PUT"
            });


        if (!res.ok) throw new Error("Field to making main photo!!");

        location.reload();

    } catch (err) {
        console.error(err.Message);
    }

    closeSpinnerBtn(btnElement);

}



const submitRequest = async function (btnElement, formData,info) {
    try {

        startSpinnerBtn(btnElement);

        for (var key in info) {
            formData.append(key, info[key])
        }

        const res = await fetch(`/api/StoreApi/edit-product`, {
            method: "PUT",
            body: formData
        });

        if (!res.ok) throw new Error("Filed to update product!!");

        location.reload();

    } catch (err) {
        console.error(err.Message);
    }

    closeSpinnerBtn(btnElement);

}



const deleteProductRequest = async function (btnElement,productId) {

    startSpinnerBtn(btnElement);

    try {

        const req = await fetch(`/api/StoreApi/delete-product?productId=${productId}`, {
            method: "DELETE"
        });

        if (!req.ok) throw new Error("A Wrong durign deleting product!!");

        location.href = "/Identity/Account/MyProfile/StoreManager/ProductsManager";

    } catch (err) {
        console.error(err.Message);
    }


    closeSpinnerBtn(btnElement);


}


//Events

//Remove image Event
removeImgBtns.forEach((removeBtn) => {
    removeBtn.addEventListener("click", (e) => {
        e.preventDefault();
        //Constans
        const imageGroup = e.target.closest(".image-group");
        const fileUpload = imageGroup
            .closest(".upload-group")
            .querySelector(".file-upload-container");
        const fileUploadInput = fileUpload.querySelector("input");
        const image = imageGroup.querySelector(".product-image");

        //input upload is updated
        fileUploadInput.dataset.updated = "true";

        //Remove file
        if (fileUploadInput.files.length > 0) {
            fileUploadInput.value = null;
        }


        //Displaying upload
        imageGroup.classList.add("d-none");
        fileUpload.classList.remove("d-none");

        //Making image as spinner
        image.src = `${location.origin}/assets/loading.gif`;



    });
});

//Upload file
imageInputs.forEach((imageinput) => {
    imageinput.addEventListener("change", (e) => {
        //Constans
        const fileUpload = e.target.closest(".file-upload-container");
        const imageGroup = e.target
            .closest(".upload-group")
            .querySelector(".image-group");

        //upload and display image
        const image = e.target
            .closest(".upload-group")
            .querySelector(".product-image");

        let reader = new FileReader();

        reader.onload = (e) => {
            image.src = e.target.result;
            imageinput.dataset.updated = "true";
        };

        reader.readAsDataURL(imageinput.files[0]);

        //Dispalying image
        fileUpload.classList.add("d-none");
        imageGroup.classList.remove("d-none");
    });
});




//Making main photo
makeMainBtns.forEach(mmb => mmb.addEventListener('click', (e) => {
    const btn = e.target.closest('.btn-warning');

    const urlParams = new URLSearchParams(location.search);
    const productId = urlParams.get("id");
    const id = btn.dataset.id;



    makingMainRequest(btn,productId,id);
}))



//Cancel changes
cancelChangesBtn.addEventListener('click', (e) => {
    e.preventDefault();

    startSpinnerBtn(cancelChangesBtn);

    location.reload();
})


//Confirm delete product
removeProductBtn.addEventListener('click', (e) => {
    e.preventDefault();


    blackBg.classList.remove('d-none');

    confirmRemoving.classList.add('visiable-confirm-removing');



})



//Delete product
deleteProductButton.addEventListener('click', () => {

    
    const productId = new URLSearchParams(location.search).get("id");

    deleteProductRequest(deleteProductButton, productId);

    
})



//Cancel removing product
cancelRemovingProduct.addEventListener('click', () => {

    blackBg.classList.add('d-none');
    confirmRemoving.classList.remove('visiable-confirm-removing');
})






//Form submit
form.addEventListener("submit", (e) => {
    e.preventDefault();

    let formData = new FormData();

    imageInputs.forEach(imageInput => {


        if (imageInput.files.length > 0 && imageInput.dataset.publicid && imageInput.dataset.updated == "true") {

            formData.append("updatedFiles", imageInput.files[0]);
            formData.append("updated", imageInput.dataset.publicid)
        } else if (imageInput.dataset.updated == "true" && !imageInput.dataset.publicid) {
            formData.append("addedFiles", imageInput.files[0])
        } else if (imageInput.dataset.updated == "true" && imageInput.files.length === 0 && imageInput.dataset.publicid) {

            formData.append("deleted", imageInput.dataset.publicid);
        }
    });


    const info = {
            id: new URLSearchParams(location.search).get('id'),
            name : nameInput.value,
            description : descriptionInput.value,
            classification : classificationInput.value,
            price : priceInput.value,
            currency : currencyInput.value,
            isAllowed : isAllowedInput.checked ? true : false
    };
    
    submitRequest(submitBtn, formData,info)

});
