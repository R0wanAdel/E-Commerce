document.addEventListener('DOMContentLoaded', () => {

      const toggleBtn = document.querySelector('.menu-toggle');
  const nav = document.querySelector('.nav-links');

  toggleBtn.addEventListener('click', () => {
    nav.classList.toggle('show');
  });
    const token = localStorage.getItem('token');

    if (!token) {
        alert('You must be logged in to view your cart.');
        window.location.href = 'LogIn.html';
        return;
    }

    const cartContainer = document.querySelector('.cart-summary');

    // Cargar carrito del usuario
    async function loadCart() {
        try {
            const response = await fetch('https://localhost:7112/api/Cart', {
                headers: {
                    'Authorization': 'Bearer ' + token
                }
            });

            if (!response.ok) {
                throw new Error('Failed to load cart.');
            }

            const items = await response.json();
            cartContainer.innerHTML = '';

            if (items.length === 0) {
                cartContainer.innerHTML = '<p>Your cart is empty.</p>';
                return;
            }

            items.forEach(item => {
                const div = document.createElement('div');
                div.classList.add('cart-item');
                div.innerHTML = `
                    <h3 class="item-title">${item.productName}</h3>
                    <p class="item-description">Quantity: ${item.quantity}</p>
                    <strong class="item-price">$${item.total.toFixed(2)}</strong>
                    <button class="remove-btn" data-productid="${item.productId}">Remove</button>
                `;
                cartContainer.appendChild(div);
            });

        } catch (error) {
            console.error(error);
            cartContainer.innerHTML = '<p>Error loading cart.</p>';
        }
    }

    // Eliminar producto del carrito
    cartContainer.addEventListener('click', async (e) => {
        if (e.target.classList.contains('remove-btn')) {
            const productId = e.target.getAttribute('data-productid');
            try {
                const response = await fetch(`https://localhost:7112/api/Cart/RemoveItem/${productId}`, {
                    method: 'DELETE',
                    headers: {
                        'Authorization': 'Bearer ' + token
                    }
                });

                if (response.ok) {
                    alert('Product removed from cart.');
                    loadCart();
                } else {
                    alert('Error removing product.');
                }
            } catch (error) {
                console.error(error);
                alert('Failed to remove product.');
            }
        }
    });

    // Finalizar orden
    const form = document.querySelector('form');
    form.addEventListener('submit', async (e) => {
        e.preventDefault();

        try {
            const response = await fetch('https://localhost:7112/api/Order/CreateOrder', {
                method: 'POST',
                headers: {
                    'Authorization': 'Bearer ' + token
                }
            });

            if (response.ok) {
                alert('Order placed successfully!');
                window.location.href = 'Home.html';
            } else {
                const text = await response.text();
                alert('Error placing order: ' + text);
            }
        } catch (error) {
            console.error('Error placing order:', error);
            alert('Failed to place order.');
        }
    });

    loadCart();
});
