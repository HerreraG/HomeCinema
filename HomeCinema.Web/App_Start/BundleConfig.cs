﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace HomeCinema.Web.App_Start {
    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Vendors/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                        "~/Scripts/Vendors/jquery.js",
                        "~/Scripts/Vendors/bootstrap.min.js",
                        "~/Scripts/Vendors/toastr.js",
                        "~/Scripts/Vendors/jquery.raty.js",
                        "~/Scripts/Vendors/respond.src.js",
                        "~/Scripts/Vendors/angular.min.js",
                        "~/Scripts/Vendors/angular-route.min.js",
                        "~/Scripts/Vendors/angular-cookies.min.js",
                        "~/Scripts/Vendors/angular-validator.min.js",
                        "~/Scripts/Vendors/angular-base64.min.js",
                        "~/Scripts/Vendors/angular-file-upload.min.js",
                        "~/Scripts/Vendors/angucomplete-alt.min.js",
                        "~/Scripts/Vendors/jk-rating-stars.min.js",
                        "~/Scripts/Vendors/ui-bootstrap-tpls-2.4.0.js",
                        "~/Scripts/Vendors/underscore.js",
                        "~/Scripts/Vendors/raphael.js",
                        "~/Scripts/Vendors/morris.js",
                        "~/Scripts/Vendors/jquery.fancybox.js",
                        "~/Scripts/Vendors/jquery.fancybox-media.js",
                        "~/Scripts/Vendors/loading-bar.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/spa").Include(
                        "~/Scripts/spa/modules/common.core.js",
                        "~/Scripts/spa/modules/common.ui.js",
                        "~/Scripts/spa/app.js",
                        "~/Scripts/spa/services/apiService.js",
                        "~/Scripts/spa/services/notificationService.js",
                        "~/Scripts/spa/services/membershipService.js",
                        "~/Scripts/spa/services/fileUploadService.js",
                        "~/Scripts/spa/layout/topBar.directive.js",
                        "~/Scripts/spa/layout/sideBar.directive.js",
                        "~/Scripts/spa/layout/customPager. directive.js",
                        "~/Scripts/spa/directives/rating.directive.js",
                        "~/Scripts/spa/directives/availableMovie.directive.js",
                        "~/Scripts/spa/account/loginCtrl.js",
                        "~/Scripts/spa/account/registerCtrl.js",
                        "~/Scripts/spa/home/rootCtrl.js",
                        "~/Scripts/spa/home/indexCtrl.js",
                        "~/Scripts/spa/customers/customersCtrl.js",
                        "~/Scripts/spa/customers/customersRegCtrl.js",
                        "~/Scripts/spa/customers/customerEditCtrl.js",
                        "~/Scripts/spa/movies/moviesCtrl.js",
                        "~/Scripts/spa/movies/movieAddCtrl.js",
                        "~/Scripts/spa/movies/movieDetailsCtrl.js",
                        "~/Scripts/spa/movies/movieEditCtrl.js",
                        "~/Scripts/spa/controllers/rentalCtrl.js",
                        "~/Scripts/spa/rental/rentMovieCtrl.js",
                        "~/Scripts/spa/rental/rentStatsCtrl.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/content/css/site.css",
                    "~/content/css/bootstrap.min.css",
                    "~/content/css/bootstrap-theme.min.css",
                    "~/content/css/font-awesome.css",
                    "~/content/css/morris.css",
                    "~/content/css/toastr.css",
                    "~/content/css/jquery.fancybox.css",
                    "~/content/css/jk-rating-stars.min.css",
                    "~/content/css/loading-bar.min.css"));
            BundleTable.EnableOptimizations = false;        
        }
    }
}