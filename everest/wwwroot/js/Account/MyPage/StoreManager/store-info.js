
//Constans
const formInfo = document.querySelector('form.container-fluid');
const fileUploadInput = document.querySelector('.file-upload-input');
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
const fileUplaod = document.querySelector('.file-upload');
const storeImage = document.querySelector('.store-image');
const changeImageBtn = document.querySelector('.change-image');
const cancelChanginBtn = document.querySelector('.cancel-changing');


//Image upload

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(".image-upload-wrap").hide();

            $(".file-upload-image").attr("src", e.target.result);
            $(".file-upload-content").show();

            $(".image-title").html(input.files[0].name);
        };

        reader.readAsDataURL(input.files[0]);
    } else {
        removeUpload();
    }
}

function removeUpload() {
    $(".file-upload-input").replaceWith($(".file-upload-input").clone());
    $(".file-upload-content").hide();
    $(".image-upload-wrap").show();
}
$(".image-upload-wrap").bind("dragover", function () {
    $(".image-upload-wrap").addClass("image-dropping");
});
$(".image-upload-wrap").bind("dragleave", function () {
    $(".image-upload-wrap").removeClass("image-dropping");
});

if (changeImageBtn !== null) {
    //Chaneg image
    changeImageBtn.addEventListener('click', () => {
        storeImage.classList.add('d-none');
        fileUplaod.classList.remove('d-none');
    });

}


//Image upload






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

    const request = async function () {



        //Photo request
        if (fileUploadInput.files.length !== 0) {
            let formData = new FormData();

            formData.append("file", fileUploadInput.files[0], fileUploadInput.name);



            try {
                const res = await fetch("/api/storeapi", {
                    method: "POST",
                    body: formData
                });

                if (!res.ok) throw new Error("Field to post photo");
            } catch (err) {
                console.error(err.Message);
            }
        }

        //Information request

        try {
            const resInfo = await fetch("/api/storeapi", {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(info)

            });

            if (!resInfo.ok) throw new Error("Field to update informations");

            window.location.reload();

        } catch (err) {
            console.error(err.Message);
        }

    }


    request();









});

//Cancel changing
cancelChanginBtn.addEventListener('click', () => {
    window.location.reload();
});
