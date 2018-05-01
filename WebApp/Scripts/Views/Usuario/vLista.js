function vListaUsuarios() {

    this.tblUsuariosId = 'listausuarios';
    this.service = 'Usuario/GetAll';
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblUsuariosId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblUsuariosId, true);
    }

    this.ActionControls = function(data) {
        /* Handle actions */
        switch (data.action) {
            case "view":
                window.location.replace("/Usuario/vVer");
                break;
            case "edit":
                window.location.replace("/Usuario/vModificar");
                break;
            case "delete":
                this.DeleteUsuario();
                break;
        }
    }

    this.DeleteUsuario = function() {
        var usuario = JSON.parse(sessionStorage.getItem("listausuarios_selected"));

        //Hace el post al delete

        $(".cancel").remove();
        $("#confirm").unbind("click");
        confirm("¿Seguro que desea eliminar el usuario: " + usuario.Email + "?");
        $("#confirm").click(function () {
            var lista = new ControlActions();
            lista.DeleteToAPI("Usuario/Delete", usuario, function () {
                var lista = new ControlActions();
                lista.FillTable("", "listausuarios", true);
            });
        });
    }
}

//ON DOCUMENT READY
$(document).ready(function () {

    var vlistausuarios = new vListaUsuarios();
    vlistausuarios.RetrieveAll();

});