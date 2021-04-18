/**
 * Created by wang.chen on 2017/2/16.
 */
define(['myapp', 'angular'], function (myapp, angular) {
    myapp.controller("ContractorQuaController", ['$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', 'ConQuaService', '$upload', '$translatePartialLoader', '$translate', 'GateGuest', '$timeout',
        function ($scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, ConQuaService, $upload, $translatePartialLoader, $translate, GateGuest, $timeout) {
            var lang = window.localStorage.lang;
            $scope.flowkey = "GateContractorQuaProcess";
            $scope.bpmnloaded = false;
            $scope.note = {};
            $scope.event = {};
            $scope.record = {};
            $scope.projects = {};
            $scope.IsUpdate = false;
            $scope.IsChange = false;
            $scope.IsChangeFR = false;
            $scope.IsVietnamese = true;
            $scope.isHrUser = false;
            $scope.isHSEUser = false;
            $scope.isSecurityUser = false;
            $scope.fileAttached = "";
            $scope.file = [];
            $scope.removeList = [];
          

            var query = {};
            query.language = window.localStorage.lang;
            query.contractorName = "";
            query.cType = "";
            query.departmentID = "";
            query.userid = "";
            query.status = "";

            $scope.selectTerm = function (value) {
                $scope.otherInfomation = value;
            }

            ConQuaService.CountContractor().get({ ContractorID: '' }).$promise.then(function (res) {
                $scope.rpCounter = res[0];
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            ConQuaService.ContractorQualification().get(query).$promise.then(function (res) {
                $scope.AllContractorList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            ConQuaService.GetContractorRegion().get({ language: window.localStorage.lang }).$promise.then(function (res) {
                $scope.RegionList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            GateGuest.GetQueryStatus().get({ cType: "C_Worker", language: lang, flag: "" }).$promise.then(function (res) {
                console.log(res);
                $scope.StatusList = res;
            })

            $scope.$watch('note.Classify', function (value) {
                if (value == 'A') {
                    $scope.note.Status = 'A'
                    $scope.note.ContractorID = 'A'
                } else if (value == 'IA') {
                    $scope.note.Status = 'IA'
                    $scope.note.ContractorID = 'IA'
                }else if (value == '') {
                    $scope.note.Status = ''
                    $scope.note.ContractorID = ''
                }else if(value==undefined)
                {
                    $scope.note.Classify='A';
                }
            })

         
            $scope.showPng = function () {
                if ($scope.bpmnloaded == true) {
                    $scope.bpmnloaded = false;
                } else {
                    $scope.bpmnloaded = true;
                }
            }

            function creatVoucherId(callback) {
                ConQuaService.GetVoucherID({}, function (res) {
                    $scope.voucherID = res[0].voucherid;
                    callback($scope.voucherID, '');
                }, function (errResponse) {
                    callback('', errResponse);
                });
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
                    field: 'VoucherID',
                    cellTemplate: '<a ng-click="grid.appScope.getVoucherProcessInstanceId(row)"  style="padding:5px;display:block; cursor:pointer">{{COL_FIELD}}</a>',
                    displayName: $translate.instant("VoucherID"),
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'StatusRemark',
                    displayName: $translate.instant("Status"),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true,
                    cellClass: function (grid, row, col, rowIndex, colIndex) {
                        var val = row.entity.Status;
                        var valDept = row.entity.SuspendDept;
                        if ((val == 'C' || val == 'PC' || val == 'CC') && valDept == 'HSE') {
                            return 'red';
                        }
                        else if ((val == 'C' || val == 'PC' || val == 'CC') && valDept == 'SECURITY') {
                            return 'blue';
                        }
                        else if (val == 'P') {
                            return 'green';
                        }
                        else if (val == 'L') {
                            return 'yellow';
                        }
                    }
                },
                {
                    field: 'Name',
                    displayName: $translate.instant("ConName"),
                    minWidth: 200,
                    cellTooltip: true
                },
                {
                    field: 'IdCard',
                    displayName: $translate.instant("ID_PP"),
                    minWidth: 150,
                    cellTooltip: true
                },
                {
                    field: 'ContractorName',
                    displayName: $translate.instant("Contractor"),
                    minWidth: 200,
                    cellTooltip: true
                },
                {
                    field: 'Region',
                    displayName: $translate.instant("ConQua_Region"),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'CardNO',
                    displayName: $translate.instant("CardNO"),
                    width: 100,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'ContractorByEmloyee',
                    displayName: $translate.instant("ContractorByEmloyee"),
                    width: 140,
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
                    field: 'InsuranceDuration',
                    displayName: $translate.instant("Insurance"),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'SafetyCerDuration',
                    displayName: $translate.instant("SafetyCer"),
                    width: 190,
                    minWidth: 100,
                    cellTooltip: true,
                },
                {
                    field: 'TrainDate',
                    displayName: $translate.instant("TrainDate"),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'AppointmentDate',
                    displayName: $translate.instant("AppointmentDate"),
                    width: 190,
                    minWidth: 100,
                    cellTooltip: true,
                },
                {
                    field: 'ReasonReturn',
                    displayName: $translate.instant("DenyReason"),
                    width: 300,
                    minWidth: 100,
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
                },
                {
                    field: 'Remark',
                    displayName: $translate.instant("Remark"),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'Mark',
                    displayName: $translate.instant("Mark"),
                    width: 140,
                    minWidth: 100,
                    cellTooltip: true,
                    visible: false
                },
                {
                    field: 'UpdateLeaveUser',
                    displayName: $translate.instant("UpdateLeaveUser"),
                    width: 200,
                    minWidth: 100
                }
            ];

            $scope.gridOptions = {
                columnDefs: col,
                data: [],
                enableFiltering: true,
                enableColumnResizing: true,
                enableFullRowSelection: true,
                enableSorting: true,
                showGridFooter: false,
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
                action: function () {
                    creatVoucherId(function (id, errmsg) {
                        console.log(id);
                        if (id == '' && errmsg) {
                            $timeout(function () {
                                Notifications.addError({ 'status': 'error', 'message': errmsg });
                            }, 400);
                        } else {
                            $scope.EmpStatus = "";
                            $scope.record.Foreign = "0";
                            $scope.isReturn = false;
                            $scope.loadContractorByDept();
                            $('#modalContractorEmployee').modal('show');
                        }
                    });
                },
                order: 1
            }, {
                title: '✏️ ' + $translate.instant('Update'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    var e = resultRows[0];
                    if (resultRows.length == 1) {
                        if ($scope.isHSEUser && (e.Status == "W" || e.Status == "P")) {
                            $scope.isReturn = false;
                            $scope.IsChange = true;
                            $scope.IsChangeFR = true;
                            $scope.loadVoucherDetail(e.VoucherID, e.IdCard, e.Status);
                            $('#modalContractorEmployee').modal('show');
                        }
                        else if ($scope.isHrUser && (e.Status == "W" || e.Status == "P") && e.IsForeign == "1") {
                            $scope.isReturn = false;
                            $scope.IsChange = true;
                            $scope.IsChangeFR = false;
                            $scope.IsVietnamese = false;
                            $scope.loadVoucherDetail(e.VoucherID, e.IdCard, e.Status);
                            $('#modalContractorEmployee').modal('show');
                        }
                        else if (e.UserID != Auth.username) {
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant('Update_owner_MSG')
                            });
                            return;
                        } else if (e.Status == "N" || e.Status == "RT") {
                            $scope.isReturn = false;
                            $scope.loadVoucherDetail(e.VoucherID, e.IdCard, e.Status);
                            $('#modalContractorEmployee').modal('show');
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
                title: '❌ ' + $translate.instant('Delete'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    var e = resultRows[0];
                    if (resultRows.length == 1) {
                        if (e.UserID != Auth.username) {
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant('Delete_owner_MSG')
                            });
                            return;
                        } else {
                            if (e.Status == 'N' || e.Status == 'RT') {
                                var query = {}
                                query.VoucherID = e.VoucherID;
                                query.IdCard = e.IdCard
                                query.status = 'X'
                                query.employeeID = e.EmployeeID
                                ConQuaService.ContractorQuaSaveStatus().save(query, {}).$promise.then(function (res) {
                                    console.log(res);
                                    $scope.SearchEmployee();
                                    $timeout(function () {
                                        Notifications.addMessage({ 'status': 'info', 'message': $translate.instant('Delete_Succeed_Msg') });
                                    }, 200);
                                }, function (errResponse) {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                });
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
                            window.open('#/ContractorQuaDetail?code=' + resultRows[0].VoucherID + '&IdCard=' + resultRows[0].IdCard
                                + '&IsForeign=' + resultRows[0].IsForeign);
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Detail_MSG") });
                        }
                    }
                    else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                },
                order: 4
            },
            // {
                // title: '🗓️ ' + $translate.instant('UpdateTraining'),
                // shown: function () {
                    // return $scope.isHSEUser;
                // },
                // action: function () {
                    // if ($scope.note.ContractorID != undefined) {
                        // ConQuaService.GetContractorConfirm().get({
                            // ContractorID: $scope.note.ContractorID,
                            // Language: window.localStorage.lang
                        // }).$promise.then(function (res) {
                            // console.log(res);
                            // debugger;
                            // if (res.length > 0) {
                                // var _flag = false;
                                // for (let index = 0; index < res.length; index++) {
                                    // const element = res[index];
                                    // if (element.Status == 'Q' && (element.Mark == "" || parseInt(element.Mark) < 5)) {
                                        // _flag = true;
                                        // break;
                                    // }
                                // }
                                // if (_flag) {
                                    // $location.url("/ContractorQuaConfirm?ContractorID=" + $scope.note.ContractorID + '&state=training');
                                // } else {
                                    // Notifications.addError({ 'status': 'error', 'message': $translate.instant("Conqua_NotVoucherQ") });
                                // }

                            // } else
                                // Notifications.addError({ 'status': 'error', 'message': $translate.instant("Conqua_NotVoucherQ") });

                        // }, function (errResponse) {
                            // Notifications.addError({ 'status': 'error', 'message': errResponse });
                        // });


                    // } else {
                        // Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_conqua") });
                    // }
                // },
                // order: 5
            // },
            // {
                // title: '💯 ' + $translate.instant('UpdateMark'),
                // shown: function () {
                    // return $scope.isHSEUser;
                // },
                // action: function () {
                    // if ($scope.note.ContractorID != undefined) {
                        // ConQuaService.GetContractorConfirm().get({
                            // ContractorID: $scope.note.ContractorID,
                            // Language: window.localStorage.lang,
                        // }).$promise.then(function (res) {
                            // console.log(res);
                            // if (res.length > 0) {
                                // $location.url("/ContractorQuaConfirm?ContractorID=" + $scope.note.ContractorID + '&state=mark');
                            // } else Notifications.addError({ 'status': 'error', 'message': $translate.instant("Conqua_NotVoucherQ") });
                        // }, function (errResponse) {
                            // Notifications.addError({ 'status': 'error', 'message': errResponse });
                        // });


                    // } else {
                        // Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_conqua") });
                    // }
                // },
                // order: 6
            // },
            {
                title: '✔️ ' + $translate.instant('Confirm'),
                shown: function () {
                    return $scope.isHSEUser;
                },
                action: function () {
                    if ($scope.note.ContractorID != undefined) {
                        ConQuaService.GetContractorConfirm().get({
                            ContractorID: $scope.note.ContractorID,
                            Language: window.localStorage.lang,
                        }).$promise.then(function (res) {
                            console.log(res);
                            if (res.length > 0) {
                                var _flag = false;
                                for (let index = 0; index < res.length; index++) {
                                    const element = res[index];
                                    if (element.Status == 'Q') {// && parseInt(element.Mark) >= 5
                                        _flag = true;
                                        break;
                                    }
                                }
                                if (_flag) {
                                    $location.url("/ContractorQuaConfirm?ContractorID=" + $scope.note.ContractorID + '&state=confirm');
                                } else {
                                    Notifications.addError({ 'status': 'error', 'message': $translate.instant("Conqua_NotVoucherQ") });
                                }

                            } else Notifications.addError({ 'status': 'error', 'message': $translate.instant("Conqua_NotVoucherQ") });
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });


                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_conqua") });
                    }
                },
                order: 7
            },

            {
                title: '🚫 ' + $translate.instant('Suspended'),
                shown: function () {
                    if ($scope.isHSEUser || $scope.isSecurityUser)
                        return true;
                    else return false;
                },
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    var e = resultRows[0];
                    if (resultRows.length == 1) {
                        if (e.Status == 'W') {
                            $scope.isCancelSuspend = false;
                            if ($scope.isHSEUser) {
                                $scope.isHseSuspened = true
                            } else $scope.isHseSuspened = false
                            $scope.ContractorSuspended = e.Name;
                            $('#delContractor').modal('show');
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant('SuspendedEmp_MSG') });
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                },
                order: 8
            },
            {
                title: '🔄 ' + $translate.instant('CancelSuspended'),
                shown: function () {
                    if ($scope.isHSEUser || $scope.isSecurityUser)
                        return true;
                    else return false;
                },
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    var e = resultRows[0];
                    console.log(e)
                    if (resultRows.length == 1) {
                        if (e.Status == 'PC' || e.Status == 'C' || e.Status == 'SC' || e.Status == 'L') {
                            if (($scope.isHSEUser && e.SuspendDept == 'HSE') || e.Status == 'L') {
                                var query = {}
                                query.voucherID = e.VoucherID
                                query.idCard = e.IdCard
                                query.enddate = e.EndDate
                                query.employeeID = e.EmployeeID
                                query.status = e.Status
                                query.cancelUser = Auth.username
                                ConQuaService.CancelSuspendEmployee().save(query, {
                                }).$promise.then(function (res) {
                                    console.log(res);
                                    debugger;
                                    var today = $filter('date')(new Date(), 'yyyy-MM-dd');
                                    if (e.EndDate >= today) {
                                        ConQuaService.uploadUser().get({})
                                    }

                                    if (e.Status != 'L') {
                                        ConQuaService.SuspendedMail().get({
                                            EmployeeID: e.EmployeeID,
                                            mailKind: "c_Cancelsuspended"
                                        }, {}).$promise.then(function (res) {
                                            console.log(res);
                                            $scope.SearchEmployee();
                                            $timeout(function () {
                                                Notifications.addMessage({ 'status': 'info', 'message': $translate.instant('Cancel_Suspended_Success_MSG') });
                                            }, 200);
                                        }, function (err) {
                                            Notifications.addError({
                                                'status': 'error',
                                                'message': 'Update status error: ' + err
                                            });
                                        });
                                    } else {
                                        ConQuaService.SuspendedMail().get({
                                            EmployeeID: e.EmployeeID,
                                            mailKind: "c_unlock"
                                        }, {}).$promise.then(function (res) {
                                            console.log(res);
                                            $scope.SearchEmployee();
                                        }, function (err) {
                                            Notifications.addError({
                                                'status': 'error',
                                                'message': 'Update status error: ' + err
                                            });
                                        });

                                    }
                                }, function (errResponse) {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                });
                            } else if ($scope.isSecurityUser && (e.SuspendDept == 'SECURITY')) {
                                $scope.isHseSuspened = true;
                                $scope.isCancelSuspend = true;
                                $scope.ContractorSuspended = e.Name;
                                $('#delContractor').modal('show');
                            }
                            else {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant('OtherDeptSuspened_MSG') });
                            }
                        }
                        else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant('Cancel_Suspended_Status_MSG') });
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                },
                order: 9
            },
            {
                title: '✖️  ' + $translate.instant('Leave'),
                action: function () {
                    debugger;
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    var e = resultRows[0];
                    if (resultRows.length == 1) {
                        if (e.Status == 'Q' || e.Status == 'S' || e.Status == 'W') {
                            var params = {}
                            params.voucherID = e.VoucherID
                            params.status = 'O'
                            params.stardateCancel = e.StartdateCancel || ''
                            params.enddateCancel = e.EnddateCancel || ''
                            params.employeeID = e.EmployeeID
                            params.suspendReason = ''
                            params.suspendUser = Auth.username
                            params.suspendDept = ''
                            ConQuaService.ContractorSaveStatusSuspend().save(params, {}).$promise.then(function (res) {
                                if (params.status == 'W') {
                                    ConQuaService.deleteUser().get({})
                                }

                                $scope.SearchEmployee();
                                $timeout(function () {
                                    Notifications.addMessage({ 'status': 'info', 'message': $translate.instant('Update_Success_MSG') });
                                }, 500);
                            }, function (errResponse) {
                                Notifications.addError({ 'status': 'error', 'message': errResponse });
                            });
                        }
                        else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant('Leave_Signed_MSG') });
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                },
                order: 10
            }];

            $scope.SearchEmployee = function () {
                ConQuaService.ContractorList().get({
                    VoucherID: "",
                    ContractorID: $scope.note.ContractorID || "",
                    Language: lang || "",
                    Status: $scope.note.Status || "",
                    UserId: $scope.note.onlyOwner == true ? Auth.username : "",
                    Name: $scope.note.Name || "",
                    IdCard: $scope.note.IdCard || "",
                    Region: $scope.note.Region || "",
                    Fromdate: $scope.note.FromDate || "",
                    Todate: $scope.note.ToDate || "",
                    Classify:$scope.note.Classify || ""
                }).$promise.then(function (res) {
                    $scope.gridOptions.data = res;
                })
            }

            $scope.getVoucherProcessInstanceId = function (obj) {
                $('#Details').modal('show')
                var VoucherID = obj.entity.VoucherID;
                var IdCard = [];
                IdCard.push(obj.entity.IdCard);

                if (VoucherID) {
                    ConQuaService.getContractorQuaProcess().get({
                        employer: VoucherID,
                        idCard: IdCard
                    }).$promise.then(function (conres) {
                        $scope.processList = conres[conres.length - 1];
                    }, function (errResponse) {
                        Notifications.addError({
                            'status': 'error',
                            'message': errResponse
                        });
                    });

                    ConQuaService.getContractorCancelPID().get({
                        employerid: IdCard
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
            };

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
                    } else {
                        callback("");
                    }

                }
            }

            $scope.invalidEmployee = function () {
                IsCanInvalid(function (errmsg) {
                    if (errmsg) {
                        $timeout(function () {
                            Notifications.addError({ 'status': 'error', 'message': errmsg });
                        }, 400);
                    } else {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        var e = resultRows[0];
                        if ($scope.isHSEUser || $scope.isCancelSuspend) {
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
                                    console.log(leaderList);
                                    if (!$scope.otherInfomation) {
                                        $scope.projects.StartdateCancel = null;
                                        $scope.projects.EnddateCancel = null;
                                    }
                                    formVariables.push({ name: 'ChecherArray', value: leaderList });
                                    formVariables.push({ name: 'start_remark', value: e.Name });
                                    formVariables.push({ name: 'Name', value: e.Name });
                                    formVariables.push({ name: 'invalidReason', value: $scope.invalidReason });
                                    formVariables.push({ name: 'StartdateCancel', value: $scope.projects.StartdateCancel });
                                    formVariables.push({ name: 'EnddateCancel', value: $scope.projects.EnddateCancel });
                                    formVariables.push({ name: "EmployerId", value: e.IdCard });
                                    formVariables.push({ name: 'ContractorID', value: e.ContractorID });
                                    formVariables.push({ name: 'SendUser', value: Auth.username });
                                    if ($scope.fileAttached != "") {
                                        formVariables.push({ name: 'FileSuspend', value: $scope.fileAttached });
                                    }
                                    formVariables.push({ name: 'statusBeforeSub', value: e.Status });

                                    historyVariable.push({ name: 'IdCard', value: e.IdCard })
                                    historyVariable.push({ name: 'ConName', value: e.Name });
                                    historyVariable.push({ name: 'ContractorName', value: e.ContractorName });
                                    historyVariable.push({ name: 'VoucherID', value: e.VoucherID });
                                    historyVariable.push({ name: 'DeleteReason', value: $scope.invalidReason });
                                    historyVariable.push({ name: 'StartdateCancel', value: $scope.projects.StartdateCancel });
                                    historyVariable.push({ name: 'EnddateCancel', value: $scope.projects.EnddateCancel });

                                    $scope.getFlowDefinitionId('FEPVConInfoCancel', function (FlowDefinitionId) {
                                        if (FlowDefinitionId) {
                                            $scope.startflowid(FlowDefinitionId, e.IdCard, formVariables, historyVariable, function (url, message) {
                                                if (message) {
                                                    Notifications.addError({ 'status': 'error', 'message': message });
                                                } else {
                                                    var query = {}
                                                    query.VoucherID = e.VoucherID
                                                    query.IdCard = e.IdCard
                                                    if ($scope.isCancelSuspend) {
                                                        query.status = 'F'
                                                    } else {
                                                        query.status = 'FC'
                                                    }
                                                    query.employeeID = e.EmployeeID
                                                    ConQuaService.ContractorQuaSaveStatus().save(query, {}).$promise.then(function (res) {
                                                        $location.path('/taskForm/' + url);
                                                    }, function (errResponse) {
                                                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                                                    });
                                                }
                                            })
                                        } else {
                                            Notifications.addError({ 'status': 'error', 'message': $translate.instant('Process_Err_MSG') });
                                            return;
                                        }
                                    })
                                }
                            });
                        } else {
                            // security suspend
                            var today = $filter('date')(new Date(), 'yyyy-MM-dd');
                            var param = {}
                            param.voucherID = e.VoucherID
                            param.employeeID = e.EmployeeID
                            param.stardateCancel = $scope.projects.StartdateCancel || ''
                            param.enddateCancel = $scope.projects.EnddateCancel || ''
                            param.suspendReason = $scope.invalidReason
                            param.suspendUser = Auth.username
                            param.suspendDept = 'SECURITY'
                            if ($scope.otherInfomation) {
                                if ($scope.projects.StartdateCancel == today) {
                                    suspendedEmployee("C", param);
                                }
                                else {
                                    suspendedEmployee("SC", param);
                                }
                            } else {
                                suspendedEmployee("PC", param);
                            }
                        }
                    }
                })
            };

            function suspendedEmployee(suspendStatus, param) {
                param.status = suspendStatus
                ConQuaService.ContractorSaveStatusSuspend().save(param, {}).$promise.then(function (res) {
                    console.log(res)

                    if(suspendStatus == 'PC' || suspendStatus == 'C'){
                        ConQuaService.deleteUser().get({})
                    }

                    if (suspendStatus == 'PC') {
                        sendMailSuspendedEmployee(param.employeeID, "c_suspended")
                    } else {
                        sendMailSuspendedEmployee(param.employeeID, "c_suspendedTemporary")
                    }

                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            function sendMailSuspendedEmployee(empID, _mailKind) {
                ConQuaService.SuspendedMail().get({
                    EmployeeID: empID,
                    mailKind: _mailKind
                }, {}).$promise.then(function (res) {
                    console.log(res);
                    $scope.SearchEmployee();
                }, function (err) {
                    Notifications.addError({
                        'status': 'error',
                        'message': 'send mail error: ' + err
                    });
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
                    if ($scope.IsVietnamese != undefined && !$scope.IsVietnamese) {
                        if (!$scope.file.name.toLowerCase().includes(".zip") && !$scope.file.name.toLowerCase().includes(".rar")) {
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant('FileCompressedValidation_MSG')
                            });
                            return false;
                        }
                    }
                    $scope.fileAttached = $scope.file.name;
                }
            }

            $scope.getRemoveFileName = function () {
                $scope.removeList.push($scope.fileAttached);
                $scope.fileAttached = "";
                $scope.file = [];
            }

            $scope.removeFile = function (_name) {
                if (_name) {
                    ConQuaService.DeleteFile().save({
                        filename: _name
                    }).$promise.then(function (res) {
                        console.log(res)
                    }, function (error) {
                        console.log(error);
                    })
                }
            }

            $scope.formatFileName = function (_fileName) {
                if (_fileName.length > 40) {
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
    ])
});