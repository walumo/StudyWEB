// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ConfirmTaskDelete(id) {
    let confirmDelete = confirm("Delete task?")
    if (confirmDelete) {
        $.ajax({
            url: 'Topic/RemoveTask',
            data: { taskId: id }
        }).done(function () {
            location.reload()
        });
    }
}

function ConfirmTopicDelete(id) {
    let confirmDelete = confirm("Delete topic?")
    if (confirmDelete) {
        $.ajax({
            url: 'Topic/Delete',
            data: { topicId: id }
        }).done(function () {
            location.reload()
        })
    }
}

function ConfirmDeleteNote(id) {
    let confirmDelete = confirm('Delete note?')
    if (confirmDelete) {
        $.ajax({
            url: 'Topic/DeleteNote',
            data: { noteId: id }
        }).done(function () {
            location.reload()
        })
    }
}

function GetHelp() {
    alert("This is a simple app, why don't you go and take a spin with it and see how it goes.")
}
