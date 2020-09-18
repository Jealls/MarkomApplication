
//Setting bahasa dan format tanggal datepicker
$(function () {
    $(".datepicker-here").datepicker({ language: 'en' });
    $('.datepicker-here').datepicker({ dateFormat: 'dd/mm/yyyy' }).val();
});


var ProgressHtml = '<div class="progress progress-striped active" style="margina-bottom: 0"><div class="progress-bar" style="width:100%"></div></div>';

//DROPDOWN CODE
$(function () { 
    $("#dropdown_code").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Company/AutoCompleteCompanyCode/',
                selectFirst: true,
                data: "{ 'prefix': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item.code;
                    }));
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#dropdown_company_id").val(i.item.id);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
    //}).focus(function () {
    //    $(this).autocomplete("search");
    //});
});

//DROPDOWN NAME
$(function () {
    $("#dropdown_name").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Company/AutoCompleteCompanyName/',
                selectFirst: true,
                data: "{ 'prefix': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item.name;
                    }));
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#dropdown_company_id").val(i.item.id);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
});


//MODAL ADD COMPANY SHOW
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


//SAVE ADD COMPANY
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

            var vjsName = $("#name").val();
            var vjsCode = $("#code").val();
            $(".modal_title_form").html("Edit Company - " + vjsName + " (" + vjsCode + ")");
        }
    });
});

//BTN SAVE MODAL EDIT
$(document).on("click", "#save_edit_company", function () {
    var vjsName = $("#name").val();
    validationName(vjsName);

    if (cekValidationCompany()) {
        $("#modal_confirm_up_company").modal("show");
    }
});

//CONFIRM SAVE MODAL EDIT
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



//MODAL VIEW SHOW
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

            var vjsName= $("#name").val();
            var vjsCode = $("#code").val();
            $(".modal_title_form").html("View Company - " + vjsName + " (" + vjsCode + ")");
        }
    });
});



//CONFIRM DELETE SHOW
$(document).on("click", "#btn_del_company", function () {

    var thisId = $(this).data('id');

    $("#modal_confirm_del_company").modal("show");
    $("#confirm_del_company").attr('data-id', thisId);
});

//BTN DELETE COMPANY
$(document).on("click", "#confirm_del_company", function () {
    debugger;
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


//BTN SEARCH
$(document).on("click", "#btn_search_company", function () {
    var vjsCode = $("#dropdown_code").val();
    var vjsName = $("#dropdown_name").val();
    var vjsCreatedDate = $("#created_date").val().split("/").reverse().join("-");
    var vjsCreatedBy = $("#created_by").val();
    debugger;
    var item = {
        code: vjsCode,
        name: vjsName,
        createDate2: vjsCreatedDate,
        createBy: vjsCreatedBy
    };

    $.ajax({
        type: 'POST',
        url: $("#btn_search_company").data('url'),
        data: {
           paramSearch: item
        },
        success: function (result) {
            alert("success");
        },
        error: function (result) {
            alert("error!");  // 
        }
    });
});




