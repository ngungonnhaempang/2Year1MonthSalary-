define(['myapp', 'angular', 'higthchartexport'], function (myapp, angular) {
    myapp.controller('StapleOverGradeController', ['$scope', '$filter', '$http', '$q', '$compile', '$routeParams', '$resource', '$location', 'i18nService',
        'Notifications', 'Auth', '$translate', 'LIMSService', 'LIMSBasic','$interval','$timeout',
        function ($scope, $filter, $http, $q, $compile, $routeParams, $resource, $location, i18nService, Notifications, Auth,
                  $translate, LIMSService, LIMSBasic,$interval,$timeout) {

            $scope.isRun = false;
            var lang = window.localStorage.lang || 'EN';
            $scope.lang = lang;
            $scope.reloadGrid = false;
            var obj = $('#mychart');
            //Default test case --------------------
            $scope.dateFrom = moment(new Date()).subtract(1, 'months').date(1).format('YYYY-MM-DD');
            $scope.dateTo = moment(new Date()).date(1).subtract(1, 'days').format('YYYY-MM-DD');
            $scope.SampleName = 'S03020001';
            //----------------------------------------

            $scope.ListStatus =[
                                {ID:'I',Name:$translate.instant('In Stock')}
                                ,{ID:'S',Name:$translate.instant('Selected')}
                               ];
            $scope.Status='I';
            $scope.GradeList =[
                {ID:'A',Name:'A'},
                {ID:'B',Name:'B'},
                {ID:'C',Name:'C'}
                ];


            /** SET HIGHTCHART OPTIONS SETTING */
            Highcharts.setOptions({
                global: {
                    useUTC: false
                }
            });

            var options = {
                chart: {
                    // renderTo: 'container',
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.2f}%</b>'
                },
                // colors: ['#058DC7', '#50B432', '#ED561B', '#DDDF00', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],

                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b style="color:{point.labelcolor};font-weight: bold;"> {point.name} : {point.percentage:.2f} %</b>',
                        },
                        showInLegend: true
                    }
                },
                series: [{
                    name: 'Total',
                    colorByPoint: true,
                    // data: [
                    //     {
                    //         name: 'CB-612',
                    //         y: 31.81,
                    //     }, {
                    //         name: 'CB-602',
                    //         y: 15.99
                    //     }
                    // ]
                }]

            }
            function getSamples(){
                var deferred = $q.defer();

                LIMSBasic.GetSamples({ userid: Auth.username, query: '5' }, function (res) {
                    if(res){
                        var data = res.filter(x=>x.SampleName===$scope.SampleName);
                        LIMSBasic.GetLinesByAB({
                            userid: Auth.username,
                            sampleName: $scope.SampleName,
                            ab: data[0].AB
                        }, function (result) {
                            $scope.LinesList = result;
                        });
                        deferred.resolve(res);
                    }


                },function (err) {
                    deferred.reject(err);
                });
                return deferred.promise;
            }
            $q.all([getSamples()]).then(function (result) {
                $scope.sampleList = result[0].filter((el) => {
                    return (el.SampleName==='S03020001');
            });

            })

            $scope.$watch('SampleName', function (n) {
                if (n !== undefined) {
                    if($scope.sampleList!=null && $scope.sampleList!=undefined){
                        var data = $scope.sampleList.filter(x=>x.SampleName===n);
                        LIMSBasic.GetAttribute({
                            sampleName: $scope.SampleName
                        }, function (res) {
                            if (res.length == 0) {
                                $scope.note.attribute = '';
                            }
                            $scope.Attributs = res;
                        });
                        LIMSBasic.GetLinesByAB({
                            userid: Auth.username,
                            sampleName: $scope.SampleName,
                            ab: data[0].AB
                        }, function (res) {
                            $scope.LinesList = res;
                        });
                    }

                }

            });
            var col = [

                {
                    field: 'Group',
                    displayName: $translate.instant('Group')
                    , minWidth: 80, cellTooltip: true
                },
                {
                    field: 'MaterialNO',
                    displayName: $translate.instant('MaterialNO'),
                    enableCellEdit: false,
                    minWidth: 120,
                    cellTooltip: true
                },
                {
                    field: 'BarCode',
                    displayName: $translate.instant('BarCode'),
                    enableCellEdit: false,
                    minWidth: 140,
                    cellTooltip: true
                },
                {
                    field: 'ProdSpec',
                    displayName: $translate.instant('ProdSpec'),
                    enableCellEdit: false,
                    minWidth: 180,
                    cellTooltip: true
                },
                {
                    field: 'Batch',
                    displayName: $translate.instant('Batch'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'ProdDate',
                    displayName: $translate.instant('ProdDate'),
                    enableCellEdit: false,
                    minWidth: 180,
                    cellTooltip: true
                },
                {
                    field: 'State',
                    displayName: $translate.instant('State'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'Grade',
                    displayName: $translate.instant('Grade'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'Line',
                    displayName: $translate.instant('Line'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true
                }
            ];
            var colReport = [


                {
                    field: 'MaterialNO',
                    displayName: $translate.instant('MaterialNO'),
                    enableCellEdit: false,
                    minWidth: 120,
                    cellTooltip: true
                },
                {
                    field: 'ProdSpec',
                    displayName: $translate.instant('ProdSpec'),
                    enableCellEdit: false,
                    minWidth: 180,
                    cellTooltip: true
                },
                {
                    field: 'LINE',
                    displayName: $translate.instant('Line'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'group_name',
                    displayName: $translate.instant('Group')
                    , minWidth: 80, cellTooltip: true
                },
                {
                    field: 'Total',
                    displayName: $translate.instant('Total'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'TotalGradeNotA',
                    displayName: $translate.instant('TotalGradeNotA'),
                    enableCellEdit: false,
                    minWidth: 180,
                    cellTooltip: true
                },
                {
                    field: '%GradeA',
                    displayName: $translate.instant('%GradeA'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true
                }
            ];
            $scope.gridOptions = {
                columnDefs: col,
                data: [],
                multiSelect: true,
                enableColumnResizing: true,
                enableColumnResize: true,
                enableCellEditOnFocus: true,
                enableSorting: true,
                showGridFooter: true,
                enableGridMenu: true,
                exporterMenuPdf: false,
                enableSelectAll: true,
                enableRowHeaderSelection: true,
                enableRowHashing: false,
                enableRowSelection: true,
                paginationPageSizes: [50, 100, 200, 500],
                paginationPageSize: 50,
                exporterOlderExcelCompatibility: false,
                useExternalPagination: true,
                enablePagination: true,
                enablePaginationControls: true,
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $interval( function() {
                        gridApi.core.addToGridMenu(gridApi.grid, gridMenu);
                    }, 0, 1);
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        $scope.selectedVoucherid = row.entity.VoucherID;
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        paginationOptions.pageNumber = newPage;
                        paginationOptions.pageSize = pageSize;
                        $scope.Search();
                    });

                },

                rowTemplate: rowTemplate()

            };
            $scope.ModifyGrid = {
                columnDefs: col,
                data: [],
                multiSelect: true,
                enableColumnResizing: true,
                enableColumnResize: true,
                enableCellEditOnFocus: true,
                enableSorting: true,
                showGridFooter: true,
                enableGridMenu: false,
                exporterMenuPdf: false,
                enableSelectAll: true,
                enableRowHeaderSelection: true,
                enableRowHashing: false,
                enableRowSelection: true,
                paginationPageSizes: [50, 100, 200, 500],
                paginationPageSize: 50,
                exporterOlderExcelCompatibility: false,
                useExternalPagination: true,
                enablePagination: true,
                enablePaginationControls: true,
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;

                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        $scope.selectedVoucherid = row.entity.VoucherID;
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        paginationOptions.pageNumber = newPage;
                        paginationOptions.pageSize = pageSize;

                    });

                },

                rowTemplate: rowTemplate()

            };

            var getPage = function () {
                var query = {};

                    query.pageIndex = paginationOptions.pageNumber || '1';
                    query.pageSize = paginationOptions.pageSize || '50';
                    query.des = '';
                    query.B= $scope.dateFrom||'';
                    query.E= $scope.dateTo||'';
                    query.SampleName= $scope.SampleName;
                    query.MaterialNO= $scope.MaterialNO||'';
                    query.Line= $scope.Line||'';
                    query.Group=$scope.GroupBarcode ||'';
                    query.Status=$scope.Status ||'';


                return query;
            };
            var paginationOptions = {
                pageNumber: 1,
                pageSize: 50,
                sort: null
            };
            function rowTemplate() {

                return '<div ng-dblclick="grid.appScope.doubleClick(row.entity)" style="cursor: pointer;" >' +
                    '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div>' +
                    '</div>';
            }
            $scope.Print = function () {
                window.print();
            }
            $scope.ViewReport=function(){

                var query ={};
                query.B= $scope.dateFrom;
                query.E= $scope.dateTo;
                query.MaterialNO= $scope.MaterialNO||'';
                query.Line= $scope.Line||'';
                query.Group=$scope.GroupBarcode ||'';
                LIMSService.Entrusted.GET_EvaluateStapleGrade(query,function (res) {
                    if(res){
                        $scope.reloadGrid = true;
                        $scope.gridOptions.columnDefs= colReport;
                        $scope.gridOptions.data = res.TableData;
                        $scope.reloadGrid = false;
                    }
                })

            }
            $scope.Search = function () {
                $scope.isRun = false;
            var query= getPage();
                console.log(query);
                LIMSService.Entrusted.Get_BarcodeInStock(query, function (res) {

                    if(res){
                        $scope.reloadGrid = true;
                        $scope.gridOptions.columnDefs= col;
                        $scope.gridOptions.data = res.TableData;
                        $scope.gridOptions.totalItems = res.TableCount[0];
                        $scope.reloadGrid = false;
                    }
                    //Parse to float to show value. Because hightcharts wont work with string
                    // options.colors = ['#fdda10', '#000000', '#5edfff', '#fa773b', '#010c70', '#a6e235', '#f682e3', '#b4b4b4', '#fdc610']
                    // options.series[0].data = res.getchart;
                    // options.title = {
                    //     // text: '$scope.SampleName + ' (' + $scope.dateFrom + ' - ' + $scope.dateTo + ')''
                    //     text: '生產產品百分比'
                    // }
                    // res.getchart.forEach(function (element, index) {
                    //     // element.name = (element.Grade == "0" ? '(B) ' : '') + element.name;
                    //     console.log(index);
                    //     if (element.Grades == '0') options.colors[index] = 'red';
                    //     element.y = parseFloat(element.y);
                    // });
                    /**
                     * res.getchart: chart data
                     * res.getheader:
                     * res.getdetail:
                     */

               //     obj.highcharts(options);
                   // ShowData(res.getheader, res.getdetail);
                    console.log(res);
                })
            }

            function ShowData(h, d) {
                var ha = h.filter(x => x.Grades == '1');
                var hb = h.filter(x => x.Grades == '0');
                //definition
                if($scope.SampleName == 'S01020001')
                    $scope.vdepart = 'POLY';
                if ($scope.SampleName == 'S01020002')
                    $scope.vdepart = 'SSP';

                if  ($scope.SampleName == 'S01020003')
                    $scope.vdepart = 'POLY52';
                if  ($scope.SampleName == 'S01020004')
                    $scope.vdepart = 'PSF';

                //GRADE A
                $scope.lenA = ha.length;
                $scope.spanA = ha[0]; //first row of Grade A
                $scope.ha = ha = ha.splice(1, h.length); //other rows of Grade A
                //GRADE B, C,... = Other Grades
                $scope.lenB = hb.length;//h.length - a;
                $scope.spanB = hb[0]; //first row of [Other Grades]
                $scope.hb = hb = hb.splice(1, h.length); //other Rows of [Other Grades]
                //distinct property
                var taf = TAFFY(d);
                $scope.sampleheader = taf().distinct("LOT_NO", "Grades", "Grade");
                $scope.propheader = taf().distinct("PropertyName");
                console.log($scope.sampleheader);
                console.log($scope.propheader);
                //details
                $scope.detail = d;
                $scope.isRun = true;
            }
            var gridMenu = [
                {
                    title: $translate.instant('Choose Range'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length > 0) {
                            var ListStaplePickForGrade = [];
                            for(var i =0;i<resultRows.length;i++){
                                var item = resultRows[i];
                                if(item.State!='N'){
                                    ListStaplePickForGrade = [];
                                    Notifications.addMessage({
                                        'status': 'error',
                                        'message': 'The status of barcode invalid:<br>  <b>' + item.BarCode + '</b></br>'
                                    });
                                    return;
                                }
                                var StaplePickForGrade ={};
                                StaplePickForGrade.BarCode=item.BarCode;
                                StaplePickForGrade.MaterialNO=item.MaterialNO;
                                StaplePickForGrade.ProdDate=item.ProdDate;
                                StaplePickForGrade.Batch=item.Batch;
                                StaplePickForGrade.Grade=item.Grade;
                                StaplePickForGrade.GroupBarCode=item.Group;
                                StaplePickForGrade.ProdSpec=item.ProdSpec;
                                StaplePickForGrade.Line=item.Line;
                                StaplePickForGrade.isModifyGrade=false;
                                ListStaplePickForGrade.push(StaplePickForGrade);

                            }
                            LIMSService.EntrustedInfo().Save_StaplePickForGrade({list_StaplePickForGrade:ListStaplePickForGrade}).$promise.then(function (res) {
                                if(res){
                                    $scope.Search();
                                    Notifications.addError({

                                        'message': 'Successfully selected distance'
                                    });
                                }

                            },function (err) {

                            })
                        }else{
                            Notifications.addError({
                                'status': 'error',
                                'message': 'Please choose at least one row to continue'
                            });
                        }

                    },
                    order: 1,
                    id: 'ChooseRange'
                },
                {
                    title: $translate.instant('Set Grade'),
                    action: function () {
                        $timeout(function () {
                            var resultRows = $scope.gridApi.selection.getSelectedRows();
                            if (resultRows.length > 0) {
                                var modifyGridData = [];
                                for (var i = 0; i < resultRows.length; i++) {
                                    var item = resultRows[i];
                                    if (item.State != 'S') {
                                        modifyGridData = [];
                                        Notifications.addMessage({
                                            'status': 'error',
                                            'message': 'The status of barcode invalid:<br>  <b>' + item.BarCode + '</b></br>'
                                        });
                                        return;
                                    } else {
                                        modifyGridData.push(item);
                                    }


                                }
                                $scope.ModifyGrid.data = modifyGridData;
                                $('#ModifyGradeModal').modal('show');
                            }else{
                                Notifications.addError({
                                    'status': 'error',
                                    'message': 'Please choose at least one row to continue'
                                });
                            }
                            $scope.$apply();
                        }, 0);

                      //  Notifications.addMessage({ 'message': 'Loading failed:<br>  <b>àdasfsd</b></br>' });

                    },
                    order: 2,
                    id: 'SetGrade'
                }
            ];
            $scope.CloseModal= function () {
                $scope.ModifyGrid.data=[];
              //  $scope.gridApi.core.addToGridMenu(gridApi.grid, gridMenu);
                $('#ModifyGradeModal').modal('hide');
            }
            $scope.modalModifySubmit = function () {
                var ListStaplePickForGrade = [];
                for(var i =0;i<$scope.ModifyGrid.data.length;i++){
                    var item = $scope.ModifyGrid.data[i];
                    if(item.State!='S'){
                        ListStaplePickForGrade = [];
                        Notifications.addMessage({
                            'status': 'error',
                            'message': 'The status of barcode invalid:<br>  <b>' + item.BarCode + '</b></br>'
                        });
                        return;
                    }
                    var StaplePickForGrade ={};
                    StaplePickForGrade.BarCode=item.BarCode;
                    StaplePickForGrade.MaterialNO=item.MaterialNO;
                    StaplePickForGrade.ProdDate=item.ProdDate;
                    StaplePickForGrade.Batch=item.Batch;
                    StaplePickForGrade.Grade=$scope.Grade;
                    StaplePickForGrade.GroupBarCode=item.Group;
                    StaplePickForGrade.ProdSpec=item.ProdSpec;
                    StaplePickForGrade.Line=item.Line;
                    StaplePickForGrade.isModifyGrade=true;
                    ListStaplePickForGrade.push(StaplePickForGrade);

                }
                LIMSService.EntrustedInfo().Save_StaplePickForGrade({list_StaplePickForGrade:ListStaplePickForGrade}).$promise.then(function (res) {
                    if(res){
                        $scope.ModifyGrid.data=[];
                        $scope.Search();
                        $('#ModifyGradeModal').modal('hide');
                        $interval( function() {
                            Notifications.addMessage({

                                'message': 'Successfully changed grade'
                            });
                        }, 0, 1);

                    }

                },function (err) {

                })
            }
        }
    ])

})
