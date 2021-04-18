define(['myapp', 'angular'], function (myapp, angular) {
    myapp.controller("ContractorStatisticController", ['$scope', '$filter', 'Notifications', 'ConQuaService', '$translate',
        function ($scope, $filter, Notifications, ConQuaService, $translate) {
            $scope.IsStatistic = true;
            $scope.query = {};
            $scope.query.FromDate = $filter('date')(new Date(), "yyyy-MM-dd");
            $scope.query.ToDate = $filter('date')(new Date(), "yyyy-MM-dd");

            document.getElementById("btnViewLog").style.backgroundColor = "white";
            document.getElementById("btnStatistic").style.backgroundColor = "lightgreen";

            var param = {};
            param.language = window.localStorage.lang;
            param.contractorName = "";
            param.cType = "";
            param.departmentID = "";
            param.userid = "";
            param.status = "";
            ConQuaService.ContractorQualification().get(param).$promise.then(function (res) {
                $scope.ContractorList = res;
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            ConQuaService.GetContractorRegion().get({ language: window.localStorage.lang }).$promise.then(function (res) {
                $scope.RegionList = res;
                $scope.query.Region = "3";
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            $scope.$watch('query.Region', function (value) {
                if (value) {
                    if (value == "1") {
                        $scope.Region = "South Gate"
                        $scope.RegionName = "南廠 Xưởng Nam"
                    } else if (value == "2") {
                        $scope.Region = "North Gate"
                        $scope.RegionName = "北廠 Xưởng Bắc"
                    } else {
                        $scope.Region = ""
                        $scope.RegionName = "整個工廠 Toàn xưởng"
                    }
                }
            })

            $scope.viewLog = function () {
                $scope.IsStatistic = false;
                document.getElementById("btnViewLog").style.backgroundColor = "lightgreen";
                document.getElementById("btnStatistic").style.backgroundColor = "white";
            }


            $scope.statistic = function () {
                $scope.IsStatistic = true;
                document.getElementById("btnViewLog").style.backgroundColor = "white";
                document.getElementById("btnStatistic").style.backgroundColor = "lightgreen";
            }

            var colLog = [
                {
                    field: 'ContractorName',
                    displayName: $translate.instant("Contractor"),
                    minWidth: 250,
                    cellTooltip: true,
                    enableFiltering: false
                },
                {
                    field: 'IdCard',
                    displayName: $translate.instant("IdCard"),
                    minWidth: 120,
                    cellTooltip: true,
                    enableFiltering: false
                },

                {
                    field: 'Name',
                    displayName: $translate.instant("ConName"),
                    minWidth: 200,
                    cellTooltip: true,
                    enableFiltering: false
                },
                {
                    field: 'InOutDetail',
                    displayName: $translate.instant("HistorySwipeCard"),
                    minWidth: 300,
                    cellTooltip: true,
                    enableFiltering: false
                },
                {
                    field: 'CheckIn',
                    displayName: $translate.instant("InCount"),
                    minWidth: 150,
                    cellTooltip: true,
                    enableFiltering: false
                },
                {
                    field: 'CheckOut',
                    displayName: $translate.instant("OutCount"),
                    minWidth: 150,
                    cellTooltip: true,
                    enableFiltering: false
                },
                {
                    field: 'Remark',
                    displayName: $translate.instant("invial"),
                    minWidth: 150,
                    cellTooltip: true,
                    enableFiltering: false
                }
            ];

            $scope.gridOptionsLog = {
                columnDefs: colLog,
                data: [],
                enableColumnResizing: true,
                enableSorting: true,
                showGridFooter: true,
                enableGridMenu: true,
                exporterMenuPdf: false,
                enableSelectAll: false,
                enableRowHeaderSelection: true,
                enableRowSelection: true,
                multiSelect: false,
                exporterOlderExcelCompatibility: true,
                useExternalPagination: true,
                enableFiltering: true,
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                }
            };

            $scope.searchLog = function () {
                ConQuaService.SearchLog().get({
                    Contractor: $scope.query.ContractorName || "",
                    IdCard: $scope.query.IdCard || "",
                    startdate: $scope.query.FromDate || "",
                    enddate: $scope.query.ToDate || "",
                    Region: $scope.Region || "",
                    EmpName: $scope.query.EmpName || "",
                    vendor: $scope.query.Classify || ""
                }).$promise.then(function (res) {
                    $scope.gridOptionsLog.data = res;
                }, function (errResponse) {
                    Notifications.addError({
                        'status': 'error',
                        'message': errResponse
                    });
                });
            }

            $scope.exportLog = function () {
                ConQuaService.ExportLog().get({
                    Contractor: $scope.query.ContractorName || "",
                    IdCard: $scope.query.IdCard || "",
                    startdate: $scope.query.FromDate || "",
                    enddate: $scope.query.ToDate || "",
                    Region: $scope.Region || "",
                    RegionName: $scope.RegionName || "",
                    EmpName: $scope.query.EmpName || "",
                    vendor: $scope.query.Classify || ""
                }).$promise.then(function (res) {
                    console.log(res.FileName);
                    if (res.FileName) {
                        window.open('api/cmis/downContractorFile?filename=' + res.FileName);
                    } else {
                        Notifications.addMessage({
                            'status': 'information',
                            'message': $translate.instant('No_StatisticLog')
                        });
                    }

                }, function (errResponse) {
                    Notifications.addError({
                        'status': 'error',
                        'message': errResponse
                    });
                });
            }

            var colStatistic = [
                {
                    field: 'ContractorName',
                    displayName: $translate.instant("ContractorName"),
                    minWidth: 300,
                    cellTooltip: true,
                    enableFiltering: false
                },
                {
                    field: 'Department',
                    displayName: $translate.instant("Department"),
                    minWidth: 400,
                    cellTooltip: true,
                    enableFiltering: false
                },
                {
                    field: 'Issue_Card',
                    displayName: $translate.instant("Issue_Card"),
                    minWidth: 300,
                    cellTooltip: true,
                    enableFiltering: false
                },
                {
                    field: 'InOutCount',
                    displayName: $translate.instant("InOutCount"),
                    minWidth: 300,
                    cellTooltip: true,
                    enableFiltering: false
                }
            ];

            $scope.gridOptionsStatistic = {
                columnDefs: colStatistic,
                data: [],
                enableColumnResizing: true,
                enableSorting: true,
                showGridFooter: true,
                enableGridMenu: true,
                exporterMenuPdf: false,
                enableSelectAll: false,
                enableRowHeaderSelection: true,
                enableRowSelection: true,
                multiSelect: false,
                exporterOlderExcelCompatibility: true,
                useExternalPagination: true,
                enableFiltering: true,
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                }
            };


            $scope.searchStatistic = function () {
                ConQuaService.SearchStatistic().get({
                    Contractor: $scope.query.ContractorName || "",
                    startdate: $scope.query.FromDate || "",
                    enddate: $scope.query.ToDate || ""
                }).$promise.then(function (res) {
                    debugger
                    if (res.length > 0) {
                        for (let index = 0; index < res.length; index++) {

                            const obj = res[index];

                            Object.keys(obj).forEach(function (key) {
                                if (obj[key] === null) {
                                    obj[key] = '0';
                                }
                            })

                            if (index == res.length - 1) {
                                $scope.gridOptionsStatistic.data = res;
                                var column = [];
                                var columnName = Object.keys(res[0])
                                for (i = 1; i < columnName.length; i++) {
                                    const element = columnName[i];
                                    var col = {};
                                    col.field = element;
                                    col.displayName = $translate.instant(element);
                                    if (i == 3 || i == 4 || i == columnName.length - 1) {
                                        col.minWidth = 220;
                                    } else if (i > 4) {
                                        col.minWidth = 130;
                                    }
                                    else {
                                        col.minWidth = 400;
                                    }

                                    col.cellTooltip = true;
                                    col.enableFiltering = false;
                                    column.push(col);
                                    if (i == columnName.length - 1) {
                                        $scope.gridOptionsStatistic.columnDefs = column;
                                    }
                                }
                            }
                        }

                    }
                }, function (errResponse) {
                    Notifications.addError({
                        'status': 'error',
                        'message': errResponse
                    });

                });
            }

            $scope.exportStatistic = function () {
                ConQuaService.ExportStatistic().get({
                    Contractor: $scope.query.ContractorName || "",
                    startdate: $scope.query.FromDate || "",
                    enddate: $scope.query.ToDate || ""
                }).$promise.then(function (res) {
                    console.log(res.FileName);
                    if (res.FileName) {
                        window.open('api/cmis/downContractorFile?filename=' + res.FileName);
                    } else {
                        Notifications.addMessage({
                            'status': 'information',
                            'message': $translate.instant('No_StatisticLog')
                        });
                    }
                }, function (errResponse) {
                    Notifications.addError({
                        'status': 'error',
                        'message': errResponse
                    });
                });
            }
        }
    ])
})
