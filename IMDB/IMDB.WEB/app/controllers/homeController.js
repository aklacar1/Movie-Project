'use strict';
app.controller('homeController', ['$scope', '$http', 'authService', function ($scope, $http, authService) {
    //#region Auth
    $scope.authentication = authService.authentication;
    //#endregion
}]);