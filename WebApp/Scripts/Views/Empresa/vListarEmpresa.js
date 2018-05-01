function vListarEmpresa() {

    this.tblEmpresaId = 'listaEmpresa';
    this.service = 'Empresa/GetAll';
    this.ctrlActions = new ControlActions();
    this.columns = "CedulaJuridica,NombreEmpresa,EmailEncargado,Telefono,Estado";

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblEmpresaId, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblEmpresaId, true);
    }

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
    }

    this.ActionControls = function (data) {
        switch (data.action) {
            case "view":
                window.location.replace("/Empresa/vVerEmpresa");
                break;

            case "edit":
                window.location.replace("/Empresa/vModificarEmpresa");
                break;

            case "delete":
                let dataEmpresa = JSON.parse(sessionStorage.getItem("listaEmpresa_selected"));
                var accion = dataEmpresa.Estado === "Activo" ? "desactivar" : "activar";

                $(".cancel").remove();
                $("#confirm").unbind("click");
                confirm("¿Seguro que desea " + accion + " la empresa: " + dataEmpresa.NombreEmpresa + "?");

                $("#confirm").click(function () {
                    var ctrlActions = new ControlActions();

                    ctrlActions.PutToAPI("Empresa/DesactivarEmpresa", dataEmpresa, function () {
                        this.ReloadTable();
                    });
                });

                break;
        }
    }
}

$(document).ready(function () {
    var vEmpresa = new vListarEmpresa();
    vEmpresa.RetrieveAll();
});