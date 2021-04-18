define(['myapp', 'angular'], function (myapp, angular) {
    myapp.controller('VoucherDetailController', ['$filter', 'Notifications', 'Auth', 'EngineApi', 'VoucherService', '$translate', '$q', '$scope', '$routeParams',
        function ($filter, Notifications, Auth, EngineApi, VoucherService, $translate, $q, $scope, $routeParams) {

            var DateOut;
            /**
             * Report init
             */
            $scope.VoucherID = $routeParams.code; //param
            $scope.voucherItemDetail = [];
            $scope.voucher = {};
            var lang = window.localStorage.lang; //language
            /**Get voucher and voucher detail  */
            $q.all([loadHeader(), loadDetail()]).then(function (result) {
                console.log(result);
            }, function (error) {
                Notifications.addError({ 'status': 'Failed', 'message': 'Loading failed: ' + error });
            });
            function loadHeader() {
                var deferred = $q.defer();
                VoucherService.GetVoucher({ VoucherID: $scope.VoucherID,Lang:lang }, function (data) {
                    $scope.voucher = data[0];
                    $scope.date1 = moment(data[0].DateOut).format('DD/MM/YYYY');
                    $scope.date2 = 'ngày '+ moment(data[0].DateComplete).format("DD") + ' tháng ' +moment(data[0].DateComplete).format("MM")+ ' năm '+ moment(data[0].DateComplete).format("YYYY");
                }, function (error) {
                    deferred.reject(error);
                })
            }
            function loadDetail() {
                var deferred = $q.defer();
                VoucherService.GetVoucherDetail({ VoucherID: $scope.VoucherID ,lang:lang }, function (data) {
                   if(data.length<7){
                       for (var i =0;i<=(7-data.length);i++){
                           data.push([]);
                       }
                   }
                    $scope.voucherItemDetail = data;
                }, function (error) {
                    deferred.reject(error);
                })
            }
            $scope.Total = 0;
            $scope.CalculateSum= function (v) {
                if(v.Weight!=undefined){
                    $scope.Total += v.Weight;

                }

            }

            $scope.ResetTotalAmt = function (product) {
                $scope.TotalAmt =0;
            }
            /** PRINT REPORT */
            function PrintReport() {

                window.print();

            }
            
          //  $('.container').css({ 'padding-top': '0px' });
            $(document).ready(function () {
                setTimeout(function () {
                    window.print(); 
                }, 500);
            })

        }])
})
