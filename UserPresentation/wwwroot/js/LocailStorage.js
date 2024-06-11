
function saveFavorites(event, productId) {
    event.preventDefault();

    let image = document.getElementById('fav-' + productId); 

    let inputId = "i-" + productId;

    let d = document.getElementById(inputId).value;

    let favorites = localStorage.getItem('favorites');
    let oldData = favorites ? JSON.parse(favorites) : [];

    let index = oldData.indexOf(d);
    if (index !== -1) {
        oldData.splice(index, 1);
        image.src = "/images/img/icon/heart.png";
    }
    else {
        oldData.push(d);
        image.src = "/images/img/icon/Blackheart.png";
    }

    localStorage.setItem('favorites', JSON.stringify(oldData));
    getFavorites();
}
    $(document).ready(function() {
        $('#filterInput').on('input', function () {
            var query = $(this).val();
            $.ajax({
                url: '/product/FilterFavorites',
                type: 'GET',
                data: { query: query },
                success: function (result) {
                    $('#favoritesContainer').html(result);
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        });
    });
