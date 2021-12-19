
//Elements
const filesImages = document.querySelectorAll(".file-image");
const form = document.querySelector('.form-add-product');
const nameInp = document.querySelector('#Input_Name');
const descriptionInp = document.querySelector('#Input_Description');
const classificationInp = document.querySelector('#Input_Classification');
const priceInp = document.querySelector('#Input_Price');
const currencyInp = document.querySelector('#Input_Currency');
const isAllowedInp = document.querySelector('#Input_IsAllowed');
const spinnerAddProduct = document.querySelector('.btn-add-product .spinner-border');
const addText = document.querySelector('.btn-add-product .btn-text');

//Functions
const request = async function (information, formData) {


    //Show spinner
    addText.classList.add('d-none');
    spinnerAddProduct.classList.remove('d-none');

    let data;

    //Add product data
    try {
        const res = await fetch("/api/StoreApi/add-product", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(information)
        });

        if (!res.ok) throw new Error("Filed to add product information");

        data = await res.json();



    } catch (err) {
        console.error(err.Message);
    }



    //add product photos
    try {
        const resImg = await fetch(`/api/StoreApi/add-product-photos?id=${data.id}`, {
            method: "POST",
            body: formData
        });

        if (!resImg.ok) throw new Error("Field to add photos");


        //Reload page
        location.reload();


    } catch (err) {
        console.log(err.Message);


        //Hidden spinner
        spinnerAddProduct.classList.add('d-none');
        addText.classList.remove('d-none');

    }

};








//File upload
filesImages.forEach((fIm) => {
    fIm.addEventListener("change", (e) => {
        let reader = new FileReader();
        const image = e.target.closest(".image-box").querySelector("img");
        const uploadBox = e.target
            .closest(".image-box")
            .querySelector(".upload-box");

        reader.onload = (e) => {
            image.src = e.target.result;
        };

        reader.readAsDataURL(fIm.files[0]);
        image.classList.remove("d-none");
        uploadBox.classList.add("d-none");
    });
});




//Form submit
form.addEventListener('submit', (e) => {
    e.preventDefault();


    let imagesData = new FormData();

    if (filesImages[0].files.length === 0) {
        alert("يجب إضافة الصورة الرئيسية!!");
        return;
    }

    filesImages.forEach(fIm => {
        if (fIm.files.length !== 0) {
            imagesData.append("files", fIm.files[0], fIm.name);
        }
    });

    const info = {
        name: nameInp.value,
        description: descriptionInp.value,
        classification: classificationInp.value,
        price: priceInp.value,
        currency: currencyInp.value,
        isAllowed: isAllowedInp.checked ? true : false
    };

    request(info, imagesData);
});

