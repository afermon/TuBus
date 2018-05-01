function vGeneral() {

    this.service = 'Configuracion/General';
    this.ctrlActions = new ControlActions();

    this.Retrieve = function() {
        var jqxhr = $.get({
            url: this.ctrlActions.GetUrlApiService(this.service),
            headers: { 'Authorization': auth.GetAccessToken() }
        }).done(function (response) {
            var vgeneral = new vGeneral();
            vgeneral.BindFields(response.Data);
        }).fail(function (response) {
            var data = response.responseJSON;
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('E', data.ExceptionMessage);
            console.log(data);
        });
    }

    this.Update = function() {
        if (!$("#frmConfiguracionGeneral").valid()) {
            this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
        } else {
            var data = {};
            data = this.ctrlActions.GetDataForm('frmConfiguracionGeneral');
            //Hace el put al API
            this.ctrlActions.PutToAPI(this.service, data, function () {
                window.location.replace("/Configuracion");
            });
        }
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmConfiguracionGeneral', data);
    }
}

//ON DOCUMENT READY
$(document).ready(function () {
    var vgeneral = new vGeneral();
    vgeneral.Retrieve();

    $("#frmConfiguracionGeneral").validate({
        rules: {
            DiasExpiracionContrasena: {
                required: true,
                minlength: 1,
                min: 1,
                number: true
            },
            CantCaracteresContrasena: {
                required: true,
                minlength: 1,
                min: 6,
                number: true
            },
            CantContrasenasAnteriores: {
                required: true,
                minlength: 1,
                min: 1,
                number: true
            }
        },
        messages: {
            DiasExpiracionContrasena: {
                required: "Por favor digite un valor válido.",
                minlength: "Por favor ingrese mínimo 1 dígito.",
                min: "Por favor ingrese un numero mayor."
            },
            CantCaracteresContrasena: {
                required: "Por favor digite un valor válido.",
                minlength: "Por favor ingrese mínimo 1 dígito.",
                min: "Por favor ingrese un numero mayor."
            },
            CantContrasenasAnteriores: {
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
