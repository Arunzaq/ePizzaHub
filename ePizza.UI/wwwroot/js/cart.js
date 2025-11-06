const { get } = require("jquery");

function AddToCart(ItemId, UnitPrice, Quantity) {
    $.ajax({
        type: "Get",
        "url": "/Cart/AddToCart/" + ItemId + "/" + UnitPrice + "/" + Quantity,
        success function(res) {

            alert("Item Added");
        }
    })
}