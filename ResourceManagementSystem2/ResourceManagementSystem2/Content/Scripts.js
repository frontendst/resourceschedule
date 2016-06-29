
function onSync(request) {
    console.log("onSync() starts");
    var scheduler = $('#scheduler').data('kendoScheduler');
    scheduler.resources[0].dataSource.read();
    scheduler.dataSource.read();
    scheduler.view(scheduler.view().name);
}

//function onChange(data) {
//    console.log("onChange() starts");
//    var scheduler = $('#scheduler').data('kendoScheduler');
//    console.log(scheduler.view());
//    scheduler.view(scheduler.view().name);
//}

function onChange(data) {//фикс ми рефреш мультиселектов
    console.log("onChange() starts");
    if ("action" in data) {
        if (data.action == "sync") {
            var scheduler = $('#scheduler').data('kendoScheduler');
            scheduler.resources[0].dataSource.data(data.items);
            scheduler.view(scheduler.view().name);
        }
    }
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

    $(".k-scheduler-toolbar").append("<a id=\"filteringButton\" class=\"k-link\">Фильтрация</a>");
    $('#filteringButton').bind('click', function () {
        $("#filterwindow").data("kendoWindow").center().open();
    });



    $("body").append("<div id='filterwindow'><span>Projects:</span><input id=\"projectsmultiselect\"></input><span>Programmers:</span><input id=\"programmerssmultiselect\"></input></div>");

    $("#filterwindow").kendoWindow({
        width: "600px",
        title: "Filtering",
        visible: false,
        actions: [
            "Minimize",
            "Close"
        ]
    });


    $("#projectsmultiselect").kendoMultiSelect({
        dataTextField: "Title",
        dataValueField: "ProjectViewModelID",
        dataSource: {
            transport: {
                read: {
                    url: "/Scheduler/GetProjectColors"
                }
            }

        },
        placeholder: "Select filter...",
        change: function (e) {
            var projectsId = this.value();
            var filters = [];
            for (var projectId in projectsId) {
                filters.push({ field: "ProjectID", value: projectsId[projectId] });

            }
            $("#scheduler").data("kendoScheduler").dataSource.filter({ logic: "or", filters: filters });

        }
    });

    $("#programmerssmultiselect").kendoMultiSelect({
        dataTextField: "Name",
        dataValueField: "ProgrammerViewModelID",
        dataSource: {
            transport: {
                read: {
                    url: "/Programmers/ReadForScheduler"
                }
            }

        },
        placeholder: "Select filter...",
        change: function (e) {
            var programmersId = this.value();
            var filters = [];
            filters = [];
            for (var programmerId in programmersId) {
                filters.push({ field: "ProgrammerViewModelID", value: programmersId[programmerId] });
            }
            var scheduler = $("#scheduler").data("kendoScheduler");
            scheduler.resources[0].dataSource.filter({ logic: "or", filters: filters });
            scheduler.view(scheduler.view().name);
        }
    });
}

function readSchedulerResources(request) {
    console.log("readSchedulerResources() starst");
    var scheduler = $("#scheduler").getKendoScheduler();
    request.success(scheduler.resources[0].dataSource.data().toJSON());
}

