/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/Util.js" />


angular.module('GridApp', [])
.controller('GridController', function ($scope, $http) {

    $(document).ready(function () {
        mostraAguarde();
        var usuario = JSON.parse(window.localStorage.getItem('usuarioProprietario'));
        $scope.padarias = [];

        $http.get('API/padaria/' + usuario.UsuarioId)
            .then(function (data) {
                $scope.padarias = data.data.d;
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

    $scope.gerenciar = gerenciar;

});

function gerenciar(PadariaId) {
    window.location = 'Dashboard.html?PadariaId=' + PadariaId;
}
