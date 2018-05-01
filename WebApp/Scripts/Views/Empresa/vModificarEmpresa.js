function verEmpresa() {
    let nombre = document.querySelector('#txtNombre');
    let cedula = document.querySelector('#txtCedulaJuridica');
    let telefono = document.querySelector('#txtNumContacto');
    let correo = document.querySelector('#txtCorreo');

    let data = JSON.parse(sessionStorage.getItem("listaEmpresa_selected"));

    nombre.disabled = true;
    nombre.value = data.NombreEmpresa;

    cedula.disabled = true;
    cedula.value = data.CedulaJuridica;

    telefono.value = data.Telefono;

    correo.value = data.EmailEncargado;
}

function vModificarEmpresa() {
    this.ctrlActions = new ControlActions();
    this.service = "Empresa/UpdateEmpresa";

    if (!$("#frmEdition").valid()) {
        this.ctrlActions.ShowMessage("E", "Por favor verifique los campos señalados");
        return;
    }

    else {
        this.Create = function () {
            let terminal = JSON.parse(sessionStorage.getItem("listaEmpresa_selected"));
            var empresaData = {};

            empresaData = this.ctrlActions.GetDataForm('frmEdition');

            this.ctrlActions.PutToAPI(this.service, empresaData, function () {
                window.location.replace("/Empresa/vListarEmpresa");
            });
        }
    }
}

$(document).ready(function () {
    verEmpresa();
    $("#frmEdition").validate({
        rules: {
            Telefono: {
                required: true,
                minlength: 8
            },
            EmailEncargado: {
                required: true,
                minlength: 5
            }
        },
        messages: {
            Telefono: {
                required: "Ingrese el teléfono",
                minlength: "Debe digitar mínimo 8 caracteres"
            },
            EmailEncargado: {
                required: "Ingrese el correo electrónico del encargado",
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
});