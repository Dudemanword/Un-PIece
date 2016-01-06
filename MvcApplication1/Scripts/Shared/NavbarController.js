var app = angular.module('unPiece', ['ui.bootstrap', 'ngAnimate', 'ngRoute'], function(){
});

app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/home", {
            templateUrl: '/static/HomePartial.html',
            controller: 'HomeController',
            resolve: {
                function() {
                    console.log("In Home")
                }
            }
        }).when("/", {
            templateUrl: '/static/HomePartial.html',
            controller: 'HomeController',
            resolve: {
                function() {
                    console.log("In Home")
                }
            }
        })

        .when("/videos", {
            templateUrl: '/static/VideoPartial.html',
            controller: 'VideoController',
            resolve: {
                function() {
                    console.log("In Video")
                }
            }
        })

        .when("/blogs", {
            templateUrl: '/static/CreatePost.html',
            controller: 'BlogController'
        })

    .otherwise({ redirectTo: "/" })
}]);

app.controller('NavbarController', function ($scope, $location) {
    $scope.isActive = function (viewLocation) {
        return viewLocation === $location.path();
    };
});

