/**
 * Create by Isaac 08/11/2018
 * Service for VoucherService
 */
define([
    'app',
    'angular'
], function (app, angular) {
    app.service('VoucherService', ['$resource', '$q', 'Auth', '$location', '$translate', function ($resource, $q, Auth, $location, $translate) {
        function VoucherService() {
            var _WASTERAPI = '/Waste/';
            this.GetInfoBasic = $resource(_WASTERAPI + 'VoucherController/:operation', {}, {
                getList:
                {
                    method: 'GET',
                    params: {
                        operation: 'GetAll'
                    },
                    isArray: true
                },
                getById:
                {
                    method: 'POST',
                    params: { operation: 'FindById' }

                },
                deleteById: {
                    method: 'POST',
                    params: { operation: 'Remove' }

                },
                updateEntity: {
                    method: 'POST',
                    params: { operation: 'Update' }

                },
                createEntity: {
                    method: 'POST',
                    params: { operation: 'Create' }
                },
                getDepartments: {
                    method: 'GET',
                    params: { operation: 'GetDepartments' },
                    isArray: true
                },
                getDetail: {
                    method: 'GET',
                    params: { operation: 'ReportVoucherDetail' },
                    isArray: true
                },
                getLastestVoucher: {
                    method: 'GET',
                    params: { operation: 'GetLastestVoucheNO' },
                    isArray: true
                },
                searchVoucher: {
                    method: 'GET',
                    params: { operation: 'Search' }
                    //isArray: true
                },
                EHSDetailReport: {
                    method: 'GET',
                    params: { operation: 'DetailExport' },
                    isArray: true
                },
                
                ExportXLSData: {
                    method: 'GET',
                    params: { operation: 'ExportXLSData' },
                    isArray: true
                },
                HSE_LockVoucher:{
                    method:'POST',
                    params:{operation:'HSE_LockVoucher'}
                }
                
            })
        }
        
        VoucherService.prototype.ExportXLSData = function (query, callback) {
            this.GetInfoBasic.ExportXLSData(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }
        VoucherService.prototype.EHSDetailReport = function (query, callback) {
            this.GetInfoBasic.EHSDetailReport(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }
        VoucherService.prototype.Search = function (query, callback) {
            this.GetInfoBasic.searchVoucher(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }
        VoucherService.prototype.GetVoucherNO = function (callback) {
            this.GetInfoBasic.getLastestVoucher().$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }
        VoucherService.prototype.GetVoucher = function (query, callback) {
            this.GetInfoBasic.getList(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }
        VoucherService.prototype.GetVoucherDetail = function (query, callback) {
            this.GetInfoBasic.getDetail(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }
        VoucherService.prototype.GetDepartment = function (query, callback) {
            this.GetInfoBasic.getDepartments(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }
        VoucherService.prototype.CreateVoucher = function (query, callback) {
            this.GetInfoBasic.createEntity(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }
        VoucherService.prototype.DeleteByVoucherID = function (query, callback) {
            console.log(query);
            this.GetInfoBasic.deleteById(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }
        VoucherService.prototype.FindByID = function (query, callback) {
            console.log(query);
            this.GetInfoBasic.getById(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }
        VoucherService.prototype.UpdateVoucher = function (query, callback) {
            console.log(query);
            this.GetInfoBasic.updateEntity(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }
        VoucherService.prototype.GetOnwerComp = function (query, callback) {
            console.log(query);
            this.GetInfoBasic.getOnwerComp(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }
        VoucherService.prototype.GetProcessComp = function (query, callback) {
            console.log(query);
            this.GetInfoBasic.getProcessComp(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }
        return new VoucherService();
    }]);

});