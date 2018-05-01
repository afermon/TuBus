function vListarMultas() {

    this.tblCustomersId = 'listamulta';
    this.service = 'Multa/ObtenerMultas';
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblCustomersId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblCustomersId, true);
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
    }

    this.ActionControls = function (data) {
        /* Handle actions */
        switch (data.action) {
            case "view":
                window.location.replace("/Multa/vVerMulta");
                break;
            case "edit":
                window.location.replace("/Multa/vModificarMulta");
                break;
            case "delete":

                let multaData = JSON.parse(sessionStorage.getItem("listamulta_selected"));

                //Hace el post al delete
                if (multaData.Estado == "Activo" || multaData.Estado == "Pendiente") {
                $(".cancel").remove();
                $("#confirm").unbind("click");
                    confirm("¿Seguro que desea cancelar la multa a la empresa: " + multaData.NombreEmpresa + "?");
                $("#confirm").click(function () {
                    var lista = new ControlActions();
                    lista.DeleteToAPI("Multa/DesactivarMulta", multaData, function () {
                        var lista = new ControlActions();
                        lista.FillTable("", "listamulta", true);
                    });
                });
                } else {
                    multaData.Estado = "Activo";
                    confirm("¿Seguro que desea activar la multa a la empresa: " + multaData.NombreEmpresa + "?");
                    $("#confirm").click(function () {
                        var lista = new ControlActions();
                        lista.PutToAPI("Multa/ModificarMulta", multaData, function () {
                            window.location.replace("/Multa/");
                        });
                    });
                }
                break;
        }
    }
}

//ON DOCUMENT READY
$(document).ready(function () {

    var vMultas = new vListarMultas();
    vMultas.RetrieveAll();

});