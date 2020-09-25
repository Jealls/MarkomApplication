//Setting bahasa dan format tanggal datepicker
$(function () {
    $(".datepicker-here").datepicker({ language: 'en' });
    $('.datepicker-here').datepicker({ dateFormat: 'dd/mm/yyyy' }).val();
});


var ProgressHtml = '<div class="progress progress-striped active" style="margina-bottom: 0"><div class="progress-bar" style="width:100%"></div></div>';


//DOCUMENT READY
$(document).ready(function () {

    $.ajax({
        type: 'POST',
        url: $("#btn_search_event").data('url'),
        success: function (result) {
            $("#tbl_list_event").html(result);

        },
        error: function (result) {
            alert("error!");  // 
        }
    });

});


//MODAL ADD EVENT SHOW
$(document).on("click", "#btn_add_event", function () {

    $("#modal_form").modal("show");
    $(".modal_title_form").html("Add Event Request");
    $("#modal_content_body").html(ProgressHtml);


    $.ajax({
        url: $("#btn_add_event").data('url'),
        type: 'get',
        success: function (result) {
            $("#modal_content_body").html(result);

        }
    });
});

//SAVE ADD EVENT
$(document).on("click", "#btn_save_event", function () {
    var vjsName = $("#eventName").val();
    var vjsPlace = $("#place").val();
    var vjsStartDate = $("#startDate").val().split("/").reverse().join("-");
    var vjsEndDate = $("#endDate").val().split("/").reverse().join("-");
    var vjsBudget = parseInt($("#budget").val());
    var vjsReqBy = $("#reqBy").val();
    var vjsReqDate = $("#reqDate").val().split("/").reverse().join("-");
    var vjsNote = $("#note").val();

    debugger;
    validationName(vjsName);
    validationPlace(vjsPlace);
    validationStartDate(vjsStartDate);
    validationEndDate(vjsEndDate);
    validationBudget(vjsBudget);
    validationReqBy(vjsReqBy);
    validationReqDate(vjsReqDate);

    if (cekValidationEvent()) {

        var item = {
            eventName: vjsName
            , place: vjsPlace
            , startDate: vjsStartDate
            , endDate: vjsEndDate
            , budget: vjsBudget
            , requestBy: vjsReqBy
            , requestDate: vjsReqDate
            , note: vjsNote
        };

        $.ajax({
            async: true,
            type: 'post',
            url: $("#btn_save_event").data('url'),
            data: { paramAddEvent: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form").modal("hide");

                    var msg = "<strong>Data Saved!</strong> Transaction event request has been add with code " + result.latestCode.bold() + " !";
                    fcAlertBlue(msg);

                } else {
                    fcAlertRedNonReload(result.message);
                }
            },
            error: function (result) {
                fcAlertRed(result.message);
            }

        });
    }
});



//BTN SEARCH
$(document).on("click", "#btn_search_event", function () {
    var vjsCode = $("#trans_event_code").val();
    var vjsReqName = $("#req_by").val();
    var vjsReqDate = $("#req_date").val().split("/").reverse().join("-");
    var vjsStatus = $("#req_status").val();
    var vjsCreatedDate = $("#created_date").val().split("/").reverse().join("-");
    var vjsCreatedBy = $("#created_by").val();
    debugger;
    var item = {
        code: vjsCode,
        requestByName: vjsReqName,
        requestDate2: vjsReqDate,
        statusName: vjsStatus,
        createDate: vjsCreatedDate,
        createBy: vjsCreatedBy
    };

    $.ajax({
        type: 'post',
        url: $("#btn_search_event").data('url'),
        data: {
            paramSearch: item
        },
        success: function (result) {
            $("#tbl_list_event").html(result);
        },
        error: function (result) {
            alert(result.message);  // 
        }
    });
});



//BTN SHOW EDIT 
$(document).on("click", "#btn_edit_event", function () {

    $("#modal_form").modal("show");
    $(".modal_title_form").html("Edit Event Request - ");
    $("#modal_content_body").html(ProgressHtml);

    $.ajax({
        url: $("#btn_edit_event").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_content_body").html(result);

            var vjsCode = $("#code").val();
            $(".modal_title_form").html("Edit Event Request - " + vjsCode );
        }
    });
});

//SAVE EDIT EVENT
$(document).on("click", "#save_update_event", function () {
    var vjsName = $("#eventName").val();
    var vjsPlace = $("#place").val();
    var vjsStartDate = $("#startDate").val().split("/").reverse().join("-");
    var vjsEndDate = $("#endDate").val().split("/").reverse().join("-");
    var vjsBudget = parseInt($("#budget").val());
    var vjsReqBy = $("#requestBy").val();
    var vjsReqDate = $("#requestDate").val();
    var vjsNote = $("#note").val();
    var vjsStatus = $("#status").val();
    var vjsId = $("#id").val();

     debugger;
    if (vjsStatus == 1) {

        validationName(vjsName);
        validationPlace(vjsPlace);
        validationStartDate(vjsStartDate);
        validationEndDate(vjsEndDate);
        validationBudget(vjsBudget);
        validationReqBy(vjsReqBy);
        validationReqDate(vjsReqDate);

        if (cekValidationEvent()) {

            var item = {
                id: vjsId
                ,eventName: vjsName
                , place: vjsPlace
                , startDate: vjsStartDate
                , endDate: vjsEndDate
                , budget: vjsBudget
                , note: vjsNote
            };

            $.ajax({
                async: true,
                type: 'post',
                url: $("#save_update_event").data('url'),
                data: { paramEditEv: item },
                success: function (result) {
                    if (result.success) {

                        $("#modal_form").modal("hide");

                        var msg = "<strong>Data Updated!</strong> Transaction event request with code " + result.latestCode.bold() + " has been updated !";
                        fcAlertBlue(msg);

                    } else {
                        fcAlertRedNonReload(result.message);
                    }
                }

            });
        }
    } else {
        $("#modal_form").modal("hide");

        fcAlertRed("Data tidak bisa di edit karena telah di approve oleh administator!");
    }
});



//BTN SHOW APPROVAL OR CLOSE
$(document).on("click", "#btn_choose_event", function () {

    $("#modal_form").modal("show");
    $(".modal_title_form").html("Approval Event Request - ");
    $("#modal_content_body").html(ProgressHtml);
    var g = $(this).data('id');
    debugger;
    $.ajax({
        url: $("#btn_choose_event").data('url'),
        type: 'get',
        data: {
            paramId: $(this).data('id')
        },
        success: function (result) {
            $("#modal_content_body").html(result);

            var vjsCode = $("#code").val();
            $(".modal_title_form").html("Approval Event Request - " + vjsCode );
        }
    });
});

//-----------------------IN APPROVAL MODAL--------------------

//BTN EVENT APPROVAL
$(document).on("click", "#btn_approve_event", function () {
    var vjsId = $("#id").val();
    var vjsAssignTo = $("#assignTo").val();

    validationAssignTo(vjsAssignTo);

    debugger;
    if (cekValidationApproveEvent()) {
        var item = {
            id: vjsId,
            assignTo: vjsAssignTo
        };


        $.ajax({
            async: true,
            type: 'post',
            url: $("#btn_approve_event").data('url'),
            data: { paramAppEv: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_form").modal("hide");

                    var msg = "<strong>Data Approved!</strong> Transaction event request with code" + result.latestCode.bold() + " has been approved !";
                    fcAlertBlue(msg);
                }
            }
        });
    }
});


//BTN EVENT REJECT
$(document).on("click", "#btn_reject_event", function () {

    var thisId = $("#id").val();

    $("#modal_confirm_reject").modal("show");
    $("#confirm_reject").attr('data-id', thisId);
});

//CONFIRM REJECT
$(document).on("click", "#confirm_reject", function () {

    var vjsRejectReason = $("#txt_reject_reason").val();
    debugger;

    validationRejectReason(vjsRejectReason);

    if (cekValidationRejectEvent()) {

        var item = {
            id: $(this).data('id'),
            rejectReason: vjsRejectReason
        };

        $.ajax({
            async: true,
            type: 'post',
            url: "/Event/RejectDataEvent",
            data: { paramRejectEv: item },
            success: function (result) {
                if (result.success) {

                    $("#modal_confirm_reject").modal("hide");
                    $("#modal_form").modal("hide");


                    var msg = "<strong>Data Rejected!</strong> Transaction event request with code" + result.latestCode.bold() + " is rejected by Administator!";
                    fcAlertRed(msg);
                } else {
                    fcAlertRedNonReload(result.message);
                }
            }
        });
    }
});


//-----------------------IN MODAL CLOSE REQUEST---------------------------

//BTN EVENT CLOSE REQUEST
$(document).on("click", "#close_req_event", function () {
    var item = {
        id: $("#id").val()
    };

        $.ajax({
            async: true,
            type: 'post',
            url: $("#close_req_event").data('url'),
            data: {
                paramCloseEv: item
            },
            success: function (result) {
                if (result.success) {

                    $("#modal_form").modal("hide");

                    var msg = "<strong>Data Closed!</strong> Transaction event request with code" + result.latestCode.bold() + " has been close request !";
                    fcAlertBlue(msg);

                } else {
                    fcAlertRedNonReload(result.message);
                }
            }
        });
});
