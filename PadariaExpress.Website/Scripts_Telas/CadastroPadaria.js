/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/Util.js" />



$(document).ready(function () {
    carregaMascaras();
    aplicarValidacao();
    carregaUpload();
    buscaPadaria();

    if (IsEmpty(querystring('PadariaId')) == true) {
        $('.opcoesmenu').hide();
    }
});

function gravar(event) {
    event.preventDefault();
    if ($('#cadastroForm').valid()) {
        mostraAguarde();
        var model = mapearPadaria();
        var url = 'API/padaria';

        if (IsEmpty(model.PadariaId) == false) {
            url += '/atualizar/';
        }

        chamadaAPI(url, 'POST', model, function (response) {
            window.location = 'Principal.html?alert=1';
        }, function (erro) {
            ocultaAguarde();
            if (IsEmpty(erro.responseJSON.Message) == false) {
                mostraErro(erro.responseJSON.Message);
            } else {
                mostraErro(erro.responseJSON);
            }
        });
    }
}

function buscarCEP(input) {

    if (IsEmpty(input.value) == false) {
        mostraAguarde();
        var cep = input.value.replace('-', '');

        chamadaAPI('API/localizacao/' + cep, 'GET', {}, function (response) {
            var d = response.d;

            $('#Logradouro').val(d.logradouro);
            $('#Cidade').val(d.localidade);
            $('#Bairro').val(d.bairro);
            $('#Estado').selectpicker('val', d.uf);
            ocultaAguarde();
        }, function (erro) {
            $('#Logradouro').val('');
            $('#Cidade').val('');
            $('#Bairro').val('');
            $('#Estado').selectpicker('val', '');
            ocultaAguarde();
        });
    }
   

}

function buscaPadaria() {

    if (IsEmpty(querystring('PadariaId')) == false) {
        var model = {};
        mostraAguarde();
        chamadaAPI('API/padaria/buscar/' + querystring('PadariaId'), 'GET', model, function (response) {
            carregaPadaria(response.d);
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
}

function carregaPadaria(padaria) {
    $('#NomeFantasia').val(padaria.NomeFantasia);
    $('#RazaoSocial').val(padaria.RazaoSocial);
    $('#CNPJ').val(padaria.CNPJ);
    $('#Descricao').val(padaria.Descricao);
    $('#CEP').val(padaria.CEP);
    $('#Logradouro').val(padaria.Logradouro);
    $('#Numero').val(padaria.Numero);
    $('#Complemento').val(padaria.Complemento);
    $('#Cidade').val(padaria.Cidade);
    $('#Bairro').val(padaria.Bairro);
    $('#DistanciaEntrega').val(padaria.DistanciaEntrega);
    $('#ValorFrete').val(padaria.ValorFrete);
    $('#Email').val(padaria.Email);
    $('#Telefone').val(padaria.Telefone);
    $('#Estado').selectpicker('val', padaria.Estado);
    document.getElementById('chkAtivo').checked = padaria.Ativo;
    if (IsEmpty(padaria.FotoPrincipal) == false) {
        $('#FotoPadaria').attr('src', 'Foto.ashx?PadariaId=' + padaria.PadariaId);
        $('#FotoPadaria').show();
    }
    else {
        $('#FotoProduto').hide();
    }


    for (var i = 0; i < padaria.PeriodosDeFuncionamento.length; i++) {
        $($($('#dia' + padaria.PeriodosDeFuncionamento[i].DiaDaSemana).children()[0]).children()).children()[0].checked = true;

        $($('#dia' + padaria.PeriodosDeFuncionamento[i].DiaDaSemana).children()[2]).children()[0].value = padaria.PeriodosDeFuncionamento[i].HoraAbertura.substring(0,5);
        $($('#dia' + padaria.PeriodosDeFuncionamento[i].DiaDaSemana).children()[3]).children()[0].value = padaria.PeriodosDeFuncionamento[i].HoraFechamento.substring(0, 5);
    }
}

function carregaUpload() {
    $("#Foto").on('change', function () {

        var file, img;
        if ((file = this.files[0])) {
            img = new Image();
            img.onload = function () {
                sendFile(file);
            };
            img.onerror = function () {
                alert("Not a valid file:" + file.type);
            };
            img.src = _URL.createObjectURL(file);
        }
    });

    $("#Foto").fileinput({ 'showUpload': false, 'showPreview': false });

    $('.fileinput-remove').on('click', function (event, key) {
        $.ajax({
            type: 'post',
            url: 'Upload.ashx?DeletarFoto=1&Caminho=' + $("#FotoPadaria").attr("src"),
            data: null,
            success: function (status) {
                if (status != 'error') {
                    $('#FotoPadaria').hide();
                }
            },
            processData: false,
            contentType: false,
            error: function () {
                alert("Whoops something went wrong!");
            }
        });
    });
}

function mapearPadaria() {
    var padaria = new Object();

    padaria.Nome = pegaValorID('Nome');
    padaria.NomeFantasia = pegaValorID('NomeFantasia');
    padaria.RazaoSocial = pegaValorID('RazaoSocial');
    padaria.CNPJ = pegaValorID('CNPJ');
    padaria.Email = pegaValorID('Email');
    padaria.CEP = pegaValorID('CEP');
    padaria.Logradouro = pegaValorID('Logradouro');
    padaria.Numero = pegaValorID('Numero');
    padaria.Complemento = pegaValorID('Complemento');
    padaria.Cidade = pegaValorID('Cidade');
    padaria.Bairro = pegaValorID('Bairro');
    padaria.Estado = pegaValorID('Estado');
    padaria.DistanciaEntrega = pegaValorID('DistanciaEntrega');
    padaria.Descricao = pegaValorID('Descricao');
    padaria.Telefone = pegaValorID('Telefone');
    padaria.ValorFrete = pegaValorID('ValorFrete');
    padaria.Proprietario = buscarProprietario();
    padaria.PeriodosDeFuncionamento = mapearPeriodoFuncionamento();
    padaria.Ativo = document.getElementById('chkAtivo').checked;
    padaria.Caminho = $('#FotoPadaria').attr('src');
    if (IsEmpty(querystring('PadariaId')) == false) {
        padaria.PadariaId = querystring('PadariaId');
    }
    return padaria;
}

function buscarProprietario() {
    var proprietario = window.localStorage.getItem('usuarioProprietario');
    proprietario = JSON.parse(proprietario);
    proprietario.DataCadastro = null;
    proprietario.DataAlteracao = null;
    return proprietario;
}

function mapearPeriodoFuncionamento() {
    var periodos = new Array();

    for (var i = 0; i < 7; i++) {
        if ($($($('#dia' + i).children()[0]).children()).children()[0].checked) {
            var periodo = new Object();
            periodo.DiaDaSemana = i;
            periodo.StrHoraAbertura = $($('#dia' + i).children()[2]).children()[0].value;
            periodo.StrHoraFechamento = $($('#dia' + i).children()[3]).children()[0].value;
            periodos.push(periodo);
        }
    }

    return periodos;
}

function aplicarValidacao() {
    carregaConfiguracaoValidacao();
    $("#cadastroForm").validate({
        ignore: ':not(select:hidden, input:visible, textarea:visible)',
        rules: {
            Nome: {
                required: true
            },
            NomeFantasia: {
                required: true
            },
            RazaoSocial: {
                required: true
            },
            CNPJ: {
                required: true,
                cnpj: true
            },
            Email: {
                required: true,
                email: true
            },
            Logradouro: {
                required: true
            },
            CEP: {
                required: true
            },
            Numero: {
                required: true
            },
            Cidade: {
                required: true
            },
            Bairro: {
                required: true
            },
            DistanciaEntrega: {
                required: true
            },
            Descricao: {
                required: true
            },
            Estado: {
                required: true
            },
            ValorFrete: {
                required: true
            },
            Telefone: {
                required: true
            },
            AberturaDomingo:{
                required: function () {
                    return validaHorario('chkDomingo', 'AberturaDomingo');
                }
            },
            FechamentoDomingo: {
                required: function () {
                    return validaHorario('chkDomingo', 'FechamentoDomingo');
                }
            },
            AberturaSegunda: {
                required: function () {
                    return validaHorario('chkSegunda', 'AberturaSegunda');
                }
            },
            FechamentoSegunda: {
                required: function () {
                    return validaHorario('chkSegunda', 'FechamentoSegunda');
                }
            },
            AberturaTerca: {
                required: function () {
                    return validaHorario('chkTerca', 'AberturaTerca');
                }
            },
            FechamentoTerca: {
                required: function () {
                    return validaHorario('chkTerca', 'FechamentoTerca');
                }
            },
            AberturaQuarta: {
                required: function () {
                    return validaHorario('chkQuarta', 'AberturaQuarta');
                }
            },
            FechamentoQuarta: {
                required: function () {
                    return validaHorario('chkQuarta', 'FechamentoQuarta');
                }
            },
            AberturaQuinta: {
                required: function () {
                    return validaHorario('chkQuinta', 'AberturaQuarta');
                }
            },
            FechamentoQuinta: {
                required: function () {
                    return validaHorario('chkQuinta', 'FechamentoQuarta');
                }
            },
            AberturaSexta: {
                required: function () {
                    return validaHorario('chkSexta', 'AberturaSexta');
                }
            },
            FechamentoSexta: {
                required: function () {
                    return validaHorario('chkSexta', 'FechamentoSexta');
                }
            },
            AberturaSabado: {
                required: function () {
                    return validaHorario('chkSabado', 'AberturaSabado');
                }
            },
            FechamentoSabado: {
                required: function () {
                    return validaHorario('chkSabado', 'FechamentoSabado');
                }
            }
        },
        messages: {
            Nome: {
                required: 'Campo Obrigatório.'
            },
            NomeFantasia: {
                required: 'Campo Obrigatório.'
            },
            RazaoSocial: {
                required: 'Campo Obrigatório.'
            },
            CNPJ: {
                required: 'Campo Obrigatório.',
                cnpj: 'Informe um CNPJ válido.'
            },
            Email: {
                required: 'Campo Obrigatório.',
                email: 'Email inválido.'
            },
            Logradouro: {
                required: 'Campo Obrigatório.'
            },
            CEP: {
                required: 'Campo Obrigatório.'
            },
            Numero: {
                required: 'Campo Obrigatório.'
            },
            Cidade: {
                required: 'Campo Obrigatório.'
            },
            Bairro: {
                required: 'Campo Obrigatório.'
            },
            DistanciaEntrega: {
                required: 'Campo Obrigatório.'
            },
            Descricao: {
                required: 'Campo Obrigatório.'
            },
            Estado: {
                required: 'Campo Obrigatório.'
            },
            ValorFrete: {
                required: 'Campo Obrigatório.'
            },
            Telefone: {
                required: 'Campo Obrigatório.'
            },
            AberturaDomingo: {
                required: 'Campo Obrigatório.'
            },
            FechamentoDomingo: {
                required: 'Campo Obrigatório.'
            },
            AberturaSegunda: {
                required: 'Campo Obrigatório.'
            },
            FechamentoSegunda: {
                required: 'Campo Obrigatório.'
            },
            AberturaTerca: {
                required: 'Campo Obrigatório.'
            },
            FechamentoTerca: {
                required: 'Campo Obrigatório.'
            },
            AberturaQuarta: {
                required: 'Campo Obrigatório.'
            },
            FechamentoQuarta: {
                required: 'Campo Obrigatório.'
            },
            AberturaQuinta: {
                required: 'Campo Obrigatório.'
            },
            FechamentoQuinta: {
                required: 'Campo Obrigatório.'
            },
            AberturaSexta: {
                required: 'Campo Obrigatório.'
            },
            FechamentoSexta: {
                required: 'Campo Obrigatório.'
            },
            AberturaSabado: {
                required: 'Campo Obrigatório.'
            },
            FechamentoSabado: {
                required: 'Campo Obrigatório.'
            }
        }
    });

}

function validaHorario(chk, input) {
    if (document.getElementById(chk).checked) {
        if (IsEmpty($('#' + input).val()) == true) {
            return true;
        }
        else {
            var timeStr = $('#' + input).val();
            var valido = (timeStr.search(/^\d{2}:\d{2}:\d{2}$/) != -1) &&
            (timeStr.substr(0, 2) >= 0 && timeStr.substr(0, 2) <= 24) &&
            (timeStr.substr(3, 2) >= 0 && timeStr.substr(3, 2) <= 59) &&
            (timeStr.substr(6, 2) >= 0 && timeStr.substr(6, 2) <= 59);

            return !valido;
        }
    }
    else {
        return false;
    }
}

function sendFile(file) {

    var formData = new FormData();
    formData.append('file', $('#Foto')[0].files[0]);
    $.ajax({
        type: 'post',
        url: 'Upload.ashx?FotoPadaria=1',
        data: formData,
        success: function (status) {
            if (status != 'error') {
                var my_path = "FotosPadarias/" + status;
                $("#FotoPadaria").attr("src", my_path);
                $('#FotoPadaria').show();
            }
        },
        processData: false,
        contentType: false,
        error: function () {
            alert("Whoops something went wrong!");
        }
    });
}

var _URL = window.URL || window.webkitURL;