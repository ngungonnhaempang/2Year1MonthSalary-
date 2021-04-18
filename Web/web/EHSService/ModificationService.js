
define(['app', 'angular'], function (app, angular) {
    app.service("ModificationService", ['$resource', '$http', 'Auth', '$translate', function ($resource, $http, Auth, $translate) {
        function ModificationService() {
            this.searchISO = $resource("/ehs/modification/Info/SearchISO", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.saveISO_Application = $resource("ehs/modification/Info/SaveISO_Application", {}, {
                save: { method: 'POST' }
            });

            this.getStatus = $resource("ehs/modification/Info/GetStatus", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getAppicationType = $resource("ehs/modification/Info/GetAppicationType", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.searchRange = $resource("/ehs/modification/Info/SearchRange", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.saveRange = $resource("ehs/modification/Info/SaveRange", {}, {
                save: { method: 'POST' }
            });

            this.getCategoryLevel = $resource("ehs/modification/Info/GetCategoryLevel", {}, {
                get: { method: 'GET' }
            });

            this.getCategoryParent = $resource("ehs/modification/Info/GetCategoryParent", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.saveCategory = $resource("ehs/modification/Info/SaveCategory", {}, {
                save: { method: 'POST' }
            });

            this.saveSubCategory = $resource("ehs/modification/Info/SaveSubCategory", {}, {
                save: { method: 'POST' }
            });

            this.searchCategory = $resource("/ehs/modification/Info/SearchCategory", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.changeStatusCategory = $resource("/ehs/modification/Info/ChangeStatusCategory", {}, {
                save: { method: 'PUT' }
            });

            this.checkExistCode = $resource("ehs/modification/Info/CheckExistCode", {}, {
                get: { method: 'GET' }
            });

            this.getDeparment = $resource("ehs/modification/GetDepartment", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getEmployeeInfo = $resource("ehs/modification/GetEmployeeInfo", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.createProjectID = $resource("ehs/modification/CreateProjectID", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.saveMD_Application = $resource("ehs/modification/Save_ModificationApp", {}, {
                save: { method: 'POST' }
            });

            this.searchModificationApp = $resource("/ehs/modification/Search_ModificationApp", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getModificationAppDetail = $resource("/ehs/modification/GetModificationAppDetail", {}, {
                get: { method: 'GET' }
            });

            this.deleteFile = $resource("/ehs/modification/files/DeleteFile", {}, {
                save: { method: 'DELETE' }
            });

            this.updateMDStatus = $resource("/ehs/modification/UpdateStatus", {}, {
                save: { method: 'PUT' }
            });

            this.searchRiskAssessment = $resource("ehs/modification/RiskAssessment/SearchRiskAssessment", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.createRiskAssessment = $resource("ehs/modification/RiskAssessment/CreateRiskAssessment", {}, {
                save: { method: 'POST' }
            });

            this.getModificationProjectID = $resource("ehs/modification/RiskAssessment/GetModificationProjectID", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getRiskAssessmentDetail = $resource("ehs/modification/RiskAssessment/GetRiskAssessmentDetail", {}, {
                get: { method: 'GET' }
            });

            this.searchBeforeOperatingEvaluation = $resource("ehs/modification/BeforeOperatingEvaluation/SearchBeforeOperatingEvaluation", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.createBOEvaluation = $resource("ehs/modification/BeforeOperatingEvaluation/CreateBOEvaluation", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.updateRiskStatus = $resource("/ehs/modification/RiskAssessment/UpdateStatus", {}, {
                save: { method: 'PUT' }
            });

            this.updateBEStatus = $resource("/ehs/modification/BeforeOperatingEvaluation/UpdateStatus", {}, {
                save: { method: 'PUT' }
            });

            this.getBeforeEvaluateDetail = $resource("/ehs/modification/BeforeOperatingEvaluation/GetBeforeEvaluateDetail ", {}, {
                get: { method: 'GET' }
            });

            this.searchClosingEvaluation = $resource("ehs/modification/ClosingEvaluation/SearchClosingEvaluation", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.createClosingEvaluation = $resource("ehs/modification/ClosingEvaluation/CreateClosingEvaluation", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getClosingEvaluateDetail = $resource("ehs/modification/ClosingEvaluation/GetClosingEvaluateDetail", {}, {
                get: { method: 'GET' }
            });

            this.updateCloseStatus = $resource("/ehs/modification/ClosingEvaluation/UpdateStatus", {}, {
                save: { method: 'PUT' }
            });

            this.getProcess = $resource("ehs/modification/GetProcess", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.countMDProject = $resource("ehs/modification/CountModificationProject", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getCheckerByKind = $resource("ehs/modification/Checker/GetCheckerByKind", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getModificationPID = $resource("/ehs/modification/PID", {}, {
                get: { method: 'GET' }
            });

            this.findModificationAppByID = $resource("/ehs/modification/FindModificationAppByID", {}, {
                get: { method: 'GET' }
            });

            this.getModificationAndRiskPID = $resource("/ehs/ModificationAndRisk/PID", {}, {
                get: { method: 'GET' }
            });

            this.getEvaluationAndClosingPID = $resource("/ehs/EvaluationAndClosing/PID", {}, {
                get: { method: 'GET' }
            });
            
            this.getCalculateRiskAssessment = $resource("ehs/modification/RiskAssessment/GetCalculateRiskAssessment", {}, {
                get: { method: 'GET',  isArray: true }
            });

            this.getCheckers = $resource("ehs/modification/Checker/GetCheckers", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.sendMail = $resource("ehs/modification/SendMail", {}, {
                post: { method: 'POST' }
            });
            
            this.checkForeignKey = $resource("ehs/modification/RiskAssessment/CheckForeignKey", {}, {
                get: { method: 'GET' }
            });

            this.sendMailSpecialDelete = $resource("ehs/modification/SendMailSpecialDelete", {}, {
                post: { method: 'POST' }
            });
            
            
            
        }

        ModificationService.prototype.SendMailSpecialDelete = function () {
            return this.sendMailSpecialDelete;
        }

        ModificationService.prototype.CheckForeignKey = function () {
            return this.checkForeignKey;
        }

        ModificationService.prototype.SendMail = function () {
            return this.sendMail;
        }

        ModificationService.prototype.GetCheckers = function () {
            return this.getCheckers;
        }

        ModificationService.prototype.GetCalculateRiskAssessment = function () {
            return this.getCalculateRiskAssessment;
        }


        ModificationService.prototype.GetEvaluationAndClosingPID = function () {
            return this.getEvaluationAndClosingPID;
        }

        ModificationService.prototype.GetModificationAndRiskPID = function () {
            return this.getModificationAndRiskPID;
        }

        ModificationService.prototype.FindModificationAppByID = function () {
            return this.findModificationAppByID;
        }


        ModificationService.prototype.GetModificationPID = function () {
            return this.getModificationPID;
        }

        ModificationService.prototype.GetCheckerByKind = function () {
            return this.getCheckerByKind;
        };

        ModificationService.prototype.CountMDProject = function () {
            return this.countMDProject;
        };

        ModificationService.prototype.GetProcess = function () {
            return this.getProcess;
        };

        ModificationService.prototype.UpdateCloseStatus = function () {
            return this.updateCloseStatus;
        };

        ModificationService.prototype.GetClosingEvaluateDetail = function () {
            return this.getClosingEvaluateDetail;
        };

        ModificationService.prototype.CreateClosingEvaluation = function () {
            return this.createClosingEvaluation;
        };

        ModificationService.prototype.SearchClosingEvaluation = function () {
            return this.searchClosingEvaluation;
        };

        ModificationService.prototype.GetBeforeEvaluateDetail = function () {
            return this.getBeforeEvaluateDetail;
        };

        ModificationService.prototype.UpdateBEStatus = function () {
            return this.updateBEStatus;
        };

        ModificationService.prototype.UpdateRiskStatus = function () {
            return this.updateRiskStatus;
        };


        ModificationService.prototype.CreateBOEvaluation = function () {
            return this.createBOEvaluation;
        };

        ModificationService.prototype.GetRiskAssessmentDetail = function () {
            return this.getRiskAssessmentDetail;
        };

        ModificationService.prototype.CreateRiskAssessment = function () {
            return this.createRiskAssessment;
        };

        ModificationService.prototype.SearchBeforeOperatingEvaluation = function () {
            return this.searchBeforeOperatingEvaluation;
        };

        ModificationService.prototype.GetModificationProjectID = function () {
            return this.getModificationProjectID;
        };

        ModificationService.prototype.SearchRiskAssessment = function () {
            return this.searchRiskAssessment;
        };

        ModificationService.prototype.UpdateMDStatus = function () {
            return this.updateMDStatus;
        };


        ModificationService.prototype.DeleteFile = function () {
            return this.deleteFile;
        };

        ModificationService.prototype.GetModificationAppDetail = function () {
            return this.getModificationAppDetail;
        };

        ModificationService.prototype.Search_ModificationApp = function () {
            return this.searchModificationApp;
        };

        ModificationService.prototype.Save_ModificationApp = function () {
            return this.saveMD_Application;
        };

        ModificationService.prototype.CreateProjectID = function () {
            return this.createProjectID;
        };

        ModificationService.prototype.GetDeparment = function () {
            return this.getDeparment;
        };

        ModificationService.prototype.GetEmployeeInfo = function () {
            return this.getEmployeeInfo;
        };

        ModificationService.prototype.CheckExistCode = function () {
            return this.checkExistCode;
        };

        ModificationService.prototype.ChangeStatusCategory = function () {
            return this.changeStatusCategory;
        };

        ModificationService.prototype.SearchCategory = function () {
            return this.searchCategory;
        };


        ModificationService.prototype.SaveSubCategory = function () {
            return this.saveSubCategory;
        };

        ModificationService.prototype.SaveCategory = function () {
            return this.saveCategory;
        };

        ModificationService.prototype.GetCategoryParent = function () {
            return this.getCategoryParent;
        };

        ModificationService.prototype.GetCategoryLevel = function () {
            return this.getCategoryLevel;
        };

        ModificationService.prototype.SaveRange = function () {
            return this.saveRange;
        };

        ModificationService.prototype.SearchRange = function () {
            return this.searchRange;
        };

        ModificationService.prototype.GetAppicationType = function () {
            return this.getAppicationType;
        };

        ModificationService.prototype.GetStatus = function () {
            return this.getStatus;
        };

        ModificationService.prototype.SaveISO_Application = function () {
            return this.saveISO_Application;
        };

        ModificationService.prototype.SearchISO = function () {
            return this.searchISO;
        };

        return new ModificationService();
    }])
});
