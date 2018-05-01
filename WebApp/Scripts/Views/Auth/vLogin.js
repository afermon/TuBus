function vLogin() {
    this.service = 'Usuario';
    this.ctrlActions = new ControlActions();

    this.Login = function () {
        var userData = {};

        userData = this.ctrlActions.GetDataForm('frmLogin');

        auth.GetToken(userData);
    }
}

//ON DOCUMENT READY
$(document).ready(function () {
    
});
