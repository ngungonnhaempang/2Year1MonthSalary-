define(['myapp', 'controllers/EHS/Waste/GenerationUnitVoucher',  'angular'], function (myapp, angular) {
    myapp.controller('GenerationUnitController',
        ['$filter','Notifications','Auth','EngineApi','$translate','$q','$scope','WasteItemService','GenerationUnitService',
        function ($filter,Notifications,Auth,EngineApi,$translate,$q,$scope,WasteItemService,GenerationUnitService){
            $scope.onlyOwner = true;
            $scope.bpmnloaded = false;

            $scope.dateFrom = $filter('date')(new Date(), 'yyyy-MM-dd');
            $scope.dateTo = $filter('date')(new Date(), 'yyyy-MM-dd');
            $scope.username = Auth.username;
            $scope.LineMan = Auth.username + '-' + Auth.nickname;
            $scope.workflow = 'FEPVHSEGenerationUnit';
            $scope.SpecialUser = false;
            $scope.isNormalUser = false;
            $scope.PageIndex=1;
            $scope.PageSize= 50;

            var VehicleTypes = 'UnJointTruck';
            var lang = window.localStorage.lang || 'EN';
            $q.all([loadAreaData(),loadRootsData()]);
            $scope.statuslist = [{
                id: 'N',
                name: $translate.instant('Available')
            },
                {
                    id: 'X',
                    name: $translate.instant('Unavailable')
                },
            ];
            $scope.getItemStatus = function (id) {
                var statLen = $filter('filter')($scope.statuslist, { 'id': id });
                if (statLen.length > 0) {
                    return statLen[0].name;
                } else {
                    return id;
                }
            }
            $scope.getArea = function (id) {
                var statLen = $filter('filter')($scope.AreaData, { 'AreaID': id });
                if (statLen.length > 0) {
                    return statLen[0].AreaName;
                } else {
                    return id;
                }
            }
            $scope.getRoot = function (id) {
                var statLen = $filter('filter')($scope.RootsData, { 'RootID': id });
                if (statLen.length > 0) {
                    return statLen[0].RootName;
                } else {
                    return id;
                }
            }
            var col = [

                {
                    field: 'GenerationUnitID',
                    displayName: $translate.instant('GenerationUnitID')
                    , minWidth: 80, cellTooltip: true
                },
                {
                    field: 'State',
                    displayName: $translate.instant('Status_On_Off'),
                    width: 100,
                    minWidth: 10,
                    cellTooltip: true,
                    cellTemplate: '<span>&nbsp{{grid.appScope.getItemStatus(row.entity.State)}}</span>'
                },
                {
                    field: 'RootID',
                    displayName: $translate.instant('RootID'),
                    enableCellEdit: false,
                    minWidth: 120,
                    cellTooltip: true,
                    cellTemplate: '<span>&nbsp{{grid.appScope.getRoot(row.entity.RootID)}}</span>'
                },
                {
                    field: 'AreaID',
                    displayName: $translate.instant('AreaID'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true,
                    cellTemplate: '<span>&nbsp{{grid.appScope.getArea(row.entity.AreaID)}}</span>'
                },
                {
                    field: 'PartsArisingNContractorsArising',
                    displayName: $translate.instant('PartsArisingNContractorsArising'),
                    enableCellEdit: false,
                    minWidth: 120,
                    cellTooltip: true
                },
                {
                    field: 'Description',
                    displayName: $translate.instant('Description'),
                    enableCellEdit: false,
                    minWidth: 180,
                    cellTooltip: true
                },
                {
                    field: 'UserID',
                    displayName: $translate.instant('UserID'),
                    enableCellEdit: false,
                    minWidth: 180,
                    cellTooltip: true
                },
                {
                    field: 'CreateDate',
                    displayName: $translate.instant('CreateDate'),
                    enableCellEdit: false,
                    minWidth: 120,
                    cellTooltip: true
                },
                {
                    field: 'LastModifyDate',
                    displayName: $translate.instant('Last Modify Date'),
                    enableCellEdit: false,
                    minWidth: 180,
                    cellTooltip: true
                }
            ];
             // Grid configuration and col
            $scope.gridOptions = {
                columnDefs: col,
                data: [],
                multiSelect: false,
                enableColumnResizing: true,
                enableColumnResize: true,
                enableCellEditOnFocus: true,
                enableSorting: true,
                showGridFooter: true,
                enableGridMenu: true,
                exporterMenuPdf: true,
                enableSelectAll: false,
                enableRowHeaderSelection: true,
                enableRowHashing: false,
                enableRowSelection: true,
                paginationPageSizes: [50, 100, 200, 500],
                paginationPageSize: 50,
                exporterOlderExcelCompatibility: true,
                useExternalPagination: true,
                enablePagination: true,
                enablePaginationControls: true,
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    EngineApi.getTcodeLink().get({
                        'userid': Auth.username,
                        'tcode': 'FEPVUnjointTruckContainer'
                    }, function (linkres) {
                        if (linkres.IsSuccess) {

                            $scope.SpecialUser = true;
                        }
                    });
                    EngineApi.getTcodeLink().get({
                        'userid': Auth.username,
                        'tcode': $scope.workflow
                    }, function (linkres) {
                        if (linkres.IsSuccess) {
                            /** IF have Auth and Get lastest workflow 's definition ID */

                                gridApi.core.addToGridMenu(gridApi.grid, gridMenu);

                                //gridApi.core.removeFromGridMenu(gridApi.grid, 'test');


                        }
                    });
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        $scope.selectedVoucherid = row.entity.VoucherID;
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        paginationOptions.pageNumber = newPage;
                        paginationOptions.pageSize = pageSize;
                        getPage();
                    });
                },
                rowTemplate: rowTemplate()

            };
            function rowTemplate() {

                return '<div ng-dblclick="grid.appScope.doubleClick(row.entity)" style="cursor: pointer;" >' +
                    '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div>' +
                    '</div>';
            }

            var gridMenu = [

                {
                    title: $translate.instant('Create'),
                    action: function () {
                        $scope.note={};
                        $('#WasteGenerationModal').modal('show');
                    },
                    order: 1,
                    id: 'Create'
                },
                {
                    title: $translate.instant('Update'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        console.log(resultRows);

                        if (resultRows.length == 1) {

                            getGUnitDataByGUnitID(resultRows[0]);
                        } else {
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant('Select_MSG')
                            });
                        }
                    },
                    order: 3,
                    id: 'Update'
                }, {
                    title: $translate.instant('Delete'),
                    icon: 'ui-grid-icon-cancel',
                    action: function () {
                        var selectRows = $scope.gridApi.selection.getSelectedGridRows();
                        var GenerationUnitID = selectRows[0].entity.GenerationUnitID;
                        var PartsArisingNContractorsArising = selectRows[0].entity.PartsArisingNContractorsArising;
                        if (confirm(GenerationUnitID + '|' + PartsArisingNContractorsArising + $translate.instant('Delete_IS_MSG'))) {
                            GenerationUnitService.GenerationUnit.deleteGUnitID({GUnitID:GenerationUnitID},{})
                                .$promise.then(function (res) {
                                if(res!=null){
                                    Notifications.addMessage({'status': 'info', 'message': res.msg});
                                    $scope.Search();
                                }
                            },function (err) {
                                Notifications.addError({'status': 'error', 'message': error});
                            })
                        }
                    },
                    order: 5,
                    id: 'Delete'
                },

            ];
            function  getGUnitDataByGUnitID(resultRows) {


                if (resultRows.State == 'N' && resultRows.UserID == Auth.username) {
                    GenerationUnitService.GenerationUnit.getGUnitData({GUnitID:resultRows.GenerationUnitID}).$promise.then(function (res) {
                        if(res!=null){

                            $scope.note=res;
                            $('#UpdateWasteGenerationModal').modal('show');
                        }
                    },function (error) {

                    })
                }

            }
            function loadAreaData(){
                var deffered =$q.defer();
                WasteItemService.GetInfoBasic.GetAreabyLang({Lang : lang}).$promise.then(function (res) {
                    $scope.AreaData = res;
                    deffered.resolve(res);
                },function (err) {
                    deffered.reject(err);
                })
            }
            function loadRootsData() {
                var deffered = $q.defer();
                WasteItemService.GetInfoBasic.GetRootsbyLang({Lang: lang}).$promise.then(function (res) {
                    $scope.RootsData = res;
                    deffered.resolve(res);
                },function (err) {
                    deffered.reject(err);
                })
            }
            function loadGenerateUnit(){
                var deffered = $q.defer();
                if($scope.recod.Area!=null && $scope.recod.Roots!=null){
                    WasteItemService.GetInfoBasic.GetGenerateUnitParts({AreaID:$scope.recod.Area ,RootsID:$scope.recod.Roots})
                        .$promise.then(function (res) {
                        $scope.GenerateUnitPartsData = res;
                        deffered.resolve(res);
                    },function (err) {
                        deffered.reject(err);
                    })
                }

            }
            $scope.$watch("recod.Area", function (n) {
                if (n !== undefined ) {
                    loadGenerateUnit();
                }
            });
            $scope.$watch("recod.Roots", function (n) {
                if (n !== undefined ) {
                    loadGenerateUnit();
                }
            });
            $scope.closeModal= function(){
                $('#WasteGenerationModal').modal('hide');
            }
            $scope.closeUpdateModal= function(){
                $('#UpdateWasteGenerationModal').modal('hide');
            }

            $scope.submitGenerationUnit= function () {
                var query = {};
                query = $scope.note;
                query.UserID=Auth.username;


                GenerationUnitService.GenerationUnit.save(query).$promise.then(function (res) {
                    if(res!=null){
                        $scope.note.GenerationUnitID= res.GenerationUnitID;
                        Notifications.addMessage({'status': 'info', 'message': 'Added new GenerationUnit'});
                        $scope.Search();
                        $('#WasteGenerationModal').modal('hide');
                    }
                },function (error) {
                    Notifications.addError({'status': 'error', 'message': error});
                })
            }
            $scope.UpdateGenerationUnit= function () {
                var query = {};
                query = $scope.note;
                query.UserID=Auth.username;


                GenerationUnitService.GenerationUnit.save(query).then(function (res) {
                    if(res!=null){
                        $scope.note.GenerationUnitID= res.GenerationUnitID;
                        Notifications.addMessage({'status': 'info', 'message': 'Updated GenerationUnit'});
                        $('#UpdateWasteGenerationModal').modal('hide');
                        $scope.Search();
                    }
                },function (error) {
                    Notifications.addError({'status': 'error', 'message': error});
                })
            }

            $scope.Search = function(){
                var query={};


                if($scope.onlyOwner)
                {
                    query.UserID= $scope.username;
                }else{
                    query.UserID= "";
                }
                query.dateFrom=$scope.dateFrom;
                query.dateTo=$scope.dateTo;
                query.GUnitID=$scope.GUnitID||"";

                query.State=$scope.State||"";
                query.RootID=$scope.RootID||"";
                query.AreaID=$scope.AreaID||"";
                query.PartsArisingNContractorsArising=$scope.PartsArisingNContractorsArising||"";

                query.Des = "";
                query.pageIndex= $scope.PageIndex;
                query.pageSize = $scope.PageSize;
                GenerationUnitService.GenerationUnit.query(query).$promise.then(function (res) {
                    if(res!=null){
                        $scope.gridOptions.data=  res.TableData;
                        $scope.gridOptions.totalItems = res.TableCount[0];
                    }
                },function (error) {
                    Notifications.addError({'status': 'error', 'message': error});
                })
            }

        }]);
});