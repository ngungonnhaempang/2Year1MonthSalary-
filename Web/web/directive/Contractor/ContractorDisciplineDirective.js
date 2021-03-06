
define(['app', 'angular'], function (app, angular) {
    app.directive('viewDiscipline', ['ConDisciplineService', 'ModificationService', 'ConQuaService', 'socket', '$resource', '$http', '$filter', 'Notifications', '$routeParams', '$translate', '$timeout', 'GateGuest', 'EngineApi', '$location', 'Auth', 'Forms', '$route', '$window', '$upload',
        function (ConDisciplineService, ModificationService, ConQuaService, socket, $resource, $http, $filter, Notifications, $routeParams, $translate, $timeout, GateGuest, EngineApi, $location, Auth, Forms, $route, $window, $upload) {
            return {
                restrict: 'E',
                controller: function ($scope) {
                    var lang = window.localStorage.lang;
                    $scope.query = {};
                    $scope.data = {};
                    $scope.Print = false;
                    $scope.Payment = false;
                    $scope.ChildList = []
                    $scope.ParentList = []
                    $scope.CategoryList = []
                    $scope.DisciplineList = []
                    $scope.listfileEvidence = [];
                    $scope.listfileRecords = [];
                    $scope.listEmail = [];
                    $scope.filePayment = [];
                    $scope.btnFile = false;
                    $scope.EditPayment = false;
                    $scope.hidethis = true;
                    $scope.showProcessRecord = false;
                    $scope.showProcessEvidence = false;
                    $scope.totalFileSize = 0;
                    $scope.listEmployee = [];
                
                    $scope.btnShowCategory = function (id) {
                        $(".collapse.show").each(function(){
                            $(this).prev(".card-header").find(".glyphicon").addClass("glyphicon-minus-sign").removeClass("glyphicon-plus-sign");
                        });
                        
                        // Toggle plus minus icon on show hide of collapse element
                        $(".collapse").on('show.bs.collapse', function(){
                            $(this).prev(".card-header").find(".glyphicon").removeClass("glyphicon-plus-sign").addClass("glyphicon-minus-sign");
                        }).on('hide.bs.collapse', function(){
                            $(this).prev(".card-header").find(".glyphicon").removeClass("glyphicon-minus-sign").addClass("glyphicon-plus-sign");
                        });
                    }

                    var query = {};
                    query.language = lang;
                    query.contractorName = "";
                    query.cType = "";
                    query.departmentID = "";
                    query.userid = "";
                    query.status = "";
                    $scope.data.listfile_Evidence = false;
                    $scope.data.listfile_Records = false;
                    $scope.data.TotalFine = 0;
                    $scope.ContractorList = [];

                    ConDisciplineService.GetContractorNotOnSytem().get().$promise.then(function (res) {
                        debugger;
                        if (res.length > 0) {
                            $scope.ContractorList = res;
                        }
                        console.log($scope.ContractorList);
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });


                    $scope.suggestionContractor = function (string) {
                        if ($scope.ContractorList.length > 0) {
                            $scope.hidethis = string ? false : true;
                            var output = [];
                            output = $filter("filter")($scope.ContractorList, { ContractorName: string });
                            $scope.filterContractorName = output;
                        }
                    }

                    $scope.fillTextboxContractor = function (string) {
                        $scope.data.ContractorName = string;
                        $scope.hidethis = true;
                    }

                    $scope.printApp = function () {
                        window.print();
                    }

                    $scope.goBack = function () {
                        window.history.back();
                    }

                    $scope.checkValidate = function () {
                        if ($scope.data.TotalFine > 0 && $scope.listfileEvidence.length > 0 && $scope.listfileRecords.length > 0) {
                            return false
                        } else return true
                    }

                    $scope.checkValidateFile = function () {
                        if ($scope.filePayment.length > 0)
                            return false;
                        else return true;
                    }

                    /*** Upload file ***/
                    $scope.btnfile = function (id, filename) {
                        $('#' + id).click();
                    }

                    $scope.UploadFileHSE = function ($files, _colName) {
                        var isValidFile;
                        if (_colName != "payment") {
                            isValidFile = checkFileLimited($files, 17, 5);
                        } else {
                            isValidFile = checkFileLimited($files, 17, 1);
                        }

                        if (!isValidFile.success) {
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant(isValidFile.message)
                            });

                        } else {
                            $upload.upload({
                                url: '/ehs/contractor/files/UploadFileDiscipline',
                                method: "POST",
                                file: $files
                            }).progress(function (evt) {
                                if (_colName == "DisciplineEvidence")
                                    $scope.showProcessEvidence = true;
                                else $scope.showProcessRecord = true;
                                var percentInt = parseInt(100.0 * evt.loaded / evt.total);
                                console.log('percent: ' + percentInt);
                                $("#MyFileUploadProcessBar").css("width", percentInt.toString() + "%");
                                $("#MyFileUploadProcessBarText").html("uploaded" + percentInt + "%");
                                if (percentInt = 0) {
                                    $("#MyFileUploadProcessBarDiv").show();
                                    $scope.showProcessEvidence = false;
                                    $scope.showProcessRecord = false;
                                } else if (percentInt = 100) {
                                    setTimeout(function () {
                                        $("#MyFileUploadProcessBar").css("width", "0%");
                                        $scope.showProcessEvidence = false;
                                        $scope.showProcessRecord = false;
                                    }, 1000);
                                }
                            }).then(function (res) {
                                res.data.forEach(x => {
                                    var file = x.FileID.toLowerCase();
                                    debugger;

                                    var dt = {
                                        name: x.FileID,
                                        col: _colName,
                                        size: x.MegabyteSize
                                    };

                                    console.log(dt);

                                    if (_colName == "payment") {
                                        if ($scope.filePayment.length > 0) {
                                            Notifications.addError({
                                                'status': 'error',
                                                'message': $translate.instant('File_Number_MSG')
                                            });
                                            deleteFileInFolder(x.FileID);
                                        } else {
                                            $scope.filePayment.push(dt);
                                        }
                                    } else {
                                        if ((file.includes(".jpg") || file.includes(".jpeg") || file.includes(".png")) && dt.col == "DisciplineEvidence") {
                                            $scope.listfileEvidence.push(dt);
                                            $scope.data.listfile_Evidence = true;
                                            $scope.totalFileSize += dt.size;
                                        } else if ((file.includes(".pdf") || file.includes(".mp4")) && dt.col == "DisciplineRecords") {
                                            $scope.listfileRecords.push(dt);
                                            $scope.data.listfile_Records = true;
                                            $scope.totalFileSize += dt.size;
                                        } else {
                                            $timeout(function () {
                                                Notifications.addMessage({
                                                    'status': 'error',
                                                    'message': $translate.instant('FormatFile_MSG')
                                                });
                                            }, 300);
                                            deleteFileInFolder(x.FileID);
                                        }
                                    }
                                })
                            })
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

                    /**
                     * Create by Isaac 2019-07-23
                     * @param {file In Upload} $files get all files when Upload
                     * @param {*} maximumSize 
                     * @param {number of TotalFiles} totalFile 
                     */

                    function checkFileLimited($files, maximumSize, totalFile) {
                        const fileCount = $files.length;
                        const maximumFileSize = maximumSize * 1024 * 1024
                        var objectResult = {
                            success: true,
                            message: "Upload Completed!"
                        };

                        if (fileCount > totalFile) {
                            objectResult.success = false;
                            objectResult.message = $translate.instant('OverUpload') + `${totalFile}` + ' file';
                        } else {
                            var totalSize = $scope.totalFileSize;
                            $files.forEach(item => {
                                if (item.size > maximumFileSize) {
                                    objectResult.success = false;
                                    objectResult.message = $translate.instant('OverUpload') + ` ${maximumSize}MB `;
                                } else {
                                    debugger;
                                    totalSize += item.size / 1000000;
                                    if (totalSize > 17) {
                                        objectResult.success = false;
                                        objectResult.message = $translate.instant('OverUpload') + ` ${maximumSize}MB `;
                                    }
                                }
                            })
                        }
                        return objectResult;
                    }

                    $scope.removeFile = function (index, col) {
                        if (col == "DisciplineEvidence") {
                            $scope.totalFileSize = $scope.totalFileSize - $scope.listfileEvidence[index].size;
                            if ($routeParams.DisciplineID)
                                deleteFileInFolder($scope.listfileEvidence[index].name);

                            $scope.listfileEvidence.splice(index, 1);


                            if ($scope.listfileEvidence.length == 0) {
                                $scope.data.listfile_Evidence = false;
                            }
                        } else if (col == "DisciplineRecords") {
                            $scope.totalFileSize = $scope.totalFileSize - $scope.listfileRecords[index].size;

                            if ($routeParams.DisciplineID)
                                deleteFileInFolder($scope.listfileRecords[index].name);

                            $scope.listfileRecords.splice(index, 1);

                            if ($scope.listfileRecords.length == 0) {
                                $scope.data.listfile_Records = false;
                            }
                        } else {
                            $scope.filePayment = [];
                        }
                    }

                    debugger;
                    /*** Get Categories ***/
                    ConDisciplineService.SearchISO().get({
                        Language: lang,
                        code: '',
                        Status: 'A',
                        FromDate: '',
                        ToDate: '',
                    }).$promise.then(function (res) {
                        if ($routeParams.code != "Create") {
                            $scope.IsUpdate = true
                            if ($routeParams.code == "Print") {
                                $scope.Print = true;
                            }

                            if ($routeParams.code == "Payment" || $routeParams.code == "EditPayment") {
                                $scope.Payment = true;
                            }

                            if ($routeParams.code == "EditPayment") {
                                $scope.EditPayment = true;
                            }

                            $scope.loadDiscipline($routeParams.DisciplineID)
                        } else {
                            $scope.IsUpdate = false
                            var status = 'A'
                            var date = ''
                            showCategories(status, date, function (errorCategory) {
                                if (errorCategory != '') {
                                    Notifications.addError({ 'status': 'error', 'message': errorCategory });
                                    return;
                                }
                            })
                        }

                        if (res.length > 0) {
                            $scope.ISO_AppCode = res[0].ISO_AppCode;
                            $scope.data.ISO_ID = res[0].ISO_ID;
                        }
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });

                    function showCategories(status, date, callback) {
                        ConDisciplineService.SearchCategory().get({
                            Lan: lang,
                            code: '',
                            Status: status,
                            Content: '',
                            FromDate: date,
                            ToDate: date,
                            type:''
                        }).$promise.then(function (res) {
                            res.forEach(element => {
                                element.isSelect = false
                                $scope.CategoryList.push(element)
                            })
                            if ($scope.CategoryList.length > 0) {
                                $scope.ParentList = $filter("filter")($scope.CategoryList, { Parent: null });
                                $scope.ChildList = $filter("filter")($scope.CategoryList, { Parent: '' });
                                callback('')
                            } else {
                                callback('')
                            }
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                            callback(errResponse)
                        });
                    }

                    /** LOAD VOUCHER **/
                    $scope.loadDiscipline = function (_AppID) {
                        ConDisciplineService.GetDiscipline().get({
                            Lan: lang,
                            AppID: _AppID
                        }).$promise.then(function (res) {
                            $scope.data = res.TableDiscipline[0]
                            $scope.data.TimeViolation = $filter('date')($scope.data.TimeViolation, 'yyyy-MM-dd HH:mm:ss')

                            if ($scope.data.PaymentDate) {
                                $scope.data.FinePaymentDate = $filter('date')($scope.data.PaymentDate, 'yyyy-MM-dd HH:mm:ss')
                            }

                            $scope.data.EmployeeName = $scope.data.ContractorByEmployee + ' - ' + $scope.data.EmployeeName

                            if ($routeParams.code == "Payment" || $routeParams.code == "EditPayment") {
                                $scope.data.ConfirmUserName = Auth.nickname;
                            }

                            if ($scope.data.IsConOther == 1) {
                                $scope.data.IsConOther = true;
                                $scope.showEmployeeList($scope.data.Department)
                            } else $scope.data.IsConOther = false

                            $scope.DisciplineDetail = res.TableDetail

                            $scope.FileAttached = res.TableFile

                            $scope.totalFileSize = $scope.FileAttached[0].totalFileSize

                            $scope.FileAttached.forEach(element => {
                                var x = {};
                                x.AppID = element.AppID;
                                x.name = element.FileID;
                                x.col = element.AttachedForItem;
                                x.size = element.MegabyteSize;

                                if (x.col == "DisciplineEvidence") {
                                    $scope.listfileEvidence.push(x);
                                    $scope.data.listfile_Evidence = true
                                } else if (x.col == "DisciplineRecords") {
                                    $scope.listfileRecords.push(x);
                                    $scope.data.listfile_Records = true
                                } else {
                                    $scope.filePayment.push(x);
                                }
                            })

                            if ($scope.data.Status == 'WP' || $scope.data.Status == 'P' || $scope.checker == 'Leader') {
                                $scope.GetInformation($routeParams.DisciplineID, $scope.data.Status)
                            }

                            var status = 'A'
                            var date = ''

                            if ($routeParams.code != 'Create') {
                                status = '';
                                date = $scope.data.CreateDate;
                            }

                            showCategories(status, date, function (errorCategory) {
                                if (errorCategory != '') {
                                    Notifications.addError({ 'status': 'error', 'message': errorCategory });
                                    return;
                                } else {
                                    $scope.ChildList.forEach(element => {
                                        var _category = $filter("filter")($scope.DisciplineDetail, { CategoryID: element.ID }, true);
                                        if (_category.length > 0) {
                                            element.Quantity = _category[0].Quantity
                                            element.Fine = _category[0].Fine
                                            $scope.DisciplineList.push(element);
                                        }
                                    })
                                    $scope.checkAllCategory();
                                }
                            })
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }

                    /*** Get Contractor ***/
                    ConQuaService.ContractorQualification().get(query).$promise.then(function (res) {
                        $scope.AllContractorList = res;
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });

                    $scope.showEmployeeList = function (dept_id) {
                        if (dept_id == null || dept_id == '') return;
                        ConQuaService.GetEmployee({
                            DepartmentID: dept_id
                        }, function (res) {
                            if (res.length > 0) {
                                $scope.listEmployee = res;
                            } else $scope.listEmployee = [];
                        })
                    }

                    $scope.setContractor = function (_contractorID) {
                        var conOther = $filter("filter")($scope.AllContractorList, { ContractorID: _contractorID }, true);
                        if (conOther.length > 0) {
                            ConQuaService.ContractorQualification().getDetailHeader({ "contractorID": conOther[0].ContractorID, language: lang }).$promise.then(function (res) {
                                $scope.data.Department = res[0].DepartmentID;
                                $scope.data.ContractorCode = res[0].Rcode;
                                $scope.data.ContractorByEmployee = res[0].ContractorByEmloyee;
                                $scope.data.EmployeeName = res[0].ContractorByEmloyee + ' - ' + res[0].ResponsiblePerson;
                                $scope.data.Email = res[0].Email;
                                $scope.data.EmailContractor = res[0].EmailContractor;
                                $scope.data.ContractorID = res[0].ContractorID;
                                $scope.data.ContractorName = res[0].ContractorName;
                            }, function (errResponse) {
                                Notifications.addError({ 'status': 'error', 'message': errResponse });
                            });
                        }
                    }

                    ModificationService.GetDeparment().get({ language: lang }).$promise.then(function (res) {
                        $scope.DepartmentList = res;
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });


                    $scope.getInfoPersonInCharge = function () {
                        var InfoPersonInCharge = $filter("filter")($scope.listEmployee, { EmployeeID: $scope.data.ContractorByEmployee }, true);
                        $scope.data.Email = InfoPersonInCharge[0].Email;

                    }



                    $scope.clearData = function () {
                        $scope.data.ContractorCode = '';
                        $scope.data.ContractorName = '';
                        $scope.data.ContractorID = '';
                        $scope.data.Department = '';
                        $scope.data.ContractorByEmployee = '';
                        $scope.data.EmployeeName = '';
                        $scope.data.EmailContractor = '';
                        $scope.data.Email = '';
                        $scope.data.ConstructionItems = '';
                    }

                    $scope.checkParent = function (_parent) {
                        if (!_parent.isSelect) {
                            var childRemoveList = $filter("filter")($scope.DisciplineList, { Parent: _parent.Code });
                            if (childRemoveList.length > 0) {
                                childRemoveList.forEach(element => {
                                    $scope.deleteDiscipline(element);
                                });
                            }
                        }
                    }

                    $scope.checkAllCategory = function () {
                        $scope.ParentPrintList = []
                        if ($scope.DisciplineList.length != 0) {
                            $scope.ParentList.forEach(element => {
                                var list = $filter("filter")($scope.DisciplineList, { Parent: element.Code });
                                if (list.length > 0) {
                                    element.isSelect = true
                                    $scope.ParentPrintList.push(element)
                                } else element.isSelect = false
                            })
                        } else {
                            $scope.ParentList.forEach(element => {
                                element.isSelect = false
                            })
                        }
                    }

                    $scope.checkChildList = function (code, value) {
                        if (code == "add") {
                            $scope.ChildList = $filter("filter")($scope.ChildList, { ID: '!' + value.ID }, true);
                            $scope.checkAllCategory()
                        } else if (code == "delete") {
                            value.Fine = null;
                            value.Quantity = null;
                            $scope.ChildList.push(value);
                            $scope.checkAllCategory()
                        }

                    }

                    $scope.setTotal = function () {
                        if ($scope.DisciplineList.length >= 0) {
                            $scope.data.TotalFine = 0
                            $scope.DisciplineList.forEach(element => {
                                element.total = element.Quantity * element.Fine
                                $scope.data.TotalFine = $scope.data.TotalFine + element.total
                            })
                        }
                    }

                    $scope.addDiscipline = function (_idList) {
                        if (_idList != '') {
                            _idList.forEach(element => {
                                var dis = $filter("filter")($scope.CategoryList, { ID: element });
                                if (dis.length > 0) {
                                    dis[0].Quantity = 1
                                    if (dis[0].Fine == null) {
                                        dis[0].noFine = true
                                    } else dis[0].noFine = false
                                    $scope.DisciplineList.push(dis[0]);
                                    $scope.checkChildList("add", dis[0]);
                                    $scope.setTotal();
                                }
                            })
                        }
                    };

                    $scope.deleteDiscipline = function (value) {
                        $scope.DisciplineList = $filter("filter")($scope.DisciplineList, { ID: '!' + value.ID }, true);
                        $scope.checkChildList("delete", value);
                        $scope.setTotal();
                    };

                    $scope.$watch("data.Department", function (deptID) {
                        debugger;
                        if (deptID) {
                            console.log(deptID);
                            var dept = $filter("filter")($scope.DepartmentList, { DepartmentID: deptID }, true);
                            if (dept.length > 0) {
                                $scope.data.DeptName = dept[0].DepartmentID + ' - ' + dept[0].Specification_VN + ' ' + dept[0].Specification_TW;
                            }
                        }
                    });

                    $scope.$watch("data.ContractorByEmployee", function (EmpID) {
                        if (EmpID && $scope.listEmployee.length > 0) {
                            debugger;
                            console.log(EmpID);
                            var Emp = $filter("filter")($scope.listEmployee, { EmployeeID: EmpID }, true);
                            if (Emp.length > 0) {
                                $scope.data.EmployeeName = EmpID + ' - ' + Emp[0].Name;
                            }
                        }
                    });



                    /*** SAVE VOUCHER ***/
                    var formVariables = [];
                    var historyVariable = [];
                    $scope.saveVoucher = function (status) {
                        var lsFile = [];
                        var lsDetail = [];
                        if ($scope.DisciplineList.length > 0) {
                            $scope.DisciplineList.forEach(element => {
                                var f = {};
                                f.DisciplineID = $scope.data.DisciplineID || null;
                                f.CategoryID = element.ID;
                                f.Fine = element.Fine;
                                f.Quantity = element.Quantity
                                lsDetail.push(f);
                            })
                        }
                        if ($scope.listfileEvidence.length > 0) {
                            $scope.listfileEvidence.forEach(element => {
                                var f = {};
                                f.FileID = element.name
                                f.AppID = $scope.data.DisciplineID || null
                                f.AttachedForItem = element.col;
                                f.MegabyteSize = element.size;
                                lsFile.push(f);
                            })
                        }

                        if ($scope.listfileRecords.length > 0) {
                            $scope.listfileRecords.forEach(element => {
                                var f = {};
                                f.FileID = element.name
                                f.AppID = $scope.data.DisciplineID || null
                                f.AttachedForItem = element.col;
                                f.MegabyteSize = element.size;
                                lsFile.push(f);
                            })
                        }

                        var params = {}
                        params.DisciplineID = $scope.data.DisciplineID || null;
                        params.DisciplineCode = $scope.data.DisciplineCode || null;
                        params.LocationViolation = $scope.data.LocationViolation;
                        params.TimeViolation = $scope.data.TimeViolation;
                        if (status == 'ReSubmit' || status == 'Submit') {
                            params.Status = 'F';
                        } else {
                            params.Status = 'N';
                        }
                        params.DescriptionViolation = $scope.data.DescriptionViolation;
                        params.Creator = Auth.username;
                        params.TotalFine = $scope.data.TotalFine;
                        params.ISO_ID = $scope.data.ISO_ID;
                        params.Department = $scope.data.Department;
                        params.ContractorCode = $scope.data.ContractorCode;
                        params.ContractorByEmployee = $scope.data.ContractorByEmployee;
                        params.Email = $scope.data.Email;
                        params.EmailContractor = $scope.data.EmailContractor;
                        params.ContractorID = $scope.data.ContractorID || null;
                        params.ContractorName = $scope.data.ContractorName;
                        params.ConstructionItems = $scope.data.ConstructionItems;
                        params.LinkFile = $scope.data.LinkFile || null;

                        if ($scope.data.IsConOther == true) {
                            params.IsConOther = 1
                        } else params.IsConOther = 0

                        params.CategoryList = lsDetail;
                        params.files = lsFile;

                        ConDisciplineService.Save_Discipline().save({}, params).$promise.then(function (res) {
                            $scope.data.DisciplineID = res.DisciplineID
                            $scope.data.DisciplineCode = res.DisciplineCode
                            if (status == 'Submit') {
                                GetChecker(Auth.username, function (errorChecker) {
                                    if (errorChecker != '') {
                                        Notifications.addError({ 'status': 'error', 'message': errorChecker });
                                        return;
                                    } else {
                                        formVariables.push({ name: "HSEArray", value: $scope.listChecker });
                                        formVariables.push({ name: "VoucherID", value: $scope.data.DisciplineID });
                                        formVariables.push({ name: "start_remark", value: $scope.data.DisciplineCode + ' - ' + $scope.data.ContractorName });

                                        formVariables.push({ name: "CcEmail", value: "ctthuy@fenc.vn,ttnhan.it@fenc.vn" }); // $scope.data.Email -- person in charge's email
                                        formVariables.push({ name: "BccEmail", value: "ctthuy@fenc.vn,ttnhan.it@fenc.vn" });
                                        formVariables.push({ name: "Title", value: "[10.20.46.23] TH??NG B??O C???A H??? TH???NG X??? PH???T NH?? TH???U - ?????????????????????????????????????????????" });
                                        debugger;
                                        formVariables.push({ name: "ContentMail", value: getContentMail($scope.data.DisciplineCode, $scope.data.ContractorName, $scope.data.DeptName, $scope.data.EmployeeName) });

                                        historyVariable.push({ name: "VoucherID", value: $scope.data.DisciplineCode });
                                        historyVariable.push({ name: "ContractorName", value: $scope.data.ContractorName });

                                        GetChecker($scope.data.ContractorByEmployee, function (errorChecker) {
                                            if (errorChecker != '') {
                                                Notifications.addError({ 'status': 'error', 'message': errorChecker });
                                                return;
                                            } else {
                                                var person = $scope.listChecker[$scope.listChecker.length - 1]
                                                var LeaderArray = [];
                                                LeaderArray.push(person);
                                                var email = $scope.listEmail[$scope.listEmail.length - 1];
                                                console.log(email);
                                                formVariables.push({ name: "LeaderArray", value: LeaderArray });
                                                formVariables.push({ name: "LeaderEmail", value: "ctthuy@fenc.vn" });

                                                $scope.getFlowDefinitionId('ContractorDiscipline', function (FlowDefinitionId) {
                                                    if (FlowDefinitionId) {
                                                        $scope.startflowid(FlowDefinitionId, "", formVariables, historyVariable, function (url, message) {
                                                            if (message) {
                                                                Notifications.addError({ 'status': 'error', 'message': message });
                                                            } else {
                                                                sendMail($scope.data.DisciplineID, 'init', function (err) {
                                                                    $location.path('/taskForm/' + url);
                                                                });
                                                            }
                                                        })
                                                    } else {
                                                        Notifications.addError({ 'status': 'error', 'message': $translate.instant('Process_Err_MSG') });
                                                        return;
                                                    }
                                                })
                                            }
                                        })
                                    }
                                })
                            } else if (status == 'ReSubmit') {
                                $scope.formVariables.push({ name: "update_result", value: "OK" });
                                $scope.historyVariable.push({ name: "update_result", value: "YES" });
                                sendMail($scope.variable.VoucherID, 'init', function (err) {
                                    $scope.submit();
                                })
                            }
                            else {
                                $scope.goBack();
                                $scope.searchDiscipline();
                                $timeout(function () {
                                    Notifications.addMessage({ 'status': 'info', 'message': $translate.instant('Save_Success_MSG') });
                                }, 500);
                            }
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }

                    function GetChecker(_user, callback) {
                        $scope.listChecker = [];
                        $scope.listEmail = [];
                        ConDisciplineService.GetCheckers().get({
                            owner: _user,
                            flowkey: "ContractorDiscipline"
                        }).$promise.then(function (res) {
                            var leaderList = [];
                            var leaderEmailList = [];
                            for (var i = 0; i < res.length; i++) {
                                leaderList[i] = res[i].Person;
                                leaderEmailList[i] = res[i].Email;
                            }
                            if (leaderList.length <= 0) {
                                callback($translate.instant('Leader_NO_MSG'))
                                return
                            } else {
                                $scope.listChecker = leaderList;
                                $scope.listEmail = leaderEmailList;
                                callback('')
                            }
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                            callback(errResponse)
                        });
                    }

                    $scope.saveSubmit = function () {
                        $scope.formVariables.push({ name: $scope.checker + "Agree", value: $scope.data.IsAgree });
                        $scope.historyVariable.push({ name: $scope.checker + "Agree", value: $scope.data.IsAgree });

                        if ($scope.data.IsAgree === "YES") {
                            if ($scope.checker == 'Leader') {
                                $scope.exportPDF(function (err) {
                                    $scope.exportImagePDF(function (err) {
                                        sendMail($scope.variable.VoucherID, "complete", function (errMsg) {
                                            if (errMsg != '') {
                                                Notifications.addError({ 'status': 'error', 'message': errMsg });
                                            } else {
                                                $scope.changeStatus('WP')
                                            }

                                        })
                                    })
                                })
                            }
                            else {
                                var today = new Date();

                                $scope.formVariables.push({ name: "OverTwoDate", value: today });
                                sendMail($scope.variable.VoucherID, "hseLeaderSubmit", function (err) {
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
                        ConDisciplineService.ChangeStatus_Discipline().save(query, {}).$promise.then(function (res) {
                            console.log(res);
                            $scope.submit();
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        })
                    }

                    function sendMail(_voucherID, _mailkind, callback) {
                        ConDisciplineService.SendMail_Discipline().post({
                            flowname: "ContractorDiscipline",
                            VoucherID: _voucherID,
                            FromUser: _mailkind == "paymentFine" ? $scope.data.Creator : Auth.username,
                            MailKind: _mailkind
                        }, {}).$promise.then(function (res) {
                            callback('')
                        }, function (err) {
                            callback(err)
                        });
                    }

                    /** Information from MongoDB . Get who receive the voucher and approved it */
                    $scope.GetInformation = function (_AppID, _status) {
                        ConDisciplineService.GetDisciplinePID().get({ VoucherID: _AppID }).$promise.then(function (res) {
                            console.log(res);
                            if (res) {
                                EngineApi.getProcessLogs.getList({ "id": res.ProcessInstanceId, "cId": "" }, function (data) {
                                    console.log(data[0].Logs);

                                    data[0].Logs = $filter('orderBy')(data[0].Logs, 'FormatStamp');
                                    var taf = TAFFY(data[0].Logs);

                                    var receiver = [];
                                    if (data[1] != null || data[1] != undefined) {
                                        var taf_1 = TAFFY(data[1].Logs);
                                        receiver[0] = taf({ TaskName: "????????????" }).first() || taf_1({ TaskName: "????????????" }).first(); //initiator
                                    } else {
                                        receiver[0] = taf({ TaskName: "????????????" }).first(); //initiator
                                    }

                                    receiver[1] = taf({ TaskName: "HSE Leader Check" }).start(1).first();
                                    receiver[2] = taf({ TaskName: "HSE Leader Check" }).last();

                                    if ($scope.checker != 'Leader') {
                                        receiver[3] = taf({ TaskName: "Leader Check" }).start(1).first();
                                    }
                                    $scope.receiver = receiver;
                                    console.log($scope.receiver)

                                })
                            }

                        }, function (err) {
                            Notifications.addError({
                                'status': 'error',
                                'message': err
                            });
                        })
                    }

                    $scope.Convert = function (number) {
                        if (number >= 0) {
                            return (number).toLocaleString('vi')
                        }
                    }

                    $scope.formatCurrency = function (category) {
                        if (category.Fine === "") { return; }
                        $("#Fine").val(category.Fine.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, "."));

                        $scope.setTotal()
                    }

                    /**S??? th??nh ch??? */
                    var ChuSo = [" kh??ng ", " m???t ", " hai ", " ba ", " b???n ", " n??m ", " s??u ", " b???y ", " t??m ", " ch??n "];
                    var Tien = ["", " ngh??n", " tri???u", " t???", " ngh??n t???", " tri???u t???"];

                    //1. H??m ?????c s??? c?? ba ch??? s???;
                    function DocSo3ChuSo(baso) {
                        var tram;
                        var chuc;
                        var donvi;
                        var KetQua = "";
                        tram = parseInt(baso / 100);
                        chuc = parseInt((baso % 100) / 10);
                        donvi = baso % 10;
                        if (tram == 0 && chuc == 0 && donvi == 0) return "";
                        if (tram != 0) {
                            KetQua += ChuSo[tram] + " tr??m ";
                            if ((chuc == 0) && (donvi != 0)) KetQua += " l??? ";
                        }
                        if ((chuc != 0) && (chuc != 1)) {
                            KetQua += ChuSo[chuc] + " m????i";
                            if ((chuc == 0) && (donvi != 0)) KetQua = KetQua + " l??? ";
                        }
                        if (chuc == 1) KetQua += " m?????i ";
                        switch (donvi) {
                            case 1:
                                if ((chuc != 0) && (chuc != 1)) {
                                    KetQua += " m???t ";
                                }
                                else {
                                    KetQua += ChuSo[donvi];
                                }
                                break;
                            case 5:
                                if (chuc == 0) {
                                    KetQua += ChuSo[donvi];
                                }
                                else {
                                    KetQua += " l??m ";
                                }
                                break;
                            default:
                                if (donvi != 0) {
                                    KetQua += ChuSo[donvi];
                                }
                                break;
                        }
                        return KetQua;
                    }

                    //2. H??m ?????c s??? th??nh ch??? (S??? d???ng h??m ?????c s??? c?? ba ch??? s???)
                    $scope.DocTienBangChu = function (SoTien) {
                        var lan = 0;
                        var i = 0;
                        var so = 0;
                        var KetQua = "";
                        var tmp = "";
                        var ViTri = [];
                        if (SoTien < 0) return "S??? ti???n ??m !";
                        if (SoTien == 0) return "Kh??ng ?????ng !";
                        if (SoTien > 0) {
                            so = SoTien;
                        }
                        else {
                            so = -SoTien;
                        }
                        if (SoTien > 8999999999999999) {
                            //SoTien = 0;
                            return "S??? qu?? l???n!";
                        }
                        ViTri[5] = Math.floor(so / 1000000000000000);
                        if (isNaN(ViTri[5]))
                            ViTri[5] = "0";
                        so = so - parseFloat(ViTri[5].toString()) * 1000000000000000;
                        ViTri[4] = Math.floor(so / 1000000000000);
                        if (isNaN(ViTri[4]))
                            ViTri[4] = "0";
                        so = so - parseFloat(ViTri[4].toString()) * 1000000000000;
                        ViTri[3] = Math.floor(so / 1000000000);
                        if (isNaN(ViTri[3]))
                            ViTri[3] = "0";
                        so = so - parseFloat(ViTri[3].toString()) * 1000000000;
                        ViTri[2] = parseInt(so / 1000000);
                        if (isNaN(ViTri[2]))
                            ViTri[2] = "0";
                        ViTri[1] = parseInt((so % 1000000) / 1000);
                        if (isNaN(ViTri[1]))
                            ViTri[1] = "0";
                        ViTri[0] = parseInt(so % 1000);
                        if (isNaN(ViTri[0]))
                            ViTri[0] = "0";
                        if (ViTri[5] > 0) {
                            lan = 5;
                        }
                        else if (ViTri[4] > 0) {
                            lan = 4;
                        }
                        else if (ViTri[3] > 0) {
                            lan = 3;
                        }
                        else if (ViTri[2] > 0) {
                            lan = 2;
                        }
                        else if (ViTri[1] > 0) {
                            lan = 1;
                        }
                        else {
                            lan = 0;
                        }
                        for (i = lan; i >= 0; i--) {
                            tmp = DocSo3ChuSo(ViTri[i]);
                            KetQua += tmp;
                            if (ViTri[i] > 0) KetQua += Tien[i];
                            if ((i > 0) && (tmp.length > 0)) KetQua;
                        }

                        KetQua = KetQua.substring(1, 2).toUpperCase() + KetQua.substring(2);
                        return KetQua + ' ?????ng';
                    }

                    $scope.numberToChinese = function (num) {
                        var s = '';
                        if (num < 0)
                            num = abs(num);

                        var chineseOfNumber = ['???', '???', '???', '???', '???', '???', '???', '???', '???', '???', '???', '???', '???', '???', '???'];
                        var bit = 0;
                        var tmp = num;
                        if (tmp == 0)
                            s = chineseOfNumber[0];
                        while (tmp > 0) {
                            tmp = Math.floor(tmp / 10);
                            bit += 1;
                        }

                        tmp = num;
                        while (tmp > 0) {
                            if (tmp < 10) {
                                s += chineseOfNumber[tmp];
                                tmp -= 10;
                            }

                            else if (tmp < 100) {
                                s += chineseOfNumber[Math.floor(tmp / 10)];
                                s += '???';
                                tmp = tmp % 10;
                            }

                            else if (tmp < 1000) {
                                s += chineseOfNumber[Math.floor(tmp / 100)];
                                s += '???';
                                tmp = tmp % 100;
                                if (tmp < 10 && tmp > 0)
                                    s += '???';
                            }

                            else if (tmp < 10000) {
                                s += chineseOfNumber[Math.floor(tmp / 1000)];
                                s += '???';
                                tmp = tmp % 1000;
                                if (tmp < 100 && tmp > 0)
                                    s += '???';
                            }

                            else if (tmp < 100000000) {
                                s += $scope.numberToChinese(Math.floor(tmp / 10000));
                                s += '???';
                                tmp = tmp % 10000;
                                if (tmp < 1000 && tmp > 0)
                                    s += '???';
                            }

                            else if (tmp >= 100000000) {
                                s += $scope.numberToChinese(Math.floor(tmp / 100000000));
                                s += '???';
                                tmp = tmp % 100000000;
                                if (tmp < 10000000 && tmp > 0)
                                    s += '???';
                            }

                            else continue;
                        }

                        return s;

                    }


                    $scope.$watch("data.FinePaymentDate", function (date) {
                        if (date !== undefined) {
                            console.log(date);
                            $scope.data.PaymentDate = new Date(date);
                        }
                    });

                    $scope.confirmPayment = function () {
                        ConDisciplineService.ConfirmPayment().post({
                            DisciplineID: $scope.data.DisciplineID,
                            paymentDate: $scope.data.FinePaymentDate,
                            confirmUserID: Auth.username,
                            contractorRepresentative: $scope.data.ContractorRepresentative,
                            FileID: $scope.filePayment[0].name,
                            modifyReason: $scope.data.ModifyReason || '',
                            type: $routeParams.code == "Payment" ? "Payment" : "EditPayment"
                        }, {}).$promise.then(function (res) {
                            sendMail($scope.data.DisciplineID, 'paymentFine', function (err) {
                                $location.url('/ContractorDiscipline/Discipline/' + $scope.data.DisciplineCode);
                                $timeout(function () {
                                    Notifications.addMessage({ 'status': 'info', 'message': $translate.instant('Update_Success_MSG') });
                                }, 500);
                            })
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });



                    }

                    function getContentMail(DisciplineCode, ContractorName, DeptName, PersonInCharge) {
                        return '<html><head><style> #tbInfo {border-collapse: collapse; width: 100%; }' +
                            '#tbInfo td, #tbInfo th { border: 1px solid #ddd; padding: 8px; }' +
                            '#tbInfo th { padding-top: 12px; padding-bottom: 12px;text-align: left;background-color: #F2F2F2; width: 35%;}' +
                            '.linksystem {font-weight: bold;color: darkblue;}' +
                            '.fontchina {font-family: "Times New Roman", "DFKai-SB";font-size: 15pt;}</style></head>' +
                            '<body><div class="fontchina">Ch??o Anh/Ch???, <b>"H??? th???ng x??? ph???t nh?? th???u"</b> xin th??ng b??o, Anh/Ch??? c?? ????n sau c???n x??? l??:' +
                            '<br> ?????????<b>?????????????????????????????????????????????</b> ????????????????????????????????????' +
                            '<br><br>' +
                            '<table id="tbInfo">' +
                            '<tr><th> M?? ????n ?????? </th><td><b style="color:red">' + DisciplineCode + '</b></td></tr>' +
                            '<tr><th> Lo???i ????n ?????? </th><td>????N X??? PH???T NH?? TH???U VI PH???M AN TO??N V??? SINH ????????????????????????????????????</td></tr>' +
                            '<tr><th> T??n nh?? th???u vi ph???m ????????????????????? </th><td><b style="background-color:yellow">' + ContractorName + '</b></td></tr>' +
                            '<tr><th> B??? ph???n ph??t th???u ???????????? </th><td>' + DeptName + '</td></tr>' +
                            '<tr><th> Ng?????i ph??? tr??ch ph??t th???u ??????????????? </th><td>' + PersonInCharge + '</b></td></tr>' +
                            '<tr><th> Ng?????i n???p ????n ????????? </th><td>' + Auth.username + ' - ' + Auth.nickname + '</td></tr>' +
                            '</table>' +
                            '<br>' +
                            '<b><u>L???i nh???c | ????????????:</u></b>' +
                            '<br> (1) H??? th???ng c?? bi???u ????n ch??? Anh/ Ch??? x??? l??, vui l??ng v??o <a href="http://10.20.46.22:843/" class="linksystem">h??? th???ng</a> ????? x??c nh???n.' +
                            '<br> &nbsp&nbsp&nbsp ???????????????????????????????????????<a href="http://10.20.46.22:843/" class="linksystem">??????</a>?????????' +
                            '<br> (2) N???u Anh/Ch??? ???? x??? l?? bi???u ????n n??y, h??y b??? qua l???i nh???c n??y.' +
                            '<br> &nbsp&nbsp&nbsp ?????????????????????????????????????????????????????????' +
                            '<br> (3) N???u c?? b???t k??? v???n ????? n??o v??? h??? th???ng, vui l??ng li??n h??? v???i <b>IT - Team MES</b> ????? ???????c h??? tr???. Xin c???m ??n!' +
                            '<br> &nbsp&nbsp&nbsp ???????????????????????????????????? <b>IT - Team MES</b> ???????????????!' +
                            ' </div></body></html>';
                    }

                    $scope.exportPDF = function (callback) {
                        pdfMake.fonts = {
                            Roboto: {
                                normal: 'Roboto-Regular.ttf',
                                bold: 'Roboto-Regular.ttf',
                                italics: 'Roboto-Regular.ttf',
                                bolditalics: 'Roboto-Regular.ttf'
                            },
                            TimesNewRoman: {
                                normal: 'Times-Regular.ttf',
                                bold: 'Times-Bold.ttf',
                                italics: 'Times-Italic.ttf',
                                bolditalics: 'Times-BoldItalic.ttf'
                            },
                            DFKaiSB:
                            {
                                normal: 'DFKai-SB.ttf',
                                bold: 'DFKai-SB.ttf',
                                italics: 'DFKai-SB.ttf',
                                bolditalics: 'DFKai-SB.ttf'

                            }
                        }

                        var docDefinition = {
                            pageSize: 'A4',
                            pageOrientation: 'portrait',
                            pageMargins: [12, 12, 12, 24], // left top right bottom

                            footer: {
                                text: $scope.ISO_AppCode,
                                alignment: 'right',
                                margin: [0, 0, 12, 0]
                            },
                            content: [
                                {
                                    style: 'title',
                                    text: 'C??NG TY TNHH POLYTEX FAR EASTERN (VI????T NAM)'
                                },
                                {
                                    style: 'title',
                                    text: '????????????(??????)????????????',
                                    fontSize: 14,
                                    font: 'DFKaiSB'
                                },
                                {
                                    style: 'title',
                                    text: '????N X??? PH???T NH?? TH???U VI PH???M AN TO??N V??? SINH'
                                },
                                {
                                    style: 'title',
                                    text: '????????????????????????????????????',
                                    fontSize: 14,
                                    font: 'DFKaiSB'
                                },
                                '\n'
                                ,
                                {
                                    alignment: 'justify',
                                    columns: [
                                        {
                                            width: '10%',
                                            text: 'M?? s??? ????n',
                                            bold: true

                                        },
                                        {
                                            width: '10%',
                                            text: '?????????: ',
                                            fontSize: 14,
                                            font: 'DFKaiSB',

                                        },
                                        {
                                            width: '23%',
                                            text: ' ' + $scope.data.DisciplineCode,
                                        },
                                        {
                                            width: 'auto',
                                            text: 'Ng??y l???p ????n ',
                                            bold: true
                                        },
                                        {
                                            width: '13%',
                                            text: '???????????????',
                                            fontSize: 14,
                                            font: 'DFKaiSB',
                                            bold: true
                                        },
                                        {
                                            width: 'auto',
                                            text: 'N??m ' + filterDate($scope.data.CreateDate, 'yyyy'),
                                        },
                                        {
                                            width: '3%',
                                            text: '???',
                                            font: 'DFKaiSB'
                                        },
                                        {
                                            width: 'auto',
                                            text: ' th??ng ' + filterDate($scope.data.CreateDate, 'MM'),
                                        },
                                        {
                                            width: '3%',
                                            text: ' ???',
                                            font: 'DFKaiSB'
                                        },
                                        {
                                            width: 'auto',
                                            text: ' ng??y ' + filterDate($scope.data.CreateDate, 'dd'),
                                        },
                                        {
                                            width: 'auto',
                                            text: ' ???',
                                            font: 'DFKaiSB'
                                        }

                                    ]

                                },
                                {
                                    table: {
                                        style: "tbDiscipline",
                                        dontBreakRows: true,
                                        heights: 0,
                                        headerRows: 0,
                                        widths: [100, 'auto', 100, 150],
                                        body: [
                                            [{ style: 'headerTb', text: ['Nh?? th???u vi ph???m ', { text: '\r???????????????', font: 'DFKaiSB' }], rowSpan: 2 }
                                                , { text: $scope.data.ContractorName, rowSpan: 2 }
                                                , { style: 'headerTb', text: ['Th???i gian vi ph???m', { text: '\r????????????', font: 'DFKaiSB' }] }
                                                , filterDate($scope.data.TimeViolation, "yyyy-MM-dd hh:mm:ss")
                                            ],

                                            ['', '', { style: 'headerTb', text: ['?????a ??i???m vi ph???m', { text: '\r????????????', font: 'DFKaiSB' }] }
                                                , $scope.data.LocationViolation
                                            ],

                                            [{ style: 'headerTb', text: ['B??? ph???n ph??t th???u', { text: '\r????????????', font: 'DFKaiSB' }] }
                                                , $scope.data.Department + '-' + $scope.data.Specification
                                                , { style: 'headerTb', text: ['H???ng m???c thi c??ng', { text: '\r????????????', font: 'DFKaiSB' }] }
                                                , $scope.data.ConstructionItems
                                            ],

                                            [{ style: 'headerTb', text: ['H???ng m???c vi ph???m', { text: '\r????????????', font: 'DFKaiSB' }] }
                                                , {
                                                margin: [-5, 0, -5, -1],
                                                table: {
                                                    widths: [290, 'auto', 'auto'],
                                                    style: 'tbCategories',
                                                    body: createtbCategories()
                                                },
                                                colSpan: 3
                                            },
                                            ],

                                            [{ style: 'headerTb', text: ['M?? t??? vi ph???m', { text: '\r????????????', font: 'DFKaiSB' }], rowSpan: 4 }
                                                , {
                                                text: $scope.data.DescriptionViolation + '\r\n\r\n'
                                                , colSpan: 3, border: [true, true, true, false]
                                            }
                                            ],

                                            [
                                                '',
                                                {
                                                    text: [{ text: '- B???ng ch???ng vi ph???m ', bold: true }, { text: '????????????: ', font: 'DFKaiSB' }
                                                        , { text: $scope.data.DisciplineCode + '_B???ng_Ch???ng_' }, { text: '??????', font: 'DFKaiSB' }, '.pdf']
                                                    , colSpan: 3, border: [true, false, true, false]
                                                }
                                            ],

                                            [
                                                '',
                                                { text: [{ text: '- H??? s?? li??n quan ', bold: true }, { text: '????????????:', font: 'DFKaiSB' }], colSpan: 3, border: [true, false, true, false] }
                                            ],

                                            [
                                                '',
                                                { ul: createRecordList(), colSpan: 3, border: [true, false, true, true] }
                                            ],

                                            [{ style: ['headerTb', 'margin16'], text: ['T???ng s??? ti???n ph???t', { text: '\r???????????????', font: 'DFKaiSB' }] }
                                                , { style: 'margin16', text: $scope.Convert($scope.data.TotalFine) + ' (VN??)' }
                                                , { style: ['headerTb', 'margin16'], text: ['B???ng ch???', { text: '\r??????', font: 'DFKaiSB' }] }
                                                , { style: 'margin16', text: [$scope.DocTienBangChu($scope.data.TotalFine), { text: '\r' + $scope.numberToChinese($scope.data.TotalFine), font: 'DFKaiSB' }] }
                                            ],

                                            [{ style: 'headerTb', text: ['????n v??? thu ti???n ph???t', { text: '\r????????????', font: 'DFKaiSB' }] }
                                                , {
                                                stack: [
                                                    {
                                                        ol: [
                                                            {
                                                                text: ['N???u b???ng ch???ng vi ph???m r?? r??ng, nh??ng ????n v??? vi ph???m (nh?? th???u) c?? th???c m???c v??? Bi??n b???n x??? ph???t th?? trong v??ng 2 ng??y, t??nh t??? ng??y k?? x??c nh???n v??o ????n x??c nh???n vi ph???m ph???i li??n h??? b??? ph???n an to??n m??i tr?????ng ????? gi???i quy???t.',
                                                                    { text: ' ??????????????????????????????????????????????????????????????????????????????????????????2????????????????????????????????????????????????????????????', font: 'DFKaiSB' }]
                                                            },

                                                            {
                                                                text: ['Sau khi nh???n ???????c th??ng b??o trong v??ng 07 ng??y Nh?? th???u vui l??ng li??n h??? v???i b??? ph???n T???ng v??? ????? ????ng to??n b??? s??? ti???n ph???t theo quy ?????nh.',
                                                                    { text: ' ????????????????????????07??????????????????????????????????????????????????????????????????', font: 'DFKaiSB' }]
                                                            },

                                                            {
                                                                text: ['Ph????ng th???c ????ng ti???n ph???t: b???ng ti???n m???t (VN??)',
                                                                    { text: ' ????????????????????????(??????) ', font: 'DFKaiSB' }]
                                                            },

                                                            {
                                                                text: ['N???u qu?? h???n nh?? th???u v???n kh??ng ????ng ph???t h??? th???ng s??? t??? ?????ng ????nh ch??? v??o c???ng v?? ng??ng x??t duy???t ????n thi c??ng m???t ng??y cho ?????n khi nh?? th???u n???p ph???t theo quy tr??nh.',
                                                                    { text: ' ?????????????????????????????????????????????????????????????????????????????? ???????????????????????????', font: 'DFKaiSB' }]
                                                            }
                                                        ]
                                                    }
                                                ], colSpan: 3, fillColor: '#CCCCCC'
                                            }
                                            ],

                                            [{ style: 'headerTb', text: ['????n v??? ph??t th???u', { text: '\r????????????', font: 'DFKaiSB' }] }
                                                , { style: 'headerTb', text: ['????n v??? l???p ????n x??? ph???t', { text: '\r????????????', font: 'DFKaiSB' }], colSpan: 3 }
                                            ],

                                            [{ style: 'headerTb', text: ['Ch??? qu???n', { text: '\r????????????', font: 'DFKaiSB' }] }
                                                , { style: 'headerTb', text: ['Ch??? qu???n x?????ng', { text: '\r????????????', font: 'DFKaiSB' }] }
                                                , { style: 'headerTb', text: ['Ch??? qu???n ????n v???', { text: '\r????????????', font: 'DFKaiSB' }] }
                                                , { style: 'headerTb', text: ['Nh??n vi??n l???p ????n', { text: '\r?????????', font: 'DFKaiSB' }] }
                                            ],

                                            [{ text: [Auth.username, { text: '\r' + Auth.nickname, font: 'DFKaiSB' }, { text: '\r(' + filterDate(new Date(), 'yyyy-MM-dd HH:mm:ss') + ')', color: 'gray', fontSize: 9 }], alignment: 'center' }
                                                , { text: [$scope.receiver[2].UserId, checkSignature($scope.receiver[2]), { text: '\r(' + $scope.receiver[2].FormatStamp + ')', color: 'gray', fontSize: 9 }], alignment: 'center' }
                                                , { text: [$scope.receiver[1].UserId, checkSignature($scope.receiver[1]), { text: '\r(' + $scope.receiver[1].FormatStamp + ')', color: 'gray', fontSize: 9 }], alignment: 'center' }
                                                , { text: [$scope.receiver[0].UserId, checkSignature($scope.receiver[0]), { text: '\r(' + $scope.receiver[0].FormatStamp + ')', color: 'gray', fontSize: 9 }], alignment: 'center' }
                                            ],

                                            [{ style: 'headerTb', text: ['TH??NG TIN X??C NH???N ????NG TI???N PH???T', { text: '\r??????????????????', font: 'DFKaiSB' }], colSpan: 4 }],

                                            [{ style: 'headerTb', text: ['Ng??y thu ti???n', { text: '\r????????????', font: 'DFKaiSB' }] }
                                                , { text: ['N??m       ', { text: '??? ', font: 'DFKaiSB' }, 'th??ng    ', { text: '??? ', font: 'DFKaiSB' }, 'Ng??y     ', { text: '???    ', font: 'DFKaiSB' }, 'Gi???', { text: '???   ', font: 'DFKaiSB' }, 'Ph??t', { text: '???', font: 'DFKaiSB' }], colSpan: 3 }
                                            ],

                                            [{ style: ['headerTb', 'margin16'], text: ['T???ng s??? ti???n ph???t', { text: '\r???????????????', font: 'DFKaiSB' }] }
                                                , { style: 'margin16', text: $scope.Convert($scope.data.TotalFine) + ' (VN??)' }
                                                , { style: ['headerTb', 'margin16'], text: ['B???ng ch???', { text: '\r??????', font: 'DFKaiSB' }] }
                                                , { style: 'margin16', text: [$scope.DocTienBangChu($scope.data.TotalFine), { text: '\r' + $scope.numberToChinese($scope.data.TotalFine), font: 'DFKaiSB' }] }
                                            ],

                                            [{ style: 'headerTb', text: ['T???ng v??? (k?? ghi r?? h??? t??n, ????ng d???u)', { text: '\r?????????(???????????????)', font: 'DFKaiSB' }], colSpan: 2 }
                                                , ''
                                                , { style: 'headerTb', text: ['?????i di???n nh?? th???u (k?? v?? ghi r?? h??? t??n)', { text: '\r??????????????????(???????????????)', font: 'DFKaiSB' }], colSpan: 2 }
                                                , ''
                                            ],

                                            [{ text: '', colSpan: 2, margin: [0, 30, 0, 30] }, '', { text: '', colSpan: 2 }, '']

                                        ]
                                    }

                                }
                            ],
                            styles: {
                                title: {
                                    bold: true,
                                    alignment: 'center',
                                },
                                headerTb:
                                {
                                    alignment: 'center',
                                    bold: true,
                                    fillColor: '#CCCCCC'
                                },
                                margin16: {
                                    margin: [0, 16, 0, 16],
                                    alignment: 'center'

                                },
                                marginCategories: {
                                    margin: [0, 16, 0, 0],
                                    alignment: 'center',
                                    border: [false, true, true, false]
                                }
                            }
                            , defaultStyle: {
                                fontSize: 12,
                                font: "TimesNewRoman",
                                lineHeight: 1.3
                            }
                        };

                        //pdfMake.createPdf(docDefinition).download("test.pdf");
                        //pdfMake.createPdf(docDefinition).open();

                        const pdfDocGenerator = pdfMake.createPdf(docDefinition);
                        pdfDocGenerator.getBase64((data) => {
                            console.log(data);
                            var params = {};
                            params.VoucherID = $scope.data.DisciplineCode;
                            params.Type = "Discipline";
                            params.Base64BinaryStr = data;
                            ConDisciplineService.SavePDF().post({}, params).$promise.then(function (res) {
                                console.log(res);
                                callback('')
                            }, function (errResponse) {
                                Notifications.addError({ 'status': 'error', 'message': errResponse });
                                callback('')
                            });
                        });
                    };

                    function createtbCategories() {
                        var body = [];
                        for (let index = 0; index < $scope.ParentPrintList.length; index++) {
                            const element = $scope.ParentPrintList[index];
                            var row = [];
                            var cell = {};
                            cell.text = [remove_linebreaks(element.Numbering + ' ' + element.ContentVN), { text: remove_linebreaks(element.ContentTW), font: "DFKaiSB" }];
                            cell.colSpan = 3;
                            cell.border = [false, false, false, true];
                            row.push(cell, '', '');
                            body.push(row);

                            if (index == $scope.ParentPrintList.length - 1) {
                                body.push([{ style: 'marginCategories', text: ['N???i dung vi ph???m', { text: '\r????????????', font: 'DFKaiSB' }], bold: true, border: [false, true, true, false] }
                                    , { style: 'marginCategories', text: ['S??? ti???n ph???t (VN??)', { text: '\r????????????', font: 'DFKaiSB' }], bold: true }
                                    , { style: 'marginCategories', text: ['S??? vi ph???m', { text: '\r????????????', font: 'DFKaiSB' }], bold: true, border: [false, true, false, false] }])
                                $scope.DisciplineList.forEach(dt => {
                                    body.push([{ text: [remove_linebreaks(dt.Numbering + ' ' + dt.ContentVN), { text: remove_linebreaks(dt.ContentTW), font: "DFKaiSB" },], border: [false, true, true, false] }, { text: $scope.Convert(dt.Fine), alignment: 'right', border: [false, true, true, false] }, { text: dt.Quantity, alignment: 'center', border: [false, true, false, false] }]);
                                });
                            }
                        }
                        return body
                    }

                    function remove_linebreaks(str) {
                        return str.replace(/[\r\n]+/gm, "");
                    }


                    function checkSignature(receiver) {
                        if (receiver.UserId.contains('NN')) {
                            return { text: '\r' + receiver.UserName, font: 'DFKaiSB' };

                        } else {
                            return { text: '\r' + receiver.UserName };
                        }
                    }

                    function createRecordList() {
                        var ul = [];
                        $scope.listfileRecords.forEach(element => {
                            ul.push(element.name);
                        });

                        return ul;
                    }

                    function filterDate(date, format) {
                        return $filter('date')(date, format);
                    }

                    $scope.exportImagePDF = function (callback) {
                        createImg(function (result) {
                            if (result == '') {
                                Notifications.addError({ 'status': 'error', 'message': errorCategory });
                                return;
                            } else {
                                pdfMake.fonts = {
                                    Roboto: {
                                        normal: 'Roboto-Regular.ttf',
                                        bold: 'Roboto-Regular.ttf',
                                        italics: 'Roboto-Regular.ttf',
                                        bolditalics: 'Roboto-Regular.ttf'
                                    },
                                    TimesNewRoman: {
                                        normal: 'Times-Regular.ttf',
                                        bold: 'Times-Bold.ttf',
                                        italics: 'Times-Italic.ttf',
                                        bolditalics: 'Times-BoldItalic.ttf'
                                    },
                                    DFKaiSB:
                                    {
                                        normal: 'DFKai-SB.ttf',
                                        bold: 'DFKai-SB.ttf',
                                        italics: 'DFKai-SB.ttf',
                                        bolditalics: 'DFKai-SB.ttf'

                                    }
                                }

                                var docDefinition = {
                                    pageSize: 'A4',
                                    pageOrientation: 'landscape',
                                    pageMargins: [20, 12, 12, 24], // left top right bottom


                                    content: result,

                                    styles: {
                                        title: {
                                            bold: true,
                                            alignment: 'center',
                                            fontSize: 16
                                        }
                                    }
                                    , defaultStyle: {
                                        fontSize: 12,
                                        font: "TimesNewRoman",
                                        lineHeight: 1.3
                                    }
                                };

                                //pdfMake.createPdf(docDefinition).download("test.pdf");
                                //pdfMake.createPdf(docDefinition).open();

                                const pdfDocGenerator = pdfMake.createPdf(docDefinition);
                                pdfDocGenerator.getBase64((data) => {
                                    console.log(data);
                                    var params = {};
                                    params.VoucherID = $scope.data.DisciplineCode;
                                    params.Type = "Evidence";
                                    params.Base64BinaryStr = data;
                                    ConDisciplineService.SavePDF().post({}, params).$promise.then(function (res) {
                                        console.log(res);
                                        callback('');
                                    }, function (errResponse) {
                                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                                        callback('')
                                    });
                                });
                            }
                        })

                    }

                    function createImg(callback) {
                        var imgArr =
                            [
                                { style: 'title', text: 'B???ng ch???ng vi ph???m' },
                                { style: 'title', text: '????????????', font: 'DFKaiSB' }
                            ];

                        for (let index = 0; index < $scope.listfileEvidence.length; index++) {
                            const element = $scope.listfileEvidence[index];
                            imgToBase64(element.name, function (imageData) {
                                if (imageData == '') {
                                    Notifications.addError({ 'status': 'error', 'message': errorCategory });
                                    return;
                                } else {
                                    var img = {}
                                    var i = new Image();
                                    i.src = imageData;
                                    i.onload = function () {
                                        if (i.width > 800)
                                            img.width = 800
                                        if (i.height > 400)
                                            img.height = 400

                                        img.margin = [0, 50, 0, 10];
                                        img.image = imageData;

                                        imgArr.push(img);
                                        if (checkCallback(imgArr.length, $scope.listfileEvidence.length + 2)) {
                                            callback(imgArr);
                                        }
                                    };
                                }
                            })


                        }
                    }

                    function checkCallback(imgArrLength, listfileEvidenceLength) {
                        if (imgArrLength == listfileEvidenceLength)
                            return true;
                        else return false;
                    }


                    function imgToBase64(_filename, callback) {
                        ConDisciplineService.ImgToBase64().get({
                            filename: _filename
                        }).$promise.then(function (res) {
                            console.log(res.file);
                            callback('data:image/png;base64,' + res.file)
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                            callback('')
                        });
                    }
                },
                templateUrl: './forms/ContractorDiscipline/Manage_Discipline/ContractorDiscipline/CreateDiscipline.html'
            }
        }
    ]);
});
