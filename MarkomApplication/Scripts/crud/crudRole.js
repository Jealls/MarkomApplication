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
        url: $("#btn_search_role").data('url'),
        success: function (result) {
            $("#tbl_list_role").html(result);
        },
        error: function (result) {
            alert("error!");  // 
        }
    });

});



//DROPDOWN ROLE NAME
$(function () {
    $("#dropdown_name_role").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Role/AutoCompleteRoleName/',
                selectFirst: true,
                data: "{ 'prefix': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item.fullName;
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
            $("#dropdown_name_role_id").val(i.item.id);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
});

//DROPDOWN ROLE CODE
$(function () {
    $("#dropdown_code_role").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Role/AutoCompleteRoleCode/',
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
            $("#dropdown_code_role_id").val(ui.item.id);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
});



//MODAL ADD EMPLOYEE SHOW
$(document).on("click", "#btn_add_role", function () {

    $("#modal_form_md").modal("show");
    $(".modal_title_form_md").html("Add Role");
    $("#modal_body_md").html(ProgressHtml);


    $.ajax({
        url: $("#btn_add_role").data('url'),
        type: 'get',
        success: function (result) {
            $("#modal_body_md").html(result);
        }
    });
});


//SAVE ADD EMPLOYEE
$(document).on("click", "#btn_save_role", function () {

    var vjsName = $("#name").val();
    var vjsDesc = $("#description").val(); 

    validationName();
    
    if (cekValidationRole()) {
        var item = {
            name: vjsName,
            description: vjsDesc
        };

        $.ajax({
            async: true,
            type: 'post',
            url: $("#btn_save_role").data('url'),
            data: { paramAddRole: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form_md").modal("hide");
                    var msg = "<strong>Data Saved!</strong> New role has been add with code " + result.latestCode.bold() + " !";
                    fcAlertBlue(msg);

                } else {
                    fcAlertRed(result.message);
                }
            }

        });
    }
});

//BTN SEARCH
$(document).on("click", "#btn_search_role", function () {
    var vjsCode = $("#dropdown_code_role").val();
    var vjsName = $("#dropdown_name_role").val();
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
        url: $("#btn_search_role").data('url'),
        data: {
            paramSearch: item
        },
        success: function (result) {
            $("#tbl_list_role").html(result);
        },
        error: function (result) {
            alert("error!");  // 
        }
    });
});

//BTN SHOW EDIT ROLE
$(document).on("click", "#btn_edit_role", function () {

    $("#modal_form_md").modal("show");
    $(".modal_title_form_md").html("Edit Role - ");
    $("#modal_body_md").html(ProgressHtml);

    $.ajax({
        url: $("#btn_edit_role").data('url'),
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

//SAVE EDIT EMPLOYEE
$(document).on("click", "#save_update_role", function () {
    var vjsName = $("#name").val();
    var vjsDesc = $("#description").val();
    var vjsId = $("#id").val();

    debugger;
    validationName();

    if (cekValidationRole()) {
        var item = {
            id: vjsId,
            description: vjsDesc,
            name: vjsName
        };

        $.ajax({
            async: true,
            type: 'post',
            url: $("#save_update_role").data('url'),
            data: { paramEditRole: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form_md").modal("hide");

                    var msg = "<strong>Data Updated!</strong> Data Role has been updated !";
                    fcAlertBlue(msg);

                } else {
                    fcAlertRed(result.message);
                }
            }

        });
    }
});

//VIEW EMPLOYE DETAIL
$(document).on("click", "#btn_view_role", function () {

    $("#modal_form_md").modal("show");
    $(".modal_title_form_md").html("View Role - ");
    $("#modal_body_md").html(ProgressHtml);


    $.ajax({
        url: $("#btn_view_role").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_body_md").html(result);

            var vjsName = $("#name").val();
            var vjsCode = $("#code").val();
            $(".modal_title_form_md").html("View Role - " + vjsName + " (" + vjsCode + ")");
        }
    });
});

//DELETE ROLE SHOW
$(document).on("click", "#btn_del_role", function () {

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
        url: "/Role/DeleteDataRole",
        dataType: 'json',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_confirm_del").modal("hide");

            var msg = "<strong>Data Deleted!</strong> Data role with code " + result.latestCode.bold() + " has been deleted !";
            fcAlertBlue(msg);
        }
    });
});