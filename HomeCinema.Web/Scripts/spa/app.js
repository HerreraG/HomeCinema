(function () {
    'use strict';


    angular.module('homeCinema', ['common.core', 'common.ui'])
           .config(config)
           .run(run);

    config.inject = ['$routeProvider'];

    function config($routeProvider) {
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
            templateUrl: "scripts/spa/customers/index.html",
            controller: "customersCtrl"
        })
        .when("/customers/register", {
            templateUrl: "scripts/spa/customers/register.html",
            controller: "customersRegCtrl"
        })
        .when("/movies", {
            templateUrl: "scripts/spa/movies/index.html",
            controller: "moviesCtrl"
        })
        .when("/movies/add", {
            templateUrl: "scripts/spa/movies/add.html",
            controller: "movieAddCtrl"
        })
        .when("/movies/:id", {
            templateUrl: "scripts/spa/movies/details.html",
            controller: "movieDetailsCtrl"
        })
        .when("/movies/edit/:id", {
            templateUrl: "scripts/spa/movies/edit.html",
            controller: "movieEditCtrl"
        })
        .when("/rental", {
            templateUrl: "scripts/spa/rental/index.html",
            controller: "rentStatsCtrl"
        }).otherwise({ redirectTo: "/" });
    }

    run.inject = ['$rootScope', '$location', '$cookieStore', '$http'];

    function run($rootScope, $location, $cookieStore, $http) {
        $rootScope.repository = $cookieStore.get('repository') || {};

        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common['Authorization'] = $rootScope.repository.loggedUser.authdata;
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