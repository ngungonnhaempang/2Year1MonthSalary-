define(['app'], function (app) {
    app.directive('improvementCreport', ['CReportService', 'InfolistService', 'Auth', '$timeout', 'Notifications', '$translate', '$upload','$q',
        function (CReportService, InfolistService, Auth, $timeout, Notifications, $translate, $upload,$q ) {
            return {
                restrict: 'E',
                controller: function ($scope, $element, $attrs) {
                    $scope.listfile = [];
                    // Notifications.addError({
                    //     'status': 'error',
                    //     'message': $translate.instant('Update_onlyowner_MSG')
                    // });
                    $scope.ActionEvaluateList = [];
                    function getActionEvaluate(type){
                        var deferred = $q.defer();

                        CReportService.G_ActionEvaluate({Rp_ID:$scope.variable.Rp_ID},function (res) {
                            if(res){
                                deferred.resolve(res);
                            }
                        },function (err) {
                            deferred.reject(err);
                        })

                        return deferred.promise;
                    }
                    $scope.LoadImprovementInfo = function (rp_id) {
                        $scope.ImprovementRecord = {};
                        $scope.isShowDate =false;
                        CReportService.FindByID({
                            Rp_ID: $scope.variable ? $scope.variable.Rp_ID : rp_id
                        }, function (data) {
                            $scope.listfileAC = [];
                            $scope.listfile = [];
                            if (new Date(data.Estimate_DateComplete) >= new Date() || $scope.show.isHSEUser) {
                                $scope.ImprovementRecord = data;
                                $scope.ImprovementRecord.Estimate_DateComplete =moment(data.Estimate_DateComplete).format('YYYY-MM-DD');
                                data.FileAttached.forEach(element => {
                                    var x = {};
                                    x.Rp_ID = element.Rp_ID;
                                    x.name = element.File_ID;
                                    x.col = element.ColumnName;
                                    $scope.listfile.push(x);
                                })
                                $q.all([getActionEvaluate(data.Rp_Type)]).then(function (res) {
                                    $scope.ActionEvaluateList= res[0];
                                    $timeout(function () {
                                        $scope.datepickerMax = Math.ceil((new Date( $scope.ImprovementRecord.Estimate_DateComplete) - new Date()) / (1000 * 60 * 60 * 24));
                                        $scope.isShowDate = true;
                                        $scope.$apply();
                                        $('#modal_Improvement').modal('show');
                                    }, 0);


                                })

                            } else {
                                $timeout(function () {
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': $translate.instant('Date to complete Improvement are out: ' + data.RpAC_DateComplete || '.')
                                    });
                                }, 400);
                                return false;
                            }
                        }, function (error) {});
                        // } else {
                        //     Notifications.addError({
                        //         'status': 'error',
                        //         'message': $translate.instant('Select_ONE_MSG')
                        //     });
                        // }
                    }

                    $scope.btnImprovementSave = function (myrecord) { //Improvement Save button functions
                        var templsFile = [];
                        if ($scope.listfile.length > 0) {
                            $scope.listfile.forEach(element => {
                                templsFile.push({
                                    File_ID: element.name,
                                    ColumnName: element.col,
                                    Rp_ID: myrecord.Rp_ID
                                });
                            })
                        }

                        myrecord.FileAttached = templsFile; //File list
                        console.log(myrecord);
                        CReportService.GetInfoBasic.Update(myrecord, function (res) {
                                if (res.Success) {
                                    $('#modal_Improvement').modal('hide');
                                    $scope.LoadDetail(res.Data);
                                    $scope.update_msg();
                                } else {
                                    $scope.update_error_msg();
                                }
                            },
                            function (error) {
                                $scope.update_error_msg(error);
                            })
                    };
                },
                templateUrl: './forms/EHS/CReport/updateImprovement.html'
            }
        }
    ]);




    app.directive('evaluateUpdateCreport', ['CReportService', 'InfolistService', 'Auth', '$timeout', 'Notifications', '$translate', '$upload','$q',
        function (CReportService, InfolistService, Auth, $timeout, Notifications, $translate, $upload,$q ) {
            return {
                restrict: 'E',
                controller: function ($scope, $element, $attrs) {
                    $scope.listfile = [];
                    // Notifications.addError({
                    //     'status': 'error',
                    //     'message': $translate.instant('Update_onlyowner_MSG')
                    // });

                    $scope.action={};
                    $scope.action.EvaluateDate= moment(new Date()).format('YYYY-MM-DD');
                    $scope.checkboxAction={};
                    // $scope.$watch("checkboxAction", function(nv,ov){
                        angular.forEach($scope.ActionEvaluateList, function(opt){
                            debugger
                            if(nv[opt.ID]){
                                opt.checked = nv[opt.ID] ? nv[opt.ID]  : false;
                            }


                        })
                    // }, true)
                    $scope.ActEvaChange = function(obj){
                        debugger
                        angular.forEach($scope.ActionEvaluateList, function(opt){
                            debugger
                            if(opt.ID==obj){
                                var ischeck= opt.checked;
                                if(ischeck=='true'){
                                    opt.checked='false';
                                }else{
                                    opt.checked='true';
                                }

                            }


                        })
                    }

                    $scope.ActionEvaluateList = [];
                    function getActionEvaluate(type){
                        var deferred = $q.defer();

                        CReportService.G_ActionEvaluate({Rp_ID:type},function (res) {
                            if(res){
                                deferred.resolve(res);
                            }
                        },function (err) {
                            deferred.reject(err);
                        })

                        return deferred.promise;
                    }
                    $scope.LoadImprovementInfo = function (rp_id) {
                        $scope.EvaluateRecord = {};
                        $scope.isShowDate =false;
                        CReportService.FindByID({
                            Rp_ID: $scope.variable ? $scope.variable.Rp_ID : rp_id
                        }, function (data) {
                            $scope.listfileAC = [];
                            $scope.listfile = [];
                            if (new Date(data.Estimate_DateComplete) >= new Date() || $scope.show.isHSEUser) {
                                $scope.EvaluateRecord = data;
                                $scope.EvaluateRecord.Estimate_DateComplete =moment(data.Estimate_DateComplete).format('YYYY-MM-DD');
                                data.FileAttached.forEach(element => {
                                    var x = {};
                                x.Rp_ID = element.Rp_ID;
                                x.name = element.File_ID;
                                x.col = element.ColumnName;
                                $scope.listfile.push(x);
                            })
                                $q.all([getActionEvaluate(data.Rp_ID)]).then(function (res) {
                                    $scope.ActionEvaluateList= res[0];
                                    $timeout(function () {
                                        $scope.datepickerMax = Math.ceil((new Date( $scope.EvaluateRecord.Estimate_DateComplete) - new Date()) / (1000 * 60 * 60 * 24));
                                        $scope.isShowDate = true;
                                        $scope.$apply();
                                        $('#modal_Evaluate').modal('show');
                                    }, 0);


                                })

                            } else {
                                $timeout(function () {
                                    Notifications.addError({
                                        'status': 'error',
                                        'message': $translate.instant('Date to complete Improvement are out: ' + data.RpAC_DateComplete || '.')
                                    });
                                }, 400);
                                return false;
                            }
                        }, function (error) {});
                        // } else {
                        //     Notifications.addError({
                        //         'status': 'error',
                        //         'message': $translate.instant('Select_ONE_MSG')
                        //     });
                        // }
                    }

                    $scope.btnEvaluate = function (myrecord) { //Evaluate Save button functions
                        var templsFile = [];
                        if ($scope.listfile.length > 0) {
                            $scope.listfile.forEach(element => {
                                templsFile.push({
                                    File_ID: element.name,
                                    ColumnName: element.col,
                                    Rp_ID: myrecord.Rp_ID
                                });
                        })
                        }

                        var ActEvaluateArr =[];

                        angular.forEach($scope.ActionEvaluateList, function(opt){
                           if(opt.checked=='true'){
                               ActEvaluateArr.push(opt.ID);
                           }
                        })

                        myrecord.FileAttached = templsFile; //File list
                        myrecord.ActionEvaluateDate=$scope.action.EvaluateDate;
                        myrecord.ActionEvaluate =  ActEvaluateArr.join();
                        console.log(myrecord);
                        CReportService.GetInfoBasic.Update(myrecord, function (res) {
                                if (res.Success) {
                                    $('#modal_Evaluate').modal('hide');
                                    $scope.LoadDetail(res.Data);
                                    $scope.update_msg();
                                } else {
                                    $scope.update_error_msg();
                                }
                            },
                            function (error) {
                                $scope.update_error_msg(error);
                            })
                    };
                },
                templateUrl: './forms/EHS/CReport/updateEvaluate.html'
            }
        }
    ]);
});
