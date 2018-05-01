function vListaConvenio() {
    this.tblTarjetasId = 'listaConvenios';
    this.service = 'Convenio/GetAllAgreements';
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
                window.location.replace("/Convenio/Actualizar");
            break;
        case "delete":
            let conveio = JSON.parse(sessionStorage.getItem("listaConvenios_selected"));
            $(".cancel").remove();
            $("#confirm").unbind("click");
            confirm("¿Seguro que desea eliminar el convenio: " + conveio.NombreInstitucion + "?");
            $("#confirm").click(function () {
                var lista = new ControlActions();
                lista.DeleteToAPI("Convenio/Delete", conveio, function() {
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