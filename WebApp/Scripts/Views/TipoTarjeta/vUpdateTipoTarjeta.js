function vUpdateTipoTarjeta() {
    this.ctrlActions = new ControlActions();
    this.service = "TipoTarjeta/Update";

    if (!$("#frmTipoTarjeta").valid()) {
        this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
        return;
    } else {
        this.Update = function () {
            var tipoTarjeta = {};
            tipoTarjeta = this.ctrlActions.GetDataForm('frmTipoTarjeta');
            let  tipoTarjetaStored = JSON.parse(sessionStorage.getItem("listaTiposTarjetas_selected"));
            tipoTarjeta.TipoTarjetaId = tipoTarjetaStored.TipoTarjetaId;
            //Hace el post al create
            this.ctrlActions.PutToAPI(this.service, tipoTarjeta);
            alert("Tipo de tarjeta actualizado");
            $("#acept").click(function () { window.location.replace("/TiposTarjetas") });
        }
    }
}

function LoadElements() {
    let tipoTarjeta = JSON.parse(sessionStorage.getItem("listaTiposTarjetas_selected"));
    $("#txtNombre").val(tipoTarjeta.Nombre);
    $("#txtDescuento").val(tipoTarjeta.DiscountPercentage);
}


$(document).ready(function () {
    LoadElements();
    $("#frmTipoTarjeta").validate({
        rules: {
            Nombre: {
                required: true
            },
            DiscountPercentage: {
                required: true,
                number: true,
                minlength: 1,
                maxlength: 3
            }
        },
        messages: {
            Nombre: {
                required: "Por favor ingrese el nombre del tipo de tarjeta"
            },
            DiscountPercentage: {
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