app.controller('homeController', ['$scope', '$http', 'mostraPopUpErro', 'mostraAguarde', 'ocultaAguarde', '$ionicPlatform', '$timeout', 'mostraMensagemTemporaria', 'Initializer', function ($scope, $http, mostraPopUpErro, mostraAguarde, ocultaAguarde, $ionicPlatform, $timeout, mostraMensagemTemporaria, Initializer) {

    $scope.Padarias = new Array();
    $scope.MarcadoresPadarias = [];
    $scope.EstaOnLine = true;
    $scope.Endereco = new Object();
    $scope.$on('$ionicView.beforeEnter', function () {
        $scope.map = new Object();
        $scope.map.showMap = true;
        $ionicPlatform.ready(function () {
            $scope.EstaOnLine = EstaConectado();
            $scope.buscarPadariasPorLocalizacao = function () {
                try {

                    mostraAguarde();
                    try {
                        document.activeElement.blur();
                    }
                    catch (e) { }
                    if ($scope.EstaOnLine) {
                        navigator.geolocation.getCurrentPosition(function (posicao) {
                            //$scope.map = { center: { latitude: posicao.coords.latitude, longitude: posicao.coords.longitude }, zoom: 16 };
                            $scope.carregarMapa(posicao);
                        }, function (erro) {
                            $scope.map.showMap = false;
                            ocultaAguarde();
                            mostraMensagemTemporaria('Não foi possível pegar sua localização atual.');
                        }, { timeout: 8000, enableHighAccuracy: true });

                    }
                    else {
                        $scope.map.showMap = false;
                        ocultaAguarde();
                        mostraMensagemTemporaria('Você não está conectado à internet.');
                    }
                }
                catch (e) {
                    $scope.map.showMap = false;
                    ocultaAguarde();
                    alert('Erro ao carregar o mapa: ' + e.message);
                }
            };

            $scope.carregarMapa = function (posicao) {

                Initializer.mapsInitialized.then(function () {
                    $scope.map.showMap = true;
                    var latlng = new google.maps.LatLng(posicao.coords.latitude, posicao.coords.longitude);
                    var mapOptions = {
                        center: latlng,
                        scrollWheel: false,
                        zoom: 16,
                        mapTypeId: google.maps.MapTypeId.ROADMAP,
                    };

                    var map = new google.maps.Map(document.getElementById("MapaPadarias"), mapOptions);
                    var infoWindow = new google.maps.InfoWindow();
                    var bounds = new google.maps.LatLngBounds();
                    bounds.extend(latlng);

                    //Adicionando o cliente

                    var clienteMarker = new google.maps.Marker({
                        position: latlng,
                        map: map,
                        title: 'Você está aqui!',
                        icon: 'img/pessoamarker.png'
                    });

                    var infoWindowContentCliente = '<div class="info_content">' +
                      '<h4>' + 'Você está aqui!' + '</h4>'
                      //'<p>' + 'Você está aqui!' + '</p>'
                      + '</div>';

                    google.maps.event.addListener(clienteMarker, 'click', (function (marker, i) {
                        return function () {
                            infoWindow.setContent(infoWindowContentCliente);
                            infoWindow.open(map, marker);
                        }
                    })(clienteMarker, 0));

                    $http.get(
                        pegaURLAPI() + 'padaria/listar/proximidade?lat=' + posicao.coords.latitude + '&lng=' + posicao.coords.longitude
                    ).success(function (data) {
                        $scope.Padarias = data.d;
                        window.localStorage.setItem('padarias', JSON.stringify($scope.Padarias));
                        var infoWindowContents = new Array();
                        for (var i = 0; i < $scope.Padarias.length; i++) {
                            var infoWindowContent = '<div class="info_content">' +
                              '<h3>' + $scope.Padarias[i].NomeFantasia + '</h3>' +
                              '<img style="width: 200px;height:150px" src="data:image/png;base64,' + $scope.Padarias[i].FotoPrincipal + '"/><br>' +
                              '<span>' + $scope.Padarias[i].Logradouro + ' - ' + $scope.Padarias[i].Numero + '</span><br>' +
                            '<span>' + 'Telefone: ' + ' - (' + $scope.Padarias[i].Telefone.substring(0, 2) + ') ' + $scope.Padarias[i].Telefone.substring(2, 6) + '-' + $scope.Padarias[i].Telefone.substring(6, 10) + '</span><br>' +
                            '<span>' + 'Clique ' + ' <a href="#/app/padaria/' + $scope.Padarias[i].PadariaId + '">aqui</a> para visualizar o perfil desta padaria</span>'
                              + '</div>';
                            infoWindowContents.push(infoWindowContent);

                            var position = new google.maps.LatLng($scope.Padarias[i].Latitude, $scope.Padarias[i].Longitude);
                            bounds.extend(position);
                            var marker = new google.maps.Marker({
                                position: position,
                                map: map,
                                title: $scope.Padarias[i].NomeFantasia,
                                icon: 'img/padariamarker.png'
                            });

                            google.maps.event.addListener(marker, 'click', (function (marker, i) {
                                return function () {
                                    infoWindow.setContent(infoWindowContents[i]);
                                    infoWindow.open(map, marker);
                                }
                            })(marker, i));

                            map.fitBounds(bounds);
                        }


                        ocultaAguarde();
                        mostraMensagemTemporaria('Foram localizadas ' + $scope.Padarias.length + ' padarias próximas.');
                    }).error(function (data) {
                        ocultaAguarde();
                        mostraMensagemTemporaria(data.Message);
                    });


                });
            };



            $(document).ready(function () {
                window.localStorage.removeItem('pedido');
                window.localStorage.removeItem('padarias');
                window.localStorage.removeItem('padaria');
                $scope.buscarPadariasPorLocalizacao();
            });
        });




        $scope.buscarPadariasPorEndereco = function () {

            mostraAguarde();
            document.activeElement.blur();

            if (_.isEmpty($scope.Endereco.Logradouro) == false) {


                if ($scope.EstaOnLine) {
                    $http.get('http://maps.googleapis.com/maps/api/geocode/json?address=' + $scope.Endereco.Logradouro)
                            .success(function (data) {
                                // data.results[0].geometry.location.lat;
                                //data.results[0].geometry.location.lat;

                                if (data.results.length > 0) {
                                    $scope.map.showMap = true;
                                    var posicao = new Object();
                                    posicao.coords = new Object();
                                    posicao.coords.latitude = data.results[0].geometry.location.lat;
                                    posicao.coords.longitude = data.results[0].geometry.location.lng;

                                    $scope.carregarMapa(posicao);
                                }
                                else {
                                    $scope.map.showMap = false;
                                    ocultaAguarde();
                                    mostraMensagemTemporaria('Parece que ocorreu um problema ao consultar este endereço... Tente digitar "Rua X, Numero - Bairro - Cidade" ou então o CEP do endereço sem traço');
                                }

                            })
                            .error(function (erro) {
                                $scope.map.showMap = false;
                                ocultaAguarde();
                                mostraMensagemTemporaria(erro.Message);
                            });
                }
                else {
                    $scope.map.showMap = false;
                    ocultaAguarde();
                    mostraMensagemTemporaria('Você não está conectado à internet.');
                }
            }
            else {
                ocultaAguarde();
                mostraMensagemTemporaria('Preencha o endereço.');
            }
        };


        //alert('plataforma pronta - controller!');
        //var div = document.getElementById("MapaPadarias");
        //try {
        //    // Initialize the map view
        //    var map = plugin.google.maps.Map.getMap(div);

        //    // Wait until the map is ready status.
        //    map.addEventListener(plugin.google.maps.event.MAP_READY, function () {
        //        alert('Mapa Pronto');

        //    });
        //}
        //catch (e) {

        //    alert('erro no mapa: ' + e.message)
        //}
    });


}]);