import events from "./addSlideEventsHandler.js";
import { cancelBtn, addImageInput, storeSelect, form } from "./htmlElements.js";

export let files = [];
export const setFiles = (newFiles) => (files = newFiles);

addImageInput.addEventListener("change", events.addImageChange);

storeSelect.addEventListener('change', events.storeNameChangeEvent);

cancelBtn.addEventListener("click", events.cancelBtnEvent);

form.addEventListener("submit", events.submitHandeler);