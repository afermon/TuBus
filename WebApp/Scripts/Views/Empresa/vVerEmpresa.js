function verEmpresa() {
    let nombre = document.querySelector('#txtNombre');
    let cedula = document.querySelector('#txtCedulaJuridica');
    let telefono = document.querySelector('#txtNumContacto');
    let correo = document.querySelector('#txtCorreo');

    let data = JSON.parse(sessionStorage.getItem("listaEmpresa_selected"));

    nombre.disabled = true;
    nombre.value = data.NombreEmpresa;

    cedula.disabled = true;
    cedula.value = data.CedulaJuridica;

    telefono.disabled = true;
    telefono.value = data.Telefono;

    correo.disabled = true;
    correo.value = data.EmailEncargado;
}

$(document).ready(function () {
    verEmpresa();
});