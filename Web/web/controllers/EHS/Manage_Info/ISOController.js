define(['myapp', 'angular'], function (myapp) {
    myapp.controller('ISOController', ['ModificationService', '$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', '$upload', '$translatePartialLoader', '$translate', 'GateGuest', '$timeout',
        function (ModificationService, $scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, $upload, $translatePartialLoader, $translate, GateGuest, $timeout) {
            var lang = window.localStorage.lang;
            $scope.flowkey = "ModificationProcess_EHS";
            $scope.query = {};
            $scope.data = {};
            $scope._AppTypeDisabled = true;
            $scope._isUpgrade = false;
            $scope._DateDisabled = false;
            $scope._IsExist = false;
            $scope._isUpdate = false;

            $scope.resetISOModal = function () {
                $scope.data = {};
                $scope._AppTypeDisabled = true;
                $scope._isUpgrade = false;
                $scope._DateDisabled = false;
                $scope._IsExist = false;
                $scope._isUpdate = false;
                $scope.searchISO();
            }

            ModificationService.GetStatus().get({ type: "Info", language: lang }).$promise.then(function (res) {
                $scope.StatusList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            ModificationService.GetAppicationType().get({ table: "MD_ApplicationType", language: lang }).$promise.then(function (res) {
                $scope.ApplicationTypeList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            var col = [
                {
                    field: 'AppTypeName',
                    displayName: $translate.instant('VoucherType'),
                    width: 320,
                    minWidth: 100,
                },
                {
                    field: 'Version',
                    displayName: $translate.instant('Version'),
                    width: 120,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'ISO_AppCode',
                    displayName: $translate.instant('ISO_AppCode'),
                    width: 150,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'StatusName',
                    displayName: $translate.instant('Status'),
                    width: 140,
                    minWidth: 100,
                },
                {
                    field: 'StartDate',
                    displayName: $translate.instant('MD_StartDate'),
                    width: 220,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'EndDate',
                    displayName: $translate.instant('MD_EndDate'),
                    width: 180,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'ModifyReason',
                    displayName: $translate.instant('Reason_edit'),
                    width: 220,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'UpgradeReason',
                    displayName: $translate.instant('Reason_upgrade'),
                    width: 180,
                    minWidth: 80,
                    cellTooltip: true
                }
            ];

            $scope.gridOptions = {
                columnDefs: col,
                data: [],
                enableFiltering: true,
                enableColumnResizing: true,
                enableFullRowSelection: true,
                enableSorting: true,
                showGridFooter: true,
                enableGridMenu: true,
                exporterMenuPdf: false,
                enableSelectAll: false,
                enableRowHeaderSelection: true,
                enableRowSelection: true,
                multiSelect: false,
                paginationPageSizes: [50, 100, 200, 500],
                paginationPageSize: 100,
                enableFiltering: false,
                exporterOlderExcelCompatibility: true,
                useExternalPagination: true,
                enablePagination: true,
                enablePaginationControls: true,
                showGridFooter: true,
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    EngineApi.getTcodeLink().get({ 'userid': Auth.username, 'tcode': $scope.flowkey }, function (linkres) {
                        if (linkres.IsSuccess) {
                            gridApi.core.addToGridMenu(gridApi.grid, gridMenu);
                        }
                    })
                }
            };

            var gridMenu = [
                // {
                //     title: $translate.instant('Create'),
                //     icon: 'ui-grid-icon-plus-squared',
                //     action: function () {
                //         $scope._AppTypeDisabled = false;
                //         $scope.data.Version = 1;
                //         $('#modalISO').modal('show');
                //     },
                //     order: 1
                // },
                {
                    title: '✏️ ' + $translate.instant('Update'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            if (resultRows[0].Status == 'A') {
                                $scope.checkCode(resultRows[0], function (_hasUpdate) {
                                    if (!_hasUpdate) {
                                        $scope._isUpdate = true;
                                        $scope.data = resultRows[0];
                                        $scope._DateDisabled = true;
                                        $('#modalISO').modal('show');
                                    }
                                });
                            } if (resultRows[0].Status == 'UAP') {
                                $scope._isUpdate = true;
                                $scope.data = resultRows[0];
                                $('#modalISO').modal('show');
                            }
                            else if (resultRows[0].Status == 'UA') {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant("Edit_StatusApply_MSG") });
                            }
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                        }
                    },
                    order: 2
                },
                {
                    title: '❌ ' + $translate.instant('Delete'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            if (resultRows[0].Status == 'UAP') {
                                var params = {};
                                params = resultRows[0];
                                params.Status = 'X';
                                ModificationService.SaveISO_Application().save({}, params).$promise.then(function (res) {
                                    console.log(res);
                                    $scope.searchISO();
                                    $timeout(function () {
                                        Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("Delete_Succeed_Msg") });
                                    }, 400);
                                }, function (errResponse) {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                });
                            }else {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant("Delete_NotApply_Msg") });
                            }
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                        }
                    }, order: 3
                },
                {
                    title: '📤 ' + $translate.instant('UpgradeVersion'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            if (resultRows[0].Status == 'A') {
                                $scope.checkCode(resultRows[0], function (_hasUpgrade) {
                                    if (!_hasUpgrade) {
                                        $scope._isUpgrade = true;
                                        $scope.data = resultRows[0];
                                        $scope.data.Version = $scope.data.Version + 1;
                                        $scope.data.ISO_AppCode = $scope.data.ApplicationType + '-0' + $scope.data.Version
                                        $scope.data.StartDate = $filter('date')(new Date(), 'yyyy-MM-dd');
                                        $('#modalISO').modal('show');
                                    }
                                });
                            } else {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant("UpgradeVersion_StatusApply_MSG") });
                            }
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                        }

                    },
                    order: 4
                }];

            $scope.searchISO = function () {
                ModificationService.SearchISO().get({
                    Language: lang,
                    code: $scope.query.ISO_AppCode || '',
                    Status: $scope.query.Status || '',
                    ApplicationType: $scope.query.ApplicationType || '',
                    FromDate: $scope.query.FromDate || '',
                    ToDate: $scope.query.ToDate || ''
                }).$promise.then(function (res) {
                    $scope.gridOptions.data = res;
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            $scope.saveISO = function () {
                var params = {}
                if ($scope._isUpgrade) {
                    params.Status = "Upgrade"
                    params.UpgradeReason = $scope.data.UpgradeReason
                } else params.Status = $scope.data.Status || null
                params.ISO_ID = $scope.data.ISO_ID || null
                params.ISO_AppCode = $scope.data.ISO_AppCode || null
                params.StartDate = $scope.data.StartDate
                params.EndDate = $scope.data.EndDate
                params.Version = $scope.data.Version || null
                params.ApplicationType = $scope.data.ApplicationType
                params.ModifyReason = $scope.data.ModifyReason || null

                ModificationService.SaveISO_Application().save({}, params).$promise.then(function (res) {
                    console.log(res);
                    $('#modalISO').modal('hide');
                    $scope.resetISOModal();
                    $timeout(function () {
                        Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("Save_Success_MSG") });
                    }, 400);
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            $scope.checkCode = function (data, callback) {
                ModificationService.CheckExistCode().get({
                    Table: "MD_ISO_Application",
                    Code: data.ApplicationType,
                    Version: data.Version + 1
                }).$promise.then(function (res) {
                    console.log(res);
                    if (res.ISO_AppCode) {
                        //  $scope._IsExist = true;
                        callback(true)
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Version_isUpgrade_MSG") });
                    } else {
                        //  $scope._IsExist = false;
                        callback(false)
                    }
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });

            }
        }
    ]);
})
