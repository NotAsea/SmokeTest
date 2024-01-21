function edit(id) {
// ReSharper disable once UseOfImplicitGlobalInFunctionScope
    $.ajax({
        url: `/Home/Edit/${id}`,
        success: data => {
            $("#editBox").html(data).modal("show");
        },
        error: () => console.error(`Request fail, id ${id} is not exist`)
    });
}

function getAddNew() {
    $.ajax({
        url: "/Home/Add",
        success: data => {
            $("#editBox").html(data).modal("show");
        },
        error: () => console.error(`Request fail`)
    });
}

document.getElementById("searchUser").addEventListener("keyup", (event) => {
    if (event.key !== "Enter")
        return;
    const value = event.target.value;
    loadTable(1, value);
})

function deleteUser(id) {
    window.location.href = `/Home/Delete/${id}`;
}

function loadTable(index = 1, search = "") {
    if (index === -1 && checkOverflow(index)) {
        index = parseInt($("li.page-item.active").attr("tag-index")) - 1;
    } else if (index === -2 && checkOverflow(index)) {
        index = parseInt($("li.page-item.active").attr("tag-index")) + 1;
    } else if (index < 0 && !checkOverflow(index)) return;
    $.ajax({
        url: `/Home/GetTable?index=${index}&size=15&name=${search}`,
        success: data => {
            $("#UserTable").html(data);
        },
        error: () => {
            console.error();
        }
    });
}

function checkOverflow(index) {
    const current = parseInt($("li.page-item.active").attr("tag-index"));
    switch (index) {
        case -1:
            return current > 1;
        case -2 :
            { 
            const limit = parseInt($("#pageLimit").attr("value"));
            return current < limit;
            }
        default:
            return false;
    }
}
