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
        url: $("#btn_search_unit").data('url'),
        success: function (result) {
            $("#tbl_list_unit").html(result);
        },
        error: function (result) {
            alert("error!");  // 
        }
    });

});


//DROPDOWN NAME
$(function () {
    $("#dropdown_name_unit").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Unit/AutoCompleteUnitName/',
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
                    alert("tidak ada data!");
                    //alert(response.responseText);
                },
                failure: function (response) {
                    alert("tidak ada data!");
                    //alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#dropdown_name_unit_id").val(i.item.id);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
});

//DROPDOWN CODE
$(function () {
    $("#dropdown_code_unit").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Unit/AutoCompleteUnitCode/',
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
                    alert("tidak ada data!");
                    //alert(response.responseText);
                },
                failure: function (response) {
                    alert("tidak ada data!");
                    //alert(response.responseText);
                }
            });
        },
        select: function (e, ui) {
            $("#dropdown_code_unit_id").val(ui.item.id);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
});

//MODAL ADD SHOW
$(document).on("click", "#btn_add_unit", function () {

    $("#modal_form_md").modal("show");
    $(".modal_title_form_md").html("Add Unit");
    $("#modal_body_md").html(ProgressHtml);


    $.ajax({
        url: $("#btn_add_unit").data('url'),
        type: 'get',
        success: function (result) {
            $("#modal_body_md").html(result);
        }
    });
});


//SAVE ADD 
$(document).on("click", "#btn_save_unit", function () {

    var vjsName = $("#name").val();
    var vjsDesc = $("#description").val();

    validationName();

    if (cekValidationUnit()) {
        var item = {
            name: vjsName,
            description: vjsDesc
        };

        $.ajax({
            async: true,
            type: 'post',
            url: $("#btn_save_unit").data('url'),
            data: { paramAddUnit: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form_md").modal("hide");
                    var msg = "<strong>Data Saved!</strong> New unit has been add with code " + result.latestCode.bold() + " !";
                    fcAlertBlue(msg);

                } else {
                    fcAlertRedNonReload(result.message);
                }
            }

        });
    }
});



//BTN SEARCH
$(document).on("click", "#btn_search_unit", function () {
    var vjsCode = $("#dropdown_code_unit").val();
    var vjsName = $("#dropdown_name_unit").val();
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
        type: 'post',
        url: $("#btn_search_unit").data('url'),
        data: {
            paramSearch: item
        },
        success: function (result) {
            $("#tbl_list_unit").html(result);
        },
        error: function (result) {
            alert("error!");  // 
        }
    });
});

//BTN SHOW EDIT
$(document).on("click", "#btn_edit_unit", function () {

    $("#modal_form_md").modal("show");
    $(".modal_title_form_md").html("Edit Role - ");
    $("#modal_body_md").html(ProgressHtml);

    $.ajax({
        url: $("#btn_edit_unit").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_body_md").html(result);

            var vjsName = $("#name").val();
            var vjsCode = $("#code").val();
            $(".modal_title_form_md").html("Edit Role - " + vjsName + " (" + vjsCode + ")");
        }
    });
});

//SAVE EDIT
$(document).on("click", "#save_update_unit", function () {
    var vjsName = $("#name").val();
    var vjsDesc = $("#description").val();
    var vjsId = $("#id").val();

    debugger;
    validationName();

    if (cekValidationUnit()) {
        var item = {
            id: vjsId,
            description: vjsDesc,
            name: vjsName
        };

        $.ajax({
            async: true,
            type: 'post',
            url: $("#save_update_unit").data('url'),
            data: { paramEditUnit: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form_md").modal("hide");

                    var msg = "<strong>Data Updated!</strong> Data Role has been updated !";
                    fcAlertBlue(msg);

                } else {
                    fcAlertRedNonReload(result.message);
                }
            }

        });
    }
});

//VIEW DETAIL
$(document).on("click", "#btn_view_unit", function () {

    $("#modal_form_md").modal("show");
    $(".modal_title_form_md").html("View Unit - ");
    $("#modal_body_md").html(ProgressHtml);


    $.ajax({
        url: $("#btn_view_unit").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_body_md").html(result);

            var vjsName = $("#name").val();
            var vjsCode = $("#code").val();
            $(".modal_title_form_md").html("View Unit - " + vjsName + " (" + vjsCode + ")");
        }
    });
});

//DELETE  SHOW
$(document).on("click", "#btn_del_unit", function () {

    var thisId = $(this).data('id');

    $("#modal_confirm_del").modal("show");
    $("#confirm_del_data").attr('data-id', thisId);
});

//BTN DELETE
$(document).on("click", "#confirm_del_data", function () {
    var thi = $(this).data('id');
    debugger;
    $.ajax({
        type: 'POST',
        url: "/Unit/DeleteDataUnit",
        dataType: 'json',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {

            if (result.success) {

                $("#modal_confirm_del").modal("hide");

                var msg = "<strong>Data Deleted!</strong> Data unit with code " + result.latestCode.bold() + " has been deleted !";
                fcAlertBlue(msg);
            } else {
                fcAlertRedNonReload(result.message);
            }
        }
    });
});