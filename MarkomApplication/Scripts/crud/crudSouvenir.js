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
        url: $("#btn_search_sou").data('url'),
        success: function (result) {
            $("#tbl_list_sou").html(result);
        },
        error: function (result) {
            alert("error!");  // 
        }
    });

});



//DROPDOWN NAME
$(function () {
    $("#dropdown_name_sou").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Souvenir/AutoCompleteSouvenirName/',
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
            $("#dropdown_name_sou_id").val(i.item.id);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
});

//DROPDOWN CODE
$(function () {
    $("#dropdown_code_sou").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Souvenir/AutoCompleteSouvenirCode/',
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
            $("#dropdown_code_sou_id").val(ui.item.id);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
});

//DROPDOWN UNIT


$(function () {
    $("#dropdown_unit_sou").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Souvenir/AutoCompleteUnitName/',
                selectFirst: true,
                data: "{ 'prefix': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            value: item.unitName,
                            unitName: item.unitName,
                            mUnitId: item.mUnitId
                        };
                    }));
                }
            });
        },
        select: function (e, ui) {
            $("#dropdown_unit_sou_id").val(ui.item.mUnitId);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
});

//MODAL ADD  SHOW
$(document).on("click", "#btn_add_sou", function () {

    $("#modal_form").modal("show");
    $(".modal_title_form").html("Add Souvenir");
    $("#modal_content_body").html(ProgressHtml);


    $.ajax({
        url: $("#btn_add_sou").data('url'),
        type: 'get',
        success: function (result) {
            $("#modal_content_body").html(result);


            $(function () {
                $("#unitName").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '/Souvenir/AutoCompleteUnitName/',
                            selectFirst: true,
                            data: "{ 'prefix': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data, function (item) {
                                    return {
                                        value: item.unitName,
                                        unitName: item.unitName,
                                        mUnitId: item.mUnitId
                                    };
                                }));
                            }
                        });
                    },
                    select: function (e, ui) {
                        $("#mUnitId").val(ui.item.mUnitId);
                    },
                    minLength: 1
                }).focus(function (event, ui) {
                    $(".ui-helper-hidden-accessible").hide();
                    event.preventDefault();
                });
            });


        }
    });
});


//SAVE ADD 
$(document).on("click", "#btn_save_sou", function () {

    var vjsName = $("#name").val();
    var vjsUnitId = parseInt($("#mUnitId").val());
    var vjsDesc = $("#description").val();
    debugger;
    validationName();
    validationIdUnit();

    if (cekValidationSouvenir()) {
        var item = {
            name: vjsName,
            mUnitId: vjsUnitId,
            description: vjsDesc
        };

        $.ajax({
            async: true,
            type: 'post',
            url: $("#btn_save_sou").data('url'),
            data: { paramAddSou: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form").modal("hide");
                    var msg = "<strong>Data Saved!</strong> New souvenir has been add with code " + result.latestCode.bold() + " !";
                    fcAlertBlue(msg);

                } else {
                    fcAlertRedNonReload(result.message);
                }
            }

        });
    }
});

//BTN SEARCH
$(document).on("click", "#btn_search_sou", function () {
    var vjsCode = $("#dropdown_code_sou").val();
    var vjsName = $("#dropdown_name_sou").val();
    var vjsUnitId = $("#dropdown_unit_sou_id").val();
    var vjsCreatedDate = $("#created_date").val().split("/").reverse().join("-");
    var vjsCreatedBy = $("#created_by").val();

    if ($("#dropdown_unit_sou").val() == "") {
        vjsUnitId = 0;
    }
    debugger;
    var item = {
        code: vjsCode,
        name: vjsName,
        mUnitId: vjsUnitId,
        createDate2: vjsCreatedDate,
        createBy: vjsCreatedBy
    };

    $.ajax({
        type: 'post',
        url: $("#btn_search_sou").data('url'),
        data: {
            paramSearch: item
        },
        success: function (result) {
            $("#tbl_list_sou").html(result);
        },
        error: function (result) {
            alert("error!");  // 
        }
    });
});

//BTN SHOW EDIT
$(document).on("click", "#btn_edit_sou", function () {

    $("#modal_form").modal("show");
    $(".modal_title_form").html("Edit Souvenir");
    $("#modal_content_body").html(ProgressHtml);

    $.ajax({
        url: $("#btn_edit_sou").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_content_body").html(result);

            var vjsName = $("#name").val();
            var vjsCode = $("#code").val();
            $(".modal_title_form").html("Edit Souvenir - " + vjsName + " (" + vjsCode + ")");



            $(function () {
                $("#unitName").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '/Souvenir/AutoCompleteUnitName/',
                            selectFirst: true,
                            data: "{ 'prefix': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data, function (item) {
                                    return {
                                        value: item.unitName,
                                        unitName: item.unitName,
                                        mUnitId: item.mUnitId
                                    };
                                }));
                            }
                        });
                    },
                    select: function (e, ui) {
                        $("#mUnitId").val(ui.item.mUnitId);
                    },
                    minLength: 1
                }).focus(function (event, ui) {
                    $(".ui-helper-hidden-accessible").hide();
                    event.preventDefault();
                });
            });


        }
    });
});

//SAVE EDIT 
$(document).on("click", "#save_update_sou", function () {
    var vjsName = $("#name").val();
    var vjsUnitId = parseInt($("#mUnitId").val());
    var vjsDesc = $("#description").val();
    var vjsId = $("#id").val();

    debugger;
    validationName();
    validationIdUnit();

    if (cekValidationSouvenir()) {
        var item = {
            id: vjsId,
            mUnitId: vjsUnitId,
            description: vjsDesc,
            name: vjsName
        };

        $.ajax({
            async: true,
            type: 'post',
            url: $("#save_update_sou").data('url'),
            data: { paramEditSou: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form").modal("hide");

                    var msg = "<strong>Data Updated!</strong> Data Souvenir has been updated !";
                    fcAlertBlue(msg);

                } else {
                    fcAlertRedNonReload(result.message);
                }
            }

        });
    }
});

//VIEW DETAIL
$(document).on("click", "#btn_view_sou", function () {


    $("#modal_form").modal("show");
    $(".modal_title_form").html("Add Souvenir");
    $("#modal_content_body").html(ProgressHtml);


    $.ajax({
        url: $("#btn_view_sou").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_content_body").html(result);

            var vjsName = $("#name").val();
            var vjsCode = $("#code").val();
            $(".modal_title_form").html("View Souvenir - " + vjsName + " (" + vjsCode + ")");
        }
    });
});

//DELETE  SHOW
$(document).on("click", "#btn_del_sou", function () {

    var thisId = $(this).data('id');

    $("#modal_confirm_del").modal("show");
    $("#confirm_del_data").attr('data-id', thisId);
});

//BTN DELETE 
$(document).on("click", "#confirm_del_data", function () {
    $.ajax({
        type: 'POST',
        url: "/Souvenir/DeleteDataSouvenir",
        dataType: 'json',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {

            if (result.success) {

                $("#modal_confirm_del").modal("hide");

                var msg = "<strong>Data Deleted!</strong> Data souvenir with code " + result.latestCode.bold() + " has been deleted !";
                fcAlertBlue(msg);
            } else {
                fcAlertRedNonReload(result.message);
            }
        }
    });
});