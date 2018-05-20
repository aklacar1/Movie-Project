'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', '$route', '$window', 'Notification', function ($scope, $location, authService, $route, $window, Notification) {
   //#region Auth Methods
    $scope.logOut = function () {
        authService.logOut();
        $window.location.reload();
        $location.path('/login'); 
    }
    $scope.authentication = authService.authentication;
    //#endregion
   //#region View Methods
   $scope.isActive = function (viewLocation) {
    return viewLocation === $location.path();
   };
   //#endregion
   //#region Check if Admin
    $scope.isAdmin = function () {
        if ($scope.authentication.roles.lastIndexOf("SU") != -1)
            return true;
        return false;
    };
    $scope.isAdmin();
    //#endregion
}]);