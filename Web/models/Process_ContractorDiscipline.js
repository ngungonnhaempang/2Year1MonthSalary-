var mongodb = require('./db');
var  Schema=mongodb.mongoose.Schema;

var Process_ContractorDisciplineSchema=new Schema({

    ProcessInstanceId:String,
    activityId:String,
    initiator:String,
    ID:String

},{ collection: 'Process_ContractorDiscipline' });



module.exports = mongodb.mongoose.model("Process_ContractorDiscipline", Process_ContractorDisciplineSchema);
