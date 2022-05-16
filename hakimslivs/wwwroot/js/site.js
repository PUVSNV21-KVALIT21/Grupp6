let shoppingCart = readLocalStorage() || new Map();
let shoppingCartElt = document.getElementsByClassName('bi-cart')[0];

readLocalStorage();
allAddButtons = document.getElementsByClassName('add-to-cart');

function registerHandlers() {
    for (const addButton of allAddButtons) {
        addButton.onclick = event => {
            let productClicked = addButton.name;
            var stock = (addButton.title);
            if (shoppingCart.has(productClicked)) {
                let currentQuantity = shoppingCart.get(productClicked);
                if (currentQuantity === stock - 1) {
                    shoppingCart.set(productClicked, currentQuantity + 1);
                    addButton.textContent = "Slut"
                    addButton.disabled = true;

                    var className = ".card-text-" + addButton.name;
                    document.querySelector(className).textContent = "Slut 🔴";
                }
                else if (!(currentQuantity >= stock)) {
                    shoppingCart.set(productClicked, currentQuantity + 1);
                }
                else {
                    addButton.textContent = "Slut"
                    addButton.disabled = true;
                    var className = ".card-text-" + addButton.name;
                    document.querySelector(className).textContent = "Slut 🔴";
                }
            }
            else {
                shoppingCart.set(productClicked, 1);
            }

            writeLocalStorage();
            numberOfItemsInCart();
        }
    }
};

window.addEventListener("load", () => {
    disableAddButtons();
});
function disableAddButtons() {
    var btns = document.querySelectorAll(".add-to-cart");

    btns.forEach(btn => {
        if (shoppingCart.get(btn.name) !== undefined) {
            if (shoppingCart.get(btn.name) === parseInt(btn.title)) {
                btn.textContent = "Slut"
                btn.disabled = true;
                var className = ".card-text-" + btn.name;
                document.querySelector(className).textContent = "Slut 🔴";
            }
        }
    });
}

function writeLocalStorage() {
    localStorage.setItem('shopping-cart', JSON.stringify(Object.fromEntries(shoppingCart)));
}

function readLocalStorage() {
    let cartStorage = JSON.parse(localStorage.getItem('shopping-cart'));

    if (cartStorage != null) {
        return new Map(Object.entries(cartStorage));
    };
}

function numberOfItemsInCart() {
    let total = 0;

    var mapIter = shoppingCart.values();
    shoppingCart.forEach(value => {
        total += mapIter.next().value;
    });
    let number = document.getElementById("amount");
    if (total !== 0) {
        number.textContent = total + " ";
        amount.style.color = "green";
    }

    if (shoppingCart.size > 0) {
        shoppingCartElt.style.color = "green";
    }
}

registerHandlers();
numberOfItemsInCart();
