function vListarSanciones() {

    this.tblCustomersId = 'listaSanciones';
    this.service = 'Sancion/ObtenerSancionesActivas';
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblCustomersId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblCustomersId, true);
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
    }

    this.ActionControls = function (data) {
        /* Handle actions */
        switch (data.action) {
            case "view":
                window.location.replace("/Sancion/vVerSancion");
                break;
            case "edit":
                window.location.replace("/Sancion/vModificarSancion");
                break;
            case "delete":
                
                let sancionData = JSON.parse(sessionStorage.getItem("listaSanciones_selected"));

                //Hace el post al delete
                
                $(".cancel").remove();
                $("#confirm").unbind("click");
                confirm("¿Seguro que desea eliminar la sanción a la empresa: " + sancionData.Empresa + "?");
                $("#confirm").click(function () {
                    var lista = new ControlActions();
                    lista.DeleteToAPI("Sancion/DesactivarSancion", sancionData, function () {
                        var lista = new ControlActions();
                        lista.FillTable("", "listaSanciones", true);
                    });
                });
                              
                break;
        }
    }
}


//ON DOCUMENT READY
$(document).ready(function () {

    var vSanciones = new vListarSanciones();
    vSanciones.RetrieveAll();

});