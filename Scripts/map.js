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

    // auto complete

    const input = document.getElementById("start");
    autoComplete(input, map);

    // get direction
    const directionsService = new google.maps.DirectionsService();
    const directionsRenderer = new google.maps.DirectionsRenderer();
    directionsRenderer.setPanel(document.getElementById("right-panel"));
    directionsRenderer.setMap(map);
    var getDirection = document.getElementById("get-direction");
    getDirection.addEventListener("click", function () {
        calculateAndDisplayRoute(directionsService, directionsRenderer);
    });


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
                position: results[0].geometry.location,
                icon: "https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png",
            });

            marker.addListener("click", function () {
                infowindow.open(map, marker);
            })
        }
    })

}

function autoComplete(input, map) {
    const autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.setTypes(["address"]);
    autocomplete.bindTo("bounds", map);

}

function calculateAndDisplayRoute(directionsService, directionsRenderer) {
    var mode = document.getElementById("mode");
    directionsService.route(
        {
            origin: {
                query: document.getElementById("start").value,
            },
            destination: {
                query: document.getElementById("end").value,
            },
            travelMode: google.maps.TravelMode[mode.value],
        },
        (response, status) => {
            if (status === "OK") {
                directionsRenderer.setDirections(response);
            } else {
                window.alert("Directions request failed due to " + status);
            }
        }
    );
}



