/// <reference path="jquery-2.1.4-vsdoc.js" />
/// <reference path="bootstrap.js" />
/// <reference path="jquery.validate-vsdoc.js" />
/// <reference path="bootstrap-table.js" />



function adicionaEstilosInputs() {
    $('.form-group').addClass('has-success');
}

function mostraAguarde() {
    $('#modalAguarde').modal('show');
}

function ocultaAguarde() {
    $('#modalAguarde').modal('hide');
}

function mostraErro(mensagem) {
    $('#lblMensagem', $('#modalErro')).html(mensagem);
    $('#modalErro').modal('show');    
}

function ocultaErro() {
    $('#modalErro').modal('hide');
}

function mostraSucesso(msg, funcao) {
    $('#lblMensagemSucesso').html(msg);
    $('#btnSucesso').on('click', funcao);
    $('#modalSucesso').modal('show');
}

function ocultaSucesso() {
    $('#modalSucesso').modal('hide');
}

function mostraConfirmacao(msg, funcao) {
    $('#lblMensagemConfirmacao').html(msg);
    $('#btnSim').on('click', funcao);
    $('#modalConfirmacao').modal('show');
}

function ocultaConfirmacao() {
    $('#modalConfirmacao').modal('hide');
}

function chamadaAPI(url, tipo, obj, sucesso, erro) {

    $.ajax({
        type: tipo,
        dataType: "json",
        url: url,
        data: JSON.stringify(obj),
        contentType: 'application/json',
        success: sucesso,
        error: erro
    });
}

function validaSessao() {
    if (IsEmpty(window.localStorage.getItem('usuarioProprietario')) == true) {
        window.location = 'Index.html';
    }
}


function sair(event) {
    event.preventDefault();
    window.localStorage.removeItem('usuarioProprietario');
    window.localStorage.removeItem('usuarioFuncionario');
    window.location = 'Index.html';
}

function pegaValorID(id) {
    return $('#' + id).val();
}

function IsEmpty(data) {
    if (typeof (data) == 'number' || typeof (data) == 'boolean') {
        return false;
    }
    if (typeof (data) == 'undefined' || data === null) {
        return true;
    }
    if (typeof (data.length) != 'undefined') {
        return data.length == 0;
    }
    var count = 0;
    for (var i in data) {
        if (data.hasOwnProperty(i)) {
            count++;
        }
    }
    return count == 0;
}

function carregaMascaras() {
    $('.data').mask('00/00/0000');
    $('.cpf').mask('999.999.999-99');
    $('.cnpj').mask('99.999.999/9999-99');
    $('.cep').mask('99999-999');
    $('.hora').mask('99:99');
    $('.telefone').mask('(99) 9999-9999');
}

function carregaConfiguracaoValidacao() {
    $.validator.setDefaults({
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
            $(element).closest('.form-group').removeClass('has-success');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
            $(element).closest('.form-group').addClass('has-success');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }

            if (element.hasClass('selectpicker')) {
                error.insertAfter('.bootstrap-select');
            } else {
                error.insertAfter(element);
            }
        }
    });


    adicionaValidacaoCNPJ();
    adicionaValidacaoDATA();
    adicionaValidacaoCPF();
    adicionaValidacaoDATANASCIMENTO();
    adicionaValidacaoRangeDatasMenorQue();
    adicionaValidacaoRangeDatasMaiorQue();
}

function mostraMensagem(div, label, mensagem, cssClass) {
    ocultaMensagem();
    $('#' + div).addClass('alert-' + cssClass);
    $('#' + div).addClass('alert');
    $('#' + label).html(mensagem);
    $('#' + div).show();
}

function ocultaMensagem(div) {
    $('#' + div).hide();
    $('#' + div).removeClass();
}

function strDataToDate(strData) {
    var arr = strData.split('/');
    if (arr.length != 3) {
        return null;
    }

    var data = new Date(arr[2], (parseInt(arr[1]) - 1), arr[0]);

    return data;
}

function adicionaValidacaoDATA() {

    jQuery.validator.addMethod("data", function (strData, element) {
        return strDataToDate(strData) != null;
    }, 'Data inválida.');
}

function adicionaValidacaoDATANASCIMENTO() {

    jQuery.validator.addMethod("datanascimento", function (strData, element) {
        var dataNascimento = strDataToDate(strData);
        var dataAtual = Date.parse(new Date().toDateString())
        if (dataNascimento >= dataAtual) {
            return false;
        }
        else {
            return true;
        }
    }, 'Data inválida.');
}

function adicionaValidacaoRangeDatasMaiorQue() {

    jQuery.validator.addMethod("greaterThan",
function (value, element, params) {

    if (IsEmpty(value) == true) {
        return false;
    }

    if (IsEmpty($(params).val()) == true) {
        return false;
    }


    var arr = value.split('/');
    var data = new Date(arr[2], parseInt(arr[1]) - 1, arr[0], 0, 0, 0, 0);

    var dataComparada = $(params).val().split('/');

    var data2 = new Date(dataComparada[2], parseInt(dataComparada[1]) - 1, dataComparada[0], 0, 0, 0, 0);
    

    return data >= data2;

    return 
}, 'Deve ser maior do que {0}.');
}

function adicionaValidacaoRangeDatasMenorQue() {

    jQuery.validator.addMethod("lessThan",
function (value, element, params) {

    if (IsEmpty(value) == true) {
        return false;
    }

    if (IsEmpty($(params).val()) == true) {
        return false;
    }


    var arr = value.split('/');
    var data = new Date(arr[2], parseInt(arr[1]) - 1, arr[0], 0, 0, 0, 0);

    var dataComparada = $(params).val().split('/');

    var data2 = new Date(dataComparada[2], parseInt(dataComparada[1]) - 1, dataComparada[0], 0, 0, 0, 0);


    return data <= data2;
}, 'Deve ser menor do que {0}.');
}

function replaceAll(find, replace, str) {
    return str.replace(new RegExp(find, 'g'), replace);
}

function adicionaValidacaoCPF() {

    jQuery.validator.addMethod("cpf", function (cpf, element) {
        cpf = cpf.replace(/[^\d]+/g, '');
        if (cpf == '') return false;
        // Elimina CPFs invalidos conhecidos    
        if (cpf.length != 11 ||
            cpf == "00000000000" ||
            cpf == "11111111111" ||
            cpf == "22222222222" ||
            cpf == "33333333333" ||
            cpf == "44444444444" ||
            cpf == "55555555555" ||
            cpf == "66666666666" ||
            cpf == "77777777777" ||
            cpf == "88888888888" ||
            cpf == "99999999999")
            return false;
        // Valida 1o digito 
        add = 0;
        for (i = 0; i < 9; i++)
            add += parseInt(cpf.charAt(i)) * (10 - i);
        rev = 11 - (add % 11);
        if (rev == 10 || rev == 11)
            rev = 0;
        if (rev != parseInt(cpf.charAt(9)))
            return false;
        // Valida 2o digito 
        add = 0;
        for (i = 0; i < 10; i++)
            add += parseInt(cpf.charAt(i)) * (11 - i);
        rev = 11 - (add % 11);
        if (rev == 10 || rev == 11)
            rev = 0;
        if (rev != parseInt(cpf.charAt(10)))
            return false;
        return true;
    }, "Informe um CPF válido.");
}

function adicionaValidacaoCNPJ() {
    jQuery.validator.addMethod("cnpj", function (cnpj, element) {
        cnpj = jQuery.trim(cnpj);// retira espaços em branco
        // DEIXA APENAS OS NÚMEROS
        cnpj = cnpj.replace('/', '');
        cnpj = cnpj.replace('.', '');
        cnpj = cnpj.replace('.', '');
        cnpj = cnpj.replace('-', '');

        var numeros, digitos, soma, i, resultado, pos, tamanho, digitos_iguais;
        digitos_iguais = 1;

        if (cnpj.length < 14 && cnpj.length < 15) {
            return false;
        }
        for (i = 0; i < cnpj.length - 1; i++) {
            if (cnpj.charAt(i) != cnpj.charAt(i + 1)) {
                digitos_iguais = 0;
                break;
            }
        }

        if (!digitos_iguais) {
            tamanho = cnpj.length - 2
            numeros = cnpj.substring(0, tamanho);
            digitos = cnpj.substring(tamanho);
            soma = 0;
            pos = tamanho - 7;

            for (i = tamanho; i >= 1; i--) {
                soma += numeros.charAt(tamanho - i) * pos--;
                if (pos < 2) {
                    pos = 9;
                }
            }
            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            if (resultado != digitos.charAt(0)) {
                return false;
            }
            tamanho = tamanho + 1;
            numeros = cnpj.substring(0, tamanho);
            soma = 0;
            pos = tamanho - 7;
            for (i = tamanho; i >= 1; i--) {
                soma += numeros.charAt(tamanho - i) * pos--;
                if (pos < 2) {
                    pos = 9;
                }
            }
            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            if (resultado != digitos.charAt(1)) {
                return false;
            }
            return true;
        } else {
            return false;
        }
    }, "Informe um CNPJ válido.");
}

function redirecionaNoContextoDaPadaria(event, pagina) {
    event.preventDefault();
    window.location = pagina + '?PadariaId=' + querystring('PadariaId');
}

function querystring(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}