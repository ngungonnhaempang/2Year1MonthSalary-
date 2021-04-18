/**
 * Created by ptanh on 4/14/2018.
 */
define(['myapp', 'angular'], function (myapp, angular) {
    myapp.directive('createVoucher', ['VoucherService', 'Auth', '$q','$filter',
        function (VoucherService, Auth, $q,$filter) {
            return {
                restrict: 'E',
                controller: function ($scope) {
                    $scope.flowkey = 'HW-User'; //HW02

                    $scope.Creator = $scope.username = Auth.username;
                    $scope.recod.DateTransfer= $filter('date')(new Date(),'yyyy-MM-dd HH:mm:ss');
                    formVariables = $scope.formVariables = [];
                    historyVariable = $scope.historyVariable = [];

                    /**
                     * Init Data to save
                     */
                    function saveInitData() {

                        var note = {};
                        note.VoucherID = $scope.recod.voucher_id || '';
                        note.OWnerComp = $scope.recod.owner_comp || '';
                        note.ProcessComp = $scope.recod.process_comp || '';
                        note.VoucherNumber = $scope.recod.voucher_number || '';
                        note.DepartReq = $scope.recod.depart_req || '';
                        note.DepartProcess = $scope.recod.depart_process || '';
                        note.InternalPhone = $scope.recod.internal_phone || '';
                        note.Location = $scope.wasteItems[0].location || '';
                        note.DateOut = $scope.recod.DateTransfer || '';
                        note.DateComplete = $scope.recod.DateTransfer || '';
                        note.ReturnReason = '';//$scope.recod.return_reason;  
                        note.CreateTime = $scope.recod.create_time;
                        note.UserID = Auth.username;
                        note.Stamp = Date.now();
                        note.Status = $scope.status;
                        note.VehicleNo=$scope.recod.VehicleNO;
                        note.VoucherDetails = $scope.wasteItems;
                        note.VoucherWasteType= $scope.recod.WasteType;
                        note.GenUnitID= $scope.wasteItems[0].GUnitID;
                        note.Area=$scope.recod.Area;
                        note.Roots= $scope.recod.Roots;
                        return note;
                    }
                    /**
                     * Update status by updateByID
                     */
                    function updateByID(data) {
                        VoucherService.UpdateVoucher(data, function (res) {
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
                     * Save Voucher
                     */
                    function SaveVoucher(data) {
                        VoucherService.CreateVoucher(data, function (res) {
                            console.log(res)
                            if (res.Success) {
                                $scope.Search();
                                $('#myModal').modal('hide');
                                $('#messageModal').modal('hide');
                                $('#nextModal').modal('hide');
                            }
                            else {
                                Notifications.addError({ 'status': 'error', 'message': $translate.instant('saveError') + res.Message });
                            }

                        }, function (error) {
                            Notifications.addError({ 'status': 'error', 'message': $translate.instant('saveError') + error });
                        })
                    }
                    /**
                     *reset data function
                     */
                    $scope.reset = function () {
                        $scope.recod = {};
                        $scope.wasteItems = [];
                        $('#myModal').modal('hide');
                        $scope.Search();
                    }
                    /**
                     * save submit Voucher
                     */
                    $scope.saveSubmit = function () {
                        var note = saveInitData();
                        var status = $scope.status;
                        switch (status) {
                            case 'N':
                                SaveVoucher(note);
                                break;
                            case 'M':
                                updateByID(note);
                                break;
                            default:
                                SaveVoucher(note);
                                break;
                        }

                    };
                },
                templateUrl: './forms/EHS/Voucher/createVoucher.html'




            }
        }]);
});