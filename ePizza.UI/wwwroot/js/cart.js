

function AddToCart(ItemId, UnitPrice, Quantity) {
    $.ajax({
        type: "Get",
        "url": "/Cart/AddToCart/" + ItemId + "/" + UnitPrice + "/" + Quantity,
        success : function(res) {
           
            $("#cartCounter").text(res.count);
        }
    })
}