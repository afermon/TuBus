function vListaTipoTarjeta() {
    this.tblTarjetasId = 'listaTiposTarjetas';
    this.service = 'TipoTarjeta/GetAllTypes';
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function () {
        var actions = '<a href="#" data-action="edit"><i class="fa fa-pencil" aria-hidden="true"></i></a> ';

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
       case "edit":
                window.location.replace("/TiposTarjetas/Actualizar");
            break;
       case "delete":
                let tipoTarjetaStored = JSON.parse(sessionStorage.getItem("listaTiposTarjetas_selected"));
                $(".cancel").remove();
                $("#confirm").unbind("click");
               confirm("¿Seguro que desea eliminar el tipo de tarjeta: " + tipoTarjetaStored.Nombre + "?");
               $("#confirm").click(function() {
                   var lista = new ControlActions();
                   lista.DeleteToAPI("TipoTarjeta/Delete", tipoTarjetaStored, function() {
                        var lista = new ControlActions();
                       lista.FillTable("", "listaTiposTarjetas", true);
                   });
                   
               });
            break;
        }
    }
}

$(document).ready(function () {
    var tiposTarjetas = new vListaTipoTarjeta();
    
    tiposTarjetas.RetrieveAll();
});