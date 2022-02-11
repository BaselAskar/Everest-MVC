import events from './userDetailsEvents.js';


//HTML Eelements
export const rolesBtns = document.querySelectorAll('.role-btn');
export const blackBackground = document.querySelector('.black-background');
export const confirmStore = document.querySelector('#confirm-store');
export const hidenConfirmStoreBtn = document.getElementById('hiden-confirm-store');
export const addClassificationForm = document.querySelector('#confirm-store form');
export const addClassificationInp = document.getElementById('classification-input');
export const textInValidClassification = document.getElementById('text-invalid-classification');
export const classificationsNames = document.querySelectorAll('#classifications option');
export const addedClassificationsContainer = document.querySelector('.classifications-list');
export const confirmStoreBtn = document.getElementById('confirm-store-btn');
export const storeInfo = document.getElementById('store-info');






//Events
rolesBtns.forEach(btn => btn.addEventListener('click', events.roleBtnEventHanlar));
hidenConfirmStoreBtn.addEventListener('click', events.hidenConfirmStoreHaanlar);
addClassificationForm.addEventListener('submit', events.submitAddClassificationFormHandlar);
confirmStoreBtn.addEventListener('click', events.confirmToStoreHanldar);