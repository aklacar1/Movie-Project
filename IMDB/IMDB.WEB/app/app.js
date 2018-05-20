var app = angular.module('IMDBWEB', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'treeGrid', 'ngFileUpload', 'ui.bootstrap', 'ui-notification', 'ngMaterial', 'ngMessages']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/faq", {
        controller: "indexController",
        templateUrl: "/app/views/FAQ.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });
    $routeProvider.when("/movies", {
        controller: "moviesController",
        templateUrl: "/app/views/movies.html"
    });
    $routeProvider.when("/movies/:id", {
        controller: "moviesController",
        templateUrl: "/app/views/movie.html"
    });
    $routeProvider.when("/admin/movies", {
        controller: "moviesController",
        templateUrl: "/app/views/Admin Views/movieDashboard.html"
    });
    $routeProvider.otherwise({ redirectTo: "/home" });

});

var serviceBase = 'http://localhost:51346/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});



app.filter('trustAsResourceUrl', ['$sce', function ($sce) {
    return function (val) {
        return $sce.trustAsResourceUrl(val);
    };
}]);
app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

app.config(function (NotificationProvider) {
    NotificationProvider.setOptions({
        
        startTop: 20,
        startRight: 70,
        delay: 5000,
        verticalSpacing: 20,
        horizontalSpacing: 20,
        positionX: 'center',
        positionY: 'bottom'
    });
});

app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.withCredentials = true;
}])
