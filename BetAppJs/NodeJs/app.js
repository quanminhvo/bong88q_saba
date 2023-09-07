//var mongo = require('mongodb');
const WebSocket = require('ws');

const ws = new WebSocket('wss://3qvsm5.5566688.com/socket.io/?gid=531751372842580a&token=6e25c370-31a2-48ae-8246-5402ff4230a6&id=36956123&rid=2&EIO=3&transport=websocket&sid=0yrUpqfe6-N529eMAfSW');

ws.on('open', function open() {
    ws.send("2probe");
});

ws.on('message', function incoming(data) {
    console.log(data);
    if (data == "3probe") {
        ws.send("5");
        ws.send('42["subscribe","odds",[{"id":"c2","rev":0,"condition":{"sporttype":1,"marketid":"L","bettype":[1,3,5,7,8,15,301,302,303,304],"sorting":"n"}}]]');
    }
});

ws.on('error', function () {
    console.log('error...');
});

ws.on('close', function () {
    console.log('close...');
});

setInterval(function () {
    ws.send("2");
}, 7000);
