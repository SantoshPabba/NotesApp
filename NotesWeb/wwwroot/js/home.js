$(document).ready(function () {
    $("#addNoteForm").submit(function (event) {
        event.preventDefault();
        let myFormData = new FormData(event.target);
        let notesData = Object.fromEntries(myFormData.entries());
        let formData = JSON.stringify(notesData);
        $.ajax({
            url: "/Home/CreateNote",
            type: "POST",
            data: formData,
            contentType: 'application/json',
            success: function (data) {
                location.reload();
            },
            error: function (data) {
                console.log(data);
                alert("Error creating note");
            }
        });
    });
   
    $('a[data-target="#editModal"]').click(function () {
        var id = $(this).data('id');
        $.get('/Home/GetNoteByID', { id: id }, function (note) {
            $('#editTitle').val(note.title);
            $('#editContent').val(note.content);
            $('#editId').val(note.id);
            $('#editModal').modal('show');
        });
    });

    $('a[data-target="#deleteModal"]').click(function () {
        var id = $(this).data('id');
        $('#deleteNoteId').val(id);
        $('#deleteModal').modal('show');
    });
    $("button[data-bs-dismiss='modal']").click(function (e) {
        console.log($(this).closest("div.modal").find("form").length);
        $(this).closest("div.modal").find("form")[0].reset();
    });

    $("#editForm").submit(function (event) {
        event.preventDefault();
        let myFormData = new FormData(event.target);
        let notesData = Object.fromEntries(myFormData.entries());
        let formData = JSON.stringify(notesData);
        $.ajax({
            url: "/Home/UpdateNote",
            type: "POST",
            data: formData,
            contentType: 'application/json',
            success: function (data) {
                location.reload();
            },
            error: function (data) {
                console.log(data);
                alert("Error creating note");
            }
        });
    });

    $('#deleteNoteBtn').click(function () {
        // Get the note ID from the hidden input field
        var noteId = $('#deleteNoteId').val();
        // Send the AJAX request to delete the note
        $.ajax({
            url: '/Home/DeleteNote',
            type: 'POST',
            data: { id: noteId },
            success: function (result) {
                location.reload();
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    });
});

Clear_AddForm = () => {
    $("#addNoteForm")[0].reset();
};

