let queja = JSON.parse(sessionStorage.getItem("listaQuejas_selected"));

function LoadElements() {
    this.ctrlActions = new ControlActions();
    $("#txtQueja").val(queja.DetalleQueja);
    this.ctrlActions.FillComboBox("Ruta/GetAll", "cbRutas", "Id", "RutaName");
    $("#cbRutas").val(queja.Ruta);

    if (!queja.Chofer == "")
    {
        this.ctrlActions.FillComboBox("Chofer/ObtenerChoferRuta?Ruta=" + queja.Ruta, "cbChofer", "Cedula", "NombreCompleto");
        $("#cbChofer").val(queja.Chofer);
        $("#comboChofer").removeClass("d-none");
    }
    $("#txtPlaca").val(queja.Placa);
    $("#datetimepick").val(queja.Fecha);
}

$(document).ready(function () {

    LoadElements();
});