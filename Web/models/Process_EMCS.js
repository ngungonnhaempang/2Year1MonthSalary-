var mongodb = require('./db');
var  Schema=mongodb.mongoose.Schema;

var process_EMCSS = new Schema({
    ProcessInstanceId:String,
    activityName:String,
    VoucherID:String,
    Name:String
},{ collection: 'process_EMCS' });
module.exports = mongodb.mongoose.model("process_EMCS", process_EMCSS);
