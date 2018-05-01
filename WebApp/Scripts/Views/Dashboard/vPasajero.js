Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';

$(document).ready(function () {
    LoadUserInfo();
    RenderCharts();
    $("#terminalSelector").change(DrawLineChart);
});

function LoadUserInfo() {
    $("#userName").text(`${auth.GetCurrentUser().Nombre} ${auth.GetCurrentUser().Apellidos}`);
}

function RenderCharts() {
    GetData(LoadOnMemory, "Report/GetMovimientosUsuario?email=" + auth.GetCurrentUser().Email);
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
    DrawLineChart(apiResponse);
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
function DrawLineChart(apiResponse) {
    var ctx = document.getElementById("myAreaChart");
    var rederElement = CreateLineObject(apiResponse.Data);
    myLineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: rederElement.Labels,
            datasets: [{
                label: rederElement.Name[0],
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
            tooltips: {
                callbacks: {
                    labels: function (tooltipItem, data) {
                        var label = data.labels[tooltipItem.index];
                        return label + ': '+ tooltipItem.xLabel;
                    }
                }
            },
            legend: {
                display: false
            }
        }
    });
}

function CreateLineObject(apiResponse) {
    var obj = {};  
    obj.Labels = apiResponse.map(e => e.AdditionalValue);
    obj.Values = apiResponse.map(e => e.Value);
    obj.Name = apiResponse.map(e => e.Label);
    return obj;
}