var mongodb = require('./db');
var  Schema=mongodb.mongoose.Schema;

var Process_ModificationAndRiskSchema=new Schema({

    ProcessInstanceId:String,
    activityId:String,
    initiator:String,
    ID:String

},{ collection: 'Process_ModificationAndRisk' });



module.exports = mongodb.mongoose.model("Process_ModificationAndRisk", Process_ModificationAndRiskSchema);
