
define(['app', 'angular'], function (app, angular) {
    app.directive('viewDenounce', ['ConDisciplineService', 'ConQuaService', '$upload', '$resource', '$http', '$filter', 'Notifications', '$routeParams', '$translate', '$timeout', 'GateGuest', 'EngineApi', '$location', 'Auth', 'Forms', '$route', '$window',
        function (ConDisciplineService, ConQuaService, $upload, $resource, $http, $filter, Notifications, $routeParams, $translate, $timeout, GateGuest, EngineApi, $location, Auth, Forms, $route, $window) {
            return {
                restrict: 'E',
                controller: function ($scope) {
                    var lang = window.localStorage.lang;
                    $scope.query = {};
                    $scope.data = {};
                    $scope.listfile = [];
                    $scope.btnFile = false;
                    $scope.hidethis = true;
                    $scope.AllContractorList = [];
                    $scope.showProcess = false;
                    $scope.Safety = "";
                    $scope.Environment = "";
                    $scope.FireProtection = "";

                    ConDisciplineService.GetDenounceChecker().get({}).$promise.then(function (checkerList) {
                        if (checkerList.length > 0) {
                            $scope.Safety = "";
                            $scope.Environment = "";
                            $scope.FireProtection = "";
                            checkerList.forEach(element => {
                                if (element.Kind == 'Safety')
                                    $scope.Safety = element.Person

                                if (element.Kind == 'Environment')
                                    $scope.Environment = element.Person

                                if (element.Kind == 'FireProtection')
                                    $scope.FireProtection = element.Person
                            });
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': "Can not Get EHS Checker" });
                        }
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    })

                    $scope.checkViolationField = function (value) {
                        if (value == "Unknow") {
                            $scope.data.Safety = false;
                            $scope.data.Environment = false;
                            $scope.data.FireProtection = false;
                        } else {
                            $scope.data.Unknow = false;
                        }
                    }

                    $scope.resetModal = function () {
                        $scope.data = {};
                        $scope.listfile = [];
                        $scope.btnFile = false;
                        $scope.Create = false;
                    }

                    $scope.btnfile = function (id, filename) {
                        $('#' + id).click();
                    }
                    //*** UPload file */
                    $scope.UploadFileHSE = function ($files, _colName) {
                        var isValidFile = checkFileLimited($files, _colName, 6, 3);
                        if (!isValidFile.success) {
                            alert(isValidFile.message)
                        } else {
                            $upload.upload({
                                url: '/ehs/contractor/files/UploadFile',
                                method: "POST",
                                file: $files
                            }).progress(function (evt) {
                                $scope.showProcess = true;
                                var percentInt = parseInt(100.0 * evt.loaded / evt.total);
                                console.log('percent: ' + percentInt);
                                $("#MyFileUploadProcessBar").css("width", percentInt.toString() + "%");
                                $("#MyFileUploadProcessBarText").html("uploaded" + percentInt + "%");
                                if (percentInt = 0) {
                                    $("#MyFileUploadProcessBarDiv").show();
                                    $scope.showProcess = false;
                                } else if (percentInt = 100) {
                                    setTimeout(function () {
                                        $("#MyFileUploadProcessBar").css("width", "0%");
                                        $scope.showProcess = false;
                                    }, 1000);
                                }
                            }).then(function (res) {
                                res.data.forEach(x => {
                                    var file = x.toLowerCase();
                                    var dt = {
                                        name: x,
                                        col: _colName
                                    };

                                    if (file.includes(".jpg") || file.includes(".jpeg") || file.includes(".png") || file.includes(".mp4")) {
                                        $scope.listfile.push(dt);
                                        $scope.data.listfile_Denounce = true;

                                    } else {
                                        $timeout(function () {
                                            Notifications.addMessage({
                                                'status': 'info',
                                                'message': $translate.instant('FormatFile_MSG')
                                            });
                                        }, 300);
                                        deleteFileInFolder(x)
                                        return;
                                    }

                                })

                            })
                        }
                    }

                    /**
                     * Create by Isaac 2019-07-23
                     * @param {file In Upload} $files get all files when Upload
                     * @param {*} maximumSize 
                     * @param {number of TotalFiles} totalFile 
                     */

                    function checkFileLimited($files, colname, maximumSize, totalFile) {
                        const fileCount = $files.length;
                        const maximumFileSize = maximumSize * 1024 * 1024 // 3MB
                        var objectResult = {
                            success: true,
                            message: "Upload Completed!"
                        };
                        //check file exist in list
                        var listOfFiles = (($scope.listfile.length + fileCount) > maximumSize ||
                            ($scope.listfile.filter(x => x.col === colname).length + fileCount) > maximumSize) ? true : false;
                        if (fileCount > totalFile || listOfFiles) {
                            objectResult.success = false;
                            objectResult.message = `Your number of files over ${totalFile}`;
                        } else {
                            $files.forEach(item => {
                                if (item.size > maximumFileSize) {
                                    objectResult.success = false;
                                    objectResult.message = `Your file upload over ${maximumSize}MB\n Please uploade another file!`;
                                }
                            })
                        }
                        return objectResult;
                    }

                    $scope.removeFile = function (index) {
                        if ($scope.Create)
                            deleteFileInFolder($scope.listfile[index].name)

                        $scope.listfile.splice(index, 1);

                        if ($scope.listfile.length == 0) {
                            $scope.data.listfile_Denounce = false;
                        }
                    }

                    function deleteFileInFolder(_filename) {
                        ConDisciplineService.DeleteFile().save({
                            filename: _filename
                        }, function (res) {
                            if (res.Success) {
                                console.log('delete file in folder', res.Success);
                            }
                        }, function (error) {
                            console.log(error);
                        })
                    }

                    /** LOAD VOUCHER **/
                    $scope.loadDenounce = function (_AppID) {
                        ConDisciplineService.GetDenounce().get({
                            AppID: _AppID
                        }).$promise.then(function (res) {
                            $scope.data = res.TableDenounce[0]
                            $scope.data.TimeViolation = $filter('date')($scope.data.TimeViolation, 'yyyy-MM-dd')
                            $scope.FileAttached = res.TableFile
                            $scope.FileAttached.forEach(element => {
                                var x = {};
                                x.AppID = element.AppID;
                                x.name = element.FileID;
                                x.col = element.AttachedForItem;
                                $scope.listfile.push(x);
                                $scope.data.listfile_Denounce = true;
                            })
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }

                    if ($routeParams.code == "Checker") {
                        $scope.loadDenounce($routeParams.VoucherID);
                    }

                    /** SAVE VOUCHER **/
                    var formVariables = [];
                    var historyVariable = [];
                    $scope.saveVoucher = function (status) {
                        var lsFile = [];
                        if ($scope.listfile.length > 0) {
                            $scope.listfile.forEach(element => {
                                var f = {};
                                f.FileID = element.name
                                f.AppID = $scope.data.DenounceID || null
                                f.AttachedForItem = 'CD_Denounce';
                                f.MegabyteSize = null;
                                lsFile.push(f);
                            })
                        }
                        var params = {}
                        params.DenounceID = $scope.data.DenounceID || null;
                        params.DenounceCode = $scope.data.DenounceCode || null;
                        params.LocationViolation = $scope.data.LocationViolation;
                        params.TimeViolation = $scope.data.TimeViolation;
                        params.ContractorName = $scope.data.ContractorName;
                        if (status == 'ReSubmit' || status == 'Submit') {
                            params.Status = 'F';
                        } else {
                            params.Status = 'N';
                        }
                        params.DescriptionViolation = $scope.data.DescriptionViolation;
                        params.Remark = $scope.data.Remark || null;
                        params.Creator = Auth.username;
                        params.Unknow = $scope.data.Unknow || null;
                        params.Safety = $scope.data.Safety || null;
                        params.Environment = $scope.data.Environment || null;
                        params.FireProtection = $scope.data.FireProtection || null;
                        params.ContactUser = $scope.data.ContactUser || null;
                        params.files = lsFile;

                        ConDisciplineService.Save_Denounce().save({}, params).$promise.then(function (res) {
                            $scope.data.DenounceID = res.DenounceID
                            $scope.data.DenounceCode = res.DenounceCode
                            if (status == 'Submit') {
                                ConDisciplineService.GetCheckers().get({
                                    owner: Auth.username,
                                    flowkey: "DenounceContractorProcess"
                                }).$promise.then(function (res) {
                                    var leaderList = [];
                                    for (var i = 0; i < res.length; i++) {
                                        if (i == res.length - 1)
                                            leaderList.push(res[i].Person)
                                    }
                                    if (leaderList.length <= 0) {
                                        Notifications.addError({
                                            'status': 'error',
                                            'message': $translate.instant('Leader_NO_MSG')
                                        });
                                        return
                                    } else {
                                        formVariables = [];
                                        historyVariable = [];

                                        formVariables.push({ name: "LeaderArray", value: leaderList });
                                        formVariables.push({ name: "VoucherID", value: $scope.data.DenounceID });
                                        formVariables.push({ name: "start_remark", value: $scope.data.ContractorName });

                                        historyVariable.push({ name: "VoucherID", value: $scope.data.DenounceCode });
                                        historyVariable.push({ name: "ContractorName", value: $scope.data.ContractorName });

                                        getDenounceChecker(function (checkerList) {
                                            if (checkerList.length > 0) {
                                                formVariables.push({ name: "HSEArray", value: checkerList });
                                                $scope.getFlowDefinitionId('DenounceContractorProcess', function (FlowDefinitionId) {
                                                    if (FlowDefinitionId) {
                                                        $scope.startflowid(FlowDefinitionId, "", formVariables, historyVariable, function (url, message) {
                                                            if (message) {
                                                                Notifications.addError({ 'status': 'error', 'message': message });
                                                            } else {
                                                                sendMail($scope.data.DenounceID, 'init', function (err) {
                                                                    $scope.resetModal();
                                                                    $scope.searchDenounce();
                                                                    $('#modalDenounceContractor').modal('hide');
                                                                    $("#modalDenounceContractor").on('hidden.bs.modal', function () {
                                                                        $location.path('/taskForm/' + url);
                                                                        $scope.$apply();
                                                                    })
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
                                            } else {
                                                Notifications.addError({ 'status': 'error', 'message': "Can not Get EHS Checker" });
                                            }
                                        })
                                    }
                                });
                            }
                            else if (status == 'ReSubmit') {
                                $scope.formVariables.push({ name: "update_result", value: "OK" });
                                $scope.historyVariable.push({ name: "update_result", value: "YES" });
                                getDenounceChecker(function (checkerList) {
                                    if (checkerList.length > 0) {
                                        $scope.formVariables.push({ name: "HSEArray", value: checkerList });
                                        sendMail($scope.variable.VoucherID, 'init', function (err) {
                                            $scope.submit();
                                        })
                                    } else {
                                        Notifications.addError({ 'status': 'error', 'message': "Can not Get EHS Checker" });
                                    }
                                }, function (errResponse) {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                })
                            }
                            else {
                                $scope.resetModal();
                                $scope.searchDenounce();
                                $('#modalDenounceContractor').modal('hide');
                                $timeout(function () {
                                    Notifications.addMessage({ 'status': 'info', 'message': $translate.instant('Save_Success_MSG') });
                                }, 500);
                            }
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }

                    function getDenounceChecker(callback) {
                        var HSEArray = [];
                        if ($scope.data.Unknow) {
                            HSEArray.push($scope.Safety + ',' + $scope.Environment + ',' + $scope.FireProtection);
                        } else {
                            var checker = ""
                            if ($scope.data.Safety) {
                                checker += $scope.Safety + ',';
                            }

                            if ($scope.data.Environment) {
                                checker += $scope.Environment + ',';
                            }

                            if ($scope.data.FireProtection) {
                                checker += $scope.FireProtection + ',';
                            }

                            HSEArray.push(checker.slice(0, -1));
                        }

                        callback(HSEArray)
                    }

                    $scope.saveSubmit = function () {
                        $scope.formVariables.push({ name: $scope.checker + "Agree", value: $scope.data.IsAgree });
                        $scope.historyVariable.push({ name: $scope.checker + "Agree", value: $scope.data.IsAgree });

                        if ($scope.data.IsAgree === "YES") {
                            if ($scope.checker == 'HSE') {
                                sendMail($scope.variable.VoucherID, "complete", function (err) {
                                    $scope.changeStatus('Q')
                                })
                            }
                            else {
                                sendMail($scope.variable.VoucherID, "LeaderSubmit", function (err) {
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
                        sendMail($scope.variable.VoucherID, 'reject', function (err) {
                            $scope.submit();
                        })
                    }

                    $scope.deleteRejectApp = function () {
                        $scope.formVariables.push({ name: "update_result", value: "NO" })
                        $scope.historyVariable.push({ name: "update_result", value: "NO" })
                        $scope.changeStatus('X')
                    }

                    $scope.changeStatus = function (_status) {
                        var query = {}
                        query.VoucherID = $scope.variable.VoucherID
                        query.status = _status
                        ConDisciplineService.ChangeStatus_Denounce().save(query, {}).$promise.then(function (res) {
                            console.log(res);
                            $scope.submit();
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        })
                    }

                    function sendMail(_AppID, _mailkind, callback) {
                        ConDisciplineService.SendMail_Denounce().post({
                            flowname: "DenounceContractorProcess",
                            VoucherID: _AppID,
                            FromUser: Auth.username,
                            MailKind: _mailkind,
                        }, {}).$promise.then(function (res) {
                            callback('')
                        }, function (err) {
                            callback(err)
                        });
                    }

                    var query = {};
                    query.language = lang;
                    query.contractorName = "";
                    query.cType = "";
                    query.departmentID = "";
                    query.userid = "";
                    query.status = "";

                    ConQuaService.ContractorQualification().get(query).$promise.then(function (res) {
                        $scope.AllContractorList = res;
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });

                    $scope.suggestionContractor = function (string) {
                        if ($scope.AllContractorList.length > 0) {
                            $scope.hidethis = string ? false : true;
                            var output = [];
                            output = $filter("filter")($scope.AllContractorList, { ContractorName: string });
                            $scope.filterContractorName = output;
                        }
                    }

                    $scope.fillTextboxContractor = function (string) {
                        $scope.data.ContractorName = string;
                        $scope.hidethis = true;
                    }

                    $scope.checkValidate = function () {
                        if (($scope.data.Safety || $scope.data.Environment || $scope.data.FireProtection || $scope.data.Unknow) && $scope.listfile.length > 0)
                            return false
                        else return true
                    }


                },
                templateUrl: './forms/ContractorDiscipline/Manage_Discipline/DenounceContractor/CreateDenounce.html'
            }
        }
    ]);
});
