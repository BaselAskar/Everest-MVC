import { classificationsNames } from "./app.js";

export const validationClassificationForm = (inputElement,textValidationElement) => {

    let classificatonsValues = [];

    classificationsNames.forEach(cl => classificatonsValues.push(cl.value));

    if (!classificatonsValues.includes(inputElement.value)) {
        inputElement.classList.add('is-invalid');
        textValidationElement.textContent = "الرجاء إضافة تصنيف صحيح!";

        return false;
    }


    let addedClassifications = [];
    const addedClassificationsEl = document.querySelectorAll('.classifications-list .class-item');
    addedClassificationsEl.forEach(el => addedClassifications.push(el.textContent.trim()));


    console.log(addedClassifications);

    if (addedClassifications.includes(inputElement.value)) {
        inputElement.classList.add('is-invalid');
        textValidationElement.textContent = "التصنيف مضاف مسبقا!!";

        return false;
    }

    return true;
}