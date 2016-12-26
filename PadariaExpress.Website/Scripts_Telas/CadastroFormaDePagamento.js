/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/Util.js" />

$(document).ready(function () {    
    carregaBandeiras();
    carregaValidacao();

    
});

function buscaFormaDePagamento() {

    if (IsEmpty(querystring('FormaDePagamentoId')) == false) {
        var model = {};
        chamadaAPI('API/formasdepagamento/buscar/' + querystring('FormaDePagamentoId'), 'GET', model, function (response) {
            carregaFormaDePagamento(response.d);
            controlaDivBandeira($('#TipoFormaDePagamento'));
            ocultaAguarde();
        }, function (erro) {
            ocultaAguarde();
            if (IsEmpty(erro.responseJSON.Message) == false) {
                mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON.Message, 'danger');
            } else {
                mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON, 'danger');
            }
        });
    }
    else
    {
        ocultaAguarde();
    }
}

function carregaFormaDePagamento(formaDePagamento) {
    $('#Nome').val(formaDePagamento.Nome);
    $('#TipoFormaDePagamento').val(formaDePagamento.Tipo);
    $('#TipoFormaDePagamento').selectpicker('refresh');
    if (formaDePagamento.BandeiraCartao != undefined && formaDePagamento.BandeiraCartao != null) {
        $('#BandeiraCartao').val(formaDePagamento.BandeiraCartao.BandeiraCartaoId);
        $('#BandeiraCartao').selectpicker('refresh');
    }
    
    document.getElementById('chkAtivo').checked = formaDePagamento.Ativo;
}

function gravar(event) {
    event.preventDefault();
    if ($('#cadastroForm').valid()) {
        var model = mapearFormaDePagamento();
        var url = 'API/formasdepagamento';

        if (IsEmpty(model.FormaDePagamentoId) == false) {
            url += '/atualizar/';
        }

        chamadaAPI(url, 'POST', model, function (response) {
            window.location = 'FormasDePagamento.html?alert=1&PadariaId=' + querystring('PadariaId');
        }, function (erro) {
            if (IsEmpty(erro.responseJSON.Message) == false) {
                mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON.Message, 'danger');
            } else {
                mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON, 'danger');
            }
        });
    }
}

function mapearFormaDePagamento() {
    var formaDePagamento = new Object();
    if (pegaValorID('TipoFormaDePagamento') != '0') {
        formaDePagamento.BandeiraCartao = new Object();
        formaDePagamento.BandeiraCartao.BandeiraCartaoId = pegaValorID('BandeiraCartao');
    }
    
    formaDePagamento.Ativo = document.getElementById('chkAtivo').checked;
    formaDePagamento.Nome = pegaValorID('Nome');
    formaDePagamento.Tipo = pegaValorID('TipoFormaDePagamento');
    formaDePagamento.Padaria = new Object();    
    formaDePagamento.Padaria.PadariaId = querystring('PadariaId');
    if (IsEmpty(querystring('FormaDePagamentoId')) == false) {
        formaDePagamento.FormaDePagamentoId = querystring('FormaDePagamentoId');
    }

    return formaDePagamento;
}

function carregaBandeiras() {
    var model = {};
    mostraAguarde();
    chamadaAPI('API/bandeiras/', 'GET', model, function (response) {
        var bandeiras = response.d;
        populaSelectBandeiras(bandeiras);
        buscaFormaDePagamento();
    }, function (erro) {
        ocultaAguarde();
        if (IsEmpty(erro.responseJSON.Message) == false) {
            mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON.Message, 'danger');
        } else {
            mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON, 'danger');
        }
    });
}

function populaSelectBandeiras(bandeiras) {
    var select = document.getElementById('BandeiraCartao');
    select.options.length = 0; // clear out existing items
    for (var i = 0; i < bandeiras.length; i++) {
        var option = document.createElement("option");
        option.text = bandeiras[i].Nome;
        option.value = bandeiras[i].BandeiraCartaoId;
        select.add(option);
    }

    var option = document.createElement("option");
    option.text = 'Selecione...';
    option.value = '';
    select.add(option, 0);
    $(select).selectpicker('refresh');
}

function controlaDivBandeira(ddl) {
    switch ($(ddl).val()) {
        case '0':
            $('#divBandeiraCartao').hide();
            break;
        case '1':
            $('#divBandeiraCartao').show();
            break;
        default:
            $('#divBandeiraCartao').hide();
            break;
    }
}

function carregaValidacao() {

    carregaConfiguracaoValidacao();
    $("#cadastroForm").validate({
        ignore: ':not(select:hidden, input:visible, textarea:visible)',
        rules: {
            Nome: {
                required: true
            },
            TipoFormaDePagamento: {
                required: true
            },
            BandeiraCartao: {
                required: function (element) {                   
                    if ($('#TipoFormaDePagamento').val() == '1') {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        },
        messages: {
            Nome: {
                required: 'Campo obrigatório.'
            },
            TipoFormaDePagamento: {
                required: 'Campo obrigatório.'
            },
            BandeiraCartao: {
                required: 'Campo obrigatório.'
            }
        }
    });

}