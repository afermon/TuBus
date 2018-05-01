function ControlActions() {

    this.URL_API = $(location).attr("origin").replace("web", "api") + "/api/";
    this.actionLinks = '<a href="#" data-action="view"><i class="fa fa-eye" aria-hidden="true"></i></a> ' +
        '<a href="#" data-action="edit"><i class="fa fa-pencil" aria-hidden="true"></i></a> ';
    this.actionLinksDelete = ' <a href="#" data-action="delete"><i class="fa fa-trash-o" aria-hidden="true"></i></a>';
    this.actionLinksActivate = ' <a href="#" data-action="delete"><i class="fa fa-undo" aria-hidden="true"></i></a>';

    this.GetUrlApiService = function (service, terminal = true) {
        var terminalPath = "";

        if (terminal) {
            try {
                terminalPath = auth.GetCurrentTerminal();
                if (terminalPath == null)
                    terminalPath = "0";
                terminalPath += "/";
            }
            catch (err) {
                terminalPath = "0/";
            }
        }

        return this.URL_API + terminalPath + service;
    }

    this.GetTableColumsDataName = function (tableId) {
        var val = $('#' + tableId).attr("ColumnsDataName");

        return val;
    }

    this.FillTable = function (service, tableId, refresh, actions = this.actionLinks) {
        var actionsDelete = this.actionLinksDelete;
        var actionsActivate = this.actionLinksActivate;
        if (!refresh) {
            columns = this.GetTableColumsDataName(tableId).split(',');
            var arrayColumnsData = [];

            $.fn.dataTable.ext.errMode = function (settings, helpPage, message) {
                console.log(message);
                var ctr = new ControlActions();
                ctr.ShowMessage('E', "Error al cargar la tabla.");
            };

            $.each(columns, function (index, value) {
                var obj = {};
                obj.data = value;
                if (value === "Acciones") {
                    obj = {
                        sortable: false,
                        data: null,
                        render: function (data, type, full, meta) {

                            if (data.hasOwnProperty("Estado")) {
                                if (data.Estado == "Activo" || data.Estado == "Pendiente") {
                                    return actions + actionsDelete;
                                } else {
                                    return actions + actionsActivate;
                                }
                            }
                            else if (data.hasOwnProperty("EstadoTarjeta") || data.hasOwnProperty("EstadoPago")) {
                                return actions;
                            }
                        }
                    };
                }
                arrayColumnsData.push(obj);
            });

            $('#' + tableId).DataTable({
                "language": {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                },
                "processing": true,
                "ajax": {
                    "url": this.GetUrlApiService(service),
                    'headers': {
                        'Authorization': auth.GetAccessToken()
                    },
                    dataSrc: 'Data'
                },
                "columns": arrayColumnsData,
                initComplete: function () {
                    var columns = this.api().settings().init().columns;
                    this.api().columns().every(function (index) {
                        var column = this;
                        if (columns[index].data == "Estado") {
                            $('#dataTables-example .head .head_hide').html('');

                            var select = $('<select id="formfilter" class="filterdropdown"><option value="">' +
                                $(column.header()).text() +
                                '</option></select>')
                                .appendTo($(column.header()).empty())
                                .on('change',
                                function () {
                                    var val = $.fn.dataTable.util.escapeRegex(
                                        $(this).val()
                                    );
                                    column
                                        .search(val ? '^' + val + '$' : '', true, false)
                                        .draw();
                                });

                            column.data().unique().sort().each(function (d, j) {
                                select.append('<option value="' + d + '">' + d + '</option>');
                                select.val("Activo");
                                select.trigger("change");
                            });
                        }
                    });
                }
            });
        } else {
            //RECARGA LA TABLA
            $('#' + tableId).DataTable().ajax.reload();
        }
    }

    this.GetSelectedRow = function () {
        var data = sessionStorage.getItem(tableId + '_selected');

        return data;
    };

    this.BindFields = function (formId, data) {
        $('#' + formId + ' *').filter(':input').each(function (input) {
            if ($(this).attr("type") !== "password") {
                var columnDataName = $(this).attr("ColumnDataName");
                this.value = data[columnDataName];
            }
        });
    }

    this.GetDataForm = function (formId) {
        var data = {};

        $('#' + formId + ' *').filter(':input', 'select').each(function (input) {
            var columnDataName = $(this).attr("ColumnDataName");
            data[columnDataName] = $(this).val();
        });

        return data;
    }


    this.ShowMessage = function (type, message, callback = "") {
        if (type == 'E') {
            $("#AlertModal .modal-body").removeClass("successLabel");
            $("#AlertModal .modal-body").addClass("errorLabel");
            alert("Error: " + message);
            $("#AlertModal #acept").click(callback);
        } else if (type == 'Inactivo') {
            $("#AlertModal .modal-body").removeClass("errorLabel");
            $("#AlertModal .modal-body").addClass("successLabel");
            alert("Éxito " + message);
            $("#acept").click(callback);
        }
    };

    this.FillComboBox = function (service, comboBoxId, valueKey, textKey) {
        var jqxhr = $.get({
            url: this.GetUrlApiService(service),
            headers: { 'Authorization': auth.GetAccessToken() },
            async: false
        }).done(function (response) {
            sessionStorage.setItem("list-" + comboBoxId, JSON.stringify(response.Data));
            if (response.Data.length == 0) {
                var option = $("<option />");
                option.attr("value", "").text("Sin elementos");
                option.prop('disabled', true);
                $("#" + comboBoxId).append(option);
            }
            $(response.Data).each(function () {
                var option = $("<option />");
                option.attr("value", this[valueKey]).text(this[textKey]);
                var estado = "";
                if (this.hasOwnProperty("Estado") && this["Estado"] != "Activo") {
                    option.prop('disabled', true);
                    option.text(option.text() + " - Inactivo");
                }

                $("#" + comboBoxId).append(option);
            });
        }).fail(function (response) {
            console.log(response);
        });
    };

    this.FillIframe = function (service, frameId) {
        var xhr = new XMLHttpRequest();
        xhr.open('GET', this.GetUrlApiService(service), true);
        xhr.responseType = 'blob';
        xhr.setRequestHeader('Authorization', auth.GetAccessToken());
        xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
        xhr.onload = function (e) {
            if (this.status == 200) {
                var blob = new Blob([this.response], { type: 'application/pdf' });
                var downloadUrl = URL.createObjectURL(blob);
                $("#" + frameId).attr('src', downloadUrl);
            } else {
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', 'Error al generar el reporte');
            }
        };
        xhr.send();
    };

    this.GetData = function (service, data) {
        var jqxhr = $.get({
            url: this.GetUrlApiService(service),
            headers: { 'Authorization': auth.GetAccessToken() },
            //data: data,
            async: false
        }).done(function (response) {
            return response;
        }).fail(function (response) {
            console.log(response);
        });
    };

    this.GetUrlParameter = function getUrlParameter(sParam) {
        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : sParameterName[1];
            }
        }
    };

    this.PostToAPI = function (service, data, callback = "") {
        var jqxhr = $.post({
            url: this.GetUrlApiService(service),
            headers: { 'Authorization': auth.GetAccessToken() },
            data: data,
            async: false
        }).done(function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('Inactivo', response.Message, callback);

        }).fail(function (response) {
            var data = response.responseJSON;
            var ctrlActions = new ControlActions();

            ctrlActions.ShowMessage('E', data.ExceptionMessage ? data.ExceptionMessage : data.Message);
        });
    };

    this.PutToAPI = function (service, data, callBack = "") {
        var jqxhr = $.put(this.GetUrlApiService(service), data, function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('Inactivo', response.Message, callBack);
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage ? data.ExceptionMessage : data.Message);
                console.log(data);
            });
    };

    this.DeleteToAPI = function (service, data, callBack = "") {
        var jqxhr = $.delete(this.GetUrlApiService(service), data, function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('Inactivo', response.Message, callBack);
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage ? data.ExceptionMessage : data.Message);
                console.log(data);
            });
    };
}

//Custom jquery actions
$.put = function (url, data, callback) {
    if ($.isFunction(data)) {
        type = type || callback,
            callback = data,
            data = {}
    }
    return $.ajax({
        url: url,
        type: 'PUT',
        headers: { 'Authorization': auth.GetAccessToken() },
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
}

$.delete = function (url, data, callback) {
    if ($.isFunction(data)) {
        type = type || callback,
            callback = data,
            data = {}
    }
    return $.ajax({
        url: url,
        type: 'DELETE',
        headers: { 'Authorization': auth.GetAccessToken() },
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
}