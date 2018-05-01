let chofer = JSON.parse(sessionStorage.getItem("listachofer_selected"));

function vVerChofer() {
    this.ctrlActions = new ControlActions();

    this.BindFields = function (data) {
        this.ctrlActions.BindFields('frmEdition', data);
    }

    this.FillComboBoxes = function () {
        this.ctrlActions.FillComboBox("Empresa/GetAll", "cbEmpresa", "CedulaJuridica", "NombreEmpresa");
        $("#cbEmpresa").val(chofer.Empresa);
    }

}

function LoadElements() {
    var control = new ControlActions();
    control.FillComboBox("Empresa/GetAll", "cbEmpresa", "CedulaJuridica", "NombreEmpresa");
    $("#cbEmpresa").val(chofer.Empresa);
}

$(document).ready(function () {
    LoadElements();
    var vchofer = new vVerChofer();
    vchofer.BindFields(chofer);
});