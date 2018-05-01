function verRol() {
    let idRol = document.querySelector('#txtNombre');
    let descripcion = document.querySelector('#txtDescripcion');
    let homePage = document.querySelector('#txtHomePage');

    let terminal = JSON.parse(sessionStorage.getItem("listaRoles_selected"));

    idRol.disabled = true;
    idRol.value = terminal.RoleId;

    descripcion.disabled = true;
    descripcion.value = terminal.Descripcion;

    homePage.disabled = true;
    homePage.value = terminal.Homepage;
}

function LoadElements() {
    var control = new ControlActions();
    let role = JSON.parse(sessionStorage.getItem("listaRoles_selected"));

    control.FillComboBox("Vista/ObtenerVistas", "Vistas", "VistaId", "Nombre");
}

$(document).ready(function () {
    verRol();
    LoadElements();
});