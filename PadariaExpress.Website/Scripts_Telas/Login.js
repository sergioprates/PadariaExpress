$(document).ready(function () {
    carregaValidacao();
});

function entrar(event) {
    event.preventDefault();
    if ($("#loginForm").valid()) {
        //setaStatusLogando();
        var model = mapearUsuarioLogin();
        chamadaAPI('API/usuario', 'POST', model, function (response) {
            window.localStorage.removeItem('usuarioProprietario');
            window.localStorage.removeItem('usuarioFuncionario');
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
            //removeStatusLogando();
            if (IsEmpty(erro.responseJSON.Message) == false) {
                mostraMensagem('AlertaErroLogin', 'lblErroLogin', erro.responseJSON.Message, 'danger');
            } else {
                mostraMensagem('AlertaErroLogin', 'lblErroLogin', erro.responseJSON, 'danger');
            }
        });
    }
}

function mapearUsuarioLogin() {
    var usuario = new Object();
    usuario.Email = $('#txtEmailForm').val();
    usuario.Senha = $('#txtSenhaForm').val();
    return usuario;
}

function carregaValidacao() {
    $("#loginForm").validate({
        rules: {
            txtEmailForm: {
                required: true,
                email: true
            },
            txtSenhaForm: {
                required: true,
                minlength: 6,
                maxlength: 30
            }
        },
        messages: {
            txtEmailForm: {
                required: 'Campo obrigatório.',
                email: 'Email inválido.'
            },
            txtSenhaForm: {
                required: 'Campo obrigatório.',
                minlength: 'Senha deve ter entre 6 e 30 caracteres',
                maxlength: 'Senha deve ter entre 6 e 30 caracteres'
            }
        }
    });
}