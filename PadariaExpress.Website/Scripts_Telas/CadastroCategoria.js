/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/Util.js" />

$(document).ready(function () {
    carregaValidacao();
    buscaCategoria();
});

function buscaCategoria() {

    if (IsEmpty(querystring('CategoriaId')) == false) {
        var model = {};
        mostraAguarde();
        chamadaAPI('API/categorias/buscar/' + querystring('CategoriaId'), 'GET', model, function (response) {
            carregaCategoria(response.d);
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

function carregaCategoria(categoria) {

    $('#Nome').val(categoria.Nome);
    $('#Descricao').val(categoria.Descricao);
    document.getElementById('chkAtivo').checked = categoria.Ativo;

}

function gravar(event) {
    event.preventDefault();
    if ($('#cadastroForm').valid()) {
        var model = mapearCategoria();
        var url = 'API/categorias';

        if (IsEmpty(model.CategoriaId) == false) {
            url += '/atualizar/';
        }

        chamadaAPI(url, 'POST', model, function (response) {
            window.location = 'Categorias.html?alert=1&PadariaId=' + querystring('PadariaId');
        }, function (erro) {
            if (IsEmpty(erro.responseJSON.Message) == false) {
                mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON.Message, 'danger');
            } else {
                mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON, 'danger');
            }
        });
    }
}

function mapearCategoria() {
    var categoria = new Object();

    categoria.Ativo = document.getElementById('chkAtivo').checked;
    categoria.Nome = pegaValorID('Nome');
    categoria.Descricao = pegaValorID('Descricao');
    categoria.Padaria = new Object();
    categoria.Padaria.PadariaId = querystring('PadariaId');
    if (IsEmpty(querystring('CategoriaId')) == false) {
        categoria.CategoriaId = querystring('CategoriaId');
    }

    return categoria;
}

function carregaValidacao() {

    carregaConfiguracaoValidacao();
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