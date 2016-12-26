/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/Util.js" />


angular.module('GridApp', [])
.controller('GridController', function ($scope, $http) {
    $scope.formasDePagamento = [];
    $(document).ready(function () {
        mostraAguarde();
        carregaValidacao();
        $http.get('API/funcionarios/' + querystring('PadariaId'))
        .then(function (data) {
            $scope.funcionarios = data.data.d;
            ocultaAguarde();
        }, function (erro) {
            ocultaAguarde();
            mostraErro(erro.Message);
        });
    });


    $scope.gerenciarFuncionario = function (id) {
        mostraConfirmacao('Confirma a alteração da ativação deste funcionário?', function () {
            $http.get('api/funcionario/ativacao/' + id)
           .then(function (data) {
               ocultaAguarde();
               mostraSucesso('Funcionário alterado com sucesso!', function () {
                   ocultaSucesso();
                   ocultaConfirmacao();
                   mostraAguarde();
                   $http.get('API/funcionarios/' + querystring('PadariaId'))
                        .then(function (data) {
                            $scope.funcionarios = data.data.d;
                            ocultaAguarde();
                        }, function (erro) {
                            ocultaAguarde();
                            mostraErro(erro.Message);
                        });
               });
           }, function (erro) {
               ocultaAguarde();
               mostraErro(erro.data.Message);
           });
        });

    };

});

function convidarFuncionario(event) {
    event.preventDefault();

    if ($("#cadastroForm").valid()) {
        $('#modalConvite').modal('hide');
        mostraAguarde();        
        chamadaAPI('api/funcionario/convite/?PadariaId=' + querystring('PadariaId') + '&Email=' + pegaValorID('Email'), 'GET', {}, function () {
            ocultaAguarde();
            mostraSucesso('Funcionário convidado com sucesso!', function () {
                ocultaSucesso();
            });
        }, function (erro) {
            ocultaAguarde();
            mostraErro(erro.Message);
        });
    }
}

function carregaValidacao() {

    carregaConfiguracaoValidacao();

    $("#cadastroForm").validate({
        rules: {
            Email: {
                required: true,
                email: true
            }
        },
        messages: {
            Email: {
                required: 'Campo obrigatório.',
                email: 'Email inválido.'
            }
        }
    });
}

