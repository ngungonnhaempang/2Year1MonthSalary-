define(['myapp', 'angular'], function (myapp) {
    myapp.controller('BeforeOperateEvaluationController', ['ModificationService', '$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', '$upload', '$translatePartialLoader', '$translate', 'GateGuest', '$timeout',
        function (ModificationService, $scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, $upload, $translatePartialLoader, $translate, GateGuest, $timeout) {
            var lang = window.localStorage.lang;
            $scope.query = {};
            $scope.data = {};
            $scope.viewProcess='555111';
            $scope.Language = window.localStorage.lang;
            
            EngineApi.getTcodeLink().get({ 'userid': Auth.username, 'tcode': 'ModificationProcess_EHS' }, function (linkres) {
                if (linkres.IsSuccess) {
                    $scope._isHSE = '';
                    $scope.viewProcess='hse'
                } else {
                    $scope._isHSE = Auth.username;
                }
            })

            ModificationService.GetDeparment().get({ language: lang }).$promise.then(function (res) {
                $scope.DepartmentList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            ModificationService.GetStatus().get({ type: "Application", language: lang }).$promise.then(function (res) {
                $scope.StatusList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            })

            $scope.searchEvaluateApp = function () {
                ModificationService.SearchBeforeOperatingEvaluation().get({
                    Lang: lang,
                    MD_ProjectID: $scope.query.MD_ProjectID || '',
                    Name: $scope.query.MD_Name || '',
                    Department: $scope.query.DepartmentID || '',
                    Status: $scope.query.Status || '',
                    FromDate: $scope.query.FromDate || '',
                    ToDate: $scope.query.ToDate || '',
                    UserID: $scope._isHSE
                }).$promise.then(function (res) {
                    $scope.gridOptions.data = res;
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            $scope.getTranslatedCol = function (id) {
                return $translate.instant($scope.ResultList.find(item => item.id === id).name);
            };

            $scope.ResultList = [{
                id: 'Yes',
                name: $translate.instant('Allowed')
            },
            {
                id: 'No',
                name: $translate.instant('Not_Allowed')
            }];

            var colEvaluation = [
                {
                    field: 'MD_ProjectID',
                    width: 180,
                    displayName: $translate.instant('MD_ProjectID'),
                    minWidth: 100,
                    cellTemplate: "<a ng-click='grid.appScope.getProcessEvaluationLog(row)' style='padding:5px;display:block; cursor:pointer'>{{COL_FIELD}}</a>",
                },
                {
                    field: 'StatusName',
                    displayName: $translate.instant('Status'),
                    width: 140,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'Specification',
                    displayName: $translate.instant('Dept_Proposed'),
                    width: 260,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'MD_Name',
                    displayName: $translate.instant('MD_Name'),
                    width: 290,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'Result',
                    displayName: $translate.instant('Result_Evaluate'),
                    width: 200,
                    minWidth: 80,
                    cellTemplate: '<span class="grid_cell_ct">{{grid.appScope.getTranslatedCol(row.entity.Result)}}</span>'
                },
                {
                    field: 'CreateDate',
                    displayName: $translate.instant('Evalua_date'),
                    width: 150,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'Creator',
                    displayName: $translate.instant('Creator'),
                    width: 140,
                    minWidth: 80
                }
            ];

            $scope.gridOptions = {
                columnDefs: colEvaluation,
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
                    EngineApi.getTcodeLink().get({ 'userid': Auth.username, 'tcode': 'ModificationProcess' }, function (linkres) {
                        if (linkres.IsSuccess) {
                            gridApi.core.addToGridMenu(gridApi.grid, gridMenu);
                        }else{
                            EngineApi.getTcodeLink().get({ 'userid': Auth.username, 'tcode': 'ModificationProcessQuery' }, function (linkres) {
                                if (linkres.IsSuccess) {
                                    gridApi.core.addToGridMenu(gridApi.grid, gridMenuViewDetail);
                                }
                            })

                        }
                    })
                }
            };

            var gridMenu = [{
                title: $translate.instant('Create'),
                icon: 'ui-grid-icon-plus-squared',
                action: function () {
                    $location.url('/Manage_Modification/BeforeOperateEvaluation/Create/null');
                },
                order: 1
            }, {
                title: '?????? ' + $translate.instant('Update'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    if (resultRows.length == 1) {
                        row = resultRows[0]
                        if (row.Status == 'N') {
                            if (row.Creator != Auth.username) {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': $translate.instant('Update_owner_MSG')
                                });
                                return;
                            }
                            else {
                                $location.url('/Manage_Modification/BeforeOperateEvaluation/Update/' + resultRows[0].Evaluation_AppID);
                            }
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant('Edit_Draf_MSG') });
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                },
                order: 2
            },
            {
                title: '??????? ' + $translate.instant('Print_voucher'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    if (resultRows.length == 1) {
                        if (resultRows[0].Status != "X") {
                            var href = '#/Manage_Modification/BeforeOperateEvaluation/Print/' + resultRows[0].Evaluation_AppID
                            window.open(href);
                        } else {
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant('PrintVoucherDelete_MSG')
                            });
                        }
                    } else {
                        Notifications.addError({
                            'status': 'error',
                            'message': $translate.instant('Select_ONE_MSG')
                        });
                    }
                },
                order: 3
            },
            {
                title: '??? ' + $translate.instant('Delete'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    if (resultRows.length == 1) {
                        if (resultRows[0].Status == 'N') {
                            if (resultRows[0].Creator == Auth.username || $scope._isHSE == '') {
                                $scope.itemDelete = {
                                    Name: resultRows[0].MD_ProjectID,
                                    ID: resultRows[0].Evaluation_AppID,
                                    Status: 'X'
                                };
                                $('#ConfirmDeleteModal').modal('show');
                            }
                            else {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': $translate.instant('Delete_owner_MSG')
                                });
                                return;
                            }
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant('Delete_Draf_Msg') });
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                }, order: 4
            },
            {
                title: '??? ' + $translate.instant('SPDelete'),
                shown: function () {
                    return $scope._isHSE == '';
                },
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    if (resultRows.length == 1) {
                        if (resultRows[0].Status == 'Q') {
                            ModificationService.CheckForeignKey().get({
                                Table: 'ClosingApplication',
                                AppID: resultRows[0].Evaluation_AppID
                            }).$promise.then(function (res) {
                                if (!res.Closing_AppID) {
                                    $scope.itemDelete = {
                                        Name: resultRows[0].MD_ProjectID,
                                        ID: resultRows[0].Evaluation_AppID,
                                        Status: "SPD",
                                        SpecialDeleteReason: ""
                                    };
                                } else {
                                    if (res.Status == "Q") {
                                        $scope.itemDelete = {
                                            Name: resultRows[0].MD_ProjectID,
                                            ID: null,
                                            Close: true
                                        };
                                    } else {
                                        $scope.itemDelete = {
                                            Name: resultRows[0].MD_ProjectID,
                                            ID: null,
                                            Evaluation_AppID: null,
                                            Closing_AppID: res.Closing_AppID,
                                            Close: false
                                        };

                                    }

                                }
                                $('#ConfirmDeleteModal').modal('show');
                            }, function (errResponse) {
                                Notifications.addError({ 'status': 'error', 'message': errResponse });
                            });

                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant('OnlyDeleteSigned_MSG') });
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                }, order: 5
            }];

            var gridMenuViewDetail = [
                {
                    title: '???? ' + $translate.instant('Detail'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            if (resultRows[0].Status != "X") {
                                var href = '#/Manage_Modification/BeforeOperateEvaluation/Print/' + resultRows[0].Evaluation_AppID
                                window.open(href);
                            } else {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': $translate.instant('PrintVoucherDelete_MSG')
                                });
                            }
                        } else {
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant('Select_ONE_MSG')
                            });
                        }
                    },
                    order: 1
            }];


            $scope.deleteItem = function (_appID, _status, _reason) {
                var query = {}
                query.Evaluation_AppID = _appID
                query.status = _status
                query.deletePerson = Auth.username
                query.specialDeleteReason = _reason || ''
                ModificationService.UpdateBEStatus().save(query, {}).$promise.then(function (res) {
                    console.log(res);
                    sendMailSpecialDelete(_appID, _status, function (err) {
                        $scope.searchEvaluateApp();
                        $('#ConfirmDeleteModal').modal('hide');
                        $timeout(function () {
                            Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("Delete_Succeed_Msg") });
                        }, 400);
                    })
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                })
            }

            function sendMailSpecialDelete(_AppID, _status, callback) {
                if (_status == 'SPD') {
                    ModificationService.SendMailSpecialDelete().post({
                        VoucherID: _AppID,
                        VoucherType: "BE"
                    }, {}).$promise.then(function (res) {
                        callback('')
                    }, function (err) {
                        callback(err)
                    });
                } else {
                    callback('')
                }
            }


            $scope.getFlowDefinitionId = function (keyname, callback) {
                EngineApi.getKeyId().getkey({
                    'key': keyname
                }, function (res) {
                    callback(res.id);
                });
            }

            $scope.startflowid = function (definitionID, businessKey, formVariables, historyVariable, callback) {
                var variablesMap = Forms.variablesToMap(formVariables)
                var historyVariableMap = Forms.variablesToMap(historyVariable)
                var datafrom = {
                    formdata: variablesMap,
                    businessKey: businessKey,
                    historydata: historyVariableMap
                };
                console.log(datafrom);
                EngineApi.doStart().start({
                    'id': definitionID
                }, datafrom, function (res) {
                    console.log(res);
                    if (res.result && !res.message) {
                        callback(res.url, '');
                    } else {
                        callback('', res.message)
                    }
                }, function (errResponse) {
                    callback('', errResponse)
                })
            }

            $scope.getProcessEvaluationLog = function (obj) {
                debugger
                var _VoucherID = obj.entity.Evaluation_AppID;
                ModificationService.GetEvaluationAndClosingPID().get({
                    VoucherID: _VoucherID
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
})