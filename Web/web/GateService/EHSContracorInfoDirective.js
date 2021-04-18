/**
 * Created by wangyanyan on 2017/2/18.
 */
define(['app', 'angular'], function (app, angular) {
    app.directive('myContractor', ['$resource', '$http', '$filter', 'Notifications', 'ConQuaService', '$routeParams', 'Auth', 'EngineApi', 'GateGuest', '$timeout', '$translate', '$location', 'Forms','$upload',
        function ($resource, $http, $filter, Notifications, ConQuaService, $routeParams, Auth, EngineApi, GateGuest, $timeout, $translate, $location, Forms,$upload) {
            return {

                restrict: 'E',

                controller: function ($scope, $upload) {
                    console.log("myContractor");
                    $scope.flowkey = "GateContractorInfoProcess";
                    var lang = window.localStorage.lang;

                    $scope.note = {};
                    $scope.tomorrow = new Date();
                    $scope.tomorrow = $filter('date')($scope.tomorrow.setDate($scope.tomorrow.getDate() + 1), 'yyyy/MM/dd');
                    console.log("tomorrow: " + $scope.tomorrow);

                    ConQuaService.ContractorTypeList().get({ kind: "Kind", language: lang }).$promise.then(function (res) {
                        $scope.KindList = res;
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });


                    ConQuaService.GetDepartment({ EmployeeID: Auth.username }, function (res) {
                        $scope.departID = res[0].DepartmentID;
                        console.log($scope.departID);
                        showEmployeeList($scope.departID);
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });

                    $scope.checkremove = false;

                    $scope.checkContractorName = function () {
                        if ($scope.projects.ContractorName == undefined || $scope.projects.ContractorName == '') {
                            $scope.checkremove = false;
                        }
                        else {
                            var query = {};
                            query.contractorName = $scope.projects.ContractorName
                            query.contractorID = $scope.projects.ContractorID || ""
                            ConQuaService.ContractorQualification().getByName(query).$promise.then(function (res) {
                                console.log(res)
                                if (res.ContractorID != null) {
                                    $scope.checkremove = true;
                                } else $scope.checkremove = false;
                            }, function (errResponse) {
                                Notifications.addError({ 'status': 'error', 'message': errResponse });
                            });
                        }
                    }

                    function showEmployeeList(dept_id) {
                        if (dept_id == null || dept_id == '') return;
                        ConQuaService.GetEmployee({
                            DepartmentID: dept_id
                        }, function (res) {
                            if (res.length > 0) {
                                $scope.listEmployee = res;
                                console.log($scope.listEmployee);
                            } else $scope.listEmployee = [];
                        })
                    }

                    $scope.saveSubmit = function (status) {
                        if (status == 'Q') {
                            if ($scope.projects.RenewalDate < $scope.projects.EndDate) {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant('RenewDate_MSG') });
                            }
                            else { $scope.SubmitAfterCheck(status) }
                        } else { $scope.SubmitAfterCheck(status) }
                    }

                    $scope.SubmitAfterCheck = function (status) {
                        GateGuest.GetGateCheckers().getCheckers({
                            owner: Auth.username,
                            fLowKey: $scope.flowkey,
                            Kinds: "",
                            CheckDate: ""
                        }).$promise.then(function (res) {
                            var leaderList = [];
                            for (var i = 0; i < res.length; i++) {
                                leaderList[i] = res[i].Person;
                            }
                            if (leaderList.length <= 0) {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant('Leader_NO_MSG') });
                                return
                            } else {
                                $scope.saveInfo(status, function (contractorID, obj) {
                                    if (obj && !contractorID) {
                                        Notifications.addError({ 'status': 'error', 'message': obj });
                                    } else {
                                        var formVariables = [];
                                        var historyVariable = [];
                                        console.log(leaderList);
                                        formVariables.push({ name: "ChecherArray", value: leaderList });
                                        formVariables.push({ name: "EmployerId", value: $scope.projects.ContractorID });
                                        formVariables.push({ name: "Employer", value: $scope.projects.ContractorID });

                                        historyVariable.push({ name: "ContractorName", value: $scope.projects.ContractorName });
                                        historyVariable.push({ name: "ContractorID", value: $scope.projects.ContractorID });

                                        if (status == 'Q') {
                                            formVariables.push({ name: "start_remark", value: "Renew" });
                                            formVariables.push({ name: "Renew_Date", value: $scope.projects.RenewalDate });
                                            historyVariable.push({ name: "RenewalDate", value: $scope.projects.RenewalDate });
                                        }
                                        else {
                                            formVariables.push({ name: "start_remark", value: "new contractor" });
                                        }

                                        $scope.getFlowDefinitionId($scope.flowkey, function (FlowDefinitionId) {
                                            if (FlowDefinitionId) {
                                                $scope.startflowid(FlowDefinitionId, 'abc', formVariables, historyVariable, function (url, message) {
                                                    if (message) {
                                                        Notifications.addError({ 'status': 'error', 'message': message });
                                                    } else {
                                                        var query = {}
                                                        var mail = '';
                                                        query.employerId = $scope.projects.ContractorID
                                                        if (status == 'N') {
                                                            query.status = 'F';
                                                            mail = "c_init";
                                                        }
                                                        else {
                                                            query.status = 'RN' // renew}
                                                            mail = "c_renew"
                                                        }
                                                        ConQuaService.ConQuaSaveStatus().save(query, {}).$promise.then(function (res) {
                                                            console.log(res);

                                                            ConQuaService.SendMailSubmit().get({
                                                                flowname: $scope.flowkey,
                                                                IdVoucher: $scope.projects.ContractorID,
                                                                FromUser: Auth.username,
                                                                MailKind: mail
                                                            }, {}).$promise.then(function (res) {
                                                                console.log(res);
                                                            }, function (err) {
                                                                Notifications.addError({
                                                                    'status': 'error',
                                                                    'message': 'error send mail: ' + err
                                                                });
                                                            });

                                                            $('#modalContractor').modal('hide');
                                                            $("#modalContractor").on('hidden.bs.modal', function () {
                                                                $location.path('/taskForm/' + url);
                                                                $scope.$apply();
                                                            });
                                                        }, function (errResponse) {
                                                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                                                        })
                                                    }
                                                })
                                            } else {
                                                Notifications.addError({ 'status': 'error', 'message': $translate.instant('Process_Err_MSG') });
                                            }
                                        })
                                    }
                                });
                            }
                        })
                    }

                    /* Check before save*/
                    function IsCanSave(callback) {
                        if ($scope.projects.StartDate > $scope.projects.EndDate) {
                            callback($translate.instant('Date_Msg'))
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

                    $scope.saveInfo = function(Status, callback) {
                        IsCanSave(function (errmsg) {
                            if (errmsg) {
                                callback("", errmsg);
                                return;
                            } else {
                                if ($scope.removeList.length > 0) {
                                    $scope.removeFile($scope.removeList[0]);
                                }
                                $scope.projects.UserID = Auth.username;
                                $scope.projects.DepartmentID = $scope.departID;
                                $scope.projects.Status = Status;
                                $scope.projects.isvalid = 1;
                                if ($scope.fileAttached != "") {
                                    $scope.projects.ContractorFile = $scope.fileAttached;
                                } else $scope.projects.ContractorFile = null;

                                $scope.projects.AccDate = $filter('date')(new Date(), "yyyy-MM-dd HH:mm:ss");

                                ConQuaService.CreateContractorQualification().save($scope.projects).$promise.then(function (rese) {
                                    $scope.projects.ContractorID = JSON.parse(rese.contractorID).contractorID;
                                    callback($scope.projects.ContractorID, "")
                                }, function (errResponse) {
                                    callback("", errResponse);
                                });
                            }
                        })
                    }

                    $scope.saveDraft = function () {
                        var _state = $scope.readUpdateQ ? 'Q' : 'N'
                        $scope.saveInfo(_state, function (contractorID, errMessage) {
                            if (errMessage && !contractorID) {
                                $timeout(function () {
                                    Notifications.addError({ 'status': 'error', 'message': errMessage });
                                }, 400);

                            } else {
                                $('#modalContractor').modal('hide');
                                $scope.QueryInfo();
                                $scope.ResetContractor();
                                $timeout(function () {
                                    Notifications.addMessage({
                                        'status': 'information',
                                        'message': $translate.instant('Save_Success_MSG')
                                    });
                                }, 500);
                            }
                        })

                    }

                    $scope.loadContractorDetail = function (_contractorID, _userID, _status) {
                        ConQuaService.ContractorQualification().getByName({ "contractorID": _contractorID }).$promise.then(function (res) {
                            $scope.projects = res;
                            $scope.projects.StartDate = $filter('date')(res.StartDate, 'yyyy-MM-dd');
                            $scope.projects.EndDate = $filter('date')(res.EndDate, 'yyyy-MM-dd');
                            if ($scope.projects.ContractorFile != null) {
                                $scope.fileAttached = $scope.projects.ContractorFile;
                            }
                            console.log($scope.fileAttached);

                            if (_status == "Q") {
                                $scope._IsSubmit = false;
                                $scope.readUpdateQ = true;
                            }
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }

                    $scope.ResetContractor = function () {
                        $scope.readUpdateQ = false;
                        $scope.projects = {};
                        $scope.fileAttached = "";
                        $scope.removeList = [];
                        $scope.file = [];
                    }

                },
                templateUrl: './forms/ConQua/new.html'
            }
        }]);
});