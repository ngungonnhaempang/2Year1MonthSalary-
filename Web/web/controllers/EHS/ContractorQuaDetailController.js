define(['myapp', 'angular'], function (myapp, angular) {
    myapp.controller("ContractorQuaDetailController", ['$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', 'uiGridConstants', '$http', 'EngineApi', 'ConQuaService', '$upload', '$translatePartialLoader', '$translate', 'GateGuest',
        function ($scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, uiGridConstants, $http, EngineApi, ConQuaService, $upload, $translatePartialLoader, $translate, GateGuest) {

            $scope.detail = true;
            
            ConQuaService.getContractorQuaProcess().get({
                employer: $routeParams.code,
                idCard: $routeParams.IdCard
            }).$promise.then(function (conres) {
                $scope.processList = conres[conres.length - 1];
                EngineApi.getProcessLogs.getList({ "id": $scope.processList.ProcessInstanceId, "cId": "" }, function (data) {
                    if (data.length === 0) {
                        $scope.processLogs = "";
                    } else {
                        $scope.processLogs = data[0];
                    }
                })
            }, function (errResponse) {
                Notifications.addError({
                    'status': 'error',
                    'message': errResponse
                });
            });

            if ($routeParams.IsForeign == 1) {
                $scope.IsForeign = true;
            } else $scope.IsForeign = false;

            ConQuaService.GetContractorEmployer().get({
                VoucherID: $routeParams.code,
                ContractorID: '',
                Language: window.localStorage.lang
            }).$promise.then(function (res) {
                $scope.note = res[0];
                $scope.note.ContractorName = res[0].ContractorName;
                $scope.EmployeeList = [];
                $scope.note.StartDate = res[0].StartDate;
                $scope.note.EndDate = res[0].EndDate;
                if (res[0].FileName != null) {
                    $scope.note.FileName = res[0].FileName;
                } else $scope.note.FileName = '';

                res.forEach(element => {
                    var x = {};
                    x.Name = element.Name;
                    x.IdCard = element.IdCard;
                    x.Sex = element.Sex;
                    x.Birthday = element.Birthday;
                    x.Job = element.Job;
                    x.Region = element.Region;
                    x.StatusRemark = element.StatusRemark;
                    x.InsuranceDuration = element.InsuranceDuration;
                    x.SafetyCerDuration = element.SafetyCerDuration;
                    x.Remark = element.Remark;
                    x.Status = element.Status;
                    x.StartdateCancel = element.StartdateCancel;
                    x.EnddateCancel = element.EnddateCancel;
                    x.Phone = element.Phone;
                    if ($scope.IsForeign) {
                        x.PassPort_Expiry = element.PassPort_Expiry;
                        x.PassPort_Nationality = element.PassPort_Nationality;
                        x.WorkPermit_No = element.WorkPermit_No;
                        x.WorkPermit_Start = element.WorkPermit_Start;
                        x.WorkPermit_End = element.WorkPermit_End;
                        x.CategoryCard = element.CategoryCard;
                        x.Card_Type = element.Card_Type;
                        x.Card_No = element.Card_No;
                        x.Card_Start = element.Card_Start;
                        x.Card_End = element.Card_End;
                    }
                    $scope.EmployeeList.push(x);
                });
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });
        }
    ]);
});