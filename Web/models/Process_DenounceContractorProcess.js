var mongodb = require('./db');
var  Schema=mongodb.mongoose.Schema;

var Process_DenounceContractorProcessSchema=new Schema({

    ProcessInstanceId:String,
    activityId:String,
    initiator:String,
    ID:String

},{ collection: 'Process_DenounceContractorProcess' });



module.exports = mongodb.mongoose.model("Process_DenounceContractorProcess", Process_DenounceContractorProcessSchema);
