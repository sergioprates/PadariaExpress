app.controller('appController', ['$scope', '$http', 'mostraPopUpErro', 'mostraAguarde', 'ocultaAguarde', '$ionicSideMenuDelegate', function ($scope, $http, mostraPopUpErro, mostraAguarde, ocultaAguarde, $ionicSideMenuDelegate) {
    $scope.toggleLeft = function () {
        $ionicSideMenuDelegate.toggleLeft();
    };
}]);