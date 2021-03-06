
define( ['app','angular'],function(app,angular){
    app.service('socket',['$filter','$timeout','$rootScope',function($filter,$timeout,$rootScope){
        var socket = io.connect(window.location.origin);
        return{
            on : function(eventName,callback){
                socket.on(eventName,function(){
                    var args = arguments;
                    $rootScope.$apply(function(){
                        callback.apply(socket,args);
                    });
                });
            },
            emit : function(eventName,data,callback){
                socket.emit(eventName,data,function(){
                    var args = arguments;
                    $rootScope.$apply(function(){
                        callback.apply(socket,args);
                    });
                });
            }
        }
    }]);
});