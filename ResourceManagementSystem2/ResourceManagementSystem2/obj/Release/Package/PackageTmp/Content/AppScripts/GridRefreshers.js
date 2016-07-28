function cancelSpecGridChanges() {
    $('#specializationsGrid').data("kendoGrid").cancelChanges();
    alert("Чтобы удалить специализацию, сначала удалите всех входящих в неё программистов!")
}

function cancelDepGrid() {
    $('#departmentsGrid').data("kendoGrid").cancelChanges();
    alert("Чтобы удалить отдел, сначала удалите всех входящих в него программистов!");
}
