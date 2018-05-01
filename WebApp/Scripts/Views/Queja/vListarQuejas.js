function vListarQuejas() {

    this.tblCustomersId = 'listaQuejas';
    this.service = 'Queja/ObtenerQuejasActivas';
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function () {
        var actions = '<a href="#" data-action="view"><i class="fa fa-eye" aria-hidden="true"></i></a> ';

        this.ctrlActions.FillTable(this.service, this.tblCustomersId, false, actions);
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
                window.location.replace("/Quejas/vVerQueja");
                break;
            case "delete":
                
                let quejaData = JSON.parse(sessionStorage.getItem("listaQuejas_selected"));
                //Hace el post al delete
                
                $(".cancel").remove();
                $("#confirm").unbind("click");
                confirm("¿Seguro que desea eliminar la queja?");
                $("#confirm").click(function () {
                    var lista = new ControlActions();
                    lista.DeleteToAPI("Queja/DesactivarQueja", quejaData, function () {
                        var lista = new ControlActions();
                        lista.FillTable("", "listaQuejas", true);
                    });
                });
                break;
        }
    }
}

//ON DOCUMENT READY
$(document).ready(function () {

    var vQuejas = new vListarQuejas();
    vQuejas.RetrieveAll();

});