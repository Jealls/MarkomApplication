
function validationName() {
    var vjsName = document.getElementById("name").value;
    if (vjsName == "") {
        $("#name").addClass("invalid");
    } else {
        $("#name").removeClass("invalid");
    }
}
function validationIdUnit() {
    var unit = parseInt(document.getElementById("mUnitId").value) ? parseInt(document.getElementById("mUnitId").value) : 0;
    if (unit == 0) {
        $("#unitName").addClass("invalid");
    } else {
        $("#unitName").removeClass("invalid");
    }
}

function cekValidationSouvenir() {

    var name = $("#name").hasClass("invalid");
    var unit = $("#unitName").hasClass("invalid");

    var result = false;

    if (!name && !unit) {
        result = true;
    }

    return result;
}