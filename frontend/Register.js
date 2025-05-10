document.addEventListener('DOMContentLoaded', () => {
    const form = document.querySelector('.register-form');
    form.addEventListener('submit', async (event) => {
        event.preventDefault();

        const username = document.getElementById('username').value;
        const email = document.getElementById('email').value;
        const confirmEmail = document.getElementById('confirm-email').value;
        const password = document.getElementById('password').value;

        if (email !== confirmEmail) {
            alert('Emails do not match.');
            return;
        }

        const registrationData = {
            firstName: username,
            lastName: username,
            email: email,
            password: password,
            address: "Placeholder Address",
            phoneNumber: "0000000000"
        };

        try {
            const response = await fetch('https://localhost:7112/api/Account/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(registrationData)
            });

            const result = await response.json();

            if (response.ok) {
                alert('Registration successful! Redirecting to login...');
                window.location.href = 'LogIn.html';
            } else {
                alert(result.message || 'Registration failed.');
            }
        } catch (error) {
            console.error('Error:', error);
            alert('Something went wrong. Please try again.');
        }
    });
});
