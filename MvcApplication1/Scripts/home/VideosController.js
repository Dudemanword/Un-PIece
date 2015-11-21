var app = angular.module('unPiece', ['ui.bootstrap', 'ngAnimate']);
app.controller("VideosController", function ($scope, $databaseService, $q, $uibModal, $sce) {
    $scope.getLatestVideos = function () {
        if (!$scope.latestVideos) {
            console.log("No vids")
            $databaseService.getLatestVideosFromDatabase().then(function (response) {
                $scope.latestVideos = response;
                //for (var i = 0; i < response.length; i++) {
                //    var video = response[i];
                //    video["VideoUrl"] = $sce.trustAsResourceUrl(video["VideoUrl"]);
                //}
            });
        }
    }

    $scope.getLatestBlogs = function () {
        $databaseService.getLatestBlogsFromDatabase().then(function (response) {
            $scope.blogs = response;
        });
    }
    $scope.Interval = 3000;
    $scope.noWrapSlides = false;

    $scope.open = function (_video) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'myModalContent.html',
            resolve: {
                video: function () {
                    return _video
                }
            },
            controller: function ($scope) {
                $scope.video = _video;
                $scope.cancel = function () {
                    modalInstance.dismiss('cancel');
                };
            },
        });
    }

    $scope.cancel = function () {
        modalInstance.dismiss('cancel');
    };

    $scope.getLatestVideos();
    $scope.getLatestBlogs();
}).filter('trustAsResourceUrl', ['$sce', function ($sce) {
    return function (val) {
        return $sce.trustAsResourceUrl(val);
    };
}]);;

app.service('$databaseService', function ($q, $http) {
    this.getLatestVideosFromDatabase = function (callback) {
        return $q(function (resolve, reject) {
            $http.get('/api/videosapi').success(function (data) {
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
