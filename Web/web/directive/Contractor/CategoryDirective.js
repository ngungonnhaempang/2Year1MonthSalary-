
define(['app', 'angular'], function (app, angular) {
    app.directive('categorySubmitView', ['ConDisciplineService', '$upload', '$resource', '$http', '$filter', 'Notifications', '$routeParams', '$translate', '$timeout', 'GateGuest', 'EngineApi', '$location', 'Auth', 'Forms', '$route', '$window',
        function (ConDisciplineService, $upload, $resource, $http, $filter, Notifications, $routeParams, $translate, $timeout, GateGuest, EngineApi, $location, Auth, Forms, $route, $window) {
            return {
                restrict: 'E',
                controller: function ($scope) {
                    var lang = window.localStorage.lang;
                    $scope.view = {};
                    $scope.CategoryList = []
                    $scope.ParentList = []
                    $scope.ChildList = []
                    $scope.ChildNoParent = []
                    $scope.UList = []
                    $scope.today = $filter('date')(new Date(), 'yyyy-MM-dd');
                    $scope.IsExpiry = false;

                    $scope.resetSubmitModal = function () {
                        $scope.view = {}
                    }

                    $scope.Convert = function (number) {
                        if (number > 0) {
                            return (number).toLocaleString('vi-VN')
                        }
                    }

                    ConDisciplineService.GetCategoryType().get({ language: lang }).$promise.then(function (res) {
                        $scope.CategoryTypeList = res;
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });

                    $scope.checkValidate = function () {
                        if ($scope.CategoryList.length > 0) {
                            return false;
                        } else {
                            return true;
                        }
                    }

                    $scope.viewSubmit = function (_status, _type) {
                        $scope.CategoryList = []
                        $scope.ParentList = []
                        $scope.ChildList = []
                        $scope.ChildNoParent = []
                        ConDisciplineService.SearchCategory().get({
                            Lan: lang,
                            code: '',
                            Status: _status,
                            Content: '',
                            FromDate: '',
                            ToDate: '',
                            type:_type
                        }).$promise.then(function (res) {
                            debugger;
                            console.log(res);
                            if (res.length > 0) {
                                if (_status == "F") {
                                    $scope.CategoryList = $filter('filter')(res, { SubmitDate: $scope.variable.SubmitDate }, true);
                                } else {
                                    $scope.CategoryList = $filter('filter')(res, { Status: 'N' }, true);
                                }

                                if ($scope.CategoryList.length > 0) {
                                    $scope.ParentList = $filter("filter")($scope.CategoryList, { Parent: null });
                                    $scope.ChildList = $filter("filter")($scope.CategoryList, { Parent: '' });
                                    if ($scope.ChildList.length > 0) {
                                        $scope.ChildList.forEach(element => {
                                            var ParentSubmitted = $filter("filter")($scope.ParentList, { Code: element.Parent }, true);
                                            if (ParentSubmitted.length == 0) {
                                                $scope.ChildNoParent.push(element)
                                            }
                                        })
                                    }
                                }

                                if ($scope.variable.Flag == "Update" || $scope.variable.Flag == "Upgrade") {
                                    var params = {}
                                    params = angular.copy($scope.CategoryList[0]);

                                    params.ContentVN = $scope.variable.S_ContentVN;
                                    params.ContentTW = $scope.variable.S_ContentTW;
                                    params.Numbering = $scope.variable.S_Numbering;
                                    params.StartDate = $scope.variable.S_StartDate;
                                    params.EndDate = $scope.variable.S_EndDate;
                                    params.Remark = $scope.variable.S_Remark;
                                    params.Fine = $scope.variable.S_Fine;
                                    params.Version = $scope.variable.S_Version
                                    params.CategoryType = $scope.variable.S_CategoryType


                                    $scope.UList.push(params);
                                    console.log($scope.UList);
                                }
                            }
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }

                    if ($scope.checker) {
                        $scope.viewSubmit('F','');
                        if ($scope.variable.EndDate < $scope.today || $scope.variable.S_EndDate < $scope.today) {
                            $scope.IsExpiry = true;
                        } else {
                            $scope.IsExpiry = false;
                        }
                    }

                    $scope.leaderSubmit = function () {
                        debugger;
                        console.log($scope.checker)
                        if ($scope.CategoryList.length > 0) {
                            if (!$scope.IsExpiry) {
                                saveInfo($scope.checker, function (errResponse) {
                                    if (errResponse != '') {
                                        Notifications.addError({ 'status': 'error', 'message': errResponse })
                                    } else {
                                        $scope.formVariables.push({ name: $scope.checker + "Agree", value: $scope.view.IsAgree });
                                        $scope.historyVariable.push({ name: $scope.checker + "Agree", value: $scope.view.IsAgree });

                                        if ($scope.view.IsAgree == "NO") {
                                            $scope.historyVariable.push({ name: $scope.checker + "DenyReason", value: $scope.view.DenyReason });
                                            sendMail($scope.variable.SubmitDate, '', $scope.variable.initiator, 'reject', function (err) {
                                                $scope.submit();
                                            })
                                        } else {
                                            var _mailkind = $scope.checker == 'Leader' ? 'LeaderSubmit' : 'complete'
                                            sendMail($scope.variable.SubmitDate, '', $scope.variable.initiator, _mailkind, function (err) {
                                                $scope.submit();
                                            })
                                        }
                                    }
                                })
                            } else {
                                $scope.view.IsAgree = "NO";
                                $scope.view.DenyReason = "Hết hạn - expiry";
                                saveInfo($scope.checker, function (errResponse) {
                                    if (errResponse != '') {
                                        Notifications.addError({ 'status': 'error', 'message': errResponse })
                                    } else {
                                        $scope.formVariables.push({ name: $scope.checker + "Agree", value: 'NO' });
                                        $scope.historyVariable.push({ name: $scope.checker + "Agree", value: 'NO' });
                                        sendMail($scope.variable.SubmitDate,'', $scope.variable.initiator, 'reject', function (err) {
                                            $scope.submit();
                                        })
                                    }
                                })
                            }

                        } else {
                            $scope.formVariables.push({ name: $scope.checker + "Agree", value: 'NO' });
                            $scope.historyVariable.push({ name: $scope.checker + "Agree", value: 'NO' });
                            $scope.submit();
                        }

                    }

                    function saveInfo(status, callback) {
                        var reason = $scope.view.IsAgree == "NO" ? $scope.view.DenyReason : ""
                        if ($scope.variable.Flag == "SubmitNew") {
                            var _categoryList = $scope.CategoryList;
                            ConDisciplineService.SubmitCategory().post({ Status: status, UserID: Auth.username, AllRejectReason: reason, StartDate: $scope.variable.StartDate, EndDate: $scope.variable.EndDate }, {
                                _categoryList
                            }).$promise.then(function (res) {
                                console.log("success");
                                console.log(res);
                                callback('')
                            }, function (errResponse) {
                                callback(errResponse)
                            });
                        } else {
                            var isUpdate = $scope.variable.Flag == "Update" ? true : false;
                            var _categoryList = $scope.view.IsAgree == "NO" ? $scope.CategoryList : $scope.UList;
                            ConDisciplineService.UpdateOrUpgradeSubmit().post({ Status: status, UserID: Auth.username, AllRejectReason: reason, SubmitDate: '', SubmitReason: '', IsUpdate: isUpdate }, {
                                _categoryList
                            }).$promise.then(function (res) {
                                console.log("success");
                                console.log(res);
                                callback('')
                            }, function (errResponse) {
                                callback(errResponse)
                            });
                        }
                    }


                    var formVariables = [];
                    var historyVariable = [];

                    $scope.submitCategory = function () {
                        debugger;
                        var _categoryList = $scope.CategoryList;

                        var reason = $scope.view.IsAgree == "NO" ? $scope.view.DenyReason : ""

                        ConDisciplineService.SubmitCategory().post({ Status: "F", UserID: Auth.username, AllRejectReason: reason, StartDate: '', EndDate: '' }, {
                            _categoryList
                        }).$promise.then(function (res) {
                            console.log("success");
                            console.log(res);
                            formVariables = [];
                            historyVariable = [];
                            var leaderList = [];
                            var managerList = [];
  
                                    leaderList.push($scope.view.LeaderID)
                                    managerList.push($scope.Manager[0].Person )
                                    formVariables.push({ name: "LeaderArray", value: leaderList });
                                    formVariables.push({ name: "ManagerArray", value: managerList});
                                    formVariables.push({ name: "SubmitDate", value: res.SubmitDate });
                                    formVariables.push({ name: "start_remark", value: $scope.view.Remark || null });
                                    formVariables.push({ name: "Flag", value: "SubmitNew" });
                                    formVariables.push({ name: "StartDate", value: $scope.view.StartDate });
                                    formVariables.push({ name: "EndDate", value: $scope.view.EndDate });


                                    historyVariable.push({ name: "SubmitDate", value: res.SubmitDate });

                                    $scope.getFlowDefinitionId("DisciplineCategories", function (FlowDefinitionId) {
                                        if (FlowDefinitionId) {
                                            debugger;
                                            $scope.startflowid(FlowDefinitionId, "", formVariables, historyVariable, function (url, message) {
                                                if (message) {
                                                    Notifications.addError({ 'status': 'error', 'message': message });
                                                } else {
                                                    sendMail(res.SubmitDate, $scope.view.LeaderID, Auth.username, 'init', function (err) {
                                                        $('#SubmitViewModal').modal('hide');
                                                        $("#SubmitViewModal").on('hidden.bs.modal', function () {
                                                            $location.path('/taskForm/' + url);
                                                            $scope.$apply();
                                                        });
                                                    })
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
                  
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse })
                        });
                    }

                    $scope.$watch('view.Duration', function (value) {
                        if (value && $scope.view.StartDate) {
                            $scope.view.EndDate = $filter('date')(new Date($scope.view.StartDate).setMonth(new Date($scope.view.StartDate).getMonth() + parseInt(value)), 'yyyy-MM-dd');
                        }
                    })

                    $scope.$watch('view.StartDate', function (value) {
                        if (value && $scope.view.Duration) {
                            $scope.view.EndDate = $filter('date')(new Date(value).setMonth(new Date(value).getMonth() + parseInt($scope.view.Duration)), 'yyyy-MM-dd');
                        }
                    })

                    function sendMail(_submitDate, _toUser, _creator, _mailkind, callback) {
                        ConDisciplineService.SendCategoryMail().post({
                            flowname: "DisciplineCategories",
                            SubmitDate: _submitDate,
                            FromUser: Auth.username,
                            ToUser : _toUser,
                            Creator: _creator,
                            MailKind: _mailkind,
                        }, {}).$promise.then(function (res) {
                            callback('')
                        }, function (err) {
                            callback(err)
                        });
                    }

                    $scope.closeCheckerModal = function () {
                        debugger;
                        $('#modalChecker').modal('hide');
                    }

                    $scope.getCategoryByType = function () {
                        if($scope.view.CategoryType){
                            $scope.viewSubmit('N',$scope.view.CategoryType)
                        }
                    }

                },
                templateUrl: './forms/ContractorDiscipline/Manage_Info/Category/SubmitView.html'
            }
        }
    ]);
});
