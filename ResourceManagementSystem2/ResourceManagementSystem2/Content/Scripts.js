
function onSync(request) {
    console.log("onSync() starts");
    var scheduler = $('#scheduler').data('kendoScheduler');
    scheduler.resources[0].dataSource.read();

    scheduler.dataSource.read();
    scheduler.view(scheduler.view().name);
}

function onChange() {
    console.log("onChange() starts");
    var scheduler = $('#scheduler').data('kendoScheduler');
    console.log(scheduler.view());
    scheduler.view(scheduler.view().name);
}

function onClose() {
    console.log("onClose() starts");
    var scheduler = $('#scheduler').data('kendoScheduler');
    scheduler.resources[0].dataSource.read();
    scheduler.dataSource.read();
    scheduler.view(scheduler.view().name);
}

function setTextBoxStyle () {
    console.log("setTextBoxStyle() starts");
    $("input[type='text']").addClass("k-textbox");
}

function onError(e) {
    var grid = $("#specializationGrid").data("kendoGrid");
    grid.dataSource.cancelChanges();
    alert("Удаление невозможно!");
}

function renderButtons() {
    console.log("renderButtons() starts");

    $(".k-scheduler-toolbar").append("<a id=\"logOffButton\" href=\"javascript:document.getElementById('logoutForm').submit()\" class=\"k-link\">Выйти</a>");

    $(".k-scheduler-toolbar").append("<a id=\"projectsButton\" class=\"k-link\">Проекты</a>");
    $("#projectsButton").bind('click', function () {
        $("#projectsWindow").data("kendoWindow").center().open();
    });

    $(".k-scheduler-toolbar").append("<a id=\"programmersButton\" class=\"k-link\">Разработчики</a>");
    $('#programmersButton').bind('click', function () {
        $("#programmersWindow").data("kendoWindow").center().open();
    });

    $(".k-scheduler-toolbar").append("<a id=\"specializationsButton\" class=\"k-link\">Специализации</a>");
    $('#specializationsButton').bind('click', function () {
        $("#specializationsWindow").data("kendoWindow").center().open();
    });
}

function readSchedulerResources(request) {
    console.log("readSchedulerResources() starst");
    var scheduler = $("#scheduler").getKendoScheduler();
    request.success(scheduler.resources[0].dataSource.data().toJSON());
}