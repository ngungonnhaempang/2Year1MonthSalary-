define(['myapp', 'angular'], function (myapp, angular) {
    myapp.controller("EastGateController", ['$scope', '$filter', '$location', 'Notifications', 'Forms', 'Auth', 'EngineApi', 'ConQuaService', '$upload', '$translate', 'GateGuest', '$timeout',
        function ($scope, $filter, $location, Notifications, Forms, Auth, EngineApi, ConQuaService, $upload, $translate, GateGuest, $timeout) {
            var lang = window.localStorage.lang;
            $scope.flowkey = "EastGateInOut";
            $scope.bpmnloaded = false;
            $scope.query = {};
            $scope.detail = false;

            $scope.IsUpdate = false;
            $scope.IsChange = false;
            $scope.isSecurityUser = false;

            $scope.departID = "";
            
            $scope.Language = window.localStorage.lang;

            GateGuest.GetQueryStatus().get({ cType: "EastGate", language: lang, flag: "" }).$promise.then(function (res) {
                console.log(res);
                $scope.StatusList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            })

            ConQuaService.GetContractorDepartment().get({ language: lang }).$promise.then(function (res) {
                $scope.DepartmentList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });


            /**CHECK USER ROLE **/
            ConQuaService.GetRole().get({
                UserID: Auth.username
            }, function (res) {
                console.log(res);
                if (res.length > 0) {
                    var dept = res[0].Dept.trim();
                    if (dept == 'SECURITY') {
                        $scope.isSecurityUser = true;
                    }
                }
            });

            $scope.getProcessEastGateInOutLog = function (obj) {
                debugger
                var _VoucherID = obj.entity.VoucherID;
                ConQuaService.GetEastGateInOutPID().get({
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
                    field: 'VoucherID',
                    displayName: $translate.instant("VoucherID"),
                    minWidth: 100,
                    cellTemplate: '<a ng-click="grid.appScope.getProcessEastGateInOutLog(row)"  style="padding:5px;display:block; cursor:pointer">{{COL_FIELD}}</a>'
                },
                {
                    field: 'StatusName',
                    displayName: $translate.instant("Status"),
                    width: 140,
                    minWidth: 100
                },
                {
                    field: 'EmpID',
                    displayName: $translate.instant("EmployeeID"),
                    minWidth: 150,
                    cellTooltip: true
                },
                {
                    field: 'Name',
                    displayName: $translate.instant("ConName"),
                    minWidth: 200
                },
                {
                    field: 'Gender',
                    displayName: $translate.instant("Gender"),
                    width: 120,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'CardNo',
                    displayName: $translate.instant("CardNO"),
                    width: 100,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'Birthday',
                    displayName: $translate.instant("Birthday"),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'PositionName',
                    displayName: $translate.instant("Job"),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'AppointmentDate',
                    displayName: $translate.instant("FingerPrintAppointment"),
                    width: 200,
                    minWidth: 100,
                    cellTooltip: true,
                },
                {
                    field: 'RegisterReason',
                    displayName: $translate.instant("RegisterReason"),
                    width: 300,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'StartDate',
                    displayName: $translate.instant("BeginDate"),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'EndDate',
                    displayName: $translate.instant("EndDate"),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'Creator',
                    displayName: $translate.instant("Creator"),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'DeptName',
                    displayName: $translate.instant("Department"),
                    minWidth: 600,
                    cellTooltip: true
                },
                {
                    field: 'LockDate',
                    displayName: $translate.instant("LockDate"),
                    width: 180,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'LockReason',
                    displayName: $translate.instant("LockReason"),
                    width: 250,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'LockUser',
                    displayName: $translate.instant("CardLocker"),
                    width: 180,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'UnLockDate',
                    displayName: $translate.instant("UnlockDate"),
                    width: 180,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'UnlockReason',
                    displayName: $translate.instant("UnlockReason"),
                    width: 250,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'UnLockUser',
                    displayName: $translate.instant("CardUnlocker"),
                    width: 180,
                    minWidth: 100,
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

                onRegisterApi: function (gridApi) {
                    EngineApi.getTcodeLink().get({ "userid": Auth.username, "tcode": $scope.flowkey }, function (linkres) {
                        if (linkres.IsSuccess) {
                            gridApi.core.addToGridMenu(gridApi.grid, gridMenu);
                        }
                    })
                    $scope.gridApi = gridApi;
                }
            };

            var gridMenu = [{
                title: $translate.instant("Create"),
                icon: 'ui-grid-icon-plus-squared',
                action: function () {
                    $('#EastGateModal').modal('show');
                },
                order: 1
            }, {
                title: '✏️ ' + $translate.instant('Update'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    var e = resultRows[0];
                    if (resultRows.length == 1) {
                        if (e.Creator != Auth.username) {
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant('Update_owner_MSG')
                            });
                            return;
                        } else if (e.Status == "N") {
                            $scope.loadVoucherDetail(e.VoucherID);
                            $('#EastGateModal').modal('show');
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Edit_Draf_MSG") });
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                },
                order: 2
            },
            {
                title: '❌ ' + $translate.instant('MD_Cancel'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    var e = resultRows[0];
                    if (resultRows.length == 1) {
                        if (e.Creator != Auth.username) {
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant('Delete_owner_MSG')
                            });
                            return;
                        } else {
                            if (e.Status == 'N') {
                                $scope.itemDelete = {
                                    ID: e.VoucherID
                                };
                                $('#ConfirmDeleteModal').modal('show');

                            } else {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant('Delete_Draf_Msg') });
                            }
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                },
                order: 3
            },
            {
                title: '🧾  ' + $translate.instant('Detail'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    if (resultRows.length == 1) {
                        if (resultRows[0].Status != 'X') {
                            var href = '#/InOutEastGate/ViewDetail/' + getRandomString(20) + resultRows[0].VoucherID + getRandomString(20)
                            window.open(href);
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Detail_MSG") });
                        }
                    }
                    else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                },
                order: 4
            }];

            $scope.searchRegisterVoucher = function () {
                var dept = ""
                if(!$scope.isSecurityUser){
                    dept = $scope.departID
                }
                ConQuaService.SearchRegisterVoucher().get({
                    VoucherID: "",
                    Name: $scope.query.EmpName || "",
                    EmpID: $scope.query.EmpID || "",
                    FromDate: $scope.query.FromDate || "",
                    ToDate: $scope.query.ToDate || "",
                    CostCenter: $scope.query.DepartmentID || dept,
                    Status: $scope.query.Status || "",
                    Language: lang || "",
                    User: Auth.username
                }).$promise.then(function (res) {
                    debugger;
                    $scope.gridOptions.data = res;
                })
            }


            function getRandomString(length) {
                var randomChars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
                var result = '';
                for (var i = 0; i < length; i++) {
                    result += randomChars.charAt(Math.floor(Math.random() * randomChars.length));
                }
                return result;
            }

            $scope.deleteVoucher = function (_VoucherID) {
                var query = {}
                query.VoucherID = _VoucherID
                query.AppointmentDate = ''
                query.status = 'X'
                ConQuaService.UpdateStatusVoucher().save(query, {}).$promise.then(function (res) {
                    console.log(res);
                    $scope.searchRegisterVoucher();
                    $('#ConfirmDeleteModal').modal('hide');
                    $timeout(function () {
                        Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("Delete_Succeed_Msg") });
                    }, 400);                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                })                
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
    ])
});