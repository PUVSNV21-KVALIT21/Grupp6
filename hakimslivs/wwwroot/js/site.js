// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let shoppingCart = new Map();
let shoppingCartElt = document.getElementsByClassName('bi-cart')[0];


allAddButtons = document.getElementsByClassName('add-to-cart');

function registerHandlers() {
    for (const addButton of allAddButtons) {
        addButton.onclick = event => {
            productClicked = addButton.name;
            if (shoppingCart.has(productClicked)) {
                currentQuantity = shoppingCart.get(productClicked);
                shoppingCart.set(productClicked, currentQuantity + 1)
            }
            else {
                shoppingCart.set(productClicked, 1)
            }
            if (shoppingCart.size > 0) {
                shoppingCartElt.style.color = "red";
            }
            console.log(productClicked + " was clicked");
            console.log(shoppingCart);
        }
    }
};



registerHandlers();






