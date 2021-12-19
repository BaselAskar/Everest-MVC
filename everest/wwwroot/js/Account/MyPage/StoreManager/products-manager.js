
//Constans
let productRows = document.querySelectorAll('tbody tr');
const searchForm = document.querySelector('.search-container');
const searchInput = document.querySelector('.search-input');
const productsTable = document.querySelector('.product-container');
const bodyTable = document.querySelector('tbody');


//Functions

const startSpinnerTable = (tabelElement) => {
    const spinner = tabelElement.querySelector('.spinner-container');

    
    spinner.classList.remove('d-none');
}

const closeSpinnerTable = (tabelElement) => {
    const spinner = tabelElement.querySelector('.spinner-container');

    spinner.classList.add('d-none');

}

const searchRequest = async function (searchKey,tableElement) {

    startSpinnerTable(tableElement);

    try {

        const res = await fetch(`/api/StoreApi/searchProducts?searchKey=${searchKey}`);

        
        if (!res.ok) throw new Error("Field to searching products!!");

        const products = await res.json();

        console.log(products);

        bodyTable.innerHTML = "";

        

        products.forEach(product => {

            const html = `
            <tr data-id="${product.id}">
                <td>${product.id}</td>
                <td>${product.name}</td>
                <td><img class="table-product-image" src="${product.mainPhotoUrl}" alt="صورة المنتج" /></td>
            </tr>
            `;

            bodyTable.insertAdjacentHTML('beforeend', html);

            productRows = document.querySelectorAll('tbody tr');

            productRows.forEach(product => {
                product.addEventListener('click', (e) => {
                    const id = e.target.closest('tr').dataset.id;

                    location.href = `/Identity/Account/MyProfile/StoreManager/EditProduct?id=${id}`;
                })
            });
        });

    } catch (err) {
        console.error(err.Message);
    }

    closeSpinnerTable(tableElement);

}







//Events


//Search form
searchForm.addEventListener('submit', (e) => {
    e.preventDefault();

    const searchKey = searchInput.value;


    searchRequest(searchKey, productsTable)

})







//product row event
productRows.forEach(product => {
    product.addEventListener('click', (e) => {
        const id = e.target.closest('tr').dataset.id;

        location.href = `/Identity/Account/MyProfile/StoreManager/EditProduct?id=${id}`;
    })
});

