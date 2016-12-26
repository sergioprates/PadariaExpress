/// <reference path="Scripts/Util.js" />
/// <reference path="E:\Desenvolvimento\Produtos\BakeryExpress\BakeryExpressWebsite\Scripts/Util.js" />


$(document).ready(function () {
    aplicarValidacao();
});

function gravar() {
    var model = mapearModel();
    callJSONAPI('api/user', 'POST', model, function (data) {
        mostraMensagem(data.msg, 'success');
    }, function (response) {
        mostraMensagem(response.responseJSON, 'danger');
    });   
}


function mapearModel() {
    var model = new Object();
    model = new Object();
    model.Name = pegaValorID('txtNome');
    model.Username = pegaValorID('txtLogin');
    model.Password = pegaValorID('txtSenha');
    model.ConfirmPassword = pegaValorID('txtConfirmacaoSenha');
    model.Email = pegaValorID('txtEmail');
    model.GenderID = pegaValorID('ddlSexo');
    model.Active = document.getElementById('chkAtivo').checked;
    return model;
}

function aplicarValidacao() {
    $('#frmPrincipal').bootstrapValidator({
        framework: 'bootstrap',
        fields: {
            txtNome: {
                validators: {
                    notEmpty: {
                        message: 'Preencha o nome'
                    }
                }
            },
            txtLogin: {
                validators: {
                    notEmpty: {
                        message: 'Preencha o login'
                    },
                    stringLength: {
                        min: 6,
                        max: 30,
                        message: 'O login deve ter entre 6 e 30 caracteres'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9_\.]+$/,
                        message: 'O login deve conter apenas letras, numeros, pontos e underline'
                    }
                }
            },
            txtSenha: {
                validators: {
                    notEmpty: {
                        message: 'Preencha a senha'
                    },
                    stringLength: {
                        min: 6,
                        max: 30,
                        message: 'A senha deve conter entre 6 e 30 caracteres'
                    }
                },
                identical: {
                    field: 'txtConfirmacaoSenha',
                    message: 'A senha e a confirmação de senha devem ser iguais'
                },
                different: {
                    field: 'txtLogin',
                    message: 'A senha não pode ser a mesma que o login'
                }
            },
            txtConfirmacaoSenha: {
                validators: {
                    notEmpty: {
                        message: 'Preencha a confirmação de senha'
                    },
                    stringLength: {
                        min: 6,
                        max: 30,
                        message: 'A senha deve conter entre 6 e 30 caracteres'
                    }
                },
                identical: {
                    field: 'txtConfirmacaoSenha',
                    message: 'A senha e a confirmação de senha devem ser iguais'
                },
                different: {
                    field: 'txtLogin',
                    message: 'A senha não pode ser a mesma que o login'
                }
            },
            txtEmail: {
                validators: {
                    emailAddress: {
                        message: 'Preencha com um e-mail válido'
                    },
                    notEmpty: {
                        message: 'Preencha o e-mail'
                    }
                }
            }
        }
    });
}