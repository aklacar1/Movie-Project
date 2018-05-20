'use strict';
app.factory('moviesService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {
    //#region Variables
    var serviceBase = ngAuthSettings.apiServiceBaseUri;  
    var movieServiceFactory = {}
    //#endregion
    //#region Service Calls
    var _getTopRated = function () {
        return $http.get(serviceBase + 'api/Movies/GetTop100RatedMovies').then(function (response) {
            return response;
        });
    }
    var _search = function (query) {
        var uri = serviceBase + 'api/Movies/SearchMovieByTitle/' + query;
        console.log(uri);
        return $http.get(uri).then(function (response) {
            return response;
        });
    }
    var _GetMovie = function (id) {
        return $http.get(serviceBase + 'api/Movies/GetMovieById/' + id).then(function (response) {
            return response;
        });
    };

    var _LoadMovies = function () {
        return $http.get(serviceBase + 'api/Movies/LoadMovies').then(function (response) {
            return response;
        });
    }
    var _AddMovie = function (data) {
        return $http.post(serviceBase + 'api/Movies/InsertMovie',data).then(function (response) {
            return response;
        });
    }
    var _UpdateMovie = function (data) {
        return $http.put(serviceBase + 'api/Movies/UpdateMovie',data).then(function (response) {
            return response;
        });
    }
    var _DeleteMovie = function (id) {
        return $http.delete(serviceBase + 'api/Movies/DeleteMovie/'+id).then(function (response) {
            return response;
        });
    }
    //#endregion
    //#region Exposing Factory
    movieServiceFactory.Search = _search;
    movieServiceFactory.GetTopRated = _getTopRated;
    movieServiceFactory.GetMovie = _GetMovie;
    movieServiceFactory.LoadMovies = _LoadMovies;
    movieServiceFactory.AddMovie = _AddMovie;
    movieServiceFactory.UpdateMovie = _UpdateMovie;
    movieServiceFactory.DeleteMovie = _DeleteMovie;
    return movieServiceFactory;
    //#endregion
}]);