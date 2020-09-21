//Setting bahasa dan format tanggal datepicker
$(function () {
    $(".datepicker-here").datepicker({ language: 'en' });
    $('.datepicker-here').datepicker({ dateFormat: 'dd/mm/yyyy' }).val();
});


var ProgressHtml = '<div class="progress progress-striped active" style="margina-bottom: 0"><div class="progress-bar" style="width:100%"></div></div>';

//MODAL ADD EVENT SHOW
$(document).on("click", "#btn_add_event", function () {

    $("#modal_form").modal("show");
    $(".modal_title_form").html("Add Event Request");
    $("#modal_content_body").html(ProgressHtml);


    $.ajax({
        url: $("#btn_add_event").data('url'),
        type: 'get',
        success: function (result) {
            $("#modal_content_body").html(result);
        }
    });
});
