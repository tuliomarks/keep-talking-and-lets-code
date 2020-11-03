var toMarker = function(camera) {
    return {
        lat: camera.latitude,
        lng: camera.longitude
    };
}

var initMap = function (containerId, markers) {

    if (!markers) return;

    const centerMarker = toMarker(markers[0]);
    const map = new google.maps.Map(document.getElementById(containerId), {
        zoom: 4,
        center: centerMarker,
    });

    $.each(markers, function (index) {

        var camera = markers[index];
        new google.maps.Marker({
            position: toMarker(camera),
            map,
            title: camera.Name,
        });
    });

}

$(document).ready(function () {

    // get all markers from API 
    $.get("api/v1/Camera/", function (data) {
        initMap("map", data);
    });

});


