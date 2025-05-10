document.addEventListener('DOMContentLoaded', () => {
    const form = document.querySelector('.login-form');

    form.addEventListener('submit', async (event) => {
        event.preventDefault();

        const email = document.getElementById('LIemail').value;
        const password = document.getElementById('LIpassword').value;

        const loginData = {
            email: email,
            password: password
        };

        try {
            const response = await fetch('https://localhost:7112/api/Account/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(loginData)
            });

            const result = await response.json();

            if (response.ok) {
                localStorage.setItem('token', result.token);
                alert('Login successful!');
                window.location.href = 'Home.html';
            } else {
                alert(result.message || 'Login failed.');
            }
        } catch (error) {
            console.error('Error:', error);
            alert('An error occurred while logging in.');
        }
    });
});
