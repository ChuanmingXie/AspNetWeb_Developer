
var uri="api/products"

$(function () {
    $.getJSON(uri, function (data) {
        $('#products').empty();
        $.each(data, function (key, item) {
            var row = '<td>' + item.Name + '</td><td>' + item.Price + '</td>';
            $('<tr/>', { html: row }).appendTo($('#products'));
        });
    });
})