/**
 * Created by wangyanyan on 2016-08-03.
 */
var mongoose = require('mongoose');
var sworm = require('sworm');
var log = require('log4js').getLogger("GateGuestRoute");
var conn = "";
var Process_GateGuest = require('./../models/Process_GateGuest.js');
var Process_GateGoodsOut = require('./../models/Gate_GoodsOut.js');
var Process_GateUnJointTruck = require('./../models/Process_GateUnJointTruck.js');
var Process_GatePtaEgTruck = require('./../models/Process_GatePtaEgTruck.js');
var Process_GateBlackList = require('./../models/Process_GateBlackList.js');
var Process_GateJointTruck = require('./../models/Process_GateJointTruck.js')
var process_ContractorQuaProcess = require('./../models/Process_ContractorQuaProcess.js');
var process_FEPVConInfoCancel = require('./../models/Process_FEPVConInfoCancel.js');
var process_GateContractorInfo = require('./../models/Process_GateContractorInfo.js');
var process_QCGrades = require('./../models/Process_QCGrades.js');
var process_QCOverGrade = require('./../models/Process_QCOverGrade.js');
var process_EMCS = require('./../models/Process_EMCS.js');
var process_CReportHSE = require('./../models/Process_CReportHSE.js');
var Process_ModificationAndRisk = require('../models/Process_ModificationAndRisk.js');
var Process_EvaluationAndClosing = require('../models/Process_EvaluationAndClosing.js');
var Process_EastGateInOut = require('../models/Process_EastGateInOut.js');
var Process_DenounceContractorProcess = require('../models/Process_DenounceContractorProcess.js');
var Process_ContractorDiscipline = require('../models/Process_ContractorDiscipline.js');

exports.DBSQLConnection = function (config) {
    console.log("-Connection--")
    conn = {
        driver: 'mssql',
        config: {
            user: config.dbuser,
            password: config.dbpassword,
            server: config.datasource,
            database: config.database
        }
    };
};

exports.Get_ContractorDisciplinePID = (req, res) => {
    var query = {}
    console.log(req.query.VoucherID);
    if (!req.query.VoucherID) {
        res.send(500, "The Voucher Not Exist!")
    }
    query.activityId = 'startevent1';
    query.VoucherID = req.query.VoucherID;
    console.log(req.query.VoucherID);
    Process_ContractorDiscipline.find(query, (err, dbdata) => {
        if (err) {
            log.debug(err);
            res.send(500, err)
        }
        else {
            var data = {}
           
            if (dbdata[dbdata.length-1]) {
                data.ProcessInstanceId = dbdata[dbdata.length-1].ProcessInstanceId;
                return res.json(data);
            } else {
                res.send(500, "There is no any history process!");
            }
        }
    })
}

exports.Get_DenounceContractorPID = (req, res) => {
    var query = {}
    console.log(req.query.VoucherID);
    if (!req.query.VoucherID) {
        res.send(500, "The Voucher Not Exist!")
    }
    query.activityId = 'startevent1';
    query.VoucherID = req.query.VoucherID;
    console.log(req.query.VoucherID);
    Process_DenounceContractorProcess.find(query, (err, dbdata) => {
        if (err) {
            log.debug(err);
            res.send(500, err)
        }
        else {
            var data = {}
           
            if (dbdata[dbdata.length-1]) {
                data.ProcessInstanceId = dbdata[dbdata.length-1].ProcessInstanceId;
                return res.json(data);
            } else {
                res.send(500, "There is no any history process!");
            }
        }
    })
}

exports.Get_EastGateInOutPID = (req, res) => {
    var query = {}
    console.log(req.query.VoucherID);
    if (!req.query.VoucherID) {
        res.send(500, "The Voucher Not Exist!")
    }
    query.activityId = 'startevent1';
    query.VoucherID = req.query.VoucherID;
    console.log(req.query.VoucherID);
    Process_EastGateInOut.find(query, (err, dbdata) => {
        if (err) {
            log.debug(err);
            res.send(500, err)
        }
        else {
            var data = {}
           
            if (dbdata[dbdata.length-1]) {
                data.ProcessInstanceId = dbdata[dbdata.length-1].ProcessInstanceId;
                return res.json(data);
            } else {
                res.send(500, "There is no any history process!");
            }
        }
    })
}

exports.Get_ModificationAndRiskPID = (req, res) => {
    var query = {}
    console.log(req.query.VoucherID);
    if (!req.query.VoucherID) {
        res.send(500, "The Voucher Not Exist!")
    }
    query.activityId = 'startevent1';
    query.VoucherID = req.query.VoucherID;
    console.log(req.query.VoucherID);
    Process_ModificationAndRisk.find(query, (err, dbdata) => {
        if (err) {
            log.debug(err);
            res.send(500, err)
        }
        else {
            var data = {}
           
            if (dbdata[dbdata.length-1]) {
                data.ProcessInstanceId = dbdata[dbdata.length-1].ProcessInstanceId;
                return res.json(data);
            } else {
                res.send(500, "There is no any history process!");
            }
        }
    })
}

exports.Get_EvaluationAndClosingPID = (req, res) => {
    var query = {}
    console.log(req.query.VoucherID);
    if (!req.query.VoucherID) {
        res.send(500, "The Voucher Not Exist!")
    }
    query.activityId = 'startevent1';
    query.VoucherID = req.query.VoucherID;
    console.log(req.query.VoucherID);
    Process_EvaluationAndClosing.find(query, (err, dbdata) => {
        if (err) {
            log.debug(err);
            res.send(500, err)
        }
        else {
            var data = {}
           
            if (dbdata[dbdata.length-1]) {
                data.ProcessInstanceId = dbdata[dbdata.length-1].ProcessInstanceId;
                return res.json(data);
            } else {
                res.send(500, "There is no any history process!");
            }
        }
    })
}

exports.Get_CReportHSE_ID =(req,res)=>{
    var query={}
    console.log(req.query.Rp_ID);
    if(!req.query.Rp_ID){
        res.send(500,"The Voucher Not Exist!")
    }
    query.activityId ='startevent1';
    query.Rp_ID =req.query.Rp_ID;
    process_CReportHSE.find(query,(err,dbdata)=>{
        debugger;
    if(err){
        log.debug(err);
        res.send(500,err)
    }
    else{
        var data ={}
        if(dbdata[0]){
            data.ProcessInstanceId =dbdata[0].ProcessInstanceId;
            return res.json(data);
        }else{
            res.send(500,"There is no any history process!");
        }
    }
})
}
exports.GetQCOverGrade =(req,res)=>{
    var query={}
    console.log(req.query.OverID);
    if(!req.query.OverID){
        res.send(500,"The Voucher Not Exist!")
    }
    query.activityId ='StartEvent_2';
    query.OverID =req.query.OverID;
    process_QCOverGrade.find(query,(err,dbdata)=>{
        debugger;
    if(err){
        log.debug(err);
        res.send(500,err)
    }
    else{
        var data ={}
        if(dbdata[0]){
            data.ProcessInstanceId =dbdata[0].ProcessInstanceId;
            return res.json(data);
        }else{
            res.send(500,"There is no any history process!");
        }
    }
})
}
exports.GetQCGrades=function(req,res){
  
    var ret = {};
    var query = {};
    if (!req.query.ID) {
        res.send(500, "没有单据号码");
    }
    query.activityId = 'StartEvent_1'
    query.ID=req.query.ID;
    console.log(query);
    process_QCGrades.find(query, function (err, dbdata) {
        if (err) {
            log.debug(err);
            res.send(500, err);
        } else {
            var data = {};
            if (dbdata[0]) {
                data.ProcessInstanceId = dbdata[0].ProcessInstanceId;
                return res.json(data);
            } else {
                res.send(500, "还没有流程日志");
            }
        }

    });
}
//承揽商取消日志
exports.getContractorCancelByEmployer=function(req,res)
{
    var query={};
    query.EmployerId=req.query.employerid;
    query.activityId="StartEvent_Create";
    console.log(query);
    process_FEPVConInfoCancel.find(query,function (err, dbdata) {
        // console.log(dbdata);
        if(err){
            res.send(500,err);
        }else
        {
            res.send(200,dbdata);
        }
    });
}
//承揽商日志
exports.getContractorInfoByEmployer=function(req,res)
{
    var query={};
    query.EmployerId=req.query.employerid;
    query.activityId="StartEvent_Create";
    console.log(query);
    process_GateContractorInfo.find(query,function (err, dbdata) {
        // console.log(dbdata);
        if(err){
            res.send(500,err);
        }else
        {
            res.send(200,dbdata);
        }
    });
}
//承揽商办卡的信息查询
exports.getContractorQuaInfoByCard=function(req,res){
    var query={};
    query.eventStart_Employer=req.query.employer;
    query.eventStart_IdCard=req.query.idCard;
    query.activityId="eventStart";
    console.log(query);
    process_ContractorQuaProcess.find(query,function (err, dbdata) {
        // console.log(dbdata);
        if(err){
            res.send(500,err);
        }else
        {
            res.send(200,dbdata);
        }
    });
}

exports.queryGateGuestPID = function (req, res) {
    var ret = {};
    var query = {};
    if (!req.query.VoucherID) {
        res.send(500, "没有单据号码");
    }
    query.activityId = req.query.activityName;
    query.VoucherID = req.query.VoucherID;
    
    Process_GateGuest.find(query, function (err, dbdata) {
        if (err) {
            log.debug(err);
            res.send(500, err);
        } else {
            var data = {};
            if (dbdata[0]) {
                data.ProcessInstanceId = dbdata[0].ProcessInstanceId;
                return res.json(data);
            } else {
                res.send(500, "还没有流程日志");
            }
        }

    });
}
exports.queryGatePtaEgTruckPID = function (req, res) {
    var ret = {};
    var query = {};
    if (!req.query.VoucherID) {
        res.send(500, "单据号为空");
    }
    query.activityId = req.query.activityId;
    query.VoucherID = req.query.VoucherID;
    console.log(query);
    Process_GatePtaEgTruck.find(query, function (err, dbdata) {
        if (err) {
            log.error(err);
            res.send(500, err);
        } else {
            var data = {};
            if (dbdata[0]) {
                data.ProcessInstanceId = dbdata[0].ProcessInstanceId;
                return res.json(data);
            } else {
                res.send(500, "还没有流程日志");
            }
        }

    });
}
exports.queryGateUnjointTruckPID = function (req, res) {
    console.log("queryGateUnjointTruckPID")
    var ret = {};
    var query = {};
    if (!req.query.VoucherID) {
        res.send(500, "VoucherID is Null");
    }
    query.activityId = req.query.activityId;
    query.VoucherID = req.query.VoucherID;
    console.log(query);
    Process_GateUnJointTruck.find(query, function (err, dbdata) {
        if (err) {
            log.error(err);
            res.send(500, err);
        } else {
            var data = {};
            if (dbdata[0]) {
                data.ProcessInstanceId = dbdata[0].ProcessInstanceId;
                return res.json(data);
            } else {
                res.send(500, "No process log");
            }
        }

    });
}
exports.queryGoodsOutPID = function (req, res) {
    var ret = {};
    var query = {};
    if (!req.query.VoucherID) {
        res.send(500, "VoucherID is NUll");
    }
    query.activityId = req.query.activityName;
    query.VoucherID = req.query.VoucherID;
    console.log('MArco: @@@'+query);

    var emcs = new process_EMCS({
        ProcessInstanceId: 'marco',
        activityName:'Activity_Marco',
        VoucherID:'Voucher_marco',
        Name:'Marco'
    })
    emcs.save(function(res){
        console.log(res);
    })
    Process_GateGoodsOut.find(query, function (err, dbdata) {
        if (err) {
            log.error(err);
            res.send(500, err);
        } else {
            var data = {};
            if (dbdata[0]) {
                data = dbdata[0];
              //  data.taskDefinitionKey=  dbdata[0].taskDefinitionKey;
                return res.json(data);
            } else {
                res.send(500, "It is no Log");
            }
        }

    });
}
exports.queryGateJointTruckPID = function (req, res) {
    var ret = {};
    var query = {};
    if (!req.query.VoucherID) {
        res.send(500, "No VoucherID");
    }
    query.activityId = req.query.activityId;
    query.VoucherID = req.query.VoucherID;
    console.log(query);
    Process_GateJointTruck.find(query, function (err, dbdata) {
        if (err) {
            log.error(err);
            res.send(500, err);
        } else {
            var data = {};
            if (dbdata[0]) {
                data.ProcessInstanceId = dbdata[0].ProcessInstanceId;
                return res.json(data);
            } else {
                res.send(500, "No process log");
            }
        }

    });
}
exports.queryGateBlackListPI = function (req, res) {
    var ret = {};
    var query = {};
    if (!req.query.VoucherID) {
        res.send(500, "NO VoucherID");
    }
    query.activityName = req.query.activityName;
    query.VoucherID = req.query.VoucherID;
    console.log(query);
    Process_GateBlackList.find(query, function (err, dbdata) {
        if (err) {
            log.error(err);
            res.send(500, err);
        } else {
            var data = {};
            if (dbdata[0]) {
                data.ProcessInstanceId = dbdata[0].ProcessInstanceId;
                return res.json(data);
            } else {
                res.send(500, "No process log");
            }
        }

    });
}
exports.SaveGood = function (req, res) {
    console.log("saveGood");
    var docNumber = req.body.DocNumber

    //req is the object you passed
    sworm.db(conn).then(function (db) {
        db.query('insert into Test values (@DocNumber)', {DocNumber: docNumber})
            .then(function (result) {
                res.send(200, result);
            })
            .catch(function (error) {
                res.send(500, error);
            })
            .finally(function () {
                db.close();
            });
    })
}
exports.GetGuestTypes = function (req, res) {
    console.log("GetGuestTypes");
    sworm.db(conn).then(function (db) {
        db.query('EXEC D_GuestType ', {})
            .then(function (result) {
                res.send(200, result);
            })
            .catch(function (error) {
                res.send(500, error);
            })
            .finally(function () {
                db.close();
            });
    })
}
exports.GetRegions = function (req, res) {
    console.log("GetRegions");
    sworm.db(conn).then(function (db) {
        db.query('EXEC D_Regions ', {})
            .then(function (result) {
                res.send(200, result);
            })
            .catch(function (error) {
                res.send(500, error);
            })
            .finally(function () {
                db.close();
            });
    })
}
exports.EmployeeName = function (req, res) {
    console.log("EmployeeName");
    res.send(200, [{"Name": "Cassie"}]);
}
