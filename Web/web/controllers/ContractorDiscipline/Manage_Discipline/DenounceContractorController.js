define(['myapp', 'angular'], function (myapp) {
    myapp.controller('DenounceContractorController', ['ConDisciplineService', '$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', '$upload', '$translatePartialLoader', '$translate', 'GateGuest', '$timeout',
        function (ConDisciplineService, $scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, $upload, $translatePartialLoader, $translate, GateGuest, $timeout) {
            var lang = window.localStorage.lang;
            $scope.flowkey = "GateContractorDenounce";
            $scope.query = {};
            $scope.data = {};
            $scope.Create = false;
 
            EngineApi.getTcodeLink().get({ 'userid': Auth.username, 'tcode': 'GateContractorEHS' }, function (linkres) {
                if (linkres.IsSuccess) {
                    $scope._isHSE = '';
                } else {
                    $scope._isHSE = Auth.username;
                }
            })

            ConDisciplineService.GetStatus().get({ type: "DenounceApp", language: lang }).$promise.then(function (res) {
                $scope.StatusList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            var col = [
                {
                    field: 'DenounceCode',
                    displayName: $translate.instant('VoucherID'),
                    width: 150,
                    minWidth: 100
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
                    cellTooltip: true,
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
                    field: 'CreateDate',
                    displayName: $translate.instant('CreateDate'),
                    width: 180,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'Remark',
                    displayName: $translate.instant('Remark'),
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
                    EngineApi.getTcodeLink().get({ 'userid': Auth.username, 'tcode': $scope.flowkey }, function (linkres) {
                        if (linkres.IsSuccess) {
                            gridApi.core.addToGridMenu(gridApi.grid, gridMenu);
                        }
                    })
                }
            };

            var gridMenu = [
                {
                    title: $translate.instant('Create'),
                    icon: 'ui-grid-icon-plus-squared',
                    action: function () {
                        $scope.Create = true;
                        $scope.detail = false;
                        $scope.data.listfile_Denounce = false;
                        $('#modalDenounceContractor').modal('show');
                    },
                    order: 1
                },
                {
                    title: '✏️ ' + $translate.instant('Update'),
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
                                    $scope.data.listfile_Denounce = false;
                                    $scope.loadDenounce(row.DenounceID)
                                    $('#modalDenounceContractor').modal('show');
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
                    title: '📋 ' + $translate.instant('Detail'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            if (resultRows[0].Status != "X") {
                                $scope.detail = true;
                                $scope.viewdetail = true;
                                $scope.loadDenounce(resultRows[0].DenounceID)
                                $('#modalDenounceContractor').modal('show');
                            } else {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': $translate.instant('Detail_MSG')
                                });
                            }

                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                        }
                    }, order: 3
                },
                {
                    title: '❌ ' + $translate.instant('Delete'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            if (resultRows[0].Status == 'N') {
                                var params = {};
                                debugger
                                params.VoucherID = resultRows[0].DenounceID;
                                params.status = 'X';
                                ConDisciplineService.ChangeStatus_Denounce().save(params, {}).$promise.then(function (res) {
                                    $scope.searchDenounce();
                                    $timeout(function () {
                                        Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("Delete_Succeed_Msg") });
                                    }, 400);
                                }, function (errResponse) {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                });
                            } else {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant("Delete_Draf_Msg") });
                            }
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                        }
                    }, order: 4
                },
                {
                    title: '🧾 ' + $translate.instant('InProcess'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            if (resultRows[0].Creator != Auth.username) {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': $translate.instant('ViewProcess_MSG')
                                });
                                return;
                            }
                            else {
                                $scope.getProcessDenounce(resultRows[0].DenounceID);
                            }

                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                        }
                    }, order: 5
                },];

            $scope.searchDenounce = function () {
                ConDisciplineService.Search_Denounce().get({
                    Language: lang,
                    VoucherID: $scope.query.DenounceID || '',
                    Con_Name: $scope.query.CotractorName || '',
                    Type: $scope.query.FieldViolation || '',
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

            $scope.getProcessDenounce = function (DenounceID) {
                ConDisciplineService.GetDenouncePID().get({
                    VoucherID: DenounceID
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
        }
    ]);
})
