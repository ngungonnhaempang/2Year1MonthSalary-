define(['myapp', 'angular', 'jszip', 'xlsx'], function (myapp, jszip) {
    myapp.controller('QualifiedController', ['$scope', '$filter', '$compile', '$routeParams', '$resource', '$location',
        'i18nService', 'Notifications', 'Auth', 'uiGridConstants', '$http',
        '$translatePartialLoader', '$translate', 'LIMSBasic', 'LIMSService', 'EngineApi', '$timeout', 'GateGuest','$q',
        function ($scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications,
                  Auth, uiGridConstants, $http, $translatePartialLoader,
                  $translate, LIMSBasic, LIMSService, EngineApi, $timeout, GateGuest, $q,xlsx) {
            /** INIT */
            var username = Auth.username;
            $scope.flowkey = 'QCOverGrade';
            $scope.QCNode = 'lab_leadercheck';
            var lang = window.localStorage.lang || 'EN';
            var startDate = moment(new Date()).date(1).format('YYYY-MM-DD');
            var endDate =  $filter('date')(new Date(), 'yyyy-MM-dd');
            var paginationOptions = {
                pageNumber: 1,
                pageSize: 100,
                sort: null
            };
            $scope.Report_ISO_Detail={};

            $scope.note = {};
            $scope.lang = lang;
            $scope.dateFrom = $scope.dateBegin = startDate;
            $scope.dateFrom = '2018-01-01';
            $scope.dateTo = $scope.dateEnd = endDate;

            $scope.onlyOwner = false;
            $scope.modalWidth = 80;
           // $scope.isValid =true;
            var q_category = {
                userid: Auth.username,
                Language: $scope.lang
            };
            /**GET BASIC */
            LIMSBasic.GetSamples({
                userid: Auth.username,
                query: ''
            }, function (data) {
                console.log(data);
                if (data.length > 0) {
                    $scope.sampleList = data.filter((el) => {
                        return (el.TypeID == '2' || el.TypeID == '4'||el.TypeID == '3' || el.TypeID == '5')
                    })
                    console.log($scope.sampleList);
                }
            });



            function GetCategorys() {
                var deferred = $q.defer();
                LIMSBasic.GetCategorys(q_category, function (data) {
                    console.log(data)
                    deferred.resolve(data);

                    //$scope.note.TypeID = 2;

                });
                return deferred.promise;
            }

            function getStatus() {
                var deferred = $q.defer();
                LIMSBasic.GetStatus({
                    ctype: 'Grade',
                    lang: lang
                }, function (data) {

                    deferred.resolve(data)
                });
                return deferred.promise;
            }
            function getVendor() {
                var deferred = $q.defer();
                LIMSBasic.getVendor({
                    Language: $scope.lang,
                    Fromtime: $scope.dateBegin,
                    Totime: $scope.dateEnd
                }, function (res) {

                        deferred.resolve(res);
                });
                return deferred.promise;
            }
            $q.all([getVendor(),getStatus(),GetCategorys()]).then(function (result) {
                $scope.VendorList = result[0];
                $scope.StatusList =  result[1];
                $scope.CategoryList = result[2].filter(x => x.TypeID == 2||x.TypeID == 3 || x.TypeID == 5);
            })

            /**UI-GRID INIT */
            var col = [{
                    field: 'VoucherID',
                    displayName: $translate.instant('VoucherID'),
                    minWidth: 100,
                    cellTooltip: true,
                    cellTemplate: '<a ng-click="grid.appScope.toDetail(row.entity.VoucherID,row.entity.SampleName,row.entity.Status)" style="padding:5px;display:block; cursor:pointer" target="_blank">{{COL_FIELD}}</a>'
                },
                {
                    field: 'SampleName',
                    displayName: $translate.instant('SampleName'),
                    minWidth: 100,
                    cellTooltip: true
                    // cellTemplate: '<span >{{grid.appScope.getSampleName(row.entity.SampleName)}}</span>'
                },
                {
                    field: 'Line',
                    displayName: $translate.instant('Line'),
                    minWidth: 30,
                    cellTooltip: true
                },
                {
                    field: 'LOT_NO',
                    displayName: $translate.instant('Material'),
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'Status',
                    displayName: $translate.instant('Status'),
                    minWidth: 70,
                    cellTooltip: true,
                    cellTemplate: '<span >{{grid.appScope.getStatus(row.entity.Status)}}</span>'
                },
                {
                    field: 'ColorLabel',
                    displayName: $translate.instant('ColorLabel'),
                    minWidth: 100,
                    cellTooltip: true,
                    cellTemplate: '<span style="text-align:center; font-size:16pt;">■</span>',
                    cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                        if (grid.getCellValue(row, col) === 'Red' || grid.getCellValue(row, col) === 'RED') {
                            return 'red';
                        } else return 'yellow'
                    },
                },
                {
                    field: 'CreateBy',
                    displayName: $translate.instant('CreateBy'),
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'CreateDate',
                    displayName: $translate.instant('CreateDate'),
                    minWidth: 130,
                    cellTooltip: true
                },
                {
                    field: 'BeginDate',
                    displayName: $translate.instant('BeginDate'),
                    minWidth: 90,
                    cellTooltip: true
                },
                {
                    field: 'EndDate',
                    displayName: $translate.instant('EndDate'),
                    minWidth: 90,
                    cellTooltip: true
                },
                {
                    field: 'Stamp',
                    displayName: $translate.instant('Last Modify Date'),
                    minWidth: 130,
                    cellTooltip: true
                },
            ];
            $scope.gridOptions = {
                columnDefs: col,
                data: [],
                enableColumnResizing: true,
                enableFullRowSelection: true,
                enableSorting: true,
                showGridFooter: true,
                gridFooterTemplate: '<div class="mygridFooter"><b>Total: {{grid.appScope.Total}} items </b></div>',
                // showColumnFooter: true,
                enableGridMenu: true,
                exporterMenuPdf: false,
                enableSelectAll: false,
                enableRowHeaderSelection: true,
                enableRowSelection: true,
                multiSelect: false,
                paginationPageSizes: [50, 100, 200, 500],
                paginationPageSize: 50,
                useExternalPagination: true,
                enablePagination: true,
                enablePaginationControls: true,
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    EngineApi.getTcodeLink().get({
                        userid: Auth.username,
                        tcode: $scope.flowkey
                    }, function (linkres) {
                        if (linkres.IsSuccess) {
                            gridApi.core.addToGridMenu(gridApi.grid, gridMenu_flow);
                        }
                        gridApi.core.addToGridMenu(gridApi.grid, gridMenu_commonuser);
                    });
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        $scope.selectedVoucherid = row.entity.VoucherID;
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        paginationOptions.pageNumber = newPage;
                        paginationOptions.pageSize = pageSize;
                        $scope.Search();
                    });
                }
            };
            var gridMenu_flow = [{
                    title: $translate.instant('Create'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            $scope.sampleName = resultRows[0].SampleName;
                            $scope.material = resultRows[0].LOT_NO;
                            $scope.colorlabel = resultRows[0].ColorLabel;
                            $scope.dateBegin = resultRows[0].BeginDate;
                            $scope.dateEnd = resultRows[0].EndDate;
                            $scope.line = resultRows[0].Line;
                        } else {
                            $scope.dateBegin = $scope.dateFrom
                            $scope.dateBegin_change($scope.dateFrom);
                        }
                        $('#myModal').modal('show');
                    },
                    order: 1
                },
                {
                    title: $translate.instant('Delete'),
                    action: function ($event) {
                        var selectRows = $scope.gridApi.selection.getSelectedGridRows();
                        if (selectRows.length > 0) {
                            var VoucherID = selectRows[0].entity.VoucherID;
                            var Status = selectRows[0].entity.Status;

                            if (Status != 'N') {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': 'Can not delete this voucher ' + VoucherID
                                });
                                return;
                            }
                            if (confirm('Delete this VoucherID: ' + VoucherID + '?')) {
                                LIMSService.UpdateRYStatusVoucher({
                                    voucherID: VoucherID,
                                    status: 'X',
                                    userid: username
                                }, function (req) {
                                    if (req.Success) {
                                        Notifications.addMessage({
                                            'status': 'info',
                                            'message': 'Delete success'
                                        });

                                            $scope.Search();
                                    }
                                }, function (errResponse) {
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': 'Can not delete because : ' + errResponse.data.Message
                                    });
                                });
                            }
                        } else {
                            Notifications.addMessage({
                                'status': 'error',
                                'message': 'Please Select Row'
                            });
                        }
                    },
                    order: 2
                }


            ];
            var gridMenu_commonuser = [{
                    title: $translate.instant('PrintQualifed'),
                    action: function () {
                        if (!checkSelectedRow()) return;
                        var resultRows = $scope.gridApi.selection.getSelectedRows();

                        var href = "#/LIMS/QualifiedControl/print/" + resultRows[0].VoucherID;
                        window.open(href);
                    },
                    order: 4
                },
                {
                    title: $translate.instant('InProcess'),
                    action: function () {
                        if (!checkSelectedRow()) return;
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            var ID = resultRows[0].VoucherID
                            LIMSService.QCOverGradePID().get({
                                OverID: ID
                            }).$promise.then(function (res) {
                                console.log(res);
                                if (res) {
                                    window.open('#/processlog/' + res.ProcessInstanceId);
                                }
                            }, function (err) {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': err.data
                                });
                            })
                        }
                    },
                    order: 5
                },
                {
                    title: $translate.instant('PrintUQRedVoucher'),
                    action: function () {
                        if (!checkSelectedRow()) return;
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        var href = '#/LIMS/QualifiedControl/printRedUQ/' + resultRows[0].VoucherID;
                        window.open(href);
                    },
                    order: 6
                },
                // {
                //     title: $translate.instant('DetailExport'),
                //     action: function () {
                //         var query = SearchList();
                //         LIMSService.ISOQualify.DetailExport(query, function (res) {
                //             if (res) {
                //                 console.log(res);
                //                 var ws = XLSX.utils.json_to_sheet(res);
                //                 var wb = XLSX.utils.book_new();
                //                 XLSX.utils.book_append_sheet(wb, ws, 'downloadQC');
                //                 XLSX.writeFile(wb, 'downloadQC2.xls');
                //             }
                //         })


                //     },
                //     order: 8
                // },
                {
                    title: $translate.instant('printAllUQ'),
                    action: function () {
                        var href = '#/LIMS/QualifiedControl/printAllUQ/' + $scope.dateFrom + '&' + $scope.dateTo;
                        window.open(href);
                    },
                    order: 8
                },
            ];
            /**CLOSE BUTTON */
            $scope.Close = function () {
                $('#myModal').modal('hide');
                $('#DetailModal').modal('hide');

            };
            /**SEARCH BUTTON*/
            $scope.Reset = function () {
                $scope.voucherid = '';
                $scope.sampleName = '';
                $scope.material = '';
                $scope.colorlabel = '';
                $scope.dateFrom = '';
                $scope.dateTo = '';
                $scope.onlyOwner = '';
                $scope.status = '';
                $scope.note.line = '';
                $scope.note.vendor='';
            }

            function SearchList() {
                var params = {};
                params.voucherid = $scope.voucherid || '';
                params.sampleName = $scope.sampleName || '';
                params.LOT_NO = $scope.material || '';
                params.colorlabel = $scope.colorlabel || '';
                params.B = $scope.dateFrom || '';
                params.E = $scope.dateTo || '';
                params.userID = $scope.onlyOwner ? Auth.username : '';
                params.status = $scope.status || '';
                params.Line = $scope.line || '';
                params.materialType= $scope.note.TypeID||'';
                return params;
            }
            $scope.Search = function () {
                var query = SearchList();
                $scope.Total = 0;
                if ($scope.onlyOwner == true) {
                    $scope.Owner = Auth.username;
                } else {
                    //$scope.Owner = '';
                    $scope.Owner = $scope.department;
                }
                LIMSService.ISOQualify.SearchRYVouchers(query, function (res) {
                    $scope.Total = res.length;
                    $scope.gridOptions.data = res;

                });

            };
            /**************************FUNCTIONS OF WATCHED AND CHANGED VALUES AND CONDITIONS *****************************************/
            $scope.$watch('sampleName', function (n) {
                if (n !== undefined && $scope.sampleName !== null) {
                 var data=    $scope.sampleList.filter((data)=>{
                        return (data.SampleName== n)

                });

                   // $scope.AB=;
                    LIMSBasic.GetLinesByAB({
                        userid: Auth.username,
                        sampleName: '',
                        ab: data[0].AB||''
                    },function (res) {
                        $scope.LinesList = res;
                    });
                    LIMSBasic.GetMaterial({
                        userid: Auth.username,
                        sampleName: $scope.sampleName,
                        query: '0'
                    }, function (res) {
                        $scope.materialList = res;

                    });

                    LIMSBasic.getVendor({
                        Language: $scope.lang,
                        Fromtime: $scope.dateBegin,
                        Totime: $scope.dateEnd
                    },function (res) {

                        $scope.VendorList=res;
                    });


                }
                // $scope.material=undefined;
                // $scope.vendor =undefined;
                // $scope.line =undefined;
                // $scope.isValid =true;

            })



            //UI for query
            $scope.$watch('note.TypeID', function (newValue, oldValue) {

                if (newValue != null) {
                    var q_sample = {
                        userid: Auth.username,
                        TypeID: $scope.note.TypeID
                    };
                    LIMSBasic.GetSamplesByCategory(q_sample, function (data) {
                        //console.log(data)
                        $scope.SampleList = data;
                        $scope.note.SampleName = '';
                    });
                }

            });
            $scope.dateBegin_change = function (date) {
                var d =$filter('date')(new Date(), 'yyyy-MM-dd');
                // d.setDate(d.getDate()+1);
                $scope.dateEnd = moment(d).add(+1, 'days').format('YYYY-MM-DD');
            }
            $scope.getStatus = function (Status) {
                var statLen = $filter('filter')($scope.StatusList, {
                    'State': Status
                });
                if (statLen.length > 0) {
                    return statLen[0].StateSpec;
                } else {
                    return Status;
                }
            };

            function checkSelectedRow() {
                var resultRows = $scope.gridApi.selection.getSelectedRows();
                if (resultRows.length == 1) {
                    return true;
                } else {
                    Notifications.addError({
                        'status': 'error',
                        'message': $translate.instant('Please choose row')
                    });
                    return false;
                }

            }
            /*********************************COMMON FUNCTION *************************************/


            /**get Information of next Candidate */
            function getGateCheck(samplename) {

                $scope.checkList = [];
                $scope.leaderlist = [];
                GateGuest.GetGateCheckers().OS_CheckerOverGrade({
                    owner: username,
                    kinds: samplename || '',
                    QCNode: $scope.QCNode || ''
                }).$promise.then(function (leaderlist) {
                    if (leaderlist.length > 0) {
                        var checkList = [];
                        for (var i = 0; i < leaderlist.length; i++) {
                            checkList[i] = leaderlist[i].Person;
                        }
                        $scope.checkList = checkList;
                        $scope.leaderlist = leaderlist;
                        return true;
                    };
                    return false;

                }, function (errormessage) {
                    console.log(errormessage);
                    return false;
                })

            }


            /********************************SHOW DETAIL MODALS***************************************************** */
                $scope.toDetail = function (parram_VoucherID, parram_Samplename, parram_status) {
                /** 1- GET RECEIVERS */
                    $scope.receiver = [];
                $scope.propertyName = {};
                $scope.Kind='';
                /** 2- GET DETAIL */
                LIMSService.ISOQualify.GetDetailReport({
                    voucherID: parram_VoucherID
                }, function (data) {
                    var plansHeader = [];
                    $scope.plansHeader = [];
                    if (data.length > 0) {
                        $scope.recod = {};
                        $scope.UQList = data;
                        var plansHeader = [];
                        for (var key in data[0]) {

                            if (['VoucherID', 'ColorLabel', 'State', 'VoucherNO',
                                    'CreateDate', 'BeginDate', 'EndDate', 'Stamp', 'Status',
                                    'Reason', 'Solution', 'Prevention', 'Remark', 'CreateBy',
                                    'SampleName', 'LINE', 'LOT_NO','DepartmentID','Vendor',
                                    'Description_CN','Description_EN','Specification_EN','Specification_CN',
                                    'TypeID','Group_name'

                                ].indexOf(key) < 0 && key.indexOf('$') < 0 ) {
                                if(data[0].Status === 'N'){
                                    if(parram_Samplename ==='S03020001'  && data[0].ColorLabel.toLowerCase() ==='red' && key!=='TotalBag'){

                                        plansHeader.push(key);

                                    }else{
                                        if( key!=='TotalBag'&&key!=='RangeBarCode'){
                                            plansHeader.push(key);
                                        }

                                    }
                                }else
                                {
                                    if(parram_Samplename ==='S03020001'  && data[0].ColorLabel.toLowerCase() ==='red'){

                                        plansHeader.push(key);

                                    }else{
                                        if( key!=='RangeBarCode' && key!=='TotalBag'){
                                            plansHeader.push(key);

                                        }

                                    }

                                }


                            }
                        }
                        /** Information of Red or Yellow Voucher*/
                        if (data[0].ColorLabel == 'Red') {
                            if(data[0].SampleName !== 'S03020001'){
                                $scope.isRed = true;
                                $scope.color = {
                                    'TW': '紅',
                                    'VN': 'ĐỎ',
                                    'ISO': '5VGAAQR140-01', //this ISO number should be stored in Database (they are developing)...
                                    'Solution': data[0].Solution
                                };
                            }else{
                                $scope.isRed = true;
                                $scope.color = {
                                    'TW': '紅',
                                    'VN': 'ĐỎ',
                                    'ISO': '5SGAAQR140-01', //this ISO number should be stored in Database (they are developing)...
                                    'Solution': data[0].Solution
                                };
                            }



                        } else {
                            if(data[0].SampleName !== 'S03020001') {
                                $scope.color = {
                                    'TW': '黃',
                                    'VN': 'VÀNG',
                                    'ISO': '5VGAAQR141-01', //...as well as this number
                                    'Solution': data[0].Solution
                                };

                            }else{
                                $scope.color = {
                                    'TW': '黃',
                                    'VN': 'VÀNG',
                                    'ISO': '5SGAAQR141-01', //...as well as this number
                                    'Solution': data[0].Solution
                                };
                            }
                            $scope.isRed = false; // to define which Color
                        }

                        /// module USER IN DEPART
                        $scope.plansHeader = plansHeader;
                        if (plansHeader.length > 10) $scope.modalWidth = 100;
                        else $scope.modalWidth = 80;
                        $scope.recod = data[0];
                        if($scope.recod.SampleName ==='S03020001' ){

                            $scope.recod.LINE= $scope.recod.Group_name;
                               $scope.isStableVoucher=  ($scope.recod.Status === 'N') ? true : false;
                        }else
                        {
                            $scope.isStableVoucher=false;

                        }
                        $scope.isShow = ($scope.recod.Status === 'N') ? true : false; //show submit button (printQualifed)
                        $scope.recod.showSampleName =   $scope.SampleList.filter((data)=>{
                            return (data.SampleName== $scope.recod.SampleName)

                        });
                        if( $scope.recod.showSampleName.length >0){

                            if(lang=='EN'||lang=='VN'){
                                $scope.DepartmentSpec =$scope.recod.Specification_EN;
                                $scope.showSampleName =$scope.recod.showSampleName[0]['Description_EN'];
                            }else{
                                $scope.DepartmentSpec =$scope.recod.Specification_CN;
                                $scope.showSampleName =$scope.recod.showSampleName[0]['Description_CN'];
                            }

                        }


                        if($scope.note.TypeID=='5' ||$scope.note.TypeID=='3'){
                            $scope.Kind=$scope.recod.DepartmentID;
                        }else{
                            $scope.Kind=$scope.recod.SampleName;
                        }
                        if (parram_status != 'N') {
                            LIMSService.QCOverGradePID().get({
                                OverID: parram_VoucherID
                            }).$promise.then(function (res) {
                                console.log(res);
                                if (res) {
                                    EngineApi.getProcessLogs.getList({
                                        "id": res.ProcessInstanceId,
                                        "cId": ""
                                    }, function (data) {
                                        console.log(data[0].Logs);
                                        data.forEach(function (value, index) {
                                            if (index >= 1)
                                                data[0].Logs.push.apply(data[0].Logs, data[index].Logs)
                                        })
                                        var receiver = [];
                                        var taf = TAFFY(data[0].Logs);
                                        receiver[0] = taf({
                                            TaskName: "起始表单"
                                        }).first(); //initiator
                                        receiver[1] = taf({
                                            TaskName: "Production"
                                        }).first(); //Product team
                                        if ($scope.recod.Status.indexOf(['S', 'X'] >= 0)) { //(if published)
                                            receiver[3] = taf({
                                                TaskName: "Production leader",
                                            }).start(1).first(); //QCleader
                                            receiver[2] = taf({
                                                TaskName: "Production leader",
                                            }).start(2).first(); //QCmanager
                                        }
                                        receiver[4] = taf({
                                            TaskName: "ISO"
                                        }).first(); //ISO team
                                        $scope.receiver = receiver;
                                        console.log(receiver);
                                    })
                                }
                            },function (error) {
                                console.log(error);
                                Notifications.addError({
                                    'status': 'error',
                                    'message': error.data
                                });
                            })

                        } else if (getGateCheck( $scope.Kind)) {
                            Notifications.addError({
                                'status': 'error',
                                'message': 'Error on getting data'
                            });
                        }


                        $('#DetailModal').modal('show'); //SHOW IF GET DATA SUCCESS
                    } else $timeout(() => {
                        Notifications.addErorr({
                            'status': 'error',
                            'message': 'There is no data or error on getting data'
                        });
                    }, 2000);
                });



            }

            /*****************************SAVE BUTTON **************************************/
            function saveInitNote() {
                var note = {};
                note.SampleName = $scope.sampleName || '';
                note.LOT_NO = $scope.material || '';
                note.ColorLabel = $scope.colorlabel || '';
                note.BeginDate = $scope.dateBegin || '';
                note.EndDate = $scope.dateEnd || '';
                note.CreateBy = Auth.username;
                note.Line = $scope.note.line || '';
                note.Status = 'N';
                note.Vendor = $scope.note.vendor||'';
                return note;
            }
            $scope.Save = () => {
                var note = saveInitNote();
                LIMSService.ISOQualify.CreateVoucher(note).$promise.then(function (res) {
                    if (res.Success.length > 0) {
                        Notifications.addMessage({
                            'status': 'info',
                            'message': 'Create ' + note.ColorLabel + ' ' + res.Success[0].VoucherID + ' successed!'
                        });
                        $timeout(() => {
                            $scope.Search();
                        }, 2000);
                    } else {
                        Notifications.addError({
                            'status': 'error',
                            'message': 'Can not create ' + note.LOT_NO + ' ' + note.ColorLabel + ' it not exist'
                        });
                    }
                });
                $scope.colorlabel = '';
                $scope.dateFrom = $scope.dateBegin;
                $scope.dateTo = $scope.dateEnd;
                $('#myModal').modal('hide');


            }
            /*****************************SAVE/SUBMIT BUTTON**************************************/
            $scope.SaveSubmit = function (isCreated) {
                var formVariables = [];
                var historyVariable = [];

                /**Check if user have permission to submit */
                EngineApi.getTcodeLink().get({
                    userid: Auth.username,
                    tcode: $scope.flowkey
                }, function (linkres) {
                    if (linkres.IsSuccess) {


                        formVariables.push({
                            name: 'LableadercheckArray',
                            value: $scope.checkList
                        }); //initiator --> LableadercheckArray --> EnginerArray -> QCManagerList
                        historyVariable.push({
                            name: 'workflowkey',
                            value: $scope.flowkey
                        });
                        //Voucher has not created yet, then create.
                        /**Save and Submit Button */
                        if (confirm('Would you like save and submit this Voucher?')) {
                            if (isCreated) {
                                /** Save  */
                                var note = saveInitNote();
                                LIMSService.ISOQualify.CreateVoucher(note).$promise.then(function (res) {
                                    if (res.Success.length > 0) {
                                        var newVoucherID = res.Success[0].VoucherID;
                                        formVariables.push({
                                            name: 'OverID',
                                            value: newVoucherID
                                        });
                                        /**Submit */
                                        SubmitAndChangeStatus(newVoucherID);
                                    } else {
                                        Notifications.addError({
                                            'status': 'error',
                                            'message': 'Voucher doesn\'t exist data. It wont be create and submit.'
                                        });
                                    }
                                })
                            } else {
                                /**Submit Button */
                                formVariables.push({
                                    name: 'OverID',
                                    value: $scope.recod.VoucherID
                                });
                                LIMSService.ISOQualify.GetNewRYVoucher({
                                    voucherid: $scope.recod.VoucherID
                                }, function (res) {
                                    if (res.Success) {
                                        SubmitAndChangeStatus($scope.recod.VoucherID);
                                    } else
                                        alert('This voucher has been submit befored!');
                                })
                            }
                        }


                    } else alert("You don't have permission!")
                });

                function SubmitAndChangeStatus(voucherid) {
                    /**Submit to BPMN */
                    $scope.Report_ISO_Detail.ListProperty = [];
                    var list={};

                    for (var key in $scope.propertyName) {
                        list.PropertyName=key;
                        list.PropertyValue = $scope.propertyName[key];
                        $scope.Report_ISO_Detail.ListProperty.push(list);
                        list={};
                    }
                    $scope.Report_ISO_Detail.voucherID=voucherid;
                    $scope.Report_ISO_Detail.status= 'P';
                    $scope.Report_ISO_Detail.UserID =username;

                    LIMSService.SubmitBPM($scope.flowkey, formVariables, historyVariable, '', function (res, message) {
                        if (message) {
                            alert('Can not Submit this voucher because : ' + message);
                        } else {
                            /**Save to Server and change status */
                            LIMSService.ISOQualify.UpdateSubmittedVoucher( $scope.Report_ISO_Detail).$promise.then(function (res)  {
                                if (res.Success) {
                                    var reminder_parrams = {
                                        voucherID: voucherid,
                                        userid: Auth.username,
                                        formkey: 'start_processes'
                                    }
                                    debugger;
                                    LIMSService.SendReminder(reminder_parrams, function (res) {
                                        if (res.Success) return;
                                    }, function (error) {
                                        Notifications.addError({
                                            'status': 'error',
                                            'message': $translate.instant('saveError') + error
                                        });
                                    })
                                    Notifications.addMessage({
                                        'status': 'info',
                                        'message': 'Your voucher ' + voucherid + ' is submited!'
                                    });
                                    $('#DetailModal').modal('hide');
                                    $timeout(() => {
                                        $scope.Search();
                                }, 2000);
                                }
                            }, function (err) {
                                Notifications.addError({
                                    'status': 'error',
                                    'message': 'Save Error' + err
                                });
                            })
                        }
                    })
                    /** Change Status to P */

                }


            }






        }
    ]);
});
