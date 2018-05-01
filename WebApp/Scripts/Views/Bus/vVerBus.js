function vVer() {
    this.ctrlActions = new ControlActions();
    this.busData = JSON.parse(sessionStorage.getItem("listaBuses_selected"));
    this.service = 'Bus/ObtenerBus';

    this.Retrieve = function () {
        var jqxhr = $.get({
            url: this.ctrlActions.GetUrlApiService(this.service),
            data: { "placa": this.busData.Id },
            headers: { 'Authorization': auth.GetAccessToken() }
        }).done(function (response) {
            var vBus = new vVer();

            vBus.BindFields(response.Data);
        }).fail(function (response) {
            var data = response.responseJSON;
            var ctrlActions = new ControlActions();

            ctrlActions.ShowMessage('E', data.ExceptionMessage);
        });
    }

    this.BindFields = function (data) {
        var index;
        this.ctrlActions.BindFields('frmEdition', data);

        let placa = document.querySelector('#txtPlaca');
        let capacidadSentado = document.querySelector('#txtCapacidadSentado');
        let capacidadDePie = document.querySelector('#txtCapacidadDePie');
        let capacidadDiscapacitados = document.querySelector('#txtCapacidadDiscapacitados');
        let seguro = document.querySelector('#chbSeguro');
        let marchamo = document.querySelector('#chbMarchamo');
        let rtv = document.querySelector('#chbRtv');
        let empresa = document.querySelector('#txtEmpresa');

        placa.disabled = true;
        capacidadSentado.disabled = true;
        capacidadDePie.disabled = true;
        capacidadDiscapacitados.disabled = true;
        marchamo.disabled = true;
        seguro.disabled = true;
        rtv.disabled = true;
        empresa.disabled = true;

        index = data.Requisitos.map(function (d) { return d['Permiso']; }).indexOf('Seguro');
        seguro.checked = data.Requisitos[index].Estado === "Activo";

        index = data.Requisitos.map(function (d) { return d['Permiso']; }).indexOf('RTV');
        rtv.checked = data.Requisitos[index].Estado === "Activo";

        index = data.Requisitos.map(function (d) { return d['Permiso']; }).indexOf('Marchamo');
        marchamo.checked = data.Requisitos[index].Estado === "Activo";
    }


    function LoadElements() {
        var control = new ControlActions();

        control.FillComboBox("Vista/ObtenerVistas", "Vistas", "VistaId", "Nombre");
    }
}

$(document).ready(function () {
    var verBus = new vVer();
    verBus.Retrieve();
});

