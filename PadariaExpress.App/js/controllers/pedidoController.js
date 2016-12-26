/// <reference path="../scripts/lodash.js" />



app.controller('pedidoController', ['$scope', '$http', 'mostraPopUpErro', 'mostraAguarde', 'ocultaAguarde', '$stateParams', 'returnToState', 'mostraMensagemTemporaria', function ($scope, $http, mostraPopUpErro, mostraAguarde, ocultaAguarde, $stateParams, returnToState, mostraMensagemTemporaria) {
    $scope.Pedido = new Object();
    $scope.Pedido.ValorTotal = 0;
    
    $scope.$on('$ionicView.beforeEnter', function () {

        $scope.gerenciaExibicao = function ($event) {
            if ($stateParams.PedidoId != undefined) {
                $event.preventDefault();
            }
        };

        $scope.pedidoCarregado = false;
        if ($stateParams.PedidoId != undefined) {
            mostraAguarde();
            $scope.pedidoCarregado = true;
            $http.get(pegaURLAPI() + 'pedidos/buscar/' + $stateParams.PedidoId)
         .success(function (data, status, headers, config) {
             $scope.Pedido = data.d;
             ocultaAguarde();
         }).error(function (data, status, headers, config) {
             ocultaAguarde();
             try {
                 mostraPopUpErro(data.Message);
             }
             catch (e) {
                 mostraPopUpErro('Ocorreu um erro ao buscar os produtos... Por favor tente novamente.');
             }
         });

        }
        else {
            $scope.Pedido = JSON.parse(window.localStorage.getItem('pedido'));
            $scope.Pedido.ValorTotal += $scope.Pedido.Padaria.ValorFrete;
        }
    });

    $scope.cancelarPedido = function () {
        window.localStorage.removeItem('pedido');
        window.location = '#/app/home';
    };

    $scope.finalizarPedido = function () {
        var pedido;
        pedido = JSON.parse(window.localStorage.getItem('pedido'));

        if (pedido.Itens.length == 0) {
            mostraMensagemTemporaria('Para realizar um pedido é necessário que o mesmo possua no mínimo um produto.');
            return;
        }

        window.location = '#/app/finalizarpedido';
    };

    $scope.FotoPadrao = new Object();
    $scope.FotoPadrao.Valor = "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAECklEQVR4Xu2ajZHTMBCFuQqACggVABVgKiBUQKiAowJMBUAF5CoAKiBUAFSA6QAqgP1AO/Nm8V8Ox3G43RlNbGsl6z3tn3x3dq1fVtb91Fpl7W5Rbez3nbXX1rg+aTnrWf1z66sH0NH/4pQZ6CLgjYHaCLAvZddvFGu4I31bu35yqiS0EbA2MG8LoG+FiF0AWNn9K2tOxLNyf3I8tBHw1VCsrP2wht83Haiwhs/Wbln7bu12+T0pEiIBuvuYNebdJxvrxF2QMfqLIycSUNsKCX7IzYEdxQI+WPPssLNr2hTyccK5etcTCcCvSXtIX4aI4KcAHecY2oBJ3tlnAV0EKHiyA6JZYZKF2SQP5rCCfQmI4CtbJEXRfWuYLff/IrUNdhdcHAFt4In+u6tCAAGPHcbs+QU8cmUIIOcjCp4MADFYx3/vAtG3FTx9XQSgR7Gkh6mm6Mc5a3uw2Bigi43g2wggnW4EeASLCxFAOUxBCHISBCj4C1v0yppmgcruqQ55PkYgAuAcrxdPQATPDu+EgG0B78A5TFFcET/QQ5iDxliIc2Es+ot1gTbwLN4JYCcJhi5jToiVKQOcGIHoHIurA/yUiNmzey5OgN9zigSYZw1Rbb2ENOaI1eTiCMCUkfMAIxJwmVPhqhB2XeaejYB1YZ8gBDj3wb7DkB+YGKMEdKVCdvmxNXycyN8mvPuldDwq63q/hzUNWdtf/YD8WZ5isrxsiABd6Nb0+RDiwYxFtwHUT2x9FkIMcCvweNCUd+wNbswAJQB9DUJtFoC1+Ocyn39oTC2k+ph7HbsKoVhKlD5rHIOzUycSoIrxpZoFCHSI+myb+W9Mx78YMUZ3FxLYXZU2sug/OgH48Cdrq7JazBiX4ZlLJCASVpkiz5wQxhPosCAXAq3HFyVmFgLwfxbpu6RmClAWj1C61uV6I4Aau2aMuwRp0+sCjQ1be+5mTrygD2F+JxRrYR6vD2YhwGty3yEHRGQGKBJrAJ7pru0KIP1W2FYQoeeBk0zCHIB3wrCMWnRmI4CX0jwTaICL3wEKJ79/2MmH5QHErXoIowugkODFj47xLKEkzUoAC9xa02iMSQJK/VUJiIDogzB3G9X1a/oAqYFULeyoBCigsaUtBBHYAETBA8AuwpyEyi5wFyQG0aMSwIIg4bzsEosZI4BeW8MlIGOMoM844oASdnQCxiz+kDpJgLHrmWL2IHjInR07d1pAWkC6QMaADIIlYmYWGJs69tXTDyJ6zN13nkPoZxrMNJhpMNNgpsFMg38YyDrgEHnWmfU/jWUdYIzUh2L6EvNmIZSFUBZCWQhlIZSFUBZCWQlmKZxngTwM5WlwluNwY75GW4rwPwP+P0OzELAU4HEd/IeKEzH5Gn8BJ7xcUC7ipoUAAAAASUVORK5CYII=";
}]);