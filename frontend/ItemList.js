document.addEventListener('DOMContentLoaded', async () => {
    const productGrid = document.querySelector('.product-grid');
    const searchInput = document.getElementById('search-input');
    const searchButton = document.getElementById('search-button');
    let allProducts = [];

    async function loadProducts() {
        try {
            const response = await fetch('https://localhost:7112/api/Products');
            const products = await response.json();
            allProducts = products;
            renderProducts(allProducts);
        } catch (error) {
            console.error('Error loading products:', error);
            productGrid.innerHTML = '<p>Error loading products. Please try again later.</p>';
        }
    }

    function renderProducts(productsToShow) {
        productGrid.innerHTML = '';
        if (!productsToShow || productsToShow.length === 0) {
            productGrid.innerHTML = '<p>No products found.</p>';
            return;
        }

        productsToShow.forEach(product => {
            const card = document.createElement('div');
            card.className = 'product-card';
            card.innerHTML = `
                <a href="ItemDetails.html?id=${product.productId}">
                    <img src="background.jpg" alt="${product.name}" class="product-image">
                    <div class="product-info">
                        <h3 class="product-title">${product.name}</h3>
                        <p class="product-description">${product.description}</p>
                        <p class="product-price">$${Number(product.price).toFixed(2)}</p>
                    </div>
                </a>
            `;
            productGrid.appendChild(card);
        });
    }

    function filterProducts() {
        const query = searchInput.value.trim();
        const filtered = allProducts.filter(product =>
            product.name.includes(query) || product.description.includes(query)
        );
        renderProducts(filtered);
    }

    searchButton.addEventListener('click', filterProducts);
    searchInput.addEventListener('keypress', (e) => {
        if (e.key === 'Enter') filterProducts();
    });

    await loadProducts();
});
