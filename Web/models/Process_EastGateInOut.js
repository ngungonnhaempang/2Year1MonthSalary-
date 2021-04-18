var mongodb = require('./db');
var  Schema=mongodb.mongoose.Schema;

var Process_EastGateInOutSchema=new Schema({

    ProcessInstanceId:String,
    activityId:String,
    initiator:String,
    ID:String

},{ collection: 'Process_EastGateInOut' });



module.exports = mongodb.mongoose.model("Process_EastGateInOut", Process_EastGateInOutSchema);
