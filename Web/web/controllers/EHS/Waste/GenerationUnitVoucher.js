define(['myapp','angular'],function (myapp,angular) {
    myapp.directive('createGenerationUnit',['$filter', '$http',
        '$routeParams', '$resource', '$location', '$interval',
        'Notifications', 'Forms', 'Auth', 'uiGridConstants', 'EngineApi', 'GateUnJointTruck',
        'GateGuest', 'GateJointTruck', '$translate', '$q',
    function ($filter, $http, $routeParams,
              $resource, $location, $interval, Notifications, Forms, Auth, uiGridConstants,
              EngineApi, GateUnJointTruck, GateGuest, GateJointTruck, $translate, $q) {
        return{
            restrict:'E',
            controller: function($scope){

            },
            templateUrl: './forms/EHS/Generation/createGenerationUnit.html'
        }

    }])
})