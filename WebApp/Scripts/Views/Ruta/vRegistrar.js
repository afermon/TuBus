
function vRegistrar()
{
    this.ctrlActions = new ControlActions();
    this.service = "Ruta/Create";

    this.user = auth.GetCurrentUser();
    this.empresa = auth.GetCurrentEmpresa();

    this.Create = function () {
        if (!$("#frmEdition").valid()) {
            this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
            return;
        } else {
            var rutaData = this.ctrlActions.GetDataForm('frmEdition');
            rutaData["JsonRoute"] = JSON.stringify({
                origin: terminalMarker.getPosition(),
                destination: destinoMarker.getPosition(),
                waypoints: directionsDisplay.directions.routes[0].overview_path,
                travelMode: 'DRIVING'
            });
            rutaData["Horarios"] = horarios;
            this.ctrlActions.PostToAPI(this.service, rutaData, function() {
                window.location.replace("/Ruta");
            });
        }
         
    }

    this.FillComboBoxes = function () {
        this.ctrlActions.FillComboBox("Empresa/GetAll", "cbEmpresa", "CedulaJuridica", "NombreEmpresa");
        this.ctrlActions.FillComboBox("Terminal/ObtenerTerminales", "cbTerminal", "Id", "TerminalName");
        this.ctrlActions.FillComboBox("Tarifa/GetTarifas", "cbTarifa", "RouteId", "RouteName");
        this.ctrlActions.FillComboBox("List/Get/DAYS", "cbDia", "Value", "Description");
        this.FillLineas();
    }

    this.FillLineas = function() {
        this.serviceLineas = this.empresa == null ? 'Linea/GetAllLines?terminal=' + this.user.TerminalId + '&empresaId=' + $("#cbEmpresa").val() : 'Linea/GetAllLines?terminal=' + this.user.TerminalId + '&empresaId=' + this.empresa.CedulaJuridica;
        if (this.empresa != null) {
            $("#cbEmpresa").prop('disabled', 'disabled');
        }
        $("#cbLinea").html("");
        this.ctrlActions.FillComboBox(this.serviceLineas, "cbLinea", "LineaId", "NombreLinea"); 
    }

    this.GetTarifa =  function () {
        var tarifa = JSON.parse(sessionStorage.getItem("list-cbTarifa")).find(tarifa => tarifa.RouteId == $("#cbTarifa").val());
        $("#nCostoTotal").val(tarifa.RegularFare);
    }

    this.AddTime = function () {
        $("#cbDia").val().forEach(function(dia) {
            var horario = {
                "Dia": dia,
                "Hora": $('#dpHora').val()
            }

            var check = $.grep(horarios, function(h) {
                return h.Dia === horario.Dia && h.Hora === horario.Hora;
            });

            if (check.length > 0) {
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', "Ya existe el horario indicado.");
            } else {
                horarios.push(horario);
                var vr = new vRegistrar();
                vr.DrawHorario();
            }

        });
    }
    this.DrawHorario = function () {
        var table = this.GroupBy(horarios, "Dia");
        for (var d = 1; d <= 7; d++) {
            if (table.hasOwnProperty(d)) {
                $('#dia-' + d).html("");
                var c = table[d];
                c.sort(function(a, b) {
                    return moment(a.Hora, 'HH:mm:ss').diff(moment(b.Hora, 'HH:mm:ss'));
                });
                c.forEach(function (h) {
                    $('#dia-' + d).append("<li class='list-group-item'>"+ h.Hora +"  <i class='fa fa-times rem-hora' data-dia='"+h.Dia+"' data-hora='"+ h.Hora+"' aria-hidden='true'> </i></li>");
                });
            } else {
                $('#dia-' + d).html("<li class='list-group-item text-danger'>Sin servicio</li>");
            }
        }
        $(".rem-hora").click(function () {
            var horario = {
                "Dia": $(this).data("dia"),
                "Hora": $(this).data("hora")
            };

            horarios = horarios.filter(function(h) {
                return h.Dia != horario.Dia || h.Hora != horario.Hora;
            });
            var vr = new vRegistrar();
            vr.DrawHorario();
        });
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
var horarios = [];
var map;
var newRoute;
var newRoutePoints;
var directionsService;
var directionsDisplay;
var directionsResponse;
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

    var polylineOptionsRenderer = {
        strokeColor: main_color,
        strokeOpacity: 0.8,
        strokeWeight: 5
    };

    directionsDisplay = new google.maps.DirectionsRenderer({
        suppressMarkers: true,
        draggable: true,
        polylineOptions: polylineOptionsRenderer
    });

    directionsService = new google.maps.DirectionsService();
    directionsDisplay.setMap(map);

    directionsDisplay.addListener('directions_changed', function () {
        CalcDistance(directionsDisplay.getDirections());
    });

    //Listen for any clicks on the map.
    google.maps.event.addListener(map, 'click', function (event) {
        //Get the location that the user clicked.
        var clickedLocation = event.latLng;
        //If the marker hasn't been added.
        if (destinoMarker === false) {
            //Create the marker.
            destinoMarker = new google.maps.Marker({
                position: clickedLocation,
                map: map,
                icon: destinationIcon,
                draggable: true //make it draggable
            });
            //Listen for drag events!
            google.maps.event.addListener(destinoMarker, 'dragend', function (event) {
                markerLocation();
            });
        } else {
            //Marker has already been added, so just change its location.
            destinoMarker.setPosition(clickedLocation);
        }
        //Get the marker's location.
        markerLocation();
    });

    $(document).ready(function() { Start();  });
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
            function(response, status) {
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

function CalcDistance(result) {
    var total = 0;
    var myroute = result.routes[0];
    for (var i = 0; i < myroute.legs.length; i++) {
        total += myroute.legs[i].distance.value;
    }
    total = total / 1000;
    $('#nDistancia').val(total);
    CalcCosts();
}

function CalcCosts() {
    $("#nCostoKm").val(($("#nCostoTotal").val() / $('#nDistancia').val()).toFixed(2));
}

function Start () {
    var vregistrar = new vRegistrar();
    vregistrar.FillComboBoxes();
    vregistrar.GetTarifa();

    $("#cbTarifa").change(function () {
        var vregistrar = new vRegistrar();
        vregistrar.GetTarifa();
    });

    var terminal = JSON.parse(sessionStorage.getItem("list-cbTerminal")).find(terminal => terminal.Id == $("#cbTerminal").val());

    terminalMarker = new google.maps.Marker({
        position: new google.maps.LatLng(terminal.Latitude, terminal.Longitude),
        title: terminal.TerminalName,
        map: map,
        icon: terminalIcon
    });

    $("#cbTerminal").change(function () {
        var terminal = JSON.parse(sessionStorage.getItem("list-cbTerminal")).find(terminal => terminal.Id == $("#cbTerminal").val());
        terminalMarker.setPosition(new google.maps.LatLng(terminal.Latitude, terminal.Longitude));
        terminalMarker.setTitle(terminal.TerminalName);
        calcRoute();
    });

    $("#cbEmpresa").change(function () {
        var vregistrar = new vRegistrar();
        vregistrar.FillLineas();
    });

    $("#frmEdition").validate({
        rules: {
            RutaName: {
                required: true,
                minlength: 3,
                maxlength: 50
            },
            RutaDescripcion: {
                required: true,
                minlength: 3,
                maxlength: 100
            },
            TerminalId: {
                required: true,
                min: 1
            },
            LineaId: {
                required: true,
                minlength: 3
            },
            TarifaId: {
                required: true,
                min: 1
            },
            Distancia: {
                required: true,
                min: 1
            },
            DestinoLatitude: {
                required: true,
                number: true,
                minlength: 10
            },
            DestinoLongitude: {
                required: true,
                number: true,
                minlength: 10
            }
        },
        messages: {
            RutaName: {
                required: "Por favor ingrese el nombre de la ruta",
                minlength: "Debe digitar mínimo 3 caracteres"
            },
            RutaDescripcion: {
                required: "Por favor ingrese la descripción",
                minlength: "Debe digitar mínimo 3 caracteres"
            },
            TerminalId: {
                required: "Campo obligatorio",
                min: "Campo obligatorio"
            },
            LineaId: {
                required: "Campo obligatorio",
                min: "Campo obligatorio"
            },
            TarifaId: {
                required: "Campo obligatorio",
                min: "Campo obligatorio"
            },
            Distancia: {
                required: "Campo obligatorio",
                min: "Campo obligatorio"
            },
            DestinoLatitude: {
                required: "Por favor selecione en el mapa.",
                minlength: "Por favor ingrese mínimo 10 digitos",
                number: "El valor digitado debe ser numerico"
            },
            DestinoLongitude: {
                required: "Por favor selecione en el mapa.",
                minlength: "Por favor ingrese mínimo 10 digitos",
                number: "El valor digitado debe ser numerico"
            }
        },

        errorElement: "label",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            error.insertAfter(element);

        },
        highlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-valid");
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-invalid");
            $(element).addClass("is-valid");
        }
    });
}