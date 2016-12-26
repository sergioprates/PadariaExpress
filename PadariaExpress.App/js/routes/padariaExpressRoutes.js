app.config(function ($stateProvider, $urlRouterProvider) {
    $stateProvider
    .state('app', {
        url: '/app',
        abstract: true,
        templateUrl: 'paginas/menu.html',
        controller: 'appController'
    })
    .state('app.home', {
        url: "/home",
        views: {
            'menuContent': {
                templateUrl: "paginas/home.html",
                controller: 'homeController'
            }
        }
    })
    .state('app.pedidos', {
        url: "/pedidos",
        views: {
            'menuContent': {
                templateUrl: "paginas/pedidos.html",
                controller: 'pedidosController'
            }
        }
    })
    .state('app.padarias', {
        url: "/padarias",
        views: {
            'menuContent': {
                templateUrl: "paginas/padarias.html",
                controller: 'padariasController'
            }
        }
    })
    .state('app.padaria', {
        url: "/padaria/:PadariaId",
        views: {
            'menuContent': {
                templateUrl: "paginas/padaria.html",
                controller: 'padariaController'
            }
        }
    })
    .state('app.produtos', {
        url: "/produtos/:PadariaId",
        views: {
            'menuContent': {
                templateUrl: "paginas/produtos.html",
                controller: 'produtosController'
            }
        }
    })
    .state('app.produto', {
        url: "/produto/:ProdutoId",
        views: {
            'menuContent': {
                templateUrl: "paginas/produto.html",
                controller: 'produtoController'
            }
        }
    })
    .state('app.pedido1', {
        url: "/pedido/:PedidoId",
        views: {
            'menuContent': {
                templateUrl: "paginas/pedido.html",
                controller: 'pedidoController'
            }
        }
    })
        .state('app.pedido', {
            url: "/pedido",
            views: {
                'menuContent': {
                    templateUrl: "paginas/pedido.html",
                    controller: 'pedidoController'
                }
            }
        })
    .state('app.perfil', {
        url: "/perfil",
        views: {
            'menuContent': {
                templateUrl: "paginas/perfil.html",
                controller: 'perfilController'
            }
        }
    })
    .state('app.finalizarpedido', {
        url: "/finalizarpedido",
        views: {
            'menuContent': {
                templateUrl: "paginas/finalizarpedido.html",
                controller: 'finalizarpedidoController'
            }
        }
    })
    //.state('minhasApostas', {
    //    url: '/2',
    //    templateUrl: 'paginas/minhasApostas.html'
    //})



    $urlRouterProvider.otherwise("/app/home");
}).config(function ($httpProvider) {
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
});