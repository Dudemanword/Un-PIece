app.controller("HomeController", function ($scope, $serverCommunication, $q, $uibModal, $sce, $route) {
    $scope.getVideos = function () {
        if (!$scope.videos) {
            $serverCommunication.getVideosFromDatabase().then(function (response) {
                $scope.videos = response.videos;
                $scope.latestVideo = response.latestVideo;
            });
        }
    }

    $scope.getLatestBlogs = function () {
        $serverCommunication.getLatestBlogsFromDatabase().then(function (response) {
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

    $scope.getVideos();
    $scope.getLatestBlogs();
}).filter('trustAsResourceUrl', ['$sce', function ($sce) {
    return function (val) {
        return $sce.trustAsResourceUrl(val);
    };
}]);;

app.service('$serverCommunication', function ($q, $http) {
    this.getVideosFromDatabase = function (callback) {
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

    this.postBlogPost = function (postData) {
        return $q(function (resolve, reject) {
            $http.post('/api/blogs', postData).success(function (data) {
                console.log('success!');
            });
        });
    }
});



