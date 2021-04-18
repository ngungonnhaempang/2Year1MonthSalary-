//marco history direcive
//elro current grade 
define(['app', 'bpmn'], function (app) {
    app.directive('showHistoryGrades', ['$resource', 'EngineApi', 'LIMSService','Notifications','$filter','$timeout', function ($resource, EngineApi, LIMSService,Notifications,$filter,$timeout) {
        return {
            restrict: 'EAC',
            link: function (scope, element, attrs,$rootScope) {
                scope.linkClick = function (data, header) {
                    scope.dataLinkClick = data;
                    scope.HeaderName = header;
                    $('#showPropertyDetails').modal('show');
                };
                scope.reload = false;
                scope.header = {};
                scope.finalData = {};
                scope.data={};
                var mydata = JSON.parse(attrs.test);
                scope.ID = mydata.ID
                scope.Report = function(data,id) {
                    scope.ValidDate= data;
                    scope.ID= id;
                    LIMSService.gradeVersion.HistoryforGrade_Report({
                        sampleName: mydata.sampleName.SampleName,
                        lotNo: mydata.lotNo,
                        ValidDate: data
                    }).$promise.then(function (res) {
                        console.log(res);
                        var headers = [];
                        if (res.length > 0) {
                            for (var key in res[0]) {
                                if (['Status','Remark'].indexOf(key) < 0 && key.indexOf(['$'])<0)
                                    headers.push(key);
                            }

                            scope.headerModal = headers;
                            scope.MaxVersionModal = TAFFY(res)({ Grade: 'A' }).max('VersionSpc');
                            scope.ActualLengthData = res.length;

                            scope.CreatorRemark = res[0].Remark;
                            scope.finalDataModal = res;
                            scope.templateData = res;
                            scope.reload = true;
                        }
                        else
                            Notifications.addError({
                                'status': 'error',
                                'message': 'There is no data'
                            });

                    });

                    LIMSService.QCGradesPID().get({ ID: scope.ID }).$promise.then(function (res) {
                        console.log(res);
                        if (res) {

                            EngineApi.getProcessLogs.getList({
                                "id": res.ProcessInstanceId,
                                "cId": ""
                            }, function (data) {
                                console.log(data[0].Logs);
                                data.forEach(function (value, index) {
                                    if (index >= 1)
                                        data[0].Logs.push.apply(data[0].Logs, data[index].Logs)
                                })
                                var receiver = [];
                                data[0].Logs = $filter('orderBy')(data[0].Logs, 'FormatStamp');
                                var taf = TAFFY(data[0].Logs);

                                for(var i =0;i<taf().get().length;i++){
                                    var test = taf().get()[i];
                                    receiver[i]= taf().get()[i];
                                }
                                // receiver[0] = taf({
                                // TaskName: "起始表单"
                                // }).first(); //initiator
                                // receiver[1] = taf({
                                // TaskName: "Leader check"
                                // }).first(); //Leader check
                                // receiver[2] = taf({
                                // TaskName: "Check Grade"
                                // }).first(); //Check Grade
                                // receiver[3] = taf({
                                // TaskName: "QCLeader Check"
                                // }).first(); //PQCLeader Check
                                scope.receiver = receiver;
                                var res =scope.templateData.filter((el) => {
                                    return (el.Grade !=undefined && el.Grade !='')
                            });

                                var extend ={};
                                extend.PropertyName= "Remark";
                                extend.Remark= scope.CreatorRemark;
                                res.push(extend);
                                if(scope.receiver!=undefined && scope.receiver.length >0){
                                    for (var i=0;i<scope.receiver.length;i++) {
                                        var item =    scope.receiver[i]['HistoryField'];
                                        var RemarkHeader = scope.receiver[i];
                                        for(var j =0 ;j< item.length;j++){
                                            var subItem = item[j];
                                            if(subItem.Name=='AgreeReason'){
                                                var extend ={};
                                                extend.PropertyName= "Remark "+ RemarkHeader.TaskName;
                                                extend.Remark= subItem.Value;

                                                res.push(extend);
                                            }
                                        }
                                    }
                                }

                                scope.finalDataModal = res;
                                $('#myModalGradeHistory').modal('show');

                                console.log(receiver);
                            })
                        }

                    }, function (err) {
                        Notifications.addError({
                            'status': 'error',
                            'message': err
                        });
                    })

                }


                scope.Close= function(){
                    $('#myModalGradeHistory').modal('hide');
                    $('#showPropertyDetails').modal('hide');
                    scope.reload = false;
                    $timeout(function () {
                        scope.reload = true;
                        scope.finalDataModal =[];
                        scope.$apply();

                    }, 0);
                }
                scope.print = function () {
                    scope.reload = false;
                    var res =scope.templateData.filter((el) => {
                        return (el.Grade !=undefined && el.Grade !='')
                });

                    var extend ={};
                    extend.PropertyName= "Remark";
                    extend.Remark= scope.CreatorRemark;
                    res.push(extend);
                    if(scope.receiver!=undefined && scope.receiver.length >0){
                        for (var i=0;i<scope.receiver.length;i++) {
                            var item =    scope.receiver[i]['HistoryField'];
                            var RemarkHeader = scope.receiver[i];
                            for(var j =0 ;j< item.length;j++){
                                var subItem = item[j];
                                if(subItem.Name=='AgreeReason'){
                                    var extend ={};
                                    extend.PropertyName= "Remark "+ RemarkHeader.TaskName;
                                    extend.Remark= subItem.Value;

                                    res.push(extend);
                                }
                            }
                        }
                    }
                    if(res.length <= 10){
                        var length = 35-res.length;
                        for (var i =0;i<length;i++){ // 1 page

                            res.push([]);
                        }
                    }
                    else if(res.length >10 && res.length<=25){// 1 page
                        var length = 35-res.length;
                        for (var i =0;i<length;i++){

                            res.push([]);
                        }
                    }else if(res.length >25 && res.length <=40){ // 2 pages

                        for (var i =0;i<=10;i++){
                            var index = 25 + i;
                            res.splice(index,0,[])
                        }
                        var length = 75-res.length;
                        for (var i =0;i<length;i++){

                            res.push([]);
                        }
                    }else if(res.length >40){ // 2 pages
                        for (var i =0;i<=10;i++){
                            var index = 25 + i;
                            res.splice(index,0,[])
                        }
                        var length = 75-res.length;
                        for (var i =0;i<length;i++){

                            res.push([]);
                        }
                    }

                    scope.finalDataModal = res;


                    $timeout(function () {
                        scope.reload = true;
                        scope.$apply();
                        printElement(document.getElementById("printThis"));
                        window.print();
                        var res = scope.templateData.filter((el) => {
                            return (el.Grade !=undefined && el.Grade !='')

                    })
                        var extend ={};
                        extend.PropertyName= "Remark";
                        extend.Remark= scope.CreatorRemark;
                        res.push(extend);
                        if(scope.receiver!=undefined && scope.receiver.length >0){
                            for (var i=0;i<scope.receiver.length;i++) {
                                var item =    scope.receiver[i]['HistoryField'];
                                var RemarkHeader = scope.receiver[i];
                                for(var j =0 ;j< item.length;j++){
                                    var subItem = item[j];
                                    if(subItem.Name=='AgreeReason'){
                                        var extend ={};
                                        extend.PropertyName= "Remark "+ RemarkHeader.TaskName;
                                        extend.Remark= subItem.Value;

                                        res.push(extend);
                                    }
                                }
                            }
                        }

                        scope.finalDataModal = res;
                    }, 0);

                }
                LIMSService.gradeVersion.HistoryOfGradeVersion({
                    sampleName: mydata.sampleName.SampleName,
                    lotNo: mydata.lotNo,
                    grades: mydata.grades
                }).$promise.then(function (res) {
                    var headers = [];
                    if (res.length > 0) {
                        for (var key in res[0]) {
                            if (['Status','id'].indexOf(key) < 0 && key.indexOf(['$'])<0)
                                headers.push(key);
                        }
                        scope.header = headers;
                        scope.MaxVersion = TAFFY(res)({ Status: 'S' }).max('Version');
                        scope.finalData = res;

                        scope.RawData = mydata;
                        console.log(res);
                        console.log(scope.MaxVersion);
                    }
                    else
                        Notifications.addError({
                            'status': 'error',
                            'message': 'There is no data'
                        });
                });
                function printElement(elem, append, delimiter) {
                    var domClone = elem.cloneNode(true);

                    var $printSection = document.getElementById("printSection");

                    if (!$printSection) {
                        var $printSection = document.createElement("div");
                        $printSection.id = "printSection";
                        document.body.appendChild($printSection);
                    }

                    if (append !== true) {
                        $printSection.innerHTML = "";
                    }

                    else if (append === true) {
                        if (typeof(delimiter) === "string") {
                            $printSection.innerHTML += delimiter;
                        }
                        else if (typeof(delimiter) === "object") {
                            $printSection.appendChlid(delimiter);
                        }
                    }

                    $printSection.appendChild(domClone);
                }
                // LIMSService.QCGradesPID().get({ ID: scope.ID }).$promise.then(function (res) {
                //     console.log(res);
                //     if (res) {
                //
                //         EngineApi.getProcessLogs.getList({
                //             "id": res.ProcessInstanceId,
                //             "cId": ""
                //         }, function (data) {
                //             console.log(data[0].Logs);
                //             data.forEach(function (value, index) {
                //                 if (index >= 1)
                //                     data[0].Logs.push.apply(data[0].Logs, data[index].Logs)
                //             })
                //             var receiver = [];
                //             var taf = TAFFY(data[0].Logs);
                //             receiver[0] = taf({
                //                 TaskName: "起始表单"
                //             }).first(); //initiator
                //             receiver[1] = taf({
                //                 TaskName: "Leader check"
                //             }).first(); //Leader check
                //             receiver[2] = taf({
                //                 TaskName: "Check Grade"
                //             }).first(); //Check Grade
                //             receiver[3] = taf({
                //                 TaskName: "QCLeader Check"
                //             }).first(); //PQCLeader Check
                //             scope.receiver = receiver;
                //             console.log(receiver);
                //         })
                //     }
                //
                // }, function (err) {
                //     Notifications.addError({
                //         'status': 'error',
                //         'message': 'Không có lưu trình'
                //     });
                // })


            },
            templateUrl: '/forms/QCGrades/gradeHistory.html'
        }
    }]);
    app.directive('tooltipGradeVersion', ['$filter', function ($filter) {
        return {
            restrict: 'AEC',
            link: function (scope, element, attrs) {
                if (attrs.tooltipGradeVersion != undefined) {
                    var Property = scope.$eval(attrs.tooltipGradeVersion);
                    if (Property != undefined) {
                        if (Property.MaxValue != undefined) {
                            var Max = Property.MaxValue == '' ? '' : 'Max Value: ' + $filter('number')(Property.MaxValue, Property.Prec);
                            var min = Property.MinValue == '' ? '' : '\nMin Value: ' + $filter('number')(Property.MinValue, Property.Prec);
                            var enable = '\nEnable: ' + Property.Enable;
                            element
                                .attr('title', Max + min + enable);
                        }
                    }
                } else {
                    //element
                    //    .attr('title', 'Max Value: ' + '0' + '\nMin Value: ' + '0');
                }
            }
        }
    }]);
    app.directive('showHistoryProcess', ['$resource', 'EngineApi', 'LIMSService', 'LIMSBasic', 'Auth', '$filter', function ($resource, EngineApi, LIMSService, LIMSBasic, Auth, $filter) {
        return {
            restrict: 'AEC',
            link: function (scope, element, attrs) {
                scope.GradeMaterial = [];
                scope.lang = window.localStorage.lang;
                LIMSService.gradeVersion.GetGradeProcessing({
                    id: attrs.voucherId
                }).$promise.then(function (res) {
                    scope.Material = 'MaterialDescription_' + scope.lang;
                    scope.SampleNames = 'SampleDescription_' + scope.lang;
                    scope.Grade = res;
                    if (scope.Grade.Grades.length > 0) {
                        for (var i = 0; i < scope.Grade.Grades.length; i++) {
                            scope.Grade.Grades[i].ValidDate = $filter('date')(scope.Grade.Grades[i].ValidDate, 'yyyy-MM-dd hh:mm:ss');
                            scope.Grade.Grades[i].Enable = scope.Grade.Grades[i].Enable.toLowerCase();
                        }
                    }
                    scope.dataTest = {
                        'sampleName': scope.Grade.SampleName,
                        'lotNo': scope.Grade.LOT_NO,
                        'grades': scope.Grade.Grade
                    };
                });
            },
            templateUrl: 'forms/QCGrades/GradeInProcessing.html'
        };
    }]);
    app.directive('numberOnly', ['$filter', function ($filter) {
        return {
            restrict: 'A',
            link: function (scope, elm, attrs, ctrl) {
                elm.on('keydown', function (event) {
                    if (event.which == 64 || event.which == 16) {
                        // to allow numbers
                        return false;
                    } else if (event.which >= 48 && event.which <= 57) {
                        // to allow numbers
                        return true;
                    } else if (event.which >= 96 && event.which <= 105) {
                        // to allow numpad number
                        return true;
                    } else if ([8, 13, 27, 37, 38, 39, 40, 110, 190, 16, 36, 35, 189, 109, 46].indexOf(event.which) > -1) {
                        // to allow backspace, enter, escape, arrows
                        return true;
                    } else {
                        event.preventDefault();
                        // to stop others
                        return false;
                    }
                });
            }
        }
    }]);
    app.directive('limitNumber', ['$filter', function ($filter) {
        return {
            restrict: 'A',
            link: function (scope, elm, attrs, ctrl) {
                elm.on('keydown', function (event) {
                    if (event.which >= 96 && event.which <= 104) {
                        // to allow numbers
                        return true;
                    } else if (event.which >= 96 && event.which <= 105) {
                        // to allow numpad number
                        return true;
                    } else if (event.which >= 48 && event.which <= 56) {
                        // to allow numbers
                        return true;
                    } else if ([8, 13, 27, 37, 38, 39, 40, 110, 190, 16, 36, 35, 46].indexOf(event.which) > -1) {
                        // to allow backspace, enter, escape, arrows
                        return true;
                    } else {
                        event.preventDefault();
                        // to stop others
                        return false;
                    }
                });
            }
        }
    }]);


    /**DIRECTIVE FOR OVERGRADE (QUALIFED CONTROL) REPORTS AND APPROVATION FORM */
    app.directive('approvalMaster', ['LIMSService', 'Auth', '$q', '$filter', '$routeParams', 'Notifications', 'EngineApi',
        function (LIMSService, Auth, $q, $filter, $routeParams, Notifications, EngineApi) {
            return {
                restrict: 'AEC',
                link: function (scope, element, attrs) {
                    /**Init scope*/
                    scope.lang = window.localStorage.lang;
                    scope.UQList = [];
                    scope.plansHeader = {};
                    scope.today = $filter('date')(new Date(), "yyyy-MM-dd");
                    scope.username = Auth.username;
                    //  scope.sampleName = 'aaaaa';
                    /* Submit into BPMN **/
                    LIMSService.ISOQualify.GetDetailReport({
                        voucherID: $routeParams.VoucherID ? $routeParams.VoucherID : attrs.vid
                    }, function (data) {
                        console.log(data);
                        var plansHeader = [];
                        scope.plansHeader = [];
                        if (data.length > 0) {
                            scope.UQList = data;
                            /**add Header of the table */
                            var plansHeader = [];
                            for (var key in data[0]) {
                                if (['VoucherID', 'ColorLabel', 'State', 'VoucherNO',
                                    'CreateDate', 'BeginDate', 'EndDate', 'Stamp', 'Status',
                                    'Reason', 'Solution', 'Prevention', 'Remark', 'CreateBy', 'SampleName', 'LOT_NO', 'LINE','DepartmentID','Vendor',
                                    'Description_CN','Description_EN','Specification_EN','Specification_CN','TypeID'
                                ].indexOf(key) < 0 && key.indexOf('$') < 0) {
                                    if(data[0].Status === 'N'){
                                        if(data[0].SampleName ==='S03020001'  && data[0].ColorLabel.toLowerCase() ==='red' && key!=='TotalBag'){

                                            plansHeader.push(key);

                                        }else{
                                            if( key!=='TotalBag'&&key!=='RangeBarCode' && key !=='Group_name'){
                                                plansHeader.push(key);
                                            }

                                        }
                                    }else
                                    {
                                        if(data[0].SampleName ==='S03020001'  && data[0].ColorLabel.toLowerCase() ==='red'){
                                            if( key=='PropertyName'||key=='RangeBarCode' || key=='TotalBag' ||key =='Group_name'){
                                                plansHeader.push(key);

                                            }


                                        }else{
                                            if( key!=='RangeBarCode' && key!=='TotalBag'&& key!=='Group_name'){
                                                plansHeader.push(key);

                                            }

                                        }

                                    }
                                }
                            }

                        }
                        scope.plansHeader = plansHeader;

                        /** Information of Red or Yellow Voucher*/
                        if (data[0].ColorLabel == 'Red') {
                            if(data[0].SampleName !== 'S03020001'){
                                scope.isRed = true;
                                scope.color = {
                                    'TW': '紅',
                                    'VN': 'ĐỎ',
                                    'ISO': '5VGAAQR140-01', //this ISO number should be stored in Database (they are developing)...
                                    'Solution': data[0].Solution
                                };
                            }else{
                                scope.isRed = false;
                                scope.color = {
                                    'TW': '紅',
                                    'VN': 'ĐỎ',
                                    'ISO': '5SGAAQR140-01', //this ISO number should be stored in Database (they are developing)...
                                    'Solution': data[0].Solution
                                };
                            }



                        } else {
                            if(data[0].SampleName !== 'S03020001') {
                                scope.color = {
                                    'TW': '黃',
                                    'VN': 'VÀNG',
                                    'ISO': '5VGAAQR141-01', //...as well as this number
                                    'Solution': data[0].Solution
                                };

                            }else{
                                scope.color = {
                                    'TW': '黃',
                                    'VN': 'VÀNG',
                                    'ISO': '5SGAAQR141-01', //...as well as this number
                                    'Solution': data[0].Solution
                                };
                            }
                            scope.isRed = false; // to define which Color
                        }

                        /**set used scope */
                        if (data[0].SampleName == 'S01020002')
                            scope.vdepart = 'SSP';

                        if (data[0].SampleName == 'S03020001')
                            scope.vdepart =  'PSF1';
                        if (data[0].SampleName == 'S01020001')
                            scope.vdepart = 'POLY';
                        scope.VoucherID = data[0].VoucherID; //set it here. in case some function use it
                        scope.recod = data[0];
                        if(scope.recod.SampleName ==='S03020001' ){

                            scope.recod.LINE= scope.recod.Group_name;

                        }
                        if( scope.recod.TypeID=='5' || scope.recod.TypeID=='3'){
                            scope.sampleName = scope.recod.DepartmentID;
                        }else{
                            scope.sampleName = scope.recod.SampleName;
                        }

                        scope.isShow = (scope.recod.Status === 'N') ? true : false; //show submit button (printQualifed)
                        if(scope.lang=='EN'||scope.lang=='VN'){
                            scope.DepartmentSpec =scope.recod.Specification_EN;
                            scope.showSampleName =scope.recod['Description_EN'];
                        }else{
                            scope.DepartmentSpec =scope.recod.Specification_CN;
                            scope.showSampleName =scope.recod['Description_CN'];

                        }
                        //scope.showSampleName =$scope.recod.showSampleName[0]['Description_'+lang];
                        if (scope.GetInformation != null && !scope.isShow)  //prevent exception of loadding null from leadercheck page
                            scope.GetInformation(scope.VoucherID);


                    });
                },
                templateUrl: './forms/QCOverGrade/ApprovalPage.html'
            }
        }
    ]);
    app.directive('rawAnalysis', ['LIMSService', 'Auth', '$q', '$filter', '$routeParams', 'Notifications', 'EngineApi',
        function (LIMSService, Auth, $q, $filter, $routeParams, Notifications, EngineApi) {
            return {
                restrict: 'AEC',
                link: function ($scope, element, attrs) {
                    $scope.today = $filter('date')(new Date(), "yyyy-MM-dd");
                    //GET BPMN WHO SUBMIT THIS VOUCHER
                    $scope.isShow = true;
                    // $scope.user = Auth.username;
                    // $scope.nickname = Auth.nickname;
                    if ($scope.user || $scope.nickname) $scope.isShow = false;
                    //GET INFORMATION OF THIS VOUCHER
                    var params = {
                        VoucherID: $routeParams.code ? $routeParams.code: attrs.vid ,
                    };
                    $scope.listofMaterial = [];
                    LIMSService.Entrusted.GetReportByVoucherID(params).$promise.then(function (res) {
                        console.log(res);
                        $scope.listofMaterial = res;
                        $scope.recod = res[0];
                        $scope.sampleName = res[0].SampleName;
                    }, function (error) {
                    })

                },
                templateUrl: './forms/RawMaterial/RawAnalysis.html'

            }


        }

    ]);
});
