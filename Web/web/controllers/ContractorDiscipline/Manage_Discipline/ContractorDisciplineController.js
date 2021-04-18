define(['myapp', 'angular'], function (myapp) {
    myapp.controller('ContractorDisciplineController', ['ConDisciplineService', 'ModificationService', '$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', '$upload', '$translatePartialLoader', '$translate', 'GateGuest', '$timeout',
        function (ConDisciplineService, ModificationService, $scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, $upload, $translatePartialLoader, $translate, GateGuest, $timeout) {
            var lang = window.localStorage.lang;
            $scope.query = {};
            $scope.data = {};
            $scope.user = '';
            $scope.printApp = function () {
                window.print();
            }

            EngineApi.getTcodeLink().get({ 'userid': Auth.username, 'tcode': 'GateContractorEHS' }, function (linkres) {
                if (linkres.IsSuccess) {
                    $scope.isHSE = true;
                } else {
                    $scope.isHSE = false;
                }
            })

            EngineApi.getTcodeLink().get({ 'userid': Auth.username, 'tcode': 'GateContractorGA' }, function (linkres) {
                if (linkres.IsSuccess) {
                    $scope.isGA = true;
                    $scope.user = 'GA'
                    if($routeParams.DisciplineCode!='Search'){
                        $scope.query.DisciplineCode = $routeParams.DisciplineCode
                        $scope.searchDiscipline();
                    }
                } else {
                    $scope.isGA = false;
                }
            })

            ConDisciplineService.GetStatus().get({ type: "DisciplineApp", language: lang }).$promise.then(function (res) {
                $scope.StatusList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            ModificationService.GetDeparment().get({ language: lang }).$promise.then(function (res) {
                $scope.DepartmentList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            $scope.getProcessDiscipline = function (obj) {
                debugger
                var _VoucherID = obj.entity.DisciplineID;
                ConDisciplineService.GetDisciplinePID().get({
                    VoucherID: _VoucherID
                }).$promise.then(function (res) {
                    console.log(res);
                    if (res) {
                        window.open('#/processlog/' + res.ProcessInstanceId);
                    }
                }, function (err) {
                    Notifications.addError({
                        'status': 'error',
                        'message': err.data
                    });
                })
            }

            var col = [
                {
                    field: 'DisciplineCode',
                    displayName: $translate.instant('VoucherID'),
                    width: 150,
                    minWidth: 100,
                    cellTemplate: '<a ng-click="grid.appScope.getProcessDiscipline(row)"  style="padding:5px;display:block; cursor:pointer">{{COL_FIELD}}</a>'

                },
                {
                    field: 'ContractorName',
                    displayName: $translate.instant('ContractorName'),
                    width: 320,
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
                    field: 'TimeViolation',
                    displayName: $translate.instant('TimeViolation'),
                    width: 200,
                    minWidth: 100,
                    cellFilter: "date: 'yyyy-MM-dd HH:mm:ss'"
                },
                {
                    field: 'LocationViolation',
                    displayName: $translate.instant('LocationViolation'),
                    width: 200,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'DeptName',
                    displayName: $translate.instant('ConQua_paraDepartment'),
                    width: 400,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'CreateDate',
                    displayName: $translate.instant('CreateDate'),
                    width: 130,
                    minWidth: 80
                },
                {
                    field: 'Creator',
                    displayName: $translate.instant('Creator'),
                    width: 130,
                    minWidth: 80
                },
                {
                    field: 'Remark',
                    displayName: $translate.instant('Remark'),
                    width: 220,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'ModifyReason',
                    displayName: $translate.instant('Reason_edit'),
                    width: 220,
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
                    EngineApi.getTcodeLink().get({ 'userid': Auth.username, 'tcode': 'GateContractorInfoProcessQuery' }, function (linkres) {
                        if (linkres.IsSuccess) {
                            gridApi.core.addToGridMenu(gridApi.grid, gridMenu);
                        }
                    })
                }
            };

            var gridMenu = [
                {
                    title: $translate.instant('Create'),
                    shown: function () {
                        return $scope.isHSE;
                    },
                    icon: 'ui-grid-icon-plus-squared',
                    action: function () {
                        $location.url('/ContractorDiscipline/Discipline/Create/null');
                    },
                    order: 1
                },
                {
                    title: '✏️ ' + $translate.instant('Update'),
                    shown: function () {
                        return $scope.isHSE;
                    },
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

                                    $scope.detail = false;
                                    $location.url('/ContractorDiscipline/Discipline/Update/' + row.DisciplineID);
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
                    title: '🖨️ ' + $translate.instant('Print_voucher'),
                    shown: function () {
                        return $scope.isHSE;
                    },
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            if (resultRows[0].Status != "X") {
                                var href = '#/ContractorDiscipline/Discipline/Print/' + resultRows[0].DisciplineID
                                window.open(href);
                            } else {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': $translate.instant('PrintVoucherDelete_MSG')
                                });
                            }
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant('Select_ONE_MSG') });
                        }
                    }, order: 3
                },
                {
                    title: '❌ ' + $translate.instant('Delete'),
                    shown: function () {
                        return $scope.isHSE;
                    },
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            if (resultRows[0].Status == 'N') {
                                var params = {};
                                debugger
                                params.VoucherID = resultRows[0].DisciplineID;
                                params.status = 'X';
                                ConDisciplineService.ChangeStatus_Discipline().save(params, {}).$promise.then(function (res) {
                                    $scope.searchDiscipline();
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
                    }, order: 4
                },
                {
                    title: '💰 ' + $translate.instant('PayFine'),
                    shown: function () {
                        return $scope.isGA;
                    },
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            row = resultRows[0]
                            if (row.Status == 'WP' || row.Status == 'E') {
                                $location.url('/ContractorDiscipline/Discipline/Payment/' + row.DisciplineID);
                            } else {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant("Payment_MSG") });
                            }
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                        }
                    }, order: 5
                },
                {
                    title: '📝 ' + $translate.instant('EditPayment'),
                    shown: function () {
                        return $scope.isGA;
                    },
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            row = resultRows[0]
                            if (row.Status == 'P') {
                                $location.url('/ContractorDiscipline/Discipline/EditPayment/' + row.DisciplineID);
                            } else {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant("EditPayment_MSG") });
                            }
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                        }
                    }, order: 6
                }];

            $scope.searchDiscipline = function () {
                ConDisciplineService.Search_Discipline().get({
                    Language: lang,
                    VoucherID: $scope.query.DisciplineCode || '',
                    Con_Name: $scope.query.CotractorName || '',
                    Department: $scope.query.DepartmentID || '',
                    Status: $scope.query.Status || '',
                    FromDate: $scope.query.FromDate || '',
                    ToDate: $scope.query.ToDate || '',
                    UserID: $scope.user
                }).$promise.then(function (res) {
                    $scope.gridOptions.data = res;
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
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


        }
    ]);
})
