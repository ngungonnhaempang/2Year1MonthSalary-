/**
 * Created by wang.chen on 2016/12/5.
 */
define(['app', 'angular'], function (app, angular) {
    app.service("ConQuaService", ['$resource', '$http', 'Auth', '$translate', function ($resource, $http, Auth, $translate) {
        function ConQuaService() {
            this.ContractorKind = $resource("/ehs/gate/ConQua/GetContractorKind", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getContractorRegion = $resource("/ehs/gate/ConQua/GetContractorRegion", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getContractorJob = $resource("/ehs/gate/ConQua/GetContractorJob", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getContractorDepartment = $resource("/ehs/gate/ConQua/GetContractorDepartment", {}, {
                get: { method: 'GET', isArray: true }
            });

            this._ContractorQualification = $resource("/ehs/gate/ConQua/Get", {}, {
                get: { method: 'GET', isArray: true },
                save: { method: 'POST' },
                getDetailHeader: { method: 'GET', isArray: true },
                getList: { method: 'GET', isArray: true },
				getByName: { method: 'GET'}
            });

            this.getContractorUpdate = $resource("/ehs/gate/ConQua/GetContractorUpdate", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getContractorCheck = $resource("/ehs/gate/ConQua/GetContractorCheck", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getContractorConfirm = $resource("/ehs/gate/ConQua/GetContractorConfirm", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.saveConQua = $resource("/ehs/gate/ConQua/Get", {}, {
                save: { method: 'POST' }
            });
            
            this.conQuaSaveStatus = $resource("/ehs/gate/ConQua/ConQuaSaveStatus", {}, {
                save: { method: 'PUT' }
            });

            this.conQuaSaveStatusSuspend = $resource("/ehs/gate/ConQua/ConQuaSaveStatusSuspend", {}, {
                save: { method: 'PUT' }
            });

            this.contractorQuaSaveStatus = $resource("/ehs/gate/Contractor/ContractorQuaSaveStatus", {}, {
                save: { method: 'PUT' }
            });

            this.contractorSaveStatusSuspend = $resource("/ehs/gate/Contractor/ContractorSaveStatusSuspend", {}, {
                save: { method: 'PUT' }
            });

            this.getContractorQualification = $resource("/ehs/gate/Contractor/GetContractorsInfoByDept", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.saveContractor = $resource("/ehs/gate/Contractor/Save", {}, {
                save: { method: 'POST' }
            });

            this.getContractor = $resource("/ehs/gate/Contractor/Get", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getContractorEmployer = $resource("/ehs/gate/Contractor/GetAllEmployeeInContractor", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.contractorDone = $resource("/ehs/gate/Contractor/Done", {}, {
                save: { method: 'GET' }
            });

            this.contractorList = $resource("/ehs/gate/Contractor/GetContractors", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.queryContractorQuaProcess = $resource("/ehs/contractorQua/queryProcess", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.queryContractorPID = $resource("/ehs/ContractorInfo/PID", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.queryContractorCancelPID = $resource("/ehs/ContractorCancel/PID", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.queryContractorQuaCancelPID = $resource("/ehs/ContractorQuaCancel/PID", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.ContraImport = $resource("/ehs/gate/Contractor/ImportSave", {}, {
                save: { method: 'POST' }
            });

            this.ContraImportRemove = $resource("/ehs/gate/Contractor/ImportRemove", {}, {
                remove: { method: 'POST' }
            });

            this.contractorStatistic = $resource("/ehs/gate/ConQua/CardLogs", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.contractorStatisticDetail = $resource("/ehs/gate/ConQua/CardLogsDetail", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getContractorByIdCard = $resource("/ehs/gate/Contractor/GetContractorByIdCard", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.GetInfoBasic = $resource('/ehs/gate/Contractor/:operation', {}, {

                GetEmployee: {
                    method: 'GET',
                    params: {
                        operation: 'GetEmployee'
                    },
                    isArray: true
                },

                GetDepartment: {
                    method: 'GET',
                    params: {
                        operation: 'GetDepartment'
                    },
                    isArray: true
                },

                GetVoucherID: {
                    method: 'GET',
                    params: {
                        operation: 'GetVoucherID'
                    },
                    isArray: true
                }
            });

            this.notConfirm = $resource("/ehs/gate/Contractor/NotConfirm", {}, {
                save: { method: 'PUT' }
            });

            this.sendMail = $resource("/ehs/gate/Contractor/SuspendedMail", {}, {
                get: { method: 'POST'}
            });
            
            this.sendMailCon = $resource("/ehs/gate/ConQua/SuspendedMail", {}, {
                get: { method: 'POST'}
            });

            this.sendExtendMail = $resource("/ehs/gate/ConQua/ExtendMail", {}, {
                get: { method: 'POST' }
            });

            this.cancelSuspendContractor = $resource("/ehs/gate/ConQua/CancelSuspendContractor", {}, {
                save: { method: 'PUT' }
            });

            this.cancelSuspendEmployee = $resource("/ehs/gate/Contractor/CancelSuspendEmployee", {}, {
                save: { method: 'PUT' }
            });

            this.sendConfirmdMail = $resource("/ehs/gate/Contractor/ConfirmdMail", {}, {
                get: { method: 'POST' }
            });

            this.conQuaDelete = $resource("/ehs/gate/ConQua/Delete", {}, {
                save: { method: 'PUT' }
            });
            
            this.countContractor = $resource("/ehs/gate/Contractor/CountContractor", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.sendMailSubmit = $resource("/ehs/gate/ConQua/SendMailSubmit", {}, {
                get: { method: 'POST' }
            });

            this.contractorConfirm = $resource("/ehs/gate/Contractor/ContractorConfirm", {}, {
                save: { method: 'PUT' }
            });

            this.contractorUploadFile = $resource("/ehs/gate/ConQua/UploadFile", {}, {
                save: { method: 'POST' }
            });

            this.contractorDeleteFile = $resource("/ehs/gate/ConQua/DeleteFile", {}, {
                save: { method: 'DELETE' }
            });

            this.contractorUpdateMark = $resource("/ehs/gate/Contractor/UpdateMark", {}, {
                save: { method: 'GET' }
            });

            this.contractorGetRole = $resource("/ehs/gate/Contractor/GetRole", {}, {
                get: { method: 'GET',  isArray: true}
            });

            this.saveEastGateVoucher = $resource("/ehs/gate/InOutEastGate/SaveEastGateVoucher", {}, {
                save: { method: 'POST' }
            });
            
            this.searchRegisterVoucher = $resource("/ehs/gate/InOutEastGate/SearchRegisterVoucher", {}, {
                get: { method: 'GET', isArray:true }
            });
            
            this.updateStatusVoucher = $resource("/ehs/gate/InOutEastGate/UpdateStatusVoucher", {}, {
                save: { method: 'PUT' }
            });

            this.getEastGatePID = $resource("/ehs/EastGateInOut/PID", {}, {
                get: { method: 'GET' }
            });

            
            this.sendMailEastGate = $resource("ehs/gate/InOutEastGate/EG_SendMail", {}, {
                post: { method: 'POST' }
            });       


            this.uploadUserInDevice = $resource("ehs/timekeeper/uploadUser", {}, {
                get: { method: 'GET' }
            }); 
            
            this.deleteUserInDevice = $resource("ehs/timekeeper/deleteUser", {}, {
                get: { method: 'GET' }
            });   

            this.searchLog = $resource("/ehs/gate/ConQua/SearchLog", {}, {
                get: { method: 'GET', isArray:true }
            });  

            this.searchStatistic = $resource("/ehs/gate/ConQua/SearchStatistic", {}, {
                get: { method: 'GET', isArray:true }
            });  

            this.exportLog = $resource("/ehs/gate/ConQua/ExportLog", {}, {
                get: { method: 'GET'}
            });

            this.exportStatistic = $resource("/ehs/gate/ConQua/ExportStatistic", {}, {
                get: { method: 'GET' }
            });
            
        }

        ConQuaService.prototype.ExportStatistic = function () {
            return this.exportStatistic;
        };

        ConQuaService.prototype.ExportLog = function () {
            return this.exportLog;
        };

        ConQuaService.prototype.SearchStatistic = function () {
            return this.searchStatistic;
        };

        ConQuaService.prototype.SearchLog = function () {
            return this.searchLog;
        };

        ConQuaService.prototype.deleteUser = function () {
            return this.deleteUserInDevice;
        };

        ConQuaService.prototype.uploadUser = function () {
            return this.uploadUserInDevice;
        };


        ConQuaService.prototype.SendMailEastGate = function () {
            return this.sendMailEastGate;
        };

        ConQuaService.prototype.GetEastGateInOutPID = function () {
            return this.getEastGatePID;
        };

        ConQuaService.prototype.UpdateStatusVoucher = function () {
            return this.updateStatusVoucher;
        };

        ConQuaService.prototype.SearchRegisterVoucher = function () {
            return this.searchRegisterVoucher;
        };

        ConQuaService.prototype.SaveEastGateVoucher = function () {
            return this.saveEastGateVoucher;
        };

        ConQuaService.prototype.GetEastGateVoucherID = function () {
            return this.getEastGateVoucherID;
        };

        ConQuaService.prototype.GetRole = function () {
            return this.contractorGetRole;
        };

        ConQuaService.prototype.UpdateMark = function () {
            return this.contractorUpdateMark;
        };

        ConQuaService.prototype.DeleteFile = function () {
            return this.contractorDeleteFile;
        };

        ConQuaService.prototype.UploadFile = function () {
            return this.contractorUploadFile;
        };

        ConQuaService.prototype.ContractorConfirm = function () {
            return this.contractorConfirm;
        };

        ConQuaService.prototype.SendMailSubmit = function () {
            return this.sendMailSubmit;
        };
     
        ConQuaService.prototype.CountContractor = function () {
            return this.countContractor;
        };

        ConQuaService.prototype.Delete = function () {
            return this.conQuaDelete;
        };

        ConQuaService.prototype.SendConfirmdMail = function () {
            return this.sendConfirmdMail;
        };

        ConQuaService.prototype.CancelSuspendEmployee = function () {
            return this.cancelSuspendEmployee;
        };

        ConQuaService.prototype.CancelSuspendContractor = function () {
            return this.cancelSuspendContractor;
        };

        ConQuaService.prototype.SuspendedMail = function () {
            return this.sendMail;
        };

        ConQuaService.prototype.ConSuspendedMail = function () {
            return this.sendMailCon;
        };
        ConQuaService.prototype.ExtendMail = function () {
            return this.sendExtendMail;
        };

        ConQuaService.prototype.NotConfirm = function () {
            return this.notConfirm;
        };

        ConQuaService.prototype.GetVoucherID = function (query, callback) {
            console.log(query);
            this.GetInfoBasic.GetVoucherID(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }
     
        ConQuaService.prototype.GetEmployee = function (query, callback) {
            console.log(query);
            this.GetInfoBasic.GetEmployee(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }

        ConQuaService.prototype.GetDepartment = function (query, callback) {
            console.log(query);
            this.GetInfoBasic.GetDepartment(query).$promise.then(function (data) {
                callback(data);
            }, function (ex) {
                console.log(ex);
                callback(null, ex);
            })
        }

        ConQuaService.prototype.GetContractorByIdCard = function () {
            return this.getContractorByIdCard;
        }
        ConQuaService.prototype.GetContractorUpdate = function () {
            return this.getContractorUpdate;
        }
        ConQuaService.prototype.GetContractorCheck = function () {
            return this.getContractorCheck;
        }
        ConQuaService.prototype.GetContractorConfirm = function () {
            return this.getContractorConfirm;
        }
        ConQuaService.prototype.ContractorStatistic = function () {
            return this.contractorStatistic;
        }
        ConQuaService.prototype.ContractorStatisticDetail = function () {
            return this.contractorStatisticDetail;
        }
        ConQuaService.prototype.ContractorImport = function () {
            return this.ContraImport;
        };
        ConQuaService.prototype.ContractorImportRemove = function () {
            return this.ContraImportRemove;
        };
        ConQuaService.prototype.getContractorPID = function () {
            return this.queryContractorPID;
        };
        ConQuaService.prototype.getContractorCancelPID = function () {
            return this.queryContractorCancelPID;
        }

        ConQuaService.prototype.getContractorQuaProcess = function () {
            return this.queryContractorQuaProcess;
        };

        ConQuaService.prototype.ContractorDone = function () {
            return this.contractorDone;
        };
        ConQuaService.prototype.ConQuaSaveStatus = function () {
            return this.conQuaSaveStatus;
        };

        ConQuaService.prototype.SendMail = function () {
            return this.SendMail;
        };

        ConQuaService.prototype.ConQuaSaveStatusSuspend = function () {
            return this.conQuaSaveStatusSuspend;
        };
        ConQuaService.prototype.ContractorSaveStatusSuspend = function () {
            return this.contractorSaveStatusSuspend;
        };
        ConQuaService.prototype.ContractorQuaSaveStatus = function () {
            return this.contractorQuaSaveStatus;
        };

        ConQuaService.prototype.GetContractor = function () {
            return this.getContractor;
        };
        ConQuaService.prototype.GetContractorEmployer = function () {
            return this.getContractorEmployer;
        };

        ConQuaService.prototype.GetContractorQualification = function () {
            return this.getContractorQualification;
        };
        ConQuaService.prototype.ContractorList = function () {
            return this.contractorList;
        }
        ConQuaService.prototype.SaveContractor = function () {
            return this.saveContractor;
        };
        ConQuaService.prototype.GetEquipmentList = function () {
            return this.getEquipmentList;
        };
        ConQuaService.prototype.GetCerTypes = function () {
            return this.getCerTypes;
        };
        ConQuaService.prototype.GetIssuedBy = function () {
            return this.getIssuedBy;
        };
        ConQuaService.prototype.GetInsTypes = function () {
            return this.getInsTypes;
        };
        ConQuaService.prototype.ContractorQualification = function () {
            return this._ContractorQualification;
        };
        //增承揽商资质
        ConQuaService.prototype.CreateContractorQualification = function () {
            return this.saveConQua;
        };
        ConQuaService.prototype.GetIns = function () {
            return this.getIns;
        };
        ConQuaService.prototype.GetCer = function () {
            return this.getCer;
        };
        ConQuaService.prototype.ContractorTypeList = function () {
            return this.ContractorKind;
        };
        ConQuaService.prototype.GetContractorRegion = function () {
            return this.getContractorRegion;
        };
        ConQuaService.prototype.GetContractorJob = function () {
            return this.getContractorJob;
        };

        ConQuaService.prototype.GetContractorDepartment = function () {
            return this.getContractorDepartment;
        };

        return new ConQuaService();
    }])
});
