let map;

var xmlHttp = new XMLHttpRequest();
xmlHttp.open("GET", "Branches/GetBranches", false);// default: asyn true, false -> sync
xmlHttp.send();
var branches = JSON.parse(xmlHttp.responseText); // array(JSON)


function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: -34.397, lng: 150.644 },
        zoom: 10
    });

    // get current position
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            position => {
                const pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };

                map.setCenter(pos);
            },
            () => {
                handleLocationError(true, infoWindow, map.getCenter());
            }
        );
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }


    // mark branches
    var geocoder = new google.maps.Geocoder();
    for (var i = 0; i < branches.length; i++) {
        console.log(branches[i]);
        geodoceAddress(geocoder, map, branches[i]);
    }

}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service failed."
            : "Error: Your browser doesn't support geolocation."
    );
    infoWindow.open(map);
}


function geodoceAddress(geocoder, map, branch) {

    var content = "<h1>" + branch.Name + "</h1><p>" + branch.Address + "</p>";

    var infowindow = new google.maps.InfoWindow({ content: content });

    geocoder.geocode({ address: branch.Address }, function (results, status) {
        if (status === "OK") {
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });

            marker.addListener("click", function () {
                infowindow.open(map, marker);
            })
        }
    })

}