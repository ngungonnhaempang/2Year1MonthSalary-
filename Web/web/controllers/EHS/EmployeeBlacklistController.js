/**
 * Created by Isaac 2019-08-07
 * 
 */
define(['myapp', 'angular'], function (myapp, angular) {
    myapp.controller("EmployeeBlacklistController", ['$scope', 'ConQuaService', '$translate', 'EngineApi', 'Auth','$filter','Notifications',
        function ($scope, ConQuaService, $translate, EngineApi, Auth,$filter,Notifications) {
            var lang = window.localStorage.lang;           
            $scope.status = 'N'
            $scope.flowkey = "GateContractorInfoProcess";
            $scope.query = {};
            $scope.black = {};
            blacklistEmployees =[];
            loadAllStatus();
            lockState();

            ConQuaService.GetContractors({}, (res) => {
                $scope.contractors = res;
            });
            function loadAllStatus() {
                let params = {
                    cType: "Contractor",
                    lang: lang || 'VN'
                }
                ConQuaService.ContractorStatus(params, (res) => {
                    $scope.contractorStatus = res;
                });
            }
            function lockState() {
                let params = {
                    cType: "Blacklist",
                    lang: lang || 'VN'
                }
                ConQuaService.ContractorStatus(params, (res) => {
                    $scope.locklistStates = res;
                });
            }
            $scope.loadEmployeesInContractor=()=>{
                let params = {
                    contractorID: $scope.black.ContractorID || ''
                }              
                ConQuaService.GetEmployeesInContractor(params, (res) => {
                    $scope.employees = res;
                });
            }

            /**
             * Define All Columns in UI Grid
             */
            var columns = [

                {
                    field: 'BlackID',
                    minWidth: 120,
                    displayName: $translate.instant('BlackID'),
                    cellTooltip: true

                },
                {
                    field: 'EmployeeName',
                    displayName: $translate.instant('EmployeeName'),
                    minWidth: 150,
                    cellTooltip: true
                },
                {
                    field: 'NationalIDNumber',
                    displayName: $translate.instant('NationalIDNumber'),
                    minWidth: 150,
                    cellTooltip: true
                },
                {
                    field: 'State',
                    displayName: $translate.instant('State'),
                    width: 100,
                    minWidth: 10
                },
                {
                    field: 'Phone',
                    minWidth: 100,
                    displayName: $translate.instant('Phone'),
                    cellTooltip: true
                },
                {
                    field: 'StartDate',
                    minWidth: 70,
                    displayName: $translate.instant('StartDate'),
                    cellTooltip: true
                },
                {
                    field: 'EndDate',
                    minWidth: 100,
                    displayName: $translate.instant('EndDate'),
                    cellTooltip: true
                },               
                {
                    field: 'Initiator',
                    displayName: $translate.instant('Owner'),
                    width: 100,
                    minWidth: 10
                },
                {
                    field: 'CreateTime',
                    displayName: $translate.instant('CreateTime'),
                    width: 100,
                    minWidth: 10
                }
            ];

            /**
             * Query Grid setting
             */
            $scope.gridOptions = {
                columnDefs: columns,
                data: [],
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
                paginationPageSize: 100,
                enableFiltering: false,
                exporterOlderExcelCompatibility: true,
                useExternalPagination: true,
                enablePagination: true,
                enablePaginationControls: true,
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    EngineApi.getTcodeLink().get({
                        'userid': Auth.username,
                        'tcode': $scope.flowkey
                    }, function (linkres) {
                        if (linkres.IsSuccess) {
                            gridApi.core.addToGridMenu(gridApi.grid, gridMenu);
                        }
                    });
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        $scope.selectedSupID = row.entity.SupID;
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        paginationOptions.pageNumber = newPage;
                        paginationOptions.pageSize = pageSize;
                        $scope.search();
                    });
                }
            };
            var gridMenu = [{
                    title: $translate.instant('Create'),
                    action: function () {
                        $scope.Refresh();
                        $scope.status = 'N';
                        $('#myModal').modal('show');
                    },
                    order: 1
                }, {
                    title: $translate.instant('Update'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        $scope.status = 'M'; //Set update Status
                        if (resultRows.length == 1 && resultRows[0].State != 'X') {
                            var id =resultRows[0].BlackID
                            let result = blacklistEmployees.filter(x=>x.BlackID == id)[0]; 
                           $scope.black = result;
                            $('#myModal').modal('show');
                          
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
                    title: $translate.instant('Delete'),
                    action: function () {
                        var resultRows = $scope.gridApi.selection.getSelectedRows();
                        if (resultRows.length == 1) {
                            if (confirm($translate.instant('Delete_IS_MSG') + ': ' + resultRows[0].ContractorName)) {
                                deleteContractorById(resultRows[0].ContractorID);
                            }
                        } else {
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant('Select_ONE_MSG')
                            });
                        }
                    },
                    order: 3

                }
            ];

            /*
             * Search Params 
             */

            function SearchParams() {
                let params = {};
                params.VoucherID = $scope.query.VoucherID || '';
                params.Type =  'E';
                params.ContractorID =  '';
                params.NationalIDNumber =  $scope.query.NationalIDNumber||'';
                params.B = $scope.query.StartDate ||'';
                params.E =  $scope.query.EndDate ||'';
                params.Status = $scope.query.status || '';
                params.UserID = $scope.onlyOwner ? Auth.username : '';
                params.Lang =  lang ||'VN';
                return params;
            }
            var getParams = () => {
                let params = {}
                params.BlackID = $scope.black.BlackID || ''
                params.BusinessID = $scope.black.EmployeeID || ''
                params.StartDate = $scope.black.StartDate || ''
                params.EndDate = $scope.black.EndDate || ''
                params.Initiator = Auth.username
                params.CreateTime = $scope.black.CreateTime
                params.SuspendedFlag = $scope.status ||''
                params.Reason = $scope.black.Reason ||''
                params.Phone = $scope.black.Phone ||''
                params.isLockStatus = $scope.black.isLockStatus || 'U'

                return params;
            }

            var saveComplete = () => {
                var localParams = getParams();  
                switch ($scope.status) {
                    case 'N':
                        addBlacklist(localParams);
                        break;
                    case 'M':
                        updateBlacklist(localParams);
                        break;
                    default:
                        Notifications.addError({
                            'status': 'error',
                            'message': $translate.instant('Your action didn\'t completed') + res.Message
                        });
                }
            }
            var addBlacklist = (p) => {
                ConQuaService.CreateBlacklistVoucher(p, (res) => {
                    if (res.Success) {
                        Notifications.addMessage({ 'status': 'information', 'message': $translate.instant('Save Completed') });
                        $scope.Refresh();
                    } else {
                        Notifications.addError({
                            'status': 'error',
                            'message': $translate.instant('saveError') + res.Message
                        });
                    }                  
                  
                })
            }
            var updateBlacklist = (p) => {
                ConQuaService.UpdateBlacklist(p, (res) => {
                    if (res.Success) {
                        Notifications.addMessage({ 'status': 'information', 'message': $translate.instant('Save Completed') });                       
                        $scope.Refresh();
                    } else {
                        Notifications.addError({
                            'status': 'error',
                            'message': $translate.instant('saveError') + res.Message
                        });
                    }        
                })
            }
            var deleteContractorById=(id)=>{
                ConQuaService.ChangeContractorStatus({id:id,status:'X'}, (res) => {
                    if (res.Success) {
                        Notifications.addMessage({ 'status': 'information', 'message': $translate.instant('Delete_Success_MSG') });
                        $timeout(function () { $scope.Search() }, 500);
                    } else {
                        Notifications.addError({
                            'status': 'error',
                            'message': $translate.instant('saveError') + res.Message
                        });
                    }
                })
            }
          
            /**
             * Needed for Submit voucher
             */
            $scope.saveSubmit = () => {
                saveComplete();
            }
            $scope.saveDraft = () => {
                saveComplete();
            }
            $scope.Refresh = () => {
                $scope.black = {};
                $('#myModal').modal('hide');                
                $scope.Search();
            }
            $scope.Search = function () {
                params = SearchParams();
                if ($scope.onlyOwner == true) {
                    $scope.Owner = Auth.username;
                }               
                ConQuaService.SearchBlacklistVoucher(params, function (res) {       
                    $scope.gridOptions.data = blacklistEmployees = res;

                });

            };
          
          $scope.Search();

        }
    ]);
});
