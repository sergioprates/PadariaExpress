$(document).ready(function () {
    listarSexosAtivos();
    carregaValidacao();
    carregaMascaras();
    carregaDatepicker();
});

function listarSexosAtivos() {

    chamadaAPI('API/sexos', 'GET', {}, function (response) {
        var sexos = response.d;
        populaSelectSexo(sexos);
    }, function (erro) {
    });
}


function cadastrar(event) {
    event.preventDefault();
    if ($("#cadastroForm").valid()) {
        //setaStatusCadastrando();
        var model = mapearUsuario();
        var url = '';

        if (IsEmpty(querystring('f')) == false) {
            url = 'API/funcionario';
        }
        else {
            url = 'API/proprietario';
        }

        chamadaAPI(url, 'POST', model, function (response) {
            var usuario = response.d;
            if (usuario.TipoUsuario == 1) {
                window.localStorage.setItem('usuarioProprietario', JSON.stringify(usuario));
                window.location = 'Principal.html';
            }
            else {
                window.localStorage.setItem('usuarioFuncionario', JSON.stringify(usuario));
                window.location = 'Dashboard.html?PadariaId=' + usuario.Padarias[0].PadariaId;
            }
        }, function (erro) {
            //removeStatusCadastrando();
            if (IsEmpty(erro.responseJSON.Message) == false) {
                mostraMensagem('AlertaErroCadastro', 'lblErroCadastro', erro.responseJSON.Message, 'danger');
            } else {
                mostraMensagem('AlertaErroCadastro', 'lblErroCadastro', erro.responseJSON, 'danger');
            }
        });
    }

}

function mapearUsuario() {
    var usuario = new Object();
    usuario.Ativo = true;
    usuario.Nome = pegaValorID('Nome');
    usuario.Login = pegaValorID('Login');
    usuario.Email = pegaValorID('Email');

    var dataNascimento = $('#DataNascimento').datepicker('getDate');
    usuario.DataNascimento = new Date(dataNascimento.getFullYear(), dataNascimento.getMonth(), dataNascimento.getDate());
    usuario.Cpf = pegaValorID('Cpf');
    usuario.Senha = pegaValorID('Senha');
    usuario.ConfirmaSenha = pegaValorID('ConfirmacaoSenha');
    usuario.Senha = pegaValorID('Senha');
    usuario.SexoId = pegaValorID('Sexo');
    usuario.Hash = querystring('f');
    return usuario;
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

function carregaDatepicker() {
    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy',
        endDate: 'today',
        language: 'pt-BR',
        autoclose: true
    });
}