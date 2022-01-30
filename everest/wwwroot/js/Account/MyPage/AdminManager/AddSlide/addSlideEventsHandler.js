import { files, setFiles } from "./app.js";
import globalServices from "../../../../globalServices.js";
import { imagesGroup, userName, storeSelect, dataList, orderInput, speedInput, submitBtn } from "./htmlElements.js";

class AddSlideEvenetsHandler {


  addImageChange(e) {
    //HTML Elements
    const input = e.target;

    //Reading file function

    if (files.some((file) => file.name === input.files[0].name)) {
      return;
    }

    files.push(input.files[0]);

    let reader = new FileReader();
    reader.onload = (e) => {
      const html = `
      <div class="col-10 col-md-4 image-container my-3 position-relative">
        <img
        src=${e.target.result}
        alt="image"
        />
        <button type="button" class="btn btn-danger" data-filename="${input.files[0].name}">إزالة</button>
      </div>
    `;

      imagesGroup.insertAdjacentHTML("afterbegin", html);

      document.querySelectorAll(".image-container .btn-danger").forEach((btn) =>
        btn.addEventListener("click", (e) => {
          const btn = e.target.closest(".btn-danger");

          let newFiles = files.filter(
            (file) => file.name !== btn.dataset.filename
          );

          setFiles(newFiles);

          console.log(files);

          const imageContainer = btn.closest(".image-container");

          imageContainer.remove();
        })
      );
    };

    reader.readAsDataURL(input.files[0]);
    }


    storeNameChangeEvent(event) {
        (async function () {
            try {

                globalServices.startButtonSpinner(userName);

                const value = storeSelect.value;

                const userId = dataList.querySelector(`option[value="${value}"]`).dataset.userid;


                const respons = await fetch(`/api/Slide/username-store?userId=${userId}`);

                if (!respons.ok) throw new Error(request.statusText);

                const data = await respons.json();

                userName.innerHTML = data.userName;
            } catch (err) {
                console.error(err.Message);
            }
        })();
    }


    submitHandeler(event) {
        event.preventDefault();

        const value = storeSelect.value;

        const userId = dataList.querySelector(`option[value="${value}"]`).dataset.userid;

        let info = {
            userId: userId,
            order: Number(orderInput.value),
            speed: Number(speedInput.value)
        }

        const request = async () => {
            globalServices.startButtonSpinner(submitBtn);

            let formData = new FormData();

            try {

                if (files.length === 0) throw new Error("You have to add images!");

                for (const file of files) {
                    formData.append("files", file)
                }

                for (let key in info) {
                    formData.append(key, info[key]);
                }

                const res = await fetch(`/api/Slide/add-slide`, {
                    method: "POST",
                    body: formData
                });

                if (!res.ok) throw new Error("something was wrong during sending information!");

                location.reload();

            } catch (err) {
                console.error(err.Message);
            } finally {
                globalServices.stopButtonSpinner(submitBtn);
            }

        };

        request();
    }

  cancelBtnEvent(e) {
    const btnElement = e.target.closest(".btn");
    globalServices.startButtonSpinner(btnElement);

    location.reload();
  }
}

export default new AddSlideEvenetsHandler();
