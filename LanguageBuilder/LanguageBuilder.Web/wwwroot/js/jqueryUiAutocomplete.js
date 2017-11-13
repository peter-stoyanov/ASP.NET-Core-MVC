$(document).ready(function () {

    $('#TeamLeader').autocomplete({
        source: function (request, response) {
            pipeline.ajaxGetJson({
                url: '@Url.Content("~/api/Words/Search")',
                data: { keywords: $('#source-word').val(), rows: 10 },
                success: function (data, status, xhr) {
                    var items = [];

                    if (data) {
                        items = $.map(data.records, function (user) {
                            return {
                                label: user.fullName + ' (' + user.domainUserLogin + ')',
                                value: user.fullName,
                                id: user.id
                            };
                        });
                    }
                    response(items);
                }
            });
        },
        select: function (event, ui) {
            $('#source-word').val(ui.item.id);
        },
        minLength: 1
    });

});