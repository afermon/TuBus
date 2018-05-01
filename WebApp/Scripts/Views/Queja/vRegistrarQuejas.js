function vRegistrarQuejas() {

    this.service = "Queja/RegistrarQueja";
    this.ctrlActions = new ControlActions();
        
        this.Create = function () {
            if (!$("#frmEdition").valid()) {
                this.ctrlActions.ShowMessage('E', "Por favor verifique los campos señalados.");
            } else {

                var quejaData = {};
                quejaData = this.ctrlActions.GetDataForm('frmEdition');
                if (!$("#chbChofer").is(':checked'))
                    quejaData.Chofer = "";
                if ($("#cbChofer").val() == null)
                {
                    quejaData.Chofer = "";
                }
                if ($("#txtPlaca").val() == null)
                {
                    quejaData.Placa = "";
                }
                //Hace el post al create
                this.ctrlActions.PostToAPI(this.service, quejaData, function () {
                    var userAuth = new Auth();
                    var user = userAuth.GetCurrentUser();
                    if (user.RoleId === "ADMINISTRADOR")
                        window.location.replace("/Quejas/vListarQuejas");
                });
            }
        }
    


    this.BindFields = function (data) {
        this.ctrlActions.BindFields("frmEdition", data);
    }

    this.FillComboBoxes = function () {
   
        this.ctrlActions.FillComboBox("Ruta/GetAll", "cbRutas", "Id", "RutaName");
       
    }

}

function LoadChofer() {
    this.ctrlActions = new ControlActions();
    var quejaData = {};
    quejaData = this.ctrlActions.GetDataForm('frmEdition');
    this.ctrlActions.FillComboBox("Chofer/ObtenerChoferRuta?Ruta=" + quejaData.Ruta, "cbChofer", "Cedula", "NombreCompleto");

}

//ON DOCUMENT READY

$(document).ready(function () {

    var vregistrarquejas = new vRegistrarQuejas();
    vregistrarquejas.FillComboBoxes();

    $("#chbChofer").change(function () {
        if ($("#chbChofer").is(':checked')) {
            LoadChofer();
            $("#comboChofer").removeClass("d-none");            
        }
        else
        {
            $("#comboChofer").addClass("d-none");
        } 
    });

    $("#cbRutas").change(function () {
        LoadChofer();
    });

    $("#frmEdition").validate({

        rules: {
            DetalleQueja: {
                required: true,
                minlength: 10
            },
            Ruta: {
                required: true
             
            }

        },
        messages: {
            DetalleQueja: {
                required: "Por favor ingrese el detalle de la queja",
                minlength: "Debe digitar mínimo 50 caracteres"
            },
            Ruta: {
                required: "Por favor seleccione una ruta"
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


