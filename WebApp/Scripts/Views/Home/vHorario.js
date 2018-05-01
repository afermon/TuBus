function vHorario() {
    this.service = 'Ruta/Get';
    this.ctrlActions = new ControlActions();

    this.Retrieve = function () {
        if ($("#cbRuta").val()) {
            $(".table").removeClass("d-none");
            $("#nextBusDiv").removeClass("d-none");
            var jqxhr = $.get({
                url: this.ctrlActions.GetUrlApiService(this.service + "/" + $("#cbRuta").val())
            }).done(function (response) {
                horarios = response.Data.Horarios;
                var vhorario = new vHorario();
                vhorario.DrawHorario();
                vhorario.CostoData(response.Data);
            }).fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
                console.log(data);
            });
        } else {
            $(".table").addClass("d-none");
            $("#nextBusDiv").addClass("d-none");
            alert("La terminal seleccionada no tiene rutas disponibles en este momento.");
        }
    }

    this.CostoData = function(data) {
        $("#costo").html(data.CostoTotal);
        $("#costoKm").html(data.CostoKm + " (CRC) por Km <br> (" + data.Distancia + " Kms)");
    }

    this.DrawHorario = function () {
        $('#dia-' + moment().isoWeekday()).addClass("text-success");
        $("#nextBus").html("Mañana");
        $("#nextBusHour").html("-");
        $('#nextBusHour').addClass("d-none");
        var table = this.GroupBy(horarios, "Dia");

        for (var d = 1; d <= 7; d++) {
            if (table.hasOwnProperty(d)) {
                $('#dia-' + d).html("");
                var c = table[d];
                c.sort(function (a, b) {
                    return moment(a.Hora, 'HH:mm:ss').diff(moment(b.Hora, 'HH:mm:ss'));
                });
                c.forEach(function (h, index) {
                    if (moment().isoWeekday() == d && (index < 1 || moment().isAfter(moment(c[index - 1].Hora, 'HH:mm:ss'))) && moment().isBefore(moment(h.Hora, 'HH:mm:ss'))) {
                        nextHora = h;
                        $('#dia-' + d).append("<li class='list-group-item'><span class='badge badge-pill badge-success'>" + h.Hora.substring(0, h.Hora.length - 3) + "</span></li>");
                        $("#nextBus").html(h.Hora.substring(0, h.Hora.length - 3));
                        var vhorario = new vHorario();
                        vhorario.startTime();
                        $('#nextBusHour').removeClass("d-none");
                    } else {
                        $('#dia-' + d).append("<li class='list-group-item'>" + h.Hora.substring(0, h.Hora.length - 3) + "</li>");
                    }
                    
                });
            } else {
                if (moment().isoWeekday() == d) {
                    nextHora = null;
                }
                $('#dia-' + d).html("<li class='list-group-item text-danger'>Sin servicio</li>");
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

    this.FillComboBoxes = function () {
        this.ctrlActions.FillComboBox("Terminal/ObtenerTerminales", "cbTerminal", "Id", "TerminalName");
        this.ctrlActions.FillComboBox("Ruta/Terminal/1", "cbRuta", "Id", "RutaName");
    }

    this.startTime = function() {
        if (nextHora) {
            var dif = moment.duration(moment(nextHora.Hora, 'HH:mm:ss').diff(moment())).asMinutes();
            if (dif < 0) {
                clearTimeout(t);
                var vhorario = new vHorario();
                vhorario.DrawHorario();
            } else {
                if (dif < 1) {
                    $('#nextBusHour').html("Menos de un minuto restante.");
                } else {
                    $('#nextBusHour').html(parseInt(dif) + " minutos restantes.");
                }
                var vhorario = new vHorario();
                t = setTimeout(vhorario.startTime, 5000);
            }
        } else {
            clearTimeout(t);
            $('#nextBusHour').html("-");
        }
    }
}

var nextHora = null;
var horarios = null;
var t = null;

//ON DOCUMENT READY
$(document).ready(function () {

    var vhorario = new vHorario();
    vhorario.FillComboBoxes();
    vhorario.Retrieve();

    $("#cbTerminal").change(function() {
        var ctrlActions = new ControlActions();
        $("#cbRuta").html("");
        ctrlActions.FillComboBox("Ruta/Terminal/" + $("#cbTerminal").val(), "cbRuta", "Id", "RutaName");
        var vhorario = new vHorario();
        vhorario.Retrieve();
    });

    $("#cbRuta").change(function () {
        var vhorario = new vHorario();
        vhorario.Retrieve();
    });
});