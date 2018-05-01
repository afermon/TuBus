function vListarEmpresa() {

    this.tblEmpresaId = 'listaEmpresa';
    this.service = 'Empresa/GetAllPendientes';
    this.ctrlActions = new ControlActions();
    this.columns = "CedulaJuridica,NombreEmpresa,EmailEncargado,Telefono,Estado";

    this.RetrieveAll = function () {
        var actions = '<a href="#" data-action="view"><i class="fa fa-eye" aria-hidden="true"></i></a> ' +
            '<a href="#" data-action="delete"><i class="fa fa-trash-o" aria-hidden="true"></i></a> ' +
            '<a href="#" data-action="approved"><i class="fa fa-check" aria-hidden="true"></i></a>';

        this.ctrlActions.FillTable(this.service, this.tblEmpresaId, false, actions);
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

            case "delete":
                let dataEmpresa = JSON.parse(sessionStorage.getItem("listaEmpresa_selected"));

                $(".cancel").remove();

                $('#modalRechazar').modal('show');

                $("#enviarRechazo").click(function () {
                    var ctrlActions = new ControlActions();

                    dataEmpresa.Estado = "Inactivo";
                    dataEmpresa.Rechazo = document.getElementById("rechazoRazon").value;

                    ctrlActions.PutToAPI("Empresa/AprobarEmpresa", dataEmpresa, function () {
                        $('#modalRechazar').modal('hide');
                        ctrlActions.FillTable("", "listaEmpresa", true);
                    });
                });
                break;

            case "approved":
                this.serviceAprobar = 'Empresa/AprobarEmpresa';

                let empresaSelected = JSON.parse(sessionStorage.getItem("listaEmpresa_selected"));

                empresaSelected.Estado = "Activo";

                this.ctrlActions.PutToAPI(this.serviceAprobar, empresaSelected, function () {
                    ctrlActions.FillTable("", "listaEmpresa", true);
                });

                break;

        }
    }
}

$(document).ready(function () {
    var vEmpresa = new vListarEmpresa();
    vEmpresa.RetrieveAll();
});