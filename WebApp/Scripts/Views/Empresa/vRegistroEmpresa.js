function vRegistrarEmpresa() {
    this.ctrlActions = new ControlActions();
    this.serviceUsuario = "Usuario/Create";

    if (!$("#frmEmpresa").valid() && !$("#frmPerfilUsuario").valid()) {
        return;
    }

    else {
        this.Create = function () {
            var serviceEmpresa = "Empresa/RegistrarEmpresa";
            var ctrlActions = new ControlActions();
            var dataEmpresa = {};
            var dataUsuario = {};

            dataEmpresa = ctrlActions.GetDataForm('frmEmpresa');
            dataUsuario = ctrlActions.GetDataForm('frmPerfilUsuario');

            dataEmpresa.EmailEncargado = dataUsuario.Email;
            dataEmpresa.Telefono = dataUsuario.Telefono;

            dataUsuario.Estado = "Pendiente";
            dataUsuario.RoleId = "EMPRESARIO";

            dataEmpresa.Usuario = dataUsuario;

            ctrlActions.PostToAPI(serviceEmpresa, dataEmpresa, function () {
                if (window.location.href.indexOf("Home") === -1)
                    window.location.replace("/Empresa/vListarEmpresasPendientes");
                else {
                    $("#acept").click(function () { window.location.replace("/Home") });
                }
            });
        }
    }
}

function LoadElements() {
    var control = new ControlActions();

    control.FillComboBox("Empresa/GetAllNombreEmpresas", "Empresa", "Operator", "Operator");
}

$(document).ready(function () {
    LoadElements();

    $("#frmEmpresa").validate({
        rules: {
            NombreEmpresa: {
                required: true,
                minlength: 1
            },
            CedulaJuridica: {
                required: true,
                minlength: 5
            }
        },
        messages: {
            NombreEmpresa: {
                required: "Ingrese el nombre de la empresa",
                minlength: "Debe digitar mínimo 1 caracter"
            },
            CedulaJuridica: {
                required: "Ingrese el nombre de la página principal",
                minlength: "Debe digitar mínimo 5 caracteres"
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

    $("#frmPerfilUsuario").validate({
        rules: {
            Identificacion: {
                required: true,
                identificacion: true
            },
            Nombre: {
                required: true
            },
            Apellidos: {
                required: true
            },
            FechaNacimiento: {
                required: true,
                date: true,
                edad: true
            },
            Telefono: {
                required: true,
                rangelength: [8, 8],
                number: true
            },
            Password: {
                required: true,
                pwcheck: true,
                minlength: 6
            },
            PasswordRep: {
                equalTo: "#txtPassword"
            }
        },
        messages: {
            Identificacion: {
                required: "Ingrese su identificación.",
                minlength: "Ingrese mínimo 9 dígitos.",
                identificacion: "Ingrese una cédula o pasaporte válido. (d-dddd-dddd)"
            },
            Nombre: {
                required: "Ingrese su nombre."
            },
            Apellidos: {
                required: "Ingrese su apellido."
            },
            FechaNacimiento: {
                required: "Por favor ingrese una fecha válida.",
                date: "Ingrese una fecha válida.",
                edad: "Ingrese una fecha válida."
            },
            Telefono: {
                required: "Ingrese su teléfono.",
                rangelength: "Ingrese un teléfono móvil."
            },
            Password: {
                required: "Ingrese una contraseña.",
                minlength: "Ingrese mínimo 6 caracteres.",
                pwcheck: "La contraseña debe ser alfanumérica."
            },
            PasswordRep: {
                equalTo: "Las contraseñas no coinciden."
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

    $.validator.addMethod("edad", function (value) {
        var years = moment().diff(value, 'years', false);
        $("#txtEdad").val(years);
        return (years > 1) && (years < 100);
    });

    $.validator.addMethod("pwcheck", function (value) {
        return /^[A-Za-z0-9\d=!\-@._*]*$/.test(value) // consists of only these
            && /[a-z]/.test(value) // has a lowercase letter
            && /\d/.test(value) // has a digit
    });
});