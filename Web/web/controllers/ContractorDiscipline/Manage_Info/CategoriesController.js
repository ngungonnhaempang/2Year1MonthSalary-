define(['myapp', 'angular'], function (myapp) {
    myapp.controller('CategoriesController', ['ConDisciplineService', '$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', '$upload', '$translatePartialLoader', '$translate', 'GateGuest', '$timeout', '$compile',
        function (ConDisciplineService, $scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, $upload, $translatePartialLoader, $translate, GateGuest, $timeout, $compile) {
            var lang = window.localStorage.lang;
            $scope.flowkey = "GateContractorEHS";
            $scope.query = {};
            $scope.data = {};
            $scope._isUpgrade = false;
            $scope.NotApplicable = false;
            $scope.showParent = false;
            $scope.showFine = false;
            $scope._isUpdate = false;
            $scope.NotSubmit = false;
            $scope.dateInvalid = false;
            $scope.duplicateNumbering = false;
            $scope.Safety = [];
            $scope.Environment = [];
            $scope.FireProtection = []
            $scope.Manager = [];

            $scope.resetCategoryModal = function () {
                $scope.data = {};
                $scope._isUpgrade = false;
                $scope.NotApplicable = false;
                $scope.showParent = false;
                $scope.showFine = false;
                $scope.LevelList = [];
                $scope._isUpdate = false;
                $scope.NotSubmit = false;
                $scope.searchCategory();
            }

            $scope.resetViewDetailModal = function () {
                $scope.data = {};
            }

            ConDisciplineService.GetDenounceChecker().get({}).$promise.then(function (checkerList) {
                if (checkerList.length > 0) {
                    $scope.Safety = [];
                    $scope.Environment = [];
                    $scope.FireProtection = []
                    $scope.Manager = [];
                    checkerList.forEach(element => {
                        if (element.Kind == 'LeaderSafe')
                            $scope.Safety.push(element)

                        if (element.Kind == 'LeaderEvr')
                            $scope.Environment.push(element)

                        if (element.Kind == 'LeaderFire')
                            $scope.FireProtection.push(element)

                        if (element.Kind == 'ManagerCategory')
                            $scope.Manager.push(element)
                    });
                } else {
                    Notifications.addError({ 'status': 'error', 'message': "Can not Get EHS Checker" });
                }
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            })

            ConDisciplineService.GetStatus().get({ type: "Info", language: lang }).$promise.then(function (res) {
                $scope.StatusList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });


            ConDisciplineService.GetCategoryType().get({ language: lang }).$promise.then(function (res) {
                $scope.TypeList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            $scope.CategoryParentList = []
            ConDisciplineService.SearchCategory().get({
                Lan: lang,
                code: '',
                Status: 'A',
                Content: '',
                FromDate: '',
                ToDate: '',
                type: ''
            }).$promise.then(function (res) {
                debugger
                $scope.CategoryParentList = $filter("filter")(res, { Parent: null }, true);
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });



            $scope.viewDetailAll = function () {
                $scope.CategoryList = []
                $scope.ParentList = []
                $scope.ChildList = []
                $scope.isViewAll = true
                ConDisciplineService.SearchCategory().get({
                    Lan: lang,
                    code: '',
                    Status: '',
                    Content: '',
                    FromDate: '',
                    ToDate: '',
                    type: ''
                }).$promise.then(function (res) {
                    if ($scope.data.Code == null) {
                        $scope.data.Code = undefined
                    }
                    debugger;
                    console.log(res);
                    $scope.CategoryList = $filter("filter")(res, { Status: '!UA' }, true);
                    if ($scope.CategoryList.length > 0) {
                        $scope.CategoryList.forEach(element => {
                            if (element.Status == "N" || element.Status == "R" || element.Status == "F" || element.Status == "E") {
                                element.NotSigned = true;
                            } else {
                                var Unapplied = $filter("filter")($scope.CategoryList, { Code: element.Code, Status: 'UAP' }, true);
                                if (Unapplied.length > 0) {
                                    var Available = $filter("filter")($scope.CategoryList, { Code: element.Code, Status: 'A' }, true);
                                    if (Available.length > 0) {
                                        if (element.Status == 'UAP') {
                                            element.Unapplied = true;
                                        } else {
                                            element.Available = true;
                                        }
                                    } else {
                                        element.IsNew = true;
                                    }
                                }
                            }
                        })
                        $scope.ParentList = $filter("filter")($scope.CategoryList, { Parent: null, Code: $scope.data.Code });
                        $scope.ChildList = $filter("filter")($scope.CategoryList, { Parent: '' });
                    }
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            $scope.Convert = function (number) {
                if (number > 0) {
                    return (number).toLocaleString('vi-VN')
                }
            }

            $scope.$watch("data.Fine", function (input_val) {
                if (input_val)
                    formatCurrency(input_val.toString())
            })

            function formatCurrency(input_val) {
                if (input_val === "") { return; }
                $scope.data.Fine = input_val.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
            }

            $scope.$watch('data.StartDate', function (date) {
                if (date) {
                    $scope.checkDate(date)
                }
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
                } else {
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
                } else {
                    $("[data-toggle='popover']").popover('destroy');
                    $scope.dateInvalid = false;
                }
            })


            var col = [
                {
                    field: 'Code',
                    width: 140,
                    displayName: $translate.instant('Category_code'),
                    minWidth: 100,
                },
                {
                    field: 'StatusName',
                    displayName: $translate.instant('Status'),
                    width: 140,
                    minWidth: 100,
                },
                {
                    field: 'Version',
                    displayName: $translate.instant('Version'),
                    width: 110,
                    minWidth: 100,
                },
                {
                    field: 'Content',
                    displayName: $translate.instant('Category_name'),
                    width: 300,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'Index',
                    displayName: $translate.instant('Position'),
                    width: 80,
                    minWidth: 110,
                },
                {
                    field: 'Level',
                    displayName: $translate.instant('Rank'),
                    width: 110,
                    minWidth: 100,
                },
                {
                    field: 'Parent',
                    displayName: $translate.instant('LargeCategory'),
                    width: 300,
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'Fine',
                    displayName: $translate.instant('Số tiền phạt'),
                    width: 220,
                    minWidth: 80,
                    cellTemplate: '<div class="ui-grid-cell-contents">{{grid.appScope.Convert(COL_FIELD)}}</div>'
                },
                {
                    field: 'StartDate',
                    displayName: $translate.instant('MD_StartDate'),
                    width: 220,
                    minWidth: 80,
                },
                {
                    field: 'EndDate',
                    displayName: $translate.instant('MD_EndDate'),
                    width: 180,
                    minWidth: 80,
                },
                {
                    field: 'Description',
                    displayName: $translate.instant('Remark'),
                    width: 220,
                    minWidth: 110,
                    cellTooltip: true
                },
                {
                    field: 'ModifyReason',
                    displayName: $translate.instant('Reason_edit'),
                    width: 220,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'UpgradeReason',
                    displayName: $translate.instant('Reason_upgrade'),
                    width: 180,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'RejectReason',
                    displayName: $translate.instant('DenyReason'),
                    width: 300,
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

            var gridMenu = [{
                title: $translate.instant('Create'),
                icon: 'ui-grid-icon-plus-squared',
                action: function () {
                    $scope.data.Version = 1;
                    $scope.getLevel();
                    $('#modalCategory').modal('show');
                },
                order: 1
            }, {
                title: '✏️ ' + $translate.instant('Update'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    if (resultRows.length == 1) {
                        if (resultRows[0].Status == 'A') {
                            $scope._isUpdate = true;
                            $scope.data = resultRows[0];
                            $scope.data.Index = parseInt(resultRows[0].Index)
                            $scope.NotApplicable = true;
                            $scope.getLevel();
                            $scope.getParentCategory();
                            $('#modalCategory').modal('show');
                        } else if (resultRows[0].Status == 'UAP') {
                            $scope._isUpdate = true;
                            $scope.data = resultRows[0];
                            $scope.data.Index = parseInt(resultRows[0].Index)
                            $scope.getLevel();
                            $scope.getParentCategory();
                            $('#modalCategory').modal('show');
                        }
                        else if (resultRows[0].Status == 'UA') {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Edit_StatusNotApply_MSG") });
                        }
                        else if (resultRows[0].Status == 'N' || resultRows[0].Status == 'R' || resultRows[0].Status == 'E') {
                            $scope.NotSubmit = true;
                            $scope.data = resultRows[0];
                            $scope.data.Index = parseInt(resultRows[0].Index)
                            $scope.getLevel();
                            $scope.getParentCategory();
                            $('#modalCategory').modal('show');
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                },
                order: 2
            }, {
                title: '📤 ' + $translate.instant('UpgradeVersion'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    if (resultRows.length == 1) {
                        if (resultRows[0].Status == 'A') {
                            $scope.checkCode(resultRows[0], function (_hasUpgrade) {
                                if (!_hasUpgrade) {
                                    $scope._isUpgrade = true;
                                    $scope.data = resultRows[0];
                                    $scope.data.Index = parseInt(resultRows[0].Index)
                                    $scope.data.Version = $scope.data.Version + 1;
                                    $scope.data.StartDate = $filter('date')(new Date(), 'yyyy-MM-dd');

                                    $scope.getLevel();
                                    $scope.getParentCategory();
                                    $('#modalCategory').modal('show');
                                }
                            });
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("UpgradeVersion_StatusApply_MSG") });
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                },
                order: 3
            },
            {
                title: '❌ ' + $translate.instant('Delete'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    var e = resultRows[0];
                    if (resultRows.length == 1) {
                        if (e.Status != 'UA' && e.Status != "A") {
                            var params = {};
                            params.ID = e.ID;
                            params.Code = e.Code;
                            params.Level = e.Level;
                            params.Status = 'X';
                            params.OriginalStatus = e.Status;
                            ConDisciplineService.ChangeStatusCategory().save(params, {}).$promise.then(function (res) {
                                console.log(res);
                                $scope.searchCategory();
                                $timeout(function () {
                                    Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("Delete_Succeed_Msg") });
                                }, 400);
                            }, function (errResponse) {
                                if (errResponse.data == "\"IsParent\"") {
                                    Notifications.addError({ 'status': 'error', 'message': $translate.instant("Delete_haveChild_MSG") });
                                } else {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                }
                            });
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("DeleteCategory_MSG") });
                        }
                    } else {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Select_ONE_MSG") });
                    }
                }, order: 4
            },
            {
                title: '📋 ' + $translate.instant('Overview'),
                action: function () {
                    $scope.isViewAll = true
                    $scope.viewDetailAll()
                    $('#Details').modal('show')

                }, order: 5
            },
            {
                title: $translate.instant('Trình ký'),
                icon: 'ui-grid-icon-on',
                action: function () {
                    $scope.viewSubmit('', '');
                    $('#SubmitViewModal').modal('show')

                }, order: 6
            }];

            $scope.$watch('data.Level', function (n) {
                debugger
                if (n != null) {
                    if (n != 0) {
                        $scope.showFine = true;
                    } else $scope.showFine = false;
                }
            });

            $scope.getLevel = function () {
                ConDisciplineService.GetCategoryLevel().get({}).$promise.then(function (res) {
                    $scope.MaxLevel = parseInt(res[0]);
                    console.log($scope.MaxLevel);
                    $scope.LevelList = [];
                    for (i = 0; i <= $scope.MaxLevel; i++) {
                        $scope.LevelList.push(i);
                    }
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            $scope.getParentCategory = function () {
                if (!$scope.data.Status) {
                    $scope.data.Parent = null;
                }
                if ($scope.data.Level > 0) {
                    $scope.showParent = true;
                    ConDisciplineService.GetCategoryParent().get({ Level: $scope.data.Level }).$promise.then(function (res) {
                        debugger;
                        $scope.ParentCategoryList = res;
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });
                } else {
                    $scope.showParent = false;
                }
            }


            $scope.searchCategory = function () {
                ConDisciplineService.SearchCategory().get({
                    Lan: lang,
                    code: $scope.query.CategoryCode || '',
                    Status: $scope.query.Status || '',
                    Content: $scope.query.CategoryContent || '',
                    FromDate: $scope.query.FromDate || '',
                    ToDate: $scope.query.ToDate || '',
                    type: ''
                }).$promise.then(function (res) {
                    if ($scope.query.CategoryClassify == 'parent') {
                        $scope.ParentList = $filter("filter")(res, { Parent: null });
                        $scope.gridOptions.data = $scope.ParentList;

                    } else if ($scope.query.CategoryClassify == 'child') {
                        $scope.ChildList = $filter("filter")(res, { Parent: '' });
                        $scope.gridOptions.data = $scope.ChildList;

                    } else {
                        $scope.gridOptions.data = res;
                    }
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            $scope.saveCategory = function (_close) {
                var params = {}
                params.Status = $scope.data.Status || null
                params.StartDate = $scope.data.StartDate
                params.EndDate = $scope.data.EndDate
                params.Version = $scope.data.Version
                params.ModifyReason = $scope.data.ModifyReason || null
                params.Index = $scope.data.Index
                params.Remark = $scope.data.Remark || null
                params.Numbering = $scope.data.Numbering
                params.ContentVN = $scope.data.ContentVN
                params.ContentTW = $scope.data.ContentTW
                params.Creator = $scope.data.Creator || Auth.username

                if ($scope.data.Level == 0) {
                    params.CategoryID = $scope.data.ID || null
                    params.CategoryCode = $scope.data.Code || null
                    params.CategoryType = $scope.data.CategoryType

                    ConDisciplineService.SaveCategory().save({}, params).$promise.then(function (res) {
                        console.log(res);
                        if (_close) {
                            $('#modalCategory').modal('hide');
                            $scope.resetCategoryModal();
                        } else {
                            $scope.data.Numbering = null;
                            $scope.data.ContentVN = null;
                            $scope.data.ContentTW = null;
                            $scope.data.Remark = null;
                            $scope.data.Index = parseInt($scope.data.Index) + 1;
                            $scope.getLevel()
                            $scope.getParentCategory()
                        }

                        $timeout(function () {
                            Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("Save_Success_MSG") });
                        }, 400);
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });

                } else {
                    params.SubCategoryID = $scope.data.ID || null
                    params.SubCategoryCode = $scope.data.Code || null
                    params.SubCategoryLevel = $scope.data.Level
                    params.CategoryCode = $scope.data.Parent
                    if ($scope.data.Fine) {
                        params.Fine = $scope.data.Fine.toString().replace(/[&\/\\#,+()$~%.'":*?<>{}!\s]/g, '')
                    } else params.Fine = ''

                    ConDisciplineService.SaveSubCategory().save({}, params).$promise.then(function (res) {
                        console.log(res);
                        if (_close) {
                            $('#modalCategory').modal('hide');
                            $scope.resetCategoryModal();
                        } else {
                            var _parentIsSelected = $scope.data.Parent
                            $scope.data.Numbering = null;
                            $scope.data.ContentVN = null;
                            $scope.data.ContentTW = null;
                            $scope.data.Remark = null;
                            $scope.data.Index = parseInt($scope.data.Index) + 1;
                            $scope.getLevel()
                            $scope.getParentCategory()
                            $scope.data.Parent = _parentIsSelected;
                        }
                        $timeout(function () {
                            Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("Save_Success_MSG") });
                        }, 400);
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });
                }
            }

            $scope.checkCode = function (data, callback) {
                var tableName = "";

                if (data.Level == 0) {
                    tableName = "CD_Category";
                } else {
                    tableName = "CD_SubCategory";
                }

                ConDisciplineService.CheckExistCode().get({
                    Table: tableName,
                    Code: data.Code,
                    Version: data.Version + 1
                }).$promise.then(function (res) {
                    console.log(res);
                    if (res.CategoryCode) {
                        Notifications.addError({ 'status': 'error', 'message': $translate.instant("Version_isUpgrade_MSG") });
                        callback(true)
                    } else {
                        callback(false)
                    }
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }


            $scope.checkDuplicateNumbering = function () {
                ConDisciplineService.CheckDuplicateNumbering().get({
                    CategoryCode: $scope.data.Code || '',
                    Numbering: $scope.data.Numbering || '',
                }).$promise.then(function (res) {
                    console.log(res);
                    if (res.CategoryCode) {
                        $scope.duplicateNumbering = true;
                    } else {
                        $scope.duplicateNumbering = false;
                    }
                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }




            $scope.getFlowDefinitionId = function (keyname, callback) {
                EngineApi.getKeyId().getkey({
                    'key': keyname
                }, function (res) {
                    callback(res.id);
                });
            }

            $scope.startflowid = function (definitionID, businessKey, formVariables, historyVariable, callback) {
                var variablesMap = Forms.variablesToMap(formVariables)
                var historyVariableMap = Forms.variablesToMap(historyVariable)
                var datafrom = {
                    formdata: variablesMap,
                    businessKey: businessKey,
                    historydata: historyVariableMap
                };
                console.log(datafrom);
                EngineApi.doStart().start({
                    'id': definitionID
                }, datafrom, function (res) {
                    console.log(res);
                    if (res.result && !res.message) {
                        callback(res.url, '');
                    } else {
                        callback('', res.message)
                    }
                }, function (errResponse) {
                    callback('', errResponse)
                })
            }


            var formVariables = [];
            var historyVariable = [];

            $scope.UpdateOrUpgradeSubmit = function () {
                debugger;
                var _categoryList = [];
                _categoryList.push($scope.data);

                var reason = $scope.view.IsAgree == "NO" ? $scope.view.DenyReason : ""

                var submitDate = $filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss');

                var isUpdate = $scope._isUpdate ? true : false

                formVariables = [];

                historyVariable = [];
                var leaderList = [];
                var managerList = [];
                leaderList.push($scope.data.LeaderID)
                managerList.push($scope.Manager[0].Person)


                formVariables.push({ name: "LeaderArray", value: leaderList });
                formVariables.push({ name: "ManagerArray", value: managerList });

                formVariables.push({ name: "SubmitDate", value: submitDate });

                if ($scope._isUpdate) {
                    formVariables.push({ name: "start_remark", value: "Chỉnh sửa - 更新" });
                    formVariables.push({ name: "S_Reason", value: $scope.data.ModifyReason });
                    formVariables.push({ name: "Flag", value: "Update" });
                } else {
                    formVariables.push({ name: "start_remark", value: "Nâng cấp - 升級" });
                    formVariables.push({ name: "S_Reason", value: $scope.data.UpgradeReason });
                    formVariables.push({ name: "Flag", value: "Upgrade" });
                    formVariables.push({ name: "S_Version", value: $scope.data.Version });
                }

                formVariables.push({ name: "S_Level", value: $scope.data.Level });
                formVariables.push({ name: "S_Index", value: $scope.data.Index });
                formVariables.push({ name: "S_Numbering", value: $scope.data.Numbering });
                formVariables.push({ name: "S_ContentVN", value: $scope.data.ContentVN });
                formVariables.push({ name: "S_ContentTW", value: $scope.data.ContentTW });
                formVariables.push({ name: "S_StartDate", value: $scope.data.StartDate });
                formVariables.push({ name: "S_EndDate", value: $scope.data.EndDate });
                formVariables.push({ name: "S_Remark", value: $scope.data.Remark });
                formVariables.push({ name: "S_Parent", value: $scope.data.Parent });
                formVariables.push({ name: "S_CategoryType", value: $scope.data.CategoryType || null });

                if ($scope.data.Fine)
                    formVariables.push({ name: "S_Fine", value: parseInt($scope.data.Fine.toString().replace(/,/g, '')) });

                historyVariable.push({ name: "SubmitDate", value: submitDate });

                $scope.getFlowDefinitionId("DisciplineCategories", function (FlowDefinitionId) {
                    if (FlowDefinitionId) {
                        $scope.startflowid(FlowDefinitionId, "", formVariables, historyVariable, function (url, message) {
                            if (message) {
                                Notifications.addError({ 'status': 'error', 'message': message });
                            } else {
                                ConDisciplineService.UpdateOrUpgradeSubmit().post({ Status: "F", UserID: Auth.username, AllRejectReason: reason, SubmitDate: submitDate, SubmitReason: '', IsUpdate: isUpdate }, {
                                    _categoryList
                                }).$promise.then(function (res) {
                                    console.log("success");
                                    console.log(res);
                                    ConDisciplineService.SendCategoryMail().post({
                                        flowname: "DisciplineCategories",
                                        SubmitDate: submitDate,
                                        FromUser: Auth.username,
                                        ToUser: $scope.data.LeaderID,
                                        Creator: Auth.username,
                                        MailKind: 'init',
                                    }, {}).$promise.then(function (res) {
                                        $('#modalCategory').modal('hide');
                                        $("#modalCategory").on('hidden.bs.modal', function () {
                                            $location.path('/taskForm/' + url);
                                            $scope.$apply();
                                        });
                                    }, function (err) {
                                        $('#modalCategory').modal('hide');
                                        $("#modalCategory").on('hidden.bs.modal', function () {
                                            $location.path('/taskForm/' + url);
                                            $scope.$apply();
                                        });
                                    });
                                }, function (errResponse) {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse })
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
        }
    ]);
})
