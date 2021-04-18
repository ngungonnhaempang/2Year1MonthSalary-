define(['myapp','angular','xlsx'],function (myapp,angular) {
    myapp.directive('timekeeperUploadFile',['OAServices','Notifications','$translate',function (OAServices,Notifications,$translate) {
        return{
            restrict:'AEC',
            scope:{
                timekeeperUploadFile:"="
            },
            link: function (scope,element) {
                var test = element;
                element.bind("change",function () {
                    var file=  element[0].files[0]  ;
                    element.val(null);
                    //element.data('old-value', scope.timekeeperUploadFile);
                    if(file.type=="application/vnd.ms-excel"||file.type=="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"){
                        var reader = new FileReader();
                        scope.Foreign_Employee = {};
                        scope.Foreign_Employee.listForeign_Employee=[];
                        reader.onload = function (ev) {
                            var data = ev.target.result;
                            var workbook = XLSX.read(data,{type:'binary',cellDates:true,cellNF:false,cellText:false});
                            var  first_sheetname = workbook.SheetNames[0];
                            var worksheet = workbook.Sheets[first_sheetname];
                            var result = XLSX.utils.sheet_to_json(worksheet,{dateNF:'YYYY-MM-DD'});
                            for (var i =0 ; i <result.length;i++) {
                                var details = {};
                                var item = result[i];
                                details.EmployeeID= item.EmployeeID;
                                details.EmployeeName= item.EmployeeName;
                                details.Department_EN= item.Department_EN;
                                details.Department_CN= item.Department_CN;
                                details.DepartmentID= item.DepartmentID;
                                details.CardNumber= item.CardNumber;
                                scope.Foreign_Employee.listForeign_Employee.push(details);

                            }
                            if(scope.$parent.IsSuccess){
                                OAServices.getInformation.Foreign_EmployeeFromUploadFile({}, scope.Foreign_Employee).$promise.then(function (res) {
                                   console.log(res.MSG);

                                    if (res.MSG!="") {
                                        Notifications.addError({
                                            'status': 'error',
                                            'message': res.MSG
                                        });
                                    } else {
                                        Notifications.addMessage({
                                            'status': 'info',
                                            'message':$translate.instant('Save_Success_MSG')
                                        });
                                    }
                                }, function (errormessage) {
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': errormessage
                                    });
                                });

                            }else{
                                Notifications.addError({
                                    'status': 'error',
                                    'message': 'Can not upload file. Please contact IT department.'
                                });
                            }
                        }
                        reader.readAsBinaryString(file);
                    }
                })
            }
        }
    }])


    myapp.directive('fileReader', function (OAServices) {
        return {
            scope: {
                fileReader: '='
            },
            link: function (scope, element) {
                $(element).on('change', function (changeEvent) {
                    var files = changeEvent.target.files;
                    if (files.length) {
                        var r = new FileReader();
                        r.onload = function (e) {
                            var contents = e.target.result;
                            scope.$apply(function () {

                                scope.fileReader = contents;

                                var lines = contents.split('\n');

                                var result = [];

                                var headers = lines[0].split(',');
                                var UserInfo = '';
                                for (var i = 1; i < lines.length - 1; i++) {
                                    var obj = {};
                                    var LineSplit = lines[i].split(',');

                                    for (var j = 0; j < headers.length; j++) {
                                        obj[headers[j]] = LineSplit[j];
                                    }
                                    if (LineSplit[0] != '') {
                                        UserInfo += LineSplit[0] + ':' + LineSplit[1] + ':' + LineSplit[2] + ':' + LineSplit[3] + '|'
                                    }


                                    result.push(obj);
                                }
                                UserInfo = UserInfo.substring(0, UserInfo.length - 2);
                                console.log(UserInfo);
                                OAServices.getInformation.UploadUserHandle({UserInfo: UserInfo}).$promise.then(function (res) {
                                    console.log('----------------');//

                                    console.log(res);
                                    alert('Add success. Page will reload.');

                                    setTimeout(function () {
                                        location.reload();
                                    }, 3000);
                                }, function (errormessage) {
                                    Notifications.addError({'status': 'error', 'message': errormessage});
                                });

                            });
                        };

                        r.readAsText(files[0]);

                    }
                });
            }
        };
    });
    myapp.controller('MealTimekeeperManagementController', ['$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', 'ConQuaService', '$upload', '$translatePartialLoader', '$translate', 'OAServices',
        function ($scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, ConQuaService, $upload, $translatePartialLoader, $translate, OAServices) {

            $scope.dateFrom = $filter('date')(new Date(), 'yyyy-MM-dd');
            $scope.dateTo = $filter('date')(new Date(), 'yyyy-MM-dd');
            $scope.sum = 0;
            $scope.flowkey = 'FEPVOAMeal';
            $scope.note = {};
            //  $scope.CDepartmentList = {DepartmentID: "A", Specification: "", Spe: "", Alias: ""};
            $scope.DepartmentID = '';
            $scope.Total = 0;
            $scope.exportExel = {};
            $scope.Timekeeper={};
            var dataongrid = {};


            var file = '';
            $scope.onFileSelect = function ($files) {
                console.log($files);
                file = $files;
            };


            // EngineApi.getDepartment().getList({userid: Auth.username, ctype: ''}, function (res) {
            //     $scope.CDepartmentList = res;
            //     console.log($scope.CDepartmentList);
            // });


            var col = [
                {
                    field: 'CardID',
                    displayName: $translate.instant('CardID'),
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'EmployeeID',
                    displayName: $translate.instant('EmployeeID'),
                    minWidth: 105,
                    cellTooltip: true
                }
                ,
                {
                    field: 'EmployeeName',
                    displayName: $translate.instant('EmployeeName'),
                    cellTooltip: true,
                    minWidth: 105
                },
                {
                    field: 'Department_EN', displayName: $translate.instant('Department_EN')
                    , minWidth: 10,
                    cellTooltip: true

                },
                {
                    field: 'Department_CN',
                    displayName: $translate.instant('Department_CN'),
                    minWidth: 10, cellTooltip: true
                },
                {
                    field: 'CardNumber'
                    , displayName: $translate.instant('CardNumbers')
                    , minWidth: 10,
                    cellTooltip: true
                }
            ];
            var col1 = [
                {
                    field: 'EmployeeID',
                    // cellTemplate: '<a ng-click="grid.appScope.getVoucher(row)"  style="padding:5px;display:block; cursor:pointer">{{COL_FIELD}}</a>',
                    displayName: $translate.instant('EmployeeID'),
                    minWidth: 120,
                    cellTooltip: true
                },
                {
                    field: 'EmployeeID_Old',
                    displayName: $translate.instant('EmployeeID_Old'),
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'EmployeeName',
                    displayName: $translate.instant('EmployeeName'),
                    minWidth: 150,
                    cellTooltip: true
                },
                {field: 'Sex', displayName: $translate.instant('Sex'), minWidth: 90, cellTooltip: true},
                {
                    field: 'DepartmentID',
                    displayName: $translate.instant('Department'),
                    minWidth: 105,
                    cellTooltip: true
                },
                {
                    field: 'Specification',
                    displayName: $translate.instant('Specification'),
                    minWidth: 200,
                    cellTooltip: true
                },
                {field: 'DateSwipe', displayName: $translate.instant('DateSwipe'), minWidth: 180, cellTooltip: true},
                {field: 'TimeSwipe', displayName: $translate.instant('TimeSwipe'), minWidth: 180, cellTooltip: true},
                {field: 'Machine', displayName: $translate.instant('Machine'), minWidth: 180, cellTooltip: true},
                {field: 'Type', displayName: $translate.instant('Type'), minWidth: 150, cellTooltip: true}


            ];
            $scope.gridOptions = {
                columnDefs: col,
                data: [],
                enableColumnResizing: true,
                enableSorting: true,
                showGridFooter: true,
                //gridFooterTemplate: '<div style=\'text-align: right;padding-right: 165px\'><b>Total: {{grid.appScope.Total}} </b></div>',
                // showColumnFooter: true,
                enableGridMenu: true,
                //   exporterMenuPdf: false,
                enableSelectAll: false,
                enableRowHeaderSelection: true,
                enableRowSelection: true,
                multiSelect: true,
                paginationPageSizes: [50, 100, 200, 500],
                paginationPageSize: 50,
                useExternalPagination: true,
                enablePagination: true,
                enableAutoFitColumns: true,
                enablePaginationControls: true,
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    EngineApi.getTcodeLink().get({
                        'userid': Auth.username,
                        'tcode': $scope.flowkey
                    }, function (linkres) {
                        $scope.IsSuccess=linkres.IsSuccess;


                    });
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        $scope.selectedVoucherid = row.entity.VoucherID;
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        paginationOptions.pageNumber = newPage;
                        paginationOptions.pageSize = pageSize;
                        getPage();
                    });
                }

            };
            var paginationOptions = {
                pageNumber: 1,
                pageSize: 50,
                sort: null
            };


            $scope.Search = function () {
                getPage();
            };

            var getPage = function () {
                $scope.Total = 0;

                var query = {};

                query.EmployeeID = $scope.Timekeeper.EmployeeID || '';

                query.CardNumber = $scope.Timekeeper.CardNumber || '';
                query.EmployeeName= $scope.Timekeeper.EmployeeName || '';
                OAServices.getInformation.MEAL_GetForeignEmployee(query).$promise.then(function (res) {
                    console.log(res);
                    for (var i = 0; i < res.length; i++) {
                        $scope.Total += parseInt(res[i].Total);
                    }
                    $scope.exportExel = res;
                    $scope.gridOptions.data = res;
                }, function (errormessage) {
                    Notifications.addError({'status': 'error', 'message': errormessage});
                });
            };
            $scope.bpmnloaded = false;
            $scope.showPng = function () {
                if ($scope.bpmnloaded == true) {
                    $scope.bpmnloaded = false;
                } else {
                    $scope.bpmnloaded = true;
                }
            };


            $scope.reset = function () {
                $('#myModal').modal('hide');
            };
            // var gridMenu = [{
            //     title: $translate.instant('Print Report Details'),
            //     action: function ($event) {
            //
            //         $scope.deptID = '';
            //         if ($scope.note.Department == undefined) {
            //             $scope.deptID = 'All';
            //         }
            //         else {
            //             $scope.deptID = $scope.note.Department;
            //         }
            //         if ($scope.model.selectType == 'Vietnamese') {
            //             $scope.IncludeOTUser = 'True';
            //         }
            //         else {
            //             $scope.IncludeOTUser = 'False';
            //         }
            //
            //         //    return $scope.note.Department;
            //         var href = '#/gate/MealDetails/' + $scope.dateFrom + '/' + $scope.dateTo + '/' + $scope.deptID + '/' + $scope.IncludeOTUser;
            //         window.open(href);
            //     },
            //     order: 1
            //
            // }, {
            //     title: $translate.instant('Export Excel File Details'),
            //     order: 2,
            //     action: function ($event) {
            //         var paras = {};
            //         if ($scope.model.selectType == 'Vietnamese') {
            //             paras.IncludeOTUser = 'True';
            //         }
            //         else {
            //             paras.IncludeOTUser = 'False';
            //         }
            //         paras.UserID = Auth.username;
            //         paras.DepartmentID = '';
            //         paras.DateE = $scope.dateTo;
            //         paras.DateB = $scope.dateFrom;
            //         paras.Type = '';
            //         OAServices.getInformation.getInfoByDepartmentID(paras).$promise.then(function (res) {
            //             console.log(res);
            //
            //             $scope.exportExel = res;
            //
            //             function convertArrayOfObjectsToCSV(args) {
            //                 var result, ctr, keys, columnDelimiter, lineDelimiter, data;
            //
            //                 data = args.data || null;
            //                 if (data == null || !data.length) {
            //                     return null;
            //                 }
            //
            //                 columnDelimiter = args.columnDelimiter || ',';
            //                 lineDelimiter = args.lineDelimiter || '\n';
            //
            //                 keys = Object.keys(data[0]);
            //
            //                 result = '\uFEFF';
            //                 result += keys.join(columnDelimiter);
            //                 result += lineDelimiter;
            //
            //                 data.forEach(function (item) {
            //                     ctr = 0;
            //                     keys.forEach(function (key) {
            //                         if (ctr > 0) result += columnDelimiter;
            //
            //                         result += item[key];
            //                         ctr++;
            //                     });
            //                     result += lineDelimiter;
            //                 });
            //
            //                 return result;
            //             }
            //
            //             var data, filename, link;
            //
            //             var csv = convertArrayOfObjectsToCSV({
            //                 data: res
            //             });
            //             if (csv == null) return;
            //
            //             filename = 'export.csv';
            //
            //             if (!csv.match(/^data:text\/csv/i)) {
            //                 csv = 'data:text/csv;charset=UTF-16,' + csv;
            //             }
            //             data = encodeURI(csv);
            //
            //             link = document.createElement('a');
            //             link.setAttribute('href', data);
            //             link.setAttribute('download', filename);
            //             link.click();
            //
            //
            //         }, function (errormessage) {
            //             Notifications.addError({'status': 'error', 'message': errormessage});
            //         });
            //     }
            // },
            //     {
            //         title: $translate.instant('Export Excel File By Department'),
            //         order: 3,
            //         action: function () {
            //             var objects = dataongrid;
            //             for (var i = 0; i < objects.length; i++) {
            //                 var obj = objects[i];
            //                 for (var prop in obj) {
            //                     if (obj.hasOwnProperty(prop) && obj[prop] !== null && !isNaN(obj[prop])) {
            //                         obj[prop] = +obj[prop];
            //                     }
            //                 }
            //             }
            //             //       console.log(objects);
            //             JSONToCSVConvertor(objects, 'Meal_Report', true);
            //         }
            //     }
            // ];

            function JSONToCSVConvertor(JSONData, ReportTitle, ShowLabel) {
                //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
                var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;

                var CSV = '\uFEFF';
                CSV += ReportTitle + '\r\n\n';
                //This condition will generate the Label/Header
                if (ShowLabel) {
                    var row = '';
                    //This loop will extract the label from 1st index of on array
                    for (var index in arrData[0]) {
                        //Now convert each value to string and comma-seprated
                        row += index + ',';
                    }
                    row = row.slice(0, -1);
                    //append Label row with line break
                    CSV += row + '\r\n';
                }
                //1st loop is to extract each row
                for (var i = 0; i < arrData.length; i++) {
                    var row = '';
                    //2nd loop will extract each column and convert it in string comma-seprated
                    for (var index in arrData[i]) {
                        row += '"' + arrData[i][index] + '",';
                    }
                    row.slice(0, row.length - 1);

                    //add a line break after each row
                    CSV += row + '\r\n';
                }

                if (CSV == '') {
                    alert('Invalid data');
                    return;
                }

                //Generate a file name
                var fileName = '';
                //this will remove the blank-spaces from the title and replace it with an underscore
                fileName += ReportTitle.replace(/ /g, '_');

                //Initialize file format you want csv or xls
                var uri = 'data:text/csv;charset=utf-16,' + encodeURI(CSV);

                // Now the little tricky part.
                // you can use either>> window.open(uri);
                // but this will not work in some browsers
                // or you will not get the correct file extension

                //this trick will generate a temp <a /> tag
                var link = document.createElement('a');
                link.href = uri;

                //set the visibility hidden so it will not effect on your web-layout
                link.style = 'visibility:hidden';
                link.download = fileName + '.csv';

                //this part will append the anchor tag and remove it after automatic click
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
            }

            $scope.setDate = function () {
                // var startDate1 = $scope.dateFrom;
                $scope.dateTo = $filter('date')(new Date($scope.dateFrom), 'yyyy-MM-dd');
                //$scope.$apply();

            };


        }])
    ;
})