$(function () {
    var ints = 12;

    var z = ints;
    debugger;
    $("#parentName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Menu/AutoCompleteMenuParent/',
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
            $("#dropdown_parent_menu_id").val(i.item.id);
        },
        minLength: 1
    }).focus(function (event, ui) {
        $(".ui-helper-hidden-accessible").hide();
        event.preventDefault();
    });
});