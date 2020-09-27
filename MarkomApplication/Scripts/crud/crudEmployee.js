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
        url: $("#btn_search_employee").data('url'),
        success: function (result) {
            $("#tbl_list_employee").html(result);
        },
        error: function (result) {
            alert("error!");  // 
        }
    });

});

//DROPDOWN COMPANY NAME


//DROPDOWN EMPLOYEE NAME
$(function () {
    $("#dropdown_name_employee").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Employee/AutoCompleteEmployeeName/',
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
            $("#dropdown_name_employee_id").val(i.item.id);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
});

//DROPDOWN EMPLOYEE CODE
$(function () {
    $("#dropdown_code_employee").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Employee/AutoCompleteEmployeeNumber/',
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
            $("#dropdown_code_employee_id").val(ui.item.id);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
});



//MODAL ADD EMPLOYEE SHOW
$(document).on("click", "#btn_add_employee", function () {

    $("#modal_form").modal("show");
    $(".modal_title_form").html("Add Employee");
    $("#modal_content_body").html(ProgressHtml);


    $.ajax({
        url: $("#btn_add_employee").data('url'),
        type: 'get',
        success: function (result) {
            $("#modal_content_body").html(result);


            $(function () {
                $("#companyName").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '/Employee/AutoCompleteCompanyName/',
                            selectFirst: true,
                            data: "{ 'prefix': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data, function (item) {
                                    return {
                                        value: item.companyName,
                                        companyName: item.companyName,
                                        mCompanyId: item.mCompanyId
                                    };

                                }));
                            },
                            error: function (response) {
                                alert("tidak ada data!");
                            },
                            failure: function (response) {
                                alert("tidak ada data!");
                            }
                        });
                    },
                    select: function (e, i) {
                        $("#mCompanyId").val(i.item.mCompanyId);
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

//SAVE ADD EMPLOYEE
$(document).on("click", "#btn_save_employee", function () {
    var vjsEmpNumber = $("#code").val();
    var vjsFirstName = $("#firstName").val();
    var vjsLastName = $("#lastName").val();
    debugger;
    var vjsCompanyId = parseInt($("#mCompanyId").val());
    var vjsEmail = $("#email").val();

    
    validationFirstName(vjsFirstName);
    validationEmpNumber(vjsEmpNumber);
    validationCompanyName(vjsCompanyId);

    if (cekValidationEmployee()) {
        var item = {
            code: vjsEmpNumber,
            firstName: vjsFirstName,
            lastName: vjsLastName,
            mCompanyId: vjsCompanyId,
            email: vjsEmail
        };

        $.ajax({
            async: true,
            type: 'post',
            url: $("#btn_save_employee").data('url'),
            data: { paramAddEmployee: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form").modal("hide");

                    var msg = "<strong>Data Saved!</strong> New employee has been add with employee ID " + result.latestCode.bold() + " !";
                    fcAlertBlue(msg);

                } else {
                    fcAlertRedNonReload(result.message);
                }
            }

        });
    }
});



//BTN SEARCH
$(document).on("click", "#btn_search_employee", function () {
    var vjsCode = $("#dropdown_code_employee").val();
    var vjsName = $("#dropdown_name_employee").val();
    var vjsComId = $("#dropdown_employee_company_id").val();
    var vjsCreatedDate = $("#created_date").val().split("/").reverse().join("-");
    var vjsCreatedBy = $("#created_by").val();
    debugger;
    var item = {
        code: vjsCode,
        fullName: vjsName,
        mCompanyId: vjsComId,
        createDate2: vjsCreatedDate,
        createBy: vjsCreatedBy
    };

    $.ajax({
        type: 'post',
        url: $("#btn_search_employee").data('url'),
        data: {
            //paramSearch: item
                    code: vjsCode,
                    fullName: vjsName,
                    mCompanyId: vjsComId,
                    createDate2: vjsCreatedDate,
                    createBy: vjsCreatedBy
        },
        success: function (result) {
            $("#tbl_list_employee").html(result);
        },
        error: function (result) {
            alert("error!");  // 
        }
    });
});



//BTN SHOW EDIT EMPLOYEE
$(document).on("click", "#btn_edit_employee", function () {

    $("#modal_form").modal("show");
    $(".modal_title_form").html("Edit Employee - ");
    $("#modal_content_body").html(ProgressHtml);

    $.ajax({
        url: $("#btn_edit_employee").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_content_body").html(result);

            var vjsName = $("#firstName").val() + " " + $("#lastName").val();
            var vjsCode = $("#code").val();
            $(".modal_title_form").html("Edit Employee - " + vjsName + " (" + vjsCode + ")");



            $(function () {
                $("#companyName").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '/Employee/AutoCompleteCompanyName/',
                            selectFirst: true,
                            data: "{ 'prefix': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data, function (item) {
                                    return {
                                        value: item.companyName,
                                        companyName: item.companyName,
                                        mCompanyId: item.mCompanyId
                                    };

                                }));
                            },
                            error: function (response) {
                                alert("tidak ada data!");
                            },
                            failure: function (response) {
                                alert("tidak ada data!");
                            }
                        });
                    },
                    select: function (e, i) {
                        $("#mCompanyId").val(i.item.mCompanyId);
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

//SAVE EDIT EMPLOYEE
$(document).on("click", "#save_update_employee", function () {
    var vjsEmpNumber = $("#code").val();
    var vjsFirstName = $("#firstName").val();
    var vjsLastName = $("#lastName").val();
    var vjsCompanyId = parseInt($("#mCompanyId").val());
    var vjsEmail = $("#email").val();
    var vjsId = $("#id").val();

    debugger;

    validationFirstName(vjsFirstName);
    validationEmpNumber(vjsEmpNumber);
    validationCompanyName(vjsCompanyId);

    if (cekValidationEmployee()) {
        var item = {
            id: vjsId,
            code: vjsEmpNumber,
            firstName: vjsFirstName,
            lastName: vjsLastName,
            mCompanyId: vjsCompanyId,
            email: vjsEmail
        };

        $.ajax({
            async: true,
            type: 'post',
            url: $("#save_update_employee").data('url'),
            data: { paramEditEmp: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form").modal("hide");

                    var msg = "<strong>Data Updated!</strong> Data Employee has been updated !";
                    fcAlertBlue(msg);

                } else {
                    fcAlertRedNonReload(result.message);
                }
            }

        });
    }
});



//VIEW EMPLOYE DETAIL
$(document).on("click", "#btn_view_employee", function () {

    $("#modal_form").modal("show");
    $(".modal_title_form").html("View Employee - ");
    $("#modal_content_body").html(ProgressHtml);


    $.ajax({
        url: $("#btn_view_employee").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_content_body").html(result);

            var vjsName = $("#firstName").val() + " " + $("#lastName").val();
            var vjsCode = $("#code").val();
            $(".modal_title_form").html("View Employee - " + vjsName + " (" + vjsCode + ")");
        }
    });
});



//DELETE EMPLOYEE SHOW
$(document).on("click", "#btn_del_employee", function () {

    var thisId = $(this).data('id');

    $("#modal_confirm_del").modal("show");
    $("#confirm_del_data").attr('data-id', thisId);
});

//BTN DELETE EMPLOYEE
$(document).on("click", "#confirm_del_data", function () {
    debugger;
    $.ajax({
        type: 'POST',
        url: "/Employee/DeleteDataEmployee",
        dataType: 'json',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            if (result.success) {

                $("#modal_confirm_del").modal("hide");

                var msg = "<strong>Data Deleted!</strong> Data employee with Employee ID Number " + result.latestCode.bold() + " has been deleted !";
                fcAlertBlue(msg);

            } else {
                fcAlertRedNonReload(result.message);
            }
        }
    });
});






