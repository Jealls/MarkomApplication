
//Setting bahasa dan format tanggal datepicker
$(function () {
    $(".datepicker-here").datepicker({ language: 'en' });
    $('.datepicker-here').datepicker({ dateFormat: 'dd/mm/yyyy' }).val();
});


var ProgressHtml = '<div class="progress progress-striped active" style="margina-bottom: 0"><div class="progress-bar" style="width:100%"></div></div>';



//klik tombol add company /modal add show
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


//klik tombol show /save submit
$(document).on("click", "#btn_save_company", function () {
    var vjsName = $("#name").val();
    var vjsEmail = $("#email").val();
    var vjsPhone = $("#phone").val();
    var vjsAddress = $("#address").val();

    validationName(vjsName);

    var item = {
        name: vjsName,
        email: vjsEmail,
        phone: vjsPhone,
        address: vjsAddress
    };

    $.ajax({
        async: true,
        type: 'post',
        url: $("#btn_save_company").data('url'),
        data: { paramAddCompany:item },
        success: function (result) {
            if (result.success) {

                $("#modal_add_form").modal("hide");

                fcAlertSuccessAdd("#success_alert_add", result.latestCode);

                setTimeout(function () {
                     window.location.reload();
                }, 2000);
                //toastr.info("Data Saved! New company has been add with code " + $(this).attr("data-id"));
            } 
        }

    });
});