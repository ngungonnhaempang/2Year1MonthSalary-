define(['myapp', 'angular'], function (myapp) {
    myapp.controller('QualifiedReportController', ['$scope', '$filter', '$compile', '$routeParams', '$resource', '$location',
        'i18nService', 'Notifications', 'Auth', 'uiGridConstants', '$http',
        '$translatePartialLoader', '$translate', 'LIMSBasic', 'LIMSService', 'EngineApi', 'GateGuest', '$q',
        function ($scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, Auth, uiGridConstants, $http, $translatePartialLoader, $translate, LIMSBasic, LIMSService, EngineApi, GateGuest, $q) {
            /**init */
            var username = Auth.username;
            $scope.recod = {};
            $scope.flowKey = 'QCOverGrade';
            $scope.UQList = [];
            $scope.plansHeader = {};
            $scope.today = $filter('date')(new Date(), "yyyy-MM-dd");
            $scope.today2 = $filter('date')(new Date(), "yyyyMMdd");

            /**This part only run in "printRedUQ". Check myapp.js  */

            LIMSService.ISOQualify.GetDetailReport({ voucherID: $routeParams.VoucherID }, function (data) {
                var plansHeader = [];
                $scope.plansHeader = [];
                if (data.length > 0) {

                    $scope.UQList = data;
                    var plansHeader = [];
                    for (var key in data[0]) {
                        if (['ValueSpec', 'VoucherID', 'ColorLabel', 'State', 'VoucherNO',
                            'CreateDate', 'BeginDate', 'EndDate', 'Stamp', 'Status',
                            'Reason', 'Solution', 'Prevention', 'Remark', 'CreateBy', 'SampleName', 'LINE', 'LOT_NO'
                            ,'DepartmentID','Specification_EN','Specification_CN'
                            ,'Description_CN','Description_EN','Vendor','TypeID'].indexOf(key) < 0 && key.indexOf('$') < 0) {
                            if($scope.recod.SampleName!='S03020001'){
                                if(['Group_name','RangeBarCode','TotalBag'].indexOf(key) < 0 && key.indexOf('$') < 0){
                                    plansHeader.push(key);
                                }
                            }else{
                                plansHeader.push(key);
                            }

                        }
                    }

                    if (data[0].SampleName == 'S01020002')
                        $scope.vdepart = 'SSP';
                    else $scope.vdepart = 'POLY';
                    if (data[0].ColorLabel=='Red') $scope.isRed=true;
                    $scope.plansHeader = plansHeader;

                    $scope.BeginDate =$filter('date')(new Date(data[0].BeginDate),'yyyy-MM-dd'); //printRedUQ date of setting
                    $scope.recod = data[0];
                    $scope.GetInformation($scope.recod.VoucherID);

                }
            });

            /**Auto  printing */
            setTimeout(function () {
                window.print();
            }, 3000);
            /** Information from MongoDB . Get who receive the voucher and approved it */
            $scope.GetInformation = function (voucherID) {
                LIMSService.QCOverGradePID().get({ OverID: voucherID }).$promise.then(function (res) {
                    console.log(res);
                    if (res) {
                        EngineApi.getProcessLogs.getList({ "id": res.ProcessInstanceId, "cId": "" }, function (data) {

                            console.log(data[0].Logs);
                            var receiver = [];
                            data[0].Logs = $filter('orderBy')(data[0].Logs, 'FormatStamp');
                            var taf = TAFFY(data[0].Logs);
                            if(data[1] !=null || data[1] != undefined){
                                var taf_1 = TAFFY(data[1].Logs);
                                receiver[0] = taf({ TaskName: "起始表单" }).first() || taf_1({ TaskName: "起始表单" }).first(); //initiator
                                receiver[1] = taf({ TaskName: "Production" }).first() ||taf_1({ TaskName: "Production" }).first(); //Product team
                            }else{
                                receiver[0] = taf({ TaskName: "起始表单" }).first(); //initiator
                                receiver[1] = taf({ TaskName: "Production" }).first(); //Product team
                            }

                            var count = taf({ TaskName: "Production leader", }).get().length;

                            if($scope.recod.TypeID =='5' ||$scope.recod.TypeID =='3'){
                                if ($scope.recod.Status.indexOf(['S', 'X'] >= 0)) {
                                    receiver[2] = taf({ TaskName: "Production leader", }).start(1).first();//QCmanager (if published)
                                    receiver[3] = taf({ TaskName: "Production leader", }).start(2).first();//QCleader (if published)
                                    receiver[4] = taf({ TaskName: "Production leader", }).start(count-1).first();//QCleader (if published)
                                }
                            }else{
                                if ($scope.recod.Status.indexOf(['S', 'X'] >= 0)) {
                                    if($scope.recod.SampleName=='S03020001'){
                                        receiver[2] = taf({ TaskName: "Production leader", }).start(1).first();//QCmanager (if published)
                                        receiver[3] = taf({ TaskName: "Production leader", }).start(1).first();//QCleader (if published)
                                        receiver[4] = taf({ TaskName: "Production leader", }).start(1).first();//QCleader (if published)
                                    }
                                    else{

                                        receiver[2] = taf({ TaskName: "Production leader", }).start(2).first();//QCmanager (if published)
                                        receiver[3] = taf({ TaskName: "Production leader", }).start(1).first();//QCleader (if published)
                                        receiver[4] = taf({ TaskName: "Production leader", }).start(count-1).first();//QCleader (if published)
                                    }


                                }
                            }
                            receiver[5] = taf({ TaskName: "ISO", }).start(1).first();//ISO (if published)
                            $scope.receiver = receiver;



                        })
                    }

                }, function (err) {
                    Notifications.addError({
                        'status': 'error',
                        'message': err
                    });
                })
            }

        }
    ]);

    /**MONTHLY PRINT CONTROLLER */
    myapp.controller('PrintAllUQReportController', ['$scope', '$filter', '$compile', '$routeParams', '$resource', '$location',
        'i18nService', 'Notifications', 'Auth', 'uiGridConstants', '$http',
        '$translatePartialLoader', '$translate', 'LIMSBasic', 'LIMSService', 'EngineApi',
        function ($scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, Auth, uiGridConstants, $http, $translatePartialLoader, $translate, LIMSBasic, LIMSService, EngineApi) {
            /**Init */
            $scope.user = Auth.username + ' - ' + Auth.nickname;
            var lang = window.localStorage.lang || 'EN';

            /**Get information */
            LIMSService.ISOQualify.GetAllUQReport(
                { b: $routeParams.from
                    , e: $routeParams.end,
                    sampleName:'',
                    Lot_no:'',
                    status:'',
                    colorlabel:'',
                    Line: '' },
                function (data) {
                    $scope.monthUQ = data;
                    $scope.today = $filter('date')($routeParams.from, "yyyy '年Năm ' MM  '月Tháng'");
                })

            /** Change status from code to translated words */
            var StatusList = {};
            LIMSBasic.GetStatus({
                ctype: 'Grade',
                lang: lang
            }, function (data) {
                StatusList = data;
            });
            $scope.getStatus = function (Status) {
                var statLen = $filter('filter')(StatusList, {
                    'State': Status
                });
                if (statLen.length > 0) {
                    return statLen[0].StateSpec;
                } else {
                    return Status;
                }
            };

        }
    ]);
});
