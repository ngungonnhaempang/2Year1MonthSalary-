
define(['myapp', 'angular'], function (myapp, angular) {
    myapp.directive('createWasteItem', ['$filter', '$http',
        '$routeParams', '$resource', '$location', '$interval',
        'Notifications', 'Forms', 'Auth', 'uiGridConstants', 'EngineApi',
        'GateGuest', '$translate', '$q', 'WasteItemService',
        function ($filter, $http, $routeParams,
            $resource, $location, $interval, Notifications, Forms, Auth, uiGridConstants,
            EngineApi, GateGuest, $translate, $q, WasteItemService) {
            return {
                restrict: 'E',
                controller: function ($scope) {
                    $scope.flowkey = 'HW-EHS'; //HW01
                    $scope.username = Auth.username;
                    formVariables = $scope.formVariables = [];
                    historyVariable = $scope.historyVariable = [];
                    /**
                     * Init Data to save
                     */
                    function saveInitData() {
                        var note = {};
                        var pricehistory=[];

                        note.WasteID=$scope.recod.WasteID;
                        note.ItemCode = $scope.recod.ItemCode || '';
                        note.WasteType =$scope.recod.WasteType;
                        note.WasteValue =$scope.recod.IsValuation;
                        note.Area= $scope.recod.Area;

                        note.MethodID = $scope.recod.method_id;
                        note.CompOriginID = $scope.recod.comp_originid;
                        note.State = $scope.recod.state || '';
                        note.WasteOriginID=$scope.recod.WasteID;
                        note.Description_TW = $scope.recod.NameCN || '';
                        note.Description_CN = $scope.recod.NameCN || '';
                        note.Description_VN = $scope.recod.NameVN || '';
                        note.Description_EN = $scope.recod.NameVN || '';
                        note.Status = $scope.recod.status || '1';
                        note.UnitPrice=$scope.priceHistory.Price || '0';
                        note.UserID= Auth.username||'';
                        note.Source=$scope.recod.Roots;
                        $scope.priceHistory.UserID=Auth.username;
                        $scope.priceHistory.ModifyDate=$filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss');
                        $scope.priceHistory.IP='';
                        $scope.priceHistory.ID='';
                        $scope.priceHistory.VoucherID='';
                        $scope.priceHistory.UnitPrice=$scope.priceHistory.UnitPrice||'';
                        pricehistory.push($scope.priceHistory);
                        note.ListPriceHistory =pricehistory;
                        return note;
                    }
                    function updateByID(data) {
                        WasteItemService.UpdateWasteItem(data, function (res) {
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
                    function SaveItem(data) {
                        WasteItemService.CreateWasteItem(data, function (res) {
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
                    $scope.savesubmit = function () {
                        var note = saveInitData();
                        var status = $scope.status;
                        switch (status) {
                            case 'N':
                                SaveItem(note);
                                break;
                            case 'M':
                                updateByID(note);
                                break;
                            default:
                                SaveItem(note);
                                break;
                        }

                    };

                },
                templateUrl: './forms/EHS/WasteItem/createWasteItem.html'
            }
        }]);
});