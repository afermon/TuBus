function vListaTerminales() {

    this.tblCustomersId = 'listaterminal';
    this.service = 'Terminal/ObtenerTerminales';
    this.serviceDelete = 'Terminal/DesactivarTerminal';
    this.serviceActivate = 'Terminal/ActivarTerminal';
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function () {
        var actions = '<a href="#" data-action="view"><i class="fa fa-eye" aria-hidden="true"></i></a> ' +
            '<a href="#" data-action="edit"><i class="fa fa-pencil" aria-hidden="true"></i></a> ' +
            '<a href="#" data-action="config"><i class="fa fa-cog" aria-hidden="true"></i></a> ';

        this.ctrlActions.FillTable(this.service, this.tblCustomersId, false, actions);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblCustomersId, true);
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
    }

    this.ActionControls = function(data) {
        /* Handle actions */
        switch (data.action) {
            case "view":
                window.location.replace("/Terminal/vVerTerminal");
                break;
            case "edit":
                window.location.replace("/Terminal/vModificarTerminal");
                break;

            case "delete":

                let terminalData = JSON.parse(sessionStorage.getItem("listaterminal_selected"));
                var service;
                $(".cancel").remove();
                $("#confirm").unbind("click");

                if (terminalData.Estado == "Activo") {
                    terminalData.Estado = "Inactivo";
                    confirm("¿Seguro que desea desactivar la terminal: " + terminalData.TerminalName + "?");
                    service = this.serviceDelete;
                    $("#confirm").click(function () {
                        var lista = new ControlActions();
                        lista.DeleteToAPI(service, terminalData, function () {
                            window.location.replace("/Terminal/");
                        });
                    });
                } else {
                    terminalData.Estado = "Activo";
                    confirm("¿Seguro que desea activar la terminal: " + terminalData.TerminalName + "?");
                    service = this.serviceActivate;
                    $("#confirm").click(function () {
                        var lista = new ControlActions();
                        lista.PutToAPI(service, terminalData, function () {
                            window.location.replace("/Terminal/");
                        });
                    });
                }
                break;
            case "config":
                window.location.replace("/Configuracion/vTerminal");
                break;
        }
    }
}

//ON DOCUMENT READY
$(document).ready(function () {

    var vTerminales = new vListaTerminales();
    vTerminales.RetrieveAll();

});