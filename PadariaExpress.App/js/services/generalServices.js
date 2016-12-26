app.service('mostraAguarde', function ($ionicLoading) {
    return function () {
        $ionicLoading.show({
            template: "<ion-spinner></ion-spinner>"
        });
    }
})
.service('ocultaAguarde', function ($ionicLoading) {
    return function () { $ionicLoading.hide(); }
})
.service('mostraPopUpSucesso', function ($ionicPopup, returnToState) {
    return function (tituloModal, texto, funcao) {
        var alertPopup = $ionicPopup.alert({
            title: tituloModal,
            template: texto
        });
        alertPopup.then(funcao);
    }
})
.service('mostraPopUpErro', function ($ionicPopup) {
    return function (texto) {
        var alertPopup = $ionicPopup.alert({
            title: 'Ops... Ocorreu um erro',
            template: texto
        });
        alertPopup.then(function (res) {
        });
    }
})
.service('mostraPopUp', function ($ionicPopup) {
    return function (texto) {
        var alertPopup = $ionicPopup.alert({
            title: 'Mensagem',
            template: texto
        });
        alertPopup.then(function (res) {
        });
    }
})
.service('mostraMensagemTemporaria', function ($cordovaToast, mostraPopUp) {
    return function (mensagem, duracao, posicao) {
        try {
            $cordovaToast.showShortBottom(mensagem).then(function (success) {
                //alert('Exibiu');
            }, function (error) {
                mostraPopUp('erro na mensagem temporaria: ' + JSON.stringify(error));
            });
        }
        catch (e) {
            try {
                alert(JSON.stringify(window.plugins.toast));
                window.plugins.toast.showShortBottom(mensagem);
            }
            catch (e) {
                mostraPopUp(mensagem);
            }
        }
    }
})
    .service('mostraMensagemTemporariaErro', function ($cordovaToast, mostraPopUpErro) {
        return function (mensagem, duracao, posicao) {
            try {
                $cordovaToast.showShortBottom(mensagem).then(function (success) {
                    //alert('Exibiu');
                }, function (error) {
                    mostraPopUpErro('erro na mensagem temporaria: ' + JSON.stringify(error));
                });
            }
            catch (e) {
                try {
                    alert(JSON.stringify(window.plugins.toast));
                    window.plugins.toast.showShortBottom(mensagem);
                }
                catch (e) {
                    mostraPopUpErro(mensagem);
                }
            }
        }
    })
.service('onLoadApp', function (mostraPopUpErro, apostasFactory, $http) {
    return function () {
       
    }
})

