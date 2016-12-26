/// <reference path="E:\Desenvolvimento\Produtos\PadariaExpress\PadariaExpress.Website\Scripts/Util.js" />



angular.module('PadariaExpressApp', [])
.controller('DashboardController', function ($scope, $http) {
    $scope.Pedidos = [];
    $scope.Markers = [];
    $scope.Padaria = null;


    $(document).ready(function () {
        try {
            var usuario = null;
            if (window.localStorage.getItem('usuarioFuncionario') == null || window.localStorage.getItem('usuarioFuncionario') == 'null') {
                usuario = JSON.parse(window.localStorage.getItem('usuarioProprietario'));
            }
            else {
                usuario = JSON.parse(window.localStorage.getItem('usuarioFuncionario'));
                $('.proprietario').hide();
            }
            google.maps.event.addDomListener(window, 'load', function () {
                $scope.carregarPedidos();
            });
        }
        catch (e) {
            ocultaAguarde();
        }
    });


    $scope.carregarPedidos = function () {
        mostraAguarde();
        $http.get('api/padaria/buscar/' + querystring('PadariaId'))
          .then(function (data) {
              $scope.Padaria = data.data.d;
              $http.get('API/pedidos/' + querystring('PadariaId'))
        .then(function (data) {

            $scope.Pedidos = corrigeDatasUTC(data.data.d);

            /* position Amsterdam */
            var latlng = new google.maps.LatLng($scope.Padaria.Latitude, $scope.Padaria.Longitude);
            var infoWindow = new google.maps.InfoWindow();

            var mapOptions = {
                center: latlng,
                scrollWheel: false,
                zoom: 16,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
            };
            var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
            var bounds = new google.maps.LatLngBounds();

            bounds.extend(latlng);

            //Adicionando a padaria

            var padariaMarker = new google.maps.Marker({
                position: latlng,
                map: map,
                title: $scope.Padaria.NomeFantasia,
                icon: 'img/padariamarker.png'
            });
            var infoWindowContentPadaria = '<div class="info_content">' +
              '<h3>' + $scope.Padaria.NomeFantasia + '</h3>' +
              '<p>' + 'Você está aqui!' + '</p>'
              + '</div>';

            google.maps.event.addListener(padariaMarker, 'click', (function (marker, i) {
                return function () {
                    infoWindow.setContent(infoWindowContentPadaria);
                    infoWindow.open(map, marker);
                }
            })(padariaMarker, i));

            var infoWindowContents = new Array();
            for (var i = 0; i < $scope.Pedidos.length; i++) {
                var infoWindowContent = '<div class="info_content">' +
              '<h3>' + $scope.Pedidos[i].Cliente.Nome.split(' ')[0] + '</h3>' +
              '<p>' + $scope.Pedidos[i].Endereco.Logradouro + ' - ' + $scope.Pedidos[i].Endereco.Numero + '</p>' +
              '<p>' + 'Pedido #' + $scope.Pedidos[i].PedidoId + '</p>'
              + '</div>';
                infoWindowContents.push(infoWindowContent);

                var position = new google.maps.LatLng($scope.Pedidos[i].Endereco.Latitude, $scope.Pedidos[i].Endereco.Longitude);
                bounds.extend(position);
                var marker = new google.maps.Marker({
                    position: position,
                    map: map,
                    title: $scope.Pedidos[i].Cliente.Nome.split(' ')[0],
                    icon: 'img/pessoamarker.png'
                });

                google.maps.event.addListener(marker, 'click', (function (marker, i) {
                    return function () {
                        infoWindow.setContent(infoWindowContents[i]);
                        infoWindow.open(map, marker);
                    }
                })(marker, i));
            }

            map.fitBounds(bounds);

            ocultaAguarde();

            setTimeout(function () {
                $('[data-toggle="tooltip"]').tooltip({ trigger: 'hover' });
            }, 400);




}, function (erro) {
    ocultaAguarde();
    if (IsEmpty(erro.responseJSON.Message) == false) {
        mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON.Message, 'danger');
    } else {
        mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON, 'danger');
    }
});
  }, function (erro) {
      ocultaAguarde();
      if (IsEmpty(erro.responseJSON.Message) == false) {
          mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON.Message, 'danger');
      } else {
          mostraMensagem('AlertaErro', 'lblErro', erro.responseJSON, 'danger');
      }
  });
    };

    $scope.pegarClasse = function (status) {
        switch (status) {
            case 1:
                return 'panel-warning';
                break;
            case 2:
                return 'panel-primary';
                break;
            case 3:
                return 'panel-info';
                break;
            case 4:
                return 'panel-success';
                break;
            case 5:
                return 'panel-danger';
                break;
            default:

        }
    };

    $scope.alterarStatus = function (pedido, status) {
        mostraAguarde();
        for (var i = 0; i < $scope.Pedidos.length; i++) {
            if (pedido.PedidoId == $scope.Pedidos[i].PedidoId) {
                $scope.Pedidos[i].Status = status;
                break;
            }
        }

        var obj = new Object();
        chamadaAPI('api/pedidos/' + pedido.PedidoId + '/' + status, 'GET', obj, function () {
            ocultaAguarde();
        }, function () {
            ocultaAguarde();
        });
    };

});

function corrigeDatasUTC(pedidos) {
    for (var i = 0; i < pedidos.length; i++) {
        pedidos[i].DataPedido = new Date(pedidos[i].DataPedido.replace('T', ' ') + ' UTC');
        pedidos[i].DataPedido = pedidos[i].DataPedido.getFullYear() + '-' + (pedidos[i].DataPedido.getMonth() + 1) + '-' + pedidos[i].DataPedido.getDate() + 'T' + pedidos[i].DataPedido.getHours() + ':' + pedidos[i].DataPedido.getMinutes();
    }

    return pedidos;
}


function activeButton(btn) {
    var itens = $('[pedido=' + $(btn).attr('pedido') + ']');
    itens.removeClass('btn-primary');
    itens.removeClass('btn-danger');
    itens.removeClass('btn-warning');
    itens.removeClass('btn-success');
    itens.removeClass('btn-info');

    itens.addClass('btn-muted');

    var itemClass = $(btn).attr('class-element');
    $(btn).addClass(itemClass);

    var div = itens.parent();
    div = div.parent();
    div = div.parent();
    div = div.parent();
    div = div.parent();

    //Pegando o panel do pedido.

    $(div).removeClass('panel-primary');
    $(div).removeClass('panel-danger');
    $(div).removeClass('panel-warning');
    $(div).removeClass('panel-success');
    $(div).removeClass('panel-info');

    $(div).addClass('panel-' + itemClass.replace('btn-', ''));
    var obj = new Object();
}

function desativaBotoesAnteriores(lbl, label) {
    var status = 0;
    mostraAguarde();
    switch (lbl) {
        case 'lblPendente':
            break;
        case 'lblAceito':
            $('#lblPendente' + $(label).attr('pedido')).attr('disabled', 'disabled');
            status = 2;
            break;
        case 'lblSaiuEntrega':
            $('#lblPendente' + $(label).attr('pedido')).attr('disabled', 'disabled');
            $('#lblAceito' + $(label).attr('pedido')).attr('disabled', 'disabled');
            status = 3;
            break;
        case 'lblEntregue':
            $('#lblPendente' + $(label).attr('pedido')).attr('disabled', 'disabled');
            $('#lblAceito' + $(label).attr('pedido')).attr('disabled', 'disabled');
            $('#lblSaiuEntrega' + $(label).attr('pedido')).attr('disabled', 'disabled');
            $('#lblRejeitado' + $(label).attr('pedido')).attr('disabled', 'disabled');
            status = 4;
            break;
        case 'lblRejeitado':
            $('#lblPendente' + $(label).attr('pedido')).attr('disabled', 'disabled');
            $('#lblAceito' + $(label).attr('pedido')).attr('disabled', 'disabled');
            $('#lblSaiuEntrega' + $(label).attr('pedido')).attr('disabled', 'disabled');
            $('#lblEntregue' + $(label).attr('pedido')).attr('disabled', 'disabled');
            status = 5;
            break;
        default:
    }
    var obj = new Object();
    chamadaAPI('api/pedidos/' + $(label).attr('pedido') + '/' + status, 'GET', obj, function () {
        ocultaAguarde();
    }, function () {
        ocultaAguarde();
    });

    //atualizar status do pedido.
}