
define(['app', 'angular'], function (app, angular) {
    app.directive('viewEvaluation', ['ModificationService', 'socket', '$upload', '$resource', '$http', '$filter', 'Notifications', '$routeParams', '$translate', '$timeout', 'GateGuest', 'EngineApi', '$location', 'Auth', 'Forms', '$anchorScroll', '$route', '$window',
        function (ModificationService, socket, $upload, $resource, $http, $filter, Notifications, $routeParams, $translate, $timeout, GateGuest, EngineApi, $location, Auth, Forms, $anchorScroll, $route, $window) {
            return {
                restrict: 'E',
                controller: function ($scope) {
                    var lang = window.localStorage.lang;
                    $scope.query = {}
                    $scope.data = {}
                    $scope.fileImprove = ""
                    $scope.file = []
                    $scope.AllFile = []
                    $scope.FileList = []
                    $scope.removeFileList = []
                    $scope.CategoryList = []
                    $scope.ParentList = []
                    $scope.ChildList = []
                    $scope.EvaluationList = []
                    $scope.hseManagerList = []

                    $scope.Print = false;

                    $timeout(function () {
                        if ($routeParams.code == "Print") {
                            $scope.Print = true;
                        }
                    }, 1000)

                    $scope.printApp=function () {
                        window.print();
                    }
                    
                    $scope.viewRiskAssessmentApp = function () {
                        var href = "#/Manage_Modification/RiskAssessment/Print/" + $scope.data.Risk_AppID
                        window.open(href);
                    }

                    $scope.evaluateRiskApp = function () {
                        var href = "#/Manage_Modification/RiskAssessment/Evaluate/" + $scope.data.Risk_AppID
                        window.open(href);
                    }

                    socket.on('result-evaluate', function (data) {
                        if (data) {
                            Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("RiskEvaluate_Success") });
                            $scope.EvaluateRisk = data;
                            $scope.evaluatefirstCategory($scope.EvaluateRisk);

                        }
                    })

                    $scope.evaluatefirstCategory = function (value) {
                        if (value) {
                            $scope.ParentList.forEach(element => {
                                if (element.Index == 1) {
                                    var childAutoEvaluate = $filter("filter")($scope.ChildList, { Parent: element.Code }, true)
                                    if (childAutoEvaluate.length > 0) {
                                        childAutoEvaluate.forEach(element => {
                                            if (element.Index == 1) {
                                                element.Option = value
                                                getCalculateRiskAssessment($scope.data.Risk_CreateDate, $scope.data.Risk_AppID)
                                                return;
                                            }
                                        });
                                    }
                                    return;
                                }
                            });
                        } else {
                            $scope.data.OptionYes = null
                            $scope.data.OptionNo = null
                            $scope.data.OptionNotApply = null
                        }
                    }

                    $scope.goBack = function () {
                        window.history.back()
                    }

                    $scope.checkValidate = function () {

                        if (!$scope.data.MD_ProjectID) {
                            return true
                        }
                        else {
                            return false
                        }
                    }

                    $scope.SearchProjectID = function () {
                        $scope.VoucherType = "RA"
                        $('#modalProjectID').modal('show')
                    }

                    ModificationService.SearchISO().get({
                        Language: lang,
                        code: '',
                        Status: 'A',
                        ApplicationType: '5VEXXHR045',
                        FromDate: '',
                        ToDate: ''
                    }).$promise.then(function (res) {
                        if(res.length > 0){
                            $scope.data.ISO_AppCode = res[0].ISO_AppCode;
                            $scope.data.ISO_ID = res[0].ISO_ID;
                            $timeout(function () {
                                if ($routeParams.code != "Create") {
                                    $scope.IsUpdate = true
                                    loadEvaluateDetail($routeParams.Evaluation_AppID)
                                } else {
                                    $scope.IsUpdate = false;
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
                                                    if ($routeParams.code == "Create" && $routeParams.Evaluation_AppID != "null") {
                                                        getProjectInfo($routeParams.Evaluation_AppID)
                                                    }
                                                }
                                            })
                                        }
                                    })
    
                                }
                            }, 500);

                        }else {
                            if ($routeParams.code != "Create") {
                                $scope.IsUpdate = true
                                loadEvaluateDetail($routeParams.Evaluation_AppID)
                            }
                        }
                    

                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });

                    function getProjectInfo(_AppID) {
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
                            $scope.data.Risk_AppID = res[0].Risk_AppID;
                            $scope.EvaluateRisk = res[0].ResultEvaluate;
                            $scope.data.Risk_CreateDate = res[0].Risk_CreateDate;
                            $scope.evaluatefirstCategory($scope.EvaluateRisk);
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
                            CategoryType: 'BE',
                            Content: '',
                            FromDate: date,
                            ToDate: date
                        }).$promise.then(function (res) {
                            console.log(res);
                            $scope.CategoryList = res
                            if ($scope.CategoryList.length > 0) {
                                $scope.CategoryList.forEach(element => {
                                    element.Option = '';
                                    element.IsPass = null;
                                });

                                $scope.ParentList = $filter("filter")($scope.CategoryList, { Parent: null });
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

                    function loadEvaluateDetail(_AppID) {
                        ModificationService.GetBeforeEvaluateDetail().get({
                            Evaluation_AppID: _AppID,
                        }).$promise.then(function (res) {
                            console.log(res);
                            var boe = res.TableEvaluate[0]
                            GetDeparmentCreatorInfo(boe.Creator, function (errResponse) {
                                if (errResponse != '') {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                } else {
                                    
                                    var status = 'A'
                                    var date = ''
                                    if ($routeParams.code != "Update") {
                                        $scope.data.CreateDate = boe.CreateDate;
                                        status = ''
                                        date = $scope.data.CreateDate
                                        $scope.data.ISO_AppCode = boe.ISO_AppCode;
                                    } else {
                                        $scope.data.CreateDate = $filter('date')(new Date(), 'yyyy-MM-dd');
                                    }

                                    if (boe.Status == 'Q' || boe.Status == 'E' || ($scope.checker && $scope.checker != 'Leader')) {
                                        $scope.GetInformation($routeParams.Evaluation_AppID, boe.Status)
                                    }
                                   

                                    $scope.data.Evaluation_AppID = boe.Evaluation_AppID
                                    $scope.data.MD_ProjectID = boe.MD_ProjectID
                                    $scope.data.MD_Name = boe.MD_Name
                                    $scope.data.ExpectOperateDate = boe.ExpectOperateDate

                                    $scope.data.Request_Safe = boe.Request_Safe
                                    $scope.data.Request_Environment = boe.Request_Environment
                                    $scope.data.Request_FireProtection = boe.Request_FireProtection
                                    $scope.data.Request_DivisionManager = boe.Request_DivisionManager

                                    $scope.data.Result_Safe = boe.Result_Safe;
                                    $scope.data.Result_Environment = boe.Result_Environment;
                                    $scope.data.Result_FireProtection = boe.Result_FireProtection;
                                    $scope.data.Result_DivisionManager = boe.Result_DivisionManager;
                                    $scope.data.Result = boe.Result

                                    $scope.data.Risk_AppID = boe.Risk_AppID
                                    $scope.data.Risk_CreateDate = boe.Risk_CreateDate
                                    $scope.data.Status = boe.Status
                                    $scope.data.ISO_ID = boe.ISO_ID
                                    $scope.data.Creator = boe.Creator
                                    $scope.data.Remark = boe.Remark

                                    $scope.FileList = res.TableFile
                                    $scope.FileList.forEach(element => {
                                        if (element.AttachedForItem == "MD_Improvement") {
                                            $scope.fileImprove = element.FileID;
                                        }
                                    });

                                    if ($scope.checker == 'hseDivisionManager') {
                                        $scope.data.IshseSafety = true;
                                        $scope.data.IshseEnvironment = true;
                                        $scope.data.IshseFire = true;
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

                                        })
    
                                    }

                                    $scope.EvaluationList = res.TableDetail

                                    showCategories(status, date, function (errorCategory) {
                                        if (errorCategory != '') {
                                            Notifications.addError({ 'status': 'error', 'message': errorCategory });
                                            return;
                                        } else {
                                            getCalculateRiskAssessment($scope.data.Risk_CreateDate, $scope.data.Risk_AppID)
                                            $scope.CategoryList.forEach(element => {
                                                element.Option = 'No'
                                                var _category = $filter("filter")($scope.EvaluationList, { CategoryID: element.ID }, true);
                                                if (_category.length > 0) {
                                                    element.Option = _category[0].Option
                                                    element.IsPass_Safe = _category[0].IsPass_Safe
                                                    element.IsPass_Environment = _category[0].IsPass_Environment
                                                    element.IsPass_FireProtection = _category[0].IsPass_FireProtection
                                                    element.IsPass = _category[0].IsPass
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

                    function getCategory4Save() {
                        $scope.EvaluationList = []
                        $scope.CategoryList.forEach(element => {
                            var category = {}
                            category.Evaluation_AppID = $scope.data.Evaluation_AppID || null
                            category.CategoryID = element.ID
                            category.Option = element.Option
                            if ($scope.checker == 'hseDivisionManager' || $scope.checker == 'hseManager') {
                                category.IsPass = element.IsPass
                            } else {
                                category.IsPass = getIsPass(element)
                            }
                            category.IsPass_Safe = element.IsPass_Safe || null
                            category.IsPass_Environment = element.IsPass_Environment || null
                            category.IsPass_FireProtection = element.IsPass_FireProtection || null
                            $scope.EvaluationList.push(category)

                        });
                    };


                    $scope.getResultOperation = function (section) {
                        for (let index = 0; index < $scope.ChildList.length; index++) {
                            const element = $scope.ChildList[index];
                            if (section == 'Safe') {
                                if (element.IsPass_Safe == 'No') {
                                    $scope.data.Result_Safe = 'No'
                                    return
                                } else {
                                    $scope.data.Result_Safe = 'Yes'
                                }
                            } else if (section == 'Environment') {
                                if (element.IsPass_Environment == 'No') {
                                    $scope.data.Result_Environment = 'No'
                                    return
                                } else {
                                    $scope.data.Result_Environment = 'Yes'
                                }
                            } else if (section == 'FireProtection') {
                                if (element.IsPass_FireProtection == 'No') {
                                    $scope.data.Result_FireProtection = 'No'
                                    return
                                } else {
                                    $scope.data.Result_FireProtection = 'Yes'
                                }

                            }

                        }
                    }


                    function getCalculateRiskAssessment(createdate, riskID) {
                        debugger;
                        ModificationService.GetCalculateRiskAssessment().get({
                            FromDate: createdate,
                            ToDate: createdate,
                            Risk_AppID: riskID
                        }).$promise.then(function (res) {
                            console.log(res)
                            $scope.data.OptionYes = res[0].OptionYes
                            $scope.data.OptionNo = res[0].OptionNo
                            $scope.data.OptionNotApply = res[0].OptionNotApply
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }

                    function getResult() {
                        var _result = null
                        if ($scope.checker == 'hseSafety' || $scope.checker == 'hseEnvironment' || $scope.checker == 'hseFire' || $scope.checker == 'hseDivisionManager') {
                            if ($scope.data.Result_Safe == 'No' || $scope.data.Result_Environment == 'No' || $scope.data.Result_FireProtection == 'No' || $scope.data.Result_DivisionManager == 'No') {
                                _result = 'No'
                            } else {
                                _result = 'Yes'
                            }
                        }
                        return _result
                    }

                    function getIsPass(_category) {
                        var _isPass = null
                        if ($scope.checker == 'hseSafety' || $scope.checker == 'hseEnvironment' || $scope.checker == 'hseFire') {
                            if (_category.IsPass_Safe == 'No' || _category.IsPass_Environment == 'No' || _category.IsPass_FireProtection == 'No') {
                                _isPass = 'No'
                            } else {
                                _isPass = 'Yes'
                            }
                        }
                        return _isPass
                    }

                    $scope.saveEvaluateApp = function (_status, callback) {
                        $scope.UpFileList = [];
                        uploadFile($scope.AllFile, 0, function (result) {
                            if (result != '') {
                                $timeout(function () {
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': result
                                    });
                                }, 1000);
                            }
                            else {
                                if ($scope.removeFileList.length > 0) {
                                    $scope.removeFileList.forEach(element => {
                                        $scope.removeFile(element);
                                        $scope.FileList = $filter("filter")($scope.FileList, { FileID: '!' + element }, true);
                                    });
                                }

                                if ($scope.FileList.length > 0) {
                                    $scope.FileList.forEach(element => {
                                        $scope.UpFileList.push(element)
                                    });
                                }

                                debugger;
                                getCategory4Save();

                                var params = {}
                                params.Evaluation_AppID = $scope.data.Evaluation_AppID || null;
                                params.Risk_AppID = $scope.data.Risk_AppID;
                                params.CreateDate = $scope.data.CreateDate;

                                params.Result_Safe = $scope.data.Result_Safe || null;
                                params.Result_Environment = $scope.data.Result_Environment || null;
                                params.Result_FireProtection = $scope.data.Result_FireProtection || null;
                                params.Result_DivisionManager = $scope.data.Result_DivisionManager || null;
                                if ($scope.checker == 'hseManager') {
                                    params.Result = $scope.data.Result
                                }
                                else {
                                    params.Result = getResult()
                                }

                                params.Request_Safe = $scope.data.Request_Safe || null;
                                params.Request_Environment = $scope.data.Request_Environment || null;
                                params.Request_FireProtection = $scope.data.Request_FireProtection || null;
                                params.Request_DivisionManager = $scope.data.Request_DivisionManager || null;
                                params.ExpectOperateDate = $scope.data.ExpectOperateDate;
                                if (_status == 'ReSubmit' || _status == 'HSECheck') {
                                    params.Status = 'F'
                                } else {
                                    params.Status = 'N'
                                }

                                params.ISO_ID = $scope.data.ISO_ID;

                                if (_status == 'HSECheck') {
                                    params.Creator = $scope.data.Creator;
                                } else {
                                    params.Creator = Auth.username;
                                }

                                params.Remark = $scope.data.Remark || null;

                                params.BOEDetail = $scope.EvaluationList;
                                params.files = $scope.UpFileList;

                                ModificationService.CreateBOEvaluation().save({}, params).$promise.then(function (res) {
                                    $scope.data.Evaluation_AppID = res.Evaluation_AppID;
                                    if (_status == 'F') {
                                        debugger;
                                        ModificationService.GetCheckers().get({
                                            owner: Auth.username,
                                            flowkey: "EvaluationAndClosing"
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
                                                formVariables.push({ name: "VoucherID", value: $scope.data.Evaluation_AppID });
                                                formVariables.push({ name: "start_remark", value: $scope.data.MD_ProjectID + " _ " + $scope.data.ISO_AppCode });
                                                formVariables.push({ name: "VoucherType", value: 'BE' });

                                                historyVariable.push({ name: "MD_ProjectID", value: $scope.data.MD_ProjectID });
                                                historyVariable.push({ name: "ISO", value: $scope.data.ISO_AppCode });

                                                getHSEChecker(function (errResponse) {
                                                    if (errResponse) {
                                                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                                                        return;
                                                    } else {

                                                        $scope.getFlowDefinitionId("EvaluationAndClosing", function (FlowDefinitionId) {
                                                            if (FlowDefinitionId) {
                                                                $scope.startflowid(FlowDefinitionId, "", formVariables, historyVariable, function (url, message) {
                                                                    if (message) {
                                                                        Notifications.addError({ 'status': 'error', 'message': message });
                                                                    } else {
                                                                        debugger;
                                                                        var query = {}
                                                                        query.Evaluation_AppID = $scope.data.Evaluation_AppID
                                                                        query.status = 'F'
                                                                        query.deletePerson = Auth.username
                                                                        query.specialDeleteReason = ''
                                                                        ModificationService.UpdateBEStatus().save(query, {}).$promise.then(function (res) {
                                                                            console.log(res);
                                                                            sendMail($scope.data.Evaluation_AppID, 'init', '', function (err) {
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
                                        sendMail($scope.data.Evaluation_AppID, 'init', '', function (err) {
                                            $scope.submit();
                                        })
                                    } else if (_status == 'HSECheck') {
                                        console.log(_status);
                                    } else {
                                        $scope.goBack();
                                        $timeout(function () {
                                            Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("Save_Success_MSG") });
                                        }, 500);
                                    }
                                    callback('')

                                }, function (errResponse) {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                    callback(errResponse)
                                });
                            }
                        })
                    }





                    /** UPLOAD FILE **/
                    function uploadFile(fileAttached, i, callback) {
                        if (fileAttached.length > 0) {
                            $scope.upload = $upload.upload({
                                url: '/ehs/modification/files/UploadFile',
                                method: "POST",
                                file: fileAttached[i].file
                            }).progress(function (evt) { }).success(function (data) {
                                console.log("file is uploaded successfully");
                                console.log(data);
                                var f = {}
                                f.FileID = data[0]
                                f.AppID = $scope.data.Evaluation_AppID || null
                                f.AttachedForItem = fileAttached[i].AttachedForItem
                                $scope.UpFileList.push(f);
                                callback('');
                            }).error(function (data, status) {
                                callback(status + data);
                            });
                        } else {
                            callback('');
                        }
                    }

                    $scope.onFileSelect = function ($files, AttachedForItem) {
                        console.log($files);
                        if (true) {
                            if ($scope.fileImprove != "") {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': $translate.instant('File_Number_MSG')
                                });
                                return false;
                            }

                            $scope.file = $files[0];
                            if (!$scope.file.name.toLowerCase().includes(".zip") && !$scope.file.name.toLowerCase().includes(".rar")) {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': $translate.instant('FileCompressedValidation_MSG')
                                });
                                return false;
                            }

                            var f = {}
                            f.AttachedForItem = AttachedForItem
                            f.file = $scope.file;
                            if (AttachedForItem == "MD_Improvement") {
                                $scope.fileImprove = $scope.file.name;
                                $scope.AllFile.push(f)
                            }
                        }
                    }

                    $scope.getRemoveFileName = function (filename, AttachedForItem) {
                        $scope.removeFileList.push(filename);
                        $scope.fileImprove = "";
                        $scope.AllFile = $filter("filter")($scope.AllFile, { AttachedForItem: '!' + AttachedForItem }, true);
                    }

                    $scope.removeFile = function (_name) {
                        if (_name) {
                            ModificationService.DeleteFile().save({
                                filename: _name
                            }).$promise.then(function (res) {
                                console.log(res)
                            }, function (error) {
                                console.log(error);
                            })
                        }
                    }

                    /* SUBMIT */
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
                            formVariables.push({ name: "IshseSafety", value: true });
                            formVariables.push({ name: "IshseEnvironment", value: true });
                            formVariables.push({ name: "IshseFire", value: true });

                            callback('')
                        }, function (errResponse) {
                            callback(errResponse)
                        });
                    }



                    $scope.changeStatus = function (_status) {
                        var query = {}
                        query.Evaluation_AppID = $scope.variable.VoucherID
                        query.status = _status
                        query.deletePerson = Auth.username
                        query.specialDeleteReason = ''
                        ModificationService.UpdateBEStatus().save(query, {}).$promise.then(function (res) {
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
                        if ($scope.data.IsAgree === "YES") {
                            if ($scope.EnterApprovalReason) {
                                $scope.formVariables.push({ name: $scope.checker + "ApprovalReason", value: $scope.data.ApprovalReason });
                                $scope.historyVariable.push({ name: $scope.checker + "ApprovalReason", value: $scope.data.ApprovalReason });
                            }

                            if ($scope.checker != 'Leader') {
                                if ($scope.checker == 'hseDivisionManager') {
                                    $scope.formVariables.push({ name: "hseDivisionManagerReject", value: false });
                                    $scope.formVariables.push({ name: "hseFactoryManager", value: $scope.data.hseManager });
                                }
                                $scope.saveEvaluateApp('HSECheck', function (errResponse) {
                                    if (errResponse == '') {
                                        if ($scope.checker == 'hseManager') {
                                            sendMail($scope.data.Evaluation_AppID, "complete", '', function (err) {
                                                $scope.changeStatus('Q')
                                            })
                                        } else {
                                            sendMail($scope.data.Evaluation_AppID, $scope.MailKind, '', function (err) {
                                                $scope.submit();
                                            })
                                        }
                                    }
                                })

                            } else {
                                $scope.formVariables.push({ name: "IshseSafety", value: true });
                                $scope.formVariables.push({ name: "IshseEnvironment", value: true });
                                $scope.formVariables.push({ name: "IshseFire", value: true });
                                sendMail($scope.data.Evaluation_AppID, $scope.MailKind, '', function (err) {
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

                        if ($scope.checker != 'Leader') {
                            var _hseReject = ''
                            if ($scope.checker == 'hseDivisionManager') {
                                $scope.formVariables.push({ name: 'IshseSafety', value: $scope.data.IshseSafety });
                                $scope.formVariables.push({ name: "IshseEnvironment", value: $scope.data.IshseEnvironment });
                                $scope.formVariables.push({ name: "IshseFire", value: $scope.data.IshseFire });
                                $scope.formVariables.push({ name: "hseDivisionManagerReject", value: true });

                                if ($scope.data.IshseSafety) {
                                    if (_hseReject != '') {
                                        _hseReject = _hseReject + '|' + $scope.variable.hseSafety
                                    } else {
                                        _hseReject = $scope.variable.hseSafety
                                    }

                                }

                                if ($scope.data.IshseEnvironment) {
                                    if (_hseReject != '') {
                                        _hseReject = _hseReject + '|' + $scope.variable.hseEnvironment
                                    } else {
                                        _hseReject = $scope.variable.hseEnvironment
                                    }

                                }

                                if ($scope.data.IshseFire) {

                                    if (_hseReject != '') {
                                        _hseReject = _hseReject + '|' + $scope.variable.hseFire
                                    } else {
                                        _hseReject = $scope.variable.hseFire
                                    }

                                }



                            }

                            $scope.saveEvaluateApp('HSECheck', function (errResponse) {
                                if (errResponse == '') {
                                    var _mailkind = ''
                                    if ($scope.checker == 'hseDivisionManager') {
                                        _mailkind = 'hse_staff'
                                    } else if ($scope.checker == 'hseManager') {
                                        _mailkind = 'reject'
                                    } else {
                                        _mailkind = 'hse_divisionManager'
                                    }
                                    sendMail($scope.data.Evaluation_AppID, _mailkind, _hseReject, function (err) {
                                        $scope.submit();
                                    })
                                }
                            })

                        } else {
                            sendMail($scope.data.Evaluation_AppID, 'reject', '', function (err) {
                                $scope.submit();
                            })
                        }
                    }


                    $scope.deleteRejectApp = function () {
                        $scope.formVariables.push({ name: "update", value: "NO" })
                        $scope.historyVariable.push({ name: "update", value: "NO" })
                        $scope.changeStatus('X')
                    }



                    /** Information from MongoDB . Get who receive the voucher and approved it */
                    $scope.GetInformation = function (_AppID, _status) {
                        debugger;
                        ModificationService.GetEvaluationAndClosingPID().get({ VoucherID: _AppID }).$promise.then(function (res) {
                            console.log(res);
                            if (res) {
                                EngineApi.getProcessLogs.getList({ "id": res.ProcessInstanceId, "cId": "" }, function (data) {
                                    console.log(data[0].Logs);

                                    data[0].Logs = $filter('orderBy')(data[0].Logs, 'FormatStamp');
                                    var taf = TAFFY(data[0].Logs);

                                    var receiver = [];
                                    if (data[1] != null || data[1] != undefined) {
                                        var taf_1 = TAFFY(data[1].Logs);
                                        receiver[0] = taf({ TaskName: "起始表单" }).first() || taf_1({ TaskName: "起始表单" }).first(); //initiator
                                    } else {
                                        receiver[0] = taf({ TaskName: "起始表单" }).first(); //initiator
                                    }

                                    if (_status == 'Q') {
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

                                    } else if ($scope.checker != 'Leader') {
                                        receiver[0] = taf({ TaskName: "HSE Safety & Health Section" }).last();
                                        receiver[1] = taf({ TaskName: "HSE Environmental Section" }).last();
                                        receiver[2] = taf({ TaskName: "HSE Fire Fighting Section" }).last();
                                        if ($scope.checker != 'hseDivisionManager') {
                                            receiver[3] = taf({ TaskName: "HSE Division Manager Check" }).last();
                                        }

                                        if ($scope.checker == 'hseDivisionManager' || $scope.checker == 'hseManager') {
                                            receiver = $filter('orderBy')(receiver, 'FormatStamp');
                                        }


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

                    $scope.checkResult = function () {
                        var _result = 'Yes'
                        if ($scope.checker == 'hseSafety') {
                            if ($scope.data.Result_Safe == 'No') {
                                _result = 'No'
                            }
                        } else if ($scope.checker == 'hseEnvironment') {
                            if ($scope.data.Result_Environment == 'No') {
                                _result = 'No'
                            }
                        }
                        else if ($scope.checker == 'hseFire') {
                            if ($scope.data.Result_FireProtection == 'No') {
                                _result = 'No'
                            }
                        }
                        else if ($scope.checker == 'hseDivisionManager') {
                            if ($scope.data.Result_DivisionManager == 'No') {
                                _result = 'No'
                            }
                        }

                        else if ($scope.checker == 'hseManager') {
                            if ($scope.data.Result == 'No') {
                                _result = 'No'
                            }
                        }

                        return _result
                    }

                    $scope.IstickSectionReturn = function () {
                        if ($scope.checker == 'hseDivisionManager') {
                            if (!$scope.data.IshseSafety && !$scope.data.IshseEnvironment && !$scope.data.IshseFire) {
                                return true
                            }
                            else return false
                        } else return false
                    }

                    function sendMail(_AppID, _mailkind, _hseStaff, callback) {
                        ModificationService.SendMail().post({
                            flowname: "EvaluationAndClosing",
                            VoucherID: _AppID,
                            VoucherType: "BE",
                            FromUser: Auth.username,
                            MailKind: _mailkind,
                            hseStaff: _hseStaff
                        }, {}).$promise.then(function (res) {
                            callback('')
                        }, function (err) {
                            callback(err)
                        });
                    }



                },
                templateUrl: './forms/EHS/Modification/Manage_Modification/BeforeOperateEvaluation/CreateBeforeOperateEvaluation.html'
            }
        }
    ]);
});
