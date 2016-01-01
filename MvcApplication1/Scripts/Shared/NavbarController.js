var app = angular.module('unPiece', ['ui.bootstrap', 'ngAnimate', 'ngRoute'], function($locationProvider){
    $locationProvider.html5Mode(true);

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
        })

        .when("/videos", {
            templateUrl: '/static/Videos.html',
            controller: 'VideoController'
        })

    .otherwise({ redirectTo: "/" })
}]);

app.controller('NavbarController', function ($scope, $location) {
    $scope.isActive = function (viewLocation) {
        return viewLocation === $location.path();
    };
});

