function vForgotPassword() {

    this.serviceToken = 'Usuario/ResetToken';
    this.serviceContrasena = 'Usuario/ResetPassword';
    this.ctrlActions = new ControlActions();

    this.checkToken = function() {
        if (this.ctrlActions.GetUrlParameter("token")) {
            $("#frmContrasena").removeClass("d-none");
        } else {
            $("#frmGetResetToken").removeClass("d-none");
        }
    }

    this.Restablecer = function () {
        if ($("#frmGetResetToken").valid()) {
            var data = {};
            data = this.ctrlActions.GetDataForm('frmGetResetToken');
            this.ctrlActions.PostToAPI(this.serviceToken, data, function() {
                window.location.replace("/Auth/vLogin");
            });
        }
    }

    this.UpdateContrasena = function () {
        if ($("#frmContrasena").valid()) {
            var data = {};
            data = this.ctrlActions.GetDataForm('frmContrasena');
            data["ResetToken"] = this.ctrlActions.GetUrlParameter("token");
            data["Email"] = this.ctrlActions.GetUrlParameter("email");
            //Hace el post al API
            this.ctrlActions.PostToAPI(this.serviceContrasena, data, function () {
                window.location.replace("/Auth/vLogin");
            });
        }
    }
}

//ON DOCUMENT READY
$(document).ready(function () {
    var vreset = new vForgotPassword();
    vreset.checkToken();

    $("#frmGetResetToken").validate({
        rules: {
            Email: {
                required: true,
                email: true
            }
        },
        messages: {
            Email: {
                required: "Por favor ingrese su correo.",
                email: "Por favor ingrese su correo."
            }
        },

        errorElement: "div",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            error.insertAfter(element);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-valid");
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-invalid");
            $(element).addClass("is-valid");
        }
    });

    $("#frmContrasena").validate({
        rules: {
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
            Password: {
                required: "Por favor ingrese una contraseña.",
                minlength: "Por favor ingrese mínimo 6 caractéres.",
                pwcheck: "La contraseña debe ser alfanumérica."
            },
            PasswordRep: {
                equalTo: "Las contraseñas no coinciden."
            }
        },

        errorElement: "div",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            error.insertAfter(element);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-valid");
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-invalid");
            $(element).addClass("is-valid");
        }
    });

    $.validator.addMethod("pwcheck", function (value) {
        return /^[A-Za-z0-9\d=!\-@._*]*$/.test(value) // consists of only these
            && /[a-z]/.test(value) // has a lowercase letter
            && /\d/.test(value) // has a digit
    });
});