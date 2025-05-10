document.addEventListener('DOMContentLoaded', async () => {
    const params = new URLSearchParams(window.location.search);
    const productId = params.get('id');

    const token = localStorage.getItem('token');
    const productNameEl = document.querySelector('.product-name');
    const productSubheadingEl = document.querySelector('.product-subheading');
    const productPriceEl = document.querySelector('.product-price');
    const productDescriptionEl = document.querySelector('.product-description');

    if (!productId) {
        alert('No product ID found.');
        return;
    }

    try {
        const response = await fetch(`https://localhost:7112/api/Products/${productId}`);
        if (!response.ok) {
            throw new Error('Product not found');
        }
        const product = await response.json();

        productNameEl.textContent = product.name;
        productSubheadingEl.textContent = product.description;
        productPriceEl.textContent = `$${product.price.toFixed(2)}`;
        productDescriptionEl.textContent = product.description;

        const addToCartBtn = document.querySelector('.add-to-cart-btn');
        addToCartBtn.addEventListener('click', async () => {
            if (!token) {
                alert('You must be logged in to add to cart.');
                window.location.href = 'LogIn.html';
                return;
            }

            const data = {
                productId: product.productId,
                quantity: 1
            };

            try {
                const res = await fetch('https://localhost:7112/api/Cart/AddToCart', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + token
                    },
                    body: JSON.stringify(data)
                });

                if (res.ok) {
                    alert('Added to cart!');
                } else {
                    const error = await res.text();
                    alert('Failed to add: ' + error);
                }
            } catch (err) {
                console.error(err);
                alert('Error adding product to cart.');
            }
        });

    } catch (error) {
        console.error(error);
        alert('Failed to load product.');
    }
});
