$(document).ready(function () {
    fetchCartItems(); 
});
function fetchCartItems() {
    var CartItems = localStorage.getItem("CartItems");

    $.ajax({
        url: '/Product/GetCartItems',
        type: 'GET',
        data: { "carts": CartItems },
        success: function (data) {
            $('#CartItems').html(data);
        },
        error: function (xhr, status, error) {
            console.error('Error fetching cart items:', error);
        }
    });
}
function removeItem(productId) {
    var cartItems = JSON.parse(localStorage.getItem('CartItems'));
    var inputId = "i-" + productId;
    var d = document.getElementById(inputId).value;
    var index = cartItems.indexOf(d);

    if (index !== -1) {
        cartItems.splice(index, 1);
        localStorage.setItem('CartItems', JSON.stringify(cartItems));
        fetchCartItems();
        updateCartQuantityDisplay();
    } else {
        alert('Item with ID ' + productId + ' not found in the cart.');
    }
}

// Function to retrieve the base price of the product
function getBasePrice(productId) {
    let priceInput = $("#price-" + productId);
    let priceValue = priceInput.val().replace('$', ''); // Remove the '$' symbol
    return parseFloat(priceValue);
}

// Function to increment quantity
function incrementQuantity(element, productId) {
    let quantitySpan = $(element).closest('.quantity-controls').find('.quantity-text');
    let currentValue = parseInt(quantitySpan.text().replace('Quantity ', ''));
    let quantityId = "quantity-" + productId;
    let maxQuantity = $("#" + quantityId).val();

    if (!isNaN(currentValue) && !isNaN(parseInt(maxQuantity)) && parseInt(maxQuantity) > currentValue) {
        quantitySpan.text('Quantity ' + (currentValue + 1));
        updateTotalPrice(productId, currentValue + 1);
        $("#OrderQuantity-" + productId).val(currentValue + 1);

    }
    else {
            quantitySpan.text('Quantity 1');
        updateTotalPrice(productId,  1);
        $("#OrderQuantity-" + productId).val(1);

    }
}

// Function to decrement quantity
function decrementQuantity(element, productId) {
    let quantitySpan = $(element).closest('.quantity-controls').find('.quantity-text');
    let currentValue = parseInt(quantitySpan.text().replace('Quantity ', ''));

    if (!isNaN(currentValue) && currentValue > 1) {
        quantitySpan.text('Quantity ' + (currentValue - 1));
        updateTotalPrice(productId, currentValue - 1);
        $("#OrderQuantity-" + productId).val(currentValue - 1);
    }
    else {
        quantitySpan.text('Quantity 1');
        updateTotalPrice(productId,  1);
        $("#OrderQuantity-" + productId).val( 1);

    }
}

// Function to update the total price based on quantity
function updateTotalPrice(productId, quantity) {
    let totalPriceElement = $("#total-price-" + productId);
    let basePrice = getBasePrice(productId);
    let totalPrice = basePrice * quantity;
    totalPriceElement.text("$" + totalPrice.toFixed(2));
}

function updateIsSelected(checkbox, index) {
    var selectedItemsInput = document.getElementsByName("selectedItems[" + index + "].IsSelected")[0];
    if (checkbox.checked) {
        selectedItemsInput.value = true;
    } else {
        selectedItemsInput.value = false;
    }
}



function getSizes(productId) {
    var colorName = $("#color-" + productId + " option:selected").val();
    var sizeID = "size-" + productId;
    $.ajax({
        url: '/product/GetSizes',
        type: 'GET',
        data: { "ProductId": productId, "ColorName": colorName },
        success: function (data) {
            var items = '';
            $.each(data, function (index, size) {
                items += '<option value="' + size + '">' + size + '</option>';
            });
            $("#" + sizeID).html(items);
            $('.quantity-text').text('Quantity 1');
            getQuantityAndPrice(productId,colorName);
        },
        error: function (xhr, status, error) {
            console.error('Error fetching sizes:', error);
            }
    });
}

function getQuantityAndPrice(productId, colorName) {
  
    var sizeName = $("#size-" + productId + " option:selected").val();
    $.ajax({
        url: '/product/GetQuantityAndPrice', 
        type: 'GET',
        data: { "ProductId": productId, "ColorName": colorName, "SizeName": sizeName },
        success: function (data) {
            $("#quantity-" + productId).val(data.quantity);
           // $("#itemID-" + productId).val(data.itemId);
            $("#check-" + productId).val(data.itemId);

            $("#price-" + productId).val("$" + data.price.toFixed(2));
            $("#total-price-" + productId).text("$" + data.price.toFixed(2));
            $('.quantity-text').text('Quantity 1');

        },
        error: function (xhr, status, error) {
            console.error('Error fetching quantity and price:', error);
        }
    });
}

