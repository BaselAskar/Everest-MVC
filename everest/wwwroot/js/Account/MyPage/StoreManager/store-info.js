

//Html elements
const formInfo = document.querySelector('form.container');
const fileUploadInput = document.querySelector('.file-upload input');
const nameInput = document.querySelector('#Input_Name');
const descriptionInput = document.querySelector('#Input_Description');
const managerInput = document.querySelector('#Input_Manager');
const cityInput = document.querySelector('#Input_City');
const adressInput = document.querySelector('#Input_Adress');
const whatsapp1Input = document.querySelector('#Input_Whatsapp1');
const whatsapp2Input = document.querySelector('#Input_Whatsapp2');
const phoneNumber1Input = document.querySelector('#Input_PhoneNumber1');
const phoneNumber2Input = document.querySelector('#Input_PhoneNumber2');
const locationUrlInput = document.querySelector('#Input_LocationUrl');
const fileUplaod = document.querySelector('.file-upload-container');
const imageGroup = document.querySelector('.image-group');
const storeImage = document.querySelector('.store-image');
const removImageBtn = document.querySelector('.image-group .btn-danger');
const submitBtn = document.querySelector('.submit-btn');
const cancelChanginBtn = document.querySelector('.cancel-changing');



//Functions
const startSpinner = function (btnElement) {

    const spinner = btnElement.querySelector('.spinner-border');
    const btnContent = btnElement.querySelector('.btn-content');

    btnContent.classList.add('d-none');
    spinner.classList.remove('d-none');
}


const closeSpinner = function (btnElement) {

    const spinner = btnElement.querySelector('.spinner-border');
    const btnContent = btnElement.querySelector('.btn-content');

    spinner.classList.add('d-none');
    btnContent.classList.remove('d-none');

}



const submitRequest = async function (btnElement, formData, information) {

    try {

        startSpinner(btnElement);

        const queryInfo = new URLSearchParams(information).toString();

        const res = await fetch(`/api/StoreApi/edit-store?${queryInfo}`, {
            method: "PUT",
            body: formData
        });

        if (!res.ok) throw new Error("خطأ في تعديل بيانات المتجر...!!!");

        location.reload();


    } catch (err) {
        console.error(err.Message);
    }

    closeSpinner(btnElement);

}


//Image upload
fileUploadInput.addEventListener('change', (event) => {

    let reader = new FileReader();

    reader.readAsDataURL(event.target.files[0]);

    reader.onload = (e) => storeImage.src = e.target.result;


    //Displaying image
    fileUplaod.classList.add('d-none');
    imageGroup.classList.remove('d-none');

});



//Remove image
removImageBtn.addEventListener('click', () => {

    imageGroup.classList.add('d-none');
    fileUplaod.classList.remove('d-none');

    if (fileUploadInput.files.length > 0) fileUploadInput.value = null;
    
    storeImage.src = `${location.origin}/assets/loading.gif`;

});





//Form submit
formInfo.addEventListener('submit', e => {
    e.preventDefault();


    const info = {
        name: nameInput.value,
        description: descriptionInput.value,
        manager: managerInput.value,
        city: cityInput.value,
        adress: adressInput.value,
        whatsapp1: whatsapp1Input.value,
        whatsapp2: whatsapp2Input.value,
        phoneNumber1: phoneNumber1Input.value,
        phoneNumber2: phoneNumber2Input.value,
        locationUrl: locationUrlInput.value
    }


    let formData = new FormData();

    

    if (fileUploadInput.files.length !== 0 && !fileUploadInput.dataset.publicid) {

        formData.append("file", fileUploadInput.files[0]);

    } else if (fileUploadInput.files.length === 0 && fileUploadInput.dataset.publicid) {

        formData.append("deleteId", fileUploadInput.dataset.publicid);

    } else if (fileUploadInput.files.length !== 0 && fileUploadInput.dataset.publicid) {

        formData.append("deleteId", fileUploadInput.dataset.publicid);
        formData.append("file", fileUploadInput.files[0]);
        
    }



    
    submitRequest(submitBtn, formData, info);


});

//Cancel changing
cancelChanginBtn.addEventListener('click', () => {

    startSpinnerBtn(cancelChanginBtn);
    window.location.reload();
});
