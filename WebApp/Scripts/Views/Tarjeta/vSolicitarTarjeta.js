function vSolicitarTarjeta() {
    this.ctrlActions = new ControlActions();
    this.CardService = "Tarjeta/InitializeCard";
    this.Create = function () {
        if (!$("#frmIniciarTarjeta").valid()) {
            this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
            return;
        }
        var formDataUser = {};
        formDataUser = this.ctrlActions.GetDataForm('frmIniciarTarjeta');
      
        var nuevaTarjeta = {
            "Terminal": {
                "Id": $("#Terminales").val()
            },
            "Usuario": {
                "Email": formDataUser.UserMail,
                "Nombre": formDataUser.UserName
            },
            "TipoTarjeta": {
                "TipoTarjetaId": $("#TarjetasTipos").val()
            }
        };

        this.ctrlActions.PostToAPI(this.CardService, nuevaTarjeta, function () {
            window.location.replace("/Tarjeta");
        });
    }
}

var globalResource = {};

function LoadElements() {
    var control = new ControlActions();
    control.FillComboBox("Terminal/ObtenerTerminales", "Terminales", "Id", "TerminalName");
    var authUser = new Auth();
    var user = authUser.GetCurrentUser();
    if (user.TerminalId != -1 && user.terminalid != 0) {
        $("#Terminales").val(user.TerminalId);
        $("#Terminales").attr('disabled', true);
    }
    control.FillComboBox("TipoTarjeta/GetAllTypes/", "TarjetasTipos", "TipoTarjetaId", "Nombre");

}

$(document).ready(function () {
    $("#frmIniciarTarjeta").validate({
        rules: {
            UserName: {
                required: true
            },
            UserMail: {
                required: true,
                email: true
            }
        },
        messages: {
            UserName: {
                required: "Por favor ingrese el nombre del usuario"
            },
            UserMail: {
                required: "Por favor ingrese un correo válido",
                email: "Correo no válido"
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