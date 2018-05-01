function vListarChofer() {

    this.tblCustomersId = 'listachofer';
    this.service = 'Chofer/ObtenerChofer';
    this.serviceDelete = 'Chofer/DesactivarChofer';
    this.serviceActivate = 'Chofer/ActivarChofer'
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
                window.location.replace("/Chofer/vVerChofer");
                break;
            case "edit":
                window.location.replace("/Chofer/vModificarChofer");
                break;
            case "delete":

                var choferData = JSON.parse(sessionStorage.getItem("listachofer_selected"));
                var service;
                $(".cancel").remove();
                $("#confirm").unbind("click");

                if (choferData.Estado == "Activo") {
                    choferData.Estado = "Inactivo";
                    confirm("¿Seguro que desea desactivar el chofer: " + choferData.Nombre + "?");
                    service = this.serviceDelete;
                    $("#confirm").click(function () {
                        var lista = new ControlActions();
                        lista.DeleteToAPI(service, choferData, function () {
                            window.location.replace("/Chofer/");
                        });
                    });
                } else {
                    choferData.Estado = "Activo";
                    confirm("¿Seguro que desea activar el chofer: " + choferData.Nombre + "?");
                    service = this.serviceActivate;
                    $("#confirm").click(function () {
                        var lista = new ControlActions();
                        lista.PutToAPI(service, choferData, function () {
                            window.location.replace("/Chofer/");
                        });
                    });
                }
                break;
        }
    }
}

//ON DOCUMENT READY
$(document).ready(function () {

    var vChofer = new vListarChofer();
    vChofer.RetrieveAll();

});