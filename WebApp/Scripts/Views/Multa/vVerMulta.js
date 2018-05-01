let multa = JSON.parse(sessionStorage.getItem("listamulta_selected"));

function vVerMulta()
{
    this.ctrlActions = new ControlActions();

    this.BindFields = function (data)
    {
        this.ctrlActions.BindFields('frmEdition', data);
    }
}

$(document).ready(function () {
    
    var verMulta = new vVerMulta();
    verMulta.BindFields(multa);
});