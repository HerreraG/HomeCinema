(function (app) {
    'use strict';

    app.controller('customerEditCtrl', customerEditCtrl);

    customerEditCtrl.$inject = ['$scope', '$uibModalInstance', '$timeout', 'apiService', 'notificationService'];

    function customerEditCtrl($scope, $uibModalInstance, $timeout, apiService, notificationService) {

        $scope.datepicker = {};

        $scope.cancelEdit = cancelEdit;
        $scope.updateCustomer = updateCustomer;
        $scope.openDatePicker = openDatePicker;

        $scope.EditedCustomer.DateOfBirth = new Date($scope.EditedCustomer.DateOfBirth);

        $scope.dateOptions = {
            dateDisabled: disabled,
            maxDate: new Date(2020, 5, 22),
            minDate: $scope.EditedCustomer.DateOfBirth,
            ngModelOptions: $scope.EditedCustomer.DateOfBirth,
            formatYear: 'yy',
            startingDay: 1
        };


        function updateCustomer() {
            apiService.post('/api/customers/update/', $scope.EditedCustomer,
            updateCustomerCompleted,
            updateCustomerLoadFailed);
        }

        function updateCustomerCompleted(response) {
            notificationService.displaySuccess($scope.EditedCustomer.FirstName + ' ' + $scope.EditedCustomer.LastName + ' has been updated');
            $scope.EditedCustomer = {};
            $uibModalInstance.dismiss();
        }

        function updateCustomerLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $uibModalInstance.dismiss();
        }

        function disabled(data) {
            var date = data.date,
              mode = data.mode;
            return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
        }

        function openDatePicker() {
            $timeout(function () {
                $scope.datepicker.opened = true;
            });

            $timeout(function () {
                $('ul[datepicker-popup-wrap]').css('z-index', '10000');
            }, 100);

        };

    }

})(angular.module('homeCinema'));