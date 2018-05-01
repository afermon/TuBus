function vReposicion() {
    var auth = new Auth();
    var user = auth.GetCurrentUser();
    this.tblTarjetasId = 'listaTarjetas';
    this.service = 'Tarjeta/UpdateEstadoCard';
    this.ctrlActions = new ControlActions();

    this.Reposicion = function () {
        $("#Repoacept").unbind("click");
        $("#RepoModal ").modal('show');
        $("#Repoacept").click(function() {
            var tarjetaObj = {
                CodigoUnico : $("#tarjetas").val(),
                EstadoTarjeta: {
                    EstadoTarjetaId: $("#estados").val()
                } 
            }
            var ctrlActions = new ControlActions();
            ctrlActions.PutToAPI("Tarjeta/UpdateEstadoCard", tarjetaObj, function() {
                var ctrlActions = new ControlActions();
                ctrlActions.FillTable("", "listaTarjetas", true);
            });
        });
    }

}

function Load() {
    var control = new ControlActions();
    var userAuth = new Auth();
    var user = userAuth.GetCurrentUser();
    control.FillComboBox("Tarjeta/ListCardsByUser?userMail=" + user.Email, "tarjetas", "CodigoUnico", "NombreTarjeta");
    control.FillComboBox("EstadoTarjeta/GetStateRepositionCards", "estados", "EstadoTarjetaId", "NombreEstado");
}

$(document).ready(function () {
    Load();
    var tarjetas = new vReposicion();
    $(".breadcrumb-item a").attr("href", "#");

});