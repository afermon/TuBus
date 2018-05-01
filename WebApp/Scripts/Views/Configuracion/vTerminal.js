function vTerminal() {
    this.terminal = JSON.parse(sessionStorage.getItem("listaterminal_selected"));
    this.service = 'Configuracion/Terminal/' + this.terminal.Id;
    this.ctrlActions = new ControlActions();

    this.Retrieve = function() {
        var jqxhr = $.get({
            url: this.ctrlActions.GetUrlApiService(this.service),
            headers: { 'Authorization': auth.GetAccessToken() }
        }).done(function (response) {
            var vterminal = new vTerminal();
            vterminal.BindFields(response.Data);
        }).fail(function (response) {
            var data = response.responseJSON;
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('E', data.ExceptionMessage);
            console.log(data);
        });
    }

    this.Update = function() {
        if (!$("#frmConfiguracionTerminal").valid()) {
            this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
        } else {
            var data = {};
            data = this.ctrlActions.GetDataForm('frmConfiguracionTerminal');
            //Hace el put al API
            this.ctrlActions.PutToAPI(this.service, data, function () {
                window.location.replace("/Terminal/vListaTerminales");
            });
        }
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmConfiguracionTerminal', data);
    }
}

//ON DOCUMENT READY
$(document).ready(function () {
    var vterminal = new vTerminal();
    vterminal.Retrieve();

    $("#frmConfiguracionTerminal").validate({
        rules: {
            CantidadQuejasSancion: {
                required: true,
                minlength: 1,
                min: 1,
                number: true
            },
            CostoParqueoHora: {
                required: true,
                minlength: 2,
                min: 1,
                number: true
            },
            CostoParqueoDia: {
                required: true,
                minlength: 2,
                min: 1,
                number: true
            },
            MontoInicialTarjeta: {
                required: true,
                minlength: 2,
                min: 1,
                number: true
            },
            CostoParqueoBusMes: {
                required: true,
                minlength: 2,
                min: 1,
                number: true
            },
            CantidadTardiasSancion: {
                required: true,
                minlength: 2,
                min: 1,
                number: true
            },
            CantidadMinutosTardia: {
                required: true,
                minlength: 2,
                min: 1,
                number: true
            }
        },
        messages: {
            CantidadQuejasSancion: {
                required: "Por favor digite un valor válido.",
                minlength: "Por favor ingrese mínimo 1 dígito.",
                min: "Por favor ingrese un numero mayor."
            },
            CostoParqueoHora: {
                required: "Por favor digite un valor válido.",
                minlength: "Por favor ingrese mínimo 1 dígito.",
                min: "Por favor ingrese un numero mayor."
            },
            CostoParqueoDia: {
                required: "Por favor digite un valor válido.",
                minlength: "Por favor ingrese mínimo 1 dígito.",
                min: "Por favor ingrese un numero mayor."
            },
            MontoInicialTarjeta: {
                required: "Por favor digite un valor válido.",
                minlength: "Por favor ingrese mínimo 1 dígito.",
                min: "Por favor ingrese un numero mayor."
            },
            CostoParqueoBusMes: {
                required: "Por favor digite un valor válido.",
                minlength: "Por favor ingrese mínimo 1 dígito.",
                min: "Por favor ingrese un numero mayor."
            },
            CantidadTardiasSancion: {
                required: "Por favor digite un valor válido.",
                minlength: "Por favor ingrese mínimo 1 dígito.",
                min: "Por favor ingrese un numero mayor."
            },
            CantidadMinutosTardia: {
                required: "Por favor digite un valor válido.",
                minlength: "Por favor ingrese mínimo 1 dígito.",
                min: "Por favor ingrese un numero mayor."
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
});
