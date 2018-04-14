$(document).ready(function () {
    $("#searchbox").autocomplete({
        source: function (request, response) {

            $.ajax({
                type: 'GET',
                url: '/api/Picture/Getlocations',
                data: { query: request.term }

            }).done(function (data) {
                response($.map(data, function (item) {
                    console.log(item.PlaceId);
                    return { label: item.Value, value: item.Value, id: item.PlaceId };
                }));
            });

        },
        select: function (event, ui) {
            var value = ui.item.value;
            var id = ui.item.id;
            app.getPictures(id);
            alert(value + " " + id)
            return false;
        },
        messages: {
            noResults: "No Result Found", results: ""
        }
    });
})