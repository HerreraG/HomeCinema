(function (app) {
    'use strict';

    app.controller('customersCtrl', customersCtrl);

    customersCtrl.$inject = ['$scope', '$uibModal', 'apiService', 'notificationService'];

    function customersCtrl($scope, $uibModal, apiService, notificationService) {

        $scope.pageClass = 'page-customers';
        $scope.loadingCustomers = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.Customers = [];

        $scope.search = search;
        $scope.clearSearch = clearSearch;

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;

        function search(page) {
            page = page || 0;

            $scope.loadingCustomers = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 4,
                    filter: $scope.filterCustomers
                }
            };

            apiService.get('/api/customers/search/', config,
            customersLoadCompleted,
            customersLoadFailed);
        }

        function openEditDialog(customer) {
            $scope.EditedCustomer = customer;

            var modalInstance = $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                scope: $scope,
                templateUrl: 'scripts/spa/customers/editCustomerModal.html',
                controller: 'customerEditCtrl',
            });

            modalInstance.result.then(function () {
                clearSearch();
            }, function () {
                console.log('Modal dismissed at: ' + new Date());
            });
        }

        function customersLoadCompleted(result) {
            $scope.Customers = result.data.Items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingCustomers = false;

            if ($scope.filterCustomers && $scope.filterCustomers.length) {
                notificationService.displayInfo(result.data.Items.length + ' customers found');
            }

        }

        function customersLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterCustomers = '';
            search();
        }

        $scope.search();
    }

})(angular.module('homeCinema'));