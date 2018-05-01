function Auth() {
    this.service = "Usuario/CheckToken";
    this.ctrlActions = new ControlActions();

    this.GetToken = function (usuario) {
        usuario["grant_type"] = "password";
        var jqxhr = $.post(this.ctrlActions.GetUrlApiService("Usuario/Token", false),
                usuario,
                function (response) {
                    sessionStorage.setItem("accessToken", JSON.stringify(response));
                    auth.SetUser();
                })
            .fail(function (response) {
                var data = response.responseJSON;
                console.log(data);
                alert(data.error_description);
            });
    }

    this.CheckToken = function () {
        var jqxhr = $.get({
                url: this.ctrlActions.GetUrlApiService(this.service),
                headers: {'Authorization': this.GetAccessToken()}
            }).done(function (response) {
                console.log(response.Message);
                if (currentPath[1] === "vLogin") 
                    window.location.replace(auth.GetCurrentRole().Homepage);

            }).fail(function (response) {
                if (currentPath[0] !== "Auth") 
                    window.location.replace("/Auth/vLogin");
        });
    }

    this.Logout = function () {
        sessionStorage.clear();
        window.location.replace("/");
    }

    this.SetUser = function () {
        //Get user information
        $.get({
            url: this.ctrlActions.GetUrlApiService("Usuario/Current"),
            headers: { 'Authorization': this.GetAccessToken() }
            }).done(
            function (response) { 
                sessionStorage.setItem("currentUser", JSON.stringify(response.Data));
                auth.SetRole();
            })
            .fail(function (response) {
                console.log(response);
                var data = response.responseJSON;
                console.log(data);
                alert(data.Message);
            });
    }

    this.SetRole = function() {
        //Get the role
        $.get({
                url: this.ctrlActions.GetUrlApiService("Role/Current"),
                headers: { 'Authorization': this.GetAccessToken() }
            }).done(
                function (response) {
                    sessionStorage.setItem("currentRole", JSON.stringify(response.Data));
                    switch (auth.GetCurrentRole().RoleId) {
                        case "EMPRESARIO":
                            auth.SetEmpresa();
                            break;
                        case "ADMINISTRADOR":
                            auth.SetTerminales();
                            break;
                        default:
                            sessionStorage.setItem("currentTerminal", auth.GetCurrentUser().TerminalId);
                            window.location.replace(auth.GetCurrentRole().Homepage);
                    }
                })
            .fail(function (response) {
                var data = response.responseJSON;
                console.log(data);
                alert(data.Message);
                sessionStorage.clear();
            });
    }

    this.SetTerminales = function () {
        //Get the Terminales
        $.get({
                url: this.ctrlActions.GetUrlApiService("Terminal/ObtenerTerminales"),
                headers: { 'Authorization': this.GetAccessToken() }
            }).done(
                function (response) {
                    sessionStorage.setItem("currentListaTerminales", JSON.stringify(response.Data));
                    sessionStorage.setItem("currentTerminal", JSON.stringify(response.Data[0].Id));
                    if (window.location.pathname != auth.GetCurrentRole().Homepage) 
                        window.location.replace(auth.GetCurrentRole().Homepage);
                })
            .fail(function (response) {
                var data = response.responseJSON;
                console.log(data);
                alert(data.Message);
                sessionStorage.clear();
            });
    }

    this.SetEmpresa = function() {
        //Get Empresa
        $.get({
                url: this.ctrlActions.GetUrlApiService("Empresa/Current"),
                headers: { 'Authorization': this.GetAccessToken() }
            }).done(
                function (response) {
                    sessionStorage.setItem("currentEmpresa", JSON.stringify(response.Data));
                    auth.CheckEmpresaPendingPayments(response.Data);
                    window.location.replace(auth.GetCurrentRole().Homepage);
                })
            .fail(function (response) {
                var data = response.responseJSON;
                console.log(data);
                alert(data.Message);
                sessionStorage.clear();
            });
    }

    this.CheckEmpresaPendingPayments = function (obj) {
        //Get Empresa
        $.get({
            url: this.ctrlActions.GetUrlApiService("Empresa/RealizarCobro?empresaId=" + obj.CedulaJuridica),
                headers: { 'Authorization': this.GetAccessToken() }
            }).done(
                function (response) {
                   window.location.replace(auth.GetCurrentRole().Homepage);
                })
            .fail(function (response) {
                var data = response.responseJSON;
                console.log(data);
                alert(data.Message);
                sessionStorage.clear();
            });
    }

    this.GetCurrentListaTerminales = function () {
        return JSON.parse(sessionStorage.getItem("currentListaTerminales"));
    }

    this.GetCurrentTerminal = function () {
        return JSON.parse(sessionStorage.getItem("currentTerminal"));
    }

    this.GetCurrentEmpresa = function () {
        return JSON.parse(sessionStorage.getItem("currentEmpresa"));
    }

    this.GetCurrentUser = function()
    {
        return JSON.parse(sessionStorage.getItem("currentUser"));
    }

    this.GetCurrentRole = function() {
        return JSON.parse(sessionStorage.getItem("currentRole"));
    }

    this.GetAccessToken = function () {
        try {
            return String("Bearer " + JSON.parse(sessionStorage.getItem("accessToken")).access_token);
        }
        catch (err) {
            return String("Bearer ");
        }
    }

    this.SetUsername = function () {
        $(document).ready(function () {
            var user = auth.GetCurrentUser();
            $("#dashboard-currentuser").text(user.Nombre + " " + user.Apellidos);
            $("#breadcrumb-current").text(document.title);
        });
    }

    this.BuildMenu = function () {
        if (auth.GetCurrentRole().RoleId == "ADMINISTRADOR") {
            $(document).ready(function() {
                $(auth.GetCurrentListaTerminales()).each(function() {
                    var option = $("<option />");
                    option.attr("value", this["Id"]).text(this["TerminalName"]);
                    var estado = "";
                    if (this.hasOwnProperty("Estado") && this["Estado"] != "Activo") {
                        option.prop('disabled', true);
                        option.text(option.text() + " - Inactivo");
                    }
                    $("#terminalSelector").append(option);
                });
                $("#terminalSelector").removeClass("d-none");
                $("#terminalSelector").val(auth.GetCurrentTerminal());
                $("#terminalSelector").change(function() {
                    sessionStorage.setItem("currentTerminal", JSON.stringify($("#terminalSelector").val()));
                });
            });
        }
        var vistas = this.GroupBy(this.GetCurrentRole().Vistas.filter(vista => vista.ShowMenu === true), "AppController");
        var menu = [];
        for (var controller in vistas) {
            if (vistas.hasOwnProperty(controller)) {
                var vistasController = vistas[controller];
                var element = document.createElement("li");
                element.setAttribute("data-toggle", "tooltip");
                element.setAttribute("data-placement", "right");
                element.setAttribute("title", controller);
                element.classList.add("nav-item");
                if (vistasController.length > 1) {
                    var link = document.createElement("a");
                    link.classList.add("nav-link","nav-link-collapse","collapsed");
                    link.href = "#collapse" + controller;
                    link.setAttribute("data-toggle", "collapse");
                    link.setAttribute("data-parent", "#tubus-sidebar-menu");
                    link.innerHTML = "<i class='fa fa-fw " + vistasController[0].Icon + "'></i><span class='nav-link-text' > " + controller + "</span>";
                    element.appendChild(link);

                    var subMenu = document.createElement("ul");
                    subMenu.setAttribute("id", "collapse" + controller);
                    subMenu.classList.add("sidenav-second-level","collapse");
                    vistasController.forEach(function(vista)
                    {
                        var subLi = document.createElement("li");
                        var subLink = document.createElement("a");
                        subLink.href = vista.Url;
                        subLink.innerHTML = vista.Nombre;
                        subLi.appendChild(subLink);
                        subMenu.appendChild(subLi);
                    });

                    element.appendChild(subMenu);

                } else {
                    var link = document.createElement("a");
                    link.classList.add("nav-link");
                    link.href = vistasController[0].Url;
                    link.innerHTML = "<i class='fa fa-fw " + vistasController[0].Icon + "'></i><span class='nav-link-text' > " + vistasController[0].Nombre + "</span>";
                    element.appendChild(link);
                }

                menu.push(element);

                $("#tubus-sidebar-menu").append(menu);
            }
        }
    }

    this.GroupBy = function (arr, property) {
        return arr.reduce(function (memo, x) {
            if (!memo[x[property]]) { memo[x[property]] = []; }
            memo[x[property]].push(x);
            return memo;
        }, {});
    }
}

var auth = new Auth();
var currentPath = window.location.pathname.substr(1).split("/");

if (currentPath[0] !== "" && currentPath[0] !== "Home" && currentPath[1] !== "vForgotPassword" && currentPath[0] !== "usuario" && currentPath[0] !== "Convenio") 
   auth.CheckToken();
if (currentPath[0] !== "" && currentPath[0] !== "Home" && currentPath[0] !== "Auth" && currentPath[0] !== "usuario" && currentPath[0] !== "Convenio") 
    auth.SetUsername();

$(document).ready(function () {
    $("#terminalSelector").change(function () {
        if (currentPath[1] !== "vAdministrador")
            location.reload();
    });
});