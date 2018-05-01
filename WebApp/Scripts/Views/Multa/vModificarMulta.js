let multa = JSON.parse(sessionStorage.getItem("listamulta_selected"));

function vModificarMulta() {
    this.ctrlActions = new ControlActions();
    this.service = "Multa/ModificarMulta";

    if (!$("#frmEdition").valid()) {
        this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
        return;
    } else {
        this.Update = function () {
            var multaData = {};
            multaData = this.ctrlActions.GetDataForm('frmEdition');

            var newMulta = {
                "Id": multa.Id,
                "Empresa": $("#Empresas").val(),
                "Monto": multaData.Monto,
                "Fecha": multaData.Fecha,
                "Detalle": multaData.Detalle,
                "Estado": multa.Estado
            };
            //Hace el post al update
            this.ctrlActions.PutToAPI(this.service, newMulta, function () {
                window.location.replace("/Multa");
            });
        }
    }
}

function LoadElements() {
    var control = new ControlActions();
    control.FillComboBox("Empresa/GetAll", "Empresas", "CedulaJuridica", "NombreEmpresa");
    $("#Empresas").val(multa.Empresa);
    $("#nMonto").val(multa.Monto);
    $("#datetimepick").val(multa.Fecha);
    $("#txtDetalle").val(multa.Fecha);
}

$(document).ready(function () {
    LoadElements();
    $("#frmEdition").validate({
        rules: {
            Empresas: {
                required: true
            },
            Monto: {
                required: true,
                number: true,
                minlength: 1
            },
            Detalle: {
                required: true
            }
        },
        messages: {
            NombreLinea: {
                Empresas: "Por favor seleccione una empresa"
            },
            Monto: {
                required: "Por ingrese el monto de la multa",
                minlength: "Por favor ingrese mínimo 1 dígito",
                number: "El valor digitado debe ser numérico",
            },
            Detalle: {
                required: "Por favor ingrese la razón de la multa"
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