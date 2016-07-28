function renderRemovePopUp() {
    console.log('renderRemovePopUp() starts');

    var editorTemplate = '\
    <div class="center-block">\
       <h4>Are U sure?</h4>\
       <input id="removeTaskId" hidden>\
    </div>\
    <div>\
        <input type="button" class="k-button standart-btn left-btn" onclick="removeWindowYes()" value="Yes"/>\
        <input type="button" class="k-button standart-btn right-btn" onclick="removeWindowNo()" value="No"/>\
    </div>';

    $('body').append('<div id="removePopUp">' + editorTemplate + '</div>');

    $('#removePopUp').kendoWindow({
        width: "370px",
        title: "Remove task?",
        visible: false,
        actions: [
            "Close"
        ]
    });
}

function removeWindowNo() {
    $('#removePopUp').data('kendoWindow').close();
}

function removeWindowYes() {
    var wnd = $('#removePopUp').data("kendoWindow");
    var taskId = $('#removeTaskId').val();
    removeTask(taskId);
    wnd.close();
}

function removeTask(taskViewModelID) {
    console.log('removeTask() starts');
    var o = { TaskViewModelID: taskViewModelID };
    var scheduler = $("#scheduler").getKendoScheduler();
    console.log(o);

    jQuery.ajax({
        type: "POST",
        url: '/Tasks/Destroy',
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