var app = angular.module('unPiece', ['ui.bootstrap', 'ngAnimate']);
app.controller("VideosController", function ($scope, $databaseService, $q) {
    $scope.getLatestVideos = function () {
        $databaseService.getLatestVideosFromDatabase().then(function (response) {
            $scope.latestVideos = response;
        });
    }

    $scope.getLatestBlogs = function () {
        $databaseService.getLatestBlogsFromDatabase().then(function (response) {
            $scope.blogs = response;
        });
    }
    $scope.Interval = 3000;
    $scope.noWrapSlides = false;

    $scope.hoverIn = function () {
        this.isMouseOver = true;
    };

    $scope.hoverOut = function () {
        this.isMouseOver = false;
    };

    $scope.getLatestVideos();
    $scope.getLatestBlogs();
});

app.service('$databaseService', function ($q, $http) {
    this.getLatestVideosFromDatabase = function (callback) {
        return $q(function (resolve, reject) {
            $http.get('/api/videos').success(function (data) {
                resolve(data);
            });
        })
    }

    this.getLatestBlogsFromDatabase = function (callback) {
        return $q(function (resolve, reject) {
            $http.get('/api/blogs').success(function (data) {
                resolve(data);
            });
        })
    }
});