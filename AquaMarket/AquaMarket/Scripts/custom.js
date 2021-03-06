﻿

$('document').ready(function () {

    $('#delete_plant_btn').click(function () {
        var confirmationmessage = "Are you sure you want to delete this plant?";
        if (!confirm(confirmationmessage)) {
            event.preventDefault();
            alert("Your item will not be deleted!");
        }
        else {
            alert("Your item will be deleted!");
        }   
    });

    $('html, body').animate({ scrollTop: $('.partial_Lable').offset().top - 230 }, 1000);

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
                                label: item.PlantName,
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

    $('#article_search').autocomplete({

        source: function (request, response) {
            $.ajax({
                url: app.Urls.baseUrl + 'Articles/Autocomplete',
                data: { term: request.term },
                dataType: 'json',
                type: 'GET',
                success: function (data) {
                    response($.map(data,
                        function (item) {
                            return {
                                label: item.Title,
                                value2: item.Id
                            }
                        }));
                }
            })
        },
        select:
            function (event, ui) {
                $('#article_search').val(ui.item.label);
                $('#ArticleId').val(ui.item.value2);
                return false;
            },
        minLength: 4,

    });

});

