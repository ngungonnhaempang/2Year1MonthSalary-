define(['app', 'angular', 'xlsx'], function (app, angular) {
    app.directive('getProject', ['ModificationService', '$resource', '$http', '$filter', 'Notifications', '$routeParams', '$translate', '$timeout', 'GateGuest', 'EngineApi', '$location', 'Auth', 'Forms', '$anchorScroll', '$route', '$window',
        function (ModificationService, $resource, $http, $filter, Notifications, $routeParams, $translate, $timeout, GateGuest, EngineApi, $location, Auth, Forms, $anchorScroll, $route, $window) {
            return {
                restrict: 'E',

                controller: function ($scope, $rootScope) {
                    var lang = window.localStorage.lang;
                    $scope.data = {};
                    $scope.query={};
                    
                    var col = [
                        {
                            field: 'MD_ProjectID',
                            width: 220,
                            displayName: $translate.instant('MD_ProjectID'),
                            minWidth: 100, cellTooltip: true,
                            cellTemplate: "<a ng-click='grid.appScope.selectProject(row)' style='padding:5px;display:block; cursor:pointer'>{{COL_FIELD}}</a>",

                        },
                        {
                            field: 'MD_Name',
                            displayName: $translate.instant('MD_Name'),
                            width: 400,
                            minWidth: 80,
                            cellTooltip: true
                        },
                        {
                            field: 'CreateDate',
                            displayName: $translate.instant('Create_date'),
                            width: 400,
                            minWidth: 80,
                            cellTooltip: true
                        }
                    ];
        
                    $scope.gridOptionsID = {
                        columnDefs: col,
                        data: [],
                        enableFiltering: true,
                        enableColumnResizing: true,
                        enableFullRowSelection: true,
                        enableSorting: true,
                        enableGridMenu: true,
                        exporterMenuPdf: false,
                        enableSelectAll: false,
                        enableRowHeaderSelection: true,
                        enableRowSelection: true,
                        multiSelect: true,
                        enableFiltering: false,
                        exporterOlderExcelCompatibility: true,
                        useExternalPagination: false,
                        enablePagination: false,
                        enablePaginationControls: false,
                        showGridFooter: true,
                    }

                    $scope.searchProjectID = function(){
                        var query = {};
                        query.VoucherType = $scope.VoucherType;
                        query.UserID = Auth.username;
                        query.MD_ID = $scope.query.MD_ProjectID ||'';
                        query.CreateDate = $scope.query.CreateDate || '';
                        ModificationService.GetModificationProjectID().get(query).$promise.then(function (res) {
                            $scope.gridOptionsID.data = res;
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }
                    
                    $scope.selectProject = function (obj) {
                        $('#modalProjectID').modal('hide')
                        $scope.data.MD_ProjectID = obj.entity.MD_ProjectID;
                        $scope.data.MD_AppID = obj.entity.MD_AppID;
                        $scope.data.MD_Name = obj.entity.MD_Name;
                        $scope.data.Risk_AppID = obj.entity.Risk_AppID;
                        $scope.data.Risk_CreateDate = obj.entity.CreateDate
                        $scope.data.Evaluation_AppID = obj.entity.Evaluation_AppID;
                        $scope.EvaluateRisk = obj.entity.ResultEvaluate;
                        if($scope.EvaluateRisk){
                            $scope.evaluatefirstCategory($scope.EvaluateRisk);
                        }
                        
                    }

                    $scope.resetModal = function () {
                        $scope.query = {};
                    }                    
                },
                templateUrl: './forms/EHS/Modification/Manage_Modification/SearchProjectID.html'
            }
        }]);
});
