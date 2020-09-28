//Setting bahasa dan format tanggal datepicker
$(function () {
    $(".datepicker-here").datepicker({ language: 'en' });
    $('.datepicker-here').datepicker({ dateFormat: 'dd/mm/yyyy' }).val();
});

var ProgressHtml = '<div class="progress progress-striped active" style="margina-bottom: 0"><div class="progress-bar" style="width:100%"></div></div>';



//DOCUMENT READY
$(document).ready(function () {

    $.ajax({
        type: 'post',
        url: $("#btn_search_prod").data('url'),
        success: function (result) {
            $("#tbl_list_prod").html(result);
        },
        error: function (result) {
            alert("error!");  // 
        }
    });

});

//MODAL ADD SHOW
$(document).on("click", "#btn_add_prod", function () {

    $("#modal_form_md").modal("show");
    $(".modal_title_form_md").html("Add Prod");
    $("#modal_body_md").html(ProgressHtml);


    $.ajax({
        url: $("#btn_add_prod").data('url'),
        type: 'get',
        success: function (result) {
            $("#modal_body_md").html(result);
        }
    });
});


//SAVE ADD 
$(document).on("click", "#btn_save_prod", function () {

    var vjsName = $("#name").val();
    var vjsDesc = $("#description").val();

    validationName();
    debugger;
    if (cekValidationProduct()) {
        var item = {
            name: vjsName,
            description: vjsDesc
        };

        $.ajax({
            async: true,
            type: 'post',
            url: $("#btn_save_prod").data('url'),
            data: { paramAddProd: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form_md").modal("hide");
                    var msg = "<strong>Data Saved!</strong> New product has been add with code " + result.latestCode.bold() + " !";
                    fcAlertBlue(msg);

                } else {
                    fcAlertRedNonReload(result.message);
                }
            }

        });
    }
});


//BTN SEARCH
$(document).on("click", "#btn_search_prod", function () {
    var vjsCode = $("#txt_prod_code").val();
    var vjsName = $("#txt_prod_name").val();
    var vjsDesc = $("#txt_prod_desc").val();
    var vjsCreatedDate = $("#created_date").val().split("/").reverse().join("-");
    var vjsCreatedBy = $("#created_by").val();
    debugger;
    var item = {
        code: vjsCode,
        name: vjsName,
        description: vjsDesc,
        createDate2: vjsCreatedDate,
        createBy: vjsCreatedBy
    };

    $.ajax({
        type: 'post',
        url: $("#btn_search_prod").data('url'),
        data: {
            paramSearch: item
        },
        success: function (result) {
            $("#tbl_list_prod").html(result);
        },
        error: function (result) {
            alert("error!");  // 
        }
    });
});


//BTN SHOW EDIT ROLE
$(document).on("click", "#btn_edit_prod", function () {

    $("#modal_form_md").modal("show");
    $(".modal_title_form_md").html("Edit Product - ");
    $("#modal_body_md").html(ProgressHtml);

    $.ajax({
        url: $("#btn_edit_prod").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_body_md").html(result);

            var vjsName = $("#name").val();
            var vjsCode = $("#code").val();
            $(".modal_title_form_md").html("Edit Product - " + vjsName + " (" + vjsCode + ")");
        }
    });
});

//SAVE EDIT ROLE
$(document).on("click", "#save_update_prod", function () {
    var vjsName = $("#name").val();
    var vjsDesc = $("#description").val();
    var vjsId = $("#id").val();

    debugger;
    validationName();

    if (cekValidationProduct()) {
        var item = {
            id: vjsId,
            description: vjsDesc,
            name: vjsName
        };

        $.ajax({
            async: true,
            type: 'post',
            url: $("#save_update_prod").data('url'),
            data: { paramEditProd: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form_md").modal("hide");

                    var msg = "<strong>Data Updated!</strong> Data Product has been updated !";
                    fcAlertBlue(msg);

                } else {
                    fcAlertRedNonReload(result.message);
                }
            }

        });
    }
});

//VIEW ROLE DETAIL
$(document).on("click", "#btn_view_prod", function () {

    $("#modal_form_md").modal("show");
    $(".modal_title_form_md").html("View Product - ");
    $("#modal_body_md").html(ProgressHtml);


    $.ajax({
        url: $("#btn_view_prod").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_body_md").html(result);

            var vjsName = $("#name").val();
            var vjsCode = $("#code").val();
            $(".modal_title_form_md").html("View Product - " + vjsName + " (" + vjsCode + ")");
        }
    });
});

//DELETE ROLE SHOW
$(document).on("click", "#btn_del_prod", function () {

    var thisId = $(this).data('id');

    $("#modal_confirm_del").modal("show");
    $("#confirm_del_data").attr('data-id', thisId);
});

//BTN DELETE ROLE
$(document).on("click", "#confirm_del_data", function () {
    var thi = $(this).data('id');
    debugger;
    $.ajax({
        type: 'POST',
        url: "/Product/DeleteDataProduct",
        dataType: 'json',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {

            if (result.success) {

                $("#modal_confirm_del").modal("hide");

                var msg = "<strong>Data Deleted!</strong> Data product with code " + result.latestCode.bold() + " has been deleted !";
                fcAlertBlue(msg);
            } else {
                fcAlertRedNonReload(result.message);
            }
        }
    });
});