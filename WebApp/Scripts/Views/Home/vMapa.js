function vMapa() {
    this.ctrlActions = new ControlActions();
    this.service = "Terminal/ObtenerTerminales";
    this.serviceRutas = "Ruta/GetAll";
    this.servicePosiciones = "Recorrido/GetPositions";

    this.Retrieve = function() {
        var jqxhr = $.get({
            url: this.ctrlActions.GetUrlApiService(this.service)
        }).done(function (response) {
            response.Data.forEach(function (terminal) {
                //Markers
                terminalMarkers[terminal.Id] = new google.maps.Marker({
                    position: new google.maps.LatLng(terminal.Latitude, terminal.Longitude),
                    title: terminal.TerminalName,
                    map: map,
                    icon: terminalIcon
                });
                
                terminalInfoWindows[terminal.Id]  = new google.maps.InfoWindow({
                    content: terminal.TerminalName
                });

                terminalMarkers[terminal.Id].addListener('mouseover', function () {
                    terminalInfoWindows[terminal.Id].open(map, terminalMarkers[terminal.Id]);
                });

                terminalMarkers[terminal.Id].addListener('mouseout', function () {
                    terminalInfoWindows[terminal.Id].close();
                });
            });
            var vmapa = new vMapa();
            vmapa.RetrieveRutas();
        }).fail(function(response) {
            var data = response.responseJSON;
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('E', data.ExceptionMessage);
            console.log(data);
        });
    }

    this.RetrieveRutas = function () {
        var jqxhr = $.get({
            url: this.ctrlActions.GetUrlApiService(this.serviceRutas)
        }).done(function (response) {
            rutas = response.Data;
            var vmapa = new vMapa();
            rutas.forEach(function (ruta, index, array) {
                array[index].JsonRoute = JSON.parse(ruta.JsonRoute);
                //Markers
                destinoMarkers[ruta.Id] = new google.maps.Marker({
                    position: new google.maps.LatLng(ruta.DestinoLatitude, ruta.DestinoLongitude),
                    title: ruta.RutaName,
                    map: map,
                    icon: destinationIcon
                });
                vmapa.createPolyline(ruta.JsonRoute.waypoints);
            });

            window.setInterval(function () {
                console.log("Actualizar posiciones.");
                vmapa.RetrievePosiciones();
            }, 3000);
            
        }).fail(function (response) {
            var data = response.responseJSON;
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('E', data.ExceptionMessage);
            console.log(data);
        });
    }

    this.RetrievePosiciones = function () {
        var jqxhr = $.get({
            url: this.ctrlActions.GetUrlApiService(this.servicePosiciones)
        }).done(function (response) {
            recorridos = response.Data;
            busMarkers.forEach(function(marker) {
                marker.setMap(null);
            });
            busMarkers = [];

            var vmapa = new vMapa();

            recorridos.forEach(function (recorrido) {
                var pos = recorrido.Posicion;
                //Markers
                busMarkers[pos.RecorridoId] = new google.maps.Marker({
                    position: new google.maps.LatLng(pos.Latitude, pos.Longitude),
                    title: "Placa: " + recorrido.BusPlaca + "  Actualizado: " + moment(pos.TimeStamp).format("hh:mm a"),
                    map: map,
                    icon: busIcon
                });
            });

        }).fail(function (response) {
            var data = response.responseJSON;
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('E', data.ExceptionMessage);
            console.log(data);
        });
    }

    this.createPolyline = function (waypoints) {
        waypoints.forEach(function (waypoint, index, waypoints) {
            waypoints[index] = new google.maps.LatLng(waypoint);
        });

        var line = new google.maps.Polyline({
            path: waypoints,
            strokeColor: '#49B691',
            strokeOpacity: 0.8,
            strokeWeight: 5
        });
        line.setMap(map);
    }
}

// Gobal variables
var rutas = [];
var horarios = [];
var recorridos = [];
var recorridoTimer = null;
var map;
var directionsRequest;
var terminalMarkers = [];
var terminalInfoWindows = [];
var rutaInfoWindows = [];
var destinoMarkers = [];
var busMarkers = [];
var terminalIcon;
var destinationIcon;
var busIcon;

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
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(20, 54),
        scaledSize: new google.maps.Size(40, 62),
        labelOrigin: new google.maps.Point(0, 0)
    };

    destinationIcon = {
        url: '/img/maps/map-icon-destination.png',
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(19, 62),
        scaledSize: new google.maps.Size(38, 62),
        labelOrigin: new google.maps.Point(0, 0)
    };

    busIcon = {
        url: '/img/maps/map-icon-bus.png',
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(20, 13),
        scaledSize: new google.maps.Size(40, 26),
        labelOrigin: new google.maps.Point(0, 0)
    };

    //The center location of Costa Rica.
    var centerOfMap = new google.maps.LatLng(9.9369028, -84.0853536);

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
    map = new google.maps.Map(document.getElementById('mapa-rutas'), options);


    $(document).ready(function () {
        var vmapa = new vMapa();
        vmapa.Retrieve();
    });
}

function makeMarker(position, icon, title) {
    new google.maps.Marker({
        position: position,
        map: map,
        icon: icon,
        title: title
    });
}