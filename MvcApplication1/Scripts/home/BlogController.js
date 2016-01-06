app.controller("BlogController", function ($scope, $q, $serverCommunication) {
    $scope.submitForm = function () {
        $serverCommunication.postBlogPost({ "Title": $scope.postTitle, "Content": $scope.postContent, "Description": $scope.postDescription });
    }
});