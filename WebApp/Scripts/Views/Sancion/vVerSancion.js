let sancion = JSON.parse(sessionStorage.getItem("listaSanciones_selected"));

function vVerSancion() {
    
    this.ctrlActions = new ControlActions();

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
    }

}

function LoadElements() {
    var control = new ControlActions();
    $("#txtSancion").val(sancion.Descripcion);
    $("#nMulta").val(sancion.Multa);
    $("#datetimepick").val(sancion.Fecha);
    $("#txtSuspencion").val(sancion.Suspencion);
    $("#datetimepick2").val(sancion.FechaReactivacion);
    $("#txtEmpresa").val(sancion.NombreEmpresa);
}

$(document).ready(function () {

    LoadElements();

});