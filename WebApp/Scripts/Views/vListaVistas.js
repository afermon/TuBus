function vListaVistas() {

    this.tblTerminalId = 'listaterminal';
    this.service = 'Terminal/ObtenerTerminales';
    this.ctrlActions = new ControlActions();
    this.columns = "TERMINAL_ID,TERMINAL_NAME,CANTIDAD_LINEAS, ESTADO";

    this.RetrieveAll = function () {
        var actions = '<a href="#" data-action="view"><i class="fa fa-eye" aria-hidden="true"></i></a> ' +
            '<a href="#" data-action="edit"><i class="fa fa-pencil" aria-hidden="true"></i></a> ' +
            '<a href="#" data-action="delete"><i class="fa fa-trash-o" aria-hidden="true"></i></a> ' +
            '<a href="#" data-action="config"><i class="fa fa-cog" aria-hidden="true"></i></a>';

        this.ctrlActions.FillTable(this.service, this.tblTerminalId, false, actions);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblTerminalId, true);
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
    }

    this.ActionControls = function (data) {
        switch (data.action) {
            case "view":
                window.location.replace("/Terminal/vVerTerminal");
                break;
            case "edit":
                window.location.replace("/Terminal/vModificarTerminal");
                break;
            case "delete":
                alert("Delete");
                break;
            case "config":
                window.location.replace("/Configuracion/vTerminal");
                break;
        }
    }
}

$(document).ready(function () {
    var vListas = new vListaVistas();

    vListas.RetrieveAll();
});