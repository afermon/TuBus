function vListaConvenio() {
    this.tblTarjetasId = 'listaConvenios';
    this.service = 'Convenio/GetAllSolicitudes';
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function () {
        var actions = '<a href="#" data-action="acept"><i class="fa fa-check-circle" aria-hidden="true"></i></a>'+
            '<a href="#" data-action="delete"><i class="fa fa-trash-o" aria-hidden="true"></i></a> ';

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
            case "acept":
            let conveioAprroved = JSON.parse(sessionStorage.getItem("listaConvenios_selected"));
            var lista = new ControlActions();
            lista.PutToAPI("Convenio/ProcesarSolicitud?solicitudId=" + conveioAprroved.SolicitudId + "&isFromAdmin=true", conveioAprroved, function () {
                var lista = new ControlActions();
                lista.FillTable("", "listaConvenios", true);
            });
            break;
        case "delete":
            let conveio = JSON.parse(sessionStorage.getItem("listaConvenios_selected"));
            $(".cancel").remove();
            $("#confirm").unbind("click");
            confirm("¿Seguro que desea negar la solicitud de: " + conveio.EmailSolicitante + "?");
            $("#confirm").click(function () {
                var lista = new ControlActions();
                lista.PutToAPI("Convenio/ProcesarSolicitud?solicitudId=" + conveio.SolicitudId + "&isDenied=true", conveio, function () {
                    var lista = new ControlActions();
                    lista.FillTable("", "listaConvenios", true);
                });
            });
            break;
        }
    }
}

$(document).ready(function () {
    var convenio = new vListaConvenio();

    convenio.RetrieveAll();
});