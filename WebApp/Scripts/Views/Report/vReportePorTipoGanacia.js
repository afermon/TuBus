function vReporte() {
    this.service = "Report/GetReportAllTransactionsTipo";
    this.ctrlActions = new ControlActions();

    this.FillIframes = function () {
        this.ctrlActions.FillIframe(this.service, "iframe-reporte");
    }
}

//ON DOCUMENT READY
$(document).ready(function () {
    var vreporte = new vReporte();
    vreporte.FillIframes();
});