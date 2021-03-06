/**
 * Created by wangyanyan on 2016-09-13.
 */
define(['myapp', 'angular'], function(myapp, angular) {
    myapp.controller("SampleController", ['$scope', 'EngineApi', '$http', '$timeout', 'Notifications', '$compile', '$filter', 'Auth', '$upload', '$resource', '$translatePartialLoader', '$translate',
        function ($scope, EngineApi, $http, $timeout, Notifications, $compile, $filter, Auth, $upload, $resource, $translatePartialLoader, $translate) {
            $translatePartialLoader.addPart('GoodsOut');
            $translate.refresh();
            // test array
            $scope.kind = [{"GuestType": "return back"}, {"GuestType": " no return XX"}]
            $scope.files = []
            $scope.deletefile = function (docid, index) {
                var data = "DocId=" + docid;
                $http.delete('/api/cmis/deletefile?' + data)
                    .success(function (data, status, headers) {
                        console.log(data);
                        $scope.files.splice(index, 1);
                    })
                    .error(function (data, status, header, config) {
                        Notifications.addError({'status': 'error', 'message': status + data});
                    });
            }
            $scope.onFileSelect = function ($files, size) {
                console.log($files);
                if (!size) {
                    size = 1024 * 1024 * 1;
                }
                if ($files.size > size) {
                    Notifications.addError({'status': 'error', 'message': "upload file can't over " + size + "byte"});
                    return false;
                } else {
                    for (var i = 0; i < $files.length; i++) {
                        var $file = $files[i];

                        $scope.upload = $upload.upload({
                            url: '/api/cmis/upload',
                            method: "POST",
                            file: $file
                        }).progress(function (evt) {
                            // get upload percentage
                            console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));

                        }).success(function (data, status, headers, config) {
                            // file is uploaded successfully
                            console.log(data.file);
                            $scope.files.push(data.file);
                        }).error(function (data, status, headers, config) {
                            console.log($file);
                            console.log(status);
                            console.log(data);
                        });
                    }
                }
            }

            $scope.deletefile=function(docid,index){
                var  data="DocId="+docid;
                $http.delete('/api/cmis/deletefile?' + data)
                    .success(function (data, status, headers) {
                        console.log(data);
                        $scope.files.splice(index, 1);
                    })
                    .error(function (data, status, header, config) {
                        Notifications.addError({'status': 'error', 'message':  status + data});
                    });
            }



        }])

    myapp.controller("SampleController1", ['$scope', 'EngineApi', '$http', '$timeout', 'Notifications', '$compile', '$filter', 'Auth', '$upload', '$resource', '$translate', '$translatePartialLoader','$rootScope',
        function ($scope, EngineApi, $http, $timeout, Notifications, $compile, $filter, Auth, $upload, $resource,$translate, $translatePartialLoader,$rootScope) {

            $translatePartialLoader.addPart('GoodsOut');
            $translate.refresh();

         var testdate=   {"TableData":[{"No":"1","VoucherID":"GD201609130001","GoodsType":"??????","IsBack":"False","ExpectBack":"","TakeOut":"??????","TakeCompany":"??????A","VehicleNO":"???F12346","ExpectOut":"2016/9/13 0:00:00","Remark":"???F12346","OutTime":"2016/9/13 13:10:35","Status":"O","Stamp":"2016/9/13 9:09:28","UserID":"981038"},{"No":"2","VoucherID":"GD201609090007","GoodsType":"????????????","IsBack":"False","ExpectBack":"","TakeOut":"a","TakeCompany":"a","VehicleNO":"a","ExpectOut":"2016/9/9 0:00:00","Remark":"a","OutTime":"2016/9/9 10:41:20","Status":"O","Stamp":"2016/9/9 10:41:04","UserID":"981038"},{"No":"3","VoucherID":"GD201609090006","GoodsType":"??????","IsBack":"False","ExpectBack":"","TakeOut":"ere","TakeCompany":"66","VehicleNO":"9999","ExpectOut":"2016/9/9 0:00:00","Remark":"0","OutTime":"2016/9/9 10:20:46","Status":"O","Stamp":"2016/9/9 10:17:28","UserID":"981038"},{"No":"4","VoucherID":"GD201609090005","GoodsType":"????????????","IsBack":"False","ExpectBack":"","TakeOut":"AA","TakeCompany":"??????","VehicleNO":"ERER","ExpectOut":"2016/9/9 0:00:00","Remark":"DFD","OutTime":"2016/9/9 10:05:27","Status":"O","Stamp":"2016/9/9 10:05:02","UserID":"981038"},{"No":"5","VoucherID":"GD201609090004","GoodsType":"????????????","IsBack":"True","ExpectBack":"2016/9/9 0:00:00","TakeOut":"??????","TakeCompany":"AA","VehicleNO":"8898","ExpectOut":"2016/9/9 0:00:00","Remark":"HELLO","OutTime":"2016/9/9 9:23:28","Status":"O","Stamp":"2016/9/9 9:19:04","UserID":"981038"},{"No":"6","VoucherID":"GD201609090003","GoodsType":"????????????","IsBack":"True","ExpectBack":"2016/9/10 0:00:00","TakeOut":"AA","TakeCompany":"??????","VehicleNO":"88889","ExpectOut":"2016/9/8 0:00:00","Remark":"HELLO","OutTime":"","Status":"Q","Stamp":"2016/9/9 13:01:34","UserID":"CASSIE"},{"No":"7","VoucherID":"GD201609090002","GoodsType":"??????","IsBack":"True","ExpectBack":"2016/9/9 0:00:00","TakeOut":"??????","TakeCompany":"??????","VehicleNO":"???B9999","ExpectOut":"2016/9/9 0:00:00","Remark":"hi","OutTime":"2016/9/9 9:07:49","Status":"O","Stamp":"2016/9/9 9:01:23","UserID":"981038"},{"No":"8","VoucherID":"GD201609090001","GoodsType":"??????","IsBack":"True","ExpectBack":"2016/9/10 0:00:00","TakeOut":"??????","TakeCompany":"??????","VehicleNO":"???Q999","ExpectOut":"2016/9/9 0:00:00","Remark":"hello","OutTime":"","Status":"","Stamp":"2016/9/9 8:59:03","UserID":"980996"},{"No":"9","VoucherID":"GD201609080002","GoodsType":"????????????","IsBack":"True","ExpectBack":"2016/9/30 0:00:00","TakeOut":"??????","TakeCompany":"??????b","VehicleNO":"???V12346","ExpectOut":"2016/9/8 0:00:00","Remark":"??????","OutTime":"2016/9/8 16:31:52","Status":"O","Stamp":"2016/9/8 16:30:43","UserID":"981038"},{"No":"10","VoucherID":"GD201609080001","GoodsType":"??????","IsBack":"False","ExpectBack":"","TakeOut":"??????","TakeCompany":"??????a","VehicleNO":"???V12345","ExpectOut":"2016/9/8 0:00:00","Remark":"??????","OutTime":"2016/9/8 16:31:45","Status":"O","Stamp":"2016/9/8 16:30:10","UserID":"981038"}],"TableCount":[10]};
            $scope.setColumnDefs = function() {
                var columnDefs = [
                    { field: 'VoucherID', displayName: $translate.instant('HEADLINE'), minWidth: 180, cellTooltip: true, cellTemplate: '<a  target="_blank" href="#/gate/GoodsOut/{{COL_FIELD}}/query"  style="padding:5px;display:block; cursor:pointer">{{COL_FIELD}}</a>' },
                    {field: 'Status', displayName: '??????', minWidth: 80, cellTooltip: true},
                    {field: 'GoodsType', displayName: '????????????', minWidth: 100, cellTooltip: true},
                    {field: 'IsBack', displayName: "????????????", minWidth: 100, cellTooltip: true},
                    {field: 'ExpectBack', displayName: '????????????', minWidth: 180, cellTooltip: true},
                    {field: 'TakeOut', displayName: '?????????', minWidth: 80, cellTooltip: true},
                    {field: 'TakeCompany', displayName: '????????????', minWidth: 200, cellTooltip: true},
                    {field: 'VehicleNO', displayName: '??????', minWidth: 150, cellTooltip: true},
                    {field: 'Remark', displayName: '??????', minWidth: 200, cellTooltip: true},
                    {field: 'OutTime', displayName: '????????????', minWidth: 180, cellTooltip: true},
                    {field: 'Stamp', displayName: '??????????????????', minWidth: 180, cellTooltip: true},
                    {field: 'UserID', displayName: '?????????', minWidth: 80, cellTooltip: true}
                ];
                $scope.columnDefs = columnDefs;
            }
            $scope.setColumnDefs();
            var GetgoodsList = function(data) {
                for (var i = 0; i < data.length; i++) {
                    data[i].IsBack = data[i].IsBack=='True' ? "???":"???"
                }
                return data;
            }
            $scope.$on('$translateChangeSuccess', function() {
                console.log("$translateChangeSuccess");
                $scope.setColumnDefs();
            });
            $scope.gridOptions = {

                data: [],
                columnDefs:    $scope.columnDefs ,
                enableColumnResizing: true,
                enableSorting: true,
                showGridFooter: false,
                enableGridMenu: true,
                exporterMenuPdf: false,
                enableSelectAll: false,
                enableRowHeaderSelection: true,
                enableRowSelection: true,
                multiSelect: false,
                paginationPageSizes: [50, 100, 200, 500],
                paginationPageSize: 50,
                exporterOlderExcelCompatibility: true,
                useExternalPagination: true,
                enablePagination: true,
                enablePaginationControls: true,
                gridMenuCustomItems: [
                    {
                        title: '??????',
                        action: function($event) {

                            $('#myModal').modal('show')
                        },
                        order: 0
                    },   {
                        title: '??????2',
                        action: function($event) {

                            Notifications.addError({'status': 'error', 'message': $translate.instant('message')});
                        },
                        order: 0
                    },
                    {
                        title: '??????',
                        action: function($event) {

                        },
                        order: 0
                    }],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;

                }
            };
            $scope.gridOptions.data = GetgoodsList(testdate.TableData);



        }])


    myapp.controller("WorkDayController",['$scope','GateGuest','Notifications','$http',function($scope,GateGuest,Notifications,$http){


        $http({
            url:'/ehs/gate/Checker/IsWorkDay?day=2017-03-03',
            method:'GET'
        }).success(function(data,header,config,status){
            $scope.showmessage1=data.msg;
        }).error(function(data,header,config,status){
            Notifications.addError({
                'status': 'error',
                'message': errResponse
            });
        });

        GateGuest.GetGateCheckers().isWorkDay({
            day: '2017-03-03'
        }).$promise.then(function (res) {
                console.log(res);
                $scope.showmessage=res.msg;
          }, function (errResponse) {
                Notifications.addError({
                    'status': 'error',
                    'message': errResponse
                });
            });

    }])
})