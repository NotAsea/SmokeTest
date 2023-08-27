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
    const password = document.getElementById('Password').value;
    //const regex = /^(?=.*\d)(?=.*[a-zA-Z])[a-zA-Z0-9]{6,}$/;
    //if (!regex.test(password)) {
    //    const box = document.getElementById('errorBox');
    //    box.style.display = "block";
    //    box.innerHTML += '<p>Password must atlest 6 character, and contain at least 1 digit and 1 number, no special character allow</p>';
    //    return;
    //}
    document.getElementById('FormEdit').dispatchEvent(new Event('submit'));
}