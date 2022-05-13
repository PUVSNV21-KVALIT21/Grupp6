function LoadCart() {
    var JsonLocalStorageObj = JSON.stringify(localStorage);
    $.ajax({
        url: "/GetCartItems",
        type: "POST",
        data: JsonLocalStorageObj,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            GetTotalSum(result);
        },
        error: function (xhr, status, error) {
            console.log('Error : ' + xhr.responseText);
        }
    });
}

window.addEventListener("load", () => {
    LoadCart();
});

function GetTotalSum(jsonData) {
    var json = JSON.parse(jsonData);
    var container = document.querySelector("#totalSum");
    var total = 0;

    for (var i = 0; i < json.length; i++) {
        total += (json[i].Amount) * (json[i].Item.Price)
    }
    container.textContent = total + "kr";
}

function PlaceOrder() {
    var JsonLocalStorageObj = JSON.stringify(localStorage);
    $.ajax({
        url: "/GenerateOrder",
        type: "POST",
        data: JsonLocalStorageObj,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            console.log(result);
            console.log("nooodles");
        },
        error: function (xhr, status, error) {
            console.log('Error : ' + xhr.responseText);
        }
    });
}