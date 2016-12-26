app.service('buscarPedidoIncompleto', function () {
    return function () {
        return window.localStorage.getItem('pedido');
    }
})
.service('excluirPedidoIncompleto', function () {
    return function () {
        return window.localStorage.removeItem('pedido');
    }
});