function vCrearConvenio() {
    this.ctrlActions = new ControlActions();
    this.service = "Convenio/create";

    if (!$("#frmConvenio").valid()) {
        this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
        return;
    } else {
        this.Create = function () {
            var convenio = {};
            convenio = this.ctrlActions.GetDataForm('frmConvenio');
            //Hace el post al create
            var obj = {
                "DescuentoTarifa": convenio.DescuentoTarifa,
                "NombreInstitucion": convenio.NombreInstitucion,
                "EmailEncargado": convenio.Email,
                "CedulaJuridica": convenio.CedulaJuridica.toString(),
                "TerminalesId":  $("#Terminales").val()
            };
            this.ctrlActions.PostToAPI(this.service, obj, function() {
                window.location.replace("/Convenio");
            });
        }
    }
}
function LoadElements() {
    var control = new ControlActions();
    control.FillComboBox("Terminal/ObtenerTerminales", "Terminales", "Id", "TerminalName");
    var authUser = new Auth();
    var user = authUser.GetCurrentUser();
    if (user.TerminalId !== -1 && user.terminalid !== 0) {
        $("#Terminales").val(user.TerminalId);
        $("#Terminales").attr('disabled', true);
    }
}

$(document).ready(function () {
    LoadElements();
    $("#frmConvenio").validate({
        rules: {
            CedulaJuridica: {
                required: true,
                number: true,
                minlength: 9,
                maxlength: 9
            },
            NombreInstitucion: {
                required: true
            },
            DescuentoTarifa: {
                required: true,
                number: true,
                minlength: 1,
                maxlength: 3
            }
        },
        messages: {
            CedulaJuridica: {
                required: "Por ingrese la cédula jurídica de la institución",
                minlength: "Por favor ingrese mínimo 9 dígitos",
                number: "El valor digitado debe ser numérico",
                maxlength: "El valor debe de ser menor a 9 digitos"

            },
            NombreInstitucion: {
                required: "Por favor ingrese el nombre de la institución"
            },
            DescuentoTarifa: {
                required: "Por ingrese el porcentaje de descuento",
                minlength: "Por favor ingrese mínimo 1 dígito",
                number: "El valor digitado debe ser numérico",
                maxlength: "El valor debe de ser menor 3 digitos"
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