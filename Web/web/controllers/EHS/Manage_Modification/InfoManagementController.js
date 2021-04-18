define(['myapp', 'angular'], function (myapp) {
    myapp.controller('InfoManagementController', ['ModificationService', '$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', '$upload', '$translatePartialLoader', '$translate', 'GateGuest', '$timeout',
        function (ModificationService, $scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, $upload, $translatePartialLoader, $translate, GateGuest, $timeout) {
            var lang = window.localStorage.lang;
            $scope.flowkey = "GateContractorInfoProcess";
            $scope.query = {};
            $scope.viewProcess='555111';

            EngineApi.getTcodeLink().get({ 'userid': Auth.username, 'tcode': 'ModificationProcess_EHS' }, function (linkres) {
                if (linkres.IsSuccess) {
                    $scope._isHSE = '';
                    $scope.viewProcess='hse';
                } else {
                    $scope._isHSE = Auth.username
                }
            })

            var params={}
            params.DepartmentID = "";
            params.EmployeeID = Auth.username;
            ModificationService.GetEmployeeInfo().get(params).$promise.then(function (res) {
                $scope.UserCostCenter = res[0].DepartmentID;
                console.log($scope.UserCostCenter)
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            var colModification = [
                {
                    field: 'MD_ProjectID',
                    width: 150,
                    displayName: $translate.instant('MD_ProjectID'),
                    minWidth: 100, cellTooltip: true
                },
                {
                    field: 'MD_Name',
                    width: 300,
                    displayName: $translate.instant('MD_Name'),
                    cellTooltip: true,
                    cellClass: function (grid, row, col, rowIndex, colIndex) { return 'text-left' }
                },
                {
                    field: 'MD_StatusName',
                    displayName: $translate.instant('5VEXXHR041'),
                    width: 160,
                    minWidth: 80, cellTooltip: true,
                    cellTemplate: `
                    <a ng-click="grid.appScope.GetMDModel(row.entity)" style="padding:5px;display:block;cursor:pointer">
                    {{COL_FIELD}}</a> `
                },
                {
                    field: 'Risk_StatusName',
                    displayName: $translate.instant('5VEXXHR042'),
                    width: 160,
                    minWidth: 100,
                    cellTemplate: `
                    <div ng-if="grid.appScope.btnRisk(row.entity)" class="ui-grid-cell-contents">
                        <button type="button" class="btn btn-small btn-success"
                            ng-click='grid.appScope.GetRiskModel(row.entity)' 
                            ng-disabled='grid.appScope.checkCreator(row.entity)'
                            style="margin-top: -3px">
                            <span class="glyphicon glyphicon-plus-sign"></span>
                            {{ 'Create' | translate }}
                        </button>
                    </div>  
                    <a ng-if="row.entity.Risk_Status && row.entity.Risk_Status!='X'" ng-click="grid.appScope.GetRiskModel(row.entity)" style="padding:5px;display:block; cursor:pointer">
                        <div class="ui-grid-cell-contents" ng-bind-html="grid.appScope.showStatus(row.entity,'Risk')"></div>
                    </a>`
                },
                {
                    field: 'BE_StatusName',
                    displayName: $translate.instant('5VEXXHR045'),
                    width: 160,
                    minWidth: 100, cellTooltip: true,
                    cellTemplate: `
                    <div ng-if="grid.appScope.btnBeforeEvaluate(row.entity)" class="ui-grid-cell-contents">
                        <button type="button" class="btn btn-success"
                            ng-click='grid.appScope.GetBEModel(row.entity)'
                            ng-disabled='grid.appScope.checkCreator(row.entity)'
                            style="margin-top: -3px;">
                            <span class="glyphicon glyphicon-plus-sign"></span>
                            {{ 'Create' | translate }}
                        </button>
                    </div>
                    <a ng-if="row.entity.BE_Status && row.entity.BE_Status!='X'" ng-click="grid.appScope.GetBEModel(row.entity)" style="padding:5px;display:block; cursor:pointer">
                        <div class="ui-grid-cell-contents" ng-bind-html="grid.appScope.showStatus(row.entity,'Evaluate')"></div>
                    </a>`
                },
                {
                    field: 'Close_StatusName',
                    displayName: $translate.instant('5VEXXHR046'),
                    width: 160,
                    minWidth: 80, cellTooltip: true,
                    cellTemplate: `
                    <div ng-if="grid.appScope.btnCloseEvaluate(row.entity)" class="ui-grid-cell-contents">
                        <button type="button" class="btn btn-success"
                            ng-click='grid.appScope.GetCEModel(row.entity)'
                            ng-disabled='grid.appScope.checkCreator(row.entity)'
                            style="margin-top: -3px;">
                            <span class="glyphicon glyphicon-plus-sign"></span>
                            {{ 'Create' | translate }}
                        </button>
                    </div>
                    <a ng-if="row.entity.Close_Status && row.entity.Close_Status!='X'" ng-click="grid.appScope.GetCEModel(row.entity)" style="padding:5px;display:block; cursor:pointer">
                        <div class="ui-grid-cell-contents" ng-bind-html="grid.appScope.showStatus(row.entity,'Close')"></div>
                    </a>`
                },
                {
                    field: 'MD_CreateDate',
                    width: 140,
                    displayName: $translate.instant('Create_date'),
                    cellTooltip: true,
                    cellClass: function (grid, row, col, rowIndex, colIndex) { return 'text-left' }
                }
                ,
                {
                    field: 'Creator',
                    width: 140,
                    displayName: $translate.instant('Creator'),
                    cellTooltip: true,
                    cellClass: function (grid, row, col, rowIndex, colIndex) { return 'text-left' }
                }
            ];

            $scope.gridOptions = {
                columnDefs: colModification,
                data: [],
                enableFiltering: true,
                enableColumnResizing: false,
                enableFullRowSelection: false,
                enableSorting: false,
                showGridFooter: false,
                enableGridMenu: true,
                exporterMenuPdf: false,
                enableSelectAll: false,
                enableRowHeaderSelection: true,
                enableRowSelection: false,
                multiSelect: false,
                paginationPageSizes: [50, 100, 200, 500],
                paginationPageSize: 100,
                enableFiltering: false,
                exporterOlderExcelCompatibility: true,
                rowHeight: 40,
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    EngineApi.getTcodeLink().get({ 'userid': Auth.username, 'tcode': $scope.flowkey }, function (linkres) {
                        if (linkres.IsSuccess) {
                            gridApi.core.addToGridMenu(gridApi.grid);
                        }
                    })
                }
            }

            $scope.checkCreator = function (value) {
                if (value.CostCenter.substr(0, 6) == $scope.UserCostCenter.substr(0, 6)) {
                    return false
                } else {
                    return true
                }

            }

            $scope.searchProcess = function () {
                ModificationService.GetProcess().get({
                    lang: lang,
                    ProjectID: $scope.query.MD_ProjectID || '',
                    UserID: $scope._isHSE,
                    FromDate: $scope.query.FromDate || '',
                    ToDate: $scope.query.ToDate || '',
                    AppID: ''
                }).$promise.then(function (res) {
                    console.log(res)
                    $scope.gridOptions.data = res;
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }


            $scope.btnRisk = function (a) {
                if (a.MD_Status == "Q" && (a.Risk_AppID == null || a.Risk_Status == "X")) {
                    return true;
                }
            };

            $scope.btnBeforeEvaluate = function (a) {
                if ((a.Risk_Status == "Q" || a.Risk_Status == "E") && (a.Evaluation_AppID == null || a.BE_Status == "X")) {
                    return true;
                }
            };

            $scope.btnCloseEvaluate = function (a) {
                if (a.BE_Status == "Q" && (a.Closing_AppID == null || a.Close_Status == "X")) {
                    return true;
                }
            };

            $scope.showStatus = function (row, column) {
                if (column == 'Risk') {
                    if (row.Risk_Status == null || row.Risk_Status == "X") {
                        return null;
                    } else {
                        return row.Risk_StatusName;
                    }
                }

                if (column == 'Evaluate') {
                    if (row.BE_Status == null || row.BE_Status == "X") {
                        return null;
                    } else {
                        return row.BE_StatusName;
                    }
                }

                if (column == 'Close') {
                    if (row.Close_Status == null || row.Close_Status == "X") {
                        return null;
                    } else {
                        return row.Close_StatusName;
                    }
                }
            };

            $scope.GetMDModel = function (value) {
                if (value.MD_Status == "N" && value.MD_Creator == Auth.username) {
                    $location.url('/Manage_Modification/Modification/Update/' + value.MD_AppID)
                } else if (value.MD_Status == "F") {
                    $scope.getProcessModificationOrRiskLog(value.MD_AppID)
                } else {
                    var href = '#/Manage_Modification/Modification/Print/' + value.MD_AppID
                    window.open(href)
                }
            }

            $scope.GetRiskModel = function (value) {
                if (value.Risk_Status == "N" && value.Risk_Creator == Auth.username) {
                    $location.url('/Manage_Modification/RiskAssessment/Update/' + value.Risk_AppID);
                } else if (value.Risk_Status == null || value.Risk_Status == "X") {
                    $location.url('/Manage_Modification/RiskAssessment/Create/' + value.MD_AppID);
                }  else if (value.Risk_Status == "F") {
                    $scope.getProcessModificationOrRiskLog(value.Risk_AppID)
                }else {
                    var href = '#/Manage_Modification/RiskAssessment/Print/' + value.Risk_AppID
                    window.open(href);
                }
            }

            $scope.GetBEModel = function (value) {
                if (value.BE_Status == "N" && value.BE_Creator == Auth.username) {
                    $location.url('/Manage_Modification/BeforeOperateEvaluation/Update/' + value.Evaluation_AppID);
                } else if (value.BE_Status == null || value.BE_Status == "X") {
                    $location.url('/Manage_Modification/BeforeOperateEvaluation/Create/' + value.Risk_AppID);
                }  else if (value.Risk_Status == "F") {
                    $scope.getProcessEvaluationOrClosingLog(value.Evaluation_AppID)
                }else {
                    var href = '#/Manage_Modification/BeforeOperateEvaluation/Print/' + value.Evaluation_AppID
                    window.open(href);
                }
            }

            $scope.GetCEModel = function (value) {
                if (value.Close_Status == "N" && value.Close_Creator == Auth.username) {
                    $location.url('/Manage_Modification/ClosingEvaluation/Update/' + value.Closing_AppID);
                } else if (value.Close_Status == null || value.Close_Status == "X") {
                    $location.url('/Manage_Modification/ClosingEvaluation/Create/' + value.Evaluation_AppID);
                }  else if (value.Risk_Status == "F") {
                    $scope.getProcessEvaluationOrClosingLog(value.Closing_AppID)
                }else {
                    var href = '#/Manage_Modification/ClosingEvaluation/Print/' + value.Closing_AppID
                    window.open(href);
                }
            }

            $scope.getProcessModificationOrRiskLog = function (AppID) {
                ModificationService.GetModificationAndRiskPID().get({
                    VoucherID: AppID
                }).$promise.then(function (res) {
                    console.log(res);
                    if (res) {
                        window.open('#/Modification/processlog/'+$scope.viewProcess+'/' + res.ProcessInstanceId);
                    }
                }, function (err) {
                    Notifications.addError({
                        'status': 'error',
                        'message': err.data
                    });
                })

            }

            $scope.getProcessEvaluationOrClosingLog = function (AppID) {
                ModificationService.GetEvaluationAndClosingPID().get({
                    VoucherID: AppID
                }).$promise.then(function (res) {
                    console.log(res);
                    if (res) {
                        window.open('#/Modification/processlog/'+$scope.viewProcess+'/' + res.ProcessInstanceId);
                    }
                }, function (err) {
                    Notifications.addError({
                        'status': 'error',
                        'message': err.data
                    });
                })
            }
        }
    ]);

    myapp.controller('Manage_ModificationController', ['ModificationService', '$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', '$upload', '$translatePartialLoader', '$translate', 'GateGuest', '$timeout',
        function (ModificationService, $scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, $upload, $translatePartialLoader, $translate, GateGuest, $timeout) {
            $scope.flowkey = "GateContractorInfoProcess";
            var lang = window.localStorage.lang;
            $scope._isHSE = false;
            EngineApi.getTcodeLink().get({ 'userid': Auth.username, 'tcode': 'ModificationProcess_EHS' }, function (linkres) {
                if (linkres.IsSuccess) {
                    $scope._isHSE = true;
                }
            })

            ModificationService.CountMDProject().get().$promise.then(function (res) {
                $scope.data = res[0];
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });
        }
    ]);
})
