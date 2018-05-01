function vRegistrarChofer() {
    this.ctrlActions = new ControlActions();
    this.service = "Chofer/RegistrarChofer";

    this.Create = function () {

        if (!$("#frmEdition").valid()) {
            this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
        } else {

            var choferData = {};
            choferData = this.ctrlActions.GetDataForm('frmEdition');

            //Hace el post al create
            this.ctrlActions.PostToAPI(this.service, choferData, function () {

                window.location.replace("/Chofer");
            });

        }
    }
    
    this.FillComboBoxes = function () {
        this.ctrlActions.FillComboBox("Empresa/GetAll", "cbEmpresa", "CedulaJuridica", "NombreEmpresa");

    }

}

function LoadEmpresa() {
    let empresa = JSON.parse(sessionStorage.getItem("currentEmpresa"));
    var option = $("<option />");
    option.attr("value", empresa.CedulaJuridica);
    option.text(empresa.NombreEmpresa)
    $("#cbEmpresa").append(option);
}

$(document).ready(function () {

    if (JSON.parse(sessionStorage.getItem("currentEmpresa")) == null) {
        var vregistrar = new vRegistrarChofer();
        vregistrar.FillComboBoxes();
    } else {
        LoadEmpresa();
    }

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
                edad: "No puede ser menor a 18 años ni mayor a 100."
            },
            NumeroLicencia: {
                required: "Por favor ingrese el número de licencia",
                licencia: "El formato de la licencia es CI123456789"
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
        return (years > 19) && (years < 100);
    });

    $.validator.addMethod("expiracion", function (value) {
        var years = moment().diff(value, 'months', false);
        return (years < -6);
    });
});