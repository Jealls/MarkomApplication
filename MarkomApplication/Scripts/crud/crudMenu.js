
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
        url: $("#btn_search_menu").data('url'),
        success: function (result) {
            $("#tbl_list_menu").html(result);
        },
        error: function (result) {
            alert("error!");  // 
        }
    });

});


//DROPDOWN CODE
$(function () {
    $("#dropdown_code_menu").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Menu/AutoCompleteMenuCode/',
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
            $("#dropdown_code_menu_id").val(i.item.id);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
});

//DROPDOWN NAME
$(function () {
    $("#dropdown_name_menu").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Menu/AutoCompleteMenuName/',
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
            $("#dropdown_name_menu_id").val(i.item.id);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
});




//MODAL ADD MENU SHOW
$(document).on("click", "#btn_add_menu", function () {

    $("#modal_form_md").modal("show");
    $(".modal_title_form_md").html("Add Menu");
    $("#modal_body_md").html(ProgressHtml);


    $.ajax({
        url: $("#btn_add_menu").data('url'),
        type: 'get',
        success: function (result) {
            $("#modal_body_md").html(result);
        }
    });
});

//SAVE ADD 
$(document).on("click", "#btn_save_menu", function () {

    var vjsName = $("#name").val();
    var vjsController = $("#controller").val();
    var vjsParent = parseInt($("#parentName").val()) ? parseInt($("#parentName").val()) : 0;
    debugger;

    validationName(vjsName);
    validationController(vjsController);

    if (cekValidationMenu()) {

        var item = {
            name: vjsName,
            controller: vjsController,
            parentId: vjsParent
        };

        $.ajax({
            async: true,
            type: 'post',
            url: $("#btn_save_menu").data('url'),
            data: { paramAddMenu: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form_md").modal("hide");
                    var msg = "<strong>Data Saved!</strong> New menu has been add with code " + result.latestCode.bold() + " !";
                    fcAlertBlue(msg);

                } else {
                    fcAlertRedNonReload(result.message);
                }
            }

        });
    }
});


//BTN SEARCH
$(document).on("click", "#btn_search_menu", function () {
    var vjsCode = $("#dropdown_code_menu").val();
    var vjsName = $("#dropdown_name_menu").val();
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
        url: $("#btn_search_menu").data('url'),
        data: {
            paramSearch: item
        },
        success: function (result) {
            $("#tbl_list_menu").html(result);
        },
        error: function (result) {
            alert("error!");  // 
        }
    });
});



//BTN SHOW EDIT ROLE
$(document).on("click", "#btn_edit_menu", function () {

    $("#modal_form_md").modal("show");
    $(".modal_title_form_md").html("Edit Menu - ");
    $("#modal_body_md").html(ProgressHtml);

    $.ajax({
        url: $("#btn_edit_menu").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_body_md").html(result);

            var vjsName = $("#name").val();
            var vjsCode = $("#code").val();
            $(".modal_title_form_md").html("Edit Menu - " + vjsName + " (" + vjsCode + ")");
        }
    });
});

//SAVE EDIT ROLE
$(document).on("click", "#save_update_menu", function () {
    var vjsName = $("#name").val();
    var vjsController = $("#controller").val();
    var vjsParent = parseInt($("#parentId").val()) ? parseInt($("#parentId").val()) : 0;
    var vjsId = $("#id").val();
    debugger;

    validationName(vjsName);
    validationController(vjsController);


    if (cekValidationMenu()) {
        var item = {
            id: vjsId,
            name: vjsName,
            controller: vjsController,
            parentId: vjsParent
        };

        $.ajax({
            async: true,
            type: 'post',
            url: $("#save_update_menu").data('url'),
            data: { paramEditMenu: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form_md").modal("hide");

                    var msg = "<strong>Data Updated!</strong> Data Menu has been updated !";
                    fcAlertBlue(msg);

                } else {
                    fcAlertRedNonReload(result.message);
                }
            }

        });
    }
});



//VIEW MENU DETAIL
$(document).on("click", "#btn_view_menu", function () {

    $("#modal_form_md").modal("show");
    $(".modal_title_form_md").html("View Menu - ");
    $("#modal_body_md").html(ProgressHtml);


    $.ajax({
        url: $("#btn_view_menu").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_body_md").html(result);

            var vjsName = $("#name").val();
            var vjsCode = $("#code").val();
            $(".modal_title_form_md").html("View Menu - " + vjsName + " (" + vjsCode + ")");
        }
    });
});

//DELETE ROLE SHOW
$(document).on("click", "#btn_del_menu", function () {

    var thisId = $(this).data('id');

    $("#modal_confirm_del").modal("show");
    $("#confirm_del_data").attr('data-id', thisId);
});

//BTN DELETE ROLE
$(document).on("click", "#confirm_del_data", function () {

    $.ajax({
        type: 'POST',
        url: "/Menu/DeleteDataMenu",
        dataType: 'json',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            if (result.success) {
                $("#modal_confirm_del").modal("hide");

                var msg = "<strong>Data Deleted!</strong> Data menu with code " + result.latestCode.bold() + " has been deleted !";
                fcAlertBlue(msg);

            } else {
                fcAlertRedNonReload(result.message);
            }
        }
    });
});

