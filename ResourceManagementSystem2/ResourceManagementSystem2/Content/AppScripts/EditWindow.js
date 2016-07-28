function renderEditPopUp() {
    console.log('renderEditPopUp() starts');
    var editorTemplate = '<h4>Edit Your Task</h4>                                               \
    <input id="editTaskId" hidden>\
    <div class="edit-form">                                                                     \
        <div data-container-for="start" class="k-edit-field">                                   \
            <label>Start: <input id="editStartDate" name="start" /></label>                     \
        </div>                                                                                  \
        <div data-container-for="end" class="k-edit-field">                                     \
            <label> End:&nbsp;&nbsp;<input id="editEndDate" name="end" /></label>               \
        </div>                                                                                  \
        <div data-container-for="charge" class="k-edit-field">                                  \
            <label> Charge:&nbsp;&nbsp;<input id="editCharge" name="charge" /></label>        \
        </div>                                                                                  \
        <div data-container-for="proj" class="k-edit-field">                                    \
            <label>                                                                             \
        Project :                                                                               \
                <select id="editProjects-dropdown" name="proj"></select>                        \
            </label>                                                                            \
        </div>                                                                                  \
        <div data-container-for="programmers" class="k-edit-field">                             \
            <label>                                                                             \
        Programmer :                                                                            \
                <select id="editProgrammers-dropdown" name="programmers" disabled></select>     \
            </label>                                                                            \
        </div>                                                                                  \
        <div class="k-edit-field">                                                              \
            <input id="edit-update-btn" class="k-button" type="button" onclick="editWindowUpdate()" value="Update">               \
            <input id="edit-cancel-btn" class="k-button" type="button" onclick="editWindowClose()" value="Cancel">           \
        </div>                                                                                  \
    </div>';

    $('body').append('<div id="editPopup">' + editorTemplate + '</div>');
    $('#editPopup').kendoWindow({
        width: "370px",
        title: "Edit task",
        visible: false,
        actions: [
            "Minimize",
            "Close"
        ]
    });

    $("#editStartDate").kendoDatePicker();
    $("#editEndDate").kendoDatePicker();

    $('#editCharge').kendoNumericTextBox({
        format: "p0",
        min: 0.1,
        max: 1,
        step: 0.1
    });

    $('#editProjects-dropdown').kendoDropDownList({
        dataTextField: "Title",
        dataValueField: "ProjectViewModelID",
        dataSource: {
            type: "json",
            serverFiltering: true,
            transport: {
                read: "/Scheduler/GetProjectColors"
            }
        }
    });
    $('#editProgrammers-dropdown').kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "ProgrammerViewModelID",
        dataSource: {
            type: "json",
            serverFiltering: true,
            transport: {
                read: "/Programmers/ReadForScheduler"
            }
        }
    });
}


function editWindowUpdate() {
    var scheduler = $("#scheduler").getKendoScheduler();
    var wnd = $('#editPopup').data("kendoWindow");
    var taskId = $('#editTaskId').val();
    var startDate = $('#editStartDate').data("kendoDatePicker").value();
    var endDate = $('#editEndDate').data("kendoDatePicker").value();
    var projectValue = $('#editProjects-dropdown').data("kendoDropDownList").value();
    var programmerValue = $('#editProgrammers-dropdown').data("kendoDropDownList").value();
    var charge = $('#editCharge').data('kendoNumericTextBox').value();
    editTask(taskId, programmerValue, projectValue, startDate, endDate, charge);
    wnd.close();
}


function editWindowClose() {
    var wnd = $('#editPopup').data("kendoWindow");
    wnd.close();
}

function editTask(taskViewModelID, programmerID, projectID, start, end, charge) {
    console.log('editTask() starts');

    var o = {
        TaskViewModelID: taskViewModelID,
        ProgrammerID: programmerID,
        ProjectID: projectID,
        Start: start,
        End: end,
        Charge : charge
    };

    var scheduler = $("#scheduler").getKendoScheduler();
    console.log(o);
    jQuery.ajax({
        type: "POST",
        url: '/Tasks/Update',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(o),
        success: function (data) {
            scheduler.dataSource.read();
        },
        failure: function (errMsg) {
            console.error(errMsg);
        }
    });
}