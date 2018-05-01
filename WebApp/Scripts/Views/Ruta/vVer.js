function vVer() {
    this.ctrlActions = new ControlActions();
    this.ruta = JSON.parse(sessionStorage.getItem("listarutas_selected"));
    this.service = 'Ruta/Get/' + this.ruta.Id;

    this.Retrieve = function () {
        var jqxhr = $.get({
            url: this.ctrlActions.GetUrlApiService(this.service),
            headers: { 'Authorization': auth.GetAccessToken() }
        }).done(function (response) {
            var vruta = new vVer();
            vruta.BindFieldsRuta(response.Data);
            ruta = response.Data;
        }).fail(function (response) {
            var data = response.responseJSON;
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('E', data.ExceptionMessage);
            console.log(data);
        });
    }

    this.FillComboBoxes = function () {
        this.ctrlActions.FillComboBox("/Empresa/GetAll", "cbEmpresa", "CedulaJuridica", "NombreEmpresa");
        this.ctrlActions.FillComboBox("Terminal/ObtenerTerminales", "cbTerminal", "Id", "TerminalName");
        this.ctrlActions.FillComboBox("Linea/GetAllLines", "cbLinea", "LineaId", "NombreLinea"); 
        this.ctrlActions.FillComboBox("Tarifa/GetTarifas", "cbTarifa", "RouteId", "RouteName");
        this.ctrlActions.FillComboBox("List/Get/ESTADO", "cbEstado", "Description", "Description");
    }

    this.BindFieldsRuta = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
        horarios = data.Horarios;
        this.DrawHorario();

        //// Map
        directionsRequest = JSON.parse(data.JsonRoute);

        directionsRequest.origin = new google.maps.LatLng(directionsRequest.origin);
        directionsRequest.destination = new google.maps.LatLng(directionsRequest.destination);

        directionsRequest.waypoints.forEach(function (waypoint, index, waypoints) {
            waypoints[index] = new google.maps.LatLng(waypoint);
        });

        this.createPolyline(directionsRequest.waypoints);

        //Markers
        terminalMarker = new google.maps.Marker({
            position: directionsRequest.origin,
            map: map,
            icon: terminalIcon
        });

        destinoMarker = new google.maps.Marker({
            position: directionsRequest.destination,
            map: map,
            icon: destinationIcon
        });
    }

    this.createPolyline = function (waypoints) {

        var line = new google.maps.Polyline({
            path: waypoints,
            strokeColor: '#49B691',
            strokeOpacity: 0.8,
            strokeWeight: 5
        });
        line.setMap(map);
    }


    this.DrawHorario = function () {
        var table = this.GroupBy(horarios, "Dia");
        for (var d = 1; d <= 7; d++) {
            if (table.hasOwnProperty(d)) {
                $('#dia-' + d).html("");
                var c = table[d];
                c.sort(function (a, b) {
                    return moment(a.Hora, 'HH:mm:ss').diff(moment(b.Hora, 'HH:mm:ss'));
                });
                c.forEach(function (h) {
                    $('#dia-' + d).append("<li class='list-group-item'>" + h.Hora.substring(0, h.Hora.length - 3) + "</li>");
                });
            } else {
                $('#dia-' + d).html("<li class='list-group-item text-danger'>Sin servicio</li>");
            }
        }
    }

    this.GroupBy = function (arr, property) {
        return arr.reduce(function (memo, x) {
            if (!memo[x[property]]) { memo[x[property]] = []; }
            memo[x[property]].push(x);
            return memo;
        }, {});
    }
}

// Gobal variables
var ruta = null;
var horarios = [];
var map;
var directionsRequest;
var terminalMarker;
var destinoMarker = false;
var terminalIcon;
var destinationIcon;

/* Cargar y muestra el mapa */
function cargarMapa() {
    //Style
    var main_color = '#49B691',
        saturation_value = -20,
        brightness_value = 5;

    var style = [
        { elementType: 'labels.text.fill', stylers: [{ color: '#B6BFC0' }] },
        {
            elementType: "labels",
            stylers: [
                { saturation: saturation_value }
            ]
        },
        {
            featureType: 'administrative.locality',
            elementType: 'labels.text.fill',
            stylers: [{ color: '#EEB95A' }]
        },
        {
            featureType: 'poi',
            elementType: 'labels.text.fill',
            stylers: [{ color: '#EEB95A' }]
        },
        {
            featureType: "poi",
            elementType: "labels",
            stylers: [
                { visibility: "off" }
            ]
        },
        {
            featureType: 'road.highway',
            elementType: 'labels',
            stylers: [
                { visibility: "off" }
            ]
        },
        {
            featureType: "road.local",
            elementType: "labels.icon",
            stylers: [
                { visibility: "off" }
            ]
        },
        {
            featureType: "road.arterial",
            elementType: "labels.icon",
            stylers: [
                { visibility: "off" }
            ]
        },
        {
            featureType: "road",
            elementType: "geometry.stroke",
            stylers: [
                { visibility: "off" }
            ]
        },
        {
            featureType: "transit",
            elementType: "geometry.fill",
            stylers: [
                { hue: main_color },
                { visibility: "on" },
                { lightness: brightness_value },
                { saturation: saturation_value }
            ]
        },
        {
            featureType: "poi",
            elementType: "geometry.fill",
            stylers: [
                { hue: main_color },
                { visibility: "on" },
                { lightness: brightness_value },
                { saturation: saturation_value }
            ]
        },
        {
            featureType: "poi.government",
            elementType: "geometry.fill",
            stylers: [
                { hue: main_color },
                { visibility: "on" },
                { lightness: brightness_value },
                { saturation: saturation_value }
            ]
        },
        {
            featureType: "poi.business",
            elementType: "geometry.fill",
            stylers: [
                { hue: main_color },
                { visibility: "on" },
                { lightness: brightness_value },
                { saturation: saturation_value }
            ]
        },
        {
            featureType: "transit",
            elementType: "geometry.fill",
            stylers: [
                { hue: main_color },
                { visibility: "on" },
                { lightness: brightness_value },
                { saturation: saturation_value }
            ]
        },
        {
            featureType: "transit.station",
            elementType: "geometry.fill",
            stylers: [
                { hue: main_color },
                { visibility: "on" },
                { lightness: brightness_value },
                { saturation: saturation_value }
            ]
        },
        {
            featureType: "road",
            elementType: "geometry.fill",
            stylers: [
                { hue: main_color },
                { visibility: "on" },
                { lightness: brightness_value },
                { saturation: saturation_value }
            ]
        },
        {
            featureType: "road.highway",
            elementType: "geometry.fill",
            stylers: [
                { hue: main_color },
                { visibility: "on" },
                { lightness: brightness_value },
                { saturation: saturation_value }
            ]
        },
        {
            featureType: "water",
            elementType: "geometry",
            stylers: [
                { hue: main_color },
                { visibility: "on" },
                { lightness: brightness_value },
                { saturation: saturation_value }
            ]
        }
    ];

    ////Icons
    terminalIcon = {
        url: '/img/maps/map-icon-terminal.png',
        size: new google.maps.Size(130, 200),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(20, 54),
        scaledSize: new google.maps.Size(40, 62),
        labelOrigin: new google.maps.Point(0, 0)
    };

    destinationIcon = {
        url: '/img/maps/map-icon-destination.png',
        size: new google.maps.Size(123, 200),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(19, 62),
        scaledSize: new google.maps.Size(38, 62),
        labelOrigin: new google.maps.Point(0, 0)
    };

    //The center location of Costa Rica.
    var centerOfMap = new google.maps.LatLng(9.9369028, -84.0853536);
    var lugar = null;

    //Map options.
    var options = {
        center: centerOfMap, //Set center.
        zoom: 13, //The zoom value.
        panControl: false,
        zoomControl: true,
        mapTypeControl: false,
        streetViewControl: false,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        scrollwheel: false,
        styles: style,
        zoomControlOptions: {
            style: google.maps.ZoomControlStyle.SMALL,
            position: google.maps.ControlPosition.BOTTOM_CENTER
        }
    };

    //Create the map object.
    map = new google.maps.Map(document.getElementById('mapa-lugar'), options);

    $(document).ready(function () {
        var vver = new vVer();
        vver.FillComboBoxes();
        vver.Retrieve();
    });
}

function markerLocation() {
    //Get location.
    var currentLocation = destinoMarker.getPosition();
    //Add lat and lng values to a field that we can save.
    $('#nDestinoLatitude').val(currentLocation.lat()); //latitude
    $('#nDestinoLongitude').val(currentLocation.lng()); //longitude
    calcRoute();
}

function makeMarker(position, icon, title) {
    new google.maps.Marker({
        position: position,
        map: map,
        icon: icon,
        title: title
    });
}


function calcRoute() {
    if (destinoMarker !== false) {
        // Set destination, origin and travel mode.
        var request = {
            origin: terminalMarker.getPosition(),
            destination: destinoMarker.getPosition(),
            travelMode: 'DRIVING'
        };

        // Pass the directions request to the directions service.
        directionsService.route(request,
            function (response, status) {
                if (status === 'OK') {
                    // Display the route on the map.
                    directionsDisplay.setDirections(response);
                    directionsResponse = response;
                    newRoutePoints = response.routes[0].overview_path;
                } else {
                    alert('Could not display directions due to: ' + status);
                }
            });
    }
}