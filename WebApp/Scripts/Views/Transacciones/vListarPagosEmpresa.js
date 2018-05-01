function vListaTransacciones() {

    this.tblTarjetasId = 'listaTransacciones';
    this.service = 'Empresa/ObtenerPagos?empresaId=' + auth.GetCurrentEmpresa().CedulaJuridica;
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function () {
        var actions = '<a href="#" data-action="pay"><i class="fa fa-check-circle" aria-hidden="true"></i></a>';

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
            case "pay":
                var pago = JSON.parse(sessionStorage.getItem("listaTransacciones_selected"));
                if (pago.EstadoPago != "Cancelado") {
                    window.location.replace("/Transaction/RealizarPago");
                } else {
                    var ctrlActions = new ControlActions();
                    ctrlActions.ShowMessage('E', "Este cobro ya fue cancelado");
                }
            break;
        }
    }
}

$(document).ready(function () {
    var transactions = new vListaTransacciones();

    transactions.RetrieveAll();
});