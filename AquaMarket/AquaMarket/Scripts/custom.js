﻿

$('document').ready(function () {


    $('html, body').animate({ scrollTop: $('.partial_Lable').offset().top - 230 }, 1000);

});

$('document').ready(function () {
    var flowers = ["Астра", "Нарцисс", "Роза", "Пион", "Примула",
        "Подснежник", "Мак", "Первоцвет", "Петуния", "Фиалка"];
    //source: app.Urls.baseUrl + 'Plant/Autocomplete'

    $('#search').autocomplete({
        
         source: function (request, response) {
             $.ajax({
                 url: app.Urls.baseUrl + 'Plant/Autocomplete',
                 data: { term: request.term },
                dataType: 'json',
                type: 'GET',
                success: function (data) {
                    response($.map(data,
                        function (item) {
                            return {
                                label: item.Name,
                                value1: item.Id
                            }
                        }));
                }
            })
        },
        select:
            function (event, ui) {
                $('#search').val(ui.item.label);
                $('#Id').val(ui.item.value1);
                return false;
            },
        minLength: 2,
        
    });


});