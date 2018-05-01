let chofer = JSON.parse(sessionStorage.getItem("listachofer_selected"));

function vModificarChofer() {
    this.ctrlActions = new ControlActions();
    this.service = "Chofer/ModificarChofer";

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
    }

    if (!$("#frmEdition").valid()) {
        this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
        return;
    } else {
        this.Update = function () {
            var choferData = {};
            choferData = this.ctrlActions.GetDataForm('frmEdition');

            var newChofer = {
                "Cedula": choferData.Cedula,
                "Nombre": choferData.Nombre,
                "Apellidos": choferData.Apellidos,
                "Telefono": choferData.Telefono,
                "Correo": choferData.Correo,
                "FechaNacimiento": choferData.FechaNacimiento,
                "Edad": choferData.Edad,
                "NumeroLicencia": choferData.NumeroLicencia,
                "FechaExpiracion": choferData.FechaExpiracion,
                "Empresa": choferData.Empresa,
                "Estado": chofer.Estado
            };
            //Hace el post al update
            this.ctrlActions.PutToAPI(this.service, newChofer, function () {
                window.location.replace("/Chofer");
            });
        }
    }

}

function LoadElements() {
    var control = new ControlActions();
    control.FillComboBox("Empresa/GetAll", "cbEmpresa", "CedulaJuridica", "NombreEmpresa");
    $("#cbEmpresa").val(chofer.Empresa);
}

$(document).ready(function () {
    LoadElements();
    var vchofer = new vModificarChofer();
    vchofer.BindFields(chofer);

    $("#frmEdition").validate({
        rules: {
            Cedula: {
                required: true,
                identificacion: true
            },
            Nombre: {
                required: true,
                minlength: 4,
                maxlength: 50
            },
            Apellidos: {
                required: true,
                minlength: 4,
                maxlength: 50
            },
            Telefono: {
                required: true,
                number: true,
                rangelength: [8, 8]
            },
            Correo: {
                required: true,
                email: true
            },
            FechaNacimiento: {
                required: true,
                date: true,
                edad: true
            },
            NumeroLicencia: {
                required: true,
                licencia: true
            },
            FechaExpiracion: {
                required: true,
                date: true,
                expiracion: true
            }, Empresa: {
                required: true
            }



        },
        messages: {
            Cedula: {
                required: "Por favor ingrese su identificación.",
                minlength: "Por favor ingrese mínimo 9 dígitos.",
                identificacion: "Ingrese una cedula o pasaporte válidos. (d-dddd-dddd)"
            },
            Nombre: {
                required: "Por favor ingrese la latitud de la terminal",
                minlength: "Por favor ingrese mínimo 4 digitos",
                maxlength: "Por favor ingrese menos de 50 dígitos"
            },
            Apellidos: {
                required: "Por favor ingrese la latitud de la terminal",
                minlength: "Por favor ingrese mínimo 4 digitos",
                maxlength: "Por favor ingrese menos de 50 dígitos"
            },
            Telefono: {
                required: "Por favor ingrese su teléfono.",
                rangelength: "Por favor ingrese un teléfono móvil."
            },
            Correo: {
                required: "Por favor ingrese su correo",
                email: "El formato debe ser de correo"
            },
            FechaNacimiento: {
                required: "Por favor ingrese una fecha válida.",
                date: "Por favor ingrese una fecha válida.",
                edad: "Por favor ingrese una fecha válida."
            },
            NumeroLicencia: {
                required: "Por favor ingrese el número de licencia",
                licencia: "Porfavor ingrese una licencia válida"
            },
            FechaExpiracion: {
                required: "Por favor ingrese una fecha válida.",
                date: "Por favor ingrese una fecha válida.",
                expiracion: "La fecha de expiración debe ser mayor a 6 meses."
            },
            Empresa: {
                required: "Debe seleccionar una empresa"
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

    $.validator.addMethod("identificacion", function (value) {
        return /^[0-9]-[0-9]{4}-[0-9]{4}$/.test(value) // cedula
            || /^(?!^0+$)[a-zA-Z0-9]{3,20}$/.test(value) // pasaporte
    });

    $.validator.addMethod("licencia", function (value) {
        return /^[a-zA-Z]{2}[0-9]{9}$/.test(value) // licencia
    });

    $.validator.addMethod("edad", function (value) {
        var years = moment().diff(value, 'years', false);
        $("#txtEdad").val(years);
        return (years > 1) && (years < 100);
    });

    $.validator.addMethod("expiracion", function (value) {
        var years = moment().diff(value, 'months', false);
        return (years < -6);
    });

});