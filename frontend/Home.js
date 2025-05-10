console.log("✅ Home.js is running");

document.addEventListener('DOMContentLoaded', async () => {
    const container = document.getElementById('home-products');
    const token = localStorage.getItem('token');

    try {
        const response = await fetch('https://localhost:7112/api/Products');
        const products = await response.json();

        console.log("✅ Products fetched:", products);

        container.innerHTML = '';

        products.slice(0, 4).forEach(product => {
            const name = product.name || 'Unnamed';
            const description = product.description || 'No description';
            const price = Number(product.price);  // ← Usa solo "price" en minúscula

            if (isNaN(price)) {
                console.warn("⚠️ Skipping invalid product:", product, "Invalid price");
                return;
            }

            const productId = product.productId;

            const card = document.createElement('div');
            card.classList.add('product-card');

            card.innerHTML = `
                <img src="background.jpg" alt="${name}" class="product-image">
                <div class="product-info">
                    <h3 class="product-title">${name}</h3>
                    <p class="product-description">${description}</p>
                    <p class="product-price">$${price.toFixed(2)}</p>
                    <button class="add-to-cart" data-id="${productId}">Add to cart</button>
                </div>
            `;

            container.appendChild(card);
        });

        const buttons = document.querySelectorAll('.add-to-cart');
        buttons.forEach(button => {
            button.addEventListener('click', async (event) => {
                event.preventDefault();

                if (!token) {
                    alert('You must be logged in to add items to the cart.');
                    window.location.href = 'LogIn.html';
                    return;
                }

                const productId = parseInt(button.getAttribute('data-id'));
                const data = { productId, quantity: 1 };

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
                        alert('Product added to cart!');
                    } else {
                        const error = await res.text();
                        alert('Failed to add to cart: ' + error);
                    }
                } catch (err) {
                    console.error('Error:', err);
                    alert('Could not connect to server.');
                }
            });
        });

    } catch (error) {
        console.error('❌ Error loading products:', error);
        container.innerHTML = '<p>Error loading products. Please try again later.</p>';
    }
});
