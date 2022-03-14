/*创建ajax文件,调用api*/

var uri = 'api/products';

$(function () {
    $.getJSON(uri).done(function (data) {
        $.each(data, function (key, item) {
            $('<li>', { text: formatItem(item) }).appendTo($('#products'));
        });
    });
});

function formatItem(item) {
    return item.Name + ":$" + item.Price;
}

function find() {
    var id = $('#prodId').val();
    $.getJSON(uri + '/' + id)
        .done(function (data) {
            $('#product').text(formatItem(data))
        }).fail(function (jqXHR, textStatus, err) {
            $('#product').text('Error' + err);
        });
}