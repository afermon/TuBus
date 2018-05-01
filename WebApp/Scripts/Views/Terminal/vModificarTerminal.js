let terminal = JSON.parse(sessionStorage.getItem("listaterminal_selected"));

function editarTerminal()
{
    let nombreTerminal = document.querySelector('#txtTerminalName');
    let longitudTerminal = document.querySelector('#nLongitude');
    let latitudTerminal = document.querySelector('#nLatitude');
    let cantidadLineas = document.querySelector('#nCantidadLineas');
    let espaciosParqueo = document.querySelector('#nEspaciosParqueo');
    let espaciosParqueoBus = document.querySelector('#nEspaciosParqueoBus');
    
    nombreTerminal.value = terminal.TerminalName;
    longitudTerminal.value = terminal.Longitude;
    latitudTerminal.value = terminal.Latitude;
    cantidadLineas.value = terminal.CantidadLineas;
    espaciosParqueo.value = terminal.EspaciosParqueo;
    espaciosParqueoBus.value = terminal.EspaciosParqueoBus;
}

function vModificarTerminal() {
    
    this.ctrlActions = new ControlActions();
    this.service = "Terminal/ModificarTerminal";

    if (!$("#frmEdition").valid()) {
        this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
        return;
    } else {

        this.Create = function () {

            var terminalData = {};
            terminalData = this.ctrlActions.GetDataForm('frmEdition');
            terminalData.Id = terminal.Id;
                        
            //Put
            this.ctrlActions.PutToAPI(this.service, terminalData, function () {
                window.location.replace("/Terminal");
            });
        }
    }

}

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



    //Listen for any clicks on the map.
    google.maps.event.addListener(map, 'click', function (event) {
        //Get the location that the user clicked.
        var clickedLocation = event.latLng;
        //If the marker hasn't been added.
        if (marker === false) {
            //Create the marker.
            marker = new google.maps.Marker({
                position: clickedLocation,
                map: map,
                draggable: true //make it draggable
            });
            //Listen for drag events!
            google.maps.event.addListener(marker, 'dragend', function (event) {
                markerLocation();
            });
        } else {
            //Marker has already been added, so just change its location.
            marker.setPosition(clickedLocation);
        }
        //Get the marker's location.
        markerLocation();
    });

}

function markerLocation() {
    var inputLatitud = document.querySelector('#nLatitude');
    var inputLongitud = document.querySelector('#nLongitude');

    //Get location.
    var currentLocation = marker.getPosition();
    //Add lat and lng values to a field that we can save.
    inputLatitud.value = currentLocation.lat(); //latitude
    inputLongitud.value = currentLocation.lng(); //longitude
}

$(document).ready(function () {

    editarTerminal();
    
    $("#frmEdition").validate({
        rules: {
            TerminalName: {
                required: true,
                minlength: 3
            },
            Latitude: {
                required: true,
                number: true,
                minlength: 10
            },
            Longitude: {
                required: true,
                number: true,
                minlength: 10
            },
            CantidadLineas: {
                required: true,
                number: true,
                min: 1
            },
            CantidadLineas: {
                required: true,
                number: true,
                min: 1
            },
            EspaciosParqueo: {
                required: true,
                number: true,
                min: 1
            },
            EspaciosParqueoBus: {
                required: true,
                number: true,
                minlength: 1
            }

        },
        messages: {
            TerminalName: {
                required: "Por favor ingrese el nombre de la terminal",
                minlength: "Debe digitar mínimo 3 caracteres"
            },
            Latitude: {
                required: "Por favor ingrese la latitud de la terminal",
                minlength: "Por favor ingrese mínimo 10 digitos",
                number: "El valor digitado debe ser numerico"
            },
            Longitude: {
                required: "Por favor ingrese la longitud de la terminal",
                minlength: "Por favor ingrese mínimo 10 digitos",
                number: "El valor digitado debe ser numerico"
            },
            CantidadLineas: {
                required: "Por favor ingrese la cantidad de líneas",
                minlength: "Por favor ingrese mínimo 10 digitos",
                number: "El valor digitado debe ser numerico"
            },
            EspaciosParqueo: {
                required: "Por favor los espacios de parqueo disponibles",
                minlength: "Por favor ingrese mínimo 1 digito",
                number: "El valor digitado debe ser numerico"

            },
            EspaciosParqueoBus: {
                required: "Por favor los espacios de parqueo disponibles",
                minlength: "Por favor ingrese mínimo 1 digito",
                number: "El valor digitado debe ser numerico"

            }
        },

        errorElement: "label",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            error.insertAfter(element);

        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-invalid");
        }
    });
});