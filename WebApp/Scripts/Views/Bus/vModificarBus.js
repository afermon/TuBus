function vVer() {
    this.ctrlActions = new ControlActions();
    this.busData = JSON.parse(sessionStorage.getItem("listaBuses_selected"));
    this.service = 'Bus/ObtenerBus';

    this.Retrieve = function () {
        var jqxhr = $.get({
            url: this.ctrlActions.GetUrlApiService(this.service),
            data: { "placa": this.busData.Id },
            headers: { 'Authorization': auth.GetAccessToken() }
        }).done(function (response) {
            var vBus = new vVer();

            vBus.BindFields(response.Data);
        }).fail(function (response) {
            var data = response.responseJSON;
            var ctrlActions = new ControlActions();

            ctrlActions.ShowMessage('E', data.ExceptionMessage);
        });
    }

    this.BindFields = function (data) {
        var index;
        this.ctrlActions.BindFields('frmEdition', data);

        let seguro = document.querySelector('#chbSeguro');
        let marchamo = document.querySelector('#chbMarchamo');
        let rtv = document.querySelector('#chbRtv');

        document.getElementById("txtPlaca").disabled = true;
        document.getElementById("txtEmpresa").disabled = true;

        index = data.Requisitos.map(function (d) { return d['Permiso']; }).indexOf('Seguro');
        seguro.checked = data.Requisitos[index].Estado === "Activo";

        index = data.Requisitos.map(function (d) { return d['Permiso']; }).indexOf('RTV');
        rtv.checked = data.Requisitos[index].Estado === "Activo";

        index = data.Requisitos.map(function (d) { return d['Permiso']; }).indexOf('Marchamo');
        marchamo.checked = data.Requisitos[index].Estado === "Activo";
    }
}

function vModificarBus() {
    this.ctrlActions = new ControlActions();
    this.service = "Bus/ModificarBus";

    if (!$("#frmEdition").valid()) {
        this.ctrlActions.ShowMessage("E", "Por favor verifique los campos señalados");
        return;
    }

    else {
        this.Modificar = function () {
            let terminal = JSON.parse(sessionStorage.getItem("listaBuses_selected"));
            var busData = {};
            var requisitos = [];

            busData = this.ctrlActions.GetDataForm('frmEdition');

            requisitos.push({ 'Permiso': 'Seguro', 'Estado': document.getElementById("chbSeguro").checked ? 'Activo' : 'Pendiente', 'Placa': $("#txtPlaca").val() });
            requisitos.push({ 'Permiso': 'Marchamo', 'Estado': document.getElementById("chbMarchamo").checked ? 'Activo' : 'Pendiente', 'Placa': $("#txtPlaca").val() });
            requisitos.push({ 'Permiso': 'RTV', 'Estado': document.getElementById("chbRtv").checked ? 'Activo' : 'Pendiente', 'Placa': $("#txtPlaca").val() });

            busData.Requisitos = requisitos;

            this.ctrlActions.PutToAPI(this.service, busData, function () {
                    window.location.replace("/Bus/vListaBuses");
            });
        }
    }
}

$(document).ready(function () {
    var verBus = new vVer();
    verBus.Retrieve();

    $("#frmEdition").validate({
        rules: {
            Placa: {
                required: true,
                minlength: 1
            },
            CapacidadSentado: {
                required: true,
                minlength: 1
            },
            CapacidadDePie: {
                required: true,
                minlength: 1
            },
            AsientoDiscapacitado: {
                required: true,
                minlength: 1
            }
        },
        messages: {
            Placa: {
                required: "Ingrese la placa",
                minlength: "Debe digitar mínimo 1 caracter"
            },
            CapacidadSentado: {
                required: "Ingrese la capacidad de personas sentadas",
                minlength: "Debe ingresar mínimo 1 caracter"
            },
            CapacidadDePie: {
                required: "Ingrese la capacidad de personas de pie",
                minlength: "Debe ingresar mínimo 1 caracter"
            },
            AsientoDiscapacitado: {
                required: "Ingrese la cantidad de asientos para discapacitados",
                minlength: "Debe ingresar mínimo 1 caracter"
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