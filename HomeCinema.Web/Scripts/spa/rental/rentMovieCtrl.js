(function (app) {

    'use strict';

    app.controller('rentMovieCtrl', rentMovieCtrl);

    rentMovieCtrl.$inject = ['$scope', '$location', 'apiService', 'notificationService'];

    function rentMovieCtrl($scope, $location, apiService, notificationService) {

        $scope.Title = $scope.movie.Title;
        $scope.loadStockItems = loadStockItems;
        $scope.selectCustomer = selectCustomer;
        $scope.selectionChanged = selectionChanged;
        $scope.rentMovie = rentMovie;
        $scope.cancelRental = cancelRental;
        $scope.stockItems = [];
        $scope.selectedCustomer = -1;
        $scope.isEnabled = false;

        function loadStockItems() {
            notificationService.displayInfo('Loading available stock items for ' + $scope.movie.Title);

            apiService.get('/api/stocks/movie/' + $scope.movie.Id, null, stockItemsLoadCompleted, stockItemsLoadFailed);
        }

        function stockItemsLoadCompleted(result) {
            $scope.stockItems = result.data;
            $scope.selectedStockItem = $scope.stockItems[0].Id;
        }

        function stockItemsLoadFailed(error) {
            notificationService.displayError(error.data);
        }

        function rentMovie() {
            apiService.post('/api/rentals/rent/' + $scope.selectedCustomer + '/' + $scope.selectedStockItem, null,
            rentMovieSucceeded,
            rentMovieFailed);
        }

        function rentMovieSucceeded(response) {
            notificationService.displaySuccess('Rental completed successfully');
            $modalInstance.close();
        }

        function rentMovieFailed(response) {
            notificationService.displayError(response.data.Message);
        }

        function cancelRental() {
            $scope.stockItems = [];
            $scope.selectedStockItem = -1;
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }

        function selectCustomer($item) {
            if ($item) {
                $scope.selectedCustomer = $item.originalObject.Id;
                $scope.isEnabled = true;
            } else {
                $scope.selectedCustomer = -1;
                $scope.isEnabled = false;
            }
        }

        loadStockItems();
    }
})(angular.module('homeCinema'));