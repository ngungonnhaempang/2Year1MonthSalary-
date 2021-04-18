/**
 * Created by wang.chen on 2017/2/21.
 */
define(['app', 'angular', 'xlsx'], function (app, angular) {
    app.directive('mycontractorqua', ['$resource', '$http', '$filter', 'Notifications', 'ConQuaService', '$routeParams', '$translate', '$timeout', 'GateGuest', 'EngineApi', '$location', 'Auth', 'Forms', '$anchorScroll', '$route', '$upload',
        function ($resource, $http, $filter, Notifications, ConQuaService, $routeParams, $translate, $timeout, GateGuest, EngineApi, $location, Auth, Forms, $anchorScroll, $route, $upload) {
            return {
                restrict: 'E',

                controller: function ($scope) {
                    $scope.flowkey = "GateContractorQuaProcess";
                    $scope.event = {};
                    $scope.record = {};
                    $scope.note = {};
                    $scope.voucherID = "";
                    $scope.filename = "";
                    $scope.ConquaName = "";
                    $scope.EmpStatus = "";
                    $scope.EmployeeList = [];
                    $scope._showUploadButton = true;
                    $scope.bpmnloaded = false;
                    $scope.IsUpdate = false;
                    $scope.checkremove = false;
                    $scope.valid = true;
                    $scope.IdList = [];
                    $scope.IsNotice = false;
                    $scope.modify = true;
                    $scope.tomorrow = new Date();
                    $scope.tomorrow = $filter('date')($scope.tomorrow.setDate($scope.tomorrow.getDate() + 1), 'yyyy/MM/dd');
                    $scope.today = $filter('date')(new Date(), 'yyyy-MM-dd');

                    $scope.$watch('record.ContractorName', function (n) {
                        if (n != undefined && $scope.record.Foreign != undefined) {
                            $scope._showUploadButton = false;
                        }
                    })

                    $scope.$watch('record.Foreign', function (n) {
                        if (n != undefined) {
                            if ($scope.record.ContractorName != undefined) {
                                $scope._showUploadButton = false;
                            }

                            $scope.event = {};

                            if (n == '1') {
                                $scope.IsVietnamese = false;
                            }
                            else {
                                $scope.IsVietnamese = true;
                                $scope.getRemoveFileName();
                            }

                            if (!$scope._IsUpdate) {
                                $scope.filename = "";
                                $scope.EmployeeList = [];
                                $scope.IsNotice = false;
                            }

                        }
                    })

                    $scope.$watch('IsNotice', function (n) {
                        if (n != undefined && n == true) {
                            $scope.gotoIndex('bottomPage');
                        }
                    })

                    $scope.$watch('event.Region', function (n) {
                        $scope.checkIDNoFromTextbox()
                    })

                    $scope.loadContractorByDept = function () {
                        ConQuaService.GetDepartment({ EmployeeID: Auth.username }, function (res) {
                            var cq = {};
                            cq.contractorName = "";
                            cq.cType = "";
                            cq.language = window.localStorage.lang;
                            cq.status = "Q";
                            cq.userid = "";
                            if ($scope.IsChange) {
                                cq.departmentID = "";
                            } else {
                                cq.departmentID = res[0].DepartmentID;
                            }
                            console.log(cq.departmentID);

                            ConQuaService.GetContractorQualification().get(cq).$promise.then(function (res) {
                                $scope.ContractorList = res;
                            }, function (errResponse) {
                                Notifications.addError({ 'status': 'error', 'message': errResponse });
                            });
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }


                    /* Check ID Card */
                    var format = /[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/;

                    $scope.checkIDNoFromTextbox = function () {
                        debugger;
                        console.log("spec: " + format.test($scope.event.IdCard));

                        if ($scope.event.IdCard == undefined || $scope.event.IdCard == '') {
                            $scope.checkremove = false;
                        }
                        else if (format.test($scope.event.IdCard)) {
                            $scope.checkremove = true;
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant('SpecialChar_MSG')
                            });
                        } else {
                            $scope.checkremove = false;
                            ConQuaService.ContractorList().get({
                                VoucherID: "",
                                ContractorID: "",
                                Language: window.localStorage.lang,
                                Status: "",
                                UserId: "",
                                Name: "",
                                IdCard: $scope.event.IdCard,
                                Region: "",
                                Fromdate: "",
                                Todate: "",
                                Classify: ""
                            }).$promise.then(function (res) {
                                console.log(res);
                                if (res.length > 0) {
                                    if ($scope.record.ContractorName != null || $scope.record.ContractorName != undefined) {
                                        for (let index = 0; index < res.length; index++) {
                                            const x = res[index];
                                            if ((x.StartDate >= $scope.today || x.EndDate >= $scope.today) && x.Status != 'O' && x.Status != 'X' && x.Status != 'E') {
                                                if (x.ContractorID == $scope.record.ContractorName) {
                                                    if ($scope.event.EmployeeID != x.EmployeeID) {
                                                        $scope.checkremove = true;
                                                        Notifications.addError({
                                                            'status': 'error',
                                                            'message': $translate.instant('IDCard_InDB_MSG')
                                                        });
                                                        break;
                                                    }
                                                } else if ($scope.event.EmployeeID != x.EmployeeID) {
                                                    if ($scope.event.Name.toUpperCase().trim() != x.Name.trim()) {
                                                        $scope.checkremove = true;
                                                        Notifications.addError({
                                                            'status': 'error',
                                                            'message': $translate.instant('IDCard_Name_MSG') + $scope.event.Name + ' _ ' + x.Name + $translate.instant('IDCard_Same_MSG')
                                                        });
                                                        break;
                                                    } else {
                                                        if ($scope.event.Region != undefined) {
                                                            if (x.RegionID == "3") {
                                                                $scope.checkremove = true;
                                                                Notifications.addError({
                                                                    'status': 'error',
                                                                    'message': $translate.instant('RegionAll_MSG')
                                                                });
                                                                break;
                                                            } else if ($scope.event.Region == "3") {
                                                                $scope.checkremove = true;
                                                                Notifications.addError({
                                                                    'status': 'error',
                                                                    'message': $translate.instant('Region2nd_MSG')
                                                                });
                                                                break;
                                                            } else if (x.RegionID == $scope.event.Region) {
                                                                $scope.checkremove = true;
                                                                Notifications.addError({
                                                                    'status': 'error',
                                                                    'message': $translate.instant('RegionDuplicate_MSG')
                                                                });
                                                                break;
                                                            }
                                                        }

                                                        $scope.event.IsMultiple = false;
                                                        $scope.event.Isvalid = true;
                                                        Notifications.addError({
                                                            'status': 'error',
                                                            'message': $translate.instant('IDCard_InDB_MSG')
                                                        });
                                                        break;
                                                    }
                                                }
                                            }
                                            else {
                                                if (x.Status == 'PC' || x.Status == 'C') {
                                                    $scope.checkremove = true;
                                                    Notifications.addError({
                                                        'status': 'error',
                                                        'message': $translate.instant('IDCard_Name_MSG') + x.Name + $translate.instant('Be_Suspended_MSG')
                                                    });
                                                    break;
                                                }
                                            }
                                        }
                                    } else {
                                        Notifications.addError({
                                            'status': 'error',
                                            'message': $translate.instant('Select_conqua')
                                        });
                                    }
                                } else {
                                    $scope.event.IsMultiple = true;
                                }
                            })

                            for (let index = 0; index < $scope.EmployeeList.length; index++) {
                                const x = $scope.EmployeeList[index];
                                if ($scope.event.IdCard == x.IdCard) {
                                    $scope.checkremove = true;
                                    $timeout(function () {
                                        Notifications.addError({
                                            'status': 'error',
                                            'message': $translate.instant('IDCard_Duplicate_MSG') + x.Name
                                        });
                                    }, 300);
                                    break;
                                } else if ($scope.event.Card_No != undefined) {
                                    if ($scope.event.Card_No == x.Card_No) {
                                        $scope.checkremove = true;
                                        $timeout(function () {
                                            Notifications.addError({
                                                'status': 'error',
                                                'message': $translate.instant('SameVisa') + x.Name
                                            });
                                        }, 300);
                                        break;
                                    }
                                } else if ($scope.event.WorkPermit_No != undefined) {
                                    if ($scope.event.WorkPermit_No == x.WorkPermit_No) {
                                        $scope.checkremove = true;
                                        $timeout(function () {
                                            Notifications.addError({
                                                'status': 'error',
                                                'message': $translate.instant('SameWorkPermit') + x.Name
                                            });
                                        }, 300);
                                        break;
                                    }
                                }
                            }

                        }
                    }

                    $scope.validDate = function (contractorID) {
                        var query = {};
                        query.language = window.localStorage.lang;
                        query.contractorID = contractorID;
                        ConQuaService.ContractorQualification().get(query).$promise.then(function (res) {
                            $scope.record.StartDate = res[0].StartDate;
                            $scope.record.EndDate = res[0].EndDate;
                            $scope.ConquaName = res[0].ContractorName;
                            console.log("contractor name: " + $scope.ConquaName);
                        }, function (errResponse) {
                            Notifications.addError({
                                'status': 'error',
                                'message': errResponse
                            });
                        });

                        if ($scope.EmployeeList.length > 0) {
                            $scope.checkIdCardInDb();
                        }

                        if ($scope.event.IdCard != undefined) {
                            $scope.checkIDNoFromTextbox();
                        }
                    }

                    $scope.getEmployee = function (index) {
                        var element = $scope.EmployeeList[index];
                        $scope.event.Name = element.Name;
                        $scope.event.IdCard = element.IdCard;
                        $scope.event.Phone = element.Phone;
                        $scope.event.Birthday = $filter('date')(element.Birthday, 'yyyy-MM-dd');
                        $scope.event.Sex = element.Sex;
                        $scope.event.Job = element.Job;
                        $scope.event.Region = element.Region;
                        $scope.event.State = element.State;
                        $scope.event.Remark = element.Remark;
                        $scope.event.EmployeeID = element.EmployeeID;
                        $scope.event.InsuranceDuration = element.InsuranceDuration;
                        $scope.event.SafetyCerDuration = element.SafetyCerDuration;
                        $scope.event.IsMultiple = true;
                        if (!$scope.IsVietnamese) {
                            $scope.event.PassPort_Nationality = element.PassPort_Nationality;
                            $scope.event.PassPort_Expiry = element.PassPort_Expiry;
                            $scope.event.WorkPermit_No = element.WorkPermit_No;
                            $scope.event.WorkPermit_Start = element.WorkPermit_Start;
                            $scope.event.WorkPermit_End = element.WorkPermit_End;
                            $scope.event.CategoryCard = element.CategoryCard;
                            $scope.event.Card_Type = element.Card_Type;
                            $scope.event.Card_No = element.Card_No;
                            $scope.event.Card_Start = element.Card_Start;
                            $scope.event.Card_End = element.Card_End;
                        }
                        $scope.EmployeeList.splice(index, 1);
                        if (element.EmployeeID == null) {
                            $scope.checkIDNoFromTextbox();
                        }
                        $scope.gotoIndex('topPage');
                    };

                    $scope.gotoIndex = function (index) {
                        $location.hash(index);
                        $anchorScroll();
                        $location.hash('');
                        $location.replace();
                    };

                    $scope.addEmployee = function () {
                        $scope.event.IsNotice = false;
                        $scope.EmployeeList.push($scope.event);
                        $scope.event = {};
                        $scope.checkRedRow();
                        $scope.checkIdCardInDb();
                        $scope.gotoIndex('bottomPage');
                    };

                    $scope.IdList = [];
                    $scope.deleteEmployee = function (index) {
                        $scope.IdList.push($scope.EmployeeList[index].IdCard);
                        $scope.EmployeeList.splice(index, 1);
                        $scope.checkRedRow();
                        $scope.checkIdCardInDb();
                    };

                    $scope.checkEmployeeList = function () {
                        if ($scope.EmployeeList.length > 0 && $scope.record.ContractorName != undefined && $scope.valid == true) {
                            if ($scope.IsVietnamese) {
                                return false;
                            } else if (!$scope.IsVietnamese && $scope.fileAttached != "") {
                                return false;
                            }
                            else return true;

                        }
                        else return true;
                    }

                    $scope.checkRedRow = function () {
                        var _flag = false;
                        $scope.IsNotice = false;
                        if ($scope.EmployeeList.length > 0 && $scope.record.ContractorName != undefined) {
                            $scope.EmployeeList.forEach(x => {
                                if (x.Isvalid == false) {
                                    _flag = true;
                                    return;
                                }

                                if (x.IsMultiple == false) {
                                    $scope.IsNotice = true;
                                }

                            })
                        }

                        if (_flag) {
                            $scope.IsNotice = true;
                            $scope.valid = false;
                        }
                        else {
                            $scope.valid = true;
                        }
                    }

                    $scope.checkIdCardYellow = function () {
                        $scope.EmployeeList.forEach(y => {
                            ConQuaService.ContractorList().get({
                                VoucherID: "",
                                ContractorID: "",
                                Language: window.localStorage.lang,
                                Status: "",
                                UserId: "",
                                Name: "",
                                IdCard: y.IdCard,
                                Region: "",
                                Fromdate: "",
                                Todate: "",
                                Classify: ""
                            }).$promise.then(function (res) {
                                console.log(res)
                                res.forEach(x => {
                                    if (x.ContractorID != $scope.record.ContractorName && x.Status != 'O' && x.Status != 'X') {
                                        if (x.VoucherID == $scope.voucherID) {
                                            y.IsMultiple = true;
                                            $scope.IsNotice = true;
                                        } else {
                                            y.IsMultiple = false;
                                        }
                                    }
                                })
                            })

                        });
                    }

                    $scope.loadVoucherDetail = function (Voucher_id, Card_id, _status) {
                        if (_status == 'N' || _status == 'RT') {
                            Card_id = '';
                        }
                        $scope.loadContractorByDept();
                        ConQuaService.GetContractorUpdate().get({
                            VoucherID: Voucher_id,
                            IdCard: Card_id,
                            Language: window.localStorage.lang
                        }).$promise.then(function (res) {
                            $scope.record.ContractorName = res[0].ContractorID;
                            $scope.record.StartDate = res[0].StartDate;
                            $scope.record.EndDate = res[0].EndDate;
                            $scope.EmployeeList = [];
                            $scope.voucherID = Voucher_id;
                            $scope.EmpStatus = res[0].Status;
                            $scope.record.Foreign = res[0].IsForeign;
                            $scope.ConquaName = res[0].ContractorName;
                            if (res[0].FileName != null && res[0].FileName != "") {
                                $scope.fileAttached = res[0].FileName;
                            }
                            res.forEach(element => {
                                var x = {};
                                x.VoucherID = element.VoucherID;
                                x.Name = element.Name;
                                x.IdCard = element.IdCard;
                                x.Sex = element.Sex;
                                x.Birthday = element.Birthday;
                                x.Job = element.Job;
                                x.Region = element.Region;
                                x.Remark = element.Remark;
                                x.ReasonReturn = element.ReasonReturn;
                                x.EmployeeID = element.EmployeeID;
                                x.InsuranceDuration = element.InsuranceDuration;
                                x.SafetyCerDuration = element.SafetyCerDuration;
                                x.Phone = element.Phone;
                                if (element.ReasonReturn != "") {
                                    $scope.isReturn = true;
                                }

                                if ($scope.record.Foreign == '1') {
                                    x.PassPort_Expiry = element.PassPort_Expiry;
                                    x.PassPort_Nationality = element.PassPort_Nationality;
                                    x.WorkPermit_No = element.WorkPermit_No;
                                    x.WorkPermit_Start = element.WorkPermit_Start;
                                    x.WorkPermit_End = element.WorkPermit_End;
                                    x.CategoryCard = element.CategoryCard;
                                    x.Card_Type = element.Card_Type;
                                    x.Card_No = element.Card_No;
                                    x.Card_Start = element.Card_Start;
                                    x.Card_End = element.Card_End;
                                }
                                $scope.EmployeeList.push(x);
                            });

                            $scope._IsUpdate = true;

                            $scope.checkIdCardYellow();
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    };

                    $scope.saveDraft = function () {					
                        if ($scope.EmpStatus == 'P') {
                            status = 'W';

                            if ($scope.EmployeeList[0].InsuranceDuration < $scope.today || ($scope.EmployeeList[0].SafetyCerDuration && $scope.EmployeeList[0].SafetyCerDuration < $scope.today)) {
                                status = 'P'
                            }

                            if (!$scope.IsVietnamese) {
                                if ($scope.EmployeeList[0].Card_End < $scope.today) {
                                    status = 'P'
                                }
                            }
                        } else if ($scope.EmpStatus == 'W') {
                            status = 'W'
                        } else if ($scope.EmpStatus == 'RT') {
                            status = 'RT'
                        }
                        else status = 'N'

                        saveVoucher(status, function (result) {
                            console.log(result);
                            if (result != '') {
                                $timeout(function () {
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': $translate.instant('Msg_Save')
                                    });
                                }, 400);
                            }
                            else {
                                $('#modalContractorEmployee').modal('hide');
                                $scope.ResetEmployeeModal();
                                $scope.SearchEmployee();
                                $timeout(function () {
                                    Notifications.addMessage({
                                        'status': 'information',
                                        'message': $translate.instant('Save_Success_MSG')
                                    });
                                }, 1000);
                            }
                        })
                    }

                    $scope.uploadFileContractor = function (callback) {
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

                    function saveVoucher(status, callback) {
                        $scope.employees = [];
                        if ($scope.removeList.length > 0) {
                            $scope.removeFile($scope.removeList[0]);
                        }

                        $scope.uploadFileContractor(function (result) {
                            console.log(result);
                            if (result != '') {
                                $timeout(function () {
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': $translate.instant('Msg_Save')
                                    });
                                }, 400);
                            }
                            else {
                                $scope.EmployeeList.forEach(element => {
                                    var query = {};
                                    query.ContractorID = $scope.record.ContractorName;
                                    query.IsUpdate = $scope.IsUpdate;
                                    query.Status = status;
                                    query.UserId = Auth.username;
                                    query.Name = element.Name.toUpperCase();
                                    query.IdCard = element.IdCard;
                                    query.Birthday = element.Birthday;
                                    query.Sex = element.Sex;
                                    query.Job = element.Job;
                                    query.InsuranceDuration = element.InsuranceDuration;
                                    query.SafetyCerDuration = element.SafetyCerDuration;
                                    query.Region = element.Region;
                                    query.ReasonReturn = null;
                                    query.Remark = element.Remark || null;
                                    query.VoucherID = $scope.voucherID;
                                    if ($scope._IsUpdate) {
                                        query.EmployeeID = element.EmployeeID;
                                    }
                                    query.IsForeign = $scope.record.Foreign;
                                    if (!$scope.IsVietnamese) {
                                        query.PassPort_Expiry = element.PassPort_Expiry;
                                        query.PassPort_Nationality = element.PassPort_Nationality;
                                        query.WorkPermit_No = element.WorkPermit_No;
                                        query.WorkPermit_Start = element.WorkPermit_Start;
                                        query.WorkPermit_End = element.WorkPermit_End;
                                        query.CategoryCard = element.CategoryCard;
                                        query.Card_Type = element.Card_Type;
                                        query.Card_No = element.Card_No;
                                        query.Card_Start = element.Card_Start;
                                        query.Card_End = element.Card_End;
                                    }
                                    if ($scope.fileAttached != "") {
                                        query.FileName = $scope.fileAttached;
                                    } else query.FileName = null;
                                    
                                    query.Phone = element.Phone || null;

                                    $scope.employees.push(query);
                                })
                               
                                var _employeeList = $scope.employees;
                               
                                ConQuaService.SaveContractor().save({},{
                                    _employeeList
                                }).$promise.then(function (res) {
                                    console.log("success");
                                    console.log(res);
                                    callback('');
        
                                }, function (errResponse) {
                                    callback(errResponse)
                                });
                                // saveEmployees($scope.employees, 0, function (MessageResponse) {
                                //     callback(MessageResponse);
                                // });
                            }
                        })

                    }

                    function saveEmployees(employees, i, callback007) {
                        debugger;
                        ConQuaService.SaveContractor().save({}, employees[i]).$promise.then(function (res) {
                            console.log("success");
                            console.log(res);
                            i++;
                            if (i < employees.length) {
                                saveEmployees(employees, i, callback007);
                            } else callback007('');

                        }, function (errResponse) {
                            callback007(errResponse)
                        });
                    }

                    function CheckIdList(callback) {
                        $scope.EmployeeList.forEach(element => {
                            $scope.IdList.push(element.IdCard.toString());
                        });
                        callback($scope.IdList);
                    }

                    $scope.saveSubmit = function () {
                        CheckIdList(function (idEmployee) {
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
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': $translate.instant('Leader_NO_MSG')
                                    });
                                    return
                                } else {
                                    if ($scope.EmpStatus == 'RT') {
                                        status = 'RT';
                                        leaderList = [];
                                    }
                                    else status = 'N'
                                    saveVoucher(status, function (result) {
                                        console.log(result);
                                        if (result != '') {
                                            $timeout(function () {
                                                Notifications.addError({
                                                    'status': 'error',
                                                    'message': $translate.instant('Msg_Save')
                                                });
                                            }, 400);
                                        }
                                        else {
                                            var EHSList = [];
                                            if ($scope.record.Foreign == "1") {
                                                GateGuest.GetGateCheckerByKind("ContractorHR", function (hrList, errormsg) {
                                                    if (errormsg) {
                                                        Notifications.addError({ 'status': 'error', 'message': errormsg });
                                                        return;
                                                    } else {
                                                        EHSList.push(hrList);
                                                        GetEHSandSubmit(leaderList, EHSList, idEmployee);
                                                    }
                                                })
                                            }
                                            else GetEHSandSubmit(leaderList, EHSList, idEmployee);
                                        }
                                    })
                                }
                            });
                        });
                    };

                    function GetEHSandSubmit(leaderList, EHSList, idEmployee) {
                        GateGuest.GetGateCheckerByKind("Contractor", function (reslen, errormsg) {
                            if (errormsg) {
                                Notifications.addError({ 'status': 'error', 'message': errormsg });
                                return;
                            } else {
                                var formVariables = [];
                                var historyVariable = [];

                                EHSList.push(reslen);
                                formVariables.push({ name: "safetyleader", value: EHSList });
                                formVariables.push({ name: "start_remark", value: $scope.ConquaName });
                                formVariables.push({ name: "checker", value: leaderList });
                                formVariables.push({ name: "eventStart_Employer", value: $scope.voucherID });
                                formVariables.push({ name: "eventStart_IdCard", value: idEmployee });

                                historyVariable.push({ name: "VoucherID", value: $scope.voucherID });

                                if ($scope.EmpStatus == 'RT') {
                                    formVariables.push({ name: "agree", value: "YES" });
                                    historyVariable.push({ name: "State", value: "resubmit" });
                                }

                                $scope.getFlowDefinitionId($scope.flowkey, function (FlowDefinitionId) {
                                    if (FlowDefinitionId) {
                                        debugger;
                                        $scope.startflowid(FlowDefinitionId, "", formVariables, historyVariable, function (url, message) {
                                            if (message) {
                                                Notifications.addError({ 'status': 'error', 'message': message });
                                            } else {
                                                ConQuaService.ContractorDone().save({
                                                    VoucherID: $scope.voucherID,
                                                    updateDate: "",
                                                    idCard: idEmployee,
                                                    status: "F"
                                                }).$promise.then(function (res) {
                                                    console.log(res);
                                                    if ($scope.EmpStatus == 'RT') {
                                                        sendMailSubmit("c_ReSubmit", $scope.voucherID)
                                                    } else {
                                                        sendMailSubmit("c_init", $scope.voucherID)
                                                    }

                                                    $('#modalContractorEmployee').modal('hide');
                                                    $("#modalContractorEmployee").on('hidden.bs.modal', function () {
                                                        $location.path('/taskForm/' + url);
                                                        $scope.$apply();
                                                    });

                                                }, function (errResponse) {
                                                    Notifications.addError({
                                                        'status': 'error',
                                                        'message': errResponse
                                                    });
                                                });

                                            }
                                        })
                                    } else {
                                        Notifications.addError({
                                            'status': 'error',
                                            'message': $translate.instant('Process_Err_MSG')
                                        });
                                        return;
                                    }
                                })
                            }
                        })
                    }

                    function sendMailSubmit(_mailKind, _idvoucher) {
                        ConQuaService.SendMailSubmit().get({
                            flowname: "GateContractorQuaProcess",
                            IdVoucher: _idvoucher,
                            FromUser: Auth.username,
                            MailKind: _mailKind
                        }, {}).$promise.then(function (res) {
                            console.log(res);
                        }, function (err) {
                            Notifications.addError({
                                'status': 'error',
                                'message': 'Send Mail Err: ' + err
                            });
                        });
                    }

                    $scope.err = '';
                    $scope._msgSameID = '';
                    $scope._msgInDB = '';
                    $scope._msgPC = '';
                    $scope._msgRegion = '';
                    $scope.checkIdCardInDb = function () {
                        $scope.err = '';
                        $scope._msgSameID = '';
                        $scope._msgInDB = '';
                        $scope._msgPC = '';
                        $scope._msgRegion = '';
                        checkIsValidID($scope.EmployeeList, 0, function (result) {
                            if (result != '') {
                                Notifications.addError({ 'status': 'error', 'message': result });
                            } else {
                                
                                var _HasNotice = $filter("filter")($scope.EmployeeList, { IsNotice: true });
                                
                                if(_HasNotice.length > 0){
                                    $scope.valid = false;
                                    $scope.IsNotice = true;
                                }
                             
                                if ($scope._msgInDB != '') {
                                    console.log($scope._msgInDB);
                                    $scope._msgErr = $translate.instant('IDCard_InDB_MSG');
                                    // $scope.err = $scope.err + '<br></br>' + $scope._msgErr;
                                }

                                if ($scope._msgSameID != '') {
                                    console.log($scope._msgSameID);
                                    $scope._msgErr = $translate.instant('IDCard_Name_MSG') + $scope._msgSameID
                                    // $scope.err = $scope.err + '<br></br>' + $scope._msgErr;
                                }

                                if ($scope._msgPC != '') {
                                    console.log($scope._msgPC);
                                    $scope._msgErrPC = $translate.instant('IDCard_Name_MSG') + $scope._msgPC + $translate.instant('Permanent_Cancel');
                                    // $scope.err = $scope.err + '<br></br>' + $scope._msgErrPC;
                                }

                                if ($scope._msgRegion != '') {
                                    console.log($scope._msgRegion);
                                    $scope.err = $scope.err + '<br>' + $scope._msgRegion;
                                }

                                if ($scope.err != '') {
                                    console.log($scope.err);
                                    $timeout(function () {
                                        Notifications.addError({ 'status': 'error', 'message': $scope.err });
                                        return;
                                    }, 1000);
                                }
                            }


                        })

                    }

                    function checkIsValidID(employeeList, i, callback) {
                        var y = employeeList[i];
                        var isRed = false;
                        ConQuaService.ContractorList().get({
                            VoucherID: "",
                            ContractorID: "",
                            Language: window.localStorage.lang,
                            Status: "",
                            UserId: "",
                            Name: "",
                            IdCard: y.IdCard,
                            Region: "",
                            Fromdate: "",
                            Todate: "",
                            Classify: ""
                        }).$promise.then(function (res) {
                            console.log(res);
                            if (res.length > 0) {
                                res.forEach(x => {
                                    if ((x.StartDate >= $scope.today || x.EndDate >= $scope.today) && x.Status != 'O' && x.Status != 'X') {
                                        if (x.ContractorID == $scope.record.ContractorName) {
                                            if (y.EmployeeID != x.EmployeeID) {
                                                $scope._msgInDB = y.Name;
                                                isRed = true;
                                            }
                                        } else if (y.EmployeeID != x.EmployeeID) {
                                            if (y.Name.toUpperCase().trim() != x.Name.toUpperCase().trim()) {
                                                $scope._msgSameID = $scope._msgSameID + '<br>' + y.Name + ' _ ' + x.Name + $translate.instant('IDCard_Same_MSG');
                                                isRed = true;
                                            } else {
                                                $scope._msgInDB = y.Name;
                                                y.IsMultiple = false;
                                                $scope.IsNotice = true;

                                                if (x.RegionID == "3") {
                                                    $scope._msgRegion = $scope._msgRegion + y.Name + ': ' + $translate.instant('RegionAll_MSG') + '<br>';
                                                    isRed = true;
                                                } else if (y.Region == "3") {
                                                    $scope._msgRegion = $scope._msgRegion + y.Name + ': ' + $translate.instant('Region2nd_MSG') + '<br>';
                                                    isRed = true;
                                                } else if (x.RegionID == y.Region) {
                                                    $scope._msgRegion = $scope._msgRegion + y.Name + ': ' + $translate.instant('RegionDuplicate_MSG') + '<br>';
                                                    isRed = true;
                                                }
                                            }
                                        }
                                    }
                                    else {
                                        if (x.Status == 'PC' || x.Status == 'C') {
                                            $scope._msgPC = $scope._msgPC + y.Name + ' _ ';
                                            isRed = true;
                                        }

                                    }
                                })
                            }

                            if (isRed || y.IsSame) {
                                y.IsMultiple = true;
                                y.Isvalid = false;
                                $scope.valid = false;
                                $scope.IsNotice = true;
                            } else {
                                y.Isvalid = true;
                                $scope.valid = true;
                            }
                            i++;
                            if (i < employeeList.length) {
                                checkIsValidID(employeeList, i, callback)
                            } else callback('');
                        }, function (errResponse) {
                            callback(errResponse)
                        });
                    }

                    $scope.ResetEmployeeModal = function () {
                        $scope.record = {};
                        $scope.event = {};
                        $scope.filename = "";
                        $scope.EmployeeList = [];
                        $scope._showUploadButton = true;
                        $scope._IsUpdate = false;
                        $scope.checkremove = false;
                        $scope.IsChange = false;
                        $scope.IsNotice = false;
                        $scope.IsVietnamese = true;
                        $scope.fileAttached = "";
                        $scope.removeList = [];
                        $scope.file = [];

                    }
                },
                templateUrl: './forms/ContractorQua/new.html'
            }
        }]);

    app.directive('uploadFile', ['Notifications', '$timeout', 'ConQuaService', '$translate', '$filter',
        function (Notifications, $timeout, ConQuaService, $translate, $filter) {
            return {
                link: function (scope, element) {
                    console.log(element);
                    element.bind("change", function (changeEvent) {
                        var file = element[0].files[0];
                        element.val(null);
                        scope.filename = file.name;
                        if (file.type == "application/vnd.ms-excel" ||
                            file.type == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {
                            var reader = new FileReader();
                            scope.employee = {};
                            if (!scope._IsUpdate) {
                                scope.EmployeeList = [];
                            }
                            reader.onload = function (e) {
                                var data = e.target.result;
                                var workbook = XLSX.read(data, { type: 'binary', cellDates: true, cellNF: false, cellText: false });
                                var first_sheetname = workbook.SheetNames[0];
                                var worksheet = workbook.Sheets[first_sheetname];
                                var result = XLSX.utils.sheet_to_json(worksheet, { header: 1, raw: false, dateNF: 'YYYY-MM-DD' });

                                for (var i = 1; i < result.length; i++) {
                                    var details = {};
                                    var item = result[i];

                                    if (item[0] == null) {
                                        break;
                                    } else {
                                        details.EmployeeID = "";
                                        details.Isvalid = true;
                                        details.IsMultiple = true;
                                        details.Isnull = true;
                                        details.IsnullSex = true;
                                        details.IsnullBd = true;
                                        details.IsnullJob = true;
                                        details.IsnullRegion = true;
                                        details.IsSame = false;
                                        details.IsnullPhone = true;

                                        if (item[1] != null) {
                                            details.Name = item[1].toString().replace(/[&\/\\#,+()$~%.'":*?<>{}!]/g, '').trim();

                                        }
                                        else {
                                            console.log("missing name");
                                            break;
                                        }

                                        if (item[2] != null) {
                                            details.IdCard = item[2].toString().replace(/[&\/\\#,+()$~%.'":*?<>{}!\s]/g, '');

                                            var _duplicateIdCard = $filter("filter")(scope.EmployeeList, { IdCard: details.IdCard });    
                                            if (_duplicateIdCard.length > 0) {
                                                details.IsSame = true;
                                            }
                                        }
                                        else {
                                            console.log("missing idcard");
                                            break;
                                        }

                                        if (item[3] != null) {
                                            details.Sex = item[3];
                                        }
                                        else {
                                            details.Sex = null;
                                            details.IsnullSex = false;
                                            details.IsNotice = true;
                                        }

                                        if (item[4] != null) {
                                            details.Birthday = item[4];
                                        }
                                        else {
                                            details.Birthday = null;
                                            details.IsnullBd = false;
                                            details.IsNotice = true;
                                        }

                                        if (item[5] != null) {
                                            details.Job = item[5];
                                        }
                                        else {
                                            details.Job = null;
                                            details.IsnullJob = false;
                                            details.IsNotice = true;
                                        }

                                        if (item[6] != null) {
                                            details.InsuranceDuration = item[6];
                                        }
                                        else {
                                            details.InsuranceDuration = null;
                                            details.Isnull = false;
                                            details.IsNotice = true;
                                        }

                                        if (item[7] != null) {
                                            details.SafetyCerDuration = item[7];
                                        }
                                        else details.SafetyCerDuration = null;


                                        if (item[8] != null) {
                                            details.RegionName = item[8].toLowerCase();
                                            if (details.RegionName.includes("m")) {
                                                details.Region = "1";
                                            }
                                            else if (details.RegionName.includes("b")) {
                                                details.Region = "2";
                                            } else if (details.RegionName.includes("t")) {
                                                details.Region = "3";
                                            }
                                        }
                                        else {
                                            details.RegionName = null;
                                            details.IsnullRegion = false;
                                            details.IsNotice = true;
                                        }

                                        details.Remark = item[9] || null;

                                        if (!scope.IsVietnamese) {
                                            details.IsnullNation = true;
                                            details.IsnullPPDate = true;
                                            details.IsnullCategoryCard = true;
                                            details.IsnullCardType = true;
                                            details.IsnullCardNo = true;
                                            details.IsnullCardStart = true;
                                            details.IsnullCardEnd = true;

                                            if (item[10] != null) {
                                                details.PassPort_Nationality = item[10].toString().replace(/[&\/\\#,+()$~%.'":*?<>{}!]/g, '').trim();
                                            }
                                            else {
                                                details.PassPort_Nationality = null;
                                                details.IsnullNation = false;
                                                details.IsNotice = true;
                                            }

                                            if (item[11] != null) {
                                                details.PassPort_Expiry = item[11];
                                            }
                                            else {
                                                details.PassPort_Expiry = null;
                                                details.IsnullPPDate = false;
                                                details.IsNotice = true;
                                            }

                                            if (item[12] != null) {
                                                details.WorkPermit_No = item[12].toString().replace(/[&\/\\#,+()$~%.'":*?<>{}!\s]/g, '');
                                                var _duplicateWorkPermit = $filter("filter")(scope.EmployeeList, { WorkPermit_No: details.WorkPermit_No });
                                                if (_duplicateWorkPermit.length > 0) {
                                                    details.IsSame = true;
                                                }
                                            }
                                            else details.WorkPermit_No = null;

                                            if (item[13] != null) {
                                                details.WorkPermit_Start = item[13];
                                            }
                                            else details.WorkPermit_Start = null;

                                            if (item[14] != null) {
                                                details.WorkPermit_End = item[14];
                                            }
                                            else details.WorkPermit_End = null;

                                            if (item[15] != null) {
                                                details.CategoryCard = item[15].trim();
                                            }
                                            else {
                                                details.CategoryCard = null;
                                                details.IsnullCategoryCard = false;
                                                details.IsNotice = true;
                                            }

                                            if (item[16] != null) {
                                                details.Card_Type = item[16].toString().replace(/[&\/\\#,+()$~%.'":*?<>{}!\s]/g, '');
                                            }
                                            else {
                                                details.Card_Type = null;
                                                details.IsnullCardType = false;
                                                details.IsNotice = true;
                                            }

                                            if (item[17] != null) {
                                                details.Card_No = item[17].toString().replace(/[&\/\\#,+()$~%.'":*?<>{}!\s]/g, '');
                                                var _duplicateCard_No = $filter("filter")(scope.EmployeeList, { Card_No: details.Card_No });
                                                if (_duplicateCard_No.length > 0) {
                                                    details.IsSame = true;
                                                }
                                            }
                                            else {
                                                details.Card_No = null;
                                                details.IsnullCardNo = false;
                                                details.IsNotice = true;
                                            }

                                            if (item[18] != null) {
                                                details.Card_Start = item[18];
                                            }
                                            else {
                                                details.Card_Start = null;
                                                details.IsnullCardStart = false;
                                                details.IsNotice = true;
                                            }

                                            if (item[19] != null) {
                                                details.Card_End = item[19];
                                            }
                                            else {
                                                details.Card_End = null;
                                                details.IsnullCardEnd = false;
                                                details.IsNotice = true;
                                            }

                                            if (item[20] != null) {
                                                details.Phone = item[20].toString();
                                            }
                                            else {
                                                details.Phone = null;
                                                details.IsnullPhone = false;
                                                details.IsNotice = true;
                                            }

                                        } else {
                                            if (item[10] != null) {
                                                details.Phone = item[10].toString();
                                            }
                                            else {
                                                details.Phone = null;
                                                details.IsnullPhone = false;
                                                details.IsNotice = true;
                                            }
                                        }

                                        scope.EmployeeList.push(details);
                                    }
                                }
                                var id = scope.record.ContractorName || "";
                                scope.validDate(id);
                            };

                            reader.readAsBinaryString(file);
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant('FileExcelValidation_MSG') });
                            scope.filename = "";
                        }
                    });


                },
            }
        }]);

});