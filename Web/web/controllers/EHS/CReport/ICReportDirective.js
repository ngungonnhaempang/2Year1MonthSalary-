define(['app'], function (app) {
    app.directive('createIcReport', ['CReportService', 'InfolistService', 'Auth', '$timeout', 'Notifications', '$translate', '$upload',
        function (CReportService, InfolistService, Auth, $timeout, Notifications, $translate, $upload,) {
            return {
                restrict: 'E',
                controller: function ($scope, $element, $attrs) {

                    var lang = window.localStorage.lang;
                    $scope.IncidentTypeList = InfolistService.Infolist('IncidentType'); // IC only

                    $scope.$watch("recordIC.submittype", function (val) {

                        if (val == 'EVR') $scope.IsEVR = true
                        else IsEVR = false;
                        // ng-change="IsEVR= recordIC.submittype=='EVR'? true: false;"
                    })
                    /** * Init Data to save */
                    function saveInitDataIC() { //function before add/update data
                        var note = {};
                        $scope.count = 0;
                        note.Rp_ID = $scope.recordIC.rp_id || '';
                        var lsFile = [];
                        if ($scope.listfile.length > 0) {
                            $scope.listfile.forEach(element => {
                                if (element.col == "Rp_Location")
                                    $scope.count++;
                                var f = {};
                                f.File_ID = element.name;
                                f.ColumnName = element.col;
                                f.Rp_ID = $scope.recordIC.rp_id || '';
                                lsFile.push(f);
                            })
                        }
                        note.Rp_Status = $scope.recordIC.status || '';
                        note.Rp_Type = 'IC';
                        note.RpIC_Group = $scope.recordIC.icGroup || '';
                        note.Rp_DepartmentID = $scope.recordIC.ic_SubDeparmentid; //  Center Department  
                        note.Rp_SubDepartmentID = $scope.recordIC.ic_SubDeparmentid; //Should be SUbDepartmentID
                        note.Rp_DateTime = $scope.recordIC.icDatetime;
                        note.Rp_Location = $scope.recordIC.icLocation;
                        note.Rp_LocationDetail = $scope.recordIC.icLocationDetail;
                        note.Rp_PreventMeasure = $scope.recordIC.icPrevent;
                        note.RpIC_Description = $scope.recordIC.icProcess;
                        note.RpIC_DirectReason = $scope.recordIC.icDr_reason;
                        note.RpIC_IndirectReason = $scope.recordIC.icIdr_reason;
                        note.RpIC_BasicReason = $scope.recordIC.icBasic;
                        note.RpIC_Damage = $scope.recordIC.icResult;
                        note.RpIC_Damaged_Human = $scope.recordIC.icDamaged_Human;
                        note.RpIC_Damaged_Asset = $scope.recordIC.icDamaged_Asset;
                        note.RpIC_Damaged_Envr = $scope.recordIC.icDamaged_Envr;
                        note.RpIC_Process = $scope.recordIC.icImprove;
                        note.RpIC_Evaluate = $scope.recordIC.icEvaluate;
                        note.RpIC_IncidentType = $scope.recordIC.icType;
                        note.Rp_SubmitType = $scope.recordIC.submittype;
                        note.RpIC_Affect = $scope.recordIC.icAffect;
                        note.RpIC_AffectRange = $scope.recordIC.icAffectRange;
                        note.RpIC_TimeNotif = $scope.recordIC.TimeNotif;
                        note.RpIC_NotifPerson = $scope.recordIC.ICNotifPersion;
                        note.RpIC_ReceivePerson = $scope.recordIC.ICReceivePerson;
                        note.Rp_Date = $scope.recordIC.date || '';
                        note.Rp_Stamp = $scope.recordIC.stamp || '';
                        note.Rp_CreatorID = Auth.username;
                        note.FileAttached = lsFile; //File list
                        note.Estimate_DateComplete = $scope.recordIC.estimateComplete;
                        note.IsUpdateImprove = false;
                        return note;
                    }

                    function SaveIC(data) { //function for create and submit (if yes)
                        CReportService.Create(data, function (res) {
                            console.log(res)
                            if (res.Success) {
                                if ($scope.btnSub) {
                                    $scope.ID_IC = res.Data; //data= RP_ID
                                    $scope.formVariables.push({
                                        name: 'Rp_ID',
                                        value: $scope.ID_IC
                                    });
                                    $scope.SubmitAndChangeStatus($scope.ID_IC);
                                    $scope.btnSub = false;
                                    $('#my-modal').modal('hide');
                                    // $scope.rp_Submittype = $scope.SubmitTypelist.find(item => item.id === data.Rp_SubmitType);
                                } else {
                                    $('#my-modal').modal('hide');
                                    $scope.save_msg();
                                    $scope.Search();
                                }
                            } else {
                                $scope.save_error_msg();
                            }
                        }, function (error) {
                            $scope.save_error_msg();
                        })
                    }
                    /**
                     * Update status by updateByID
                     */
                    function updateByID_IC(data) { //function for update
                        CReportService.Update(data, function (res) {
                            if (res.Success) {

                                if ($scope.btnSub) { //?? N???u l?? singal th?? n??n ?????t t??n ????? li??n t?????ng ?????n ki???u Yes/No h??n.
                                    $('#my-modal').modal('hide');
                                    $scope.submit_success_msg();
                                    $scope.Search();
                                    $scope.btnSub = false;
                                } else {
                                    $('#my-modal').modal('hide');
                                    $scope.update_msg();

                                }
                            } else {
                                $scope.update_error_msg();
                            }
                        },
                            function (error) {
                                $scope.update_error_msg();
                            })
                    }
                    $scope.SaveICReport = function () { //function for saving/submitting
                        var note = saveInitDataIC();
                        var nofile = false;
                        if ($scope.count == 0 && $scope.recordIC.icLocation == "O") {
                            $scope.nofileLoc();
                            nofile = true;
                        }
                        if (nofile) {
                            nofile = false;
                            return;
                        } else {
                            var status = $scope.status;
                            switch (status) {
                                case 'N':
                                    SaveIC(note);
                                    $scope.resetIC();
                                    break;
                                case 'M':
                                    note.Rp_Status = 'D';
                                    updateByID_IC(note);
                                    break;
                                case 'MP':
                                    note.Rp_Status = 'P';
                                    updateByID_IC(note);
                                    break;
                                case 'SM':
                                    note.Rp_Status = 'SM';
                                    $scope.btnSub = true;
                                    updateByID_IC(note);
                                    break;
                            }
                        }
                    };
                    /*
                     *reset data function
                     */
                    $scope.resetIC = function () { //reset modal
                        $scope.recordIC = {};
                        $scope.listfile = [];
                        $scope.Search();
                        $scope.ID_IC = "";
                        $scope.IsEVR = false;
                        // $scope.btnFile = true;
                        $scope.listfile_loc_ic = false;
                        $scope.listfile_process_ic = false;
                        $scope.IC_Department_Disable = false;
                    }
                    // L???y d??? li???u l??n modalIC ????? ch???nh s???a
                    $scope.loadICDetail = function (id) {
                        CReportService.FindByID({
                            Rp_ID: id
                        }, function (data) {
                            $scope.recordIC.rp_id = data.Rp_ID;
                            $scope.recordIC.icGroup = data.RpIC_Group;
                            $scope.recordIC.ic_departmentid = data.Rp_DepartmentID;
                            $scope.recordIC.ic_SubDeparmentid = data.Rp_SubDepartmentID;
                            $scope.recordIC.ic_deparid = data.Rp_DepartmentID;


                            $scope.recordIC.icDatetime = data.Rp_DateTime;
                            $scope.recordIC.icLocation = data.Rp_Location;
                            $scope.recordIC.icLocationDetail = data.Rp_LocationDetail;
                            $scope.recordIC.icPrevent = data.Rp_PreventMeasure;
                            $scope.recordIC.icProcess = data.RpIC_Description;
                            $scope.recordIC.icDr_reason = data.RpIC_DirectReason;
                            $scope.recordIC.icIdr_reason = data.RpIC_IndirectReason;
                            $scope.recordIC.icBasic = data.RpIC_BasicReason;
                            $scope.recordIC.icResult = data.RpIC_Damage;
                            $scope.recordIC.icDamaged_Human = data.RpIC_Damaged_Human;
                            $scope.recordIC.icDamaged_Asset = data.RpIC_Damaged_Asset;
                            $scope.recordIC.icDamaged_Envr = data.RpIC_Damaged_Envr;
                            $scope.recordIC.icImprove = data.RpIC_Process;
                            $scope.recordIC.icEvaluate = data.RpIC_Evaluate;
                            $scope.recordIC.submittype = data.Rp_SubmitType;
                            if (['WasteWater', 'Gas', 'SolidWaste', 'Chemical'].indexOf(data.RpIC_IncidentType) < 0)
                                $scope.icType_CheckOther = true;
                            $scope.recordIC.icType = data.RpIC_IncidentType;
                            $scope.recordIC.icAffect = data.RpIC_Affect;
                            $scope.recordIC.icAffectRange = data.RpIC_AffectRange;
                            $scope.recordIC.TimeNotif = data.RpIC_TimeNotif;
                            $scope.recordIC.ICNotifPersion = data.RpIC_NotifPerson;
                            $scope.recordIC.ICReceivePerson = data.RpIC_ReceivePerson;
                            $scope.listfile = [];
                            data.FileAttached.forEach(element => {
                                var x = {};
                                x.Rp_ID = element.Rp_ID;
                                x.name = element.File_ID;
                                x.col = element.ColumnName;
                                $scope.listfile.push(x);
                            })
                        }, function (error) {
                            Notifications.addError({
                                'status': 'error',
                                'message': res.Message
                            });
                        })
                    };
                    /**********************************************************************************************/
                    /* general-  THIS FOLLOWING PART IS USED BY ITSELF OR FROM OTHER DIRECTIVE                    */
                    /**********************************************************************************************/
                    $scope.btnFile = false;

                    $scope.btnfile = function (id, filename) {
                        $('#' + id).click();
                        // var filein = document.querySelector("#" + id);
                        // var filename = document.querySelector("#" + filename);
                        // filein.click();
                    }
                    //*** UPload file */
                    $scope.listfile = [];
                    $scope.UploadFileHSE = function ($files, _colName) {
                        debugger
                        var isValidFile = checkFileLimited($files, _colName, 6, 3);
                        if (!isValidFile.success) {
                            alert(isValidFile.message)
                        } else {
                            $upload.upload({
                                url: '/Waste/files/Upload',
                                method: "POST",
                                file: $files
                            }).progress(function (evt) { }).then(function (res) {
                                res.data.forEach(x => {
                                    var chuthuong = x.toLowerCase();
                                    var dt = {
                                        name: x,
                                        col: _colName
                                    };

                                    if (chuthuong.includes(".doc") ||
                                        chuthuong.includes(".docx") ||
                                        chuthuong.includes(".pdf") ||
                                        chuthuong.includes(".jpg") ||
                                        chuthuong.includes(".jpeg") ||
                                        chuthuong.includes(".png")) {

                                        if (_colName == 'Injury_Description')
                                            $scope.listfileAC.push(dt);
                                        else
                                            $scope.listfile.push(dt);
                                    } else {
                                        $timeout(function () {
                                            Notifications.addMessage({
                                                'status': 'info',
                                                'message': $translate.instant('FileValidation_MSG')
                                            });
                                        }, 300);
                                        CReportService.DeleteFile({
                                            fname: x
                                        }, function (res) {
                                            if (res.Success) {
                                                console.log('Wrong type of file', res.Success);
                                            }
                                        },
                                            function (error) {
                                                console.log(error);
                                            })
                                        return;
                                    }

                                })

                            })
                        }
                    }

                    /**
                     * Create by Isaac 2019-07-23
                     * @param {file In Upload} $files get all files when Upload
                     * @param {*} maximumSize 
                     * @param {number of TotalFiles} totalFile 
                     */

                    function checkFileLimited($files, colname, maximumSize, totalFile) {
                        const fileCount = $files.length;
                        const maximumFileSize = maximumSize * 1024 * 1024 // 3MB
                        var objectResult = {
                            success: true,
                            message: "Upload Completed!"
                        };
                        //check file exist in list
                        var listOfFiles = (($scope.listfileAC.length + fileCount) > maximumSize ||
                            ($scope.listfile.filter(x => x.col === colname).length + fileCount) > maximumSize) ? true : false;
                        if (fileCount > totalFile || listOfFiles) {
                            objectResult.success = false;
                            objectResult.message = `Your number of files over ${totalFile}`;
                        } else {
                            $files.forEach(item => {
                                if (item.size > maximumFileSize) {
                                    objectResult.success = false;
                                    objectResult.message = `Your file upload over ${maximumSize}MB\n Please uploade another file!`;
                                }
                            })
                        }
                        return objectResult;
                    }
                    // GetBasic ------------ USE FOR BOTH REPORT
                    //--CenterDepartment----
                    CReportService.GetBasic({
                        TableName: "CenterDepartment",
                        Lang: lang || 'EN'
                    }, function (data) {
                        var center_dpm = data; //get center department lift for the comparation later

                        /** Get user's departments (private) */
                        CReportService.GetDataDepartment({
                            emp_id: Auth.username

                        }, function (sub_department_list) {
                            debugger;
                            if (sub_department_list.length > 0) {
                                $scope.EngineDepartmentList = $scope.submitDept = sub_department_list;
                                $scope.engine_department = $scope.recordIC.ic_departmentid = sub_department_list[0].DepartmentID;
                                if (sub_department_list.length == 1)
                                    if ($scope.getLeaderCheck)
                                        $scope.getLeaderCheck(sub_department_list[0].DepartmentID); // No need to choose submitdepartment when there is 1 value
                                center_dpm.forEach(x => {
                                    if (x.CostCenter == sub_department_list[0].DepartmentID.slice(0, 3)) {
                                        $scope.showFactoryInIC = x.Specification;
                                    }
                                })
                                console.log('subdepartment', $scope.recordIC.ic_SubDeparmentid);
                            }
                            // else
                            //     $timeout(function () {
                            //         Notifications.addMessage({
                            //             'status': 'error',
                            //             'message': "Ng?????i d??ng "+ Auth.username+" " + Auth.nickname +  " ch??a ???????c c???p quy???n ????? g???i ????n"
                            //         });
                            //     }, 300);

                        })


                    })

                    $scope.evaluatelist = InfolistService.Infolist('evaluate'); //general list
                    // location combobox
                    $scope.locationlist = InfolistService.Infolist('location'); //general list
                    // x??a file kh???i QCFiles 
                    $scope.removeFile = function (index) {
                        var namefile = {
                            fname: $scope.listfile[index].name
                        };
                        $scope.listfile.splice(index, 1);
                        // CReportService.DeleteFile(namefile, function (res) {
                        //         if (res.Success) {
                        //             console.log(res.Success);
                        //         }
                        //     },
                        //     function (error) {
                        //         console.log(error);
                        //     })
                    }

                    //---------------NOTIFICATION FUNCTIONS (CAN BE USED FOR ACREPORT-DIRECTIVE)-------------------------------------------
                    $scope.save_msg = function () {
                        $timeout(function () {
                            Notifications.addMessage({
                                'status': 'information',
                                'message': $translate.instant('Save_Success_MSG')
                            });
                        }, 200);
                    }
                    $scope.nofileLoc = function () {
                        $timeout(function () {
                            Notifications.addError({
                                'status': 'error',
                                'message': $translate.instant('File_Location_MSG')
                            });
                        }, 200);
                    }
                    $scope.update_msg = function () {
                        $timeout(function () {
                            Notifications.addMessage({
                                'status': 'information',
                                'message': $translate.instant('Update_Success_MSG')
                            });
                        }, 300);
                    }
                    $scope.submit_success_msg = function () {
                        $timeout(function () {
                            Notifications.addMessage({
                                'status': 'info',
                                'message': $translate.instant('Submit_Success_MSG')
                            });
                        }, 300);
                    }
                    $scope.save_error_msg = function () {
                        $timeout(function () {
                            Notifications.addMessage({
                                'status': 'error',
                                'message': $translate.instant('Msg_Save')
                            });
                        }, 300);
                    }
                    $scope.update_error_msg = function (err) {
                        $timeout(function () {
                            Notifications.addMessage({
                                'status': 'error',
                                'message': $translate.instant('UpdateError') + err
                            });
                        }, 300);
                    }

                },
                templateUrl: './forms/EHS/CReport/createICReport.html'
            }
        }
    ]);
});
