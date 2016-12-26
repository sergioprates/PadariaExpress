app.service('returnToState', function ($ionicHistory) {
    return function (stateName) {
        var historyId = $ionicHistory.currentHistoryId();
        var history = $ionicHistory.viewHistory().histories[historyId];
        for (var i = history.stack.length - 1; i >= 0; i--) {
            if (history.stack[i].stateId == stateName) {
                $ionicHistory.backView(history.stack[i]);
                $ionicHistory.goBack();
            }
        }
    }
})