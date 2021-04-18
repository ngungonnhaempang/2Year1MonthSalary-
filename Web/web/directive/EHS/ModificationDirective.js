
define(['app', 'angular', 'jquery'], function (app, angular, jquery) {
    app.directive('viewModification', ['ModificationService', '$compile', '$upload', '$resource', '$http', '$filter', 'Notifications', '$routeParams', '$translate', '$timeout', 'GateGuest', 'EngineApi', '$location', 'Auth', 'Forms', '$anchorScroll', '$route', '$window',
        function (ModificationService, $compile, $upload, $resource, $http, $filter, Notifications, $routeParams, $translate, $timeout, GateGuest, EngineApi, $location, Auth, Forms, $anchorScroll, $route, $window) {
            return {
                restrict: 'E',
                controller: function ($scope) {
                    var lang = window.localStorage.lang;
                    $scope.showCategoryDetail = "";
                    $scope.showCategoryPrint = "";
                    $scope.fileArea = "";
                    $scope.fileDocument = "";
                    $scope.fileRecord = "";
                    $scope.data = {};
                    $scope.file = [];
                    $scope.AllFile = []
                    $scope.FileList = []
                    $scope.OtherList = []
                    $scope.ChildList = []
                    $scope.ParentList = []
                    $scope.removeFileList = []
                    $scope.CategoryList = []
                    $scope.AffectRangeList = []
                    $scope.CheckCategoryList = []
                    $scope.CheckAffectRangeList = []
                    $scope.hseManagerList = []

                    var MaxLoopTimes = 0
                    var LoopTimes = 0;
                    $scope.data.category = false
                    $scope.data.range = false
                    $scope.rootCateoryHalfCheck = false;
                    $scope.rootRangeHalfCheck = false;
                    $scope.Print = false;

                    $scope.printApp = function () {
                        window.print();
                    }

                    $scope.goBack = function () {
                        window.history.back();
                    }

                    if ($routeParams.code == "Print") {
                        $scope.Print = true;
                    }

                    $scope.checkValidate = function () {
                        if ($scope.fileArea == "" || $scope.fileDocument == "" || $scope.fileRecord == "") {
                            return true;
                        }
                        else {
                            var IsCheckCategory = $filter("filter")($scope.CategoryList, { checked: true }, true);
                            var IsCheckRange = $filter("filter")($scope.AffectRangeList, { checked: true }, true);
                            if (IsCheckCategory.length == 0 || IsCheckRange.length == 0) {
                                return true;
                            } else {
                                return false
                            }
                        }
                    }

                    ModificationService.GetDeparment().get({ language: lang }).$promise.then(function (res) {
                        $scope.DepartmentList = res;

                        if ($routeParams.code != "Create") {
                            $scope.IsUpdate = true
                            loadModificationApp($routeParams.MD_AppID)

                        } else {
                            $scope.IsUpdate = false;
                            var status = 'A'
                            var date = ''
                            showRange(status, date, function (errorRange) {
                                if (errorRange != '') {
                                    Notifications.addError({ 'status': 'error', 'message': errorRange });
                                    return;
                                } else {

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
                                                    $scope.getProjectID($scope.data.CostCenter,$scope.data.CreateDate)
                                                    getISOCode()
                                                   
                                                }
                                            })
                                        }
                                    })
                                }
                            })

                        }
                    }, function (errResponse) {
                        Notifications.addError({ 'status': 'error', 'message': errResponse });
                    });

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

                    function loadModificationApp(_AppID) {
                        ModificationService.GetModificationAppDetail().get({
                            MD_AppID: _AppID,
                        }).$promise.then(function (res) {
                            console.log(res);
                            var md = res.TableMD[0]
                            GetDeparmentCreatorInfo(md.Creator, function (errResponse) {
                                if (errResponse != '') {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                } else {
                                    if ($routeParams.code == 'Update') {
                                        $scope.getProjectID($scope.data.CostCenter,md.CreateDate)
                                    }

                                    var status = 'A'
                                    var date = ''


                                    $scope.data.CreateDate = md.CreateDate
                                    status = ''
                                    date = md.ModifyDate


                                    if (md.Status == 'Q' || $scope.checker == 'hseDivisionManager' || $scope.checker == 'hseManager' || $scope.checker == 'Vice') {
                                        $scope.GetInformation($routeParams.MD_AppID, md.Status)
                                    }

                                    $scope.data.MD_AppID = md.MD_AppID
                                    $scope.data.MD_ProjectID = md.MD_ProjectID
                                    $scope.data.MD_Name = md.MD_Name

                                    $scope.data.MD_Type = md.MD_Type
                                    if (md.MD_Type == "longTerm")
                                        $scope.longTerm = true
                                    else $scope.shortTerm = true

                                    $scope.data.ExpectDateBegin = md.ExpectDateBegin
                                    $scope.data.ExpectDateEnd = md.ExpectDateEnd

                                    $scope.data.RiskLevel = md.RiskLevel
                                    if ($scope.data.RiskLevel == 'Level_2') {
                                        $scope.RiskLevel_2 = true;
                                        $scope.data.AffectDivision = md.AffectDivision.split('|')
                                        $scope.AffectDivisionList = getAffectDivisionList($scope.data.AffectDivision)
                                    } else {
                                        $scope.RiskLevel_1 = true;
                                        $scope.data.AffectDivision = []
                                    }

                                    $scope.data.Reason = md.Reason
                                    $scope.data.Area = md.Area
                                    $scope.data.Description = md.Description

                                    $scope.data.ConstructionName = md.ConstructionName
                                    if (md.ConstructionName) {
                                        $scope.data.ConstructionCheck = true;
                                    }

                                    $scope.data.CapitalCode = md.CapitalCode
                                    if (md.CapitalCode) {
                                        $scope.data.CapitalCodeCheck = true;
                                    }

                                    $scope.data.ProjectID = md.ProjectID
                                    if (md.ProjectID) {
                                        $scope.data.ProjectIDCheck = true;
                                    }

                                    $scope.data.Status = md.Status
                                    $scope.data.ISO_ID = md.ISO_ID
                                    $scope.data.ISO_AppCode = md.ISO_AppCode
                                    $scope.data.Creator = md.Creator

                                    $scope.FileList = res.TableFile
                                    $scope.FileList.forEach(element => {
                                        if (element.AttachedForItem == "MD_Area") {
                                            $scope.fileArea = element.FileID;
                                        } else if (element.AttachedForItem == "MD_Document") {
                                            $scope.fileDocument = element.FileID;
                                        } else if (element.AttachedForItem == "MD_Record") {
                                            $scope.fileRecord = element.FileID;
                                        }
                                    });

                                    $scope.CheckCategoryList = res.TableCategory
                                    $scope.CheckAffectRangeList = res.TableRange
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

                                    showRange(status, date, function (errorRange) {
                                        if (errorRange != '') {
                                            Notifications.addError({ 'status': 'error', 'message': errorRange });
                                            return;
                                        } else {
                                            showCategories(status, date, function (errorCategory) {
                                                if (errorCategory != '') {
                                                    Notifications.addError({ 'status': 'error', 'message': errorCategory });
                                                    return;
                                                } else {
                                                    $scope.AffectRangeList.forEach(element => {
                                                        var _range = $filter("filter")($scope.CheckAffectRangeList, { AffectRangeID: element.AffectRangeID }, true);
                                                        if (_range.length > 0) {
                                                            element.checked = true
                                                            changeRootRange_CheckBoxType()
                                                            var different = '其他khác'
                                                            if (element.AffectRangeContent.replace(/[&\/\\#,+()$~%.'":*?<>{}!\s]/g, '').toLowerCase() == different) {
                                                                _other = $filter("filter")($scope.OtherList, { ItemID: element.AffectRangeID }, true)
                                                                element.OtherContent = _other[0].OtherContent
                                                            }
                                                        }
                                                    });

                                                    $scope.CategoryList.forEach(element => {
                                                        var _category = $filter("filter")($scope.CheckCategoryList, { CategoryID: element.ID }, true);
                                                        if (_category.length > 0) {
                                                            element.checked = true
                                                            if (element.Parent != null) {
                                                                tickParent(element)
                                                            }
                                                            changeRootCategory_CheckBoxType()
                                                            if (isDifferent(element)) {
                                                                _other = $filter("filter")($scope.OtherList, { ItemID: element.ID }, true)
                                                                element.OtherContent = _other[0].OtherContent
                                                            }
                                                        }
                                                    });
                                                }
                                            })
                                        }

                                    })
                                }
                            })
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }

                    function getAffectDivisionList(divisionIDList) {
                        divisionIDList.forEach(element => {
                            var Dept = $filter("filter")($scope.DepartmentList, { DepartmentID: element }, true)
                            if (Dept.length > 0) {
                                var DeptName = Dept[0].Specification_TW + ' / ' + Dept[0].Specification_VN
                                if ($scope.AffectDivisionList) {
                                    $scope.AffectDivisionList = $scope.AffectDivisionList + ', ' + DeptName
                                } else {
                                    $scope.AffectDivisionList = DeptName

                                }
                            }
                        });
                        return $scope.AffectDivisionList
                    }

                    $scope.getProjectID = function (deptID, proposedDate) {
                        ModificationService.CreateProjectID().get({
                            DepartmentID: deptID,
                            proposedDate: proposedDate
                        }).$promise.then(function (res) {
                            $scope.PreMDProjectID = res[0].MD_ProjectID.substr(0, 12)
                            $scope.NumberMDProjectID = res[0].MD_ProjectID.substr(13, 3)
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }

                    function getISOCode() {
                        ModificationService.SearchISO().get({
                            Language: lang,
                            code: '',
                            Status: 'A',
                            ApplicationType: '5VEXXHR041',
                            FromDate: '',
                            ToDate: ''
                        }).$promise.then(function (res) {
                            $scope.data.ISO_AppCode = res[0].ISO_AppCode;
                            $scope.data.ISO_ID = res[0].ISO_ID;
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        });
                    }

                    /** CATEGORIES **/
                    function showCategories(status, date, callback) {
                        $scope.CategoryList = []
                        $scope.ParentList = []
                        $scope.ChildList = []
                        ModificationService.SearchCategory().get({
                            Lan: lang,
                            code: '',
                            Status: status,
                            CategoryType: 'MD',
                            Content: '',
                            FromDate: date,
                            ToDate: date
                        }).$promise.then(function (res) {
                            console.log(res);
                            $scope.CategoryList = res
                            if ($scope.CategoryList.length > 0) {
                                $scope.CategoryList.forEach(element => {
                                    element.checked = false;
                                    element.isHalfCheck = false;
                                    if (isDifferent(element)) {
                                        element.Other = true;
                                    } else {
                                        element.Other = false;
                                        element.OtherContent = null;
                                    }

                                    element.Expanded = false;
                                    element.showChild = false;

                                    // Check if this category has children
                                    var HasChild = $filter("filter")($scope.CategoryList, { Parent: element.Code }, true);
                                    if (HasChild.length > 0) {
                                        element.HasChild = true;
                                        element.Collapsed = true;
                                    } else {
                                        element.HasChild = false;
                                        element.Collapsed = false;
                                    }
                                });

                                console.log($scope.CategoryList);
                                $scope.ParentList = $filter("filter")($scope.CategoryList, { Parent: null });
                                $scope.ChildList = $filter("filter")($scope.CategoryList, { Parent: '' });
                                $scope.showCategoryDetail = '<ul><li ng-repeat="p in CategoryList | filter: {\'Parent\':null}:true"><label ng-class="{arrow:true, \'collapsed\': p.Collapsed, \'expanded\':p.Expanded}"><button class="fold" ng-if="p.HasChild" ng-click="openNode(p)"></button>' +
                                    '</label><label class="checkboxPseudo fitCheckPosition"><input type="checkbox" ng-model="p.checked" ng-change="checkCategory(p)"><span ng-class="{\'checked\':true, \'halfCheck\':p.isHalfCheck}"></span>{{p.Content}}</label>' +
                                    '<div ng-if="p.Other && p.checked"> <textarea type="text" class="form-control other" rows="3" ng-model="p.OtherContent" required></textarea></div>'

                                $scope.showCategoryPrint = '<ul><li ng-repeat="p in CategoryList | filter: {\'Parent\':null}:true">' +
                                    '<label class="checkboxPseudo fitCheckPosition"><input type="checkbox" ng-model="p.checked" disabled><span class="checked"></span>{{p.Content}}' +
                                    '<br><u style="white-space: break-spaces;" ng-if="p.OtherContent">{{p.OtherContent}}</u></label>'

                                MaxLoopTimes = 0

                                if ($scope.ParentList.length > 0) {
                                    $scope.ParentList.forEach(element => {
                                        if (MaxLoopTimes < LoopTimes) {
                                            MaxLoopTimes = LoopTimes
                                        }
                                        LoopTimes = 0
                                        CountChildren(element.Code, 1)
                                    });
                                }

                                generateChildrenHtml(MaxLoopTimes);
                                callback('')
                            }
                            else {
                                callback('')
                            }
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                            callback(errResponse)
                        });
                    }

                    function CountChildren(_parentCode, _countLoop) {
                        var findChild = $filter("filter")($scope.ChildList, { Parent: _parentCode });
                        if (findChild.length > 0) {
                            if (LoopTimes < _countLoop) {
                                LoopTimes = _countLoop
                            }
                            var HasChild = [];
                            findChild.forEach(element => {
                                HasChild = $filter("filter")($scope.ChildList, { Parent: element.Code });
                                if (HasChild.length > 0) {
                                    CountChildren(element.Code, _countLoop + 1)
                                } else {
                                    _countLoop = 1
                                }
                            });
                        }
                    }

                    function generateChildrenHtml(total) {
                        for (let index = 1; index <= total; index++) {
                            var parentName = '';
                            var childName = 'level' + index;
                            if (index == 1) {
                                parentName = 'p'
                            } else {
                                var i = index - 1
                                parentName = 'level' + i
                            }

                            $scope.showCategoryDetail = $scope.showCategoryDetail
                                + '<ul ng-show="' + parentName + '.showChild"><li ng-repeat="' + childName + ' in CategoryList | filter: {\'Parent\':' + parentName + '.Code}:true">'
                                + '<label ng-class="{arrow:true, \'collapsed\': ' + childName + '.Collapsed==true, \'expanded\': ' + childName + '.Expanded==true}"><button class="fold"  ng-if="' + childName + '.HasChild" ng-click="openNode(' + childName + ')"></button></label>'
                                + '<label class="checkboxPseudo fitCheckPosition"><input type="checkbox" ng-model="' + childName + '.checked" ng-change="checkCategory(' + childName + ')"> <span ng-class="{\'checked\':true, \'halfCheck\':' + childName + '.isHalfCheck}"></span> {{' + childName + '.Content}} </label>'
                                + '<div ng-if="' + childName + '.Other && ' + childName + '.checked"><textarea type="text" class="form-control other" rows="3" ng-model="' + childName + '.OtherContent" required></textarea></div>'

                            $scope.showCategoryPrint = $scope.showCategoryPrint
                                + '<ul><li ng-repeat="' + childName + ' in CategoryList | filter: {\'Parent\':' + parentName + '.Code}:true">'
                                + '<label class="checkboxPseudo fitCheckPosition"><input type="checkbox" ng-model="' + childName + '.checked" disabled> <span class="checked"></span> {{' + childName + '.Content}}'
                                + '<br><u style="white-space: break-spaces;" ng-if="' + childName + '.OtherContent">{{' + childName + '.OtherContent}}</u></label>'


                            if (index == total) {
                                for (let index = 1; index <= total; index++) {
                                    $scope.showCategoryDetail = $scope.showCategoryDetail + '</li></ul>'
                                    $scope.showCategoryPrint = $scope.showCategoryPrint + '</li></ul>'
                                }

                                $("#showCategory").html($scope.showCategoryDetail);

                                $compile($("#showCategory").contents())($scope.$new());


                                $("#showPrint").html($scope.showCategoryPrint);

                                $compile($("#showPrint").contents())($scope.$new());
                            }
                        }
                    }

                    $scope.openNode = function (node) {
                        if (node.Collapsed) {
                            console.log("open")
                            node.showChild = true;
                            node.Collapsed = false;
                            node.Expanded = true;
                        } else {
                            console.log("close")
                            node.showChild = false;
                            node.Collapsed = true;
                            node.Expanded = false;
                        }

                    }

                    function isDifferent(obj) {
                        var different = '其他khác'
                        if (obj.Content.replace(/[&\/\\#,+()$~%.'":*?<>{}!\s]/g, '').toLowerCase() == different) {
                            return true
                        } else return false
                    }

                    $scope.checkAllCategory = function (isChecked) {
                        $scope.rootCateoryHalfCheck = false
                        if (isChecked) {
                            $scope.CategoryList.forEach(element => {
                                if (isDifferent(element)) {
                                    element.checked = false
                                } else {
                                    element.checked = true
                                    if (element.HasChild) {
                                        element.isHalfCheck = false
                                    }
                                }
                            });
                        } else {
                            $scope.CategoryList.forEach(element => {
                                element.checked = false
                                if (element.HasChild) {
                                    element.isHalfCheck = false
                                }
                            });
                        }
                    }

                    $scope.checkCategory = function (_category) {
                        if (_category.HasChild) {
                            tickChild(_category)
                        } else {
                            if (isDifferent(_category)) {
                                _category.OtherContent = null
                            }
                        }

                        if (_category.Parent != null) {
                            tickParent(_category)
                        }
                        changeRootCategory_CheckBoxType()
                    }

                    function tickChild(_category) {
                        _category.isHalfCheck = false
                        if (_category.checked) {
                            $scope.ChildList.forEach(element => {
                                if (element.Parent == _category.Code && !isDifferent(element)) {
                                    element.checked = true
                                    element.isHalfCheck = false
                                    if (element.HasChild) {
                                        tickChild(element)
                                    }
                                }
                            });
                        } else {
                            $scope.ChildList.forEach(element => {
                                if (element.Parent == _category.Code) {
                                    element.checked = false
                                    element.OtherContent = null

                                    if (element.HasChild) {
                                        tickChild(element)
                                    } else {
                                        tickParent(element)
                                    }
                                }
                            });
                        }
                        // changeRootCategory_CheckBoxType()
                    }

                    function tickParent(_category) {
                        var Has_Sibling = $filter("filter")($scope.CategoryList, { Parent: _category.Parent }, true);
                        if (Has_Sibling.length > 1) {
                            var count = 0;
                            Has_Sibling.forEach(element => {
                                if (element.ID != _category.ID) {
                                    if (element.checked) {
                                        count++;
                                    }
                                }
                            });

                            if (count == 0 && !_category.checked) {
                                var isParent = $filter("filter")($scope.CategoryList, { Code: _category.Parent }, true);
                                isParent[0].checked = false;
                                if (isParent[0].Parent != null) {
                                    tickParent(isParent[0])
                                }
                            }
                            else if (count == Has_Sibling.length - 1) {
                                if (_category.checked) {
                                    var isParent = $filter("filter")($scope.CategoryList, { Code: _category.Parent }, true);
                                    isParent[0].isHalfCheck = false;
                                    if (isParent[0].Parent != null) {
                                        tickParent(isParent[0])
                                    }
                                } else {
                                    if (isDifferent(_category)) {
                                        var isParent = $filter("filter")($scope.CategoryList, { Code: _category.Parent }, true);
                                        isParent[0].isHalfCheck = false;
                                        if (isParent[0].Parent != null) {
                                            tickParent(isParent[0])
                                        }
                                    } else {
                                        changeCheckBoxTickType(_category, true)
                                    }


                                }
                            }
                            else if (count == Has_Sibling.length - 2) {
                                if (_category.checked) {
                                    var NotTick = $filter("filter")(Has_Sibling, { checked: false });
                                    if (isDifferent(NotTick[0])) {
                                        var isParent = $filter("filter")($scope.CategoryList, { Code: _category.Parent }, true);
                                        isParent[0].isHalfCheck = false;
                                        if (isParent[0].Parent != null) {
                                            tickParent(isParent[0])
                                        }
                                    } else { changeCheckBoxTickType(_category, true) }
                                } else {
                                    changeCheckBoxTickType(_category, true)
                                }
                            } else {
                                changeCheckBoxTickType(_category, true)
                            }

                        } else {
                            if (_category.checked) {
                                changeCheckBoxTickType(_category, false)
                            } else {
                                var isParent = $filter("filter")($scope.CategoryList, { Code: _category.Parent }, true);
                                isParent[0].checked = false;
                                if (isParent[0].Parent != null) {
                                    tickParent(isParent[0])
                                }
                            }
                        }
                        //   changeRootCategory_CheckBoxType()
                    }

                    function changeCheckBoxTickType(_category, _halfCheck) {
                        var isParent = $filter("filter")($scope.CategoryList, { Code: _category.Parent }, true);
                        isParent[0].checked = true;
                        isParent[0].isHalfCheck = _halfCheck
                        if (isParent[0].Parent != null) {
                            changeCheckBoxTickType(isParent[0], _halfCheck)
                        }
                    }

                    function changeRootCategory_CheckBoxType() {
                        var notTick = $filter("filter")($scope.CategoryList, { checked: false });
                        if (notTick.length > 0) {
                            $scope.data.category = true
                            $scope.rootCateoryHalfCheck = true;
                            var hasTick = $filter("filter")($scope.CategoryList, { checked: true });
                            if (hasTick.length == 0) {
                                $scope.data.category = false
                                $scope.rootCateoryHalfCheck = false;
                            } else {
                                for (let index = 0; index < notTick.length; index++) {
                                    const element = notTick[index];
                                    if (isDifferent(element)) {
                                        $scope.data.category = true
                                        $scope.rootCateoryHalfCheck = false;
                                    } else {
                                        $scope.data.category = true
                                        $scope.rootCateoryHalfCheck = true;
                                        break;
                                    }
                                }
                            }
                        } else {
                            $scope.data.category = true
                            $scope.rootCateoryHalfCheck = false;
                        }
                    }

                    function getCategory4Save() {
                        $scope.CheckCategoryList = []
                        $scope.CategoryList.forEach(element => {
                            if (element.checked) {
                                var category = {}
                                category.MD_AppID = $scope.data.MD_AppID || null
                                category.CategoryID = element.ID
                                $scope.CheckCategoryList.push(category)

                                if (isDifferent(element)) {
                                    var data = {}
                                    data.OtherID = null;
                                    data.ItemID = element.ID;
                                    data.OtherContent = element.OtherContent;
                                    data.AppID = $scope.data.MD_AppID || null
                                    $scope.OtherList.push(data)
                                }
                            }
                        });
                    }

                    /** AFFECTING RANGE **/
                    function showRange(status, date, callback) {
                        $scope.AffectRangeList = []
                        ModificationService.SearchRange().get({
                            Language: lang,
                            AffectRangeCode: '',
                            AffectRangeContent: '',
                            Status: status,
                            FromDate: date,
                            ToDate: date
                        }).$promise.then(function (res) {
                            $scope.AffectRangeList = res;

                            $scope.AffectRangeList.forEach(element => {
                                element.checked = false;
                                var different = '其他khác'
                                if (element.AffectRangeContent.replace(/[&\/\\#,+()$~%.'":*?<>{}!\s]/g, '').toLowerCase() == different) {
                                    element.Other = true;
                                } else {
                                    element.Other = false;
                                    element.OtherContent = null;
                                }
                            });

                            callback('')

                            console.log($scope.AffectRangeList);
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                            callback(errResponse)
                        });
                    }

                    $scope.checkAllRange = function (isChecked) {
                        $scope.rootRangeHalfCheck = false
                        if (isChecked) {
                            $scope.AffectRangeList.forEach(element => {
                                var different = '其他khác'
                                if (element.AffectRangeContent.replace(/[&\/\\#,+()$~%.'":*?<>{}!\s]/g, '').toLowerCase() != different) {
                                    element.checked = true
                                }

                            });
                        } else {
                            $scope.AffectRangeList.forEach(element => {
                                element.checked = false
                            });
                        }
                    }

                    $scope.checkRange = function (_range) {
                        var different = '其他khác'
                        if (_range.AffectRangeContent.replace(/[&\/\\#,+()$~%.'":*?<>{}!\s]/g, '').toLowerCase() == different) {
                            _range.OtherContent = null
                        }
                        changeRootRange_CheckBoxType()
                    }

                    function changeRootRange_CheckBoxType() {
                        var notTick = $filter("filter")($scope.AffectRangeList, { checked: false });
                        if (notTick.length > 0) {
                            $scope.data.range = true
                            $scope.rootRangeHalfCheck = true;
                            var hasTick = $filter("filter")($scope.AffectRangeList, { checked: true });
                            if (hasTick.length == 0) {
                                $scope.data.range = false
                                $scope.rootRangeHalfCheck = false;
                            } else if (hasTick.length == $scope.AffectRangeList.length - 1) {
                                var _affectRange = $filter("filter")($scope.AffectRangeList, { checked: false });
                                var different = '其他khác'
                                if (_affectRange[0].AffectRangeContent.replace(/[&\/\\#,+()$~%.'":*?<>{}!\s]/g, '').toLowerCase() == different) {
                                    $scope.rootRangeHalfCheck = false;
                                }
                            }
                        } else {
                            $scope.data.range = true
                            $scope.rootRangeHalfCheck = false;
                        }
                    }

                    function getRange4Save() {
                        $scope.OtherList = []
                        $scope.CheckAffectRangeList = []
                        $scope.AffectRangeList.forEach(element => {
                            if (element.checked) {
                                var range = {}
                                range.MD_AppID = $scope.data.MD_AppID || null
                                range.AffectRangeID = element.AffectRangeID
                                $scope.CheckAffectRangeList.push(range)

                                var different = '其他khác'
                                if (element.AffectRangeContent.replace(/[&\/\\#,+()$~%.'":*?<>{}!\s]/g, '').toLowerCase() == different) {
                                    var data = {}
                                    data.OtherID = null;
                                    data.ItemID = element.AffectRangeID;
                                    data.OtherContent = element.OtherContent;
                                    data.AppID = $scope.data.MD_AppID || null
                                    $scope.OtherList.push(data)
                                }
                            }
                        });
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
                                f.AppID = $scope.data.MD_AppID || null
                                f.AttachedForItem = fileAttached[i].AttachedForItem
                                $scope.UpFileList.push(f)
                                i++;
                                if (i < fileAttached.length) {
                                    uploadFile(fileAttached, i, callback);
                                } else callback('');
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
                            if (AttachedForItem == "MD_Area") {
                                if ($scope.fileArea != "") {
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': $translate.instant('File_Number_MSG')
                                    });
                                    return false;
                                }
                            }

                            if (AttachedForItem == "MD_Document") {
                                if ($scope.fileDocument != "") {
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': $translate.instant('File_Number_MSG')
                                    });
                                    return false;
                                }
                            }

                            if (AttachedForItem == "MD_Record") {
                                if ($scope.fileRecord != "") {
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': $translate.instant('File_Number_MSG')
                                    });
                                    return false;
                                }
                            }

                            $scope.file = $files[0];
                            if (AttachedForItem == "MD_Record") {
                                if (!$scope.file.name.toLowerCase().includes(".pdf") && !$scope.file.name.toLowerCase().includes(".zip") && !$scope.file.name.toLowerCase().includes(".rar")) {
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': $translate.instant('FileRecordNote')
                                    });
                                    return false;
                                }
                            } else {
                                if (!$scope.file.name.toLowerCase().includes(".zip") && !$scope.file.name.toLowerCase().includes(".rar")) {
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': $translate.instant('FileUploadNote')
                                    });
                                    return false;
                                }
                            }



                            var f = {}
                            f.AttachedForItem = AttachedForItem
                            f.file = $scope.file
                            if (AttachedForItem == "MD_Area") {
                                $scope.fileArea = $scope.file.name;
                                $scope.AllFile.push(f)
                            } else if (AttachedForItem == "MD_Document") {
                                $scope.fileDocument = $scope.file.name;
                                $scope.AllFile.push(f)
                            } else if (AttachedForItem == "MD_Record") {
                                $scope.fileRecord = $scope.file.name;
                                $scope.AllFile.push(f)
                            }
                        }
                    }

                    $scope.getRemoveFileName = function (filename, AttachedForItem) {
                        $scope.removeFileList.push(filename);
                        if (AttachedForItem == "MD_Area") {
                            $scope.fileArea = "";
                        } else if (AttachedForItem == "MD_Document") {
                            $scope.fileDocument = "";
                        } else if (AttachedForItem == "MD_Record") {
                            $scope.fileRecord = "";
                        }

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

                    /************************************/

                    $scope.changeRiskLevel = function () {
                        $scope.data.AffectDivision = []
                    }

                    var formVariables = [];
                    var historyVariable = [];
                    $scope.saveModificationApp = function (_status) {
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
                                getRange4Save()
                                getCategory4Save()
                                var params = {}
                                params.MD_AppID = $scope.data.MD_AppID || null
                                params.MD_ProjectID = $scope.data.MD_ProjectID || null
                                params.CostCenter = $scope.data.CostCenter
                                params.MD_Name = $scope.data.MD_Name
                                params.MD_Type = $scope.data.MD_Type

                                if ($scope.data.MD_Type == "shortTerm") {
                                    params.ExpectDateBegin = $scope.data.ExpectDateBegin
                                    params.ExpectDateEnd = $scope.data.ExpectDateEnd
                                } else {
                                    params.ExpectDateBegin = null
                                    params.ExpectDateEnd = null
                                }

                                params.RiskLevel = $scope.data.RiskLevel

                                if ($scope.data.AffectDivision) {
                                    var devision = ""
                                    $scope.data.AffectDivision.forEach(element => {
                                        if (devision) {
                                            devision = devision + '|' + element
                                        } else devision = element
                                    });

                                    params.AffectDivision = devision
                                } else params.AffectDivision = null

                                params.Reason = $scope.data.Reason
                                params.Area = $scope.data.Area
                                params.Description = $scope.data.Description
                                params.ConstructionName = $scope.data.ConstructionCheck ? $scope.data.ConstructionName : null
                                params.CapitalCode = $scope.data.CapitalCodeCheck ? $scope.data.CapitalCode : null
                                params.ProjectID = $scope.data.ProjectIDCheck ? $scope.data.ProjectID : null
                                if (_status == 'ReSubmit' || _status == 'Q') {
                                    params.Status = _status
                                } else {
                                    params.Status = 'N'
                                }

                                params.ISO_ID = $scope.data.ISO_ID
                                params.Creator = Auth.username

                                params.CategoryList = $scope.CheckCategoryList
                                params.Others = $scope.OtherList
                                params.RangeList = $scope.CheckAffectRangeList
                                params.files = $scope.UpFileList
                                debugger;
                                params.CreateDate = $scope.data.CreateDate

                                ModificationService.Save_ModificationApp().save({}, params).$promise.then(function (res) {
                                    console.log()
                                    $scope.data.MD_ProjectID = res.MD_ProjectID
                                    $scope.data.MD_AppID = res.MD_AppID
                                    if (_status == 'Submit') {
                                        ModificationService.GetCheckers().get({
                                            owner: Auth.username,
                                            flowkey: "ModificationAndRisk"
                                        }).$promise.then(function (res) {
                                            debugger;
                                            var LeaderArray = [];
                                            for (var i = 0; i < res.length; i++) {
                                                LeaderArray[i] = res[i].Person;
                                            }

                                            if (LeaderArray.length == 0) {
                                                Notifications.addError({ 'status': 'error', 'message': $translate.instant('Leader_NO_MSG') });
                                                return
                                            } else {
                                                formVariables = [];
                                                historyVariable = [];

                                                formVariables.push({ name: "LeaderArray", value: LeaderArray });
                                                formVariables.push({ name: "VoucherID", value: $scope.data.MD_AppID });
                                                formVariables.push({ name: "start_remark", value: $scope.data.MD_ProjectID + " _ " + $scope.data.ISO_AppCode + ": " + $scope.data.MD_Name });
                                                formVariables.push({ name: "VoucherType", value: 'MD' });

                                                historyVariable.push({ name: "Expected_code", value: $scope.data.MD_ProjectID + ' (Mã số này có thể thay đổi)' });
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
                                                                        var query = {}
                                                                        query.MD_AppID = $scope.data.MD_AppID
                                                                        query.status = 'F'
                                                                        query.deletePerson = Auth.username
                                                                        query.specialDeleteReason = ''
                                                                        ModificationService.UpdateMDStatus().save(query, {}).$promise.then(function (res) {
                                                                            console.log(res);
                                                                            sendMail($scope.data.MD_AppID, 'init', function (err) {
                                                                                $location.path('/taskForm/' + url);
                                                                                $timeout(function () {
                                                                                    Notifications.addMessage({ 'status': 'info', 'message': $translate.instant('Submit_Voucher_MSG') + $scope.data.MD_ProjectID });
                                                                                }, 500);
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
                                    }
                                    else if (_status == 'ReSubmit') {
                                        $scope.formVariables.push({ name: "start_remark", value: $scope.data.MD_ProjectID + " _ " + $scope.data.ISO_AppCode + ": " + $scope.data.MD_Name });
                                        $scope.formVariables.push({ name: "update", value: "OK" });
                                        $scope.historyVariable.push({ name: "update", value: "YES" });
                                        $scope.historyVariable.push({ name: "Expected_code", value: $scope.data.MD_ProjectID + ' (Mã số này có thể thay đổi)' });

                                        sendMail($scope.data.MD_AppID, 'init', function (err) {
                                            $scope.submit();
                                        })
                                    }
                                    else if (_status == 'Q') {
                                        $scope.historyVariable.push({ name: "MD_ProjectID", value: res.MD_ProjectID });

                                        sendMail($scope.data.MD_AppID, 'complete', function (err) {
                                            $scope.submit();
                                        })
                                    }
                                    else {
                                        $scope.goBack();
                                        $timeout(function () {
                                            Notifications.addMessage({ 'status': 'info', 'message': $translate.instant('Save_Voucher_MSG') + res.MD_ProjectID });
                                        }, 500);
                                    }
                                }, function (errResponse) {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                });

                            }
                        })
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
                                // else if (element.Remark == "FactoryManager") {
                                //     hseFactoryManager = element.Person
                                // }
                            });

                            formVariables.push({ name: "hseSafety", value: hseSafety });
                            formVariables.push({ name: "hseEnvironment", value: hseEnvironment });
                            formVariables.push({ name: "hseFire", value: hseFire });
                            formVariables.push({ name: "hseDivisionManager", value: hseDivisionManager });
                            // formVariables.push({ name: "hseFactoryManager", value: hseFactoryManager });

                            callback('')
                        }, function (errResponse) {
                            callback(errResponse)
                        });
                    }

                    $scope.changeStatus = function (_status) {
                        var query = {}
                        query.MD_AppID = $scope.variable.VoucherID
                        query.status = _status
                        query.deletePerson = Auth.username
                        query.specialDeleteReason = ''
                        ModificationService.UpdateMDStatus().save(query, {}).$promise.then(function (res) {
                            console.log(res);
                            $scope.submit();
                        }, function (errResponse) {
                            Notifications.addError({ 'status': 'error', 'message': errResponse });
                        })
                    }

                    function getVicePresident(callback) {
                        if ($scope.checker == 'hseManager') {
                            $scope.formVariables.push({ name: "SendVicePresident", value: $scope.data.SendVicePresident });
                            $scope.historyVariable.push({ name: "SendVicePresident", value: $scope.data.SendVicePresident });

                            if ($scope.data.SendVicePresident == "YES") {
                                ModificationService.GetCheckerByKind().get({ kind: "VicePresident" }).$promise.then(function (res) {
                                    $scope.formVariables.push({ name: "VicePresident", value: res[0].Person });
                                    callback('')
                                }, function (errResponse) {
                                    callback(errResponse)
                                });
                            } else {
                                callback('')
                            }
                        } else {
                            callback('')
                        }
                    }

                    $scope.saveSubmit = function () {
                        debugger;
                        $scope.formVariables.push({ name: $scope.checker + "Agree", value: $scope.data.IsAgree });
                        $scope.historyVariable.push({ name: $scope.checker + "Agree", value: $scope.data.IsAgree });
                        if ($scope.checker == 'hseDivisionManager') {
                            $scope.formVariables.push({ name: "hseFactoryManager", value: $scope.data.hseManager });
                        }
                        if ($scope.data.IsAgree === "YES") {
                            getVicePresident(function (errResponse) {
                                if (errResponse != '') {
                                    Notifications.addError({ 'status': 'error', 'message': errResponse });
                                    return;
                                }
                                else {
                                    if ($scope.EnterApprovalReason) {
                                        $scope.formVariables.push({ name: $scope.checker + "ApprovalReason", value: $scope.data.ApprovalReason });
                                        $scope.historyVariable.push({ name: $scope.checker + "ApprovalReason", value: $scope.data.ApprovalReason });
                                    }

                                    if ($scope.checker == 'hseManager' && $scope.data.SendVicePresident == 'NO') {
                                        $scope.saveModificationApp('Q')
                                    } else if ($scope.checker == 'Vice') {
                                        $scope.saveModificationApp('Q')
                                    } else {
                                        debugger;
                                        sendMail($scope.data.MD_AppID, $scope.MailKind, function (err) {
                                            $scope.submit();
                                        })
                                    }
                                }
                            })
                        } else {
                            $scope.reject($scope.checker + "DenyReason")
                        }
                    }

                    $scope.reject = function (_reasonName) {
                        $scope.formVariables.push({ name: _reasonName, value: $scope.data.DenyReason });
                        $scope.historyVariable.push({ name: _reasonName, value: $scope.data.DenyReason });
                        if($scope.checker=="hseSafety" || $scope.checker=="hseEnvironment" || $scope.checker=="hseFire" || $scope.checker=="hseDivisionManager" ){
                            sendMail($scope.data.MD_AppID, $scope.MailKind, function (err) {
                                $scope.submit();
                            })
                        }else{
                            sendMail($scope.data.MD_AppID, 'reject', function (err) {
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
                        ModificationService.GetModificationAndRiskPID().get({ VoucherID: _AppID }).$promise.then(function (res) {
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
                                        receiver[2] = taf({ TaskName: "Manager Check" }).last();
                                        if (receiver[2].UserId == receiver[1].UserId) {
                                            receiver[2] = {}
                                        }

                                        receiver[3] = taf({ TaskName: "HSE Safety & Health Section" }).start(1).first();
                                        receiver[4] = taf({ TaskName: "HSE Environmental Section" }).start(1).first();
                                        receiver[5] = taf({ TaskName: "HSE Fire Fighting Section" }).start(1).first();

                                        receiver[6] = taf({ TaskName: "HSE Division Manager Check" }).start(1).first();

                                        receiver[7] = taf({ TaskName: "HSE Factory Manager Check" }).start(1).first();

                                        receiver[8] = taf({ TaskName: "Vice President Check", }).start(1).first();
                                    }
                                    else if ($scope.checker == 'hseDivisionManager' || $scope.checker == 'hseManager' || $scope.checker == 'Vice') {
                                        if ($scope.checker == 'Vice') {
                                            receiver[0] = taf({ TaskName: "起始表单" }).first();

                                            var log_divisionManager = taf({ TaskName: "Manager Check" }).start(1).first();
                                            receiver.push(log_divisionManager)

                                            var log_factoryManager = taf({ TaskName: "Manager Check" }).last();
                                            if (log_factoryManager.UserId != log_divisionManager.UserId) {
                                                receiver.push(log_factoryManager)
                                            }
                                            receiver.push(taf({ TaskName: "HSE Division Manager Check" }).last());
                                            receiver.push(taf({ TaskName: "HSE Factory Manager Check" }).last())
                                        } else {
                                            receiver[0] = taf({ TaskName: "HSE Safety & Health Section" }).last();

                                            receiver.push(taf({ TaskName: "HSE Environmental Section" }).last());

                                            receiver.push(taf({ TaskName: "HSE Fire Fighting Section" }).last());
                                            if ($scope.checker == 'hseManager') {
                                                receiver.push(taf({ TaskName: "HSE Division Manager Check" }).last());
                                            }

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
                            VoucherType: "MD",
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
                templateUrl: './forms/EHS/Modification/Manage_Modification/Modification/CreateModification.html'
            }
        }
    ]);
});
