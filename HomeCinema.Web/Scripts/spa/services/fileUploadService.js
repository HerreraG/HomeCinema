(function (app) {
    'use strict';

    app.factory('fileUploadService', fileUploadService);

    fileUploadService.$inject = ['$rootScope', '$http', '$timeout', 'Upload', 'notificationService'];

    function fileUploadService($rootScope, $http, $timeout, Upload, notificationService) {

        $rootScope.upload = [];

        var service = {
            uploadImage: uploadImage
        }

        function uploadImage($files, movieId, callback) {
            //$files: an array of files selected
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    $rootScope.upload[index] = Upload.upload({
                        url: "api/movies/images/upload?movieId=" + movieId, // webapi url
                        method: "POST",
                        file: $file
                    }).then(function (resp) {
                        notificationService.displaySuccess(resp.FileName + ' uploaded successfully');
                        callback();
                    }, function (resp) {
                        notificationService.displayError(resp.status);
                    })
                })(i);
            }
        }

        return service;
    }
})(angular.module('common.core'));