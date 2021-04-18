define(['myapp', 'angular'], function (myapp, angular) {
    myapp.controller("HRAttendanceController", ['$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', '$upload', '$translatePartialLoader', '$translate', 'HRServices',
        function ($scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, $upload, $translatePartialLoader, $translate, HRServices) {

            $scope.StatusList =
                [{
                    id: 'IN',
                    name: 'IN'
                }, {
                    id: 'OUT',
                    name: 'OUT'

                }];

            $scope.dateFrom = $filter('date')(new Date(), 'yyyy-MM-dd');
            $scope.dateTo = $filter('date')(new Date(), 'yyyy-MM-dd');
            $scope.sum = 0;
            $scope.flowkey = 'HR_Attendance';
            $scope.note = {};
            $scope.DepartmentID = '';
            $scope.Total = 0;
        
            $scope.WorkplaceList = [];
            var dataongrid = {};

            $scope.model = {};
            var gridMenu = [{
                title: $translate.instant('Download Text Log'),
                action: function ($event) {
                    
                    var Attendance_Query = {};
                    Attendance_Query.From = $scope.dateFrom || '';
                    Attendance_Query.To = $scope.dateTo || '';
                    Attendance_Query.Status = $scope.Status == 'All' ? '' : $scope.Status || '';
                    Attendance_Query.CardNumber = $scope.CardNumber || '';
                    Attendance_Query.EmployeeID = $scope.EmployeeID || '';
                    Attendance_Query.Workplace = $scope.Workplace || '';

                    HRServices.getInformation.GetAttendanceToExport({}, Attendance_Query).$promise.then(function (res) {
                        dataongrid = angular.toJson(res);
                        console.log(res);
                        var filename = 'attendance_' + moment($scope.dateFrom).format('YYYYMMDD');
                        var blob = new Blob([res.Data], { type: 'text/plain' });
                        if (window.navigator && window.navigator.msSaveOrOpenBlob) {
                            window.navigator.msSaveOrOpenBlob(blob, filename);
                        } else {
                            var e = document.createEvent('MouseEvents'),
                                a = document.createElement('a');
                            a.download = filename;
                            a.href = window.URL.createObjectURL(blob);
                            a.dataset.downloadurl = ['text/json', a.download, a.href].join(':');
                            e.initEvent('click', true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
                            a.dispatchEvent(e);
                            // window.URL.revokeObjectURL(a.href); // clean the url.createObjectURL resource
                        }

                    }, function (errormessage) {
                        Notifications.addError({ 'status': 'error', 'message': errormessage });
                    });
                },
                order: 1


            },
            {
                title: $translate.instant('Download Text Log MitaPro'),
                action: function ($event) {

                    var Attendance_Query = {};
                    Attendance_Query.From = $scope.dateFrom || '';
                    Attendance_Query.To = $scope.dateTo || '';
                    Attendance_Query.Status = $scope.Status == 'All' ? '' : $scope.Status || '';
                    Attendance_Query.CardNumber = $scope.CardNumber || '';
                    Attendance_Query.EmployeeID = $scope.EmployeeID || '';
                    Attendance_Query.Workplace = $scope.Workplace || '';

                    HRServices.getInformation.GetAttendanceToExportMitaPro({}, Attendance_Query).$promise.then(function (res) {
                        dataongrid = angular.toJson(res);
                        console.log(res);
                        var filename = 'attendance_' + moment($scope.dateFrom).format('YYYYMMDD');
                        var blob = new Blob([res.Data], { type: 'text/plain' });
                        if (window.navigator && window.navigator.msSaveOrOpenBlob) {
                            window.navigator.msSaveOrOpenBlob(blob, filename);
                        } else {
                            var e = document.createEvent('MouseEvents'),
                                a = document.createElement('a');
                            a.download = filename;
                            a.href = window.URL.createObjectURL(blob);
                            a.dataset.downloadurl = ['text/json', a.download, a.href].join(':');
                            e.initEvent('click', true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
                            a.dispatchEvent(e);
                            // window.URL.revokeObjectURL(a.href); // clean the url.createObjectURL resource
                        }

                    }, function (errormessage) {
                        Notifications.addError({ 'status': 'error', 'message': errormessage });
                    });
                },
                order: 2


            }
            ];

            var file = '';
            $scope.onFileSelect = function ($files) {
                console.log($files);
                file = $files;
            };

            var col = [
                {
                    field: 'Device',
                    displayName: $translate.instant('Device'),
                    minWidth: 120,
                    cellTooltip: true
                },
                {
                    field: 'CardNo',
                    displayName: $translate.instant('CardNo'),
                    minWidth: 100,
                    cellTooltip: true
                },
                {
                    field: 'EmployeeID',
                    displayName: $translate.instant('EmployeeID'),
                    minWidth: 150,
                    cellTooltip: true
                },
                {
                    field: 'EmployeeName',
                    displayName: $translate.instant('EmployeeName'),
                    minWidth: 150,
                    cellTooltip: true
                },
                {
                    field: 'TimeSwipe',
                    displayName: $translate.instant('TimeSwipe'),
                    minWidth: 150,
                    cellTooltip: true
                },

                {
                    field: 'Status',
                    displayName: $translate.instant('Status'),
                    minWidth: 105,
                    cellTooltip: true
                }
            ];

            $scope.gridOptions = {
                columnDefs: col,
                data: [],
                enableColumnResizing: true,
                enableSorting: true,
                showGridFooter: true,
                gridFooterTemplate: '<div style=\'text-align: right;padding-right: 165px\'><b>Total: {{grid.appScope.Total}} </b></div>',
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
                        $scope.IsSuccess = linkres.IsSuccess;

                        if (linkres.IsSuccess) {
                            gridApi.core.addToGridMenu(gridApi.grid, gridMenu);

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
                }

            };

            var paginationOptions = {
                pageNumber: 1,
                pageSize: 50,
                sort: null
            };

            $scope.setDate = function () {
                // var startDate1 = $scope.dateFrom;
                $scope.dateTo = $filter('date')(new Date($scope.dateFrom), 'yyyy-MM-dd');
                //$scope.$apply();

            };

            $scope.Search = function () {
                getPage();
            };

            var getPage = function () {
                $scope.Total = 0;

                var Attendance_Query = {};
                Attendance_Query.From = $scope.dateFrom || '';
                Attendance_Query.To = $scope.dateTo || '';
                Attendance_Query.Status = $scope.Status == 'All' ? '' : $scope.Status || '';
                Attendance_Query.CardNumber = $scope.CardNumber || '';
                Attendance_Query.EmployeeID = $scope.EmployeeID || '';
                Attendance_Query.Workplace = $scope.Workplace || '';

                HRServices.getInformation.GetAttendance({}, Attendance_Query).$promise.then(function (res) {
                    dataongrid = angular.toJson(res);
                    console.log(res);
                    $scope.Total = res.length
                    $scope.gridOptions.data = res;
                }, function (errormessage) {
                    Notifications.addError({ 'status': 'error', 'message': errormessage });
                });

            };

            HRServices.getInformation.GetWorkplaces({ "language": window.localStorage.lang }).$promise.then(function (res) {
                console.log(res);
                $scope.WorkplaceList = res;
                $scope.Workplace = res[1].WorkplaceID;
            }, function (errormessage) {
                Notifications.addError({ 'status': 'error', 'message': errormessage });
            });
        }])
        ;
});
