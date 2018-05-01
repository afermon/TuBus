function vRegistrarBus() {
    this.ctrlActions = new ControlActions();
    this.serviceBus = "Bus/RegistrarBus";

    if (!$("#frmEdition").valid()) {
        this.ctrlActions.ShowMessage("E", "Por favor verifique los campos señalados");
        return;
    } else {
        this.Create = function () {
            var busData = {};
            var requisitos = [];

            busData = this.ctrlActions.GetDataForm('frmEdition');

            requisitos.push({ 'Permiso': 'Seguro', 'Estado': document.getElementById("chbSeguro").checked ? 'Activo' : 'Pendiente', 'Placa': $("#txtPlaca").val() });
            requisitos.push({ 'Permiso': 'Marchamo', 'Estado': document.getElementById("chbMarchamo").checked ? 'Activo' : 'Pendiente', 'Placa': $("#txtPlaca").val() });
            requisitos.push({ 'Permiso': 'RTV', 'Estado': document.getElementById("chbRtv").checked ? 'Activo' : 'Pendiente', 'Placa': $("#txtPlaca").val() });

            busData.Requisitos = requisitos;

            this.ctrlActions.PostToAPI(this.serviceBus, busData, function () {
                window.location.replace("/Bus/vListaBuses");
            });
        }
    }

    this.FillComboBox = function () {
        var empresaData = JSON.parse(sessionStorage.getItem("currentEmpresa"));

        if (empresaData !== null && typeof empresaData !== "undefined") {
            $('#Empresa').append(new Option(empresaData.NombreEmpresa, empresaData.CedulaJuridica, true, true));
        }
        else {
            this.ctrlActions.FillComboBox("Empresa/GetAll", "Empresa", "CedulaJuridica", "NombreEmpresa");
        }
    }
}

$(document).ready(function () {
    var vregistrar = new vRegistrarBus();
    vregistrar.FillComboBox();

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