define(['myapp', 'angular', 'higthchartexport'], function (myapp, angular) {
    myapp.controller('StapleReportController', ['$scope', '$filter', '$http', '$q', '$compile', '$routeParams', '$resource', '$location', 'i18nService',
        'Notifications', 'Auth', '$translate', 'LIMSService', 'LIMSBasic','$interval','$timeout',
        function ($scope, $filter, $http, $q, $compile, $routeParams, $resource, $location, i18nService, Notifications, Auth,
                  $translate, LIMSService, LIMSBasic,$interval,$timeout) {

            $scope.isRun = false;
            var lang = window.localStorage.lang || 'EN';
            $scope.lang = lang;
            $scope.reloadGrid = false;
            $scope.headers =[];
            $scope.headersHidden =[];
            var obj = $('#mychart');
            //Default test case --------------------
            $scope.dateFrom = moment(new Date()).format('YYYY-MM');//.subtract(1, 'months').date(1)
            $scope.dateTo = moment(new Date()).format('YYYY-MM');//.date(1).subtract(1, 'days')
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
            $scope.ReportTypeList =[
                {ID:'Month',Name:$translate.instant('Month')},
                {ID:'Year',Name:$translate.instant('Year')}

            ];
            $scope.ReportType = 'Month';
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
            function getPrecBySampleName(){
                var deferred = $q.defer()
                var query ={};
                query.SampleName=$scope.SampleName||'';
                LIMSService.EntrustedInfo().GET_PrecValueBySampleName(query).$promise.then(function (res) {
                    if(res){
                        deferred.resolve(res.TableData);
                    }
                },function (err) {
                    deferred.reject(err);
                })
                return deferred.promise;
            }
            $q.all([getSamples(),getPrecBySampleName()]).then(function (result) {
                $scope.sampleList = result[0].filter((el) => {
                    return (el.SampleName==='S03020001');
            });
                $scope.PrecList = result[1];
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

            var col_Month = [


                {
                    field: 'Line',
                    displayName: $translate.instant('Line'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'LOT_NO',
                    displayName: $translate.instant('LOT_NO'),
                    enableCellEdit: false,
                    minWidth: 130,
                    cellTooltip: true
                },
                {
                    field: 'ProdSpec',
                    displayName: $translate.instant('ProdSpec'),
                    enableCellEdit: false,
                    minWidth: 180,
                    cellTooltip: true
                }
                ,
                {
                    field: 'main_Batch',
                    displayName: $translate.instant('main_Batch'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true,
                    visible:false
                },
                {
                    field: 'sub_Batch',
                    displayName: $translate.instant('sub_Batch'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true


                },
                {
                    field: 'OrderNumber',
                    displayName: $translate.instant('OrderNumber'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true,
                    visible:false


                },
            ];
            var col_Year = [


                {
                    field: 'Line',
                    displayName: $translate.instant('Line'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true
                },
                {
                    field: 'LOT_NO',
                    displayName: $translate.instant('LOT_NO'),
                    enableCellEdit: false,
                    minWidth: 130,
                    cellTooltip: true
                },
                {
                    field: 'ProdSpec',
                    displayName: $translate.instant('ProdSpec'),
                    enableCellEdit: false,
                    minWidth: 180,
                    cellTooltip: true
                }
                ,
                {
                    field: 'main_Batch_count',
                    displayName: $translate.instant('main_Batch_count'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true,
                    visible:false
                },
                {
                    field: 'Month',
                    displayName: $translate.instant('Month'),
                    enableCellEdit: false,
                    minWidth: 80,
                    cellTooltip: true,
                    visible:false


                },
            ];
            $scope.gridOptions = {
                columnDefs: col_Month,
                data: [],
                multiSelect: false,
                enableColumnResizing: true,
                enableColumnResize: true,
                enableCellEditOnFocus: true,
                enableSorting: true,
                showGridFooter: true,
                enableGridMenu: true,
                exporterMenuPdf: false,
                enableSelectAll: false,
                enableRowHeaderSelection: true,
                enableRowHashing: false,
                enableRowSelection: true,
                paginationPageSizes: [50, 100, 200, 500],
                paginationPageSize: 50,
                exporterOlderExcelCompatibility: false,
                exporterMenuAllData : false,
                exporterMenuVisibleData: false,
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

                    });

                },

                rowTemplate: rowTemplate()

            };
            var gridMenu = [{
                    title: $translate.instant('Export To Excel'),
                    action: function () {

                        $timeout(function () {
                            var today =moment(new Date()).format('YYYY-MM-DD');
                            var TittleName = $scope.ReportType=='Month'?'StableMonthReport':'StableYearReport';
                            var ExcelTittle =$scope.ReportType=='Month'?'聚酯短纖品質月報表（Nonwoven Type)':'聚酯短纖品質年報表(Nonwoven Type)';
                            var uri = 'data:application/vnd.ms-excel;base64,'
                                , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><?xml version="1.0" encoding="UTF-8" standalone="yes"?><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head>'+'' +
                                '<body><div style="text-align: center"><legend> <h1> {excelTittle} </h1></legend><div style="text-align: center"><label>{interval}</label>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;<label>棉品技科： 513201500</label></div><table>{table}</table></body></html>'
                                , base64 = function(s) { return window.btoa(unescape(encodeURIComponent(s))) }
                                , format = function(s, c) { return s.replace(/{(\w+)}/g, function(m, p) { return c[p]; }) }

                            var interval = $scope.ReportType=='Month' ? '日期：' +$scope.dateFrom +' - ' +$scope.dateTo : '日期區間: ' +$scope.dateFrom +' - ' +$scope.dateTo;
                            var table = document.getElementById('tableReportHidden');
                            var filters = $('.ng-table-filters').remove();
                            var ctx = {worksheet: name || TittleName, table: table.innerHTML, interval: interval, today : today,excelTittle:ExcelTittle};
                            $('.ng-table-sort-header').after(filters) ;
                            var url = uri + base64(format(template, ctx));
                            var a = document.createElement('a');
                            a.href = url;
                            a.download = TittleName+'.xls';
                            a.click();
                            $scope.$apply();
                        }, 50);

                    },
                    order: 1

                }
            ];
            var getPage = function () {
                var query = {};

                //subtract(1, 'months').date(1)
                query.B= moment($scope.dateFrom).format('YYYY-MM-01 ')||'';
                query.E= moment($scope.dateTo).add(1, 'M').format('YYYY-MM-01 ')||'';
                query.ReportType =$scope.ReportType||'Month';


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
            $scope.checkIsZero= function  (number,fixNumber) {
                if(number!=null && number!=undefined){
                    return number.toFixed(fixNumber);
                }
            }
            $scope.Search = function () {
                $scope.isRun = false;
                var query= getPage();
                console.log(query);
                LIMSService.Entrusted.StapleFiber_ReportByBatch(query, function (res) {

                    if(res){
                        var headers = [];

                        $scope.reloadGrid = true;
                        $scope.Data = res.TableData;
                        $scope.DataHidden = [];
                        for (var key in res.TableData[0]) {
                            headers.push(key);

                            if (['Line', 'LOT_NO','ProdSpec','main_Batch',
                                'OrderNumber','sub_Batch'

                            ].indexOf(key) < 0 && key.indexOf('$') < 0 ) {
                                var colItem={};


                                if(key=='sub_Batch'|| key =='Month' ||key=='sub_Batch_count'){
                                    colItem.field= key,
                                        colItem.displayName = key,
                                        colItem.enableCellEdit= false,
                                        colItem.minWidth= 100,
                                        colItem.cellTooltip= true
                                }else {
                                    var PrecData = $scope.PrecList.filter(x => x.LOT_NO === res.TableData[0].LOT_NO && x.PropertyName === key
                                )
                                    ;

                                    colItem.field = key;
                                    colItem.displayName = key;
                                    colItem.enableCellEdit = false;
                                    colItem.minWidth = 100;
                                    colItem.cellTooltip = true;

                                 if(PrecData!=undefined||PrecData!=null ){
                                    if( PrecData.length>0){

                                        colItem.cellFilter= 'number:'+PrecData[0].Prec;
                                        colItem.cellTemplate= '<span  >{{grid.appScope.checkIsZero(row.entity["'+key+'"],'+PrecData[0].Prec+')}}</span>';
                                    }

                                 }else{
                                     colItem.cellTemplate= '<span  >{{grid.appScope.checkIsZero(row.entity["'+key+'"])}}</span>';
                                 }

                                }

                                if($scope.ReportType=='Month'){

                                    col_Month.push(colItem);
                                }else{

                                    col_Year.push(colItem);
                                }
                            }

                        }

                        if($scope.ReportType=='Month'){

                            $scope.gridOptions.columnDefs= col_Month;
                        }else{

                            $scope.gridOptions.columnDefs= col_Year;
                        }

                        $scope.gridOptions.data =res.TableData;// $filter('orderBy')(res.TableData, 'Line','LOT_NO','main_Batch','OrderNumber');
                        $scope.headers = headers;
                        $scope.reloadGrid = false;


                        $timeout(function () {
                            var headersHidden=[];
                            for(var  i =0;i<res.TableData.length;i++){
                                var item = res.TableData[i];
                                for (var key in res.TableData[0]) {

                                    if(headersHidden.indexOf($scope.translateHeader(key)) === -1){
                                        headersHidden.push($scope.translateHeader(key));
                                    }


                                }
                                $scope.DataHidden.push(item)
                            }
                            for (var j =0;j<$scope.DataHidden.length;j++)
                            {
                                for (var k =0;k<headersHidden.length;k++){
                                    if(!isNaN(headersHidden[k])&& headersHidden[k]!=null){
                                        var PrecDatas =   $scope.PrecList.filter(x => x.LOT_NO === $scope.DataHidden[j].LOT_NO && x.PropertyName===headersHidden[k]);
                                        if(PrecDatas.length>0){

                                            $scope.DataHidden[j][headersHidden[k]]=  $scope.DataHidden[j][headersHidden[k]].toFixed(PrecDatas[0].Prec);
                                        }
                                    }
                                }

                            }

                            $scope.headersHidden=headersHidden;
                            $scope.$apply();

                        }, 50);

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

        $scope.translateHeader = function(header){

                switch (header) {
                    case 'Line':
                        return '線別Line' ;
                    case 'LOT_NO':
                        return '產品編號Material No';
                    case 'ProdSpec':
                        return '規格Spec';
                    case 'main_Batch':
                        return '批號Batch';
                    case 'sub_Batch':
                        return '批號Batch';
                    default:
                        return header;
                }



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
