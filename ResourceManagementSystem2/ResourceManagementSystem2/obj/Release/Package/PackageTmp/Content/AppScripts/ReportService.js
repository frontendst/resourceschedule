function submitReport()
{
    var scheduler = $('#scheduler').data('kendoScheduler');
    year = scheduler.view()._startDate.getFullYear();
    window.location.href = "/Report?year=" + year;
}