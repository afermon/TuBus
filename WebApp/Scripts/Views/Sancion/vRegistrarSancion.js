function vRegistrarSancion()
{
    this.service = "Sancion/RegistrarSancion";
    this.ctrlActions = new ControlActions();

    this.Create = function () {
        if (!$("#frmEdition").valid()) {
            this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
        } else {

            var sancionData = {};
            sancionData = this.ctrlActions.GetDataForm('frmEdition');
            sancionData.Suspencion = $('#chbSuspencion')[0].checked ? "Activo" : "Inactivo";

            //Hace el post al create
            this.ctrlActions.PostToAPI(this.service, sancionData, function () {
                window.location.replace("/Sancion");
            });
           
        }
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields("frmEdition", data);
    }

    this.FillComboBoxes = function () {
        this.ctrlActions.FillComboBox("Terminal/ObtenerTerminales", "cbTerminales", "Id", "TerminalName");
        this.ctrlActions.FillComboBox("Empresa/GetAll", "cbEmpresa", "CedulaJuridica", "NombreEmpresa");
    }
}

//ON DOCUMENT READY
$(document).ready(function () {

    var vregistrarsancion = new vRegistrarSancion();
    vregistrarsancion.FillComboBoxes();

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