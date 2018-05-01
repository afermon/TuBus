function vVerTerminal() {

    this.ctrlActions = new ControlActions();

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
    }
}

let terminal = JSON.parse(sessionStorage.getItem("listaterminal_selected"));
var marker = false;

/* Cargar y muestra el mapa */
function cargarMapaLugares() {
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

    var myLatlng = new google.maps.LatLng(terminal.Latitude, terminal.Longitude);
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

    marker = new google.maps.Marker({

        position: myLatlng

    });

    marker.setMap(map);

}

$(document).ready(function () {

    
    var vverterminal = new vVerTerminal();
    vverterminal.BindFields(terminal);
});