//Setting bahasa dan format tanggal datepicker
$(function () {
    $(".datepicker-here").datepicker({ language: 'en' });
    $('.datepicker-here').datepicker({ dateFormat: 'dd/mm/yyyy' }).val();
});

var ProgressHtml = '<div class="progress progress-striped active" style="margina-bottom: 0"><div class="progress-bar" style="width:100%"></div></div>';


//DROPDOWN COMPANY NAME
$(function () {
    $("#dropdown_employee_company").autocomplete({


    //    source: function (request, response) {
    //        $.ajax({
    //            type: "POST",
    //            url: '/Employee/AutoCompleteCompanyName/',
    //            dataType: "json",
    //            data: "{ 'data': '" + request.term + "' }",
    //            contentType: "application/json; charset=utf-8",
    //            success: function (data) {
    //                response($.map(data, function (item) {
    //                    return {
    //                        value: item.companyName,
    //                        companyName: item.companyName,
    //                        mCompanyId: item.mCompanyId
    //                    }
    //                }));
    //            }
    //        });
    //    },
    //    select: function (event, ui) {
    //        $('#dropdown_employee_company').val(ui.item.companyName);
    //        $('#dropdown_employee_company_id').val(ui.item.mCompanyId);
    //        return false;
    //    }
    //});

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
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#dropdown_employee_company_id").val(i.item.mCompanyId);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
});

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
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
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
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
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
        }
    });
});



//SAVE ADD EMPLOYEE
$(document).on("click", "#btn_save_employee", function () {
    var vjsEmpNumber = $("#code").val();
    var vjsFirstName = $("#firstName").val();
    var vjsLastName = $("#lastName").val();
    var vjsCompanyName = $("#companyName").val();
    var vjsEmail = $("#email").val();

    debugger;
    
    validationFirstName(vjsFirstName);
    validationEmpNumber(vjsEmpNumber);
    validationCompanyName(vjsCompanyName);

    if (cekValidationEmployee()) {
        var item = {
            code: vjsEmpNumber,
            firstName: vjsFirstName,
            lastName: vjsLastName,
            companyName: vjsCompanyName,
            email: vjsEmail
        };
        var j = 1;
        $.ajax({
            async: true,
            type: 'post',
            url: $("#btn_save_employee").data('url'),
            data: { paramAddEmployee: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form").modal("hide");

                    fcAlertSuccessAdd("#success_alert_add", result.latestCode);

                    setTimeout(function () {
                        window.location.reload();
                    }, 800);
                }
            }

        });
    }
});


//BTN SEARCH
$(document).on("click", "#btn_search_employee", function () {
    var vjsCode = $("#dropdown_code_employee").val();
    var vjsName = $("#dropdown_name_employee").val();
    var vjsComName = $("#dropdown_employee_company_id").val();
    var vjsCreatedDate = $("#created_date").val().split("/").join("-");
    var vjsCreatedBy = $("#created_by").val();
    debugger;
    var item = {
        code: vjsCode,
        name: vjsName,
        mCompanyId: vjsComName,
        createDate2: vjsCreatedDate,
        createBy: vjsCreatedBy
    };

    $.ajax({
        type: 'get',
        url: $("#btn_search_employee").data('url'),
        data: {
            paramSearch: item
        },
        success: function (result) {
            $("#tbl_list_employee").html(result);
        },
        error: function (result) {
            alert("error!");  // 
        }
    });
});





