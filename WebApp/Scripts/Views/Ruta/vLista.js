function vLista() {

    this.tbl = 'listarutas';

    this.empresa = auth.GetCurrentEmpresa();
    this.service = this.empresa == null ? 'Ruta/GetAll' : 'Ruta/GetAll?empresaId=' + this.empresa.CedulaJuridica;

    this.serviceDelete = 'Ruta/Delete';
    this.serviceActivate = 'Ruta/Activate';

    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tbl, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tbl, true);
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
    }

    this.ActionControls = function(data) {
        /* Handle actions */
        switch (data.action) {
            case "view":
                window.location.replace("/Ruta/vVer");
                break;
            case "edit":
                window.location.replace("/Ruta/vModificar");
                break;
            case "delete":
                
                var rutaData = JSON.parse(sessionStorage.getItem("listarutas_selected"));
                var service;
                $(".cancel").remove();
                $("#confirm").unbind("click");

                if (rutaData.Estado == "Activo") {
                    rutaData.Estado = "Inactivo";
                    confirm("¿Seguro que desea desactivar la ruta: " + rutaData.RutaName + "?");
                    service = this.serviceDelete;
                    $("#confirm").click(function () {
                        var lista = new ControlActions();
                        lista.DeleteToAPI(service, rutaData, function () {
                            window.location.replace("/Ruta/");
                        });
                    });
                } else {
                    rutaData.Estado = "Activo";
                    confirm("¿Seguro que desea activar la ruta: " + rutaData.RutaName + "?");
                    service = this.serviceActivate;
                    $("#confirm").click(function () {
                        var lista = new ControlActions();
                        lista.PutToAPI(service, rutaData, function () {
                            window.location.replace("/Ruta/");
                        });
                    });
                }
                break;
        }
    }
}

//ON DOCUMENT READY
$(document).ready(function () {

    var vlista = new vLista();
    vlista.RetrieveAll();

});