app.controller('VideoController', function ($scope, $databaseService, $uibModal) {
    $databaseService.getVideosFromDatabase().then(function (response) {
        $scope.videos = response.videos;
        $scope.latestVideo = response.latestVideo;
    });

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
});