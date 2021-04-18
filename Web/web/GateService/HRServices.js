define(['app', 'angular'], function (app, angular) {
    app.service('HRServices', ['$resource', '$q', 'Auth', '$location', '$translate', function ($resource, $q, Auth, $location, $translate) {
        function HRServices() {
            this.getInformation = $resource('/ehs/gate/HRMS/:operation', {}, {

                GetAttendance: {method: 'POST', params: {operation: 'GetAttendance'},isArray:true},
                GetAttendanceToExport: {method: 'POST', params: {operation: 'GetAttendanceToExport'}},
                GetAttendanceToExportMitaPro: {method: 'POST', params: {operation: 'GetAttendanceToExportMitaPro'}},
                GetWorkplaces: {method: 'GET', params: {operation: 'GetWorkplaces'},isArray:true}

            });
        }

        HRServices.prototype.GateInfoBacsic = function () {
            return this.getInformation;
        };

        return new HRServices();
    }])

});
