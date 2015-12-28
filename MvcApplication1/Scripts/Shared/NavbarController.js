var app = angular.module('unPiece', ['ui.bootstrap', 'ngAnimate'], function($locationProvider){
    $locationProvider.html5Mode(true);
});

app.controller('NavbarController', function ($scope, $location) {
    $scope.isActive = function (viewLocation) {
        return viewLocation === $location.path();
    };
});
