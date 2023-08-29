function edit(id) {
    $.ajax({
        url: `/Home/Edit/${id}`,
        success: data => {
            $('#editBox').html(data).modal('show');
        },
        error: () => console.error(`Request fail, id ${id} is not exist`)
    });
}
function getAddnew() {
    $.ajax({
        url: '/Home/Add',
        success: data => {
            $('#editBox').html(data).modal('show');
        },
        error: () => console.error(`Request fail, id ${id} is not exist`)
    })
}
function searchUser(event) {
    if (event.key !== "Enter")
        return;
    const value = event.target.value;
    window.location.href = `/Home/Search?param=${value}`;
}
function deleteUser(id) {
    window.location.href = `/Home/Delete/${id}`;
}