function vRegistrarRol() {
    this.ctrlActions = new ControlActions();
    this.serviceRol = "Role/RegistrarRol";
    this.serviceVista = "Role/RegistrarVistaPorRol";

    if (!$("#frmEdition").valid()) {
        this.ctrlActions.ShowMessage("E", "Por favor verifique los campos señalados");
        return;
    } else {
        this.Create = function () {

            var terminalData = {};
            var vistas = [];

            terminalData = this.ctrlActions.GetDataForm('frmEdition');

            for (var x = 0; terminalData.Vistas.length > x; x++) {
                vistas.push({ "VistaId": terminalData.Vistas[x]});
            }

            terminalData.Vistas = vistas;

            this.ctrlActions.PostToAPI(this.serviceRol, terminalData);
            this.ctrlActions.PostToAPI(this.serviceVista, terminalData, function () {
                window.location.replace("/Rol/vListaRoles");
            });
        }
    }
}

function LoadElements() {
    var control = new ControlActions();

    control.FillComboBox("Vista/ObtenerVistas", "Vistas", "VistaId", "Nombre");
}

$(document).ready(function () {
    LoadElements();


    $("#frmEdition").validate({
        rules: {
            RoleId: {
                required: true,
                minlength: 1
            },
            HomePage: {
                required: true
            },
            Descripcion: {
                required: true,
                minlength: 1
            },
            Vistas: {
                required: true
            }
        },
        messages: {
            RoleId: {
                required: "Ingrese el nombre del rol",
                minlength: "Debe digitar mínimo 1 caracter"
            },
            HomePage: {
                required: "Ingrese el nombre de la página principal",
                minlength: "Por favor ingrese mínimo 1 caracter"
            },
            Descripcion: {
                required: "Ingrese la descripción de la terminal",
                minlength: "Por favor ingrese mínimo 1 caracter"
            },
           Vistas: {
                required: "Seleccione las vistas",
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