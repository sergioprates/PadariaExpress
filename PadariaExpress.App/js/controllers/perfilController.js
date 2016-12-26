/// <reference path="../scripts/lodash.js" />


app.controller('perfilController', ['$scope', '$http', 'mostraPopUpErro', 'mostraAguarde', 'ocultaAguarde', '$stateParams', 'mostraMensagemTemporaria', function ($scope, $http, mostraPopUpErro, mostraAguarde, ocultaAguarde, $stateParams, mostraMensagemTemporaria) {
    $scope.Cliente;



    $scope.$on('$ionicView.beforeEnter', function () {
        window.localStorage.removeItem('pedido');
        if (window.localStorage.getItem('cliente') != null && window.localStorage.getItem('cliente') != 'null') {
            $scope.Cliente = JSON.parse(window.localStorage.getItem('cliente'));
        }
        else {
            $scope.Cliente = new Object();
            $scope.Cliente.Ativo = true;
            $scope.Cliente.Endereco = new Object();
            $scope.Cliente.EnderecosUsuario = new Array();
        }
    });

    $scope.atualizarDados = function (form) {
        //event.preventDefault();

        if (form.$valid) {
            var url = 'cliente';
            mostraAguarde();
            $scope.Cliente.Endereco.Ativo = true;
            $scope.Cliente.EnderecosUsuario = new Array();
            $scope.Cliente.EnderecosUsuario.push($scope.Cliente.Endereco);

            $scope.Cliente.RegistroAndroid = window.localStorage.getItem('pushID');
            if ($scope.Cliente.UsuarioId != undefined) {
                url += '/atualizar';
            }
            else {
                //tentar autenticar
                url = 'cliente/autenticar';
            }


            $http.post(
                         pegaURLAPI() + url,
                         JSON.stringify($scope.Cliente),
                         {
                             headers: {
                                 'Content-Type': 'application/json'
                             }
                         }
                     ).success(function (data) {
                         $scope.Cliente.UsuarioId = data.d.UsuarioId;

                         $http.post(
                         pegaURLAPI() + 'cliente/atualizar',
                         JSON.stringify($scope.Cliente),
                         {
                             headers: {
                                 'Content-Type': 'application/json'
                             }
                         }
                         ).success(function (data) {
                             $scope.Cliente = data.d;
                             $scope.Cliente.Endereco = $scope.Cliente.EnderecosUsuario[0];
                             window.localStorage.setItem('cliente', JSON.stringify($scope.Cliente));
                             ocultaAguarde();
                             mostraMensagemTemporaria('Dados atualizados com sucesso!');
                         }).error(function (data) {
                             ocultaAguarde();
                             mostraPopUpErro(data.Message);
                         });
                     }).error(function (data) {
                         $http.post(
                            pegaURLAPI() + 'cliente',
                            JSON.stringify($scope.Cliente),
                            {
                                headers: {
                                    'Content-Type': 'application/json'
                                }
                            }
                            ).success(function (data) {
                                $scope.Cliente = data.d;
                                $scope.Cliente.Endereco = $scope.Cliente.EnderecosUsuario[0];
                                window.localStorage.setItem('cliente', JSON.stringify($scope.Cliente));
                                ocultaAguarde();
                                mostraMensagemTemporaria('Dados atualizados com sucesso!');
                            }).error(function (data) {
                                ocultaAguarde();
                                mostraPopUpErro(data.Message);
                            });
                     });
        }
        else {
            mostraMensagemTemporaria('Os campos em destaque não foram preenchidos corretamente.');
        }

    };

    $scope.buscarCep = function () {
        if ($scope.Cliente.Endereco.CEP != '' && $scope.Cliente.Endereco.CEP != undefined) {
            mostraAguarde();
            $http.get(pegaURLAPI() + 'localizacao/' + $scope.Cliente.Endereco.CEP)
                  .success(function (data) {
                      data = data.d;
                      $scope.Cliente.Endereco.Bairro = data.bairro;
                      $scope.Cliente.Endereco.Logradouro = data.logradouro;
                      $scope.Cliente.Endereco.Estado = data.uf;
                      $scope.Cliente.Endereco.Cidade = data.localidade;
                      ocultaAguarde();
                  }).error(function (data) {
                      ocultaAguarde();
                      //mostraPopUpErro(data.Message);
                  });
        }
    };
}]);