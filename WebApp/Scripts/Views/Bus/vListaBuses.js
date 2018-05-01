function vListaBuses() {
    this.tblBusesId = 'listaBuses';
    this.service = 'Bus/ObtenerBuses';
    this.ctrlActions = new ControlActions();
    this.columns = "Id,CapacidadSentado,CapacidadDePie,AsientoDiscapacitado,Estado";

    this.RetrieveAll = function () {

        this.ctrlActions.FillTable(this.service, this.tblBusesId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblBusesId, true);
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
    }

    this.ActionControls = function (data) {
        switch (data.action) {
            case "view":
                window.location.replace("/Bus/vVerBus");
                break;
            case "edit":
                window.location.replace("/Bus/vModificarBus");
                break;
            case "delete":
                this.serviceDesactivar = 'Bus/DesactivarBus';
                let busSelected = JSON.parse(sessionStorage.getItem("listaBuses_selected"));

                console.log(busSelected);

                var accion = busSelected.Estado === "Activo" ? "desactivar" : "activar";

                $(".cancel").remove();
                $("#confirm").unbind("click");
                confirm("¿Seguro que desea " + accion + " el bus : " + busSelected.Id + "?");

                $("#confirm").click(function () {
                    var ctrlActions = new ControlActions();

                    ctrlActions.PutToAPI('Bus/DesactivarBus', busSelected, function () {
                        ctrlActions.FillTable('Bus/ObtenerBuses', 'listaBuses', true)
                    });
                });

                break;
        }
    }
}

$(document).ready(function () {
    var vBuses = new vListaBuses();

    vBuses.RetrieveAll();
});