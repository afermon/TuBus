function vUpdateConvenio() {
    this.ctrlActions = new ControlActions();
    this.service = "Convenio/Update";

    if (!$("#frmConvenio").valid()) {
        this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
        return;
    } else {
        this.Update = function () {
            var convenio = {};
            convenio = this.ctrlActions.GetDataForm('frmConvenio');
            let convenioStored = JSON.parse(sessionStorage.getItem("listaConvenios_selected"));
          var obj = {
                "DescuentoTarifa": convenio.DescuentoTarifa,
                "NombreInstitucion": convenio.NombreInstitucion,
                "EmailEncargado": convenio.Email,
                "CedulaJuridica": convenio.CedulaJuridica.toString(),
                "TerminalesId": $("#Terminales").val()
            };
          this.ctrlActions.PutToAPI(this.service, obj, function () {
              window.location.replace("/Convenio");
          });
        }
    }
}

function LoadElements() {
    var control = new ControlActions();
    control.FillComboBox("Terminal/ObtenerTerminales", "Terminales", "Id", "TerminalName");
    let convenio = JSON.parse(sessionStorage.getItem("listaConvenios_selected"));
    var cedula = $("#txtCedula");
    cedula.val(convenio.CedulaJuridica);
    cedula.attr('disabled', true);
    $("#txtNombre").val(convenio.NombreInstitucion);
    $("#txtEmail").val(convenio.EmailEncargado);
    $("#txtDescuentoTarifa").val(convenio.DescuentoTarifa);
}


$(document).ready(function () {
    LoadElements();
    $("#frmConvenio").validate({
        rules: {
            CedulaJuridica: {
                required: true,
                number: true,
                minlength: 11
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
                required: "Por ingrese el valor de la cédula jurídica",
                minlength: "Por favor ingrese mínimo 11 dígitos",
                number: "El valor digitado debe ser numérico",
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