/**
 *
 * 承揽商资质 - Contractor qualification
 */
define(['myapp', 'angular'], function (myapp) {
    myapp.controller('ConQuaController', ['$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', 'ConQuaService', '$upload', '$translatePartialLoader', '$translate', 'GateGuest', '$timeout',
        function ($scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, ConQuaService, $upload, $translatePartialLoader, $translate, GateGuest, $timeout) {
            var lang = window.localStorage.lang;
            $scope.bpmnloaded = false;
            $scope.flowkey = "GateContractorInfoProcess";
            $scope.query = {};
            $scope.projects = {};
            $scope.isHSEUser = false;
            $scope.isSecurityUser = false;
            $scope.isHrUser = false;
            $scope.query.Classify='A';
            $scope.AllContractorList =[]

            var params = {};
            params.language = window.localStorage.lang;
            params.contractorName = "";
            params.cType = "";
            params.departmentID = "";
            params.userid = "";
            params.status = "";
            ConQuaService.ContractorQualification().get(params).$promise.then(function (res) {
                $scope.AllContractorList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            GateGuest.GetQueryStatus().get({ ctype: 'ConQua', language: lang, flag: '1' }).$promise.then(function (res) {
                $scope.StatusList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            $scope.$watch('query.Classify', function (value) {
                if (value == 'A') {
                    $scope.query.paraStatus = 'A'
                } else if (value == 'IA') {
                    $scope.query.paraStatus = 'IA'
                }else if (value == '') {
                    $scope.query.paraStatus = ''
                }
            })


            ConQuaService.ContractorTypeList().get({ kind: "Type", language: lang }).$promise.then(function (res) {
                debugger;
                $scope.CTypeList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            ConQuaService.GetContractorDepartment().get({ language: lang }).$promise.then(function (res) {
                $scope.CDepartmentList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            $scope.showPng = function () {
                if ($scope.bpmnloaded == true) {
                    $scope.bpmnloaded = false;
                } else {
                    $scope.bpmnloaded = true;
                }
            }

            /**CHECK USER ROLE **/
            ConQuaService.GetRole().get({
                UserID: Auth.username
            }, function (res) {
                console.log(res);
                if (res.length > 0) {
                    var dept = res[0].Dept.trim();
                    if (dept == 'HSE') {
                        $scope.isHSEUser = true;
                    }
                    else if (dept == 'SECURITY') {
                        $scope.isSecurityUser = true;

                    } else if (dept == 'HR') {
                        $scope.isHrUser = true;
                    }
                }
            });


            var col = [
                {
                    field: 'ContractorName',
                    width: 400,
                    cellTemplate: "<a ng-click='grid.appScope.getContractorProcessInstanceId(row)' style='padding:5px;display:block; cursor:pointer'>{{COL_FIELD}}</a>",
                    displayName: $translate.instant('ContractorName'),
                    minWidth: 100, cellTooltip: true
                },
                {
                    field: 'StatusRemark',
                    displayName: $translate.instant('Status'),
                    width: 140,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'ContracorKind',
                    displayName: $translate.instant('ConQua_CType'),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'ContracorType',
                    displayName: $translate.instant('Type'),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'Rcode',
                    displayName: $translate.instant('ConQua_Rcode'),
                    width: 140,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'StartDate',
                    displayName: $translate.instant('BeginDate'),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'EndDate',
                    displayName: $translate.instant('EndDate'),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'Specification',
                    displayName: $translate.instant('ConQua_paraDepartment'),
                    width: 230,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'StartdateCancel',
                    displayName: $translate.instant("Suspended_Begin"),
                    width: 180,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'EnddateCancel',
                    displayName: $translate.instant("Suspended_End"),
                    width: 180,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'SuspendUser',
                    displayName: $translate.instant("Suspend_User"),
                    width: 180,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'SuspendReason',
                    displayName: $translate.instant("DeleteReason"),
                    width: 180,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'CancelSuspendUser',
                    displayName: $translate.instant("CancelSuspend_User"),
                    width: 180,
                    minWidth: 100,
                    cellTooltip: true
                }
            ];


            $scope.getContractorProcessInstanceId = function (obj) {
                $('#Details').modal('show')
                var EmployerId = obj.entity.ContractorID
                if (EmployerId) {
                    ConQuaService.getContractorPID().get({
                        employerid: EmployerId
                    }).$promise.then(function (conres) {
                        $scope.processList = conres[conres.length - 1];
                        console.log($scope.processList);
                    }, function (errResponse) {
                        console.log(errResponse.data);
                        Notifications.addError({
                            'status': 'error',
                            'message': errResponse.data
                        });
                    });

                    ConQuaService.getContractorCancelPID().get({
                        employerid: EmployerId
                    }).$promise.then(function (conres) {
                        $scope.cancelprocessList = conres[conres.length - 1];
                    }, function (errResponse) {
                        console.log(errResponse.data);
                        Notifications.addError({
                            'status': 'error',
                            'message': errResponse.data
                        });
                    });
                }
            }

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
                action: function () {
                    $scope.projects.StartDate = null;
                    $scope.projects.EndDate = null;
                    $scope.readEmployer = false;
                    $scope._IsSubmit = true;
                    $scope._IsRenew = false;
                    $('#modalContractor').modal('show');
                },
                order: 1
            },
            {
                title: '✏️ ' + $translate.instant('Update'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    if (resultRows.length == 1) {
                        var e = resultRows[0];
                        if (e.ContractorID) {
                            if (e.UserID != Auth.username) {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': $translate.instant('Update_owner_MSG')
                                });
                                return;
                            }
                            else if ((e.Status == 'N' || e.Status == 'Q')) {
                                $scope.readEmployer = false;
                                $scope._IsSubmit = true;
                                $scope._IsRenew = false;
                                $scope.loadContractorDetail(e.ContractorID, e.UserID, e.Status);
                                $('#modalContractor').modal('show');
                            } else {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant('Edit_Draf_MSG') });
                            }
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
                    var e = resultRows[0];
                    console.log(e)
                    if (resultRows.length == 1) {
                        if (e.UserID != Auth.username) {
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant('Delete_owner_MSG')
                            });
                            return;
                        } else {
                            if (e.Status == 'N') {
                                var query = {}
                                query.contractorID = e.ContractorID
                                ConQuaService.Delete().save(query, {}).$promise.then(function (res) {
                                    console.log(res);
                                    $timeout(function () {
                                        Notifications.addMessage({ 'status': 'info', 'message': $translate.instant('Delete_Succeed_Msg') });
                                    }, 200);
                                    QueryInfoList();
                                }, function (errResponse) {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                });
                            }
                            else {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant('Delete_Draf_Msg') });
                            }
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                }, order: 3
            },
            {
                title: '📝' + $translate.instant('Renew'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    if (resultRows.length == 1) {
                        var e = resultRows[0];
                        if (e.ContractorID) {
                            if (e.UserID != Auth.username) {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': $translate.instant('Renew_owner_MSG')
                                });
                                return;
                            }
                            else if ((e.Status == 'Q')) {
                                $scope.readEmployer = false;
                                $scope._IsSubmit = false;
                                $scope._IsRenew = true;
                                $scope.loadContractorDetail(e.ContractorID, e.UserID, e.Status);
                                $('#modalContractor').modal('show');
                            } else {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant('Renew_Signed_MSG') });
                            }
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                },
                order: 4
            }, {
                title: '🧾 ' + $translate.instant('Detail'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    var e = resultRows[0];
                    if (resultRows.length == 1) {
                        if (e.Status != 'X') {
                            $location.url('/Contractor/Detail?contractorID=' + e.ContractorID);
                            $scope.projects.StartdateCancel = 0;
                        }
                        else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Detail_MSG") });
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                },
                order: 5
            }, {
                title: '🚫 ' + $translate.instant('Suspended'),
                shown: function () {
                    return $scope.isHSEUser;
                },
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    var e = resultRows[0];
                    console.log(e)
                    if (resultRows.length == 1) {
                        if (e.Status == 'Q') {
                            $scope.ContractorSuspended = e.ContractorName;
                            $('#delConfirm').modal('show');
                        }
                        else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant('Suspended_Signed_MSG') });
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                }, order: 6
            }, {
                title: '🔄 ' + $translate.instant('CancelSuspended'),
                shown: function () {
                    return $scope.isHSEUser;
                },
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    var e = resultRows[0];
                    console.log(e)
                    if (resultRows.length == 1) {
                        if (e.Status == 'C' || e.Status == 'SC' || e.Status == "PC") {
                            var query = {}
                            query.contractorID = e.ContractorID
                            query.enddate = e.EndDate
                            query.status = e.Status,
                                query.cancelUser = Auth.username
                            ConQuaService.CancelSuspendContractor().save(query, {
                            }).$promise.then(function (res) {
                                console.log(res)
                                $timeout(function () {
                                    Notifications.addMessage({ 'status': 'info', 'message': $translate.instant('Cancel_Suspended_Success_MSG') });
                                }, 200);
                                QueryInfoList();
                                ConQuaService.ConSuspendedMail().get({
                                    ContractorID: e.ContractorID,
                                    mailKind: "c_Cancelsuspended"
                                }, {}).$promise.then(function (res) {
                                    console.log(res);
                                }, function (err) {
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': 'Update status error: ' + err
                                    });
                                });
                            }, function (errResponse) {
                                Notifications.addError({ 'status': 'error', 'message': errResponse });
                            });
                        }
                        else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant('Cancel_Suspended_Status_MSG') });
                        }

                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                }, order: 7
            }];


            /* Check before suspend*/
            function IsCanInvalid(callback) {
                if ($scope.projects.StartdateCancel > $scope.projects.EnddateCancel) {
                    callback($translate.instant('Date_Msg'))
                    return;
                }
                else {
                    if ($scope.file.name != undefined) {
                        $scope.upload = $upload.upload({
                            url: '/ehs/gate/ConQua/UploadFile',
                            method: "POST",
                            file: $scope.file
                        }).progress(function (evt) { }).success(function (data) {
                            console.log("file is uploaded successfully");
                            console.log(data);
                            $scope.fileAttached = data[0];
                            console.log($scope.fileAttached);
                            callback("");
                        }).error(function (data, status) {
                            Notifications.addError({ 'status': 'error', 'message': status + data });
                            callback(status + data);
                        });
                    } else { callback(""); }
                }
            }

            $scope.invalidContractor = function () {
                IsCanInvalid(function (errmsg) {
                    if (errmsg) {
                        $timeout(function () {
                            Notifications.addError({ 'status': 'error', 'message': errmsg });
                        }, 400);
                    } else {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        var e = resultRows[0];
                        GateGuest.GetGateCheckers().getCheckers({
                            owner: Auth.username,
                            fLowKey: 'FEPVConInfoCancel',
                            Kinds: '',
                            CheckDate: ''
                        }).$promise.then(function (res) {
                            var leaderList = [];
                            for (var i = 0; i < res.length; i++) {
                                leaderList[i] = res[i].Person;
                            }
                            if (leaderList.length <= 0) {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant('Leader_NO_MSG') });
                                return
                            } else {
                                var formVariables = [];
                                var historyVariable = [];

                                formVariables.push({ name: 'ChecherArray', value: leaderList });
                                formVariables.push({ name: 'start_remark', value: "Contractor " + e.ContractorName });
                                formVariables.push({ name: 'invalidReason', value: $scope.invalidReason });
                                formVariables.push({ name: "EmployerId", value: e.ContractorID });
                                formVariables.push({ name: 'StartdateCancel', value: $scope.projects.StartdateCancel });
                                formVariables.push({ name: 'EnddateCancel', value: $scope.projects.EnddateCancel });
                                formVariables.push({ name: 'SendUser', value: Auth.username });
                                if ($scope.fileAttached != "") {
                                    formVariables.push({ name: 'FileSuspend', value: $scope.fileAttached });
                                }

                                historyVariable.push({ name: 'ContractorName', value: e.ContractorName });
                                historyVariable.push({ name: 'DeleteReason', value: $scope.invalidReason });
                                historyVariable.push({ name: 'StartdateCancel', value: $scope.projects.StartdateCancel });
                                historyVariable.push({ name: 'EnddateCancel', value: $scope.projects.EnddateCancel });

                                $scope.getFlowDefinitionId('FEPVConInfoCancel', function (FlowDefinitionId) {
                                    if (FlowDefinitionId) {
                                        $scope.startflowid(FlowDefinitionId, e.ContractorID, formVariables, historyVariable, function (url, message) {
                                            if (message) {
                                                Notifications.addError({ 'status': 'error', 'message': message });
                                            } else {

                                                var query = {}
                                                query.employerId = e.ContractorID
                                                query.status = 'FC'
                                                ConQuaService.ConQuaSaveStatus().save(query, {}).$promise.then(function (res) {
                                                    console.log(res);
                                                    $location.path('/taskForm/' + url);
                                                }, function (errResponse) {
                                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                                })
                                            }
                                        })
                                    } else {
                                        Notifications.addError({ 'status': 'error', 'message': $translate.instant('Process_Err_MSG') });
                                        return;
                                    }
                                })
                            }
                        });
                    }
                })
            };

            $scope.getFlowDefinitionId = function (keyname, callback) {
                EngineApi.getKeyId().getkey({
                    'key': keyname
                }, function (res) {
                    callback(res.id);
                });
            }

            $scope.startflowid = function (definitionID, businessKey, formVariables, historyVariable, callback) {
                debugger;
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

            $scope.QueryInfo = function () {
                QueryInfoList();
            };

            function QueryInfoList() {
                ConQuaService.ContractorQualification().getList({
                    contractorName: $scope.query.paraEmployer || '',
                    cType: $scope.query.paraCType || '',
                    language: lang,
                    departmentID: $scope.query.paraDepartment || '',
                    status: $scope.query.paraStatus || '',
                    userid: $scope.query.onlyOwner == true ? Auth.username : ''
                }).$promise.then(function (res) {
                    $scope.gridOptions.data = res;
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            $scope.fileAttached = "";
            $scope.file = [];
            $scope.onFileSelect = function ($files, size) {
                console.log($files);
                if (true) {
                    if ($scope.fileAttached != "") {
                        Notifications.addError({
                            'status': 'error',
                            'message': $translate.instant('File_Number_MSG')
                        });

                        return false;
                    }
                    $scope.file = $files[0];
                    $scope.fileAttached = $scope.file.name;
                }
            }

            $scope.removeList = [];
            $scope.getRemoveFileName = function () {
                $scope.removeList.push($scope.fileAttached);
                $scope.fileAttached = "";
                $scope.file = [];
            }

            $scope.removeFile = function (_name) {
                if (_name != "") {
                    ConQuaService.DeleteFile().save({
                        filename: _name
                    }).$promise.then(function (res) {
                        console.log(res)
                    },
                        function (error) {
                            console.log(error);
                        })
                }
            }


            $scope.formatFileName = function (_fileName) {
                if (_fileName.length > 20) {
                    return _fileName.substring(0, 10) + "..." + _fileName.substring(_fileName.length - 9);
                } else {
                    return _fileName;
                }
            }

            $scope.ResetSuspendedModal = function () {
                $scope.otherInfomation = false;
                $scope.projects = {};
                $scope.invalidReason = "";
                $scope.fileAttached = "";
                $scope.removeList = [];
                $scope.file = [];
            }
        }
    ]);
})
