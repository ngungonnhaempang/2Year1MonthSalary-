/**
 * Created by wangyanyan on 14-3-3.
 */

define(['angularAMD', 'app', 'services/main', 'directive/main'], function (angularAMD, app) {
    app.config(['$routeProvider', '$httpProvider', '$translateProvider', '$translatePartialLoaderProvider', function ($routeProvider, $httpProvider, $translateProvider, $translatePartialLoaderProvider) {

        ////拦截器
        $httpProvider.responseInterceptors.push('interceptor');
        var spinnerFunction = function (data, headersGetter) {
            // todo start the spinner here
            $('#spinner_wait').show();
            return data;
        };
        $httpProvider.defaults.transformRequest.push(spinnerFunction);
        var lang = window.localStorage.lang || 'EN';
        /*    $translateProvider.useStaticFilesLoader({
         prefix: 'i18n/Basic/',
         suffix: '.json'
         });*/

        $translatePartialLoaderProvider.addPart('Basic');

        $translateProvider.useLoader('$translatePartialLoader', {
            urlTemplate: '/i18n/{part}/{lang}.json'
        });

        $translateProvider.preferredLanguage(lang);
        $translateProvider.fallbackLanguage(lang);


        $routeProvider

            .when('/', {
                redirectTo: '/taskForm/main'
            })
            .when('/LIMS/QualifiedControl/', angularAMD.route({
                templateUrl: 'forms/QCOverGrade/search.html',
                controller: 'QualifiedController',
                controllerUrl: 'controllers/LIMS/QualifiedController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/LIMS/QualifiedControl/printRedUQ/:VoucherID', angularAMD.route({
                templateUrl: 'forms/QCOverGrade/printRedUQ.html',
                controller: 'QualifiedReportController',
                controllerUrl: 'controllers/LIMS/ReportUQController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/LIMS/QualifiedControl/printAllUQ/:from&:end', angularAMD.route({
                templateUrl: 'forms/QCOverGrade/printAllUQ.html',
                controller: 'PrintAllUQReportController',
                controllerUrl: 'controllers/LIMS/ReportUQController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/LIMS/QualifiedControl/print/:VoucherID', angularAMD.route({
                templateUrl: 'forms/QCOverGrade/printQualified.html',
                controller: 'QualifiedReportController',
                controllerUrl: 'controllers/LIMS/ReportUQController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/LIMS/QualifiedControl/approval/:VoucherID', angularAMD.route({
                templateUrl: 'forms/QCOverGrade/QCLeaderCheck.html',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/LIMS/Chart/', angularAMD.route({
                templateUrl: 'forms/FEPVLims/chart.html',
                controller: 'ChartController',
                controllerUrl: 'controllers/LIMS/ChartController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/LIMS/Entrusted/', angularAMD.route({
                templateUrl: "forms/FEPVLims/SingleDraft.html",
                controller: 'EntrustedController',
                controllerUrl: 'controllers/LIMS/EntrustedController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Lims/Ontest', angularAMD.route({
                templateUrl: "forms/FEPVLims/OnTest.html",
                controller: 'OnTestController',
                controllerUrl: 'controllers/LIMS/OnTestController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/Lims/QueryPlans', angularAMD.route({
                templateUrl: "forms/FEPVLims/QueryPlans.html",
                controller: 'QueryPlansController',
                controllerUrl: 'controllers/LIMS/QueryPlansController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Lims/ReportAnalysis', angularAMD.route({
                templateUrl: "forms/FEPVLims/ReportAnalysis.html",
                controller: 'ReportAnalysisController',
                controllerUrl: 'controllers/LIMS/ReportAnalysisController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Lims/ReportAnalysis/PrintAnalysisReport/:code', angularAMD.route({
                templateUrl: "forms/FEPVLims/ReportAnalysis_Print.html",
                controller: 'RawMaterialAnalysisReport',
                controllerUrl: 'controllers/LIMS/ReportAnalysisController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Lims/OverGradeChart', angularAMD.route({
                templateUrl: "forms/FEPVLims/overchart.html",
                controller: 'OverGradeChartController',
                controllerUrl: 'controllers/LIMS/OverGradeChartController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Lims/StapleOverGrade', angularAMD.route({
                templateUrl: "forms/FEPVLims/stapleovergrade.html",
                controller: 'StapleOverGradeController',
                controllerUrl: 'controllers/LIMS/StapleOverGradeController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Lims/StapleReport', angularAMD.route({
                templateUrl: "forms/FEPVLims/staplereport.html",
                controller: 'StapleReportController',
                controllerUrl: 'controllers/LIMS/StapleReportController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            /**
             * Create by Isaac 208-09-15
             */
            .when('/Lims/ReportPOLYnSSP', angularAMD.route({
                templateUrl: "forms/FEPVLims/ReportQuery.html",
                controller: 'ReportPOLYnSSPController',
                controllerUrl: 'controllers/LIMS/ReportPOLYnSSPController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Lims/ReportResult/:F/:E/:S/:M/:L/:month', angularAMD.route({
                templateUrl: "forms/FEPVLims/ReportResult.html",
                controller: 'ReportResultSSPnPOLY',
                controllerUrl: 'controllers/LIMS/ReportResultSSPnPOLY',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Lims/SSPDayReport/:from/:to/:material/:line/:interval/:month', angularAMD.route({
                templateUrl: "forms/FEPVLims/ReportDetail.html",
                controller: 'SSPDayReportController',
                controllerUrl: 'controllers/LIMS/SSPDayReportController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Lims/QueryPlans', angularAMD.route({
                templateUrl: "forms/FEPVLims/QueryPlans.html",
                controller: 'QueryPlansController',
                controllerUrl: 'controllers/LIMS/QueryPlansController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Lims/QueryReceive', angularAMD.route({
                templateUrl: "forms/FEPVLims/ReceiveQuery.html",
                controller: 'QueryReceiveController',
                controllerUrl: 'controllers/LIMS/QueryReceiveController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/Lims/QueryCurrentGrade', angularAMD.route({
                templateUrl: "forms/FEPVLims/QueryCurrentGrade.html",
                controller: 'CurrentGradeController',
                controllerUrl: 'controllers/LIMS/CurrentGradeController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Lims/GradeSpecVersion', angularAMD.route({
                templateUrl: 'forms/QCGrades/CreateGrade.html',
                controller: 'GradeController',
                controllerUrl: 'controllers/LIMS/GradeController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            })).when('/Lims/ShowHistory', angularAMD.route({
                templateUrl: 'forms/FEPVLims/showHistoryGradesVersion.html',
                controller: 'showHistoryController',
                controllerUrl: 'controllers/LIMS/showHistoryController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Lims/QueryCurrentGrade', angularAMD.route({
                templateUrl: 'forms/FEPVLims/QueryCurrentGrade.html',
                controller: 'CurrentGradeController',
                controllerUrl: 'controllers/LIMS/CurrentGradeController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/gate/Guest/print/:code', angularAMD.route({
                templateUrl: "forms/GateGuest/print.html",
                controller: 'GateGuestDetailController',
                controllerUrl: 'controllers/Gate/GateGuestDetailController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/gate/Meal', angularAMD.route({
                templateUrl: "forms/Meal/search.html",
                controller: 'MealController',
                controllerUrl: 'controllers/Meal/MealController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/gate/TimekeeperManagement', angularAMD.route({
                templateUrl: "forms/Meal/TimekeeperManagement.html",
                controller: 'MealTimekeeperManagementController',
                controllerUrl: 'controllers/Meal/MealTimekeeperManagementController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/gate/MealDetails/:code/:oprea/:dept/:all', angularAMD.route({
                templateUrl: "forms/Meal/view.html",
                controller: 'MealDetailsController',
                controllerUrl: 'controllers/Meal/MealDetailsController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/ContractorQua/import', angularAMD.route({
                templateUrl: "forms/ContractorQua/import.html",
                controller: 'ContractorImportController',
                controllerUrl: 'controllers/EHS/ContractorImportController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))


            .when('/gate/GoodsOut', angularAMD.route({
                templateUrl: "forms/vGateGoodOut/query.html",
                controller: 'GoodsOutController',
                controllerUrl: 'controllers/Gate/GoodsOutController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/gate/JointTruck/', angularAMD.route({
                templateUrl: "forms/GateJointTruck/search.html",
                controller: 'JointTruckController',
                controllerUrl: 'controllers/Gate/JointTruckController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/gate/JointTruck/:code/:oprea', angularAMD.route({
                templateUrl: "forms/GateJointTruck/updateVoucher.html",
                controller: 'JointTruckUpdateController',
                controllerUrl: 'controllers/Gate/JointTruckUpdateController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/gate/JointTruck/:code/', angularAMD.route({
                templateUrl: "forms/GateJointTruck/view.html",
                controller: 'JointTruckUpdateController',
                controllerUrl: 'controllers/Gate/JointTruckUpdateController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/gate/JointTruck/:code/:para', angularAMD.route({
                templateUrl: "forms/GateJointTruck/view.html",
                controller: 'JointTruckUpdateController',
                controllerUrl: 'controllers/Gate/JointTruckUpdateController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/gate/PtaEgTruck/', angularAMD.route({
                templateUrl: "forms/GatePtaEgTruck/search.html",
                controller: 'PtaEgTruckController',
                controllerUrl: 'controllers/Gate/PtaEgTruckController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/gate/PtaEgTruck/:code', angularAMD.route({
                templateUrl: "forms/GatePtaEgTruck/view.html",
                controller: 'PtaEgTruckDetailController',
                controllerUrl: 'controllers/Gate/PtaEgTruckDetailController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/ConQua/Index', angularAMD.route({  //承揽商资质查询
                templateUrl: "forms/ConQua/Index.html",
                controller: 'ConQuaController',
                controllerUrl: 'controllers/EHS/ConQuaController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Contractor/Detail', angularAMD.route({ // 承揽商资质明细
                templateUrl: "forms/ConQua/detail.html",
                controller: 'ConQuaDetailController',
                controllerUrl: 'controllers/EHS/ConQuaDetailController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Contractor', angularAMD.route({ // 承揽商资质新建
                templateUrl: "forms/ConQua/startnew.html",
                controller: 'ContractorController',
                controllerUrl: 'controllers/EHS/ContractorController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/ContractorQua/Index', angularAMD.route({
                templateUrl: "forms/ContractorQua/Index.html",
                controller: 'ContractorQuaController',
                controllerUrl: 'controllers/EHS/ContractorQuaController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/ContractorQua', angularAMD.route({
                templateUrl: "forms/ContractorQua/startnew.html",
                controller: 'ContractorQuaUpdateController',
                controllerUrl: 'controllers/EHS/ContractorQuaUpdateController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/ContractorQuaDetail', angularAMD.route({
                templateUrl: "forms/ContractorQua/detail.html",
                controller: 'ContractorQuaDetailController',
                controllerUrl: 'controllers/EHS/ContractorQuaDetailController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/ContractorQuaConfirm', angularAMD.route({
                templateUrl: "forms/ContractorQua/confirm.html",
                controller: 'ContractorQuaConfirmController',
                controllerUrl: 'controllers/EHS/ContractorQuaConfirmController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Contractor/StatisticReport', angularAMD.route({
                templateUrl: "forms/ConQua/Statistic.html",
                controller: 'ContractorStatisticController',
                controllerUrl: 'controllers/EHS/ContractorStatisticController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            //目前没用到
            .when('/Contractor/Report', angularAMD.route({
                templateUrl: "forms/ConQua/Report.html",
                controller: 'ContractorReportController',
                controllerUrl: 'controllers/EHS/ContractorReportController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/gate/GoodsOut/:code/query', angularAMD.route({
                templateUrl: "forms/vGateGoodOut/detail.html",
                controller: 'GoodsOutlookController',
                controllerUrl: 'controllers/Gate/GoodsOutlookController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/gate/UnJointTruck/', angularAMD.route({
                templateUrl: "forms/GateUnJointTruck/search.html",
                controller: 'UnJointTruckController',
                controllerUrl: 'controllers/Gate/UnJointTruckController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/gate/UnJointTruck/:code', angularAMD.route({
                templateUrl: "forms/GateUnJointTruck/query.html",
                controller: 'UnJointTruckDetailController',
                controllerUrl: 'controllers/Gate/UnJointTruckDetailController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))


            .when('/gate/GoodsOut/:code/print', angularAMD.route({
                templateUrl: "forms/vGateGoodOut/print.html",
                controller: 'GoodsOutlookController',
                controllerUrl: 'controllers/Gate/GoodsOutlookController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/taskForm/startList', angularAMD.route({
                templateUrl: "views/startList.html",
                controller: 'startListController',
                controllerUrl: 'controllers/startListController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/taskForm/main', angularAMD.route({
                templateUrl: "views/main.html",
                controller: 'mainController',
                controllerUrl: 'controllers/mainController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/taskForm/start/:id', angularAMD.route({
                templateUrl: "views/load.html",
                controller: 'startController',
                controllerUrl: 'controllers/startController',
                caseInsensitiveMatch: true,
                resolve: {

                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/processlog/:id/:cId?', angularAMD.route({
                templateUrl: "views/processLog.html",
                controller: 'processlogController',
                controllerUrl: 'controllers/processlogController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/taskForm/:processDefinitionId/complete/:pid', angularAMD.route({
                templateUrl: 'views/taskCompleted.html',
                controller: 'startFinishedController',
                controllerUrl: 'controllers/startFinishedController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/taskForm/todo', angularAMD.route({
                templateUrl: "views/todo.html",
                controller: 'todoController',
                controllerUrl: 'controllers/todoController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/taskForm/task/:taskid/:pid', angularAMD.route({
                templateUrl: "views/load.html",
                controller: 'doTaskController',
                controllerUrl: 'controllers/doTaskController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/taskForm/:taskid/complete/:pid', angularAMD.route({
                templateUrl: 'views/taskCompleted.html',
                controller: 'taskFinishedController',
                controllerUrl: 'controllers/taskFinishedController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/taskForm/TaskManage', angularAMD.route({
                templateUrl: 'views/TaskManage.html',
                controller: 'TaskManageController',
                controllerUrl: 'controllers/TaskManageController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/taskForm/RepositoryManage', angularAMD.route({
                templateUrl: 'views/RepositoryManage.html',
                controller: 'RepositoryController',
                controllerUrl: 'controllers/RepositoryController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/taskForm/DictionaryManage', angularAMD.route({
                templateUrl: 'views/DictionaryManage.html',
                controller: 'DictionaryController',
                controllerUrl: 'controllers/DictionaryController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/taskForm/ProxyUser', angularAMD.route({
                templateUrl: 'views/ProxyUser.html',
                controller: 'ProxyUserController',
                controllerUrl: 'controllers/ProxyUserController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/main/userInfo', angularAMD.route({
                templateUrl: "views/pages/userInfo.html",
                controller: 'UserInfoController',
                controllerUrl: 'controllers/mainController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/png/:processDefinitionId/:pid', angularAMD.route({
                templateUrl: 'views/pages/png.html',
                controller: 'PngController',
                controllerUrl: 'controllers/historyController',
                caseInsensitiveMatch: true
            }))
            .when('/login', angularAMD.route({
                templateUrl: 'views/login.html',
                controller: 'loginController',
                controllerUrl: 'controllers/loginController'

            }))
            .when('/logout', angularAMD.route({
                templateUrl: 'views/login.html',
                controller: 'logoutController',
                controllerUrl: 'controllers/loginController'
            }))
            .when('/FormSetting/formmain', angularAMD.route({
                templateUrl: "views/formMaintain.html",
                controller: 'formMaintainController',
                controllerUrl: 'controllers/formMaintainControl',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            /**
            * Creat Route by Isaac 08/11/2018
            */

            .when('/waste/Main', angularAMD.route({
                templateUrl: "forms/EHS/main.html",
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/waste/Company', angularAMD.route({
                templateUrl: "forms/EHS/Company/searchCompany.html",
                controller: 'CompanyController',
                controllerUrl: 'controllers/EHS/Waste/CompanyController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/waste/Generation', angularAMD.route({
                templateUrl: "forms/EHS/Generation/search.html",
                controller: 'GenerationUnitController',
                controllerUrl: 'controllers/EHS/Waste/GenerationUnitController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/waste/Method', angularAMD.route({
                templateUrl: "forms/EHS/Method/searchMethod.html",
                controller: 'MethodProcessController',
                controllerUrl: 'controllers/EHS/Waste/MethodProcessController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/waste/Voucher/print/:code', angularAMD.route({
                templateUrl: "forms/EHS/Voucher/detail.html",
                controller: 'VoucherDetailController',
                controllerUrl: 'controllers/EHS/Waste/VoucherDetailController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/waste/WasteItem', angularAMD.route({
                templateUrl: "forms/EHS/WasteItem/searchWasteItem.html",
                controller: 'WasteItemController',
                controllerUrl: 'controllers/EHS/Waste/WasteItemController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/waste/Voucher', angularAMD.route({
                templateUrl: "forms/EHS/Voucher/searchVoucher.html",
                controller: 'VoucherController',
                controllerUrl: 'controllers/EHS/Waste/VoucherController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/CircumstanceReport', angularAMD.route({ //route

                templateUrl: "forms/EHS/CReport/searchCRerport.html",
                controller: 'CReportController',
                controllerUrl: 'controllers/EHS/CReport/CReportController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/CircumstanceReport/ICReport/print/:code', angularAMD.route({
                templateUrl: "forms/EHS/CReport/ICdetail.html",
                controller: 'ICReportController',
                controllerUrl: 'controllers/EHS/CReport/ReportICDetailController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/CircumstanceReport/ACReport/print/:code', angularAMD.route({
                templateUrl: "forms/EHS/CReport/ACdetail.html",
                controller: 'ReportACdetailController',
                controllerUrl: 'controllers/EHS/CReport/ReportACDetailController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/gate/Attendance', angularAMD.route({
                templateUrl: "forms/HR/search.html",
                controller: 'HRAttendanceController',
                controllerUrl: 'controllers/HR/HRAttendanceController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))


            /*************** HSE - Modification ***************/
            .when('/Modification/Main', angularAMD.route({
                templateUrl: "forms/EHS/Modification/main.html",
                controller: 'mainController',
                controllerUrl: 'controllers/mainController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/Manage_Info', angularAMD.route({
                templateUrl: "forms/EHS/Modification/Manage_Info/Index.html",
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/Manage_Modification', angularAMD.route({
                templateUrl: "forms/EHS/Modification/Manage_Modification/Index.html",
                controller: 'Manage_ModificationController',
                controllerUrl: 'controllers/EHS/Manage_Modification/InfoManagementController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))


            .when('/Manage_Info/Category', angularAMD.route({
                templateUrl: "forms/EHS/Modification/Manage_Info/Category/SearchCategory.html",
                controller: 'CategoriesController',
                controllerUrl: 'controllers/EHS/Manage_Info/CategoriesController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/Manage_Info/AffectingRange', angularAMD.route({
                templateUrl: "forms/EHS/Modification/Manage_Info/AffectingRange/SearchAffectingRange.html",
                controller: 'AffectingRangeController',
                controllerUrl: 'controllers/EHS/Manage_Info/AffectingRangeController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
            .when('/Manage_Info/ISO', angularAMD.route({
                templateUrl: "forms/EHS/Modification/Manage_Info/ISO/SearchISO.html",
                controller: 'ISOController',
                controllerUrl: 'controllers/EHS/Manage_Info/ISOController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/Manage_Modification/Modification', angularAMD.route({
                templateUrl: "forms/EHS/Modification/Manage_Modification/Modification/SearchModification.html",
                controller: 'ModificationController',
                controllerUrl: 'controllers/EHS/Manage_Modification/ModificationController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/Manage_Modification/Modification/:code/:MD_AppID', angularAMD.route({
                templateUrl: "forms/EHS/Modification/Manage_Modification/Modification/ViewModification.html",
                controller: 'ModificationController',
                controllerUrl: 'controllers/EHS/Manage_Modification/ModificationController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/Manage_Modification/RiskAssessment', angularAMD.route({
                templateUrl: "forms/EHS/Modification/Manage_Modification/RiskAssessment/SearchRiskAssessment.html",
                controller: 'RiskAssessmentController',
                controllerUrl: 'controllers/EHS/Manage_Modification/RiskAssessmentController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/Manage_Modification/RiskAssessment/:code/:Risk_AppID', angularAMD.route({
                templateUrl: "forms/EHS/Modification/Manage_Modification/RiskAssessment/ViewRiskAssessment.html",
                controller: 'RiskAssessmentController',
                controllerUrl: 'controllers/EHS/Manage_Modification/RiskAssessmentController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/Manage_Modification/BeforeOperateEvaluation', angularAMD.route({
                templateUrl: "forms/EHS/Modification/Manage_Modification/BeforeOperateEvaluation/SearchBeforeOperateEvaluation.html",
                controller: 'BeforeOperateEvaluationController',
                controllerUrl: 'controllers/EHS/Manage_Modification/BeforeOperateEvaluationController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/Manage_Modification/BeforeOperateEvaluation/:code/:Evaluation_AppID', angularAMD.route({
                templateUrl: "forms/EHS/Modification/Manage_Modification/BeforeOperateEvaluation/ViewEvaluation.html",
                controller: 'BeforeOperateEvaluationController',
                controllerUrl: 'controllers/EHS/Manage_Modification/BeforeOperateEvaluationController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))



            .when('/Manage_Modification/ClosingEvaluation', angularAMD.route({
                templateUrl: "forms/EHS/Modification/Manage_Modification/ClosingEvaluation/SearchClosingEvaluation.html",
                controller: 'ClosingEvaluationController',
                controllerUrl: 'controllers/EHS/Manage_Modification/ClosingEvaluationController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/Manage_Modification/ClosingEvaluation/:code/:Closing_AppID', angularAMD.route({
                templateUrl: "forms/EHS/Modification/Manage_Modification/ClosingEvaluation/ViewClosing.html",
                controller: 'ClosingEvaluationController',
                controllerUrl: 'controllers/EHS/Manage_Modification/ClosingEvaluationController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))


            .when('/Manage_Project', angularAMD.route({
                templateUrl: "forms/EHS/Modification/Manage_Modification/ManageProject.html",
                controller: 'InfoManagementController',
                controllerUrl: 'controllers/EHS/Manage_Modification/InfoManagementController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/Modification/processlog/:IsHSE/:id/:cId?', angularAMD.route({
                templateUrl: "forms/EHS/Modification/processLog.html",
                controller: 'processlogController',
                controllerUrl: 'controllers/processlogController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            /*************** InOutEastGate ***************/
            .when('/InOutEastGate', angularAMD.route({
                templateUrl: "forms/InOutEastGate/search.html",
                controller: 'EastGateController',
                controllerUrl: 'controllers/InOutEastGate/EastGateController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/InOutEastGate/:code/:VoucherID', angularAMD.route({
                templateUrl: "forms/InOutEastGate/viewEastGateVoucher.html",
                controller: 'EastGateController',
                controllerUrl: 'controllers/InOutEastGate/EastGateController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            
            /*************** Contractor ***************/
            .when('/ConQua/Main', angularAMD.route({
                templateUrl: "forms/ConQua/main.html",
                controller: 'mainController',
                controllerUrl: 'controllers/mainController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))
           
            .when('/CotractorDiscipline/Main', angularAMD.route({
                templateUrl: "forms/ContractorDiscipline/main.html",
                controller: 'mainController',
                controllerUrl: 'controllers/mainController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/ContractorDiscipline/Manage_Info/Category', angularAMD.route({
                templateUrl: "forms/ContractorDiscipline/Manage_Info/Category/SearchCategory.html",
                controller: 'CategoriesController',
                controllerUrl: 'controllers/ContractorDiscipline/Manage_Info/CategoriesController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/ContractorDiscipline/Manage_Info/ISO', angularAMD.route({
                templateUrl: "forms/ContractorDiscipline/Manage_Info/ISO/SearchISO.html",
                controller: 'ISOController',
                controllerUrl: 'controllers/ContractorDiscipline/Manage_Info/ISOController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/ContractorDiscipline/Denounce', angularAMD.route({
                templateUrl: "forms/ContractorDiscipline/Manage_Discipline/DenounceContractor/SearchDenounce.html",
                controller: 'DenounceContractorController',
                controllerUrl: 'controllers/ContractorDiscipline/Manage_Discipline/DenounceContractorController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/ContractorDiscipline/ViolationConfirmation', angularAMD.route({
                templateUrl: "forms/ContractorDiscipline/Manage_Discipline/ViolationConfirmation/SearchViolation.html",
                controller: 'ViolationConfirmationController',
                controllerUrl: 'controllers/ContractorDiscipline/Manage_Discipline/ViolationConfirmationController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/ContractorDiscipline/ViolationConfirmation/:code/:ViolationID', angularAMD.route({
                templateUrl: "forms/ContractorDiscipline/Manage_Discipline/ViolationConfirmation/PrintViolation.html",
                controller: 'ViolationConfirmationController',
                controllerUrl: 'controllers/ContractorDiscipline/Manage_Discipline/ViolationConfirmationController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/ContractorDiscipline/Discipline/:DisciplineCode', angularAMD.route({
                templateUrl: "forms/ContractorDiscipline/Manage_Discipline/ContractorDiscipline/SearchDiscipline.html",
                controller: 'ContractorDisciplineController',
                controllerUrl: 'controllers/ContractorDiscipline/Manage_Discipline/ContractorDisciplineController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/ContractorDiscipline/Discipline/:code/:DisciplineID', angularAMD.route({
                templateUrl: "forms/ContractorDiscipline/Manage_Discipline/ContractorDiscipline/ViewDiscipline.html",
                controller: 'ContractorDisciplineController',
                controllerUrl: 'controllers/ContractorDiscipline/Manage_Discipline/ContractorDisciplineController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))

            .when('/ConQua/searchContractor', angularAMD.route({  //承揽商资质查询
                templateUrl: "forms/ConQua/searchContractor.html",
                controller: 'ConQuaController',
                controllerUrl: 'controllers/EHS/ConQuaController',
                caseInsensitiveMatch: true,
                resolve: {
                    User: function (AuthenticationLoader) {
                        return AuthenticationLoader();
                    }
                }
            }))




    }]);
    return angularAMD.bootstrap(app);
});
