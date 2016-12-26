/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/Util.js" />


$(document).ready(function () {
    carregaCategorias();
    carregaValidacao();
    carregaUpload();
});



function carregaCategorias() {
    mostraAguarde();
    var model = {};
    chamadaAPI('API/categorias/' + querystring('PadariaId'), 'GET', model, function (response) {
        var categorias = response.d;
        populaSelectCategorias(categorias);
        buscaProduto();
    }, function (erro) {
        ocultaAguarde();
        if (IsEmpty(erro.responseJSON.Message) == false) {
            mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON.Message, 'danger');
        } else {
            mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON, 'danger');
        }
    });
}


function populaSelectCategorias(categorias) {
    var select = document.getElementById('Categoria');
    select.options.length = 0; // clear out existing items
    for (var i = 0; i < categorias.length; i++) {
        var option = document.createElement("option");
        option.text = categorias[i].Nome;
        option.value = categorias[i].CategoriaId;
        select.add(option);
    }

    var option = document.createElement("option");
    option.text = 'Selecione...';
    option.value = '';
    select.add(option, 0);
    $(select).selectpicker('refresh');
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
            url: 'Upload.ashx?DeletarFoto=1&Caminho=' + $("#FotoProduto").attr("src"),
            data: null,
            success: function (status) {
                if (status != 'error') {
                    $('#FotoProduto').hide();
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

function buscaProduto() {

    if (IsEmpty(querystring('ProdutoId')) == false) {
        var model = {};
        chamadaAPI('API/produtos/buscar/' + querystring('ProdutoId'), 'GET', model, function (response) {
            carregaProduto(response.d);
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

function carregaProduto(produto) {

    $('#Nome').val(produto.Nome);
    $('#Descricao').val(produto.Descricao);
    $('#Preco').val(produto.Preco);
    $('#Categoria').selectpicker('val', produto.Categoria.CategoriaId);

    if (IsEmpty(produto.Foto) == false) {
        $('#FotoProduto').attr('src', 'Foto.ashx?ProdutoId=' + querystring('ProdutoId'));
        $('#FotoProduto').show();
    }
    else {
        $('#FotoProduto').hide();
    }

    document.getElementById('chkAtivo').checked = produto.Ativo;

}

function gravar(event) {
    event.preventDefault();
    if ($('#cadastroForm').valid()) {
        mostraAguarde();
        var model = mapearProduto();
        var url = 'API/produtos';

        if (IsEmpty(model.ProdutoId) == false) {
            url += '/atualizar/';
        }

        chamadaAPI(url, 'POST', model, function (response) {
            window.location = 'Produtos.html?alert=1&PadariaId=' + querystring('PadariaId');
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

function mapearProduto() {
    var produto = new Object();

    produto.Ativo = document.getElementById('chkAtivo').checked;
    produto.Nome = pegaValorID('Nome');
    produto.Descricao = pegaValorID('Descricao');
    produto.Preco = pegaValorID('Preco');
    produto.Padaria = new Object();
    produto.Padaria.PadariaId = querystring('PadariaId');
    produto.Categoria = new Object();
    produto.Categoria.CategoriaId = pegaValorID('Categoria');
    produto.Categoria.Padaria = new Object();
    produto.Categoria.Padaria.PadariaId = querystring('PadariaId');
    produto.Caminho = $('#FotoProduto').attr('src');
    if (IsEmpty(querystring('ProdutoId')) == false) {
        produto.ProdutoId = querystring('ProdutoId');
    }

    return produto;
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
            },
            Preco: {
                required: true
            },
            Categoria: {
                required: true
            }
        },
        messages: {
            Nome: {
                required: 'Campo obrigatório.'
            },
            Descricao: {
                required: 'Campo obrigatório.'
            },
            Preco: {
                required: 'Campo obrigatório.'
            },
            Categoria: {
                required: 'Campo obrigatório.'
            }
        }
    });

}

function sendFile(file) {

    var formData = new FormData();
    formData.append('file', $('#Foto')[0].files[0]);
    $.ajax({
        type: 'post',
        url: 'Upload.ashx?FotoProduto=1',
        data: formData,
        success: function (status) {
            if (status != 'error') {
                var my_path = "FotosProdutos/" + status;
                $("#FotoProduto").attr("src", my_path);
                $('#FotoProduto').show();
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
