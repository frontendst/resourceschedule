function renderAddPopUp() {
    console.log('renderAddPopUp() starts');
    var template = '<h4>SetUp Your Task</h4>                                              \
    <div class="edit-form">                                                                     \
        <div data-container-for="start" class="k-edit-field">                                   \
            <label>Start: <input id="addStartDate" name="start" /></label>                     \
        </div>                                                                                  \
        <div data-container-for="end" class="k-edit-field">                                     \
            <label> End:&nbsp;&nbsp;<input id="addEndDate" name="end" /></label>               \
        </div>                                                                                  \
        <div data-container-for="charge" class="k-edit-field">                                  \
            <label> Charge:&nbsp;&nbsp;<input id="addCharge" name="charge" value="1" /></label>        \
        </div>                                                                                  \
        <div data-container-for="proj" class="k-edit-field">                                    \
            <label>                                                                             \
        Project :                                                                               \
                <select id="addProjects-dropdown" name="proj"></select>                            \
            </label>                                                                            \
        </div>                                                                                 \
        <div data-container-for="programmers" class="k-edit-field">                             \
            <label>                                                                             \
        Programmer :                                                                            \
                <select id="addProgrammers-dropdown" name="programmers" disabled></select>         \
            </label>                                                                            \
        </div>                                                                                  \
        <div class="k-edit-field">                                                              \
            <input id="add-save-btn" class="k-button" type="button" onclick="addWindowSave()" value="Save">               \
            <input id="add-cancel-btn" class="k-button" type="button" onclick="addWindowClose()" value="Cancel">           \
        </div>                                                                                  \
    </div>';

    $('body').append('<div id="addPopup">' + template + '</div>');
    $('#addPopup').kendoWindow({
        width: "370px",
        title: "Add task",
        visible: false,
        actions: [
            "Minimize",
            "Close"
        ]
    });

    $("#addStartDate").kendoDatePicker();
    $("#addEndDate").kendoDatePicker();

    $("#addCharge").kendoNumericTextBox({
        format: "p0",
        min: 0.1,
        max: 1,
        step: 0.1
    });

    $('#addProjects-dropdown').kendoDropDownList({
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
    $('#addProgrammers-dropdown').kendoDropDownList({
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

function addWindowClose() {
    var wnd = $('#addPopup').data("kendoWindow");
    wnd.close();
}

function addWindowSave() {
    var scheduler = $("#scheduler").getKendoScheduler();
    var wnd = $('#addPopup').data("kendoWindow");
    var startDate = $('#addStartDate').data("kendoDatePicker").value();
    var endDate = $('#addEndDate').data("kendoDatePicker").value();
    var projectValue = $('#addProjects-dropdown').data("kendoDropDownList").value();
    var programmerValue = $('#addProgrammers-dropdown').data("kendoDropDownList").value();
    var charge = $('#addCharge').data('kendoNumericTextBox').value();
    addTask(0, programmerValue, projectValue, startDate, endDate, charge);
    wnd.close();
}

function addTask(taskViewModelID, programmerID, projectID, start, end, charge) {
    console.log('addTask() starts');
    var o = {
        TaskViewModelID: taskViewModelID,
        ProgrammerID: programmerID,
        ProjectID: projectID,
        Start: start,
        End: end,
        Charge: charge
    };

    var scheduler = $("#scheduler").getKendoScheduler();
    console.log(o);

    jQuery.ajax({
        type: "POST",
        url: '/Tasks/Create',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(o),
        success: function (data) {
            scheduler.dataSource.read();
            console.log('succes data post');
        },
        failure: function (errMsg) {
            console.error(errMsg);
        }
    });
}