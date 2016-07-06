
function onSync(request) {
    console.log("onSync() starts");
    var scheduler = $('#scheduler').data('kendoScheduler');
    //scheduler.resources[0].dataSource.read();
    //scheduler.dataSource.read();
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
            //$.getJSON("/Programmers/ReadForScheduler", function (data) {
            //    scheduler.resources[0].dataSource.data(data);
            //    scheduler.view(scheduler.view().name);
            //});
            //console.log(programmers);
            //console.log($("#programmersGrid").data("kendoGrid").dataSource.view());


        }
    }
}


function onDataBound() {

    var scheduler = $('#scheduler').data('kendoScheduler');
    var groups = scheduler.view().groups;
    for (var g in groups) {
        var days = 0;
        if ("_continuousEvents" in groups[g]) {

            var events = groups[g]._continuousEvents;
            for (var e in events) {
                days += events[e].end.index - events[e].start.index + 1;
            }
        }

        var scheduler = $("#scheduler").data("kendoScheduler");
        var view = scheduler.view();
        var elements = $("[rowspan='1']");
        $(elements[g]).css({ transition: "0.4s" });

        $(elements[g]).css({ background: "" });
        $(elements[g]).find("span").remove();
        if (days > 21) {
            $(elements[g]).append("<span style='font-size:13px'>  +Перегрузка: " + ((days - 21) * 8) + " часов</span>");
        } else {

        }

        if (days > 25) {
            $(elements[g]).css({ background: "#ff4108" });
        } else if (days > 23) {
            $(elements[g]).css({ background: "#ffb308" });
        }
        else if (days > 21) {
            $(elements[g]).css({ background: "#f9fd08" });
        }

        //console.log(g + ": " + days * 8);
    }

}

function onClose() {
    console.log("onClose() starts");
    var scheduler = $('#scheduler').data('kendoScheduler');
    scheduler.resources[0].dataSource.read();
    scheduler.dataSource.read();
    scheduler.view(scheduler.view().name);
}

function setTextBoxStyle() {
    console.log("setTextBoxStyle() starts");
    $("input[type='text']").addClass("k-textbox");
}

function onError(e) {
    var grid = $("#specializationGrid").data("kendoGrid");
    grid.dataSource.cancelChanges();
    alert("Удаление невозможно!");
}

function submitLogout() {
    javascript: document.getElementById('logoutForm').submit();
}


function renderButtons() {
    console.log("renderButtons() starts");

    $(".k-scheduler-toolbar").append("<input id='logOffButton' class='k-button control-but' onclick='submitLogout()' type='button' value='Выйти' />");

    $(".k-scheduler-toolbar").append("<input id='projectsButton' class='k-button control-but' type='button' value='Проекты' />");
    $("#projectsButton").bind('click', function () {
        $("#projectsWindow").data("kendoWindow").center().open();
    });

    $(".k-scheduler-toolbar").append("<input id='programmersButton' class='k-button control-but' type='button' value='Разработчики' />");
    $('#programmersButton').bind('click', function () {
        $("#programmersWindow").data("kendoWindow").center().open();
    });

    $(".k-scheduler-toolbar").append("<input id='specializationsButton' class='k-button control-but' type='button' value='Специализации' />");
    $('#specializationsButton').bind('click', function () {
        $("#specializationsWindow").data("kendoWindow").center().open();
    });

    $(".k-scheduler-toolbar").append("<input id='filteringButton' class='k-button control-but' type='button' value='Фильтрация' />");
    $('#filteringButton').bind('click', function () {
        $("#filterwindow").data("kendoWindow").center().open();
    });
}

function readSchedulerResources(request) {
    console.log("readSchedulerResources() starst");
    var scheduler = $("#scheduler").getKendoScheduler();
    request.success(scheduler.resources[0].dataSource.data().toJSON());
}

