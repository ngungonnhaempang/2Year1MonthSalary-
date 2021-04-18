define(['myapp', 'angular'], function (myapp) {
    myapp.controller('ViolationConfirmationController', ['ConDisciplineService', 'ModificationService', '$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', '$upload', 'ConQuaService', '$translate', 'GateGuest', '$timeout',
        function (ConDisciplineService, ModificationService, $scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, $upload, ConQuaService, $translate, GateGuest, $timeout) {
            var lang = window.localStorage.lang;
            $scope.flowkey = "GateContractorQuaProcessQuery";
            $scope.query = {};
            $scope.data = {};
            $scope.listfileSigned = [];
            $scope.btnFile = false;
            $scope.data.SuspendContractor = false;
            $scope.data.DisciplineContractor = false;
            $scope.hidethis = true;
            $scope.showProcess = false;

            $timeout(function () {
                if ($routeParams.code == "Print") {
                    $scope.Print = true;
                    $scope.loadViolation($routeParams.ViolationID)
                }
            }, 1000)

            $scope.checkFileValidate = function () {
                if ($scope.listfileSigned.length > 0) {
                    return false
                } else {
                    return true
                }
            }

            $scope.checkValidate = function () {
                if ($scope.data.SuspendContractor || $scope.data.DisciplineContractor) {
                    return false
                } else {
                    return true
                }
            }

            $scope.resetModal = function () {
                $scope.data = {};
                $scope.listfileSigned = [];
                $scope.btnFile = false;
            }

            $scope.btnfile = function (id, filename) {
                $('#' + id).click();
            }
            //*** UPload file */
            $scope.UploadFileHSE = function ($files, _colName) {
                if ($scope.listfileSigned.length > 0) {
                    $timeout(function () {
                        Notifications.addError({
                            'status': 'error',
                            'message': $translate.instant('File_Number_MSG')
                        });
                    }, 300);
                    return;
                } else {
                    var isValidFile = checkFileLimited($files, _colName, 6, 3, $scope.listfileSigned);
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
    
                                if (file.includes(".doc") || file.includes(".docx") || file.includes(".pdf") ||
                                    file.includes(".jpg") || file.includes(".jpeg") || file.includes(".png")) {
    
                                    $scope.listfileSigned.push(dt);
    
                                } else {
                                    deleteFileInFolder(x);
    
                                    $timeout(function () {
                                        Notifications.addError({
                                            'status': 'error',
                                            'message': $translate.instant('FileValidation_MSG')
                                        });
                                    }, 300);
    
                                    return;
                                }
                            })
                        })
                    }
                }
            }

            function deleteFileInFolder(fileName) {
                ConDisciplineService.DeleteFile().save({
                    filename: fileName
                }, function (res) {
                    console.log('Wrong type of file');
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

            function checkFileLimited($files, colname, maximumSize, totalFile, listfile) {
                const fileCount = $files.length;
                const maximumFileSize = maximumSize * 1024 * 1024 // 3MB
                var objectResult = {
                    success: true,
                    message: "Upload Completed!"
                };
                //check file exist in list
                var listOfFiles = ((listfile.length + fileCount) > maximumSize ||
                    (listfile.filter(x => x.col === colname).length + fileCount) > maximumSize) ? true : false;
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
                if($scope.data.Status=='N'){
                    deleteFileInFolder($scope.listfileSigned[index].name)
                }
                $scope.listfileSigned.splice(index, 1);
            }

            $scope.printApp = function () {
                window.print();
            }

            $scope.printSaveApp = function () {
                debugger
                saveVoucher('N', function (error) {
                    if (error != '') {
                        Notifications.addError({ 'status': 'error', 'message': error });
                        return;
                    } else {
                        var href = '#/ContractorDiscipline/ViolationConfirmation/Print/' + $scope.data.ViolationID
                        window.open(href);
                    }
                })
            }

            ModificationService.GetDeparment().get({ language: lang }).$promise.then(function (res) {
                $scope.DepartmentList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            var col = [
                {
                    field: 'ViolationCode',
                    displayName: $translate.instant('VoucherID'),
                    width: 180,
                    minWidth: 100,
                },
                {
                    field: 'ContractorName',
                    displayName: $translate.instant('ContractorName'),
                    width: 320,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'StatusName',
                    displayName: $translate.instant('Status'),
                    width: 250,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'TimeViolation',
                    displayName: $translate.instant('TimeViolation'),
                    width: 200,
                    minWidth: 100,
                    cellTooltip: true,
                    cellFilter: "date: 'yyyy-MM-dd HH:mm:ss'"
                },
                {
                    field: 'LocationViolation',
                    displayName: $translate.instant('LocationViolation'),
                    width: 200,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'DeptName',
                    displayName: $translate.instant('ConQua_paraDepartment'),
                    width: 600,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'CreateDate',
                    displayName: $translate.instant('CreateDate'),
                    width: 130,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'Creator',
                    displayName: $translate.instant('Creator'),
                    width: 130,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'Remark',
                    displayName: $translate.instant('Remark'),
                    width: 400,
                    minWidth: 80,
                    cellTooltip: true
                }
            ];

            $scope.gridOptions = {
                columnDefs: col,
                data: [],
                enableFiltering: true,
                enableColumnResizing: true,
                enableFullRowSelection: true,
                enableSorting: true,
                showGridFooter: true,
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
                    $scope.gridApi = gridApi;
                    EngineApi.getTcodeLink().get({ 'userid': Auth.username, 'tcode': $scope.flowkey }, function (linkres) {
                        if (linkres.IsSuccess) {
                            gridApi.core.addToGridMenu(gridApi.grid, gridMenu);
                        }
                    })
                }
            };


            var gridMenu = [
                {
                    title: $translate.instant('Create'),
                    icon: 'ui-grid-icon-plus-squared',
                    action: function () {
                        $scope.resetModal();
                        $('#modalViolationConfirmation').modal('show');
                    },
                    order: 1
                },
                {
                    title: '✏️ ' + $translate.instant('Update'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            row = resultRows[0]
                            if (row.Status == 'N') {
                                if (row.Creator != Auth.username) {
                                    Notifications.addError({ 'status': 'error', 'message': $translate.instant('Update_owner_MSG') });
                                    return;
                                }
                                else {
                                    $scope.loadViolation(row.ViolationID);
                                    $('#modalViolationConfirmation').modal('show');
                                }
                            } else {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant('Edit_Draf_MSG') });
                            }
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                        }
                    },
                    order: 2
                },
                {
                    title: '🖨️ ' + $translate.instant('Print_voucher'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            var href = '#/ContractorDiscipline/ViolationConfirmation/Print/' + resultRows[0].ViolationID
                            window.open(href);
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant('Select_ONE_MSG') });
                        }
                    }, order: 3
                },
                {
                    title: '📤 ' + $translate.instant('UpdateSignedApp'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            if (resultRows[0].Status == 'X') {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': $translate.instant('PrintVoucherDelete_MSG')
                                });
                            } else {
                                $('#modalUploadFile').modal('show');
                                $scope.data = resultRows[0]
                                $scope.loadViolation(resultRows[0].ViolationID)
                            }
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                        }
                    }, order: 3
                },
                {
                    title: '❌ ' + $translate.instant('Delete'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            if (resultRows[0].Status == 'N') {
                                if (row.Creator != Auth.username) {
                                    Notifications.addError({ 'status': 'error', 'message': $translate.instant('Delete_owner_MSG') });
                                    return;
                                }
                                else {
                                    var params = {};
                                    params.VoucherID = resultRows[0].ViolationID;
                                    params.Status = 'X';
                                    ConDisciplineService.ChangeStatus_Violation().save(params, {}).$promise.then(function (res) {
                                        $scope.searchViolation();
                                        $timeout(function () {
                                            Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("Delete_Succeed_Msg") });
                                        }, 400);
                                    }, function (errResponse) {
                                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                                    });
                                }
                            } else {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant("Delete_Msg") });
                            }
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                        }
                    }, order: 4
                }];


            $scope.searchViolation = function () {
                ConDisciplineService.Search_Violation().get({
                    Language: lang,
                    VoucherID: $scope.query.ViolationID || '',
                    Con_Name: $scope.query.CotractorName || '',
                    CosCenter: $scope.query.DepartmentID || '',
                    FromDate: $scope.query.FromDate || '',
                    ToDate: $scope.query.ToDate || '',
                    UserID: ''
                }).$promise.then(function (res) {
                    $scope.gridOptions.data = res;
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            $scope.loadViolation = function (_AppID) {
                ConDisciplineService.GetViolation().get({
                    Lan: lang,
                    AppID: _AppID
                }).$promise.then(function (res) {
                    debugger
                    $scope.data = res.TableViolation[0]

                    $scope.data.TimeViolation = $filter('date')($scope.data.TimeViolation, 'yyyy-MM-dd HH:mm:ss')
                    $scope.data.PrintCategoryViolation = $scope.data.CategoryViolation;
                    $scope.data.CategoryViolation = $scope.data.CategoryViolation.split('\r\n')
                  
                    $scope.FileAttached = res.TableFile
                    $scope.FileAttached.forEach(element => {
                        var x = {};
                        x.AppID = element.AppID;
                        x.name = element.FileID;
                        x.col = element.AttachedForItem;
                        $scope.listfileSigned.push(x);
                    })
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            $scope.save = function (_status) {
                saveVoucher(_status, function (error) {
                    if (error != '') {
                        Notifications.addError({ 'status': 'error', 'message': error });
                        return;
                    } else {
                        $scope.resetModal();
                        $timeout(function () {
                            Notifications.addMessage({ 'status': 'info', 'message': $translate.instant('Save_Success_MSG') });
                        }, 500);
                        $scope.searchViolation();
                    }
                })
            }

            function saveVoucher(status, callback) {
                debugger
                var lsFile = [];

                if ($scope.listfileSigned.length > 0) {
                    $scope.listfileSigned.forEach(element => {
                        var f = {};
                        f.FileID = element.name
                        f.AppID = $scope.data.ViolationID || null
                        f.AttachedForItem = element.col;
                        lsFile.push(f);
                    })
                }
                var params = {}
                params.ViolationID = $scope.data.ViolationID || null;
                params.ViolationCode = $scope.data.ViolationCode || null;
                params.LocationViolation = $scope.data.LocationViolation;
                params.TimeViolation = $scope.data.TimeViolation;
                params.ContractorName = $scope.data.ContractorName;
                params.Status = status;
                params.DescriptionViolation = $scope.data.DescriptionViolation;
                params.Remark = $scope.data.Remark || '';
                params.Creator = Auth.username;
                params.ConstructionItems = $scope.data.ConstructionItems
                params.SuspendContractor = $scope.data.SuspendContractor
                params.DisciplineContractor = $scope.data.DisciplineContractor

                params.Department = $scope.data.Department;
                
                if ($scope.data.CategoryViolation) {
                    var category = ""
                    $scope.data.CategoryViolation.forEach(element => {
                        if (category) {
                            category = category + '\r\n' + element
                        } else category = element
                    });

                    params.CategoryViolation = category
                } else params.CategoryViolation = null


                params.Quantity = $scope.data.Quantity;
                params.files = lsFile;

                ConDisciplineService.Save_Violation().save({}, params).$promise.then(function (res) {
                    $scope.data.ViolationID = res.ViolationID
                    $('#modalViolationConfirmation').modal('hide');
                    $('#modalUploadFile').modal('hide');
                    callback('')
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                    callback(errResponse)
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

            $scope.DenounceVoucherList = [];
            ConDisciplineService.Search_Denounce().get({
                Language: lang,
                VoucherID: '',
                Con_Name: '',
                Type: '',
                Status: 'Q',
                FromDate: '',
                ToDate: '',
                UserID: ''
            }).$promise.then(function (res) {
                $scope.DenounceVoucherList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            $scope.getDenounceVoucherInfo = function (VoucherID) {
                var VoucherInfo = $filter('filter')($scope.DenounceVoucherList, { DenounceID: VoucherID }, true)
                if (VoucherInfo.length > 0) {
                    $scope.data.ContractorName = VoucherInfo[0].ContractorName;
                    $scope.data.DescriptionViolation = VoucherInfo[0].DescriptionViolation;
                    $scope.data.TimeViolation = $filter('date')(VoucherInfo[0].TimeViolation, 'yyyy-MM-dd HH:mm:ss');
                    $scope.data.LocationViolation = VoucherInfo[0].LocationViolation;
                }

            }

            ConDisciplineService.SearchCategory().get({
                Lan: lang,
                code: '',
                Status: 'A',
                Content: '',
                FromDate: '',
                ToDate: '',
                type:''
            }).$promise.then(function (res) {
                
                if (res.length > 0) {
                    $scope.ParentList = $filter("filter")(res, { Parent: null });
                } 
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });
        


        }
    ]);
})
