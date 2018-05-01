function vSolicitarTarjeta() {
    this.ctrlActions = new ControlActions();
    this.service = "Convenio/SolictudTarjetas";

    if (!$("#frmCreate").valid()) {
        this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
        return;
    } else {
        this.Create = function () {
            var solicitud = {};
            solicitud = this.ctrlActions.GetDataForm('frmCreate');

            var newSolicitud = {
                "NombreUsuario": solicitud.UserName,
                "EmailSolicitante": solicitud.Email,
                "Terminal": {
                    "Id": $("#Terminales").val()
                },
                "Convenio": {
                    "CedulaJuridica": $("#Convenios").val()
                },
                "Emails": $("#txtEmails").tagEditor('getTags')[0].tags 
            };
            //Hace el post al create
            this.ctrlActions.PostToAPI(this.service, newSolicitud, function () {
                window.location.replace("/Home");
            });
        }
    }
}

function LoadElements() {
    var control = new ControlActions();
    control.FillComboBox("Terminal/ObtenerTerminales", "Terminales", "Id", "TerminalName");
    $("#Terminales").change(ReloadSpace);
    control.FillComboBox("Convenio/GetAgreementsByTerminal?terminal=" + $("#Terminales").val(), "Convenios", "CedulaJuridica", "NombreInstitucion");
}

function ReloadSpace() {
    $('#Convenios').empty();
    var control = new ControlActions();
    control.FillComboBox("Convenio/GetAgreementsByTerminal?terminal=" + $("#Terminales").val(), "Convenios", "CedulaJuridica", "NombreInstitucion");
}

function LoadTags() {
    $('#txtEmails').tagEditor({
        delimiter: ', ', /* space and comma */
        placeholder: 'Ingrese los correos eléctronicos'
    });
}


$(document).ready(function () {
    LoadElements();
    LoadTags();
    $("#frmCreate").validate({
        rules: {
            UserName: {
                required: true
            },
            Email: {
                required: true,
                email: true
            }
        },
        messages: {
            UserName: {
                required: "Por favor ingrese el nombre"
            },
            Email: {
                required: "Por ingrese el email",
                email: "Por favor ingrese un correo válido"
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
