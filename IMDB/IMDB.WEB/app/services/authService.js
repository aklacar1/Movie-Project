'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', '$location', function ($http, $q, localStorageService, ngAuthSettings, $location) {
    //#region Variables
    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var authServiceFactory = {};
    
    var _authentication = {
        isAuth: false,
        userName: "",
        UserId :null,
        roles: new Array()
    };

    var _externalAuthData = {
        provider: "",
        userName: "",
        externalAccessToken: ""
    };
    //#endregion
    //#region Main Auth Methods
    var _login = function (data) {

        console.log(data);

        var deferred = $q.defer();

        $http({
            url: serviceBase+"api/Users/Login",
            method: "POST",
            data: {
                    "userName": data.UserName,
                    "password": data.Password,
                    "rememberMe": data.RememberMe
            }, 
            withCredentials: true,
            withHeaders : true
        }).then(function (response) {
            //success
            console.log(response); 
            deferred.resolve(response);

        },
        function (response) {

            deferred.reject(response);
        });
       
        return deferred.promise;

    };
    var _logOut = function () {

          return $http({
              url: serviceBase+"api/Users/Logout",
              method:"POST",
              withCredentials:true
          })
          .then(function (response) {
              console.log(response.data);
              _authentication.isAuth = false;
              _authentication.userName = "";
          });
    };
    var _register = function (registerData) {

        console.log(registerData);

        var deferred = $q.defer();

        $http({
            url: serviceBase+"api/Users/Register",
            method: "POST",
            data: registerData,
            withCredentials: true
        }).then(function (response) {
            //success
            console.log(response);
            deferred.resolve(response);

        },
        function (response) {

            deferred.reject(response);
        });

        return deferred.promise;

    };
    //#endregion
    //#region Helper Methods
    var _fillAuthData = function () {
        if (_authentication.isAuth === true) {
            
            return;
        }
       
        var deferred = $q.defer();
        
        $http.get(serviceBase+"api/Users/Auth", { withCredentials: true })
         .then(function (response) {

             console.log(response.data);
             _authentication.isAuth = true;
             _authentication.userName = response.data.Username;
             _authentication.roles = new Array();
             _authentication.UserId = response.data.userId;
             for (var i = 0; i < response.data.roles.length; i++)
                 _authentication.roles.push(response.data.roles[i]);
             console.log(_authentication);
             deferred.resolve(response);
             if ($location.url() === '/login') {
                    $location.path('/movies');
             }
         }, function (response) {
             deferred.reject(response);
             console.log("Something went wrong");
         });
        return deferred.promise;
    };
    //#endregion
    //#region Exposing Factory
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.register = _register;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;
    return authServiceFactory;
    //#endregion
}]);