jQuery(function () {
    Inbox();
    /*$('#myModal').on('shown.bs.modal', function () {

    });*/
    $(document).on("click", "#idpageList a[href]", getPage);
});

function Inbox() {
    $("#loader").html('<div class="progress"><div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100" style="width: 100%"></div></div>');
    $("#msg").html('<div class="alert alert-info" role="alert">Cargagando ...</div>');
    var options = {
        url: "/Home/Inbox",
        type: "POST",
        data: { "page": 1}
    };
    $.ajax(options).done(function (data) {
        $("#tblinbox").html(data);
        $("#loader").html('');
        $("#msg").html('');
    });
    /*$.ajax(options).fail(function (data) {
        $("#tblinbox").html(data);
        $("#loader").html('');
        $("#msg").html('');
    });*/
}
$(document).on('click', '.trinbox', function (event) {
    //alert("Entra");
    var MessageId = $(this).attr("key");
    $('#myModal .modal-title').html('Mensaje');
    $('#myModal .modal-body').html('<div class="progress"><div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100" style="width: 100%"></div></div>'
        + '<div class="alert alert-info" role="alert">Cargagando ...</div>');
    $('#myModal .modal-footer').html('<button class="btn btn-default" type="button" data-dismiss="modal">Close</button><button class="btn btn-primary" type= "button" > Enviar</button>');
    $("#myModal .modal-dialog").css("width", "90%");
    var request = $.ajax({
        url: "/Home/Mensaje",
        method: "POST",
        data: { MessageId: MessageId },
        //dataType: "html"
    });
    request.done(function (msg) {
        //$("#log").html(msg);
        $('#myModal .modal-body').html(msg);
    });

    request.fail(function (jqXHR, textStatus) {
        $('#myModal .modal-body').html("<div class='alert alert-danger'><button class='close' data-dismiss='alert' type='button'>&times;</button><p class='youhave'>" + jqXHR.type + "<br>" + jqXHR.status + "<br>" + jqXHR.status + "<br>" + textStatus + "</p></div>");
        //alert("Request failed: " + textStatus);
    });
    $('#myModal').modal('show');
    /*$('#myModal').on('shown.bs.modal', function () {
    })*/
});