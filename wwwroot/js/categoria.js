// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {

    var categoriaTags = [
        "LIBRETTO",
        "DISEGNO",
        "CERTIFICATO SPESSORE",
        "CERTIFICATO COSTRUTTORE",
        "CERTIFICATO FUNZIONAMENTO",
        "CERTIFICATO INTEGRITA'",
        "CERTIFICATO TARATURA",
        "SCHEMA",
        "RELAZIONE",
        "VERIFICA PRIMO IMPIANTO",
        "VARIE"
    ];
    //$("input[name*='Category']").autocomplete({ source: categoriaTags });

    $.each(categoriaTags, function (i, item) {
        $('#Category').append($('<option>', {
            value: item,
            text: item
        }));
    });

});
