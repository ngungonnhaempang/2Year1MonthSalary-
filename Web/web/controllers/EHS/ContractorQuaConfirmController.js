define(['myapp', 'angular'], function (myapp, angular) {
    myapp.controller("ContractorQuaConfirmController", ['$timeout', '$scope', '$window', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Auth', 'uiGridConstants', '$http', 'EngineApi', 'ConQuaService', '$translate',
        function ($timeout, $scope, $window, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Auth, uiGridConstants, $http, EngineApi, ConQuaService, $translate) {
            $scope.note = {};
            $scope.EmployeeList = [];
            $scope.EmployeeMark = [];
            var IdList = [];
            $scope.Training = false;
            $scope.UpdateMark = false;

            $scope.$watch('Training', function (v) {
                $scope.LoadInfo();
            })           

            /* Training date */
            if ($routeParams.ContractorID && $routeParams.state == "training") {
                $scope.Training = true;
            }

            /* Update Mark */
            if ($routeParams.ContractorID && $routeParams.state == "mark") {
                $scope.UpdateMark = true;
            }

            $scope.checkAll = function (_isAll) {
                $scope.EmployeeList.forEach(element => {
                    element.isSelected = _isAll;
                });
            }


            $scope.CheckIdList = function () {
                IdList = [];
                $scope.EmployeeList.forEach(element => {
                    if (element.isSelected == true) {
                        IdList.push(element.EmployeeID);
                    }
                });
            }

            $scope.removeIdSubmitted = function () {
                $scope.CheckIdList();
                for (var i = 0; i < $scope.EmployeeList.length; i++) {
                    IdList.forEach(x => {
                        if ($scope.EmployeeList[i].EmployeeID == x) {
                            $scope.EmployeeList.splice(i, 1);
                        }
                    })
                }
            }

            $scope.Delete = function () {
                $scope.CheckIdList();
                if (IdList.length > 0) {
                    var query = {}
                    query.ContractorID = $routeParams.ContractorID
                    query.empID = IdList
                    query.status = 'X'
                    ConQuaService.NotConfirm().save(query, {}).$promise.then(function (res) {
                        console.log(res);
                        if ($scope.EmployeeList.length == IdList.length) {
                            $location.path('ContractorQua/Index');
                        } else {
                            $scope.removeIdSubmitted();
                        }
                        $timeout(function () {
                            Notifications.addMessage({ 'status': 'info', 'message': $translate.instant('Delete_Succeed_Msg') });
                        }, 800);
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });
                } else {
                    Notifications.addError({ 'status': 'error', 'message': $translate.instant('Select_ONE_MSG') });
                }
            }

            $scope.confirm = function () {
                $scope.CheckIdList();
                console.log(IdList);
                if ($scope.note.AppointmentDate < $scope.note.EndDate) {
                    if (IdList.length > 0) {
                        var query = {}
                        query.VoucherID = $routeParams.ContractorID
                        query.updateDate = $scope.note.AppointmentDate
                        query.idCard = IdList
                        if ($scope.Training) {
                            query.status = "training"
                        } else {
                            query.status = "S"
                        }

                        ConQuaService.ContractorDone().save(query, {}).$promise.then(function (res) {
                            console.log(res);

                            if ($scope.Training) {
                                sendMail("c_training")
                                $scope.LoadInfo()
                            } else {
                                sendMail("c_Appointment")
                                if ($scope.EmployeeList.length == IdList.length) {
                                    $location.path('/ContractorQua/Index');
                                } else {
                                    $scope.removeIdSubmitted();
                                }

                            }
                            $scope.note.AppointmentDate = '';
                            $timeout(function () {
                                Notifications.addMessage({ 'status': 'information', 'message': $translate.instant('Save_Success_MSG') });
                            }, 800);


                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                            callback(errResponse);
                        });
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant('Select_ONE_MSG') });
                    }
                } else {
                    Notifications.addError({ 'status': 'error', 'message': $translate.instant('AppointmentDate_MSG') });
                }
            }

            function sendMail(_mailKind) {
                ConQuaService.SendConfirmdMail().get({
                    ContractorID: $routeParams.ContractorID,
                    VoucherID: '',
                    Mailkind: _mailKind,
                    Datetime: $scope.note.AppointmentDate,
                    ContractorName: $scope.note.ContractorName
                }, {}).$promise.then(function (res) {
                    console.log(res);
                }, function (err) {
                    Notifications.addError({
                        'status': 'error',
                        'message': 'send mail error: ' + err
                    });
                });
            }


            var col = [
                {
                    field: 'Mark',
                    displayName: $translate.instant("Mark"),
                    enableFiltering: false,
                    width: 70,
                    cellTemplate: '<input type="text" ng-model="row.entity.Mark"/>',
                    cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                        var val = grid.getCellValue(row, col);
                        if (parseInt(val) < 5) {
                            return 'red'
                        } else if (parseInt(val) >= 5) {
                            return 'green'
                        }
                    },
                },
                {
                    field: 'Name',
                    displayName: $translate.instant("ConName"),
                    width: 200
                   
                },
                {
                    field: 'IdCard',
                    displayName: $translate.instant("IdCard"),
                    width: 120
                },
                {
                    field: 'Region',
                    displayName: $translate.instant("ConQua_Region"),
                    enableFiltering: false,
                    width: 110
                },
                {
                    field: 'InsuranceDuration',
                    displayName: $translate.instant("Insurance"),
                    enableFiltering: false,
                    width: 140
                },
                {
                    field: 'SafetyCerDuration',
                    displayName: $translate.instant("SafetyCer"),
                    enableFiltering: false,
                    width: 160
                }
                , {
                    field: 'TrainDate',
                    displayName: $translate.instant("TrainDate"),
                    width: 130
                },
                {
                    field: 'StatusRemark',
                    displayName: $translate.instant("Status")
                }
            ];

            $scope.gridOptionsMark = {
                columnDefs: col,
                data: [],
                enableFiltering: true,

                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                }
            };

            $scope.ChangeMark = function () {
                saveMark($scope.EmployeeMark, 0, function (MessageResponse) {
                    if (MessageResponse != '') {
                        Notifications.addError({ 'status': 'error', 'message': MessageResponse });
                    } else {
                        $scope.LoadInfo();
                    }

                });

            };

            function saveMark(employees, i, callback) {
                var query = {};
                query.ContractorID = $routeParams.ContractorID;
                query.mark = employees[i].Mark;
                query.EmpID = employees[i].EmployeeID
                ConQuaService.UpdateMark().save(query, {}).$promise.then(function (res) {
                    console.log(res);
                    i++;
                    if (i < employees.length) {
                        saveMark(employees, i, callback);
                    } else callback('');

                }, function (errResponse) {
                    callback('errResponse')

                });
            }

            $scope.LoadInfo = function () {
                ConQuaService.GetContractorConfirm().get({
                    ContractorID: $routeParams.ContractorID,
                    Language: window.localStorage.lang,
                }).$promise.then(function (res) {
                    console.log(res);
                    $scope.EmployeeMark =[];
                    $scope.EmployeeList =[];
                    $scope.note.ContractorName = res[0].ContractorName;
                    $scope.note.ContracorKind = res[0].ContracorKind;
                    $scope.note.ContracorType = res[0].ContracorType;
                    $scope.note.Rcode = res[0].Rcode;
                    $scope.note.StartDate = res[0].StartDate;
                    $scope.note.EndDate = res[0].EndDate;
                    $scope.ContractorID = $routeParams.ContractorID;
                    $scope.maxDate = $filter('date')(new Date(res[0].EndDate), 'yyyy/MM/dd');
                    $scope.EmployeeMark = [];
                    res.forEach(element => {
                        var x = {};
                        x.Name = element.Name;
                        x.IdCard = element.IdCard;
                        x.Sex = element.Sex;
                        x.Birthday = element.Birthday;
                        x.Job = element.Job;
                        x.Region = element.Region;
                        x.State = element.State;
                        x.InsuranceDuration = element.InsuranceDuration;
                        x.SafetyCerDuration = element.SafetyCerDuration;
                        x.Remark = element.Remark;
                        x.EmployeeID = element.EmployeeID;
                        x.VoucherID = element.VoucherID;
                        x.StatusRemark = element.StatusRemark;
                        x.Mark = element.Mark;
                        x.TrainDate = element.TrainDate;
                        if ($scope.Training) {
                            if ((x.Mark == "" || parseInt(x.Mark) < 5) && element.Status == 'Q') {
                                $scope.EmployeeList.push(x);
                            }
                        } else {
                            if ( element.Status == 'Q') { //parseInt(x.Mark) >= 5 &&
                                $scope.EmployeeList.push(x);
                            }
                        }
                        if (x.TrainDate != "") {
                            $scope.EmployeeMark.push(x);

                        }

                    });
                    $scope.gridOptionsMark.data = $scope.EmployeeMark;
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }
        }
    ])
});
