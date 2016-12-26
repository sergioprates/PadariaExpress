/// <reference path="../scripts/lodash.js" />



app.controller('produtoController', ['$scope', '$http', 'mostraPopUpErro', 'mostraAguarde', 'ocultaAguarde', '$stateParams', 'returnToState', 'mostraMensagemTemporaria', function ($scope, $http, mostraPopUpErro, mostraAguarde, ocultaAguarde, $stateParams, returnToState, mostraMensagemTemporaria) {
    $scope.Padaria = JSON.parse(window.localStorage.getItem('padaria'));
    $scope.Item = new Object();
    $scope.$on('$ionicView.beforeEnter', function () {

        window.localStorage.setItem("voltou", true);
        mostraAguarde();
        var produtos = JSON.parse(window.localStorage.getItem('produtos'));
        $scope.Produto = _.find(produtos, function (produto) {
            return produto.ProdutoId == $stateParams.ProdutoId;
        });


        var item = buscarItemProdutoNoPedido();
        if (_.isEmpty(item) == false) {
            $scope.Item.Quantidade = item.Quantidade;
        }

        ocultaAguarde();
    });

    function buscarItemProdutoNoPedido() {
        var pedido;
        if (window.localStorage.getItem('pedido') == null) {
            return null;
        }
        else {
            pedido = JSON.parse(window.localStorage.getItem('pedido'));
        }

        var item = _.find(pedido.Itens, function (item) {
            return item.Produto.ProdutoId == $scope.Produto.ProdutoId;
        });

        return item;
    }

    $scope.adicionarProduto = function () {
        if (window.localStorage.getItem('cliente') == null) {
            mostraMensagemTemporaria('Antes de fazer um pedido é necessário que você acesse a aba perfil e preencha seus dados');
            return;
        }
        if ($scope.Item.Quantidade != undefined) {
            var pedido;
            if (window.localStorage.getItem('pedido') == null) {
                pedido = criaPedido();
            }
            else {
                pedido = JSON.parse(window.localStorage.getItem('pedido'));
            }

            $scope.Item.Produto = $scope.Produto;
            $scope.Item.PrecoUnitario = $scope.Produto.Preco;

            $scope.Item.Quantidade = parseFloat($scope.Item.Quantidade.toString().replace(',', '.'));

            var existeItem = _.find(pedido.Itens, function (item) {
                return item.Produto.ProdutoId == $scope.Produto.ProdutoId;
            });

            if (existeItem) {
                pedido.Itens = _.map(pedido.Itens, function (item) {
                    if (item.Produto.ProdutoId == $scope.Produto.ProdutoId) {
                        return $scope.Item;
                    }
                    else {
                        return item;
                    }
                });
            }
            else {
                pedido.Itens.push($scope.Item);
            }

            pedido.ValorTotal = _.sum(pedido.Itens, function (item) {
                return item.PrecoUnitario * item.Quantidade;
            });

            window.localStorage.setItem('pedido', JSON.stringify(pedido));

            if (existeItem) {

                returnToState('app.pedido');
            }
            else {
                returnToState('app.produtos_PadariaId=' + $scope.Padaria.PadariaId);
            }

        }
        else {
            mostraMensagemTemporaria('Preencha a quantidade desejada deste produto.');
        }
    };


    $scope.removerProduto = function () {
        var pedido = JSON.parse(window.localStorage.getItem('pedido'));
        _.remove(pedido.Itens, function (item) {
            return item.Produto.ProdutoId == $scope.Produto.ProdutoId;
        });

        pedido.ValorTotal = _.sum(pedido.Itens, function (item) {
            return item.PrecoUnitario * item.Quantidade;
        });
        window.localStorage.setItem('pedido', JSON.stringify(pedido));

        returnToState('app.produtos_PadariaId=' + $scope.Padaria.PadariaId);
    };

    function criaPedido() {
        var pedido = new Object();
        pedido.Padaria = $scope.Padaria;
        pedido.Cliente = new Object();
        pedido.Itens = new Array();
        return pedido;
    }

    $scope.estaNoPedido = function () {
        var pedido;
        if (window.localStorage.getItem('pedido') == null) {
            return false;
        }
        else {
            pedido = JSON.parse(window.localStorage.getItem('pedido'));
        }

        var existeItem = _.find(pedido.Itens, function (item) {
            return item.Produto.ProdutoId == $scope.Produto.ProdutoId;
        });
        return existeItem != undefined;
    };

    $scope.FotoPadrao = new Object();
    $scope.FotoPadrao.Valor = "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAECklEQVR4Xu2ajZHTMBCFuQqACggVABVgKiBUQKiAowJMBUAF5CoAKiBUAFSA6QAqgP1AO/Nm8V8Ox3G43RlNbGsl6z3tn3x3dq1fVtb91Fpl7W5Rbez3nbXX1rg+aTnrWf1z66sH0NH/4pQZ6CLgjYHaCLAvZddvFGu4I31bu35yqiS0EbA2MG8LoG+FiF0AWNn9K2tOxLNyf3I8tBHw1VCsrP2wht83Haiwhs/Wbln7bu12+T0pEiIBuvuYNebdJxvrxF2QMfqLIycSUNsKCX7IzYEdxQI+WPPssLNr2hTyccK5etcTCcCvSXtIX4aI4KcAHecY2oBJ3tlnAV0EKHiyA6JZYZKF2SQP5rCCfQmI4CtbJEXRfWuYLff/IrUNdhdcHAFt4In+u6tCAAGPHcbs+QU8cmUIIOcjCp4MADFYx3/vAtG3FTx9XQSgR7Gkh6mm6Mc5a3uw2Bigi43g2wggnW4EeASLCxFAOUxBCHISBCj4C1v0yppmgcruqQ55PkYgAuAcrxdPQATPDu+EgG0B78A5TFFcET/QQ5iDxliIc2Es+ot1gTbwLN4JYCcJhi5jToiVKQOcGIHoHIurA/yUiNmzey5OgN9zigSYZw1Rbb2ENOaI1eTiCMCUkfMAIxJwmVPhqhB2XeaejYB1YZ8gBDj3wb7DkB+YGKMEdKVCdvmxNXycyN8mvPuldDwq63q/hzUNWdtf/YD8WZ5isrxsiABd6Nb0+RDiwYxFtwHUT2x9FkIMcCvweNCUd+wNbswAJQB9DUJtFoC1+Ocyn39oTC2k+ph7HbsKoVhKlD5rHIOzUycSoIrxpZoFCHSI+myb+W9Mx78YMUZ3FxLYXZU2sug/OgH48Cdrq7JazBiX4ZlLJCASVpkiz5wQxhPosCAXAq3HFyVmFgLwfxbpu6RmClAWj1C61uV6I4Aau2aMuwRp0+sCjQ1be+5mTrygD2F+JxRrYR6vD2YhwGty3yEHRGQGKBJrAJ7pru0KIP1W2FYQoeeBk0zCHIB3wrCMWnRmI4CX0jwTaICL3wEKJ79/2MmH5QHErXoIowugkODFj47xLKEkzUoAC9xa02iMSQJK/VUJiIDogzB3G9X1a/oAqYFULeyoBCigsaUtBBHYAETBA8AuwpyEyi5wFyQG0aMSwIIg4bzsEosZI4BeW8MlIGOMoM844oASdnQCxiz+kDpJgLHrmWL2IHjInR07d1pAWkC6QMaADIIlYmYWGJs69tXTDyJ6zN13nkPoZxrMNJhpMNNgpsFMg38YyDrgEHnWmfU/jWUdYIzUh2L6EvNmIZSFUBZCWQhlIZSFUBZCWQlmKZxngTwM5WlwluNwY75GW4rwPwP+P0OzELAU4HEd/IeKEzH5Gn8BJ7xcUC7ipoUAAAAASUVORK5CYII=";
}]);