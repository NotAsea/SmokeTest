document.forms['FormAdd'].addEventListener('submit', (event) => {
    event.preventDefault();
    fetch(event.target.action, {
        method: 'POST',
        body: new URLSearchParams(new FormData(event.target)) // event.target is the form
    }).then((response) => {
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response.json(); // or response.text() or whatever the server sends
    }).then(_ => {
        $('#editBox').modal('hide');
        window.location.href = '/Home/';
    }).catch((error) => {
        alert(error);
    });
});

function submitAdd() {
    const userName = document.getElementById('UserName').value,
        pass = document.getElementById('RawPassword').value,
        unErr = document.getElementById('UNErr'),
        pnErr = document.getElementById('PAErr');
    let flag = false;
    unErr.innerHTML = pnErr.innerHTML = "";
    if (!userName) {
        unErr.style.display = "block";
        unErr.innerHTML = `<p> UserName is required</p>`;
        flag = true;
    }
    if (!pass || !testPassword(pass)) {
        pnErr.style.display = "block";
        pnErr.innerHTML = !pass ? `<p>Password is required</p>` : `<p>Password must contain at least 1 letter, 1 number, 
            1 special character (!@#$%), and length at least 6</p>`;
        flag = true;
    }
    if (!flag)
        document.getElementById('FormAdd').dispatchEvent(new Event("submit"));
}

function testPassword(pass) {
    const regex = /^(?=.*\d)(?=.*[a-zA-Z])(?=.*[!@#\$%&]).{6,}$/;
    return regex.test(pass);
}
