// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {

    var statoTags = [
        "DA DENUNCIARE",
        "FUORI SERVIZIO",
        "IN SERVIZIO",
        "ROTTAMATO"
    ];
    $("input[name*='Stato']").autocomplete({ source: statoTags });
    //var select = document.getElementById("stato");

    //var select = $('#stato');
    //if (select.prop) {
    //    var options = select.prop('options');
    //}
    //else {
    //    var options = select.attr('options');
    //}
    //$('option', select).remove();

    //$.each(statoTags, function (i) {
    //    options[i] = new Option(statoTags[i], statoTags[i]);
    //});
    //select.val(statoTags[0]);
    
    $.each(statoTags, function (i, item) {
        $('#stato').append($('<option>', {
            value: item,
            text: item
        }));
    });
});
