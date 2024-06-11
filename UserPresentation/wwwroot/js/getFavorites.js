function getFavorites() {
    var favorites = localStorage.getItem("favorites");

    if (!favorites) {
        favorites = "[]";
    }
    $.ajax({
        type: "GET",
        url: "/Product/GetFavorites",
        data: { "favorites": favorites },
        success: function(data) {
            $('#favoritesContainer').html(data);
        },
        error: function(xhr, status, error) {
            console.error(xhr.responseText);
        }
    });
}
