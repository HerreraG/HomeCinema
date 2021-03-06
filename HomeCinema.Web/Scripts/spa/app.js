﻿(function () {
    'use strict';


    angular.module('homeCinema', ['common.core', 'common.ui'])
           .config(config)
           .run(run);

    config.inject = ['$routeProvider', '$locationProvider', '$httpProvider'];

    function config($routeProvider, $locationProvider, $httpProvider) {
        $routeProvider
        .when("/", {
            templateUrl: "scripts/spa/home/index.html",
            controller: "indexCtrl"
        })
        .when("/login", {
            templateUrl: "scripts/spa/account/login.html",
            controller: "loginCtrl"
        })
        .when("/register", {
            templateUrl: "scripts/spa/account/register.html",
            controller: "registerCtrl"
        })
        .when("/customers", {
            templateUrl: "scripts/spa/customers/customers.html",
            controller: "customersCtrl"
        })
        .when("/customers/register", {
            templateUrl: "scripts/spa/customers/register.html",
            controller: "customersRegCtrl",
            resolve: { isAuthenticated: isAuthenticated }
        })
        .when("/movies", {
            templateUrl: "scripts/spa/movies/index.html",
            controller: "moviesCtrl"
        })
        .when("/movies/add", {
            templateUrl: "scripts/spa/movies/add.html",
            controller: "movieAddCtrl",
            resolve: { isAuthenticated: isAuthenticated }
        })
        .when("/movies/:id", {
            templateUrl: "scripts/spa/movies/details.html",
            controller: "movieDetailsCtrl"
        })
        .when("/movies/edit/:id", {
            templateUrl: "scripts/spa/movies/edit.html",
            controller: "movieEditCtrl",
            resolve: { isAuthenticated: isAuthenticated }
        })
        .when("/rental", {
            templateUrl: "scripts/spa/rental/index.html",
            controller: "rentStatsCtrl"
        }).otherwise({ redirectTo: "/" });

        $locationProvider.hashPrefix('');

        $httpProvider.useApplyAsync(true);
    }

    isAuthenticated.$inject = ['membershipService', '$rootScope', '$location'];

    function isAuthenticated(membershipService, $rootScope, $location) {
        if (!membershipService.isUserLoggedIn()) {
            $rootScope.previousState = $location.path();
            $location.path('/login');
        }
    }

    run.inject = ['$rootScope', '$location', '$cookieStore', '$http'];

    function run($rootScope, $location, $cookieStore, $http) {
        $rootScope.repository = $cookieStore.get('repository') || {};

        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common['Authorization'] = $rootScope.repository.loggedUser.authData;
        }

        $(document).ready(function () {
            $(".fancybox").fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });

            $('.fancybox-media').fancybox({
                openEffect: 'none',
                closeEffect: 'none',
                helpers: {
                    media: {}
                }
            });

            $('[data-toggle=offcanvas]').click(function () {
                $('.row-offcanvas').toggleClass('active');
            });
        });
    }

})();