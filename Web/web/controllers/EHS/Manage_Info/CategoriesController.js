define(['myapp', 'angular'], function (myapp) {
    myapp.controller('CategoriesController', ['ModificationService', '$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', '$upload', '$translatePartialLoader', '$translate', 'GateGuest', '$timeout', '$compile',
        function (ModificationService, $scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, $upload, $translatePartialLoader, $translate, GateGuest, $timeout, $compile) {
            var lang = window.localStorage.lang;
            $scope.flowkey = "ModificationProcess_EHS";
            $scope.query = {};
            $scope.data = {};
            $scope._isUpgrade = false;
            $scope._DateDisabled = false;
            $scope.showParent = false;
            $scope._isUpdate = false;
            $scope.showDetailAll = "";
            var MaxLoopTimes = 0
            var LoopTimes = 0;


            $scope.resetCategoryModal = function () {
                $scope.data = {};
                $scope._isUpgrade = false;
                $scope._DateDisabled = false;
                $scope.showParent = false;
                $scope.LevelList = [];
                $scope._isUpdate = false;
                $scope.searchCategory();
            }

            $scope.resetViewDetailModal = function () {
                $scope.data = {};
                $scope.showDetailAll = "";
                $("#showDetailAll").html($scope.showDetailAll);
                $compile($("#showDetailAll").contents())($scope.$new());
            }

            ModificationService.GetStatus().get({ type: "Info", language: lang }).$promise.then(function (res) {
                $scope.StatusList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            ModificationService.GetAppicationType().get({ table: "MD_CategoryType", language: lang }).$promise.then(function (res) {
                $scope.CategoryTypeList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            $scope.showDetail = '';
            $scope.ChildList = []
            $scope.Parent = {};
            $scope.viewDetail = function (obj) {
                $scope.isViewAll = false
                $('#Details').modal('show')
                $scope.CategoryList = []
                $scope.ParentList = []
                $scope.ChildList = []
                ModificationService.SearchCategory().get({
                    Lan: lang,
                    code: '',
                    Status: 'A',
                    CategoryType: obj.entity.CategoryType,
                    Content: '',
                    FromDate: '',
                    ToDate: ''
                }).$promise.then(function (res) {
                    debugger;
                    console.log(res);

                    checkIsSelectRow(res, obj.entity.ID, function (_categoryList) {
                        $scope.CategoryList = _categoryList
                        $scope.ChildList = $filter("filter")($scope.CategoryList, { Parent: '' });
                        if (obj.entity.Parent == null) {
                            debugger
                            obj.entity.IsSelected = true;
                            $scope.Parent = obj.entity;
                            getDetail();
                        } else {
                            for (let index = 0; index < $scope.CategoryList.length; index++) {
                                const element = $scope.CategoryList[index];
                                if (element.Code == obj.entity.Parent) {
                                    FindParent(element)
                                    return;
                                }
                            }
                        }
                    })

                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            function getDetail() {
                $scope.showDetail = '';
                if ($scope.ChildList.length > 0) {
                    $scope.Level_1_List = $filter("filter")($scope.ChildList, { Parent: $scope.Parent.Code },true);

                    $scope.showDetail = $scope.showDetail + "<ul>"

                    $scope.Level_1_List.forEach(element => {
                        if (element.IsSelected) {
                            $scope.showDetail = $scope.showDetail + "<li><p class='isSelected'}>" + element.Content + "</p>"
                        } else {
                            $scope.showDetail = $scope.showDetail + "<li><p>" + element.Content + "</p>"
                        }
                        FindChild(element.Code)
                    });
                }
                $scope.showDetail = $scope.showDetail + '</li></ul>';
            }

            function FindParent(_category) {
                if (_category.Parent == null) {
                    $scope.Parent = _category
                    getDetail()
                } else {
                    var findParent = $filter("filter")($scope.CategoryList, { Code: _category.Parent },true);
                    if (findParent.length > 0) {
                        FindParent(findParent[0])
                    }
                }
            }

            function FindChild(_parentCode) {
                var findChild = $filter("filter")($scope.CategoryList, { Parent: _parentCode },true);
                if (findChild.length > 0) {
                    var HasChild = [];
                    $scope.showDetail = $scope.showDetail + "<ul>"
                    findChild.forEach(element => {
                        if (element.IsSelected) {
                            $scope.showDetail = $scope.showDetail + "<li><p class='isSelected'}>" + element.Content + "</p>"
                        } else {
                            $scope.showDetail = $scope.showDetail + "<li><p>" + element.Content + "</p>"
                        }
                        HasChild = $filter("filter")($scope.ChildList, { Parent: element.Code }, true);
                        if (HasChild.length > 0) {
                            FindChild(element.Code)
                        }
                    });
                    $scope.showDetail = $scope.showDetail + '</li></ul>';

                }
            }

            function checkIsSelectRow(_categoryList, _id, callback) {
                for (let index = 0; index < _categoryList.length; index++) {
                    const element = _categoryList[index];
                    if (element.ID == _id) {
                        element.IsSelected = true;
                    } else element.IsSelected = false;
                    element.Index = parseInt(element.Index)
                    if (index == _categoryList.length - 1) {
                        callback(_categoryList)
                    }
                }
            }

            $scope.viewDetailAll = function () {
                $scope.CategoryList = []
                $scope.ParentList = []
                $scope.ChildList = []
                ModificationService.SearchCategory().get({
                    Lan: lang,
                    code: '',
                    Status: status,
                    CategoryType: $scope.data.CategoryType,
                    Content: '',
                    FromDate: '',
                    ToDate: ''
                }).$promise.then(function (res) {
                    console.log(res);
                    $scope.CategoryList = $filter("filter")(res, { Status: '!UA' }, true);
                    if ($scope.CategoryList.length > 0) {
                        $scope.CategoryList.forEach(element => {
                            // Check if this category has unapplied category
                            var Unapplied = $filter("filter")($scope.CategoryList, { Code: element.Code, Status: 'UAP' }, true);
                            debugger
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

                        });

                        console.log($scope.CategoryList);
                        $scope.ParentList = $filter("filter")($scope.CategoryList, { Parent: null });
                        $scope.ChildList = $filter("filter")($scope.CategoryList, { Parent: '' });
                        debugger;
                        $scope.showDetailAll = '<ul><li ng-repeat="p in CategoryList | filter: {\'Parent\':null}:true">'
                            + '<span ng-if="!p.IsNew" ng-style="{\'background-color\':(p.Status==\'A\' || p.IsNew) ? \'white\':\'yellow\','
                            + ' \'color\':(p.Status==\'A\'&& p.Available) ? \'red\':\'black\'}">'
                            + '{{p.Content}}</span>'

                            + '<b ng-if="p.IsNew" style="color:blue">{{p.Content}}</b>'

                        MaxLoopTimes = 0

                        if ($scope.ParentList.length > 0) {
                            $scope.ParentList.forEach(element => {
                                if (!element.Unapplied) {
                                    if (MaxLoopTimes < LoopTimes) {
                                        MaxLoopTimes = LoopTimes
                                    }
                                    LoopTimes = 0
                                    CountChildren(element.Code, 1)
                                }
                            });
                        }

                        generateChildrenHtml(MaxLoopTimes);
						
                    }

                }, function (errResponse) {
                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                });
            }

            function CountChildren(_parentCode, _countLoop) {
                var findChild = $filter("filter")($scope.ChildList, { Parent: _parentCode },true);
                if (findChild.length > 0) {
                    if (LoopTimes < _countLoop) {
                        LoopTimes = _countLoop
                    }
                    var HasChild = [];
                    findChild.forEach(element => {
                        HasChild = $filter("filter")($scope.ChildList, { Parent: element.Code },true);
                        if (HasChild.length > 0) {
                            CountChildren(element.Code, _countLoop + 1)
                        } else {
                            _countLoop = 1
                        }
                    });
                }
            }

            function generateChildrenHtml(total) {
				if(total != 0){
					for (let index = 1; index <= total; index++) {
                    var parentName = '';
                    var childName = 'level' + index;
                    if (index == 1) {
                        parentName = 'p'
                    } else {
                        var i = index - 1
                        parentName = 'level' + i
                    }

                    $scope.showDetailAll = $scope.showDetailAll
                        + '<ul ng-if="!' + parentName + '.Unapplied"><li ng-repeat="' + childName + ' in CategoryList | filter: {\'Parent\':' + parentName + '.Code}:true">'
                        + '<span ng-if="!' + childName + '.IsNew" ng-style="{\'background-color\':(' + childName + '.Status==\'A\') ? \'white\':\'yellow\', \'color\':(' + childName + '.Status==\'A\'&& ' + childName + '.Available) ? \'red\':\'black\'}"> {{' + childName + '.Content}}</span>'
                        + '<b ng-if="' + childName + '.IsNew" style="color:blue"> {{' + childName + '.Content}}</b>'

                    if (index == total) {
                        for (let index = 1; index <= total; index++) {
                            $scope.showDetailAll = $scope.showDetailAll + '</li></ul>'
                        }

                        $("#showDetailAll").html($scope.showDetailAll);

                        $compile($("#showDetailAll").contents())($scope.$new());
                    }
                }
					
				}else
				{
					$scope.showDetailAll = $scope.showDetailAll + '</li></ul>'

					$("#showDetailAll").html($scope.showDetailAll);

					$compile($("#showDetailAll").contents())($scope.$new());
				}				
				
                
            }


            var col = [
                {
                    field: 'Code',
                    width: 140,
                    displayName: $translate.instant('Category_code'),
                    minWidth: 100,
                    cellTemplate: "<a ng-click='grid.appScope.viewDetail(row)' style='padding:5px;display:block; cursor:pointer'>{{COL_FIELD}}</a>",
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
                    field: 'CategoryTypeName',
                    displayName: $translate.instant('Category_type'),
                    width: 220,
                    minWidth: 80,
                },
                {
                    field: 'Parent',
                    displayName: $translate.instant('Category_Parent'),
                    width: 300,
                    minWidth: 100,
                    cellTooltip: true
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
                    $('#modalCategory').modal('show');
                },
                order: 1
            }, {
                title: '✏️ ' + $translate.instant('Update'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    if (resultRows.length == 1) {
                        if (resultRows[0].Status == 'A') {
                            $scope.checkCode(resultRows[0], function (_hasUpdate) {
                                if (!_hasUpdate) {
                                    $scope._isUpdate = true;
                                    $scope.data = resultRows[0];
                                    $scope.data.Index = parseInt(resultRows[0].Index)
                                    $scope._DateDisabled = true;
                                    $scope.getLevel();
                                    $scope.getParentCategory();
                                    $('#modalCategory').modal('show');
                                }
                            });
                        } if (resultRows[0].Status == 'UAP') {
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
                        if (resultRows[0].Status == 'UAP') {
                            var params = {};
                            params.ID = resultRows[0].ID;
                            params.Code = resultRows[0].Code;
                            params.Level = resultRows[0].Level;
                            params.Status = 'X';
                            ModificationService.ChangeStatusCategory().save(params, {}).$promise.then(function (res) {
                                console.log(res);
                                $scope.searchCategory();
                                $timeout(function () {
                                    Notifications.addMessage({ 'status': 'info', 'message': $translate.instant("Delete_Succeed_Msg") });
                                }, 400);
                            }, function (errResponse) {
                                if (errResponse.data == "\"IsParent\"") {
                                    Notifications.addError({ 'status': 'error', 'message': "Delete_haveChild_MSG" });
                                } else {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                }
                            });
                        } else {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant("Delete_NotApply_Msg") });
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
                    $('#Details').modal('show')

                }, order: 5
            }];

            $scope.getLevel = function () {
                ModificationService.GetCategoryLevel().get({ CategoryType: $scope.data.CategoryType }).$promise.then(function (res) {
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
                debugger;
                if (!$scope._isUpdate && !$scope._isUpgrade) {
                    $scope.data.Parent = null;
                }
                if ($scope.data.Level > 0) {
                    $scope.showParent = true;
                    ModificationService.GetCategoryParent().get({ CategoryType: $scope.data.CategoryType, Level: $scope.data.Level }).$promise.then(function (res) {
                        $scope.ParentCategoryList = res;
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });
                } else {
                    $scope.showParent = false;
                }
            }


            $scope.searchCategory = function () {
                ModificationService.SearchCategory().get({
                    Lan: lang,
                    code: $scope.query.CategoryCode || '',
                    Status: $scope.query.Status || '',
                    CategoryType: $scope.query.CategoryType || '',
                    Content: $scope.query.CategoryContent || '',
                    FromDate: $scope.query.FromDate || '',
                    ToDate: $scope.query.ToDate || ''
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
                if ($scope._isUpgrade) {
                    params.Status = "Upgrade"
                    params.UpgradeReason = $scope.data.UpgradeReason
                } else params.Status = $scope.data.Status || null
                params.StartDate = $scope.data.StartDate
                params.EndDate = $scope.data.EndDate
                params.Version = $scope.data.Version
                params.ModifyReason = $scope.data.ModifyReason || null
                params.Index = $scope.data.Index
                params.Remark = $scope.data.Description || null

                if ($scope.data.Level == 0) {
                    params.CategoryID = $scope.data.ID || null
                    params.CategoryType = $scope.data.CategoryType
                    params.CategoryContent = $scope.data.Content
                    params.CategoryCode = $scope.data.Code || null

                    ModificationService.SaveCategory().save({}, params).$promise.then(function (res) {
                        console.log(res);
                        if (_close) {
                            $('#modalCategory').modal('hide');
                            $scope.resetCategoryModal();
                        } else {
                            $scope.data.Content = null;
                            $scope.data.Description = null;
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
                    params.SubCategoryContent = $scope.data.Content
                    params.SubCategoryLevel = $scope.data.Level
                    params.CategoryCode = $scope.data.Parent

                    ModificationService.SaveSubCategory().save({ CategoryType: $scope.data.CategoryType }, params).$promise.then(function (res) {
                        console.log(res);
                        if (_close) {
                            $('#modalCategory').modal('hide');
                            $scope.resetCategoryModal();
                        } else {
                            var _parentIsSelected = $scope.data.Parent
                            $scope.data.Content = null;
                            $scope.data.Description = null;
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
                    tableName = "MD_Category";
                } else {
                    tableName = "MD_SubCategory";
                }

                ModificationService.CheckExistCode().get({
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

            
        }
    ]);
})
