function vListarLinea() {
    this.tblTarjetasId = 'listaLineas';
    var authUser = new Auth();
    var empresa = authUser.GetCurrentEmpresa();
    var user = authUser.GetCurrentUser();
    this.service = empresa == null ? 'Linea/GetAllLines' : 'Linea/GetAllLines?terminal=' + user.TerminalId + '&empresaId=' + empresa.CedulaJuridica;
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function () {
        var actions = '<a href="#" data-action="edit"><i class="fa fa-pencil" aria-hidden="true"></i></a> ';

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
        case "edit":
                window.location.replace("/Linea/Actualizar");
            break;
        case "delete":
                let linea = JSON.parse(sessionStorage.getItem("listaLineas_selected"));
            $(".cancel").remove();
            $("#confirm").unbind("click");
            confirm("¿Seguro que desea eliminar la linea: " + linea.NombreLinea + "?");
            $("#confirm").click(function () {
                var lista = new ControlActions();
                lista.DeleteToAPI("linea/Delete", linea, function () {
                    var lista = new ControlActions();
                    lista.FillTable("", "listaLineas", true);
                });

            });
            break;
        }
    }
}

$(document).ready(function () {
    var lineas = new vListarLinea();

    lineas.RetrieveAll();
});