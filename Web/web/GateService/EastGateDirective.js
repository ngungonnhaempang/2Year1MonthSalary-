define(['app', 'angular', 'xlsx'], function (app, angular) {
    app.directive('viewEastGateVoucher', ['$filter', '$routeParams', 'EngineApi', 'Notifications', 'ConQuaService', '$translate', '$timeout', 'GateGuest', '$location', 'Auth', '$anchorScroll', '$upload',
        function ($filter, $routeParams, EngineApi, Notifications, ConQuaService, $translate, $timeout, GateGuest, $location, Auth, $anchorScroll, $upload) {
            return {
                restrict: 'E',

                controller: function ($scope) {
                    $scope.data = {};
                    $scope.EmployeeList = [];
                    $scope.RegistrationList = [];
                    $scope.EmployeeRegisterList = [];
                    $scope.EmployeeInDBList = [];
                    $scope.dateInvalid = false;
                    $scope.voucherID = "";

                    $scope.bpmnloaded = false;
                    $scope.IsUpdate = false;

                    

                    function getContentMail(VoucherID) {
                        return '<html><head><style> #tbReject {border-collapse: collapse; width: 100%; }' +
                            '#tbReject td, #tbReject th { border: 1px solid #ddd; padding: 8px; }' +
                            '#tbReject th { padding-top: 12px; padding-bottom: 12px;text-align: left;background-color: #F2F2F2; width: 35%;}' +
                            '.linksystem {font-weight: bold;color: darkblue;}' +
                            '.fontchina {font-family: "Times New Roman", "DFKai-SB";font-size: 15pt;}</style></head>' +
                            '<body><div class="fontchina">Chào Anh/Chị, Chức năng <b>"Đăng ký cho nhân viên công ty ra vào cổng đông”</b> xin thông báo, Anh/Chị có đơn sau cần xử lý:' +
                            '<br> 您好，<b>【員工門禁管制系統】</b> 通知您有以下文件待處理：' +
                            '<br><br></br>' +
                            '<table id="tbReject">' +
                            '<tr><th> Mã đơn / 單據號 </th><td> <b> ' + VoucherID + '</b></td></tr>' +     
                            '<tr><th> Người nộp đơn / 申請人 </th><td>' + Auth.username + ' - ' + Auth.nickname + '</td></tr>' +
                            '<tr><th> Lý do trả về / 否決原因 </th><td><b style="background-color:yellow">Thời gian đăng ký đã hết / 註冊時間已過期</b> </td></tr>' +
                            '</table>' +
                            '<br>' +
                            '<b><u>Lời nhắc | 溫馨提示:</u></b>' +
                            '<br> (1) Hệ thống có biểu đơn chờ Anh/ Chị xử lý, vui lòng vào <a href="http://10.20.46.22:843/" class="linksystem">hệ thống</a> để xác nhận.' +
                            '<br> &nbsp&nbsp&nbsp 系統有您待處理文件，請登錄<a href="http://10.20.46.22:843/" class="linksystem">系統</a>確認。' +
                            '<br> (2) Nếu Anh/Chị đã xử lý biểu đơn này, hãy bỏ qua lời nhắc này.' +
                            '<br> &nbsp&nbsp&nbsp 若您已完成本案件之處理，請忽略本提示。' +
                            '<br> (3) Nếu có bất kỳ vấn đề nào về hệ thống, vui lòng liên hệ với <b>IT - Team MES <span style="color:blue;font-style: italic;">(Ms. Hana: ntthuong@fenc.vn - ĐT: #4419 8888)</span></b> để được hỗ trợ. Xin cảm ơn!' +
                            '<br> &nbsp&nbsp&nbsp 如系統有任何問題，請聯絡 <b>IT - Team MES <span style="color:blue;font-style: italic;">(Ms. Hana: ntthuong@fenc.vn - 分機號: #4419 8888)</span></b> 處理。謝謝!' +
                            ' </div></body></html>';


                    }


                    ConQuaService.GetDepartment({ EmployeeID: Auth.username }, function (res) {
                        debugger;
                        $scope.departID = res[0].DepartmentID;
                        if (!$scope.isSecurityUser) {
                            $scope.DepartmentList = $filter("filter")($scope.DepartmentList, { DepartmentID: $scope.departID.substr(0, 6) });
                        }

                        console.log($scope.departID);

                        getEmployeeInDBByDept($scope.departID, function (errResponse) {
                            if (errResponse) {
                                Notifications.addError({ 'status': 'error', 'message': errResponse });
                            } else {
                                ConQuaService.GetEmployee({
                                    DepartmentID: $scope.departID
                                }, function (res) {
                                    if (res.length > 0) {
                                        $scope.EmployeeList = $filter("filter")(res, { State: false }, true);

                                        if ($scope.EmployeeInDBList.length > 0) {
                                            for (let index = 0; index < $scope.EmployeeInDBList.length; index++) {
                                                const element = $scope.EmployeeInDBList[index];
                                                $scope.EmployeeList = $filter("filter")($scope.EmployeeList, { EmployeeID: '!' + element.EmpID }, true);

                                                if (index == $scope.EmployeeInDBList.length - 1) {
                                                    console.log($scope.EmployeeList);
                                                    $scope.gridEmployeeList.data = $scope.EmployeeList;
                                                }
                                            }

                                        } else {
                                            console.log($scope.EmployeeList);
                                            $scope.gridEmployeeList.data = $scope.EmployeeList;
                                        }

                                        if ($routeParams.code == "ViewDetail") {
                                            debugger;
                                            $scope.detail = true;
                                            $scope.gridRegistration.columnDefs[0].visible = false;
                                            $scope.showProcessLog = true;
                                            $scope.loadVoucherDetail($routeParams.VoucherID.substr(20, 9));

                                            ConQuaService.GetEastGateInOutPID().get({
                                                VoucherID: $routeParams.VoucherID.substr(20, 9)
                                            }).$promise.then(function (res) {
                                                debugger
                                                $scope.ProcessInstanceId = res.ProcessInstanceId;
                                                EngineApi.getProcessLogs.getList({ "id": res.ProcessInstanceId, "cId": "" }, function (data) {
                                                    if (data.length === 0) {
                                                        $scope.processLogs = "";
                                                    } else {
                                                        data[0].Logs = $filter('orderBy')(data[0].Logs, 'FormatStamp');
                                                        $scope.processLogs = data[0];
                                                        console($scope.processLogs)
                                                    }
                                                })
                                            }, function (errResponse) {
                                                Notifications.addError({
                                                    'status': 'error',
                                                    'message': errResponse
                                                });
                                            });

                                        }
                                        else if ($routeParams.code == "Checker") {
                                            $scope.showProcessLog = false;
                                            $scope.loadVoucherDetail($routeParams.VoucherID);
                                            if ($scope.detail) {
                                                $scope.gridRegistration.columnDefs[0].visible = false;
                                            }
                                        }
                                    } else $scope.EmployeeList = [];
                                })
                            }
                        });

                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });

                    function getEmployeeInDBByDept(deptID, callback) {
                        ConQuaService.SearchRegisterVoucher().get({
                            VoucherID: "",
                            Name: "",
                            EmpID: "",
                            FromDate: "",
                            ToDate: "",
                            CostCenter: deptID,
                            Status: "",
                            Language: window.localStorage.lang,
                            User: Auth.username
                        }).$promise.then(function (res) {
                            debugger
                            if (res) {
                                $scope.EmployeeInDBList = $filter("filter")(res, { Status: '!X' }, true);
                                $scope.EmployeeInDBList = $filter("filter")($scope.EmployeeInDBList, { Status: '!E' }, true);
                                console.log($scope.EmployeeInDBList)
                                callback('')
                            }
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                            callback(errResponse);
                        });
                    }

                    var colEmployeeList = [
                        {
                            field: 'EmployeeID',
                            displayName: $translate.instant("EmployeeID"),
                            minWidth: 140,
                            cellTooltip: true
                        },
                        {
                            field: 'Name',
                            displayName: $translate.instant("ConName"),
                            minWidth: 200
                        },
                        {
                            field: 'CardNo',
                            displayName: $translate.instant("CardNO"),
                            width: 130,
                            minWidth: 100,
                            cellTooltip: true
                        },
                        {
                            field: 'Birthday',
                            displayName: $translate.instant("Birthday"),
                            width: 140,
                            minWidth: 100,
                            cellTooltip: true
                        },
                        {
                            field: 'PositionName',
                            displayName: $translate.instant("Job"),
                            width: 250,
                            minWidth: 100,
                            cellTooltip: true
                        },
                        {
                            field: 'Gender',
                            displayName: $translate.instant("Gender"),
                            width: 140,
                            minWidth: 100,
                            cellTooltip: true
                        },
                        {
                            field: 'DeptName',
                            displayName: $translate.instant("Department"),
                            minWidth: 600,
                            cellTooltip: true
                        }
                    ];

                    $scope.gridEmployeeList = {
                        columnDefs: colEmployeeList,
                        data: [],
                        enableFiltering: true,
                        enableColumnResizing: true,
                        enableFullRowSelection: true,
                        enableSorting: true,
                        showGridFooter: false,
                        enableGridMenu: true,
                        exporterMenuPdf: false,
                        enableSelectAll: true,
                        enableRowHeaderSelection: true,
                        enableRowSelection: true,
                        multiSelect: true,
                        paginationPageSizes: [50, 100, 200, 500],
                        paginationPageSize: 100,
                        enableFiltering: false,
                        exporterOlderExcelCompatibility: true,
                        useExternalPagination: true,
                        enablePagination: true,
                        enablePaginationControls: true,
                        showGridFooter: true,

                        onRegisterApi: function (gridApi) {
                            $scope.gridApiEmployee = gridApi;

                            // gridApi.selection.on.rowSelectionChangedBatch($scope, function (rows) {
                            //     var msg = 'rows changed ' + rows.length;
                            //     debugger;
                            //     if (checkMultipleEmployee(rows)) {

                            //     }
                            // });

                            // gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                            //     var msg = 'row selected ' + row.isSelected;
                            //     debugger;
                            //     if (checkEmployee(row.entity)) {
                            //         row.isSelected = false;
                            //     }
                            // });

                        }
                    };

                    var colRegistration = [
                        {
                            field: 'EmployeeID',
                            displayName: $translate.instant("Remove"),
                            minWidth: 70,
                            cellTemplate: `
                            <div class="ui-grid-cell-contents">
                                <a class="btn btn-danger btn-xs" style="margin-left: 20px;" role="button" ng-click="grid.appScope.deleteEmployee(row.entity.EmployeeID)">X</a>
                            </div>`

                        },
                        {
                            field: 'EmployeeID',
                            displayName: $translate.instant("EmployeeID"),
                            minWidth: 140,
                            cellTooltip: true
                        },
                        {
                            field: 'Name',
                            displayName: $translate.instant("ConName"),
                            minWidth: 200
                        },
                        {
                            field: 'CardNo',
                            displayName: $translate.instant("CardNO"),
                            width: 130,
                            minWidth: 100,
                            cellTooltip: true
                        },
                        {
                            field: 'Birthday',
                            displayName: $translate.instant("Birthday"),
                            width: 120,
                            minWidth: 100,
                            cellTooltip: true
                        },
                        {
                            field: 'PositionName',
                            displayName: $translate.instant("Job"),
                            width: 250,
                            minWidth: 100,
                            cellTooltip: true
                        },
                        {
                            field: 'Gender',
                            displayName: $translate.instant("Gender"),
                            width: 100,
                            minWidth: 100,
                            cellTooltip: true
                        },
                        {
                            field: 'DeptName',
                            displayName: $translate.instant("Department"),
                            minWidth: 600,
                            cellTooltip: true
                        }
                    ];

                    $scope.gridRegistration = {
                        columnDefs: colRegistration,
                        data: $scope.RegistrationList,
                        enableFiltering: true,
                        enableColumnResizing: true,
                        enableFullRowSelection: true,
                        enableSorting: true,
                        showGridFooter: false,
                        enableGridMenu: false,
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
                            $scope.gridApiRegister = gridApi;

                        }
                    };



                    $scope.addEmployee = function () {
                        debugger;
                        var resultRows = $scope.gridApiEmployee.selection.getSelectedRows();
                        if (resultRows.length > 0) {
                            console.log(resultRows)
                            resultRows.forEach(element => {
                                if (!checkEmployee(element)) {
                                    $scope.RegistrationList.push(element);
                                }
                            });

                            $scope.gridRegistration.data = $scope.RegistrationList;
                            $scope.gridApiEmployee.selection.clearSelectedRows();
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                        }
                    }

                    function checkMultipleEmployee(employeeList) {
                        employeeList.forEach(element => {
                            checkEmployee(element.entity)
                        });
                    }

                    function checkEmployee(employee) {
                        debugger
                        // var ExistInDB = $filter("filter")($scope.EmployeeInDBList, { EmpID: employee.EmployeeID }, true);
                        // if (ExistInDB.length > 0) {
                        //     if (ExistInDB[0].Status == 'L') {
                        //         Notifications.addError({ 'status': 'error', 'message': $translate.instant("Nhân viên:") + ' ' + employee.EmployeeID + ' - ' + employee.Name + ' đang bị KHÓA THẺ' });

                        //     } else {
                        //         Notifications.addError({ 'status': 'error', 'message': $translate.instant("Nhân viên:") + ' ' + employee.EmployeeID + ' - ' + employee.Name + ' đã được đăng ký rồi' });
                        //     }
                        //     return true
                        // } else {
                        var ExistInRegisterList = $filter("filter")($scope.RegistrationList, { EmployeeID: employee.EmployeeID }, true);
                        if (ExistInRegisterList.length > 0) {
                            //Notifications.addError({ 'status': 'error', 'message': $translate.instant("Nhân viên:") + ' ' + employee.EmployeeID + ' - ' + employee.Name + ' đã được thêm vào danh sách đăng ký rồi' });
                            return true
                        } else return false
                        //}
                    }

                    $scope.deleteEmployee = function (empID) {
                        $scope.RegistrationList = $filter("filter")($scope.RegistrationList, { EmployeeID: '!' + empID }, true);
                        $scope.gridRegistration.data = $scope.RegistrationList;
                    };

                    $scope.deleteAllEmployee = function () {
                        $scope.RegistrationList = [];
                        $scope.gridRegistration.data = [];
                    }

                    $scope.checkValidate = function () {
                        if ($scope.RegistrationList.length > 0 && !$scope.dateInvalid) {
                            return false;
                        }
                        else return true;
                    }

                    $scope.today = $filter('date')(new Date(), 'yyyy-MM-dd');
                    $scope.minEndDate = 1;
                    $scope.loadEndDate = true;

                    $scope.$watch('data.StartDate', function (date) {
                        debugger;
                        if (date) {
                            $scope.loadEndDate = false;
                            $scope.checkDate(date)
                            $scope.minEndDate = $scope.days(date); 
                            console.log("mindate: " + $scope.minEndDate)
                            $scope.loadEndDate = true;
                        }

                    })

                    $scope.days = function (date) {
                        var date2 = new Date();
                        var date1 = new Date(date);
                        var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                        $scope.dayDifference = Math.ceil(timeDiff / (1000 * 3600 * 24));
                        return $scope.dayDifference + 1;
                    }

                    $scope.$watch('data.AppointmentDate', function (date) {
                        $scope.checkDate(date)
                    })

                    $scope.checkDate = function (date) {
                        if (date && $scope.data.EndDate) {
                            if (date > $scope.data.EndDate) {
                                $("[data-toggle='popover']").popover('show');
                                $scope.dateInvalid = true;
                            } else {
                                $("[data-toggle='popover']").popover('destroy');
                                $scope.dateInvalid = false;
                            }
                        }else{
                            $("[data-toggle='popover']").popover('destroy');
                            $scope.dateInvalid = false;
                        }
                    }

                    $scope.$watch('data.EndDate', function (_endDate) {
                        if (_endDate && $scope.data.StartDate) {
                            if (_endDate < $scope.data.StartDate) {
                                $("[data-toggle='popover']").popover('show');
                                $scope.dateInvalid = true;

                            } else {
                                $("[data-toggle='popover']").popover('destroy');
                                $scope.dateInvalid = false;
                            }
                        }else{
                            $("[data-toggle='popover']").popover('destroy');
                            $scope.dateInvalid = false;
                        }
                    })

                    $scope.loadVoucherDetail = function (Voucher_id) {
                        ConQuaService.SearchRegisterVoucher().get({
                            VoucherID: Voucher_id,
                            Name: "",
                            EmpID: "",
                            FromDate: "",
                            ToDate: "",
                            CostCenter: "",
                            Status: "",
                            Language: window.localStorage.lang,
                            User: Auth.username
                        }).$promise.then(function (res) {
                            if (res) {
                                res = $filter("filter")(res, { Status: '!X' }, true);
                                if ($scope.isUpdateSubmit) {
                                    debugger;
                                    res = $filter("filter")(res, { Status: 'F' }, true);
                                    if (res[0].EndDate <= $scope.today) {
                                        $scope.expiry = true;
                                    }
                                }

                                $scope.data.VoucherID = res[0].VoucherID;
                                $scope.data.StartDate = res[0].StartDate;
                                $scope.data.EndDate = res[0].EndDate;
                                $scope.data.Creator = res[0].Creator;
                                $scope.data.CreatorName = res[0].CreatorName;
                                $scope.data.RegisterReason = res[0].RegisterReason;
                                $scope.data.CostCenter = res[0].CreatorDept;
                                $scope.data.DeptName = res[0].CreatorDeptName;
                                $scope.data.AppointmentDate = res[0].AppointmentDate;
                                $scope.data.InternalNumber = res[0].InternalNumber;

                                $scope.RegistrationList = [];

                                res.forEach(element => {
                                    var x = {};
                                    x.VoucherID = element.VoucherID;
                                    x.EmployeeID = element.EmpID;
                                    x.Name = element.Name;
                                    x.Birthday = element.Birthday;
                                    x.PositionName = element.PositionName;
                                    x.Gender = element.Gender;
                                    x.CardNo = element.CardNo;
                                    x.Status = element.Status;
                                    x.CostCenter = element.CostCenter;
                                    x.DeptName = element.DeptName;

                                    $scope.RegistrationList.push(x);

                                });
                                $scope.gridRegistration.data = $scope.RegistrationList;
                                $scope._IsUpdate = true;

                            }
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    };

                    function getRegistrationList(status, callback) {
                        $scope.EmployeeRegisterList = []
                        for (let index = 0; index < $scope.RegistrationList.length; index++) {
                            const element = $scope.RegistrationList[index];
                            var employee = {}
                            if (element.EmployeeID.contains('NN')) {
                                employee.EmployeeID = '11' + element.EmployeeID.substr(6, 4);
                            } else {
                                employee.EmployeeID = element.EmployeeID.substr(4, 6);
                            }

                            employee.VoucherID = $scope.data.VoucherID || null
                            employee.EmpID = element.EmployeeID
                            employee.CardNO = element.CardNo

                            if (status == 'ReSubmit') {
                                employee.Status = 'F'
                                employee.PreStatus = 'F'
                            } else {
                                employee.Status = 'N'
                                employee.PreStatus = 'N'
                            }

                            employee.Name = element.Name.toUpperCase();
                            employee.ModifyDate = $scope.today;
                            $scope.EmployeeRegisterList.push(employee)
                            if (index == $scope.RegistrationList.length - 1) {
                                callback($scope.EmployeeRegisterList)
                            }
                        }
                    }

                    var formVariables = [];
                    var historyVariable = [];
                    $scope.saveVoucher = function (status) {
                        debugger
                        getRegistrationList(status, function (result) {
                            var params = {}
                            params.VoucherID = $scope.data.VoucherID || null;
                            params.StartDate = $scope.data.StartDate;
                            params.EndDate = $scope.data.EndDate;
                            params.RegisterReason = $scope.data.RegisterReason;
                            if (status == 'ReSubmit') {
                                params.Status = 'F';
                            } else {
                                params.Status = 'N';
                            }

                            params.InternalNumber = $scope.data.InternalNumber;
                            params.CostCenter = $scope.data.CostCenter || $scope.departID;
                            params.Creator = Auth.username;
                            params.EmployeeList = result;
                            ConQuaService.SaveEastGateVoucher().save({}, params).$promise.then(function (res) {
                                $scope.data.VoucherID = res.VoucherID
                                if (status == 'Submit') {
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
                                            formVariables = [];
                                            historyVariable = [];
                                            debugger;
                                            formVariables.push({ name: "LeaderArray", value: leaderList });
                                            formVariables.push({ name: "VoucherID", value: $scope.data.VoucherID });
                                            formVariables.push({ name: "start_remark", value: $scope.data.VoucherID });
                                            formVariables.push({ name: "ExpiryDate", value: $scope.data.EndDate });
                                            formVariables.push({ name: "initiatorEmail", value: "ctthuy@fenc.vn" }); // Auth.email
                                            formVariables.push({ name: "BccEmail", value: "ctthuy@fenc.vn,ttnhan.it@fenc.vn" });
                                            formVariables.push({ name: "Title", value: "[10.20.46.23] THÔNG BÁO CỦA CHỨC NĂNG ĐĂNG KÝ RA VÀO CTY TẠI CỔNG ĐÔNG - 員工門禁管制系統文件通知" });
                                            formVariables.push({ name: "ContentMail", value: getContentMail($scope.data.VoucherID) });

                                            historyVariable.push({ name: "VoucherID", value: $scope.data.VoucherID });

                                            var SecurityStaffList = [];
                                            GateGuest.GetGateCheckerByKind("SecurityStaff", function (staffList, errormsg) {
                                                if (errormsg) {
                                                    Notifications.addError({ 'status': 'error', 'message': errormsg });
                                                    return;
                                                } else {
                                                    
                                                    SecurityStaffList.push(staffList);
                                                    
                                                    formVariables.push({ name: "SecurityStaffArray", value: SecurityStaffList });

                                                    $scope.getFlowDefinitionId($scope.flowkey, function (FlowDefinitionId) {
                                                        if (FlowDefinitionId) {
                                                            debugger;
                                                            $scope.startflowid(FlowDefinitionId, "", formVariables, historyVariable, function (url, message) {
                                                                if (message) {
                                                                    Notifications.addError({ 'status': 'error', 'message': message });
                                                                } else {
                                                                    var query = {}
                                                                    query.VoucherID = $scope.data.VoucherID
                                                                    query.status = 'F'
                                                                    query.AppointmentDate = ''
                                                                    ConQuaService.UpdateStatusVoucher().save(query, {}).$promise.then(function (res) {
                                                                        console.log(res);
                                                                        sendMail($scope.data.VoucherID, 'init', function (err) {
                                                                            $scope.resetEastGateModal();
                                                                            $('#EastGateModal').modal('hide');
                                                                            $("#EastGateModal").on('hidden.bs.modal', function () {
                                                                                $location.path('/taskForm/' + url);
                                                                                $scope.$apply();
                                                                            });
                                                                        })
                                                                    }, function (errResponse) {
                                                                        Notifications.addError({ 'status': 'error', 'message': errResponse });
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

                                                }
                                            })
                                        }
                                    });
                                }
                                else if (status == 'ReSubmit') {
                                    $scope.formVariables.push({ name: "update_result", value: "OK" });
                                    $scope.historyVariable.push({ name: "update_result", value: "YES" });
                                    $scope.formVariables.push({ name: "ExpiryDate", value: $scope.data.EndDate });
                                    sendMail($scope.variable.VoucherID, 'init', function (err) {
                                        $scope.submit();
                                    })
                                }
                                else {
                                    $scope.resetEastGateModal();
                                    $scope.searchRegisterVoucher();
                                    $('#EastGateModal').modal('hide');
                                    $timeout(function () {
                                        Notifications.addMessage({ 'status': 'info', 'message': $translate.instant('Save_Success_MSG') });
                                    }, 500);

                                }
                            }, function (errResponse) {
                                Notifications.addError({ 'status': 'error', 'message': errResponse });
                            });
                        })
                    }


                    $scope.changeStatus = function (_status) {
                        var query = {}
                        query.VoucherID = $scope.variable.VoucherID
                        query.status = _status
                        query.AppointmentDate = $scope.data.AppointmentDate || ''
                        ConQuaService.UpdateStatusVoucher().save(query, {}).$promise.then(function (res) {
                            console.log(res);
                            if(_status=='S'){
                                sendMail($scope.variable.VoucherID, "complete", function (err) {
                                    $scope.submit();
                                })
                            }else{
                                $scope.submit();
                            }
                            
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        })
                    }

                    $scope.saveSubmit = function () {
                        debugger;
                        $scope.formVariables.push({ name: $scope.checker + "Agree", value: $scope.data.IsAgree });
                        $scope.historyVariable.push({ name: $scope.checker + "Agree", value: $scope.data.IsAgree });

                        if ($scope.data.IsAgree === "YES") {
                            if ($scope.checker == 'SecurityStaff') {

                                $scope.formVariables.push({ name: "AppointmentDate", value: $scope.data.AppointmentDate });
                                $scope.changeStatus('S')
                               
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

                    function sendMail(_AppID, _mailkind, callback) {
                        ConQuaService.SendMailEastGate().post({
                            flowname: "EastGateInOut",
                            VoucherID: _AppID,
                            FromUser: Auth.username,
                            MailKind: _mailkind
                        }, {}).$promise.then(function (res) {
                            callback('')
                        }, function (err) {
                            callback(err)
                        });
                    }

                    $scope.resetEastGateModal = function () {
                        $scope.data = {};
                        $scope.EmployeeList = [];
                        $scope.RegistrationList = [];
                        $scope.gridRegistration.data = [];
                        $scope.gridApiEmployee.selection.clearSelectedRows();
                    }
                },
                templateUrl: './forms/InOutEastGate/new.html'
            }
        }]);

});