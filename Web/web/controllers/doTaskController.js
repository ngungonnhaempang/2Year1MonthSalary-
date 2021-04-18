/**
 * Created by wangyanyan on 14-3-6.
 *
 */
define(['myapp', 'angular', 'bpmn'], function (myapp, angular, Bpmn) {
    myapp.controller("doTaskController", ['$q', '$scope', '$http', '$compile', '$routeParams', '$resource', '$location', 'Forms', 'Notifications', 'EngineApi', 'Auth', 'GateGuest', function ($q, $scope, $http, $compile, $routeParams, $resource, $location, Forms, Notifications, EngineApi, Auth, GateGuest) {
        var taskid = $routeParams.taskid,
            pid = $routeParams.pid,
            formVariables = $scope.formVariables = [],
            historyVariable = $scope.historyVariable = [];
        var nextleadercheck = new Array();
        $scope.bpmnloaded = false;
        $scope.showPngg = function () {
            if ($scope.bpmnloaded == true) {
                $scope.bpmnloaded = false;
            } else {
                $scope.bpmnloaded = true;
            }

        }
        $scope.butmocsubit = false;

        function asyncLoop(iterations, func, callback) {
            var index = 0;
            var done = false;
            var loop = {
                next: function () {
                    if (done) {
                        return;
                    }
                    if (index < iterations) {
                        index++;
                        func(loop);

                    } else {
                        done = true;
                        callback();
                    }
                },

                iteration: function () {
                    return index - 1;
                },

                break: function () {
                    done = true;
                    callback();
                }
            };
            loop.next();
            return loop;
        }

        EngineApi.getTaskform().getForm({
            "id": taskid
        }, function (res) {
            if (res.message) {
                console.log(res.message);
                Notifications.addError({
                    'status': 'error',
                    'message': res.message
                });
                return;
            }

            $scope.taskid = res.taskid;

            $http.get(res.formkey).success(function (data, status, headers, config) {

                $("#bindHtml").html(data);
                var newScope = $scope.$new();
                $compile($("#bindHtml").contents())($scope.$new());
            });
            //show 流程图
            $scope.$on('menuBarLoad', function () {
                //父级能得到值
                $scope.$broadcast('tomenuBarLoad', "", taskid);
                //  alert(pdid);
            });
            var superQuery = {
                id: $routeParams.pid
            };
            EngineApi.gethistoryProcessList().get(superQuery, function (superreslist) {
                if (superreslist.superProcessInstanceId) {
                    var url = "#/processlog/" + superreslist.superProcessInstanceId + "/" + $routeParams.pid;
                } else {
                    var url = "#/processlog/" + $routeParams.pid;
                }
                console.log(url);
                $scope.historyurl = url;
            });
            /* $scope.$on('menu_historyLoad', function() {
                 //父级能得到值
                 $scope.$broadcast('to_menu_historyLoad', res.pid);
                 //  alert(pdid);
             });*/
            $scope.variable = res.data;
            $scope.processInstanceId = res.pid;
            var variablesMap = {};

            function SaveGuest(note, callback) {
                GateGuest.SaveGuest().save(note).$promise.then(function (res) {
                    var voucherid = res.VoucherID;
                    if (voucherid) {
                        $scope.recod.start_voucherid = voucherid;
                        callback(voucherid, "")

                    } else {
                        callback(voucherid, $translate.instant('saveError'))
                    }
                }, function (errormessage) {
                    callback("", errormessage)
                });
            }

            $scope.modalSubmit = function () {

                $('#warningModal').modal('hide');
                $('#messageModal').modal('hide');
                $('nextModalGateGuestupdate').modal('hide');
                setTimeout(function () {


                    console.log("--do task---");
                    console.log(formVariables);
                    variablesMap = Forms.variablesToMap(formVariables);
                    historyVariable = Forms.variablesToMap(historyVariable)
                    //  console.log('TEST:  ' +variablesMap);
                    console.log(variablesMap);
                    var datafrom = {
                        formdata: variablesMap,
                        historydata: historyVariable
                    };
                    //   var datafrom = {
                    //     formdata: $scope.variable
                    //};
                    EngineApi.doTask().complete({
                        "id": $scope.taskid
                    }, datafrom, function (res) {
                        console.log(res);
                        if (res.message) {
                            Notifications.addError({
                                'status': 'error',
                                'message': res.message
                            });
                            return
                        }
                        if (!res.result) {
                            Notifications.addError({
                                'status': 'error',
                                'message': res.message
                            });
                        } else {

                            var url = "/taskForm/" + res.url + $scope.processInstanceId
                            $location.url(url);
                        }
                    })
                }, 1000);
            }
            $scope.modalCancelUpdate = function () {
                $('#warningModal').modal('hide');
                $('#messageModal').modal('hide');
                $('#nextModalGateGuestupdate').modal('hide');

            }


            $scope.submit = function () {
                variablesMap = Forms.variablesToMap(formVariables);
                historyVariable = Forms.variablesToMap(historyVariable)
                console.log(variablesMap);
                var datafrom = {
                    formdata: variablesMap,
                    historydata: historyVariable
                };
                //   var datafrom = {
                //     formdata: $scope.variable
                //};
                EngineApi.doTask().complete({
                    "id": $scope.taskid
                }, datafrom, function (res) {
                    console.log(res);
                    if (res.message) {
                        Notifications.addError({
                            'status': 'error',
                            'message': res.message
                        });
                        return
                    }
                    if (!res.result) {
                        Notifications.addError({
                            'status': 'error',
                            'message': res.message
                        });
                    } else {

                        var url = "/taskForm/" + res.url + $scope.processInstanceId
                        $location.url(url);
                    }
                })
            }
        })
    }]);
    myapp.controller("loadController", ['$scope', '$rootScope', 'EngineApi', '$location', function ($scope, $rootScope, EngineApi, $location) {
        $scope.menuBar = true;
        $scope.bindform = true;
        $scope.toggleCustom = function () {
            //   alert("0o");
            $scope.menuBar = $scope.menuBar === false ? true : false;
            $(".pinned").toggle(function () {
                $(this).addClass("highlight");
                $(this).next().fadeOut(1000);
            }, function () {
                $(this).removeClass("highlight");
                $(this).next("div .content").fadeIn(1000);
            });
        };
        $scope.showPng = function () {
            $scope.$emit('menuBarLoad');
        }

        $scope.bpmn = {};
        $scope.$on('tomenuBarLoad', function (d, flowid, taskid) {
            var diagram = $scope.bpmn.diagram;
            console.log(flowid); //子级得不到值
            // var pdid="checkForm:1:0381187d-aa59-11e3-a11f-0c84dc2d23b0";
            if (taskid) {
                if ($scope.bindform) {
                    EngineApi.getTasks().get({
                        "id": taskid
                    }, function (task) {
                        var oldTask = $scope.bpmn.task;
                        if (diagram) {
                            if (taskid == oldTask) {
                                return;
                            }
                        }
                        console.log("11111");
                        $scope.bpmn.task = taskid;
                        var taskDefinitionKey = task.taskDefinitionKey;
                        flowid = task.processDefinitionId;
                        EngineApi.getProcessDefinitions().xml({
                            id: flowid
                        }, function (result) {
                            var diagram = $scope.bpmn.diagram,
                                xml = result.bpmn20Xml;
                            if (diagram) {
                                diagram.clear();
                            }
                            var width = $('#diagram').width();
                            var height = $('#diagram').height();
                            diagram = new Bpmn().render(xml, {
                                diagramElement: 'diagram',
                                width: width,
                                height: 400
                            });
                            console.log(taskDefinitionKey);
                            diagram.annotation(taskDefinitionKey).addClasses(['bpmn-highlight']);
                            $scope.bpmn.diagram = diagram;
                        });
                    })
                    $scope.bindform = false;
                } else {
                    $scope.bindform = true;
                }
            } else {
                if ($scope.bindform) {
                    var diagram = $scope.bpmn.diagram;
                    var oldTask = $scope.bpmn.id;
                    $scope.bpmn = {};
                    if (diagram) {
                        // destroy old diagram
                        diagram.clear();
                        if (flowid == oldTask) {
                            return;
                        }
                    }
                    $scope.bpmn.id = flowid;
                    EngineApi.getProcessDefinitions().xml({
                        id: flowid
                    }, function (result) {
                        console.log(result);
                        var diagram = $scope.bpmn.diagram,
                            xml = result.bpmn20Xml;
                        if (diagram) {
                            diagram.clear();
                        }
                        var width = $('#diagram').width();
                        var height = $('#diagram').height();

                        diagram = new Bpmn().render(xml, {
                            diagramElement: 'diagram',
                            width: width,
                            height: 400
                        });
                        //  diagram.annotation(task.taskDefinitionKey).addClasses([ 'bpmn-highlight' ]);
                        $scope.bpmn.diagram = diagram;
                    });
                    $scope.bindform = false;
                } else {
                    $scope.bindform = true;
                }
            }
        });
    }]);
});