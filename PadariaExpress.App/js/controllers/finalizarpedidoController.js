/// <reference path="../scripts/lodash.js" />



app.controller('finalizarpedidoController', ['$scope', '$http', 'mostraPopUpErro', 'mostraAguarde', 'ocultaAguarde', '$stateParams', 'returnToState', 'mostraMensagemTemporaria', 'mostraPopUpSucesso', function ($scope, $http, mostraPopUpErro, mostraAguarde, ocultaAguarde, $stateParams, returnToState, mostraMensagemTemporaria, mostraPopUpSucesso) {
    $scope.Pedido;

    $scope.FormasDePagamento = [];
    $scope.Padaria;
    $scope.Dinheiro = false;
    $scope.$on('$ionicView.beforeEnter', function () {
        $scope.Padaria = JSON.parse(window.localStorage.getItem('padaria'));
        $scope.FormasDePagamento = $scope.Padaria.FormasDePagamento;
        $scope.FormasDePagamento = _.sortBy($scope.FormasDePagamento, 'Nome');
        $scope.Pedido = JSON.parse(window.localStorage.getItem('pedido'));
        $scope.Pedido.EnderecoMarcado = 'MeuEndereco';

        $scope.Pedido.ValorTotal += $scope.Pedido.Padaria.ValorFrete;
    });

    $scope.verificaSeDinheiro = function () {
        var formaDePagamento = _.find($scope.FormasDePagamento, function (item) {
            return $scope.Pedido.FormaDePagamento.FormaDePagamentoId == item.FormaDePagamentoId;
        });

        if (formaDePagamento != undefined) {
            $scope.Dinheiro = formaDePagamento.Tipo == 0;
        }
    };


    $scope.buscarCep = function () {
        if ($scope.Pedido.Endereco.CEP != '' && $scope.Pedido.Endereco.CEP != undefined) {
            mostraAguarde();
            $http.get(pegaURLAPI() + 'localizacao/' + $scope.Pedido.Endereco.CEP)
                  .success(function (data) {
                      data = data.d;
                      $scope.Pedido.Endereco.Bairro = data.bairro;
                      $scope.Pedido.Endereco.Logradouro = data.logradouro;
                      $scope.Pedido.Endereco.Estado = data.uf;
                      $scope.Pedido.Endereco.Cidade = data.localidade;
                      ocultaAguarde();
                  }).error(function (data) {
                      ocultaAguarde();
                      mostraMensagemTemporaria(data.Message);
                  });
        }
    };

    $scope.finalizarPedido = function (form) {

        if (form.$valid) {
            if ($scope.Dinheiro && $scope.Pedido.ValorTotal > $scope.Pedido.ValorPago) {
                mostraMensagemTemporaria('Valor pago não pode ser menor do que o valor total.');
                return;
            }

            mostraAguarde();
            $scope.Pedido.Padaria = new Object();
            var cliente = JSON.parse(window.localStorage.getItem('cliente'));
            var padaria = JSON.parse(window.localStorage.getItem('padaria'));
            $scope.Pedido.Cliente = cliente;
            $scope.Pedido.Padaria.PadariaId = padaria.PadariaId;

            if ($scope.Pedido.EnderecoMarcado == 'MeuEndereco') {
                $scope.Pedido.Endereco = cliente.Endereco;
            }

            for (var i = 0; i < $scope.Pedido.Itens.length; i++) {
                $scope.Pedido.Itens[i].Produto.Foto = null;
                $scope.Pedido.Itens[i].Produto.Categoria = null;
            }

            $http.post(pegaURLAPI() + 'pedido',
                         JSON.stringify($scope.Pedido),
                         {
                             headers: {
                                 'Content-Type': 'application/json'
                             }
                         }
                     ).success(function (data) {
                         $scope.Pedido = data.d;
                         window.localStorage.removeItem('pedido');
                         ocultaAguarde();
                         mostraPopUpSucesso('Sucesso!', 'Pedido realizado com sucesso!', function () {

                             window.location = '#/app/home';
                         });
                     }).error(function (data) {
                         ocultaAguarde();
                         mostraPopUpErro(data.Message);
                     });
        }
        else
        {
            mostraMensagemTemporaria('Os campos em destaque não foram preenchidos corretamente.');
        }

    };

    $scope.cancelarPedido = function () {
        window.localStorage.removeItem('pedido');
        window.location = '#/app/home';
    };
}]);