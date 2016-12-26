
var app = angular.module('padariaexpress', ['ionic', 'ngCordova', 'nemLogging', 'ui.mask', 'ngCpfCnpj'])
    .run(function ($ionicPlatform, PushProcessingService) {
        $ionicPlatform.ready(function () {
            // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
            // for form inputs)


            setTimeout(function () {
                try {
                    PushProcessingService.initialize();

                }
                catch (e) {
                    //alert('Erro ao inicializar push: ' + JSON.stringify(e));
                }
            }, 6000);



            document.addEventListener("online", onOnline, false);
            document.addEventListener("resume", onResume, false);
        });

    });

function onOnline() {
    loadMapsApi();
}

function onResume() {
    loadMapsApi();
}

function loadMapsApi() {
    if (navigator.connection.type === Connection.NONE || (global.google !== undefined && global.google.maps)) {
        return;
    }
    // load maps api

}

function buscarStatusPedido(status) {
    switch (status) {
        case 0: 
            return 'Cancelado';
        case 1:
            return 'Pendente';
        case 2:
            return 'Aceito';
        case 3:
            return 'Saiu para entrega';
        case 4:
            return 'Entregue';
        case 5:
            return 'Rejeitado';
        default:

    }

}

function buscarDiaDaSemana(dia) {

    switch (dia) {
        case 0:
            return "Domingo";

            break;
        case 1:
            return "Segunda-Feira";

            break;
        case 2:
            return "Terça-Feira";

            break;
        case 3:
            return "Quarta-Feira";

            break;
        case 4:
            return "Quinta-Feira";

            break;
        case 5:
            return "Sexta-Feira";

            break;
        case 6:
            return "Sábado";

            break;
        default:

    }
}




function pegaURLAPI() {
    //return 'http://localhost:63968/api/';
    return 'http://www.padariaexpress.com/api/';
}



function onNotificationGCM(e) {
    //    alert('EVENT -> RECEIVED:' + e.event + '');
    switch (e.event) {
        case 'registered':
            if (e.regid.length > 0) {
                //alert('REGISTERED with GCM Server -> REGID:' + e.regid);

                //call back to web service in Angular.
                //This works for me because in my code I have a factory called
                //      PushProcessingService with method registerID
                var elem = angular.element(document.querySelector('[ng-app]'));
                var injector = elem.injector();
                var myService = injector.get('PushProcessingService');
                myService.registerID(e.regid);
            }
            break;
        case 'message':
            // if this flag is set, this notification happened while we were in the foreground.
            // you might want to play a sound to get the user's attention, throw up a dialog, etc.
            if (e.foreground) {
                //we're using the app when a message is received.
                // alert('--INLINE NOTIFICATION--' + '');

                // if the notification contains a soundname, play it.
                //var my_media = new Media(&quot;/android_asset/www/&quot;+e.soundname);
                //my_media.play();
                //alert(e.payload.message);
            }
            else {
                // otherwise we were launched because the user touched a notification in the notification tray.
                //  if (e.coldstart)
                //alert('--COLDSTART NOTIFICATION--' + '');
                //else
                //alert('--BACKGROUND NOTIFICATION--' + '');

                // direct user here:
                // window.location = &quot;#/tab/featured&quot;;
            }
            //window.localStorage.setItem('concursoRealizado', JSON.stringify(e.payload.data));

            var elem = angular.element(document.querySelector('[ng-app]'));
            var injector = elem.injector();
            var myService = injector.get('$state');
            myService.go('app.pedidos');
            break;

        case 'error':
            alert('ERROR -> MSG:' + e.msg + '');
            break;

        default:
            alert('EVENT -> Unknown, an event was received and we do not know what it is');
            break;
    }
}


function EstaConectado() {
    try {
        var networkState = navigator.connection.type;

        var states = {};
        states[Connection.UNKNOWN] = 'Unknown connection';
        states[Connection.ETHERNET] = 'Ethernet connection';
        states[Connection.WIFI] = 'WiFi connection';
        states[Connection.CELL_2G] = 'Cell 2G connection';
        states[Connection.CELL_3G] = 'Cell 3G connection';
        states[Connection.CELL_4G] = 'Cell 4G connection';
        states[Connection.NONE] = 'No network connection';

        return !(states[networkState] === 'No network connection');
    }
    catch (e) {
        return true;
    }
}