function vListaTarjetas() {
    var authUser = new Auth();
    var user = authUser.GetCurrentUser();
    this.tblTarjetasId = 'listaTarjetas';
    this.service = user.TerminalId == -1 || user.TerminalId == 0 ? 'Tarjeta/GetRepositionCards' : "Tarjeta/GetRepositionCardsByTerminal";
    this.ctrlActions = new ControlActions();
    this.columns = "Usuario.Identificacion,SaldoDisponible,Terminal.TerminalName,TipoTarjeta.Nombre";

    this.RetrieveAll = function () {
        var actions = '<a href="#" data-action="config"><i class="fa fa-check-circle" aria-hidden="true"></i></a>'
                + '<a href="#" data-action="delete"><i class="fa fa-trash-o" aria-hidden="true"></i></a> ';

        this.ctrlActions.FillTable(this.service, this.tblTarjetasId, false, actions);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblTarjetasId, true);
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
    }

    this.ActionControls = function (data) {
        switch (data.action) {
        case "delete":
            $(".cancel").remove();
            $("#confirm").unbind("click");
            confirm("¿Seguro que desea eliminar la solicitud de reposición de: " + data.Usuario.Identificacion + "?");
            $("#confirm").click(function () {
                var lista = new ControlActions();
                lista.DeleteToAPI("Tarjeta/DeleteCard", data, function () {
                    var lista = new ControlActions();
                    lista.FillTable("", "listaTarjetas", true);
                });
            });
            break;
        case "config":
            $(".cancel").remove();
            $("#confirm").unbind("click");
            confirm("¿Seguro que desea aprobar la solicitud de reposición de: " + data.Usuario.Identificacion + "?");
            $("#confirm").click(function () {
                var lista = new ControlActions();
                lista.DeleteToAPI("Tarjeta/AprobarReposicion", data, function () {
                    var lista = new ControlActions();
                    lista.FillTable("", "listaTarjetas", true);
                });
            });
            break;
        }
    }
}

$(document).ready(function () {
    var tarjetas = new vListaTarjetas();

    tarjetas.RetrieveAll();
});