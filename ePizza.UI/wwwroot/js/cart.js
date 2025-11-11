

function AddToCart(ItemId, UnitPrice, Quantity) {
    $.ajax({
       
        type: "Get",
        "url": "/Cart/AddToCart/" + ItemId + "/" + UnitPrice + "/" + Quantity,
        success : function(res) {
           
            $("#cartCounter").text(res.count);
        }
    })
}

function deleteItem(id) {
    debugger;
    if (id > 0) {
        
        if (confirm("Are you sure you want to delete this item?")) {
            $.ajax({
                
                type: "GET",
                url: '/Cart/DeleteItem/' + id,
                success: function (data) {
                    if (data > 0) {
                        location.reload();
                    }
                }
            });
        }
    }
}

function updateQuantity(id, currentQuantity, quantity) {
    if ((parseInt(currentQuantity) >= 1 && quantity > -1)) {
        $.ajax({
            url: '/Cart/UpdateQuantity/' + id + "/" + quantity,
            type: 'PUT',
            success: function (response) {
                
                if (response.count > 0) {
                    location.reload();
                }
            }
        })
    }    
}

$(document).ready(function () {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: '/Cart/GetCartCount',
        success: function (data) {
            $("#cartCounter").text(data);
        },
        error: function (result) {
        },
    });
});