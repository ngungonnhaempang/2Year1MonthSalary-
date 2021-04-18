define(['myapp', 'controllers/EHS/Waste/Directive/VoucherDirective', 'angular', 'jszip', 'xlsx'], function (myapp, angular, jszip, xlsx) {
    myapp.controller('VoucherController', ['$filter', 'Notifications', 'Auth', 'EngineApi', 'VoucherService', 'WasteItemService', 'CompanyService', '$translate', '$q', '$scope', '$routeParams','$timeout',
        function ($filter, Notifications, Auth, EngineApi, VoucherService, WasteItemService,
                  CompanyService, $translate, $q, $scope, $routeParams,$timeout) {
            var lang = window.localStorage.lang || 'EN';
            $scope.isLockNUnlock=false;
            $scope.isReportShow= false;
            $scope.reloadGrid = true;
            $scope.recod = {};
            $scope.recod.Creator = Auth.username;
            $scope.onlyOwner = true;
            $scope.isError = false;
            $scope.status = '';
            var paginationOptions = {
                pageNumber: 1,
                pageSize: 50,
                totalItems: 0,
                sort: null
            };
            var full_lsWastItems = []; //defaul Full list
            var full_lsCompany = [];
            $scope.WasteTypeData = [];
            $scope.disableProcessComp = false;
            /**
             * Init data
             */

            $q.all([loadDepartment(), loadCompany(), loadWasteItems(), loadWasterType(), loadArea(), loadRoots()]).then(function (result) {
                $scope.statuslist = [{
                    id: 'N',
                    name: $translate.instant('StatusN')
                },
                    {
                        id: 'M',
                        name: $translate.instant('StatusM')
                    },
                    {
                        id: 'X',
                        name: $translate.instant('StatusX')
                    }
                ];
                $scope.HSEstatuslist = [{
                    id: 'True',
                    name: $translate.instant('Lock')
                },
                    {
                        id: 'False',
                        name: $translate.instant('Unlock')
                    }
                ];
                console.log(result);
            }, function (error) {
                Notifications.addError({
                    'status': 'Failed',
                    'message': 'Loading failed: ' + error
                });
            });

            /*
             * Load VoucherDetail
             */
            function loadVoucherDetail(id) {
                var deferred = $q.defer();
                VoucherService.FindByID({
                    VoucherID: id
                }, function (data) {
                    $scope.recod.voucher_id = data.VoucherID;
                    $scope.recod.owner_comp = data.OwnerComp;
                    $scope.recod.WasteType = data.VoucherWasteType;
                    $scope.Creator = data.UserID;
                    $scope.recod.DateTransfer = data.DateComplete;
                    $scope.recod.Roots = data.Roots;
                    $scope.recod.Area = data.Area;
                    $scope.recod.VehicleNO = data.VehicleNo;
                    $scope.recod.GUnitID = data.GenUnitID;
                    $scope.recod.DateTransfer = $filter('date')(data.DateComplete, 'yyyy-MM-dd');
                    $scope.recod.voucher_number = data.VoucherNumber; //$scope.recod.voucher_number;
                    $scope.recod.process_comp = data.ProcessComp;
                    $scope.recod.location = data.Location;
                    $scope.recod.date_out = $filter('date')(data.DateOut, 'yyyy-MM-dd');
                    $scope.recod.date_complete = $filter('date')(data.DateComplete, 'yyyy-MM-dd');
                    $scope.recod.return_reason = data.ReturnReason;
                    $scope.recod.create_time = data.CreateTime;

                    $scope.wasteItems = []; //list details of wasteitems
                    $scope.processcomp_reupdate_wastelist(data.ProcessComp);
                    data.VoucherDetails.forEach(element => {
                        var x = {};
                    var item = full_lsWastItems.filter(x => x.WasteID === element.WasteID
                )
                    ;
                    if (item.length > 0) {
                        x.method_name = item[0].MethodDescription;
                        x.waste_name = item[0].WasteDescription;
                        x.Quantity = element.Quantity;
                        x.Weight = element.Weight;
                        x.WasteID = element.WasteID;
                        x.GUnitID = data.GenUnitID;
                        x.Location = element.Location;
                        $scope.wasteItems.push(x);
                    }
                })
                    console.log(data);
                    deferred.resolve(data);
                }, function (error) {
                    deferred.reject(error);
                    P
                })
                return deferred.promise;
            }

            /**
             * Load Department into Combobox
             * */
            function loadDepartment() {
                var deferred = $q.defer();
                var query = {
                    DepartType: 'Department',
                    lang: lang
                };
                VoucherService.GetDepartment(query, function (data) {
                    $scope.departments = data;
                    deferred.resolve(data);
                }, function (error) {
                    deferred.resolve(error);
                })
                query.DepartType = 'CenterDepartment';
                VoucherService.GetDepartment(query, function (data) {
                    $scope.cdepartments = data;
                    deferred.resolve(data);
                }, function (error) {
                    deferred.resolve(error);
                })

            }

            function loadCompany() {
                var deferred = $q.defer();
                CompanyService.GetInfoBasic.GetOwnerCompany({}, function (data) {
                    $scope.company = full_lsCompany = data;
                    deferred.resolve(data);
                }, function (error) {
                    deferred.resolve(error);
                })
            }

            function loadGenerateUnit() {
                var deffered = $q.defer();
                if ($scope.recod.Area != null && $scope.recod.Roots != null) {

                }

            }

            $scope.$watch('recod.Roots + recod.Area', function (n, m) {
                if (n !== undefined && m !== null) {
                    WasteItemService.GetInfoBasic.GetGenerateUnitParts({
                        AreaID: $scope.recod.Area || '',
                        RootsID: $scope.recod.Roots || ''
                    })
                        .$promise.then(function (res) {
                        $scope.GenerateUnitPartsData = res;

                    }, function (err) {

                    })
                }
            })

            /**
             * Load WasteItems (update entities)
             */
            function loadWasteItems() {
                var deferred = $q.defer();
                var query = {
                    WasteID: '',
                    lang: lang,
                    ProcessComp: ''
                };
                WasteItemService.GetWasteItem(query, function (data) {
                    console.log(data);
                    full_lsWastItems = data;
                    deferred.resolve(data);
                }, function (error) {
                    deferred.resolve(error);
                })
            }

            function loadWasterType() {
                var deferred = $q.defer();
                WasteItemService.GetInfoBasic.GetWasterTypeByLang({lang: lang}).$promise.then(function (res) {
                    $scope.WasteTypeData = res;
                    deferred.resolve(res);
                }, function (err) {
                    deferred.reject(err);
                });
            }

            function loadArea() {
                var deffered = $q.defer();
                WasteItemService.GetInfoBasic.GetAreabyLang({Lang: lang}).$promise.then(function (res) {
                    $scope.AreaData = res;
                    deffered.resolve(res);

                }, function (err) {
                    deffered.reject(err);
                })
            }

            function loadRoots() {
                var deffered = $q.defer();
                WasteItemService.GetInfoBasic.GetRootsbyLang({lang: lang}).$promise.then(function (res) {
                    $scope.RootsData = res;
                    deffered.resolve(res);
                }, function (err) {
                    deffered.reject(err);
                })
            }

            /**
             * Define All Columns in UI Grid
             */
            var col = [{
                field: 'VoucherID',
                minWidth: 120,
                displayName: $translate.instant('VoucherID'),
                cellTooltip: true,
                visible: true,
                cellTemplate: '<a href="#/waste/Voucher/print/{{COL_FIELD}}" style="padding:5px;display:block; cursor:pointer" target="_blank">{{COL_FIELD}}</a>'

            },
                {
                    field: 'UserID',
                    minWidth: 100,
                    displayName: $translate.instant('CreateBy'),
                    cellTooltip: true
                },
                {
                    field: 'Status',
                    displayName: $translate.instant('Status'),
                    minWidth: 110,
                    cellTooltip: true,
                    cellTemplate: '<span>&nbsp{{grid.appScope.getVoucherStatus(row.entity.Status)}}</span>'
                },
                {
                    field: 'HSE_LockStatus',
                    displayName: $translate.instant('HSE_LockStatus'),
                    minWidth: 110,
                    cellTooltip: true,
                    cellTemplate: '<span>&nbsp{{grid.appScope.getHSEStatus(row.entity.HSE_LockStatus)}}</span>'
                },
                {
                    field: 'OwnerComp',
                    displayName: $translate.instant('OwnerComp'),
                    minWidth: 120,
                    cellTooltip: true,
                    visible: false

                },
                {
                    field: 'ProcessComp',
                    minWidth: 120,
                    displayName: $translate.instant('ProcessComp'),
                    cellTooltip: true
                },
                {
                    field: 'VoucherNumber',
                    minWidth: 155,
                    displayName: $translate.instant('VoucherNumber'),
                    cellTooltip: true
                },
                {
                    field: 'DepartReq',
                    minWidth: 100,
                    displayName: $translate.instant('DepartReq'),
                    cellTooltip: true
                },
                {
                    field: 'DepartProcess',
                    minWidth: 100,
                    displayName: $translate.instant('DepartProcess'),
                    cellTooltip: true
                },
                {
                    field: 'DateOut',
                    minWidth: 100,
                    displayName: $translate.instant('DateOut'),
                    cellTooltip: true
                },
                {
                    field: 'InternalPhone',
                    minWidth: 80,
                    displayName: $translate.instant('InternalPhone'),
                    cellTooltip: true
                },
                {
                    field: 'Location',
                    minWidth: 120,
                    displayName: $translate.instant('Location'),
                    cellTooltip: true
                },
                {
                    field: 'SumTotal',
                    minWidth: 80,
                    displayName: $translate.instant('SumTotal'),
                    cellTooltip: true
                },
                {
                    field: 'SumQty',
                    minWidth: 80,
                    displayName: $translate.instant('SumQty'),
                    cellTooltip: true
                },
                {
                    field: 'CreateTime',
                    displayName: $translate.instant('CreateTime'),
                    width: 170,
                    minWidth: 150,
                    cellTooltip: true
                }
            ];
            $scope.getVoucherStatus = function (id) {
                var statLen = $filter('filter')($scope.statuslist, {'id': id});
                if (statLen.length > 0) {
                    return statLen[0].name;
                } else {
                    return id;
                }
            };
            $scope.getHSEStatus = function (id) {
                var statLen = $filter('filter')($scope.HSEstatuslist, {'id': id});
                if (statLen.length > 0) {
                    return statLen[0].name;
                } else {
                    return id;
                }
            };
            /**
             * Query Grid setting
             */
            $scope.gridOptions = {
                columnDefs: col,
                data: [],
                enableColumnResizing: true,
                enableFullRowSelection: true,
                enableSorting: true,
                showGridFooter: false,
                enableGridMenu: true,
                exporterMenuPdf: false,
                enableSelectAll: false,
                enableRowHeaderSelection: true,
                enableRowSelection: true,
                multiSelect: true,
                paginationPageSizes: [1000, 20000, 500, 10000],
                paginationPageSize: 1000,
                enableFiltering: false,
                exporterOlderExcelCompatibility: true,
                useExternalPagination: true,
                enablePagination: true,
                enablePaginationControls: true,
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    EngineApi.getTcodeLink().get({
                        'userid': Auth.username,
                        'tcode': 'HW-LockNUnlock'
                    }, function (linkres) {
                        if (linkres.IsSuccess) {
                            //linkres.IsSuccess

                            $scope.isLockNUnlock=true;
                        }
                    });

                    EngineApi.getTcodeLink().get({
                        'userid': Auth.username,
                        'tcode': 'HW-Report'
                    }, function (linkres) {
                        if (linkres.IsSuccess) {
                            //linkres.IsSuccess
                            $scope.isReportShow= true;
                            if($scope.isReportShow){
                                AddLockNUnlockControl();
                            }

                            $scope.gridApi.core.addToGridMenu( $scope.gridApi.grid, gridMenu);
                        }else{
                            if($scope.isReportShow){
                                gridMenu=[];
                                AddLockNUnlockControl();
                                $scope.gridApi.core.addToGridMenu( $scope.gridApi.grid, gridMenu);
                            }


                        }
                    });


                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        $scope.selectedSupID = row.entity.SupID;
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        paginationOptions.pageNumber = newPage;
                        paginationOptions.pageSize = pageSize;
                        $scope.Search();
                    });


                }


            };
function AddLockNUnlockControl() {
    $scope.reloadGrid = false;
    if($scope.isLockNUnlock){
        gridMenu.push(
            {
                title: '📗 ' + $translate.instant('DetailExport'),
                action: function () {
                    var req = SearchList();
                    VoucherService.ExportXLSData(req, function (res) {
                        console.log(res);
                        if (res.length > 0) {
                            /**Add another header row before adding data */
                            var headArray =
                                [
                                    $translate.instant('STT'),
                                    $translate.instant('VoucherID'),
                                    $translate.instant('IsValuation'),
                                    $translate.instant('WasteName'),
                                    $translate.instant('OwnerComp'),
                                    $translate.instant('ProcessComp'),
                                    $translate.instant('WasteName'),
                                    $translate.instant('ItemCode'),
                                    $translate.instant('Status'),
                                    $translate.instant('Weight'),
                                    $translate.instant('Quantity'),
                                    $translate.instant('Price'),
                                    $translate.instant('UnitPrice'),
                                    $translate.instant('UpdatePricecount'),
                                    $translate.instant('Roots'),
                                    $translate.instant('AreaName'),
                                    $translate.instant('PartsArisingNContractorsArising'),
                                    $translate.instant('Location'),
                                    $translate.instant('CreateTime'),
                                    $translate.instant('VoucherNumber'),
                                    $translate.instant('UserID')
                                ];
                            // res.forEach(function(item, index, arr){
                            //     item.Status =   $translate.instant('Status'+item.Status);
                            //     switch (item.State){
                            //         case 'S': item.State= $translate.instant('Solid'); break;
                            //         case 'L': item.State= $translate.instant('Liquid'); break;
                            //         case 'M': item.State= $translate.instant('Sludge'); break;
                            //     }
                            // } )


                            var ws = XLSX.utils.aoa_to_sheet([headArray]);

                            ws["!cols"] = [{
                                wpx: 30
                            }, {
                                wpx: 80
                            }, {
                                wpx: 80
                            }, {
                                wpx: 80
                            }, {
                                wpx: 150
                            }, {
                                wpx: 150
                            },
                                {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 70
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }
                            ];
                            ws["!autofilter"] = {ref: "A1:U1"}
                            /** xlsx - add data to file, ingore original header */
                            XLSX.utils.sheet_add_json(ws, res, {skipHeader: true, origin: 'A2'});
                            /** make workbook */


                            var wb = XLSX.utils.book_new()
                            XLSX.utils.book_append_sheet(wb, ws, $translate.instant('DetailExport'));
                            /** write and download file */

                            XLSX.writeFile(wb, $translate.instant('Report') + '_EHS.xlsx');
                        } else Notification.addError({
                            'status': 'error',
                            'message': $translate.instant('Get no data.')
                        })
                    })


                },
                order: 5
            },{
                title: '🔒' + $translate.instant('Lock/Unlock'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();

                    $scope.ListVoucherLocknUnlock = []; $scope.ListVoucherLocknUnlock = [];
                    $scope.recod.comp_id = resultRows

                    if (resultRows.length > 0) {
                        for (var i = 0; i < resultRows.length; i++) {
                            var item = resultRows[i];
                            var data = {};
                            data.VoucherID = item.VoucherID;
                            data.UserID = Auth.username;
                            $scope.ListVoucherLocknUnlock.push(data);

                        }

                        VoucherService.GetInfoBasic.HSE_LockVoucher({_VoucherLocknUnlock: $scope.ListVoucherLocknUnlock})
                            .$promise.then(function (res) {
                            Notifications.addError({'status': 'info', 'message': res.MSG});
                            setTimeout(function () {
                                $scope.Search();
                            }, 1500);

                        }, function (Error) {

                        })
                    } else {
                        Notifications.addError({
                            'status': 'error',
                            'message': $translate.instant('Select_ONE_MSG')
                        });
                    }
                },
                order: 6
            })
    }
    if($scope.isReportShow){
        gridMenu.push(
            {
                title: '📗 ' + $translate.instant('DetailExport'),
                action: function () {
                    var req = SearchList();
                    VoucherService.ExportXLSData(req, function (res) {
                        console.log(res);
                        if (res.length > 0) {
                            /**Add another header row before adding data */
                            var headArray =
                                [
                                    $translate.instant('STT'),
                                    $translate.instant('VoucherID'),
                                    $translate.instant('IsValuation'),
                                    $translate.instant('WasteName'),
                                    $translate.instant('OwnerComp'),
                                    $translate.instant('ProcessComp'),
                                    $translate.instant('WasteName'),
                                    $translate.instant('ItemCode'),
                                    $translate.instant('Status'),
                                    $translate.instant('Weight'),
                                    $translate.instant('Quantity'),
                                    $translate.instant('Price'),
                                    $translate.instant('UnitPrice'),
                                    $translate.instant('UpdatePricecount'),
                                    $translate.instant('Roots'),
                                    $translate.instant('AreaName'),
                                    $translate.instant('PartsArisingNContractorsArising'),
                                    $translate.instant('Location'),
                                    $translate.instant('CreateTime'),
                                    $translate.instant('VoucherNumber'),
                                    $translate.instant('UserID')
                                ];
                            // res.forEach(function(item, index, arr){
                            //     item.Status =   $translate.instant('Status'+item.Status);
                            //     switch (item.State){
                            //         case 'S': item.State= $translate.instant('Solid'); break;
                            //         case 'L': item.State= $translate.instant('Liquid'); break;
                            //         case 'M': item.State= $translate.instant('Sludge'); break;
                            //     }
                            // } )


                            var ws = XLSX.utils.aoa_to_sheet([headArray]);

                            ws["!cols"] = [{
                                wpx: 30
                            }, {
                                wpx: 80
                            }, {
                                wpx: 80
                            }, {
                                wpx: 80
                            }, {
                                wpx: 150
                            }, {
                                wpx: 150
                            },
                                {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 70
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }, {
                                    wpx: 80
                                }
                            ];
                            ws["!autofilter"] = {ref: "A1:U1"}
                            /** xlsx - add data to file, ingore original header */
                            XLSX.utils.sheet_add_json(ws, res, {skipHeader: true, origin: 'A2'});
                            /** make workbook */


                            var wb = XLSX.utils.book_new()
                            XLSX.utils.book_append_sheet(wb, ws, $translate.instant('DetailExport'));
                            /** write and download file */

                            XLSX.writeFile(wb, $translate.instant('Report') + '_EHS.xlsx');
                        } else Notification.addError({
                            'status': 'error',
                            'message': $translate.instant('Get no data.')
                        })
                    })


                },
                order: 5
            })
        $scope.reloadGrid = true;
    }


}
            /**
             *search list function
             */
            function SearchList() {
                var query = {};
                query.userID = Auth.username;
                query.lang = lang;
                query.dateFrom = $scope.dateFrom || '';
                query.dateTo = $scope.dateTo || '';
                query.VoucherID = '';
                query.VoucherNumber = $scope.voucher_number || '';
                query.ProcessComp = $scope.process_comp || '';
                query.DepartProcess = $scope.depart_process || '';
                query.InternalPhone = '';
                query.DepartReq = $scope.DepartReq || '';
                query.Status = $scope.s_status || '';
                if ($scope.onlyOwner == true)
                    query.isCheck = 1;
                else query.isCheck = 0;
                return query;
            }

            function deleteById(id) {
                var data = {
                    VoucherID: id
                };
                VoucherService.DeleteByVoucherID(data, function (res) {
                        if (res.Success) {
                            $scope.Search();
                            $('#myModal').modal('hide');
                            $('#messageModal').modal('hide');
                            $('#nextModal').modal('hide');
                        } else {
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant('saveError') + res.Message
                            });
                        }

                    },
                    function (error) {
                        Notifications.addError({
                            'status': 'error',
                            'message': $translate.instant('saveError') + error
                        });
                    })
            }

            /**
             *Search function for Button Search
             */
            $scope.Search = function () {
                var deferred = $q.defer();
                if (!$scope.checkErr()) {
                    var deferred = $q.defer();
                    var query = SearchList();
                    query.pageIndex = paginationOptions.pageNumber || '';
                    query.pageSize = paginationOptions.pageSize || '';
                    console.log(query);
                    VoucherService.Search(query, function (res) {
                        $scope.gridOptions.data = res.TableData;
                        $scope.gridOptions.totalItems = res.TableCount[0];

                        //deferred.resolve(data);
                    }, function (error) {
                        deferred.reject(error);
                    })
                }
            }

            var gridMenu = [{
                title: $translate.instant('Create'),
                action: function () {
                    $scope.recod = {};
                    $scope.wasteItems = [];
                    $scope.recod.DateTransfer= $filter('date')(new Date(),'yyyy-MM-dd HH:mm:ss');
                    //$scope.recod.owner_comp = 'DBF1EA58-1326-442B-B4C3-897063F4F7FE';
                    $scope.status = 'N';
                    $scope.lsWastItems = [];
                    // $("#ProcessComp").prop('disabled', false);
                    $("#DateOut").prop('disabled', false);
                    $("#DeparReq").prop('disabled', false);
                    $('#myModal').modal('show');
                },
                order: 1
            }, {
                title: $translate.instant('Update'),
                action: function () {
                    var resultRows = $scope.gridApi.selection.getSelectedRows();
                    $scope.recod.comp_id = resultRows
                    $scope.status = 'M'; //Set update Status

                    if (resultRows.length == 1) {
                        if (resultRows[0].HSE_LockStatus === 'True') {
                            Notifications.addError({
                                'status': 'error',
                                'message': 'The Voucher was lock by HSE staff, can not modify'
                            });
                            return;
                        }
                        if (resultRows[0].Status != 'X' && resultRows[0].Status != 'M') {
                            if (resultRows[0].UserID == Auth.username) {
                                $scope.gd = {};
                                var querypromise = loadVoucherDetail(resultRows[0].VoucherID);
                                $("#ProcessComp").prop('disabled', true); //disable ProcessComp text
                                $("#DateOut").prop('disabled', true);
                                $("#DeparReq").prop('disabled', true);
                                $scope.company = full_lsCompany;
                                querypromise.then(function () {
                                    $('#myModal').modal('show');
                                }, function (error) {
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': error
                                    });
                                })
                            } else {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': $translate.instant('ModifyNotBelongUserID')
                                })
                            }


                        } else {
                            Notifications.addError({
                                'status': 'error',
                                'message': resultRows[0].Status == 'M' ? $translate.instant('Modified_Once') : $translate.instant('Modified_to_X')
                            });
                        }

                    } else {
                        Notifications.addError({
                            'status': 'error',
                            'message': $translate.instant('Select_ONE_MSG')
                        });
                    }
                },
                order: 2
            },
                {
                    title: '🖨️ ' + $translate.instant('PrintReport'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();

                        if (resultRows.length == 1) {
                            var href = '#/waste/Voucher/print/' + resultRows[0].VoucherID;
                            window.open(href);
                        } else {
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant('Select_ONE_MSG')
                            });
                        }
                    },
                    order: 4
                }


            ];
            /**
             * Trigger option changedValue of wasteitem to find method
             */
            $scope.$watch('gd.WasteID', function (n) {
                if (n !== undefined && $scope.gd.WasteID !== null) {
                    var data = full_lsWastItems.filter(x => x.WasteID === n
                )
                    ;
                    if (data.length > 0) {
                        $scope.gd.method_name = data[0].MethodDescription;
                        $scope.gd.waste_name = data[0].WasteDescription;
                    }
                }
            })
            $scope.changedValue = function (item) {
                var data = full_lsWastItems.filter(x => x.WasteID === item
            )
                ;
                if (data.length > 0) {
                    $scope.gd.method_name = data[0].MethodDescription;
                    $scope.gd.waste_name = data[0].WasteDescription;
                }

            }
            /**
             * Add wasteitem in param table (Voucherdetail)
             */
            $scope.addWasteItem = function () {
                if ($scope.gd != null || $scope.gd != {}) {
                    var data = $scope.wasteItems.filter(x => x.waste_name === $scope.gd.waste_name
                )
                    ;

                    if (data.length != 0) {
                        alert($scope.gd.waste_name + ": " + $translate.instant('waste_name_existed'));
                        $scope.gd = {};
                    } else {
                        if ($scope.gd.Quantity < 0 || $scope.gd.Weight <= 0) {
                            alert($scope.gd.waste_name + ": " + $translate.instant('positive_quantity_weight'));
                            $scope.gd.Quantity = null;
                            $scope.gd.Weight = null;
                        } else {
                            $scope.wasteItems.push($scope.gd);
                            $scope.gd = {};
                        }
                    }
                }
            };
            /**Modal: detail slice WasteItem from list */
            $scope.deleteWasteItem = function (index) {
                $scope.wasteItems.splice(index, 1);

            };
            $scope.clear = function () {
                $scope.recod = {};

                $('#myModal').modal('hide');
            }

            /**Choose process to show Waste Items */
            $scope.processcomp_reupdate_wastelist = function () {

                $scope.gd = {};
                if ($scope.recod.process_comp == null || $scope.recod.process_comp == '') return;
                WasteItemService.GetWasteByCompany({comp: $scope.recod.process_comp, lang: lang}, function (res) {
                    if (res.length > 0)
                        $scope.lsWastItems = res.filter(x => x.CompOriginID ===$scope.recod.process_comp && x.WasteType === $scope.recod.WasteType && x.Source === $scope.recod.Roots) ;
                else
                    $scope.lsWastItems = [];
                })
                $scope.wasteItems = [];
            };
            $scope.processcomp_reupdate_wastelist_bywastetype = function (wastetype) {

                $scope.gd = {};
                if (wastetype == null || wastetype == '') return;
                WasteItemService.GetWasteByCompany({comp: $scope.recod.process_comp || '', lang: lang}, function (res) {
                    if (res.length > 0)
                        $scope.lsWastItems = res.filter(x => x.CompOriginID === $scope.recod.process_comp && x.WasteType === wastetype  && x.Source === $scope.recod.Roots);
                else
                    $scope.lsWastItems = [];
                })

            };
            $scope.processcomp_reupdate_wastelist_byArea = function (Area) {

                $scope.gd = {};
                if (Area == null || Area == '') return;
                WasteItemService.GetWasteByCompany({comp: $scope.recod.process_comp || '', lang: lang}, function (res) {
                    if (res.length > 0)
                        $scope.lsWastItems = res.filter(x => x.CompOriginID === $scope.recod.process_comp && x.WasteType === $scope.recod.WasteType  && x.Source === $scope.recod.Roots);

                else
                    $scope.lsWastItems = [];
                })

            };
            $scope.processcomp_reupdate_wastelist_byRoots = function (Roots) {

                $scope.gd = {};
                if (Roots == null || Roots == '') return;
                WasteItemService.GetWasteByCompany({comp: $scope.recod.process_comp || '', lang: lang}, function (res) {
                    if (res.length > 0)
                        $scope.lsWastItems = res.filter(x => x.CompOriginID === $scope.recod.process_comp && x.WasteType === $scope.recod.WasteType  && x.Source === Roots);

                else
                    $scope.lsWastItems = [];
                })

            };
            /**check erorr before search */
            $scope.checkErr = function () {
                var startDate = $scope.dateFrom;
                var endDate = $scope.dateTo;
                $scope.errMessage = '';
                if (new Date(startDate) > new Date(endDate)) {
                    $scope.isError = true;
                    $scope.errMessage = 'End Date should be greater than Start Date';
                    Notifications.addError({
                        'status': 'error',
                        'message': $scope.errMessage
                    });
                    return true;
                }
                return false;
            };

            /** date check example
             $scope.dateCheck = function (DO) {
                            if ($filter('date')(new Date(DO), 'MM/dd/yyyy') < $filter('date')(new Date(), 'MM/dd/yyyy')) {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant('DateOut_check_ISG') });
                                $scope.recod.date_out = '';
                                return false;
                            }
                            else return true;

                        }


             */


        }])
})
