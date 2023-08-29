document.forms['FormEdit'].addEventListener('submit', (event) => {
    event.preventDefault();
    fetch(event.target.action, {
        method: 'POST',
        body: new URLSearchParams(new FormData(event.target)) // event.target is the form
    }).then((response) => {
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response.json(); // or response.text() or whatever the server sends
    }).then((body) => {
        $('#editBox').modal('hide');
        window.location.href = '/Home/';
    }).catch((error) => {
        alert(error);
    });
});
function submitBtn() {
    const password = document.getElementById('RawPassword').value;
    if (!password.includes(':SHA') && password.length > 0) {
        const regex = /^(?=.*\d)(?=.*[a-zA-Z])(?=.*[!@#\$%]).{6,}$/;
        if (!regex.test(password)) {
            const box = document.getElementById('errorBox');
            box.style.display = "block";
            box.innerHTML = '<p>Password must at least 6 character, and contain at least'
                + ' 1 digit, 1 number, 1 special character</p > ';
            return;
        }
        document.getElementById('FormEdit').dispatchEvent(new Event('submit'));
    }
    else {
        const box = document.getElementById('errorBox');
        box.style.display = "block";
        box.innerHTML = `<p> New Password is required</p>`;
    }
}