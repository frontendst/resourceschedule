function refreshFilteringMultiSelects() {
    var scheduler = $("#scheduler").data("kendoScheduler");
    scheduler.dataSource.filter([]);
    scheduler.resources[0].dataSource.filter([]);

    scheduler.view(scheduler.view().name);
    $('#programmersmultiselect').data('kendoMultiSelect').value([]);
    $('#specializationsmultiselect').data('kendoMultiSelect').value([]);
    $('#tasksmultiselect').data('kendoMultiSelect').value([]);
    $('#departmentsmultiselect').data('kendoMultiSelect').value([]);
}

function closeFilteringWindow() {
    $('#filterwindow').data('kendoWindow').close();
}

function renderFilteringWindow() {
    $("body").append("<div id='filterwindow'><span>Projects:</span><input id='tasksmultiselect'></input><span>Programmers:</span><input id='programmersmultiselect'></input><span>Specializations:</span><input id='specializationsmultiselect'></input><span>Departments:</span><input id='departmentsmultiselect'></input></div>");

    $("#filterwindow").append("<br/>");
    $("#filterwindow").append("<input id='refreshButton' class='k-button control-but'onclick='refreshFilteringMultiSelects()' type='button' value='Сбросить всё'/>");
    $("#filterwindow").append("<input id='saveButton' class='k-button control-but' onclick='closeFilteringWindow()' type='button' value='Сохранить'/>");

    $("#filterwindow").kendoWindow({
        width: "600px",
        title: "Filtering",
        visible: false,
        actions: [
            "Minimize",
            "Close"
        ]
    });

    $('#filterwindow').parent().children().find('.k-link').last().bind('click', refreshFilteringMultiSelects);

    $("#tasksmultiselect").kendoMultiSelect({
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

    $("#programmersmultiselect").kendoMultiSelect({
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
            var programmerIds = this.value();
            var specIds = $('#specializationsmultiselect').data('kendoMultiSelect').value();
            var programerFilters = [];
            var specFilters = [];

            for (var programmerId in programmerIds) {
                programerFilters.push({ field: "ProgrammerViewModelID", value: programmerIds[programmerId] });
            }

            for (var specId in specIds) {
                specFilters.push({ field: "SpecializationID", value: specIds[specId] });
            }

            var filterSet;
            if (specFilters.length == 0 && programerFilters.length != 0) {
                filterSet = [{ logic: "or", filters: programerFilters }];
            }
            else if (specFilters.length != 0 && programerFilters.length == 0) {
                filterSet = [{ logic: "or", filters: specFilters }];
            }
            else if (specFilters.length != 0 && programerFilters.length != 0) {
                filterSet = [{ logic: "or", filters: specFilters }, { logic: "or", filters: programerFilters }];
            }
            else if (specFilters.length == 0 && programerFilters.length == 0) {
                filterSet = [];
            }

            var scheduler = $("#scheduler").data("kendoScheduler");
            $('#programmersmultiselect').data('kendoMultiSelect').dataSource.filter({ logic: 'or', filters: specFilters });
            scheduler.resources[0].dataSource.filter(filterSet);

            try {
                scheduler.view(scheduler.view().name);
            }
            catch (err) {
                filterSet = [];
                scheduler.resources[0].dataSource.filter(filterSet);
                scheduler.view(scheduler.view().name);
            }
        }
    });

    $("#specializationsmultiselect").kendoMultiSelect({
        dataTextField: "Name",
        dataValueField: "SpecializationViewModelID",
        dataSource: {
            transport: {
                read: {
                    url: "/Specializations/ReadForDropdown"
                }
            }
        },
        placeholder: "Select filter...",
        change: function (e) {
            var specIds = this.value();
            if (specIds.length != 0) {
                $("#departmentsmultiselect").data('kendoMultiSelect').enable(false);
                $("#departmentsmultiselect").data('kendoMultiSelect').value([]);
            }
            else {
                $("#specializationsmultiselect").data('kendoMultiSelect').enable();
            }
            var programmerIds = $('#programmersmultiselect').data('kendoMultiSelect').value();
            var specFilters = [];
            var programerFilters = [];

            for (var specId in specIds) {
                specFilters.push({ field: "SpecializationID", value: specIds[specId] });
            }

            for (var programmerId in programmerIds) {
                programerFilters.push({ field: "ProgrammerViewModelID", value: programmerIds[programmerId] });
            }

            var filterSet;
            if (specFilters.length == 0 && programerFilters.length != 0) {
                filterSet = [{ logic: "or", filters: programerFilters }];
            }
            else if (specFilters.length != 0 && programerFilters.length == 0) {
                filterSet = [{ logic: "or", filters: specFilters }];
            }
            else if (specFilters.length != 0 && programerFilters.length != 0) {
                filterSet = [{ logic: "or", filters: specFilters }, { logic: "or", filters: programerFilters }];
            }
            else if (specFilters.length == 0 && programerFilters.length == 0) {
                filterSet = [];
            }

            var scheduler = $("#scheduler").data("kendoScheduler");
            scheduler.resources[0].dataSource.filter(filterSet);
            $('#programmersmultiselect').data('kendoMultiSelect').dataSource.filter({ logic: 'or', filters: specFilters });
            scheduler.view(scheduler.view().name);

        }

    });

    $("#departmentsmultiselect").kendoMultiSelect({
        dataTextField: "Name",
        dataValueField: "DepartmentViewModelID",
        dataSource: {
            transport: {
                read: {
                    url: "/Departments/ReadForDropdown"
                }
            }
        },
        placeholder: "Select filter...",
        change: function (e) {
            var depIds = this.value();
            console.log(depIds);
            if (depIds.length != 0) {
                $("#specializationsmultiselect").data('kendoMultiSelect').enable(false);
                $("#specializationsmultiselect").data('kendoMultiSelect').value([]);
            }
            else {
                $("#specializationsmultiselect").data('kendoMultiSelect').enable();
            }
            var programmerIds = $('#programmersmultiselect').data('kendoMultiSelect').value();
            var depFilters = [];
            var programerFilters = [];

            for (var depId in depIds) {
                depFilters.push({ field: "DepartmentID", value: depIds[depId] });
            }

            for (var programmerId in programmerIds) {
                programerFilters.push({ field: "ProgrammerViewModelID", value: programmerIds[programmerId] });
            }

            var filterSet;
            if (depFilters.length == 0 && programerFilters.length != 0) {
                filterSet = [{ logic: "or", filters: programerFilters }];
            }
            else if (depFilters.length != 0 && programerFilters.length == 0) {
                filterSet = [{ logic: "or", filters: depFilters }];
            }
            else if (depFilters.length != 0 && programerFilters.length != 0) {
                filterSet = [{ logic: "or", filters: depFilters }, { logic: "or", filters: programerFilters }];
            }
            else if (depFilters.length == 0 && programerFilters.length == 0) {
                filterSet = [];
            }

            var scheduler = $("#scheduler").data("kendoScheduler");
            scheduler.resources[0].dataSource.filter(filterSet);
            $('#programmersmultiselect').data('kendoMultiSelect').dataSource.filter({ logic: 'or', filters: depFilters });
            scheduler.view(scheduler.view().name);

        }

    });
}