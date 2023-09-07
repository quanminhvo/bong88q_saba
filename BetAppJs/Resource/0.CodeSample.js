 switch (Array.isArray(n) ? (r = n[0],i = n[1], n[2]) : (r = n.type, i = n.matchid), r) {
    case "m":
        t.updateMatch(n);
        break;
    case "dm":
    case "-m":
        t.deleteMatch(i);
        break;
    case "ls":
        t.updateLiveScore(n);
        break;
    case "-ls":
        t.deleteLiveScore(i);
        break;
    case "l":
        t.updateLeague(n);
        break;
    case "-l":
        t.deleteLeague(i);
        break;
    case "reset":
        t.reset();
        break;
    case "done":
        t.done();
        break;
    case "empty":
        t.empty()
    }

 var r = t.type || t;
 "m" == r ? (f.updateMatch(n, t), 0) : 
 "o" == r ? (f.updateProduct(n, t), "init" == i ? i = "updating" : "empty" == i && (i = "updated")) : 
 "dm" == r ? f.delMatch(n, t.matchid) : 
 "do" == r ? f.delProduct(n, t.oddsid) : 
 "reset" == r ? (f.reset(n), i = "init") : 
 "empty" == r ? (f.reset(n), i = "empty") : 
 "done" === r && (i = u && 
 "init" === u.status ? "empty" : "updated")

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    var f = ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"]
      , r = new Date("2000/6/1").getTimezoneOffset() / -60
      , e = function() {
        function n(n, t) {
            "number" != typeof n ? n = n.valueOf() : n < 2e10 && (n *= 1e3);
            "number" != typeof t && (t = r);
            this.t = n;
            this._t = new Date(n + 36e5 * (t - r));
            this.tz = t
        }
        n.prototype.format = function(n) {
            var t = this;
            return n.replace(/yyyy|MMM|MM|M|dd|d|HH|H|hh|h|mm|m|ss|s|SSS|a|A|Z/g, function(n) {
                return t[n]
            })
        }
        ,
        n.format = function(t, i, u) {
            return "string" == typeof i && (u = i,
            i = r),
            new n(t,i).format(u)
        }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
42["p",["r2",[{"type":"m","matchid":30601123,"gamestatus":0}],"2PYW5"]]
42["p",["r2",[{"type":"m","matchid":30601123,"gamestatus":5}],"2PZ7f"]]



h = 1 == t && n.GameStatus ? 
[
["lbl_PRC", "lbl_PRC_full"], 
["lbl_PPen", "lbl_PPen_full"], 
["lbl_VAR", "lbl_VAR_full"], 
["lbl_Pen", "lbl_Penalty"], 
["lbl_Inj", "lbl_Inj_full"]
]
[parseInt(n.GameStatus) - 1] : void 0, l;

lbl_Inj: "Inj."
lbl_Inj_full: "Cầu thủ chấn thương"

bl_Pen: "Pen."
lbl_Penalty: "Phạt đền"

lbl_VAR: "VAR"
lbl_VAR_full: "Hỗ trợ Trọng tài bằng Video"

bl_PPen: "PPen."
lbl_PPen_full: "Phạt đền có thể xảy ra"

lbl_PRC: "PRC"
lbl_PRC_full: "Thẻ đỏ có thể được rút"

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

42["p",["r2",[{"type":"m","matchid":30601123,"csstatus":"1","isht":true,"liveperiod":0}],"2Pbjj"]]
42["p",["r2",[{"type":"m","matchid":30601123,"csstatus":"0","isht":false}],"2Plhf"]]

switch (t.csstatus) {
	case "1":
		r = "odds_htime";
		break;
	case "2":
		r = "odds_penalty";
		break;
	default:
		r = "odds_live"
}


odds_DelayLive: "bị hoãn"
odds_htime: "H.Time"
odds_live: "!LIVE"
odds_penalty: "Phạt đền"

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

className: "timePlaying" + ("1" == n.csstatus && 0 == n.liveperiod ? " primary" : "")
          
t.prototype.isLive = function() {
    var n = this.props.match.attrs;
    return n.l > 0 || n.liveperiod > 0
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    for (var v = i(4), y = [{
        sport: 1,
        groups: ["HDPOU1", "1X2", "CS", "OE", "TG", "HTFT", "HTFTOE", "FGLG", "HDPOUALL:parlay", "OR"]
    }, {
        sport: 2,
        groups: ["HDPOU-Basketball", "HDPOUALL:parlay", "OR"]
    }, {
        sport: 3,
        groups: ["HDPOU2", "HDPOUALL:parlay", "OR"]
    }, {
        sport: 26,
        groups: ["HDPOU2", "HDPOUALL:parlay", "OR"]
    }, {
        sport: 5,
        groups: ["HDPOU4", "HDPOUALL:parlay", "OR"]
    }, {
        sport: 8,
        groups: ["HDPOU5", "HDPOUALL:parlay", "OR"]
    }, {
        sport: 50,
        groups: ["HDPOU6", "HDPOUALL:parlay", "OR"]
    }, {
        sport: 56,
        groups: ["HDPOU-Basketball", "HDPOUALL:parlay", "OR"]
    }, {
        sport: "default",
        groups: ["HDPOU3", "HDPOUALL:parlay", "OR"]
    }], s = [{
        grp: "ALL",
        bettypes: []
    }, {
        grp: "HDPOU1",
        bettypes: [1, 3, 5, 7, 8, 15, 301, 302, 303, 304]
    }, {
        grp: "1X2",
        bettypes: [5, 15]
    }, {
        grp: "CS",
        bettypes: [4, 30, 413, 414, 405]
    }, {
        grp: "OE",
        bettypes: [2, 12]
    }, {
        grp: "TG",
        bettypes: [6, 126]
    }, {
        grp: "HTFT",
        bettypes: [16]
    }, {
        grp: "HTFTOE",
        bettypes: [128]
    }, {
        grp: "FGLG",
        bettypes: [14, 127, 192, 193]
    }, {
        grp: "OR",
        bettypes: [10]
    }, {
        grp: "HDPOU-Basketball",
        bettypes: [1, 2, 3, 7, 8, 12, 20, 21, 609, 610, 611, 613]
    }, {
        grp: "HDPOU2",
        bettypes: [1, 2, 3, 7, 8, 12]
    }, {
        grp: "HDPOU3",
        bettypes: [1, 2, 3, 20]
    }, {
        grp: "HDPOU4",
        bettypes: [1, 2, 3, 20, 153]
    }, {
        grp: "HDPOU5",
        bettypes: [1, 3, 7, 8, 20, 21]
    }, {
        grp: "HDPOU6",
        bettypes: [1, 2, 3, 5, 501]
    }, {
        grp: "HDPOU-ESport",
        bettypes: [1, 3, 20, 9001]
    }, {
        grp: "HDPOUALL",
        bettypes: []
    }, {
        grp: "RACING",
        bettypes: []
    }, {
        grp: "NUMBER",
        bettypes: []
    }]

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                t.on("connect", function() {
                    n.connected = !0;
                    n.status = "connected";
                    n.emit("status.change", !0);
                    n.rooms = {};
                    t.emit("init", {
                        gid: n.gid,
                        token: n.token,
                        id: n.id,
                        rid: n.rid
                    })
                }).on("init", function(i) {
                    var r = {}, u;
                    if (n.pnid !== i.pnid)
                        for (u in n.pnid = i.pnid,
                        n.conditions)
                            n.conditions[u].rev = "";
                    i.t && e.now(i.t);
                    o.each(n.conditions, function(n) {
                        if (!n.pause) {
                            var t = r[n.provider];
                            t || (t = r[n.provider] = {
                                provider: n.provider,
                                conditions: []
                            });
                            t.conditions.push({
                                id: n.cid,
                                rev: n.rev,
                                condition: n.condition
                            })
                        }
                    });
                    o.each(r, function(n) {
                        t.emit("subscribe", n.provider, n.conditions)
                    })
                }).on("t", function(n) {
                    this.o = Date.now() - n;
                    e.now(n)
                }).on("error", function() {
                    n.connected = !1;
                    n.emit("status.change", !1)
                }).on("disconnect", function() {
                    n.connected = !1;
                    n.status = "disconnected";
                    n.emit("status.change", !1)
                });
                t.on("p", function(t) {
                    Array.isArray(t) && (Array.isArray(t[0]) || (t = [t]),
                    t.forEach(function(t) {
                        var i = n.rooms[t[0]];
                        i && i.subscriptions.forEach(function(n) {
                            n.callback(t[1]);
                            t[2] && (n.rev = t[2])
                        })
                    }))
                });
                t.on("r", function(t) {
                    Array.isArray(t) && (Array.isArray(t[0]) || (t = [t]),
                    t.forEach(function(t) {
                        var i = t[0]
                          , r = t[1]
                          , u = t[2]
                          , f = t[3];
                        n.conditions[i] && (n.rooms[r] || (n.rooms[r] = {
                            room: r,
                            subscriptions: []
                        }),
                        n.rooms[r].subscriptions.push(n.conditions[i]),
                        n.conditions[i].room = r,
                        u && n.conditions[i].callback(u),
                        f && (n.conditions[i].rev = f))
                    }))
                });
                t.on("error", function(t) {
                    n.emit("error", t)
                });

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

