/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/Util.js" />


angular.module('GridApp', [])
.controller('GridController', function ($scope, $http) {
    $scope.produtos = [];
    $(document).ready(function () {
        mostraAguarde();
        $http.get('API/produtos/' + querystring('PadariaId'))
        .then(function (data) {
            $scope.produtos = data.data.d;
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


    $scope.gerenciarProduto = gerenciarProduto;

});

function gerenciarProduto(id) {
    window.location = 'CadastroProduto.html?PadariaId=' + querystring('PadariaId') + '&ProdutoId=' + id;
}

