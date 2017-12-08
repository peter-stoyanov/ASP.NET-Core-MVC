// Write your JavaScript code.
var lb = lb || {};

//************************************************************************************************
// Auto submit form on change
//************************************************************************************************
$(document).ready(function () {
    $('form.autosubmit select, form.autosubmit input[type="checkbox"], form.autosubmit input[type="text"]').on('change', function (event) {
        $(this).closest('form').submit();
    });
});
