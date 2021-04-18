/**
 * Created by wangyanyan on 2017/2/20.
 */
define(['myapp', 'angular'], function (myapp, angular) {
    myapp.controller("ConQuaDetailController", ['$scope', '$filter', '$compile', '$routeParams', '$resource', '$location', 'i18nService', 'Notifications', 'User', 'Forms', 'Auth', '$http', 'EngineApi', 'ConQuaService', '$upload', '$translatePartialLoader', '$translate',
        function ($scope, $filter, $compile, $routeParams, $resource, $location, i18nService, Notifications, User, Forms, Auth, $http, EngineApi, ConQuaService, $upload, $translatePartialLoader, $translate) {

            var lang = window.localStorage.lang;
            $scope.bpmnloaded = false;
            $scope.project = {};
            $scope._showEmployees = false;

            ConQuaService.CountContractor().get({ ContractorID: $routeParams.contractorID }).$promise.then(function (res) { 
                $scope.rpCounter = res[0];
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });

            ConQuaService.ContractorQualification().getDetailHeader({ "contractorID": $routeParams.contractorID, language: lang }).$promise.then(function (res) {
                console.log(res);
                $scope.project = res[0];
                $scope.conquafile = [];
                if ($scope.project.ContractorFile != null) {
                    $scope.conquafile = JSON.parse($scope.project.ContractorFile);
                }
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });
			
			
			$scope.isHSEUser = false;
            $scope.isSecurityUser = false;
            $scope.isHrUser = false;

            ConQuaService.GetRole().get({
                UserID: Auth.username
            }, function (res) {
                console.log(res);
                if (res.length > 0) {
                    var dept = res[0].Dept.trim();
                    if (dept == 'HSE') {
                        $scope.isHSEUser = true;
                    }
                    else if (dept == 'SECURITY') {
                        $scope.isSecurityUser = true;

                    } else if (dept == 'HR') {
                        $scope.isHrUser = true;
                    }
                }

            });

            ConQuaService.GetContractorEmployer().get({
                VoucherID: '',
                ContractorID: $routeParams.contractorID,
                Language: window.localStorage.lang
            }).$promise.then(function (res) {
                if (res.length > 0) {
                    $scope._showEmployees = true;
                }
                $scope.EmployeeList = [];
                res.forEach(element => {
                    var x = {};
                    x.VoucherID = element.VoucherID;
                    x.Name = element.Name;
                    x.IdCard = element.IdCard;
                    x.Sex = element.Sex;
                    x.Birthday = element.Birthday;
                    x.Job = element.Job;
					x.InsuranceDuration = element.InsuranceDuration;
					x.SafetyCerDuration = element.SafetyCerDuration;
                    x.StatusRemark = element.StatusRemark;
                    x.Remark = element.Remark;
					x.Mark = element.Mark;
                    $scope.EmployeeList.push(x);
                });
            }, function (errResponse) {
                Notifications.addError({ 'status': 'error', 'message': errResponse });
            });
        }])
})