function vListaRoles() {

    this.tblRolesId = 'listaRoles';
    this.service = 'Role/ObtenerRoles';
    this.ctrlActions = new ControlActions();
    this.columns = "ROLEID,DESCRIPCION,ESTADO";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblRolesId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblRolesId, true);
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
    }

    this.ActionControls = function (data) {
        switch (data.action) {
            case "view":
                window.location.replace("/Rol/vVerRol");
                break;
            case "edit":
                window.location.replace("/Rol/vModificarRol");
                break;
            case "delete":
                this.serviceDesactivar = 'Role/DesactivarRol';

                let rolSelected = JSON.parse(sessionStorage.getItem("listaRoles_selected"));

                this.ctrlActions.PutToAPI(this.serviceDesactivar, rolSelected);
                this.ReloadTable();
                break;
        }
    }
}

$(document).ready(function () {
    var vRoles = new vListaRoles();

    vRoles.RetrieveAll();
});