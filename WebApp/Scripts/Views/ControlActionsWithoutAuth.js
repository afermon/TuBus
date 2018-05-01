function ControlActions() {

    this.URL_API = $(location).attr("origin").replace("web", "api") + "/api/";
    this.actionLinks = '<a href="#" data-action="view"><i class="fa fa-eye" aria-hidden="true"></i></a> ' +
        '<a href="#" data-action="edit"><i class="fa fa-pencil" aria-hidden="true"></i></a> ';

    this.GetUrlApiService = function (service) {
        return this.URL_API +"0/"+ service;
    }

    this.GetTableColumsDataName = function (tableId) {
        var val = $('#' + tableId).attr("ColumnsDataName");

        return val;
    }

    this.FillTable = function (service, tableId, refresh, actions = this.actionLinks) {
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
                            return actions;
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
                    dataSrc: 'Data'
                },
                "columns": arrayColumnsData
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


    this.ShowMessage = function (type, message) {
        if (type == 'E') {
            $("#alert_container").removeClass("alert alert-success alert-dismissable")
            $("#alert_container").addClass("alert alert-danger alert-dismissable");
            $("#alert_message").text(message);
        } else if (type == 'Inactivo') {
            $("#alert_container").removeClass("alert alert-danger alert-dismissable")
            $("#alert_container").addClass("alert alert-success alert-dismissable");
            $("#alert_message").text(message);
        }
        $('.alert').show();
    };

    this.FillComboBox = function (service, comboBoxId, valueKey, textKey) {
        var jqxhr = $.get({
            url: this.GetUrlApiService(service),
            async: false
        }).done(function (response) {
            $(response.Data).each(function () {
                var option = $("<option />");
                option.attr("value", this[valueKey]).text(this[textKey]);

                $("#" + comboBoxId).append(option);
            });
        }).fail(function (response) {
            console.log(response);
        });
    }

    this.PostToAPI = function (service, data) {
        var jqxhr = $.post({
            url: this.GetUrlApiService(service),
            data: data,
            async: false
        }).done(function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('Inactivo', response.Message);
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
                console.log(data);
            });
    };

    this.PutToAPI = function (service, data) {
        var jqxhr = $.put(this.GetUrlApiService(service), data, function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('Inactivo', response.Message);
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
                console.log(data);
            });
    };

    this.DeleteToAPI = function (service, data) {
        var jqxhr = $.delete(this.GetUrlApiService(service), data, function (response) {
            var ctrlActions = new ControlActions();
            ctrlActions.ShowMessage('Inactivo', response.Message);
        })
            .fail(function (response) {
                var data = response.responseJSON;
                var ctrlActions = new ControlActions();
                ctrlActions.ShowMessage('E', data.ExceptionMessage);
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
        success: callback,
        data: JSON.stringify(data),
        contentType: 'application/json'
    });
}