function vUpdateLinea() {
    this.ctrlActions = new ControlActions();
    this.service = "Linea/Update";

    if (!$("#frmLinea").valid()) {
        this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
        return;
    } else {
        this.Update = function () {
            var liena = {};
            liena = this.ctrlActions.GetDataForm('frmLinea');
            let lineaSelected = JSON.parse(sessionStorage.getItem("listaLineas_selected"));
            var newLine = {
                "LineaId": lineaSelected.LineaId,
                "NombreLinea": liena.NombreLinea,
                "EspaciosParqueo": liena.EspaciosParqueo,
                "Terminal": {
                    "Id": $("#Terminales").val()
                },
                "Empresa": {
                    "CedulaJuridica": $("#Empresas").val()
                }
            };
            //Hace el post al create
            this.ctrlActions.PutToAPI(this.service, newLine, function() {
                 window.location.replace("/Linea");
            });
        }
    }
}

function LoadElements() {
    var control = new ControlActions();
    control.FillComboBox("Terminal/ObtenerTerminales", "Terminales", "Id", "TerminalName");
    control.FillComboBox("Empresa/GetAll", "Empresas", "CedulaJuridica", "NombreEmpresa");
    let linea = JSON.parse(sessionStorage.getItem("listaLineas_selected"));
    var resultUrl = control.GetUrlApiService("linea/Espacios?terminal=") + linea.Terminal.Id;
    getData(resultUrl, LoadSpacios);
    $("#Terminales").change(ReloadSpace);
    $("#txtNombre").val(linea.NombreLinea);
    $("#txtEspacio").val(linea.EspaciosParqueo);
    $("#Terminales").val(linea.Terminal.Id);
    $("#Empresas").val(linea.Empresa.CedulaJuridica);
    var authUser = new Auth();
    var empresa = authUser.GetCurrentEmpresa();
    if (empresa != null) {
        $("#Empresas").val(empresa.CedulaJuridica);
        $("#Empresas").attr('disabled', true);
    }
}

function ReloadSpace() {
    var control = new ControlActions();
    var resultUrl = control.GetUrlApiService("linea/Espacios?terminal=") + $("#Terminales").val();
    getData(resultUrl, LoadSpacios);
}

function LoadSpacios(response) {
    var lableEspacios = $("label[for='txtEspacio']");
    lableEspacios.text("Espacios a alquilar (Disponibles: " + response.Data + "):");
}

$(document).ready(function () {
    LoadElements();
    $("#frmLinea").validate({
        rules: {
            NombreLinea: {
                required: true
            },
            EspaciosParqueo: {
                required: true,
                number: true,
                minlength: 1
            }
        },
        messages: {
            NombreLinea: {
                required: "Por favor ingrese el nombre de la línea"
            },
            EspaciosParqueo: {
                required: "Por ingrese la cantidad de espacios a alquilar",
                minlength: "Por favor ingrese mínimo 1 dígito",
                number: "El valor digitado debe ser numérico",
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

function getData(urlToSend, callback) {
    $.ajax({
        cache: false,
        type: "GET",
        url: urlToSend,
        success: function (result) {
            callback(result);
        },
        error: function (result) {
            console.log(result);
            alert(result.Message);
        }
    });
}