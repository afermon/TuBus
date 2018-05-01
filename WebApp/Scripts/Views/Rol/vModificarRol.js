function verRol() {
    let idRol = document.querySelector('#txtNombre');
    let descripcion = document.querySelector('#txtDescripcion');
    let homePage = jQuery('#txtHomePage');
    let terminal = JSON.parse(sessionStorage.getItem("listaRoles_selected"));

    idRol.disabled = true;
    idRol.value = terminal.RoleId;
    descripcion.value = terminal.Descripcion;
    homePage.val(terminal.Homepage);
}

function LoadElements() {
    var control = new ControlActions();
    let role = JSON.parse(sessionStorage.getItem("listaRoles_selected"));

    control.FillComboBox("Vista/ObtenerVistas", "Vistas", "VistaId", "Nombre");
}


function vModificarRol() {
    this.ctrlActions = new ControlActions();
    this.service = "Role/ModificarRol";

    if (!$("#frmEdition").valid()) {
        this.ctrlActions.ShowMessage("E", "Por favor verifique los campos señalados");
        return;
    }

    else {
        this.Modificar = function () {
            let terminal = JSON.parse(sessionStorage.getItem("listaRoles_selected"));
            var terminalData = {};

            terminalData = this.ctrlActions.GetDataForm('frmEdition');

            this.ctrlActions.PutToAPI(this.service, terminalData, function () {
                window.location.replace("/Rol/vListaRoles");
            });
        }
    }
}

$(document).ready(function () {
    verRol();
    LoadElements();

    $("#frmEdition").validate({
        rules: {
            Nombre: {
                required: true,
                minlength: 1,
                maxlength: 50
            },
            HomePage: {
                required: true,
                minlength: 1
            },
            Descripcion: {
                required: true,
                minlength: 1,
                maxlength: 100
            }
        },
        messages: {
            Nombre: {
                required: "Ingrese el nombre del rol",
                minlength: "Debe digitar mínimo 1 caracter",
                maxlength: "Debe digitar máximo 50 caracteres"
            },
            HomePage: {
                required: "Ingrese el nombre de la página principal",
                minlength: "Por favor ingrese mínimo 1 caracter"
            },
            Descripcion: {
                required: "Ingrese la descripción de la terminal",
                minlength: "Por favor ingrese mínimo 1 caracter",
                maxlength: "Debe digitar máximo 100 caracteres"
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