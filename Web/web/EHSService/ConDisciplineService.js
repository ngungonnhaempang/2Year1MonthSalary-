
define(['app', 'angular'], function (app, angular) {
    app.service("ConDisciplineService", ['$resource', '$http', 'Auth', '$translate', function ($resource, $http, Auth, $translate) {
        function ConDisciplineService() {
            this.searchISO = $resource("/ehs/contractor/ConDis/Info/SearchISO", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.saveISO_Application = $resource("/ehs/contractor/ConDis/Info/SaveISO_Application", {}, {
                save: { method: 'POST' }
            });

            this.getStatus = $resource("/ehs/contractor/ConDis/Info/GetStatus", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getAppicationType = $resource("/ehs/contractor/ConDis/Info/GetAppicationType", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getCategoryLevel = $resource("/ehs/contractor/ConDis/Info/GetCategoryLevel", {}, {
                get: { method: 'GET' }
            });

            this.getCategoryParent = $resource("/ehs/contractor/ConDis/Info/GetCategoryParent", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.saveCategory = $resource("/ehs/contractor/ConDis/Info/SaveCategory", {}, {
                save: { method: 'POST' }
            });

            this.saveSubCategory = $resource("/ehs/contractor/ConDis/Info/SaveSubCategory", {}, {
                save: { method: 'POST' }
            });

            this.searchCategory = $resource("/ehs/contractor/ConDis/Info/SearchCategory", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.changeStatusCategory = $resource("/ehs/contractor/ConDis/Info/ChangeStatusCategory", {}, {
                save: { method: 'PUT' }
            });

            this.checkExistCode = $resource("/ehs/contractor/ConDis/Info/CheckExistCode", {}, {
                get: { method: 'GET' }
            });

            this.submitCategory = $resource("/ehs/contractor/ConDis/Info/SubmitCategory", {}, {
                post: { method: 'POST' }
            });

            this.updateOrUpgradeSubmit = $resource("/ehs/contractor/ConDis/Info/UpdateOrUpgradeSubmit", {}, {
                post: { method: 'POST' }
            });

            this.checkDuplicateNumbering = $resource("/ehs/contractor/ConDis/Info/CheckDuplicateNumbering", {}, {
                get: { method: 'GET' }
            });

            this.sendCategoryMail = $resource("/ehs/contractor/ConDis/Info/SendCategoryMail", {}, {
                post: { method: 'POST' }
            });
            
            this.getCategoryType = $resource("/ehs/contractor/ConDis/Info/GetCategoryType", {}, {
                get: { method: 'GET', isArray: true }
            });


            //-----------Denounce contractor-------------//
            this.deleteFile = $resource("/ehs/contractor/files/DeleteFile", {}, {
                save: { method: 'DELETE' }
            });

            this.search_DenounceContractor = $resource("/ehs/contractor/ConDis/Denounce/SearchDenounce", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.save_DenounceContractor = $resource("/ehs/contractor/ConDis/Denounce/Save_DenounceContractor", {}, {
                save: { method: 'POST' }
            });

            this.getDenounce = $resource("/ehs/contractor/ConDis/Denounce/GetDenounce", {}, {
                get: { method: 'GET' }
            });

            this.changeStatus = $resource("/ehs/contractor/ConDis/Denounce/UpdateStatus", {}, {
                save: { method: 'PUT' }
            });

            this.getCheckerByKind = $resource("ehs/contractor/ConDis/Denounce/GetCheckerByKind", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getCheckers = $resource("ehs/contractor/ConDis/Denounce/GetCheckers", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.getDenouncePID = $resource("/ehs/DenounceContractorProcess/PID", {}, {
                get: { method: 'GET' }
            });

            this.sendMail_Denounce = $resource("ehs/contractor/ConDis/Denounce/SendMail", {}, {
                post: { method: 'POST' }
            });

            this.getDenounceChecker = $resource("ehs/contractor/ConDis/Denounce/GetDenounceChecker", {}, {
                get: { method: 'GET', isArray: true }
            });

            

            //-----------Violation Confirmation -------------//

            this.search_ViolationConfirmation = $resource("/ehs/contractor/ConDis/Violation/SearchViolation", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.save_ViolationConfirmation = $resource("/ehs/contractor/ConDis/Violation/Save_ViolationConfirmation", {}, {
                save: { method: 'POST' }
            });

            this.getViolation = $resource("/ehs/contractor/ConDis/Violation/GetViolation", {}, {
                get: { method: 'GET' }
            });

            this.changeStatus_Violation = $resource("/ehs/contractor/ConDis/Violation/UpdateStatus", {}, {
                save: { method: 'PUT' }
            });
            
             //-----------Contractor Discipline-------------//

            this.search_ContractorDiscipline = $resource("/ehs/contractor/ConDis/Discipline/SearchDiscipline", {}, {
                get: { method: 'GET', isArray: true }
            });

            this.save_ContractorDiscipline = $resource("/ehs/contractor/ConDis/Discipline/Save_ContractorDiscipline", {}, {
                save: { method: 'POST' }
            });

            this.getDiscipline = $resource("/ehs/contractor/ConDis/Discipline/GetDiscipline", {}, {
                get: { method: 'GET' }
            });

            this.changeStatus_Discipline = $resource("/ehs/contractor/ConDis/Discipline/UpdateStatus", {}, {
                save: { method: 'PUT' }
            });

            this.sendMail_Discipline = $resource("ehs/contractor/ConDis/Discipline/SendDisciplineMail", {}, {
                post: { method: 'POST' }
            });

            this.getDisciplinePID = $resource("/ehs/ContractorDiscipline/PID", {}, {
                get: { method: 'GET' }
            });
            
            this.confirmPayment = $resource("ehs/contractor/ConDis/Discipline/ConfirmPayment", {}, {
                post: { method: 'POST' }
            });

            this.imgToBase64 = $resource("/api/cmis/imgUrl", {}, {
                get: { method: 'GET' }
            });

            this.savePDF = $resource("ehs/contractor/ConDis/Discipline/SavePDF", {}, {
                post: { method: 'POST' }
            });     

            this.getContractorNotOnSytem = $resource("ehs/contractor/ConDis/Discipline/GetContractorNotOnSytem", {}, {
                get: { method: 'Get', isArray: true}
            });    
            
            
        }


        
        ConDisciplineService.prototype.GetContractorNotOnSytem = function () {
            return this.getContractorNotOnSytem;
        };

        ConDisciplineService.prototype.SavePDF = function () {
            return this.savePDF;
        };

        ConDisciplineService.prototype.ImgToBase64 = function () {
            return this.imgToBase64;
        };

        ConDisciplineService.prototype.ConfirmPayment = function () {
            return this.confirmPayment;
        };
        
        ConDisciplineService.prototype.GetDisciplinePID = function () {
            return this.getDisciplinePID;
        };

        ConDisciplineService.prototype.SendMail_Discipline = function () {
            return this.sendMail_Discipline;
        };

        ConDisciplineService.prototype.ChangeStatus_Discipline = function () {
            return this.changeStatus_Discipline;
        };
        
        ConDisciplineService.prototype.GetDiscipline = function () {
            return this.getDiscipline;
        };

        ConDisciplineService.prototype.Search_Discipline = function () {
            return this.search_ContractorDiscipline;
        };

        ConDisciplineService.prototype.Save_Discipline = function () {
            return this.save_ContractorDiscipline;
        };

        //-----------------------------------------------//
        ConDisciplineService.prototype.Search_Violation = function () {
            return this.search_ViolationConfirmation;
        };

        ConDisciplineService.prototype.Save_Violation = function () {
            return this.save_ViolationConfirmation;
        };

        ConDisciplineService.prototype.ChangeStatus_Violation = function () {
            return this.changeStatus_Violation;
        };

        ConDisciplineService.prototype.GetViolation = function () {
            return this.getViolation;
        };
        
        //---------------------------------------------------------------//
        ConDisciplineService.prototype.GetDenounceChecker = function () {
            return this.getDenounceChecker;
        };

        ConDisciplineService.prototype.SendMail_Denounce = function () {
            return this.sendMail_Denounce;
        };
        
        ConDisciplineService.prototype.GetDenouncePID = function () {
            return this.getDenouncePID;
        };
        
        ConDisciplineService.prototype.GetCheckers = function () {
            return this.getCheckers;
        };

        ConDisciplineService.prototype.GetCheckerByKind = function () {
            return this.getCheckerByKind;
        };

        ConDisciplineService.prototype.ChangeStatus_Denounce = function () {
            return this.changeStatus;
        };

        ConDisciplineService.prototype.GetDenounce = function () {
            return this.getDenounce;
        };

        ConDisciplineService.prototype.DeleteFile = function () {
            return this.deleteFile;
        };

        ConDisciplineService.prototype.Search_Denounce = function () {
            return this.search_DenounceContractor;
        };

        ConDisciplineService.prototype.Save_Denounce = function () {
            return this.save_DenounceContractor;
        };

        //-----------------------------------------------//
        ConDisciplineService.prototype.GetCategoryType = function () {
            return this.getCategoryType;
        };
        

        ConDisciplineService.prototype.SendCategoryMail = function () {
            return this.sendCategoryMail;
        };
        
        ConDisciplineService.prototype.CheckDuplicateNumbering = function () {
            return this.checkDuplicateNumbering;
        };
        
        ConDisciplineService.prototype.UpdateOrUpgradeSubmit = function () {
            return this.updateOrUpgradeSubmit;
        };

        ConDisciplineService.prototype.SubmitCategory = function () {
            return this.submitCategory;
        };
        
        ConDisciplineService.prototype.CheckExistCode = function () {
            return this.checkExistCode;
        };

        ConDisciplineService.prototype.ChangeStatusCategory = function () {
            return this.changeStatusCategory;
        };

        ConDisciplineService.prototype.SearchCategory = function () {
            return this.searchCategory;
        };

        ConDisciplineService.prototype.SaveSubCategory = function () {
            return this.saveSubCategory;
        };

        ConDisciplineService.prototype.SaveCategory = function () {
            return this.saveCategory;
        };

        ConDisciplineService.prototype.GetCategoryParent = function () {
            return this.getCategoryParent;
        };

        ConDisciplineService.prototype.GetCategoryLevel = function () {
            return this.getCategoryLevel;
        };

        ConDisciplineService.prototype.GetAppicationType = function () {
            return this.getAppicationType;
        };

        ConDisciplineService.prototype.GetStatus = function () {
            return this.getStatus;
        };

        ConDisciplineService.prototype.SaveISO_Application = function () {
            return this.saveISO_Application;
        };

        ConDisciplineService.prototype.SearchISO = function () {
            return this.searchISO;
        };

        return new ConDisciplineService();
    }])
});
