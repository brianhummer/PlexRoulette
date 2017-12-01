$(document).on('click', '#librarySelect', function (e) {
    e.preventDefault();

    var id = $(this).attr("data-libraryId");

    $.ajax({
        url: "/Home/RandomThree",
        contentType: "application/json",
        type: "POST",
        data: JSON.stringify({
            'LibraryId': id
        }),
        success: function (data) {
            $('div#rouletteContainer').html(data);
        },
        error: function (data) {
            alert("an error occurred: " + data);
        }
    });
});
