function cancelSpecGridChanges() {
    $('#specializationsGrid').data("kendoGrid").cancelChanges();
    alert("Чтобы удалить специализацию, сначала удалите всех входящих в неё программистов!")
}

function cancelDepGrid() {
    $('#departmentsGrid').data("kendoGrid").cancelChanges();
    alert("Чтобы удалить отдел, сначала удалите всех входящих в него программистов!");
}

function closeProgrammer(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var selectedDate = getMonthAndYear();
    console.log(dataItem);
    if (confirm("Вы действительно хотите уволить разработчика " + dataItem.Name + "?")) {
        jQuery.ajax({
            type: "POST",
            url: '/Programmers/Close',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(dataItem),
            success: function (data) {
                console.log(data);
                $('#programmersGrid').data('kendoGrid').dataSource.read();
                $('#programmersGrid').data('kendoGrid').refresh();
            },
            failure: function (errMsg) {
                console.error(errMsg);
            }
        });
    }
}

function drawClosedProgrammers(e) {
    console.log("drawClosedProgrammers(e) starts");
    var rows = e.sender.tbody.children();

    for (var j = 0; j < rows.length; j++) {
        var row = $(rows[j]);
        var dataItem = e.sender.dataItem(row);

        if (dataItem.get("DeleteDate") != null) {
            row.addClass("closed");
            row.find(".k-grid-Close").remove();
        }
    }
}