
//EVENT NAME
function validationName(Name) {;
    if (Name == "") {
        $("#event_name").addClass("invalid");
    } else {
        $("#event_name").removeClass("invalid");
    }
}

function validationEName() {
    var vjsName = document.getElementById("event_name").value;
    if (vjsName == "") {
        $("#event_name").addClass("invalid");
    } else {
        $("#event_name").removeClass("invalid");
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


//START DATE
function validationStartDate(Date) {

    if (Date == "") {
        $("#start_date").addClass("invalid");
    } else {
        $("#start_date").removeClass("invalid");
    }
}

function validationEStartDate() {
    var vjsDate = document.getElementById("start_date").value;
    if (vjsDate == "") {
        $("#start_date").addClass("invalid");
    } else {
        $("#start_date").removeClass("invalid");
    }
}

//END DATE
function validationEndDate(Date) {

    if (Date == "") {
        $("#end_date").addClass("invalid");
    } else {
        $("#end_date").removeClass("invalid");
    }
}

function validationEEndDate() {
    var vjsDate = document.getElementById("end_date").value;
    if (vjsDate == "") {
        $("#end_date").addClass("invalid");
    } else {
        $("#end_date").removeClass("invalid");
    }
}

//END DATE
function validationBudget(budget) {

    if (budget == "") {
        $("#budget").addClass("invalid");
    } else {
        $("#end_date").removeClass("invalid");
    }
}

function validationEBudget() {
    var vjsBudget = document.getElementById("budget").value;
    if (vjsBudget == "") {
        $("#budget").addClass("invalid");
    } else {
        $("#bugdet").removeClass("invalid");
    }
}



function cekValidationEvent() {

    var name = $("#event_name").hasClass("invalid");
    var sdate = $("#start_date").hasClass("invalid");
    var edate = $("#end_date").hasClass("invalid");
    var place = $("#place").hasClass("invalid");
    var budget = $("#budget").hasClass("invalid");

    var result = false;

    if (!name && !sdate && !edate && !place && !budget) {
        result = true;
    }

    return result;
}