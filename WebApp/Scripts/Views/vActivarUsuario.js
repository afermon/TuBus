function vActivarUsuario() {
    this.ctrlActions = new ControlActions();
    this.UserService = "TarjetaUsuario/CreateUser";
    this.CardService = "TarjetaUsuario/CreateCard";
    this.Create = function () {
        if (!$("#frmCreate").valid()) {
            alert("Por favor verifique los campos señalados");
            return;
        }
        var formDataUser = {};
            formDataUser = this.ctrlActions.GetDataForm('frmCreate');
        var newUser = {
            Email: formDataUser.Email,
            Password: formDataUser.Password,
            Identificacion: formDataUser.Id,
            Nombre: formDataUser.UserName,
            Apellidos: formDataUser.LastName,
            FechaNacimiento: formDataUser.Date.split("/").reverse().join("-"),
            RoleId: "PASAJERO",
            Estado: "Activo", 
            Telefono: formDataUser.Phone,
            SmsNotificationsMin: formDataUser.SmsNotfication
        }
        this.ctrlActions.PostToAPI(this.UserService, newUser);

        var nuevaTarjeta = {
            CodigoUnico: globalResource.uniqueCode,
            Terminal: {
                Id: globalResource.terminalid
            },
            Usuario: {
                Email: formDataUser.Email
            },
            TipoTarjeta: {
                TipoTarjetaId: globalResource.type
            },
            Convenio: {
                CedulaJuridica: globalResource.agreement
            }
        };

        this.ctrlActions.PostToAPI(this.CardService, nuevaTarjeta);
        alert("El usuario " + formDataUser.Email + " se registrado de manera exitosa y su tarjeta ha sido activada");
        $("#acept").click(function () {window.location.replace("/Auth/vLogin")});
        
    }
}

function GetElementsFromUrl() {
    var url = decodeURIComponent(window.location.href);
    var subQuery = url.split("?");
    if (typeof subQuery[1] == 'undefined') return {};
    return  TuBus.ConvertUrlToObj(subQuery[1]);
}

var globalResource = {};

function LoadElements() {
    var elements = GetElementsFromUrl();
    globalResource = elements;
    $("#txtNombre").val(elements.user);
    $("#txtEmail").val(elements.email);
}

$.validator.addMethod("edad", function (value) {
    var day = value.split("/").reverse().join("-");
    var years = moment().diff(day, 'years', false);
    return (years > 1) && (years < 100);
});

$(document).ready(function () {
    $("#frmCreate").validate({
        rules: {
            UserName: {
                required: true
            },
            LastName: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            Id: {
                required: true,
                minlength: 9,
                number:true
            },
            Date: {
                required: true,
                edad: true
            }, 
            Password: {
                required: true,
                minlength: 6

            },
            VerifiedPassword: {
                equalTo: "#txtPassword"
            },
            Phone : {
                required: true,
                minlength: 8,
                number: true
            },
            SmsNotfication:{
                required: true,
                minlength: 3,
                number: true
            }
        },
        messages: {
            UserName: {
                required: "Por favor ingrese su nombre "
            },
            LastName: {
                required: "Por favor ingrese su apellido"
            },
            Email: {
                required: "Por favor ingrese un correo válido",
                email: "Correo no válido"
            },
            Id: {
                required: "Por favor ingrese su identificación",
                minlength: "Por favor ingrese mínimo 9 dígitos"
            },
            Date: {
                required: "Por favor ingrese una fecha válida",
                edad: "Por favor ingrese una fecha válida."
            },
            Password: {
                required: "Por favor ingrese una contraseña",
                minlength: "Por favor ingrese mínimo 6 caractéres"
            },
            VerifiedPassword: {
                equalTo:"Las contraseñas no coinciden"
            },
            Phone: {
                required: "Por favor ingrese su teléfono",
                minlength: "Por favor ingrese mínimo 8 dígitos"
            },
            SmsNotfication: {
                required: "Por favor digite un valor válido",
                minlength: "Por favor ingrese mínimo 3 dígitos"
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

LoadElements();