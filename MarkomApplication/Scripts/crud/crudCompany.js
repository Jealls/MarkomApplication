$(function () {
    $(".datepicker-here").datepicker({ language: 'en' });
    $('.datepicker-here').datepicker({ dateFormat: 'dd/mm/yyyy' }).val();
});


var ProgressHtml = '<div class="progress progress-striped active" style="margina-bottom: 0"><div class="progress-bar" style="width:100%"></div></div>';


$(document).on("click", "#btn_add_company", function () {

    $("#modal_add_form").modal("show");
    $(".modal-title").html("Add Company");
    $("#modal_content_add").html(ProgressHtml);


    $.ajax({
        url: $("#btn_add_company").data('url'),
        type: 'get',
        success: function (result) {
            $("#modal_content_add").html(result);
        }
    });
});