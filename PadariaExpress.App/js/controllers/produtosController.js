/// <reference path="../scripts/lodash.js" />



app.controller('produtosController', ['$scope', '$http', 'mostraPopUpErro', 'mostraAguarde', 'ocultaAguarde', '$stateParams', 'buscarPedidoIncompleto', function ($scope, $http, mostraPopUpErro, mostraAguarde, ocultaAguarde, $stateParams, buscarPedidoIncompleto) {
    $scope.Produtos = new Array();
    $scope.Produto = new Object();
   
    $scope.$on('$ionicView.beforeEnter', function () {
        mostraAguarde();
        $scope.PedidoIncompleto = buscarPedidoIncompleto() != null;
        if (window.localStorage.getItem('voltou') == false || window.localStorage.getItem('voltou') == null) {
            $http.get(pegaURLAPI() + 'produtos/ativos/' + $stateParams.PadariaId)
                 .success(function (data, status, headers, config) {
                     $scope.Produtos = data.d;
                     window.localStorage.setItem('produtos', JSON.stringify($scope.Produtos))
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
    });


    $scope.listarProdutosPorNome = function () {
        if (_.isEmpty($scope.Produto.Nome) == false) {
            $scope.Produtos = JSON.parse(window.localStorage.getItem('produtos'));
            $scope.Produtos = _.filter($scope.Produtos, function (produto) {
                return produto.Nome.toLowerCase().indexOf($scope.Produto.Nome.toLowerCase()) != -1;
            });
        }
        else {
            mostraPopUpErro('Preencha o nome do produto.');
        }
    };

    $scope.cancelar = function () {
        $scope.Produtos = JSON.parse(window.localStorage.getItem('produtos'));
    };

    $scope.FotoPadrao = new Object();
    $scope.FotoPadrao.Valor = "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAECklEQVR4Xu2ajZHTMBCFuQqACggVABVgKiBUQKiAowJMBUAF5CoAKiBUAFSA6QAqgP1AO/Nm8V8Ox3G43RlNbGsl6z3tn3x3dq1fVtb91Fpl7W5Rbez3nbXX1rg+aTnrWf1z66sH0NH/4pQZ6CLgjYHaCLAvZddvFGu4I31bu35yqiS0EbA2MG8LoG+FiF0AWNn9K2tOxLNyf3I8tBHw1VCsrP2wht83Haiwhs/Wbln7bu12+T0pEiIBuvuYNebdJxvrxF2QMfqLIycSUNsKCX7IzYEdxQI+WPPssLNr2hTyccK5etcTCcCvSXtIX4aI4KcAHecY2oBJ3tlnAV0EKHiyA6JZYZKF2SQP5rCCfQmI4CtbJEXRfWuYLff/IrUNdhdcHAFt4In+u6tCAAGPHcbs+QU8cmUIIOcjCp4MADFYx3/vAtG3FTx9XQSgR7Gkh6mm6Mc5a3uw2Bigi43g2wggnW4EeASLCxFAOUxBCHISBCj4C1v0yppmgcruqQ55PkYgAuAcrxdPQATPDu+EgG0B78A5TFFcET/QQ5iDxliIc2Es+ot1gTbwLN4JYCcJhi5jToiVKQOcGIHoHIurA/yUiNmzey5OgN9zigSYZw1Rbb2ENOaI1eTiCMCUkfMAIxJwmVPhqhB2XeaejYB1YZ8gBDj3wb7DkB+YGKMEdKVCdvmxNXycyN8mvPuldDwq63q/hzUNWdtf/YD8WZ5isrxsiABd6Nb0+RDiwYxFtwHUT2x9FkIMcCvweNCUd+wNbswAJQB9DUJtFoC1+Ocyn39oTC2k+ph7HbsKoVhKlD5rHIOzUycSoIrxpZoFCHSI+myb+W9Mx78YMUZ3FxLYXZU2sug/OgH48Cdrq7JazBiX4ZlLJCASVpkiz5wQxhPosCAXAq3HFyVmFgLwfxbpu6RmClAWj1C61uV6I4Aau2aMuwRp0+sCjQ1be+5mTrygD2F+JxRrYR6vD2YhwGty3yEHRGQGKBJrAJ7pru0KIP1W2FYQoeeBk0zCHIB3wrCMWnRmI4CX0jwTaICL3wEKJ79/2MmH5QHErXoIowugkODFj47xLKEkzUoAC9xa02iMSQJK/VUJiIDogzB3G9X1a/oAqYFULeyoBCigsaUtBBHYAETBA8AuwpyEyi5wFyQG0aMSwIIg4bzsEosZI4BeW8MlIGOMoM844oASdnQCxiz+kDpJgLHrmWL2IHjInR07d1pAWkC6QMaADIIlYmYWGJs69tXTDyJ6zN13nkPoZxrMNJhpMNNgpsFMg38YyDrgEHnWmfU/jWUdYIzUh2L6EvNmIZSFUBZCWQhlIZSFUBZCWQlmKZxngTwM5WlwluNwY75GW4rwPwP+P0OzELAU4HEd/IeKEzH5Gn8BJ7xcUC7ipoUAAAAASUVORK5CYII=";

}]);
