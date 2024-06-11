function saveCartItems(event, productId) {
    event.preventDefault();

    let inputId = "i-" + productId;
    let d = document.getElementById(inputId).value;

    let cartItems = localStorage.getItem('CartItems');
    let oldData = cartItems ? JSON.parse(cartItems) : [];

    let existingItemIndex = oldData.indexOf(d);

    if (existingItemIndex === -1) {
        oldData.push(d);
        localStorage.setItem('CartItems', JSON.stringify(oldData));
        document.getElementById('confirmation_text').innerText = 'PRODUCT ADDED TO CART';
        updateCartQuantityDisplay();
    } else {
        document.getElementById('confirmation_text').innerText = 'Item already exists in the cart.';
    }

    document.getElementById('confirmationDialog').style.display = 'block';

    setTimeout(function () {
        document.getElementById('confirmationDialog').style.display = 'none';
    }, 3000);
}



function calculateTotalQuantity() {
    let cartItems = localStorage.getItem('CartItems');
    let totalQuantity = 0;

    if (cartItems) {
        let items = JSON.parse(cartItems);
            totalQuantity = items.length;
    }

    return totalQuantity;
}

function updateCartQuantityDisplay() {
    let totalQuantity = calculateTotalQuantity();
    document.getElementById('cartQuantity').innerText = totalQuantity;
    document.getElementById('cartQuantity_1').innerText = totalQuantity;
}



