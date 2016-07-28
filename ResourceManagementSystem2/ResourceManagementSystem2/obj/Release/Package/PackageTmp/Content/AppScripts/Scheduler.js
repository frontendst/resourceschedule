function getMonthAndYear() {
    var date = $("#scheduler").data("kendoScheduler").date();
    console.log({
        month: kendo.toString(date, 'MM'),
        year: kendo.toString(date, 'yyyy')
    });
    return {
        month: kendo.toString(date, 'MM'),
        year: kendo.toString(date, 'yyyy')
    };
}

function onNavigate(o) {
    var date = o.date;
    $("#scheduler").data("kendoScheduler").dataSource.transport.options.read.url = "/Tasks/Read";
    $("#scheduler").data("kendoScheduler").dataSource.read({
        month: kendo.toString(date, 'MM'),
        year: kendo.toString(date, 'yyyy')
    });
}

function onDataBound(workDaysAmount) {
    console.log("colours")
    console.log(workDaysAmount);
    var scheduler = $('#scheduler').data('kendoScheduler');
    var groups = scheduler.view().groups;
    for (var g in groups) {
        var days = 0;
        if ("_continuousEvents" in groups[g]) {
            var events = groups[g]._continuousEvents;
            for (var e in events) {
                var charge = scheduler.dataSource.getByUid(events[e].uid).Charge;
                days += (events[e].end.index - events[e].start.index + 1) * charge;
            }
        }

        var elements = $("[rowspan='1']");
        $(elements[g]).css({ transition: "0.4s" });
        $(elements[g]).css({ background: "" });
        $(elements[g]).find("span").remove();
        if (days > workDaysAmount) {
            $(elements[g]).append("<span style='font-size:12px'><br/>" + Math.ceil(days * 8) + "/" + (workDaysAmount * 8) + "(перегр.: " + Math.ceil((days - workDaysAmount) * 8) + " ч.)" + "</span>");
            $(elements[g]).css({ background: getColor(parseInt((days - workDaysAmount) * 8)) });
        }
        else {
            $(elements[g]).append("<span style='font-size:12px'><br/>" + (parseInt((days - 21) * 8) + 168) + "/" + (workDaysAmount * 8) + "</span>");
        }
    }
}


function drawWeekends() {
    console.log('drawWeekends starts!');
    var scheduler = $('#scheduler').data('kendoScheduler');
    var view = scheduler.view();
    var weekends = [];

    var weekendsAmount = 0;
    for (var a in view._dates) {
        if (view._dates[a].getDay() == 6 || view._dates[a].getDay() == 0) {
            weekendsAmount++;
        }
    }
    $.get('/Weekends/ReadForDrawing', function (data) {
        console.log('succes data get');
       
        data.forEach(function (item, i, arr) {
            weekends.push(kendo.toString(new Date(parseInt(item.Date.replace(/^\D+/g, ''))), 'd'));
        });
        if (view.table != null) {
            view.table.find("td[role='gridcell']").each(function () {
                if ($(this) != null) {
                    var element = $(this);
                    if (element != null) {
                        var slot = scheduler.slotByElement(element);
                        if (slot != null) {
                            var dateSlot = kendo.toString(slot.startDate, 'd');
                            if ($.inArray(dateSlot, weekends, 0) >= 0) {
                                element.addClass("holiday");
                            }
                        }
                    }
                }
            });
        }
        onDataBound(view._dates.length - weekends.length - weekendsAmount);
    });
}

function onClose() {
    console.log("onClose() starts");

    var scheduler = $('#scheduler').data('kendoScheduler');
    $.getJSON("/Programmers/ReadForScheduler", function (data) {
        scheduler.resources[0].dataSource.data(data);
        scheduler.view(scheduler.view().name);
    });
}

function onTaskAdd(e) {
    console.log("onTaskAdd() starts");
    var wnd = $('#addPopup').data("kendoWindow");
    var startDate = $('#addStartDate').data("kendoDatePicker");
    var endDate = $('#addEndDate').data("kendoDatePicker");
    var projects = $('#addProjects-dropdown').data("kendoDropDownList");
    var programmers = $('#addProgrammers-dropdown').data("kendoDropDownList");
    startDate.value(e.event.start);
    endDate.value(e.event.end);
    projects.dataSource.read();
    programmers.value(e.event.ProgrammerID);
    wnd.center().open();
    e.preventDefault();
}

function onTaskEdit(e) {
    console.log("onTaskEdit() starts");
    e.preventDefault();
    var scheduler = $('#scheduler').data('kendoScheduler');
    var uid = $(e.currentTarget).attr("data-uid");
    var item = scheduler.dataSource.getByUid(uid);
    console.log(item);
    var taskId = $('#editTaskId');
    var wnd = $('#editPopup').data("kendoWindow");
    var startDate = $('#editStartDate').data("kendoDatePicker");
    var endDate = $('#editEndDate').data("kendoDatePicker");
    var projects = $('#editProjects-dropdown').data("kendoDropDownList");
    var programmers = $('#editProgrammers-dropdown').data("kendoDropDownList");
    var charge = $('#editCharge').data('kendoNumericTextBox');
    taskId.val(item.TaskViewModelID);
    console.log(taskId.val());
    startDate.value(item.start);
    endDate.value(item.end);
    projects.dataSource.read();
    projects.value(item.ProjectID);
    programmers.value(item.ProgrammerID)
    charge.value(item.Charge);
    wnd.center().open();
}

function onTaskRemove(e) {
    console.log("onTaskRemove() starts");
    e.preventDefault();
    var scheduler = $('#scheduler').data('kendoScheduler');
    var uid = $(e.currentTarget).parent().parent().parent().attr('data-uid');
    var item = scheduler.dataSource.getByUid(uid);
    var taskId = $('#removeTaskId');
    taskId.val(item.TaskViewModelID);
    console.log(item);
    var wnd = $('#removePopUp').data('kendoWindow');
    wnd.center().open();
}

function refreshScheduler(e) {
    console.log('refreshScheduler')
    if (e != undefined)
        e.preventDefault();
    var scheduler = $('#scheduler').data('kendoScheduler');
    scheduler.refresh();
}

function setRemoveLabels() {
    $('.k-si-close').css('vertical-align', 'top');
}

function handleEditEvent(e) {
    console.log(e);
    e.preventDefault();
    console.log('onDataBound');
    setEvents();
    drawWeekends();
    setRemoveLabels();
}

function readSchedulerResources(request) {
    console.log("readSchedulerResources() starst");
    var scheduler = $("#scheduler").getKendoScheduler();
    request.success(scheduler.resources[0].dataSource.data().toJSON());
}

function setEvents() {
    $('.k-event').dblclick(function (event) {
        event.stopPropagation();
        onTaskEdit(event);
    });
    $('.k-si-close').click(function (event) {
        event.stopPropagation();
        onTaskRemove(event);
    });
}

function onRequestEnd(e) {
    console.log('onRequestEnd')
    var scheduler = $("#scheduler").getKendoScheduler();
    kendo.ui.progress(scheduler.element.find(".k-scheduler-content"), false);
}

function onRequestStart(e) {
    console.log('onRequestStart')
    var scheduler = $("#scheduler").getKendoScheduler();
       kendo.ui.progress(scheduler.element.find(".k-scheduler-content"), true);
}

