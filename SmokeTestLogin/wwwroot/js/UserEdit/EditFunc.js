document.forms["FormEdit"].addEventListener("submit", (event) => {
    event.preventDefault();
    fetch(event.target.action, {
        method: "POST",
        body: new URLSearchParams(new FormData(event.target)) // event.target is the form
    }).then(async (response) => {
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${(await response.json()).res}`);
        }
        return response.json(); // or response.text() or whatever the server sends
    }).then(_ => {
        $("#editBox").modal("hide");
        window.location.href = "/Home/";
    }).catch((error) => {
        alert(error);
    });
});

function submitBtn() {
    const password = document.getElementById("RawPassword").value,
        userName = document.getElementById("UserName").value,
        passBox = document.getElementById("PassErr"),
        uBox = document.getElementById("UnameErr");
    passBox.innerHTML = uBox.innerHTML = "";
    let flag = false;
    if (!password.includes(":SHA") && password.length > 0) {
        const regex = /^(?=.*\d)(?=.*[a-zA-Z])(?=.*[!@#\$%&]).{6,}$/;
        if (!regex.test(password)) {
            passBox.style.display = "block";
            passBox.innerHTML = "<p>Password must at least 6 character, and contain at least"
                + " 1 digit, 1 number, 1 special character</p > ";
            flag = true;
        }
    } else if (!password) {
        passBox.style.display = "block";
        passBox.innerHTML = `<p> New Password is required</p>`;
        flag = true;
    }
    if (!userName) {
        uBox.style.display = "block";
        uBox.innerHTML = `<p> User Name is Required</p>`;
        flag = true;
    }
    if (!flag)
        document.getElementById("FormEdit").dispatchEvent(new Event("submit"));
}