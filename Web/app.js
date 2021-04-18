var express = require('express');
var http = require('http');
//var https = require('https');
var socketio = require('socket.io');

/* log4js start  */
var log4js = require('log4js');
log4js.configure('log4js.json');
var logger = log4js.getLogger("app");
logger.setLevel('TRACE');//开发调式时TRACE
logger.info('-nodejs opening---');
/*log4js end*/



var querystring = require('querystring');
var app = express();
var URL = require('url');
console.log([app.get('env')]);
var config = require('./config.json')[app.get('env')];
var request = require('request');
var domain = require('domain');
var s = require('string');
var SysReqLog = require('./models/replog.js');
//创建文件夹
var fs = require('fs');
var basicService = require('./routes/basicRoute');


var server = require('http').createServer(app);
var io = require('socket.io').listen(server);
var messageContent;
var count = 0;
io.on('connection', function (client) {
    count++;
    io.sockets.emit("Message-from-server", messageContent);
    client.on('disconnect', function () {
        console.log("disconnected")
        count--;
        io.sockets.emit('Counter-User-Online', count);
    });
    client.on('Client-send-Server', function (data) {

        messageContent = data;
        io.sockets.emit("Message-from-server", messageContent);
    });
    io.sockets.emit('Counter-User-Online', count);

    /**socket evaluate risk of Modification system**/
    client.on('evaluate-risk', function (data) {
        io.sockets.emit('result-evaluate', data);
    });
});
var optionssl = {
    key: fs.readFileSync("privatekey.pem"),
    cert: fs.readFileSync("certificate.pem")
};
server.listen(843);
//https.createServer(optionssl, app).listen(443);




//加载静态页面
app.use(express.static('./web'));
app.use(express.static('./test'));
app.use(express.static('D:/QCFiles'));
app.use(express.static('D:/ContractorFile'));
app.use(express.static('D:/ModificationFile'));

//超时时间
app.use(express.responseTime());
//错误
app.use(express.errorHandler());
//session & cookie
app.use(express.cookieParser());
app.use(express.session({ secret: 'SEKR37' }));
//
app.use(function (req, res, next) {
    var url = req.originalUrl;

    if (!req.session['username']) {

        var query = querystring.parse(URL.parse(req.url).query);

        var autosession = query.Token;
        var code = query.code;
        if (code) {

            var autourl = 'http://ateamma01.feg.com.tw/oauth2/getUserInfo?code=' + code;
            request(
                {
                    method: 'GET', uri: autourl
                }
                , function (error, response, body) {
                    if (error) {

                        res.send(response.statusCode, error);
                    } else {
                        if (response.statusCode == 200) {
                            var userinfo = JSON.parse(body);
                            req.session['username'] = userinfo.UserId;
                            if (userinfo.UserId) {
                                if (userinfo.UserId.indexOf("fepv") >= 0) {
                                    req.session['username'] = userinfo.UserId.toLocaleUpperCase();
                                }
                            }
                            if (!req.session['username']) {
                                return res.send(response.statusCode, "no seesion Wrong userName and password");
                            }
                            req.session['email'] = userinfo.Email || "";
                            req.session['nickname'] = userinfo.UserName;
                            req.session['isAuthorize'] = true;
                            next();
                        } else {
                            return res.send(response.statusCode, "200 Wrong userName and password");

                        }
                    }
                });
        } else {
            console.log("no code");
        }
    }
    if (url != "/authorize/login?" && url != "/authorize/loginCode") {

        if (!req.session['username']) {
            if (req.headers['authorization']) {
                req.session['username'] = req.headers['authorization'];
            } else {
                console.log("no login 4011");
                return res.send(401, 'Wrong userName and password login');
            }
        }

    }
    next();


    //访问的日志记录下来

});

/*app.use(filterAuth);*/
app.post('/authorize/logout', function (req, res) {
    console.log("logout");
    res.clearCookie('username');
    req.session.destroy(function (e) { res.status(200).send('ok'); });
})
app.get('/authorize/isLogin', function (req, res) {
    var username, nickname, email;
    if (req.session != null && req.session['isAuthorize'] != null && req.session['isAuthorize'] != false) {
        username = req.session['username'];
        nickname = req.session['nickname'];
        email = req.session['email'];
        if (!username) {
            res.send(401, 'Wrong user or password');
            return;
        } else {
            res.send({ username: req.session['username'], nickname: req.session['nickname'], email: req.session['email'] });
            return;
        }
    }
    else {
        res.send(401, 'Wrong user or password');
        return;
    }


})

//authorize  test used
app.post('/authorize/login', express.bodyParser(), function (req, res) {
    var username = req.body.username;
    var password = req.body.password;
    //var   url = config.bpmrest+'Auth/login' ;
    var url = config.hrrest + 'api/HSSE/ValidateUser?username=' + username + '&password=' + password;
    request(url, function (e, r, b) {
        if (!e && r.statusCode == 200) {

            req.session['username'] = username;
            req.session['password'] = password;
            var userinfo = JSON.parse(b);
            req.session['email'] = userinfo.email;
            req.session['username'] = userinfo.username;
            req.session['nickname'] = userinfo.nickname;
            console.log(req.session['nickname']);
            req.session['isAuthorize'] = true;
            res.send(b);
            return;
        } else {
            res.send(401, 'Wrong user or password');
            return;
        }
    });
});

app.post('/authorize/loginToken', express.bodyParser(), function (req, res) {
    console.log("loginToken");
    var token = req.body.token;
    var url = config.hrrest + 'api/HSSE/ValidateUser?token=' + token;
    console.log(url);
    request(url, function (e, r, b) {
        console.log(e);
        if (!e && r.statusCode == 200) {
            var userinfo = JSON.parse(b);
            req.session['username'] = userinfo.userId;
            req.session['email'] = userinfo.email;
            req.session['nickname'] = userinfo.nickname;
            console.log(req.session['nickname']);
            req.session['isAuthorize'] = true;
            res.send(b);
            return;
        } else {
            res.send(401, 'CAS TOKEN Wrong TOKEN:' + token);
            return;
        }
    });
});

app.post('/authorize/loginCode', express.bodyParser(), function (req, res) {
    console.log("loginCode");
    var code = req.body.code;
    console.log(code);
    var url = 'http://ateamma01.feg.com.tw/oauth2/getUserInfo?code=' + code;
    console.log(url);
    if (!req.session['username']) {
        request(url, function (e, r, b) {
            console.log(b);
            console.log(r.statusCode);
            console.log(JSON.parse(b).UserId);
            if (r.statusCode == 200) {
                var userinfo = JSON.parse(b);
                if (userinfo.UserId) {
                    console.log(userinfo.UserId)
                    req.session['username'] = userinfo.UserId;
                    if (userinfo.UserId) {
                        if (userinfo.UserId.indexOf("fepv") >= 0) {
                            req.session['username'] = userinfo.UserId.toLocaleUpperCase();
                        }
                    }
                    req.session['email'] = userinfo.Email || "";
                    req.session['nickname'] = userinfo.UserName || "";
                    console.log(req.session['nickname']);
                    req.session['isAuthorize'] = true;
                    res.send({ username: userinfo.UserId, nickname: userinfo.UserName, email: req.session['email'] });
                    return;
                } else {
                    res.send(401, 'CAS  Wrong code:' + code);
                    return;
                }

            } else {
                res.send(401, 'CAS  Wrong code info:' + code);
                return;
            }
        });
    } else {
        res.send({ username: req.session['username'], nickname: req.session['username'], email: req.session['email'] || '' });
    }
});


//MARCO TEST MESSAGE SERVICE


app.get('/get/message', express.bodyParser(), function (req, res) {

    var url = config.hrrest + 'api/HSSE/Test';

    request(url, function (e, r, b) {

        if (!e && r.statusCode == 200) {
            var userinfo = JSON.parse(b);
            res.send(200, { message: b });
            return;
        } else {
            res.send(401, 'Wrong user or password');
            return;
        }
    });
});
/////////////////////////////
app.use(express.bodyParser());
//路由
app.use(app.router);
var GateService = require('./appservice/GateGuestService')(app, request, config, express);//
var BPMService = require('./appservice/bpm')(app, request, config, express);//
var UploadService = require('./appservice/uploadservice')(app, request, config, express);//
var memberSignService = require('./appservice/memberSign')(app, request, basicService, config);
//转api
app.all('/bpm/api/*', function (req, res) {
    var x = request(config.bpmurl + req.url.replace('/bpm/api/', ''));
    x.pipe(res).on('error', function (e) {
        logger.error({ title: 'bpm pipe res' + config.bpmurl + req.url.replace('/bpm/api/', ''), message: e });
        throw e;
    });
    req.pipe(x).on('error', function (err) {
        logger.error({ title: 'bpm pipe' + config.bpmurl + req.url.replace('/bpm/api/', ''), message: err });
        throw err;
    });
});

app.all('/ehs/gate/*', function (req, res) {

    var x = request(config.hrrest + req.url.replace('/ehs/gate/', 'api/Gate/'));
    console.log(config.hrrest + req.url.replace('/ehs/gate/', 'api/Gate/'));
    x.pipe(res);
    req.pipe(x);
});

app.all('/LIMS/*', function (req, res) {
    var x = request(config.hrrest + req.url.replace('/LIMS/', 'api/LIMS/'));
    console.log(config.hrrest + req.url.replace('/LIMS/', 'api/LIMS/'));
    x.pipe(res);
    req.pipe(x);
});

app.all('/Waste/*', function (req, res) {
    var x = request(config.ehshost + req.url.replace('/Waste/', 'api/EHS/'));
    console.log(config.ehshost + req.url.replace('/Waste/', 'api/EHS/'));
    x.pipe(res);
    req.pipe(x);
});

app.all('/ehs/modification/*', function (req, res) {
    var x = request(config.mdhost + req.url.replace('/ehs/modification/', 'api/EHS/Modification/'));
    console.log(config.mdhost + req.url.replace('/ehs/modification/', 'api/EHS/Modification/'));
    x.pipe(res);
    req.pipe(x);
});


app.all('/ehs/timekeeper/*', function (req, res) {
    var x = request(config.timekeeperhost + req.url.replace('/ehs/timekeeper/', 'api/AccessControlService/Contractor/Web/'));
    console.log(config.timekeeperhost + req.url.replace('/ehs/timekeeper/', 'api/AccessControlService/Contractor/Web/'));
    x.pipe(res);
    req.pipe(x);
});

app.all('/ehs/contractor/*', function (req, res) {
    var x = request(config.conhost + req.url.replace('/ehs/contractor/', 'api/Contractor/'));
    console.log(config.conhost + req.url.replace('/ehs/contractor/', 'api/Contractor/'));
    x.pipe(res);
    req.pipe(x);
});



