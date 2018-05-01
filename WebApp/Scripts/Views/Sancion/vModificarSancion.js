let sancion = JSON.parse(sessionStorage.getItem("listaSanciones_selected"));

function LoadElements() {
    var control = new ControlActions();
    control.FillComboBox("Terminal/ObtenerTerminales", "cbTerminales", "Id", "TerminalName");
    control.FillComboBox("Empresa/GetAll", "cbEmpresa", "CedulaJuridica", "NombreEmpresa");

    let chbSuspencion = document.querySelector('#chbSuspencion');

    if (sancion.Suspencion == "Activo") {
        chbSuspencion.checked = true;
    } else {
        chbSuspencion.checked = false;
    }

    $("#nId").val(sancion.Id);
    $("#txtSancion").val(sancion.Descripcion);
    $("#nMulta").val(sancion.Multa);
    $("#cbTerminales").val(sancion.TerminalId);
    $("#datetimepick").val(sancion.Fecha);
    $("#datetimepick2").val(sancion.FechaReactivacion);
    $("#cbEmpresa").val(sancion.Empresa);
}

function vModificarSancion() {
    this.service = "Sancion/ModificarSancion";
    this.ctrlActions = new ControlActions();

    this.Create = function () {
        if (!$("#frmEdition").valid()) {
            this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
        } else {
            
            var sancionData = {};
            sancionData = this.ctrlActions.GetDataForm('frmEdition');

            var newSancion = {
                "Id": sancion.Id,
                "Descripcion": sancionData.Descripcion,
                "Multa": sancionData.Multa,
                "Estado": sancion.Estado,
                "TerminalId": $("#cbTerminales").val(),
                "Fecha": sancionData.Fecha,
                "Suspencion": sancionData.Suspencion = $('#chbSuspencion')[0].checked ? "Activo" : "Inactivo",
                "FechaReactivacion": sancionData.FechaReactivacion,
                "Empresa": $("#cbEmpresa").val(),
            };
                                    
            //Hace el post al put
            this.ctrlActions.PutToAPI(this.service, newSancion, function () {
                window.location.replace("/Sancion");
            });
        }
    }

}

//ON DOCUMENT READY
$(document).ready(function () {

    var vmodificarsancion = new vModificarSancion();
    LoadElements();

    $("#frmEdition").validate({

        rules: {
            Descripcion: {
                required: true,
                maxlength: 100
            },
            Multa: {
                required: true,
                number: true
            },
            TerminalId: {
                required: true
            },
            Empresa: {
                required: true,
                maxlength: 50
            }


        },
        messages: {
            Descripcion: {
                required: "Por favor ingrese el detalle de la sanción",
                maxlength: "No puede digitar más de 100 caracteres"
            },
            Multa: {
                required: "Por favor digite una multa",
                number: "La multa debe ser un número"
            },
            TerminalId: {
                required: "Porfavor seleccione una terminal"
            },
            Empresa: {
                required: "Porfavor digite el nombre de la empresa",
                maxlength: "No puede digitar más de 50 caracteres"
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