
function validationName() {
    var vjsName = document.getElementById("name").value;
    if (vjsName == "") {
        $("#name").addClass("invalid");
    } else {
        $("#name").removeClass("invalid");
    }
}

function cekValidationProduct() {

    var name = $("#name").hasClass("invalid");
    var result = false;

    if (!name) {
        result = true;
    }

    return result;
}