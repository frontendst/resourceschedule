
function onSyncProjectsColors(request) {
    console.log("onSyncProjectsColors() starts");
    var scheduler = $('#scheduler').data('kendoScheduler');
    scheduler.dataSource.read();
    scheduler.view(scheduler.view().name);
}
