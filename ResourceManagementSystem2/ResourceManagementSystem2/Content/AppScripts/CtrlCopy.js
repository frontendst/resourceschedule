var ctrlKey = false;
var finished = true;

$(document)
     .on("keydown", function (e) {
         ctrlKey = e.ctrlKey;
     })
     .on("keyup", function (e) {
         ctrlKey = e.ctrlKey;
     });

function onTaskCopy(e) {
    if (ctrlKey && finished) {
        finished = false;
            var newEvent = e.event.clone();
            var o = {
                TaskViewModelID: newEvent.TaskViewModelID,
                ProgrammerID: newEvent.ProgrammerID,
                ProjectID: newEvent.ProjectID,
                Start: newEvent.start,
                End: newEvent.end,
                Charge: newEvent.Charge
            }

            jQuery.ajax({
                type: "POST",
                url: '/Tasks/Create',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(o),
                success: function (data) {
                    $("#scheduler").data("kendoScheduler").dataSource.read();
                    console.log('succes data post');
                    finished = true;
                },
                failure: function (errMsg) {
                    console.error(errMsg);
                }
            });
            $("#scheduler").data("kendoScheduler").dataSource.read();
        }
    }

