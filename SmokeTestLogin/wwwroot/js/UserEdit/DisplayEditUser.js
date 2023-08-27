function edit(id) {
    $.ajax({
        url: `/Home/Edit/${id}`,
        success: data => {
            $('#editBox').html(data).modal('show');
        },
        error: () => console.error(`Request fail, id ${id} is not exist`)
    });
}