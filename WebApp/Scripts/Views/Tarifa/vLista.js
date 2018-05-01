function vLista() {

    this.tblId = 'listatarifas';
    this.service = 'Tarifa/GetTarifas';
    this.updateService = 'Tarifa/UpdateTarifas';
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblId, true);
    }

    this.ActionControls = function(data) {
        /* Handle actions */
        switch (data.action) {
            default:
                //
        }
    }

    this.UpdateTarifas = function() {
        $(".progress").removeClass("d-none");
        $("#btnActualizar").attr("disabled", "disabled");
        var data = null;
        this.ctrlActions.PutToAPI(this.updateService, data,
            function() {
                window.location.replace("/Tarifa");
            });
    }
}

//ON DOCUMENT READY
$(document).ready(function () {

    var vlista = new vLista();
    vlista.RetrieveAll();

});