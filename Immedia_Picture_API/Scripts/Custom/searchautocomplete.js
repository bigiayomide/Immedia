$(document).ready(function () {
    $(document).ready(function () {
        $("#sidebar").mCustomScrollbar({
            theme: "minimal"
        });

        $('#sidebarCollapse').on('click', function () {
            $('#sidebar, #content').toggleClass('active');
            $('.collapse.in').toggleClass('in');
            $('a[aria-expanded=true]').attr('aria-expanded', 'false');
        });
    });
    $("#searchbox").autocomplete({
        source: function (request, response) {

            $.ajax({
                type: 'GET',
                url: '/api/Picture/Getlocations',
                data: { query: request.term }

            }).done(function (data) {
                response($.map(data, function (item) {
                    return { label: item.Value, value: item.Value, place: item };
                }));
            });

        },
        select: function (event, ui) {
            var value = ui.item.value;
            var place = ui.item.place;
            app.getPictures(place);
            return false;
        },
        messages: {
            noResults: "No Result Found", results: ""
        }
    });
})