
define(['app', 'angular'], function (app, angular) {
    app.directive('viewRisk', ['ModificationService', 'socket', '$resource', '$http', '$filter', 'Notifications', '$routeParams', '$translate', '$timeout', 'GateGuest', 'EngineApi', '$location', 'Auth', 'Forms', '$route', '$window',
        function (ModificationService, socket, $resource, $http, $filter, Notifications, $routeParams, $translate, $timeout, GateGuest, EngineApi, $location, Auth, Forms, $route, $window) {
            return {
                restrict: 'E',
                controller: function ($scope) {
                    var lang = window.localStorage.lang;
                    $scope.query = {};
                    $scope.data = {};
                    $scope.Evaluation = false;
                    $scope.IsUpdate = false;
                    $scope.OtherList = []
                    $scope.ChildList = []
                    $scope.ParentList = []
                    $scope.CategoryList = []
                    $scope.AssessmentList = []
                    $scope.hseManagerList = []

                    $scope.Print = false;

                    if ($routeParams.code == "Print") {
                        $scope.Print = true;
                    }

                    $scope.printApp=function () {
                        window.print();
                    }

                    $scope.goBack = function () {
                        window.history.back();
                    }

                    $scope.closePage = function () {
                        window.close()
                    }

                    $scope.checkValidate = function () {
                        if (!$scope.data.MD_ProjectID) {
                            return true;
                        }
                        else {
                            return false
                        }
                    }

                    $scope.SearchProjectID = function () {
                        $scope.VoucherType = "MA"
                        $('#modalProjectID').modal('show');
                    }

                    ModificationService.SearchISO().get({
                        Language: lang,
                        code: '',
                        Status: 'A',
                        ApplicationType: '5VEXXHR042',
                        FromDate: '',
                        ToDate: ''
                    }).$promise.then(function (res) {
                        debugger
                        $scope.data.ISO_AppCode = res[0].ISO_AppCode;
                        $scope.data.ISO_ID = res[0].ISO_ID;

                        if ($routeParams.code != "Create") {
                            $scope.IsUpdate = true
                            if ($routeParams.code == "Evaluate") {
                                $scope.IsEvaluate = true
                            } else {
                                $scope.IsEvaluate = false
                            }
                            loadRiskDetail($routeParams.Risk_AppID)
                        } else {
                            $scope.IsUpdate = false
                            $scope.IsEvaluate = false
                            var status = 'A'
                            var date = ''
                            showCategories(status, date, function (errorCategory) {
                                if (errorCategory != '') {
                                    Notifications.addError({ 'status': 'error', 'message': errorCategory });
                                    return;
                                } else {
                                    GetDeparmentCreatorInfo(Auth.username, function (errResponse) {
                                        if (errResponse != '') {
                                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                                        } else {
                                            $scope.data.CreateDate = $filter('date')(new Date(), 'yyyy-MM-dd');
                                            if ($routeParams.code == "Create" && $routeParams.Risk_AppID != "null") {
                                                getProjectInfo($routeParams.Risk_AppID)
                                            }
                                        }
                                    })
                                }
                            })
                        }
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });

                    function getProjectInfo(_AppID) {
                        debugger;
                        ModificationService.GetProcess().get({
                            lang: lang,
                            ProjectID: '',
                            UserID: Auth.username,
                            FromDate: '',
                            ToDate: '',
                            AppID: _AppID
                        }).$promise.then(function (res) {
                            $scope.data.MD_ProjectID = res[0].MD_ProjectID;
                            $scope.data.MD_Name = res[0].MD_Name;
                            $scope.data.MD_AppID = res[0].MD_AppID;
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }

                    function showCategories(status, date, callback) {
                        $scope.CategoryList = []
                        $scope.ParentList = []
                        $scope.ChildList = []
                        ModificationService.SearchCategory().get({
                            Lan: lang,
                            code: '',
                            Status: status,
                            CategoryType: 'RA',
                            Content: '',
                            FromDate: date,
                            ToDate: date
                        }).$promise.then(function (res) {
                            debugger;
                            console.log(res);
                            $scope.CategoryList = res
                            if ($scope.CategoryList.length > 0) {
                                $scope.CategoryList.forEach(element => {
                                    element.Option = 'No';
                                    element.ImproveSolution = null;
                                    element.NumberOfChild=1;
                                    if (isDifferent(element)) {
                                        element.Other = true;
                                        element.OtherContent = null;
                                    } else {
                                        element.Other = false;
                                        element.OtherContent = null;
                                    }
                                });

                                $scope.ParentList = $filter("filter")($scope.CategoryList, { Parent: null });

                                $scope.ParentList.forEach(element => {
                                    var countChild =  $filter("filter")($scope.CategoryList, { Parent: element.Code  });
                                    if(countChild.length>0){
                                        element.NumberOfChild = countChild.length+1;
                                    }
                                });

                                $scope.ChildList = $filter("filter")($scope.CategoryList, { Parent: '' });
                                callback('')
                            }else{
                                callback('')
                            }
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                            callback(errResponse)
                        });
                    }

                    function isDifferent(obj) {
                        var different = '其他khác'
                        if (obj.Content.replace(/[&\/\\#,+()$~%.'":*?<>{}!\s]/g, '').toLowerCase() == different) {
                            return true
                        } else return false
                    }

                    function GetDeparmentCreatorInfo(creator, callback) {
                        var query = {};
                        query.DepartmentID = "";
                        query.EmployeeID = creator;
                        ModificationService.GetEmployeeInfo().get(query).$promise.then(function (res) {
                            $scope.data.CostCenter = res[0].DepartmentID;
                            $scope.data.Department = res[0].DepartmentID + " - " + res[0].Specification_TW + " / " + res[0].Specification_VN;
                            callback('')
                        }, function (errResponse) {
                            callback(errResponse)
                        });
                    }


                    $scope.selectOption = function (category) {
                        if (category.Option != "Yes") {
                            category.ImproveSolution = null
                            if (isDifferent(element)) {
                                category.OtherContent = null
                            }
                        }
                    }

                    var formVariables = [];
                    var historyVariable = [];
                    $scope.saveRiskAssessmentApp = function (_status) {
                        getAssessment()
                        var params = {};
                        params.Risk_AppID = $scope.data.Risk_AppID || null
                        params.MD_AppID = $scope.data.MD_AppID
                        if (_status == 'ReSubmit') {
                            params.Status = 'F'
                        } else if (_status == 'E') {
                            params.Status = 'E'
                        } else {
                            params.Status = 'N'
                        }

                        params.ISO_ID = $scope.data.ISO_ID
                        params.Creator = Auth.username
                        params.ResultEvaluate = getResultEvaluate(_status)

                        params.RiskDetail = $scope.AssessmentList
                        params.Others = $scope.OtherList

                        ModificationService.CreateRiskAssessment().save(params).$promise.then(function (res) {
                            console.log(res.Risk_AppID)
                            $scope.data.Risk_AppID = res.Risk_AppID;
                            if (_status == 'E') {
                                debugger;
                                socket.emit('evaluate-risk', $scope.EvaluateRisk);
                                $scope.closePage();
                            } else if (_status == 'F') {
                                debugger;
                                ModificationService.GetCheckers().get({
                                    owner: Auth.username,
                                    flowkey: "ModificationAndRisk"
                                }).$promise.then(function (res) {
                                    var LeaderArray = [];
                                    for (var i = 0; i < res.length; i++) {
                                        LeaderArray[i] = res[i].Person;
                                    }

                                    if (LeaderArray.length == 0) {
                                        Notifications.addError({ 'status': 'error', 'message': $translate.instant('Leader_NO_MSG') });
                                        return
                                    } else {
                                        debugger;
                                        formVariables = [];
                                        historyVariable = [];

                                        formVariables.push({ name: "LeaderArray", value: LeaderArray });
                                        formVariables.push({ name: "VoucherID", value: $scope.data.Risk_AppID });
                                        formVariables.push({ name: "start_remark", value: $scope.data.MD_ProjectID + " _ " + $scope.data.ISO_AppCode });
                                        formVariables.push({ name: "VoucherType", value: 'RA' });

                                        historyVariable.push({ name: "MD_ProjectID", value: $scope.data.MD_ProjectID });
                                        historyVariable.push({ name: "ISO", value: $scope.data.ISO_AppCode });

                                        getHSEChecker(function (errResponse) {
                                            if (errResponse) {
                                                Notifications.addError({ 'status': 'error', 'message': errResponse });
                                                return;
                                            } else {

                                                $scope.getFlowDefinitionId("ModificationAndRisk", function (FlowDefinitionId) {
                                                    if (FlowDefinitionId) {
                                                        $scope.startflowid(FlowDefinitionId, "", formVariables, historyVariable, function (url, message) {
                                                            if (message) {
                                                                Notifications.addError({ 'status': 'error', 'message': message });
                                                            } else {
                                                                debugger;
                                                                var query = {}
                                                                query.Risk_AppID = $scope.data.Risk_AppID
                                                                query.status = 'F'
                                                                query.deletePerson = Auth.username
                                                                query.specialDeleteReason = ''
                                                                ModificationService.UpdateRiskStatus().save(query, {}).$promise.then(function (res) {
                                                                    console.log(res);
                                                                    sendMail($scope.data.Risk_AppID,'init',function (err) {
                                                                        $location.path('/taskForm/' + url);
                                                                    })
                                                                }, function (errResponse) {
                                                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                                                })
                                                            }
                                                        }) // end startflowid
                                                    } else {
                                                        Notifications.addError({ 'status': 'error', 'message': $translate.instant('Process_Err_MSG') });
                                                        return;
                                                    }
                                                }) // end getFlowDefinitionId
                                            }
                                        })
                                    }
                                })
                            } else if (_status == 'ReSubmit') {
                                $scope.formVariables.push({ name: "update", value: "OK" });
                                $scope.historyVariable.push({ name: "update", value: "YES" });
                                sendMail($scope.data.Risk_AppID,'init',function (err) {
                                    $scope.submit();
                                })
                            } else {
                                $scope.goBack();
                                $timeout(function () {
                                    Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("Save_Success_MSG") });
                                }, 500);
                            }
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }

                    function loadRiskDetail(_AppID) {
                        ModificationService.GetRiskAssessmentDetail().get({
                            Risk_AppID: _AppID,
                        }).$promise.then(function (res) {
                            debugger;
                            console.log(res);
                            var risk = res.TableRA[0]
                            GetDeparmentCreatorInfo(risk.Creator, function (errResponse) {
                                if (errResponse != '') {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                } else {
                                    var status = 'A'
                                    var date = ''

                                    if ($routeParams.code != "Update") {
                                        $scope.data.CreateDate = risk.CreateDate;
                                        status = ''
                                        date = $scope.data.CreateDate
                                    } else {
                                        $scope.data.CreateDate = $filter('date')(new Date(), 'yyyy-MM-dd');
                                    }

                                    if (risk.Status == 'Q' || risk.Status == 'E' || $scope.checker == 'hseDivisionManager' || $scope.checker == 'hseManager') {
                                        $scope.GetInformation($routeParams.Risk_AppID, risk.Status)
                                    }

                                    $scope.data.Risk_AppID = risk.Risk_AppID
                                    $scope.data.MD_AppID = risk.MD_AppID
                                    $scope.data.MD_ProjectID = risk.MD_ProjectID
                                    $scope.data.MD_Name = risk.MD_Name
                                    $scope.data.Status = risk.Status

                                    $scope.data.ISO_ID = risk.ISO_ID
                                    $scope.data.ISO_AppCode = risk.ISO_AppCode
                                    $scope.data.Creator = risk.Creator

                                    $scope.AssessmentList = res.TableCategory
                                    $scope.OtherList = res.TableOther

                                    if ($scope.checker == 'hseDivisionManager') {
                                        debugger
                                        ModificationService.GetCheckerByKind().get({ kind: "HSE" }).$promise.then(function (res) {
                                            if (res.length > 0) {
                                                res.forEach(element => {
                                                    if (element.Remark == "FactoryManager") {
                                                        $scope.hseManagerList.push(element);
                                                    }
                                                })
                                            }
                                        }, function (errResponse) {
                                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                                        });
                                    }

                                    showCategories(status, date, function (errorCategory) {
                                        if (errorCategory != '') {
                                            Notifications.addError({ 'status': 'error', 'message': errorCategory });
                                            return;
                                        } else {
                                            $scope.CategoryList.forEach(element => {
                                                if ($scope.data.Status == 'E' || $routeParams.code == 'Evaluate') {
                                                    debugger
                                                    element.IsPass = "Yes"
                                                }
                                                var _category = $filter("filter")($scope.AssessmentList, { CategoryID: element.ID }, true);
                                                if (_category.length > 0) {
                                                    element.Option = _category[0].Option
                                                    if (element.Option == "Yes") {
                                                        element.ImproveSolution = _category[0].ImproveSolution
                                                        element.IsPass = _category[0].IsPass
                                                        if (isDifferent(element)) {
                                                            _other = $filter("filter")($scope.OtherList, { ItemID: element.ID }, true)
                                                            element.OtherContent = _other[0].OtherContent
                                                        }
                                                    }
                                                }
                                            });
                                        }
                                    })
                                }
                            })
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }

                    function getAssessment() {
                        $scope.AssessmentList = []
                        $scope.OtherList = []
                        $scope.CategoryList.forEach(element => {
                            if (element.Option == 'Yes' || element.Option == 'NotApply') {
                                var category = {}
                                category.Risk_AppID = $scope.data.Risk_AppID || null
                                category.CategoryID = element.ID
                                category.Option = element.Option
                                category.ImproveSolution = element.ImproveSolution || null
                                category.IsPass = element.IsPass || null
                                $scope.AssessmentList.push(category)

                                if (isDifferent(element) && element.Option == "Yes") {
                                    var data = {}
                                    data.OtherID = null;
                                    data.ItemID = element.ID;
                                    data.OtherContent = element.OtherContent;
                                    data.AppID = $scope.data.Risk_AppID || null
                                    $scope.OtherList.push(data)
                                }
                            }
                        });
                    };

                    function getResultEvaluate(_status) {
                        $scope.EvaluateRisk = null
                        if (_status == "E") {
                            var FailList = $filter("filter")($scope.CategoryList, { IsPass: 'No' }, true)
                            if (FailList.length > 0) {
                                $scope.EvaluateRisk = 'No'
                            } else {
                                $scope.EvaluateRisk = 'Yes'
                                // var OptionYesList = $filter("filter")($scope.CategoryList, { Option: 'Yes' }, true)
                                // if (OptionYesList.length > 0) {
                                //     $scope.EvaluateRisk = 'Yes'
                                // } else {
                                //     $scope.EvaluateRisk = 'NotApply'
                                // }
                            }

                        }
                        return $scope.EvaluateRisk
                    }




                    function getHSEChecker(callback) {
                        var hseSafety = "";
                        var hseEnvironment = "";
                        var hseFire = "";
                        var hseDivisionManager = "";
                        var hseFactoryManager = "";
                        ModificationService.GetCheckerByKind().get({ kind: "HSE" }).$promise.then(function (res) {
                            var HSECheckerList = res;
                            HSECheckerList.forEach(element => {
                                if (element.Remark == "Safety") {
                                    hseSafety = element.Person

                                } else if (element.Remark == "Environment") {
                                    hseEnvironment = element.Person

                                } else if (element.Remark == "Fire") {
                                    hseFire = element.Person

                                } else if (element.Remark == "DivisionManager") {
                                    hseDivisionManager = element.Person

                                } 
                              
                            });

                            formVariables.push({ name: "hseSafety", value: hseSafety });
                            formVariables.push({ name: "hseEnvironment", value: hseEnvironment });
                            formVariables.push({ name: "hseFire", value: hseFire });
                            formVariables.push({ name: "hseDivisionManager", value: hseDivisionManager });

                            callback('')
                        }, function (errResponse) {
                            callback(errResponse)
                        });
                    }




                    $scope.changeStatus = function (_status) {
                        var query = {}
                        query.Risk_AppID = $scope.variable.VoucherID
                        query.status = _status
                        query.deletePerson = Auth.username
                        query.specialDeleteReason = ''
                        ModificationService.UpdateRiskStatus().save(query, {}).$promise.then(function (res) {
                            console.log(res);
                            $scope.submit();
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        })
                    }


                    $scope.saveSubmit = function () {
                        debugger;
                        $scope.formVariables.push({ name: $scope.checker + "Agree", value: $scope.data.IsAgree });
                        $scope.historyVariable.push({ name: $scope.checker + "Agree", value: $scope.data.IsAgree });
                        if ($scope.checker == 'hseDivisionManager') {
                            $scope.formVariables.push({ name: "hseFactoryManager", value: $scope.data.hseManager });
                        }
                        
                        if ($scope.data.IsAgree === "YES") {
                            if ($scope.EnterApprovalReason) {
                                $scope.formVariables.push({ name: $scope.checker + "ApprovalReason", value: $scope.data.ApprovalReason });
                                $scope.historyVariable.push({ name: $scope.checker + "ApprovalReason", value: $scope.data.ApprovalReason });
                            }

                            if ($scope.checker == 'hseManager') {
                                $scope.formVariables.push({ name: "SendVicePresident", value: 'NO' });
                                sendMail($scope.data.Risk_AppID,"complete",function (err) {
                                    $scope.changeStatus('Q')
                                })   
                            } else {
                                
                                sendMail($scope.data.Risk_AppID,$scope.MailKind,function (err) {
                                    $scope.submit();
                                })   
                            }
                        } else {
                            $scope.reject($scope.checker + "DenyReason")
                        }
                    }

                    $scope.reject = function (_reasonName) {
                        $scope.formVariables.push({ name: _reasonName, value: $scope.data.DenyReason });
                        $scope.historyVariable.push({ name: _reasonName, value: $scope.data.DenyReason });
                       
                        if($scope.checker=="hseSafety" || $scope.checker=="hseEnvironment" || $scope.checker=="hseFire" || $scope.checker=="hseDivisionManager" ){
                            sendMail($scope.data.Risk_AppID, $scope.MailKind, function (err) {
                                $scope.submit();
                            })
                        }else{
                            sendMail($scope.data.Risk_AppID, 'reject', function (err) {
                                $scope.submit();
                            })
                        }
                       
                    }

                    $scope.deleteRejectApp = function () {
                        debugger;
                        $scope.formVariables.push({ name: "update", value: "NO" })
                        $scope.historyVariable.push({ name: "update", value: "NO" })
                        $scope.changeStatus('X')

                    }

                    /** Information from MongoDB . Get who receive the voucher and approved it */
                    $scope.GetInformation = function (_AppID, _status) {
                        debugger;
                        ModificationService.GetModificationAndRiskPID().get({ VoucherID: _AppID }).$promise.then(function (res) {
                            console.log(res);
                            if (res) {
                                EngineApi.getProcessLogs.getList({ "id": res.ProcessInstanceId, "cId": "" }, function (data) {
                                    console.log(data[0].Logs);

                                    data[0].Logs = $filter('orderBy')(data[0].Logs, 'FormatStamp');
                                    var taf = TAFFY(data[0].Logs);

                                    var receiver = [];
                                    if(data[1] !=null || data[1] != undefined){
                                        var taf_1 = TAFFY(data[1].Logs);
                                        receiver[0] = taf({ TaskName: "起始表单" }).first() || taf_1({ TaskName: "起始表单" }).first(); //initiator
                                    }else{
                                        receiver[0] = taf({ TaskName: "起始表单" }).first(); //initiator
                                    }

                                    if (_status == 'Q' || _status == 'E') {
                                        receiver[1] = taf({ TaskName: "Manager Check" }).start(1).first();
                                        receiver[2] = taf({ TaskName: "Manager Check" }).start(2).first();
                                        if (receiver[2].UserId == receiver[1].UserId) {
                                            receiver[2] = {}
                                        }

                                        receiver[3] = taf({ TaskName: "HSE Safety & Health Section" }).start(1).first();
                                        receiver[4] = taf({ TaskName: "HSE Environmental Section" }).start(1).first();
                                        receiver[5] = taf({ TaskName: "HSE Fire Fighting Section" }).start(1).first();

                                        receiver[6] = taf({ TaskName: "HSE Division Manager Check" }).start(1).first();

                                        receiver[7] = taf({ TaskName: "HSE Factory Manager Check" }).start(1).first();

                                    } else if ($scope.checker == 'hseDivisionManager' || $scope.checker == 'hseManager') {
                                        receiver[0] = taf({ TaskName: "HSE Safety & Health Section" }).last();
                                        receiver[1] = taf({ TaskName: "HSE Environmental Section" }).last();
                                        receiver[2] = taf({ TaskName: "HSE Fire Fighting Section" }).last();
                                        if ($scope.checker == 'hseManager') {
                                            receiver[3] = taf({ TaskName: "HSE Division Manager Check" }).last();
                                        }
                                        receiver = $filter('orderBy')(receiver, 'FormatStamp');

                                        $scope.EnterApprovalReason = false;

                                        for (let index = 0; index < receiver.length; index++) {
                                            const log = receiver[index];
                                            for (let index = 0; index < log.HistoryField.length; index++) {
                                                const history = log.HistoryField[index];
                                                if (history.Name.contains('DenyReason')) {
                                                    $scope.EnterApprovalReason = true;
                                                    break;
                                                }
                                            }

                                        }
                                    }
                                    $scope.receiver = receiver;



                                })
                            }

                        }, function (err) {
                            Notifications.addError({
                                'status': 'error',
                                'message': err
                            });
                        })
                    }

                    function sendMail(_AppID, _mailkind, callback) {
                        ModificationService.SendMail().post({
                            flowname: "ModificationAndRisk",
                            VoucherID: _AppID,
                            VoucherType: "RA",
                            FromUser: Auth.username,
                            MailKind: _mailkind,
                            hseStaff: ''
                        }, {}).$promise.then(function (res) {
                            callback('')
                        }, function (err) {
                            callback(err)
                        });
                    }
                },
                templateUrl: './forms/EHS/Modification/Manage_Modification/RiskAssessment/CreateRiskAssessment.html'
            }
        }
    ]);
});
