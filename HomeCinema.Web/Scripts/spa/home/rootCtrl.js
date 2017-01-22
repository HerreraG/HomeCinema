(function (app) {
    'use strict';

    app.controller('rootCtrl', rootCtrl);

    function rootCtrl($scope) {

        $scope.userData = {};

        $scope.userData.displayUserInfo = displayUserInfo;
        $scope.logout = logout;

        function displayUserInfo() {
        }

        function logout() {

        }

        $scope.userData.displayUserInfo();
    }
})(angular.module('homeCinema'));