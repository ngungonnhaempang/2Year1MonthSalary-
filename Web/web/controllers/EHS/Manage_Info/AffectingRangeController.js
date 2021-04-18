define(['myapp', 'angular'], function (myapp) {
    myapp.controller('AffectingRangeController', ['ModificationService', '$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', '$upload', '$translatePartialLoader', '$translate', 'GateGuest', '$timeout',
        function (ModificationService, $scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, $upload, $translatePartialLoader, $translate, GateGuest, $timeout) {
            var lang = window.localStorage.lang;
            $scope.flowkey = "ModificationProcess_EHS";
            $scope.query = {};
            $scope.data = {};
            $scope._isUpgrade = false;
            $scope._DateDisabled = false;
            $scope._isUpdate = false;

            $scope.resetRangeModal = function () {
                $scope.data = {};
                $scope._isUpgrade = false;
                $scope._DateDisabled = false;
                $scope._isUpdate = false;
                $scope.searchRange();
            }

            ModificationService.GetStatus().get({ type: "Info", language: lang }).$promise.then(function (res) {
                $scope.StatusList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            })

            var col = [
                {
                    field: 'AffectRangeCode',
                    width: 130,
                    displayName: $translate.instant('Range_code'),
                    minWidth: 100, cellTooltip: true
                },
                {
                    field: 'Version',
                    displayName: $translate.instant('Version'),
                    width: 100,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'AffectRangeContent',
                    displayName: $translate.instant('Name_AffRange'),
                    width: 310,
                    minWidth: 110,
                    cellTooltip: true
                },
                {
                    field: 'Index',
                    displayName: $translate.instant('Position'),
                    width: 80,
                    minWidth: 110,
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
                    field: 'Description',
                    displayName: $translate.instant('Remark'),
                    width: 220,
                    minWidth: 110,
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

            var gridMenu = [{
                title: $translate.instant('Create'),
                icon: 'ui-grid-icon-plus-squared',
                action: function () {
                    $scope.getMaxIndex();
                    $scope.data.Version = 1;
                    $('#modalAffectingRange').modal('show');
                },
                order: 1
            }, {
                title: '✏️ ' + $translate.instant('Update'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    if (resultRows.length == 1) {
                        if (resultRows[0].Status == 'A') {
                            $scope.checkCode(resultRows[0], function (_hasUpdate) {
                                if (!_hasUpdate) {
                                    $scope._isUpdate = true;
                                    $scope.data = resultRows[0];
                                    $scope.data.Index = parseInt(resultRows[0].Index)
                                    $scope._DateDisabled = true;
                                    $('#modalAffectingRange').modal('show');
                                }
                            });
                        } if (resultRows[0].Status == 'UAP') {
                            $scope._isUpdate = true;
                            $scope.data = resultRows[0];
                            $scope.data.Index = parseInt(resultRows[0].Index)
                            $('#modalAffectingRange').modal('show');
                        }
                        else if (resultRows[0].Status == 'UA') {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Edit_StatusNotApply_MSG") });
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
                            ModificationService.SaveRange().save({}, params).$promise.then(function (res) {
                                console.log(res);
                                $scope.searchRange();
                                $timeout(function () {
                                    Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("Delete_Succeed_Msg") });
                                }, 400);
                            }, function (errResponse) {
                                Notifications.addError({ 'status': 'error', 'message': errResponse });
                            });
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Delete_NotApply_Msg") });
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                }, order: 3
            }, {
                title: '📤 ' + $translate.instant('UpgradeVersion'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    if (resultRows.length == 1) {
                        if (resultRows[0].Status == 'A') {
                            $scope.checkCode(resultRows[0], function (_hasUpgrade) {
                                if (!_hasUpgrade) {
                                    $scope._isUpgrade = true;
                                    $scope.data = resultRows[0];
                                    $scope.data.Index = parseInt(resultRows[0].Index)
                                    $scope.data.Version = $scope.data.Version + 1;
                                    $scope.data.StartDate = $filter('date')(new Date(), 'yyyy-MM-dd');
                                    $('#modalAffectingRange').modal('show');
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

            $scope.searchRange = function () {
                ModificationService.SearchRange().get({
                    Language: lang,
                    AffectRangeCode: $scope.query.AffectRangeCode || '',
                    AffectRangeContent: $scope.query.AffectRangeContent || '',
                    Status: $scope.query.Status || '',
                    FromDate: $scope.query.FromDate || '',
                    ToDate: $scope.query.ToDate || ''
                }).$promise.then(function (res) {
                    $scope.gridOptions.data = res;
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            $scope.saveRange = function () {
                var params = {}
                if ($scope._isUpgrade) {
                    params.Status = "Upgrade"
                    params.UpgradeReason = $scope.data.UpgradeReason
                } else params.Status = $scope.data.Status || null
                params.AffectRangeID = $scope.data.AffectRangeID || null
                params.AffectRangeCode = $scope.data.AffectRangeCode || null
                params.AffectRangeContent = $scope.data.AffectRangeContent;
                params.StartDate = $scope.data.StartDate
                params.EndDate = $scope.data.EndDate
                params.Version = $scope.data.Version || null
                params.ModifyReason = $scope.data.ModifyReason || null
                params.Index = $scope.data.Index
                params.Remark = $scope.data.Description || null

                ModificationService.SaveRange().save({}, params).$promise.then(function (res) {
                    console.log(res);
                    $('#modalAffectingRange').modal('hide');
                    $scope.resetRangeModal();
                    $timeout(function () {
                        Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("Save_Success_MSG") });
                    }, 400);
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            $scope.checkCode = function (data, callback) {
                ModificationService.CheckExistCode().get({
                    Table: "MD_AffectingRange",
                    Code: data.AffectRangeCode,
                    Version: data.Version + 1
                }).$promise.then(function (res) {
                    console.log(res);
                    if (res.AffectRangeCode) {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Version_isUpgrade_MSG") });
                        callback(true);
                    } else {
                        callback(false);
                    }
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            $scope.getMaxIndex = function () {
                ModificationService.SearchRange().get({
                    Language: lang,
                    AffectRangeCode: '',
                    AffectRangeContent: '',
                    Status: '',
                    FromDate: '',
                    ToDate: ''
                }).$promise.then(function (res) {
                    if (res.length > 0) {
                        var sortList = $filter('orderBy')(res, 'Index', true)
                        var maxId = sortList[0].Index;
                        $scope.data.Index = parseInt(maxId) + 1;
                    } else $scope.data.Index = 1;
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

        }
    ]);
})
