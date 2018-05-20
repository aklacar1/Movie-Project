'use strict';
app.controller('moviesController', ['$scope', 'moviesService', '$routeParams', 'authService', '$location', function ($scope, movie, $routeParams, authService,$location) {
    //#region Variables
    $scope.movieList = [];
    $scope.searchText = '';
    $scope.authentication = authService.authentication;
    var ev = $routeParams;
    //#endregion
    //#region Get Movie Methods
    function GetTopRated() {
        $scope.movieList = [];
        movie.GetTopRated().then(function (response) {
            var data = response.data;
            console.log(response.data);
            for (var i = 0; i < 10; i++) {
                SetData(data[i]);
            }
        });
    };
    function LoadMovie(movieID) {
        $scope.movieList = [];
        movie.GetMovie(movieID).then(function (response) {
            var data = response.data;
            SetData(data);
        });
    };
    $scope.search = function () {
        if ($scope.searchText.length >= 3) {
            $scope.movieList = [];
            var query = $scope.searchText;
            movie.Search(query).then(function (response) {
                var data = response.data;
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    SetData(data[i]);
                }
            });
        }
        else if ($scope.searchText.length == 0) {
            GetTopRated();
        }
    }
    //#endregion
    //#region Helper Methods
    function SetData(data) {
        $scope.movieList.push({
            title: data.title,
            img: data.image,
            rating: data.rating,
            duration: data.duration,
            ID: data.movieId,
            releaseDate: new Date(data.releaseDate),
            releaseYear: data.releaseYear,
            trailerLink: data.trailerLink.replace("watch?v=", "embed//"),
            overview: data.summary,
            company: data.company,
            movieStaff: data.movieStaff,
            genres: data.genre
        });
    }
    //#endregion
    //#region Startup Methods
    function Load() {
        var x = $location;
        if (!ev.id && x.$$path != '/admin/movies') {
            GetTopRated();
        } else if (x.$$path != '/admin/movies') {
            LoadMovie(ev.id);
        } else {
            LoadMovies();
        }
    }
    Load();
    //#endregion
    //#region Admin Dashboard For Movies
    $scope.tree_data = new Array();
    var tree;
    $scope.my_tree = tree = {}
    $scope.event = {};
    //#region Expanding Property And Col Definitions
    $scope.expanding_property = {
        field: "title",
        displayName: "Title",
        sortable: true,
        filterable: true,
        cellTemplate: "<a ng-click = 'user_clicks_branch(row.branch)'>{{row.branch[expandingProperty.field]}}</a>",
    };
    $scope.col_defs = [
        {
            field: "company",
            displayName: "Company",
            sortable: false,
            filterable: true,
            sortingType: "string",
            cellTemplate: "<span>{{row.branch[col.field].name}}</span>"
        },
        {
            field: "releaseDate",
            displayName: "Release Date",
            sortable: true,
            filterable: false,
            sortingType: "int",
            cellTemplate: "<span>{{row.branch[col.field] | date : 'medium'}}</span>"
        },
        {
            field: "rating",
            displayName: "Rating",
            sortable: true,
            filterable: false,
            sortingType: "int"
        },
        {
            field: "Actions",
            displayName: "Actions",
            cellTemplate: "<button id='viewMe{{row.branch.id}}' ng-click='cellTemplateScope.clickView(row.branch)' class='btn btn-primary btn-xs' data-toggle='modal' data-target='#viewMovieModal' >View</button>" + " " + "<button ng-click='cellTemplateScope.clickEdit(row.branch)' class='btn btn-warning btn-xs' data-toggle='modal' data-target='#editMovieModal' >Edit</button>" + " " + "<button ng-click='cellTemplateScope.clickDel(row.branch)' class='btn btn-danger btn-xs' data-toggle='modal' data-target='#delMovieModal'  >Delete</button>",
            cellTemplateScope: {
                clickEdit: function (branch) {
                    var CompanyName = branch.company.name;
                    branch.releaseDate= new Date(branch.releaseDate);
                    $scope.editMovie = branch;
                },
                clickDel: function (branch) {
                    var CompanyName = branch.company.name;
                    branch.releaseDate= new Date(branch.releaseDate);
                    $scope.delMovie = branch;
                },
                clickView: function (branch) {
                    var CompanyName = branch.company.name;
                    branch.releaseDate= new Date(branch.releaseDate);
                    branch.trailerLink = branch.trailerLink.replace("watch?v=", "embed//");
                    $scope.viewMovie = branch;
                }
            }
        }
    ];
    //#endregion
    //#region Tree Handler On Select
    $scope.my_tree_handler = function (branch) {
        console.log('Data :', branch);
    }
    //#endregion
    //#region Helper Tree Methods
    function resetData() {
        $scope.tree_data = [];
        $scope.my_tree = tree = {};
    };
    $scope.resetSearch = function () {
        $scope.filterString = "";
    }
    //#endregion
    //#region CRUD Tree Methods
    function LoadMovies() {
        resetData();
        movie.LoadMovies().then(function (response) {
            $scope.tree_data = response.data;
            console.log($scope.tree_data);
        });
    };
    $scope.addMovieF = function (data) {
        console.log(data);
        data.companyId = data.company.companyId;
        movie.AddMovie(data).then(function (response) {
            LoadMovies();
        });
    };
    $scope.editMovieF = function (data) {
        data.companyId = data.company.companyId;
        movie.UpdateMovie(data).then(function (response) {
            LoadMovies();
        });
    };
    $scope.delMovieF = function (data) {
        movie.DeleteMovie(data).then(function (response) {
            LoadMovies();
        });
    };
    //#endregion
    //#endregion

    //#region Extension Method For downloading data
    $scope.downloadVariable = function (inData) {
        var data = "text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(inData));
        var downloader = document.createElement('a');
        downloader.setAttribute('href', "data:"+data);
        downloader.setAttribute('download', inData.title+'.json');
        downloader.click();
    };
    //#endregion
}]);