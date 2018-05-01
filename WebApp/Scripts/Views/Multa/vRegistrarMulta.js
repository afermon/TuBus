function vRegistrarMulta()
{
    this.ctrlActions = new ControlActions();
    this.service = "Multa/RegistrarMulta";

    if (!$("#frmEdition").valid()) {
        this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
        return;
    } else {
        this.Create = function () {
            var multaData = {};
            multaData = this.ctrlActions.GetDataForm('frmEdition');

            var newMulta = {
                "Empresa": $("#Empresas").val(),
                "Monto": multaData.Monto,
                "Fecha": multaData.Fecha,
                "Detalle": multaData.Detalle
            };
            //Hace el post al create
            this.ctrlActions.PostToAPI(this.service, newMulta, function () {
                window.location.replace("/Multa");
            });
        }
    }
}

function LoadElements() {
    var control = new ControlActions();
    control.FillComboBox("Empresa/GetAll", "Empresas", "CedulaJuridica", "NombreEmpresa");
   
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


