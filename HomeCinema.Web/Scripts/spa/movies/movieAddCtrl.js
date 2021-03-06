﻿(function (app) {

    'use strict';

    app.controller('movieAddCtrl', movieAddCtrl);


    movieAddCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService', 'fileUploadService'];

    function movieAddCtrl($scope, $location, $routeParams, apiService, notificationService, fileUploadService) {

        $scope.pageClass = 'page-movies';
        $scope.movie = { GenreId: 1, Rating: 1, NumberOfStocks: 1 };

        $scope.genres = [];
        $scope.isReadOnly = false;
        $scope.AddMovie = AddMovie;
        $scope.prepareFiles = prepareFiles;
        $scope.openDatePicker = openDatePicker;
        $scope.changeNumberOfStocks = changeNumberOfStocks;

        $scope.dateOptions = {
            formatYear: 'yy',
            maxDate: new Date(2020, 5, 22),
            startingDay: 1
        };
        $scope.datepicker = {};

        $scope.operationStock = function (operation) {
            if (operation == 'sum') {
                $scope.movie.NumberOfStocks += 1;
            } else if (operation == 'subtraction' && $scope.movie.NumberOfStocks > 1) {
                $scope.movie.NumberOfStocks -= 1;
            }
        }

        var movieImage = null;

        function loadGenres() {
            apiService.get('api/genres/', null, genresLoadCompleted, genresLoadFailed);
        }

        function genresLoadCompleted(result) {
            $scope.genres = result.data;
        }

        function genresLoadFailed(error) {
            notificationService.displayError(error.data);
        }

        function AddMovie() {
            AddMovieModel();
        }

        function AddMovieModel() {
            apiService.post('/api/movies/add', $scope.movie, addMovieSucceded, addMovieFailed);
        }

        function prepareFiles($files) {
            movieImage = $files;
        }

        function addMovieSucceded(response) {
            notificationService.displaySuccess($scope.movie.Title + ' has been submitted to Home Cinema');
            $scope.movie = response.data;
         
            if (movieImage) {
                fileUploadService.uploadImage(movieImage, $scope.movie.Id, redirectToEdit);
            }
            else {
                redirectToEdit();
            }
        }

        function addMovieFailed(error) {
            notificationService.displayError(error.statusText);
        }

        function openDatePicker() {;
            $scope.datepicker.opened = true;
        };

        function redirectToEdit() {
            $location.url('movies/edit/' + $scope.movie.Id);
        }

        function changeNumberOfStocks($vent) {
            var btn = $('#btnSetStocks'),
            oldValue = $('#inputStocks').val().trim(),
            newVal = 0;

            if (btn.attr('data-dir') == 'up') {
                newVal = parseInt(oldValue) + 1;
            } else {
                if (oldValue > 1) {
                    newVal = parseInt(oldValue) - 1;
                } else {
                    newVal = 1;
                }
            }
            $('#inputStocks').val(newVal);
            $scope.movie.NumberOfStocks = newVal;
        }

        loadGenres();

    }
})(angular.module('homeCinema'));