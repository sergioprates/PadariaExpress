/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/bootstrap.js" />
/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/Util.js" />
/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/jquery.validate-vsdoc.js" />


$(document).ready(function () {
    carregaDatepicker();
    carregaValidacao();
});

google.load('visualization', '1.1', { packages: ['line'] });
//google.setOnLoadCallback(buscarDados);


function gerarRelatorio(event) {
    if ($('#frmRelatorio').valid()) {
        event.preventDefault();
        buscarDados();
    }   
}

function buscarDados() {
    mostraAguarde();
    var obj = new Object();
    var strDataInicial = pegaValorID('DataInicial');
    var strDataFinal = pegaValorID('DataFinal');
    var arr = strDataInicial.split('/');
    var dataInicial = arr[2] + arr[1] + arr[0];

    var arr2 = strDataFinal.split('/');
    var dataFinal = arr2[2] + arr2[1] + arr2[0];
    chamadaAPI('api/relatoriofaturamento/googlecharts/linha/' + querystring('PadariaId') + '/' + dataInicial + '/' + dataFinal,
        'GET', obj, function (data) {
            desenhaGrafico(data.d);
            ocultaAguarde();
        }, function (erro) {
        });
}

function dataToUTC(data) {
    var arr = data.split('/');
}

function desenhaGrafico(dados) {
    var formatterDate = new google.visualization.DateFormat({ pattern: 'dd/MM/yyyy' });
    var formatterNumbers = new google.visualization.NumberFormat({ pattern: 'R$ ###,###' });
    var data = new google.visualization.DataTable();
    data.addColumn('string', dados[0][0]);
    for (var i = 1; i < dados[0].length; i++) {
        data.addColumn('number', dados[0][i]);
    }

    for (var i = 1; i < dados.length; i++) {
        //pedidos[i].DataPedido.replace('T', ' ') + ' UTC'
        //dados[i][0] = formatterDate.formatValue(new Date(dados[i][0].toString().substring(0, 4), parseInt(dados[i][0].toString().substring(4, 6)) - 1, dados[i][0].toString().substring(6, 8)));
        dados[i][0] = formatterDate.formatValue(new Date(dados[i][0].toString().substring(0, 4) + '-' + dados[i][0].toString().substring(4, 6) + '-' + dados[i][0].toString().substring(6, 8) + ' UTC'));
        data.addRow(dados[i]);
    }

    //data.addRows([
    //  [1, 37.8, 80.8, 41.8],
    //  [2, 30.9, 69.5, 32.4],
    //  [3, 25.4, 57, 25.7],
    //  [4, 11.7, 18.8, 10.5],
    //  [5, 11.9, 17.6, 10.4],
    //  [6, 8.8, 13.6, 7.7],
    //  [7, 7.6, 12.3, 9.6],
    //  [8, 12.3, 29.2, 10.6],
    //  [9, 16.9, 42.9, 14.8],
    //  [10, 12.8, 30.9, 11.6],
    //  [11, 5.3, 7.9, 4.7],
    //  [12, 6.6, 8.4, 5.2],
    //  [13, 4.8, 6.3, 3.6],
    //  [14, 4.2, 6.2, 3.4]
    //]);
    formatterNumbers.format(data, 1);
    formatterNumbers.format(data, 2);
    formatterNumbers.format(data, 3);
    formatterNumbers.format(data, 4);
    formatterNumbers.format(data, 5);
    formatterNumbers.format(data, 6);

    var options = {
        chart: {
            title: 'Faturamento por status',
            subtitle: 'em reais (R$)'
        },
        height: 500
    };

    var chart = new google.charts.Line(document.getElementById('grafico'));

    chart.draw(data, options);
}

function carregaValidacao() {
    carregaConfiguracaoValidacao();

    $("#frmRelatorio").validate({
        ignore: ':not(select:hidden, input:visible, textarea:visible)',
        rules: {
            DataFinal: {
                greaterThan: "#DataInicial",
                required: true
            },
            DataInicial: {
                lessThan: "#DataFinal",
                required: true
            }
        },
        messages:
            {
                DataFinal: {
                    greaterThan: "Data final deve ser maior que a data inicial",
                    required: 'Campo obrigatório'
                },
                DataInicial: {
                    lessThan: "Data inicial deve ser menor que a data final",
                    required: 'Campo obrigatório'
                }
            }
    });

    $("#cadastroForm").validate({
        ignore: ':not(select:hidden, input:visible, textarea:visible)',
        rules: {
            Nome: {
                required: true
            },
            Descricao: {
                required: true
            }
        },
        messages: {
            Nome: {
                required: 'Campo obrigatório.'
            },
            Descricao: {
                required: 'Campo obrigatório.'
            }
        }
    });
}


function carregaDatepicker() {
    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy',
        endDate: 'today',
        language: 'pt-BR',
        autoclose: true
    });
}