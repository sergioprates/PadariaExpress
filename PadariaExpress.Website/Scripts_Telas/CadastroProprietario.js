
/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/bootstrap.js" />
/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/Util.js" />
/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/jquery.validate-vsdoc.js" />
$(document).ready(function () {
    mostraAguarde();
    carregaValidacao();
    carregaMascaras();
    carregaDatepicker();
    listarSexosAtivos();
});

function gravar(event) {
    event.preventDefault();
    if ($("#cadastroForm").valid()) {
        mostraAguarde();
        var model = mapearUsuario();
        chamadaAPI('API/proprietario/atualizar', 'POST', model, function (response) {
            var usuario = response.d;
            window.localStorage.setItem('usuarioProprietario', JSON.stringify(usuario));
            window.location = 'Principal.html';
        }, function (erro) {
            ocultaAguarde();
            mostraErro(erro.Message);
        });
    }
}

function carregaUsuario(usuario) {
    $('#Nome').val(usuario.Nome);
    $('#Email').val(usuario.Email);
    usuario.DataNascimento = usuario.DataNascimento.split('-');
    //$('#DataNascimento').val(usuario.DataNascimento[2].substring(0, 2) + '/' + usuario.DataNascimento[1] + '/' + usuario.DataNascimento[0]);
    $("#DataNascimento").datepicker("update", new Date(usuario.DataNascimento[0], parseInt(usuario.DataNascimento[1]) - 1, usuario.DataNascimento[2].substring(0, 2)));
    $('#Cpf').val(usuario.Cpf);
    $('#Senha').val(usuario.Senha);
    $('#ConfirmacaoSenha').val(usuario.Senha);
    $('#Sexo').selectpicker('val', usuario.Sexo.SexoId);
}

function mapearUsuario() {
    var usuario = new Object();
    usuario.Ativo = true;
    usuario.Nome = pegaValorID('Nome');
    usuario.Login = pegaValorID('Login');
    usuario.Email = pegaValorID('Email');

    var dataNascimento = $('#DataNascimento').datepicker('getDate');
    usuario.DataNascimento = new Date(dataNascimento.getFullYear(), dataNascimento.getMonth(), dataNascimento.getDate(),0,0,0,0);
    usuario.Cpf = pegaValorID('Cpf');
    usuario.Senha = pegaValorID('Senha');
    usuario.ConfirmaSenha = pegaValorID('ConfirmacaoSenha');
    usuario.Senha = pegaValorID('Senha');
    usuario.SexoId = pegaValorID('Sexo');
    usuario.UsuarioId = JSON.parse(window.localStorage.getItem('usuarioProprietario')).UsuarioId;
    return usuario;
}

function carregaDatepicker() {
    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy',
        endDate: 'today',
        language: 'pt-BR',
        autoclose: true
    });
}

function listarSexosAtivos() {

    chamadaAPI('API/sexos', 'GET', {}, function (response) {
        var sexos = response.d;
        populaSelectSexo(sexos);
        carregaUsuario(JSON.parse(window.localStorage.getItem('usuarioProprietario')));
        ocultaAguarde();
    }, function (erro) {
        ocultaAguarde();
        mostraErro(erro.Message);
    });
}

function populaSelectSexo(sexos) {

    var select = document.getElementById('Sexo');
    select.options.length = 0; // clear out existing items
    for (var i = 0; i < sexos.length; i++) {

        var option = document.createElement("option");
        option.text = sexos[i].Nome;
        option.value = sexos[i].SexoId;
        select.add(option);
    }

    var option = document.createElement("option");
    option.text = 'Selecione...';
    option.value = '';
    select.add(option, 0);
    $(select).selectpicker('refresh');
}

function carregaValidacao() {
    carregaConfiguracaoValidacao();

    $("#cadastroForm").validate({
        ignore: ':not(select:hidden, input:visible, textarea:visible)',
        rules: {
            Nome: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            DataNascimento: {
                required: true,
                datanascimento: true
            },
            Cpf: {
                required: true,
                cpf: true
            },
            Senha: {
                minlength: 6,
                maxlength: 30,
                required: true
            },
            ConfirmacaoSenha: {
                minlength: 6,
                maxlength: 30,
                equalTo: "#Senha",
                required: true
            },
            Sexo: {
                required: true
            }
        },
        messages: {
            Nome: {
                required: 'Campo obrigatório.'
            },
            Email: {
                required: 'Campo obrigatório.',
                email: 'Email inválido.'
            },
            DataNascimento: {
                required: 'Campo obrigatório.',
                datanascimento: 'Data de nascimento inválida.'
            },
            Cpf: {
                required: 'Campo obrigatório.',
                cpf: 'CPF inválido.'
            },
            Senha: {
                required: 'Campo obrigatório.',
                minlength: 'Senha deve conter entre 6 e 30 caracteres',
                maxlength: "Senha deve conter entre 6 e 30 caracteres",
            },
            ConfirmacaoSenha: {
                required: 'Campo obrigatório.',
                minlength: 'Senha deve conter entre 6 e 30 caracteres',
                maxlength: 'Senha deve conter entre 6 e 30 caracteres',
                equalTo: 'Senhas não conferem'
            },
            Sexo: {
                required: "Selecione seu sexo."
            }
        }
    });
}