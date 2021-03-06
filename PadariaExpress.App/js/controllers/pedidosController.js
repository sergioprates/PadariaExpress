﻿app.controller('pedidosController', ['$scope', '$http', 'mostraPopUpErro', 'mostraAguarde', 'ocultaAguarde', 'mostraMensagemTemporaria', '$state', function ($scope, $http, mostraPopUpErro, mostraAguarde, ocultaAguarde, mostraMensagemTemporaria, $state) {
    $scope.Pedidos = new Array();

    $scope.$on('$ionicView.beforeEnter', function () {
        if (window.localStorage.getItem('cliente') != null && window.localStorage.getItem('cliente') != 'null') {
            $scope.Cliente = JSON.parse(window.localStorage.getItem('cliente'));
            mostraAguarde();
            $http.get(pegaURLAPI() + 'pedidoscliente/' + $scope.Cliente.UsuarioId)
            .success(function (data, status, headers, config) {
                $scope.Pedidos = data.d;
                ocultaAguarde();
            }).error(function (data, status, headers, config) {
                ocultaAguarde();
                try {
                    mostraPopUpErro(data.Message);
                }
                catch (e) {
                    mostraPopUpErro('Ocorreu um erro ao buscar os pedidos... Por favor tente novamente.');
                }
            });
        }
        
    });

    $scope.classeStatus = function (status) {
            switch (status) {
                case 0:
                    return '';
                case 1:
                    return 'energized';
                case 2:
                    return 'positive';
                case 3:
                    return 'calm';
                case 4:
                    return 'balanced';
                case 5:
                    return 'assertive';
                default:

            }
    };

    $scope.buscarStatusPedido = buscarStatusPedido;

    $scope.FotoPadrao = new Object();
    $scope.FotoPadrao.Valor = "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAEtklEQVR4Xu2bjXHTQBCFSQVABYgKgAowFQAVICoAKkCpAKgApwJCBSgVABXgVABUAPtlbj3r9Z3+fLak2DdzE0s6Wfvevt3bOytnd9Ltnlz6IP2FdD7PtV2K4a+l/4kBOGtA9SWAnytwa/cPOXjSh4BCBv+6DcgNhpfyGTVstJQC3gb53yYOPgkYcHUi4LuMeuzG/pTjKhVLE2OqFHteOZtWcvywCwEkvN8R8IuZgFfTlxESyAPkg3WLhQDsfXYEnAfvT8zRjeagYJRs2zs5+NhGAInieduNM2AipuSt2cArIHYTWGvpz2YA2ppYycH7iM3kgZWe9wTE5K9jUcZSerSgmBg5FG9bGT/YSFEEjpvmCeCCz54Tw7azOV/lGyAoSgDZf85lb1d21o63CoAVyt9jaOuq0BJwDPJX517Ih9LngGORP7hJ5Pc9Af+OQfsG4436bQicCDgp4LgYOIXAKQfMOAlSztbSWd1RubLEfdAzgicXAtcdQFDAVNJXDiznYiu/Jk4mRYCu0Jr2IjdWcQ4Zang6VwVsrM4ExJANmSFV7CQU8FcAs3VlJV3Isd2SJzQ4l2qxra8uYpgEAetFibN4Kce6L9EkfW6zY7sA1zGTICD6Y4VYaJfmLFpSu1BDvQ8JkyBgY3/OuE+BNcmfMd+kD93AGYUAJL+QrnO235JTDnRz9iqM99ImPJj7h4IfRQG6J4/RFDCQsPVDRUAKSXgX6euPmo8CGYRH0SfYE2MPqgDvSQW49UNFMLapHsiA/eYrDkrAM3lg7SznGBWQB3xjGlQv85skSuC4b7nbRNbBCAAACcs3zfR+msP79Eo6RZGdAQifMly724Suw7WDEXAeDI7ZxC4UAFGBehliSHBNDSIg6E0HoKkhByOgqZBZiXXImqRImNBSc34MyC65Ym8E4HGars5SxQ5jbKwrCX0IsAVTXzHshQArdyQKCW0hYA2HBBTD37a2kAH8kDO0FshOQKxqAwhxHntBKeU9FEAO4JWWmBqKQGzZxlDL9ewExGJdQcamQQodvMgsAVGrYDDnWNsDvnZqYDbhO3O0rASwrE1JESC8qaFJDuMBoVMdIH0r5EQpve8uTx9ishKQqtkxCIB41OYCwC07WIvHGUcZnLtlJSD6ClqwuDKebJoSUwBRln9pKwcZWQnw21rWwEs5sO8c9SUBFfiXnSZHAEks+ipqMB4QtpHlCYm2OZ9cwRtrQ6e6JqKyKoAHxZa2hZxPvXK7kmuEB+rxRKAYkuQih6sT35GdgAt5UOketpRjNi+4hkq0wEERgNPQqM19XNuHxz0P2QngAXa+ByBepOPtWCvkJCT13dPPIYy9EABQVnY0PKkebzO4lAH+7dS2e3a9vhcC+mZ4C6I+sBKyE2DBE8P0lPS991DLLju8Q9SQlQAPHjDEN/FPAmxqbGpUgbAhQIbek42AGHg775MHmPftdAc5OtXxeYyWhYA28GMA6/rMnQkgvjXjE+/I3ld8XY0ZY9zOBGA0Mc56nn+vmxN4bM9CwBiey/XMEwFrGQRKT2+K5tLWTL7nFAI+BGo5McaqbAzBXMlDF54ATjCXH0OL/scIwCGBsnUfu7BTIPZajKikL9WY/zoHBlCVhfPrAAAAAElFTkSuQmCC";
    
    $scope.cancelarPedido = function (id, status, $event) {
        $event.preventDefault();
        if (status != 1) {
            mostraMensagemTemporaria('Não é possível cancelar um pedido que teve seu status modificado.');
        }
        else {
            mostraAguarde();
            $http.get(pegaURLAPI() + 'pedidos/' + id + '/' + 0)
                   .success(function (data, status, headers, config) {
                       $http.get(pegaURLAPI() + 'pedidoscliente/' + $scope.Cliente.UsuarioId)
                        .success(function (data, status, headers, config) {
                            ocultaAguarde();
                            mostraMensagemTemporaria('Pedido modificado com sucesso!.');
                            $state.go($state.current, {}, { reload: true });
                        }).error(function (data, status, headers, config) {
                            ocultaAguarde();
                            try {
                                mostraPopUpErro(data.Message);
                            }
                            catch (e) {
                                mostraPopUpErro('Ocorreu um erro ao buscar os pedidos... Por favor tente novamente.');
                            }
                        });
                   }).error(function (data, status, headers, config) {
                       ocultaAguarde();
                       try {
                           mostraPopUpErro(data.Message);
                       }
                       catch (e) {
                           mostraPopUpErro('Ocorreu um erro ao cancelar o pedido... Por favor tente novamente.');
                       }
                   });

        }
    };
}]);