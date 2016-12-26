/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/Util.js" />


angular.module('GridApp', [])
.controller('GridController', function ($scope, $http) {
    $scope.formasDePagamento = [];
    $(document).ready(function () {
        mostraAguarde();
        $http.get('API/formasdepagamento/' + querystring('PadariaId'))
        .then(function (data) {
            $scope.formasDePagamento = data.data.d;
            ocultaAguarde();
        }, function (erro) {
                ocultaAguarde();
                if (IsEmpty(erro.responseJSON.Message) == false) {
                    mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON.Message, 'danger');
                } else {
                    mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON, 'danger');
                }
        });
    });
    

    $scope.gerenciarFormaDePagamento = gerenciarFormaDePagamento;
    
});

function gerenciarFormaDePagamento(id) {
    window.location = 'CadastroFormaDePagamento.html?PadariaId=' + querystring('PadariaId') + '&FormaDePagamentoId=' + id;
}

