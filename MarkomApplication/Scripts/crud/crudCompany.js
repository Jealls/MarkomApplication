
//Setting bahasa dan format tanggal datepicker
$(function () {
    $(".datepicker-here").datepicker({ language: 'en' });
    $('.datepicker-here').datepicker({ dateFormat: 'dd/mm/yyyy' }).val();
});


var ProgressHtml = '<div class="progress progress-striped active" style="margina-bottom: 0"><div class="progress-bar" style="width:100%"></div></div>';



//klik tombol add company /modal add show
$(document).on("click", "#btn_add_company", function () {

    $("#modal_form").modal("show");
    $(".modal_title_form").html("Add Company");
    $("#modal_content_body").html(ProgressHtml);


    $.ajax({
        url: $("#btn_add_company").data('url'),
        type: 'get',
        success: function (result) {
            $("#modal_content_body").html(result);
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
    if (cekValidationCompany()) {
        $.ajax({
            async: true,
            type: 'post',
            url: $("#btn_save_company").data('url'),
            data: { paramAddCompany:item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form").modal("hide");

                    fcAlertSuccessAdd("#success_alert_add", result.latestCode);

                    setTimeout(function () {
                         window.location.reload();
                    }, 800);
                    //toastr.info("Data Saved! New company has been add with code " + $(this).attr("data-id"));
                } 
            }

        });
    }
});



// SHOW MODAL EDIT
$(document).on("click", "#btn_edit_company", function () {

    $("#modal_form").modal("show");
    $(".modal_title_form").html("Edit Company - ");
    $("#modal_content_body").html(ProgressHtml);

    $.ajax({
        url: $("#btn_edit_company").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_content_body").html(result);
        }
    });
});

//klik tombol save modal EDIT
$(document).on("click", "#save_edit_company", function () {
    var vjsName = $("#name").val();
    validationName(vjsName);

    if (cekValidationCompany()) {
        $("#modal_confirm_up_company").modal("show");
    }
});


//Confirm and submit update data company
$(document).on("click", "#confirm_update_company", function () {

    var vjsName = $("#name").val();
    var vjsEmail = $("#email").val();
    var vjsPhone = $("#phone").val();
    var vjsAddress = $("#address").val();
    var vjsId = $("#id").val();
    debugger;
    var item = {
        id: vjsId,
        name: vjsName,
        email: vjsEmail,
        phone: vjsPhone,
        address: vjsAddress
    };

     $.ajax({
         async: true,
         type: 'post',
         url: $("#confirm_update_company").data('url'),
         data: { paramEditCompany: item },
         success: function (result) {
             if (result.success) {

                 $("#modal_confirm_up_company").modal("hide");
                 $("#modal_form").modal("hide");

                 fcAlertSuccessEdit("#success_alert_update");

                 setTimeout(function () {
                     window.location.reload();
                 }, 800);
             }
         }
     });
});

//klik tombol VIEW company /modal VIEW SHOW
$(document).on("click", "#btn_view_company", function () {

    $("#modal_form").modal("show");
    $(".modal_title_form").html("View Company - ");
    $("#modal_content_body").html(ProgressHtml);


    $.ajax({
        url: $("#btn_view_company").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_content_body").html(result);
        }
    });
});


//klik tombol DELETE
$(document).on("click", "#btn_del_company", function () {

    var thisId = $(this).data('id');

    $("#modal_confirm_del_company").modal("show");
    $("#confirm_del_company").attr('data-id', thisId);
});

$(document).on("click", "#confirm_del_company", function () {

    $.ajax({
        type: 'POST',
        url: $("#confirm_del_company").data('url'),
        dataType: 'json',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_confirm_del_company").modal("hide");
            fcAlertSuccessDelete("#success_alert_del", result.latestCode);

            setTimeout(function () {
                window.location.reload();
            }, 800);
        }
    });
});




