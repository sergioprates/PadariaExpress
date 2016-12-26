/// <reference path="../scripts/lodash.js" />


app.controller('padariaController', ['$scope', '$http', 'mostraPopUpErro', 'mostraAguarde', 'ocultaAguarde', '$stateParams', function ($scope, $http, mostraPopUpErro, mostraAguarde, ocultaAguarde, $stateParams) {
    $scope.Padaria = null;

    $scope.$on('$ionicView.beforeEnter', function () {
        var padarias = JSON.parse(window.localStorage.getItem('padarias'));
        $scope.Padaria = _.find(padarias, function (padaria) {
            return padaria.PadariaId == $stateParams.PadariaId;
        });

        window.localStorage.setItem('padaria', JSON.stringify($scope.Padaria));
    });

    $scope.buscarDiaDaSemana = buscarDiaDaSemana;
}]);