// Write your JavaScript code.
/*
var token; 
var unwatched;
var retries;

$(document).ready(function(){
    var login = {
        "login": "brianhummer",
        "password" : "pwn89noobs"
    };

    $.ajax({
        url: "https://plex.tv/users/sign_in.json",
        data: { user: login },
        type: "POST",
        beforeSend: function (request) {
            request.setRequestHeader('X-Plex-Client-Identifier', 'test-value');
            request.setRequestHeader('X-Plex-Product', 'test-value');
            request.setRequestHeader('X-Plex-Version', '1');
        },
        success: function (data) {
            token = data['user']['authToken'];
            processUnwatched(token);
        },
        error: function (data) {
            alert("error: " + JSON.stringify(data));
        }
    });

    $(document).on('click', '#libButton', function (e) {
        retries = 3;
        $.ajax({
            url: "http://ubuntu:32400/library/sections/" + $(this).attr('data-libraryId') + "/all",
            type: "GET",
            beforeSend: function (request) {
                request.setRequestHeader('X-Plex-Platform', 'web');
                request.setRequestHeader('X-Plex-Platform-Version', '1');
                request.setRequestHeader("X-Plex-Provides", "controller");
                request.setRequestHeader('X-Plex-Token', token);
                request.setRequestHeader('X-Plex-Client-Identifier', 'test-value');
                request.setRequestHeader('X-Plex-Product', 'test-value');
                request.setRequestHeader('X-Plex-Version', '1');
                request.setRequestHeader('Accept', 'application/json');
            },
            success: function (data) {
                unwatched = [];
                for (var i = 0; i < data['MediaContainer']['Metadata'].length; i++)
                {
                    if (data['MediaContainer']['Metadata'][i]['leafCount'] - data['MediaContainer']['Metadata'][i]['viewedLeafCount'] > 0)
                    {
                        unwatched.push(data['MediaContainer']['Metadata'][i]['title']);
                    }
                }

                var container = $("#rouletteContainer");
                var html =
                    '<div class="card" style="width: 20rem;">' +
                        '<div class="card-body">' +
                            '<h4 class="card-title">' + unwatched[Math.floor((Math.random() * unwatched.length))] + '</h4>' +
                            '<p class="card-text">Retries left: ' + retries + '</p>' +
                            '<a href="#" id="retry" class="btn btn-primary" >Retry</a>' +
                        '</div>' +
                    '</div>';
                container.html(html);
            },
            error: function (data) {
                alert("error: " + JSON.stringify(data));
            }
        });
    });

    $(document).on('click', '#retry', function (e) {
        if (retries > 0)
            retries--;

        var enabledClass = retries == 0 ? "disabled" : "";

        var container = $("#rouletteContainer");
        var html =
            '<div class="card" style="width: 20rem;">' +
            '<div class="card-body">' +
            '<h4 class="card-title">' + unwatched[Math.floor((Math.random() * unwatched.length))] + '</h4>' +
            '<p class="card-text">Retries left: ' + retries + '</p>' +
            '<a href="#" id="retry" class="btn btn-primary ' + enabledClass + '">Retry</a>' +
            '</div>' +
            '</div>';
        container.html(html);
    });
});


function processUnwatched()
{
    $.ajax({
        url: "http://ubuntu:32400/library/sections",
        type: "GET",
        beforeSend: function (request) {
            request.setRequestHeader('X-Plex-Platform', 'web');
            request.setRequestHeader('X-Plex-Platform-Version', '1');
            request.setRequestHeader("X-Plex-Provides", "controller"); 
            request.setRequestHeader('X-Plex-Token', token);
            request.setRequestHeader('X-Plex-Client-Identifier', 'test-value');
            request.setRequestHeader('X-Plex-Product', 'test-value');
            request.setRequestHeader('X-Plex-Version', '1');
            request.setRequestHeader('Accept', 'application/json');
        },
        success: function (data) {
            var html = "<div class='mt-2'>";
            for (var i = 0; i < data['MediaContainer']['Directory'].length; i++)
            {
                html += ("<button class='btn btn-primary mr-2' id='libButton' data-libraryId='" + data['MediaContainer']['Directory'][i]['key'] + "'>" + data['MediaContainer']['Directory'][i]['title'] + "</button>");
            }

            html += "</div>";

            $("#rouletteContainer").append(html);

        },
        error: function (data) {
            alert("error: " + JSON.stringify(data));
        }
    });
}*/