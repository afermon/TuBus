function vVer() {
    this.usuario = JSON.parse(sessionStorage.getItem("listausuarios_selected"));
    this.serviceUsuario = 'Usuario/GetUserByEmail?email=' + this.usuario.Email;

    this.ctrlActions = new ControlActions();

    this.Retrieve = function () {
        var jqxhr = $.get({
            url: this.ctrlActions.GetUrlApiService(this.serviceUsuario),
            headers: { 'Authorization': auth.GetAccessToken() }
        }).done(function (response) {
            var vperfil = new vVer();
            vperfil.BindFieldsUsuario(response.Data);
        }).fail(function (response) {
            var data = response.responseJSON;
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('E', data.ExceptionMessage);
            console.log(data);
        });
    }

    this.BindFieldsUsuario = function (data) {
        this.ctrlActions.BindFields('frmPerfilUsuario', data);
    }

    this.FillComboBoxes = function () {
        this.ctrlActions.FillComboBox("List/Get/ESTADO", "cbEstado", "Value", "Description");
        this.ctrlActions.FillComboBox("Role/ObtenerRoles", "cbRole", "RoleId", "RoleId");
        this.ctrlActions.FillComboBox("Terminal/ObtenerTerminales", "cbTerminal", "Id", "TerminalName");
    }
}

//ON DOCUMENT READY
$(document).ready(function () {
    var vperfil = new vVer();
    vperfil.FillComboBoxes();
    vperfil.Retrieve();
});