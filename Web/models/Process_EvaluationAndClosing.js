var mongodb = require('./db');
var  Schema=mongodb.mongoose.Schema;

var Process_EvaluationAndClosingSchema=new Schema({

    ProcessInstanceId:String,
    activityId:String,
    initiator:String,
    ID:String

},{ collection: 'Process_EvaluationAndClosing' });



module.exports = mongodb.mongoose.model("Process_EvaluationAndClosing", Process_EvaluationAndClosingSchema);
