Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';

$(document).ready(function () {
    auth.SetTerminales();
    LoadUserInfo();
    RenderCharts();
});

function LoadUserInfo() {
    $("#userName").text(`${auth.GetCurrentUser().Nombre} ${auth.GetCurrentUser().Apellidos}`);
}

function RenderCharts() {
    GetData(LoadOnMemory, "Report/GetReportAllGananciasAllTerminales");
}

function getRandomColor() {
    var letters = '0123456789ABCDEF'.split('');
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

var allElements = [];

function LoadOnMemory(apiResponse) {
    var terminales = [... new Set(apiResponse.Data.map(e => e.Label))];
    terminales.forEach(t => allElements.push(apiResponse.Data.filter(r => r.Label === t)));
    DrawLineChart();
}

function GetData(callback, apiService) {
    var ctrlActions = new ControlActions();
    $.get({
        url: ctrlActions.GetUrlApiService(apiService),
        headers: { 'Authorization': auth.GetAccessToken() }
    }).done(
        function (response) {
            callback(response);
        })
        .fail(function (response) {
            var data = response.responseJSON;
            console.log(data);
            alert(data.Message);
            sessionStorage.clear();
        });
}

var myLineChart;
function DrawLineChart() {
    var ctx = document.getElementById("myAreaChart");
    if (typeof myLineChart != 'undefined') {
        myLineChart.destroy();
    }
    var rederElement = CreateLineObject();
    myLineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: rederElement.Labels,
            datasets: [{
                label: rederElement.TerminalName,
                lineTension: 0.3,
                backgroundColor: "rgba(2,117,216,0.2)",
                borderColor: "rgba(2,117,216,1)",
                pointRadius: 5,
                pointBackgroundColor: "rgba(2,117,216,1)",
                pointBorderColor: "rgba(255,255,255,0.8)",
                pointHoverRadius: 5,
                pointHoverBackgroundColor: "rgba(2,117,216,1)",
                pointHitRadius: 20,
                pointBorderWidth: 2,
                data: rederElement.Values
            }],
        },
        options: {
            scales: {
                xAxes: [{
                    time: {
                        displayFormats: { 'day': 'MM/YY' },
                        tooltipFormat: 'YYYY/MM/DD',
                        unit: 'month'
                    },
                    gridLines: {
                        display: false
                    },
                    ticks: {
                        maxTicksLimit: 7
                    }
                }],
                yAxes: [{
                    ticks: {
                        min: 0,
                        maxTicksLimit: 5
                    },
                    gridLines: {
                        color: "rgba(0, 0, 0, .125)",
                    }
                }],
            },
            legend: {
                display: false
            }
        }
    });
    window.Line = myLineChart;
}

function CreateLineObject() {
    var obj = {};
    var terminalName = auth.GetCurrentListaTerminales().filter(ter => ter.Id == auth.GetCurrentTerminal());
    var getElementsForChart = [];

    allElements.forEach(function (value, index) {
        if (terminalName[0].TerminalName === value[0].Label)
            getElementsForChart = value;
    });
    obj.Labels = getElementsForChart.map(e => e.AdditionalValue);
    obj.Values = getElementsForChart.map(e => e.Value);
    obj.TerminalName = terminalName[0].TerminalName;
    $("#cobro").text(LoadGanancias(obj.Values));
    return obj;
}

function LoadGanancias(element) {
    var result = 0;
    element.forEach(e => {
        result += e;
    });

    return result;
}

function GetUniqueData(list) {
    var obj = {};
    obj.Values = list.map(e => e.Value);
    obj.Label = list.map(e => e.Label);
    return obj;
}

function Colors(list) {
    var result = [];
    list.forEach(e => result.push(getRandomColor()));
    return result;
}
