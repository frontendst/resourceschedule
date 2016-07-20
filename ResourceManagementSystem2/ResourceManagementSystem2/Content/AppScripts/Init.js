function setTextBoxStyle() {
    console.log("setTextBoxStyle() starts");
    $('#Name').addClass("k-textbox");
    $('#Title').addClass("k-textbox");
    $('#Description').addClass("k-textbox");
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

    $(".k-scheduler-toolbar").append("<input id='weekendsButton' class='k-button control-but' type='button' value='Выходные' />");
    $('#weekendsButton').bind('click', function () {
        $("#weekendsWindow").data("kendoWindow").center().open();
    });

}

