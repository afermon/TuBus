var ctrlActions = new ControlActions();

function vParqueoPublico() {
    this.Retrieve = function () {
        var terminalId = $('#Terminales').val();
        var service = terminalId + '/ParqueoPublico/ObtenerParqueoPorTerminal';

        var jqxhr = $.get({
            url: ctrlActions.GetUrlApiService(service, false),
        }).done(function (response) {
            var parqueo = new vParqueoPublico();

            parqueo.BindFieldsParqueo(response.Data);
            createParkingLotTable(response.Data);

        }).fail(function (response) {
            var data = response.responseJSON;
            var ctrlActions = new ControlActions();

            ctrlActions.ShowMessage('E', data.ExceptionMessage);
            console.log(data);
        });
    }

    this.BindFieldsParqueo = function (data) {
        ctrlActions.BindFields('frmParqueoPublico', data);
    }
}

function loadElements() {
    var control = new ControlActions();
    control.FillComboBox("Terminal/ObtenerTerminales", "Terminales", "Id", "TerminalName");
}

function attachComboEvents() {
    $('#Terminales').change(function () {
        var vParqueo = new vParqueoPublico();
        vParqueo.Retrieve();
    });
}

function createParkingLotTable(data) {
    var totalEspacios = data.EspaciosTotales;
    var disponibles = data.EspaciosDisponibles;
    var ocupados = totalEspacios - disponibles;

    var body = $("#mapaParqueo");
    var tbl = document.createElement('table');
    var tbdy = document.createElement('tbody');

    var cars = ["redCar", "blueCar", "orangeCar"];

    var filas = totalEspacios / 5;

    tbl.setAttribute("class", "table table-hover");

    for (var i = 0; i < filas; i++) {
        var tr = document.createElement("tr");
        tr.setAttribute("class", "table-active");

        for (var j = 0; j < 5; j++) {
            var td = document.createElement("td");
            var carClass = ocupados > 0 ? cars[Math.floor(Math.random() * cars.length)] : "";
            var textToDisplay = ocupados > 0 ? "" : "Disponible";

            td.setAttribute("class", "parkingLot parkingLotTd " + carClass);
            td.innerText = textToDisplay;
            tr.appendChild(td)

            ocupados--;
        }
        tbdy.appendChild(tr);
    }

    tbl.appendChild(tbdy);
    body.html(tbl);
}

$(document).ready(function () {
    loadElements();
    attachComboEvents();

    var vParqueo = new vParqueoPublico();
    vParqueo.Retrieve();
});