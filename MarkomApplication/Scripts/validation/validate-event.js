
//EVENT NAME
function validationName(Name) {;
    if (Name == "") {
        $("#eventName").addClass("invalid");
    } else {
        $("#eventName").removeClass("invalid");
    }
}

function validationEName() {
    var vjsName = document.getElementById("eventName").value;
    if (vjsName == "") {
        $("#eventName").addClass("invalid");
    } else {
        $("#eventName").removeClass("invalid");
    }
}


//PLACE
function validationPlace(Place) {

    if (Place == "") {
        $("#place").addClass("invalid");
    } else {
        $("#place").removeClass("invalid");
    }
}

function validationEPlace() {
    var vjsPlace = document.getElementById("place").value;
    if (vjsPlace == "") {
        $("#place").addClass("invalid");
    } else {
        $("#place").removeClass("invalid");
    }
}


function matchDate(date) {
    const reg = /^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}?$/;
    return reg.test(date);
}

//START DATE
function validationStartDate(Date) {

    if (Date == "") {
        $("#startDate").addClass("invalid");
    } else {
        $("#startDate").removeClass("invalid");
    }
}

function validationEStartDate() {
    var vjsDate = document.getElementById("startDate").value;
    if (vjsDate == "" || !matchDate(vjsDate)) {
        $("#startDate").addClass("invalid");
    } else {
        $("#startDate").removeClass("invalid");
    }
}


//END DATE
function validationEndDate(Date) {

    if (Date == "") {
        $("#endDate").addClass("invalid");
    } else {
        $("#endDate").removeClass("invalid");
    }
}

function validationEEndDate() {
    var vjsDate = document.getElementById("endDate").value;
    if (vjsDate == "" || !matchDate(vjsDate)) {
        $("#endDate").addClass("invalid");
    } else {
        $("#endDate").removeClass("invalid");
    }
}


//Budget
function validationBudget(budget) {

    if (isNaN(budget)) {
        $("#budget").addClass("invalid");
    } else {
        $("#budget").removeClass("invalid");
    }
}

function validationEBudget() {
    var vjsBudget = parseInt(document.getElementById("budget").value);
    debugger;

    if (isNaN(vjsBudget)) {
        $("#budget").addClass("invalid");
    } else {
        $("#budget").removeClass("invalid");
    }
}


//REQUEST BY
function validationReqBy(vjsReqBy) {
    if (vjsReqBy == "") {
        $("#reqBy").addClass("invalid");
    } else {
        $("#reqBy").removeClass("invalid");
    }
}


//REQUEST DATE
function validationReqDate(reqDate) {

    if (reqDate == "") {
        $("#reqDate").addClass("invalid");
    } else {
        $("#reqDate").removeClass("invalid");
    }
}



//ASSIGN TO
function validationAssignTo(assignTo) {

    if (assignTo == "") {
        $("#AssignTo").addClass("invalid");
    } else {
        $("#AssignTo").removeClass("invalid");
    }
}

//REJECT REASON
function validationRejectReason(rejectReason) {

    if (rejectReason == "") {
        $("#txt_reject_reason").addClass("invalid");
    } else {
        $("#txt_reject_reason").removeClass("invalid");
    }
}




//CEK VALIDATION EVENT
function cekValidationEvent() {

    var name = $("#eventName").hasClass("invalid");
    var sdate = $("#startDate").hasClass("invalid");
    var edate = $("#endDate").hasClass("invalid");
    var place = $("#place").hasClass("invalid");
    var budget = $("#budget").hasClass("invalid");
    var reqBy = $("#requestBy").hasClass("invalid");
    var reqDate = $("#requestDate").hasClass("invalid");

    var result = false;

    if (!name && !sdate && !edate && !place && !budget && !reqBy && !reqDate) {
        result = true;
    }

    return result;
}

//CEK VALIDATION REJECT
function cekValidationRejectEvent() {

    var rejectReason = $("#txt_reject_reason").hasClass("invalid");

    var result = false;

    if (!rejectReason) {
        result = true;
    }

    return result;
}

//CEK VALIDATION APPROVE
function cekValidationApproveEvent() {

     var result = false;

    if (!$("#txt_reject_reason").hasClass("invalid")) {
        result = true;
    }

    return result;
}