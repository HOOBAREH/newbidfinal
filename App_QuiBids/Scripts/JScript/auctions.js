var debuggingLevel = 3;
var thisSessionsBids = {}; var pageID = 0; var loggedIn = false; var accountUsername = ""; var isDev = false; var ignoreOf = false; var ignoreOfTimeout = 0; var reloadTimeout; var reloadPauseTimeout; var isSimulation = 0; var auctionDetailedID = 0; var catalogDetailedID = 0; var detailedClockChange = 0; var detailedClockLast = 0; var avatarUrl = "https://static.quibids.com/site/images/avatards/"; var ssUrl = (location.protocol == "https:" ? "https://" : "http://") + "ss.quibids.com"; var ssEnabled = false; function Class_Auctions() {
    var F = 0; var J = 0; var v = (new Date()).getTime(); var h = 0; var I = false; var o = true; var b = ""; var m = 0; var t = (new Date()).getTime(); var q = (loggedIn) ? 20000 : 60000; var E = 0; var n = true; var u = new Array(); var k = {}; var j = function (P, O) { if (!mySessionID) { mySessionID = figureMyID() } this.id = P; this.state = (!O ? "Active" : O); this.timeleft = -1; this.timeleft2 = -1; this.bidder = ""; this.price = 0; this.version = 0; this.lockType = 0; this.lastLockType = 0; this.avatar = ""; this.lastupdate = 0; this.lastprice = -1; this.laststate = ""; this.lastbidder = ""; this.lasttimeupdate = -1; this.lasttime = 0; this.bidon = -1; this.pp = null; this.pp_last = 0; this.pp_cid = 0; this.bidNowPhrase = $("." + P).attr("bid"); if (!this.bidNowPhrase) { this.bidNowPhrase = _t.display("bidnow", "Bid Now") } }; this.init = function () { try { if (isSimulation) { Auctions.simulationInterval(); Auctions.displayInterval(); $.timer(1000, Auctions.displayInterval) } else { var O = 1000; if (jQuery.browser.mobile) { O = 1000 } else { if ($.browser.msie && ($.browser.version < 9)) { O = 1000 } else { O = 500 } } $.timer(O, Auctions.ajaxInterval) } } catch (P) { showConsole(P, "JS Error 1") } }; this.isFirstLoad = function () { return o }; this.resetBIN = function () { F = 0; J = 0 }; this.register = function (R, T, P) { try { Auctions.clearCacheStyles(); if (!R) { return } if (R.constructor.toString().indexOf("Array") == -1) { if (P) { auctionDetailedID = R; if (T && T == "Completed" && AuctionDetail) { AuctionDetail.loadCompletedDetails(auctionDetailedID) } } var O = R; R = new Array(); R.push(O) } var Q = 0; $.each(R, function (V, Y) { Y = parseInt(Y); if (Y < 1000000) { return } if (k["a_" + Y] == undefined) { Q += 1; k["a_" + Y] = new j(Y, T); Auctions.resetBIN() } tName = "." + Y; var W = (d[tName] != undefined) ? d[tName] : d[tName] = $(tName); var U = (d[tName + ".bid"] != undefined) ? d[tName + ".bid"] : d[tName + ".bid"] = W.find(".bid"); if (U.data("events") && U.data("events").click) { return } U.click(function () { var Z = $(this).parent().parent().attr("id"); if (!Z) { Z = $(this).parent().parent().parent().attr("id") } y(Z); return false }).hover(function (Z) { if ($(this).hasClass("orange")) { $(this).data("old", $(this).html()); if (loggedIn) { } else { if (isNewUser) { $(this).html(_t.display("register", "Register") + "!") } else { if (!loggedIn) { $(this).html(_t.display("login", "Login") + "!") } } } } }, function (Z) { if ($(this).hasClass("orange")) { $(this).html($(this).data("old")) } }); tName = ".watchlist_" + Y; var X = (d[tName] != undefined) ? d[tName] : d[tName] = $(tName); X.click(function () { var Z = $(this).attr("id"); z(Z); return false }).hover(function () { $(this).css("text-decoration", "underline") }, function () { $(this).css("text-decoration", "none") }) }); if (Q > 0) { showConsole(R, "Registering"); Auctions.ajaxInterval() } } catch (S) { showConsole(S, "JS Error 2") } }; this.unregister = function (Q) { try { if (Q === 0) { Q = new Array(); for (id in k) { var R = k[id]; Q.push(R.id) } } if (Q.constructor.toString().indexOf("Array") == -1) { var O = Q; Q = new Array(); Q.push(O) } var P = 0; $.each(Q, function (U, V) { V = parseInt(V); if (auctionDetailedID != V) { if (k["a_" + V] != undefined) { P += 1; var T = {}; for (var U in k) { if (k[U].id != V) { T["a_" + k[U].id] = k[U] } } k = T } } }); if (P > 0) { Auctions.clearCacheStyles(); showConsole(Q, "Unregister", 2) } } catch (S) { showConsole(S, "JS Error 3") } }; this.getDetailed = function (O) { if (typeof (O) == "undefined") { if (auctionDetailedID && k["a_" + auctionDetailedID]) { O = auctionDetailedID } } if (k["a_" + O]) { return k["a_" + O] } return }; this.setDetailedBIN = function (O) { if (auctionDetailedID) { if (!k["a_" + auctionDetailedID]) { k["a_" + auctionDetailedID] = new j(auctionDetailedID, "") } k["a_" + auctionDetailedID].version = O } }; var g = null; var D = function (O) { $.timer(O * 1000, Auctions.ajaxInterval) }; var c = 0; this.ajaxInterval = function () {
        try {
            if (b == "Paused") { c = 1 - c; if (c == 1) { reloadPauseTimeout = setTimeout("location.reload(true);", 15000); showConsole("Site paused reload in 15...", null, 2); return } } if (g) { clearTimeout(g) } if (I == true) { showConsole("Updates paused...", null, 2); return } var S = (new Date()).getTime(); var P = r(S); var O = ssEnabled ? "/ajax/ss.php" : ""; O += "?b=" + F + "&w=ys&m=" + mm; if ((P != false) && ((S - E) > q)) { var Q = "/ajax/l.php?&w=ys&m=" + mm + "&i=" + P; E = (new Date()).getTime(); $.ajax({ url: Q + "&c=?", cache: false, type: "get", dataType: "json", timeout: 2000, success: function (V) { if (!(V && typeof (V) == "object")) { return } for (id in V) { try { if (id == "a") { w(V[id]); continue } } catch (W) { } } }, error: function (W, X, V) { showConsole(X, "AJAX Locked Error") } }) } if (b == "Paused") { O += "&paused=1" } O += "&i=" + P; if (auctionDetailedID) { O += "&lb_id=" + auctionDetailedID } if ($("body").data("page_id")) { O += "&p=" + $("body").data("page_id") } O = ssEnabled ? ssUrl + O : O; h = S; $.ajax({
                url: O + "&c=?", cache: true, type: "get", crossDomain: true, dataType: "jsonp", timeout: 2000, success: function (V) {
                    if (!(V && typeof (V) == "object")) { return } m = 0; N(V, true); if (n) {
                        n = false; Auctions.displayInterval();
                        var W = 500; if (jQuery.browser.mobile) {
                            W = 1000
                        } else { if ($.browser.msie && ($.browser.version < 9)) { W = 1000 } } if (pageID == 2) { $.timer(W, Auctions.displayInterval) } else { $.timer(W, Auctions.displayInterval) }
                    }
                }, error: function (W, X, V) { showConsole(X, "AJAX Error"); m += 1; if (m >= 3) { ssEnabled = false; setTimeout(function () { m = 0; ssEnabled = false }, 3000 + (Math.random() * 2000)) } }
            }); var R = 1800000; if (ignoreOf) { R = ignoreOfTimeout; if (!R) { R = 3600000 } } if (S - v > R) { if ((typeof QBar == "object") && QBar.bidoActivated() == true) { } else { $(document).trigger("INACTIVITY_DETECTED", true); I = true; var U = "<br/><br><div style='float:right;margin-top:10px;'>"; U = U + "<a href='#' class='buttons orange' onclick='location.reload(true);'><font color='white'>" + _t.display("inactive_imback", "I'm back") + "</font></a></div><br/>&nbsp;<br/>"; $("#popupModal").html("<strong>" + _t.display("inactive_question", "You have not had any activity in %MINUTES% minutes, are you still here?", { MINUTES: 30 }) + "</strong>" + U); $(function () { $("#popupModal").dialog({ modal: true, title: _t.display("inactive_title", "QuiBids Inactivity"), width: 300, height: 180, resizable: false }); $(".ui-widget-overlay").live("click", function () { $("#popupModal").dialog("close") }) }) } }
        } catch (T) { showConsole(T, "JS Error 4") }
    }; function r(Q) { var R = new Array(); for (i in k) { var P = k[i]; if ((P.state == "Active" || P.state == "Paused") && (P.timeleft2 < 60 || (P.timeleft2 < 300 && Q - P.lastupdate > 30000) || (P.id == auctionDetailedID && Q - P.lastupdate > 15000) || (P.id == auctionDetailedID && P.timeleft2 < 300) || (P.state == "Paused" && Q - P.lastupdate > 3000))) { R.push(P.id) } } if (R.length < 1) { return false } var O = R.sort(f).join(""); for (x = 10; x <= 35; x++){ O = O.replace(new RegExp(x, "g"), String.fromCharCode(x + 55)) } for (x = 40; x <= 65; x++){ O = O.replace(new RegExp(x, "g"), String.fromCharCode(x + 57)) } return O } this.checkSS = function (P) { var Q = (new Date()).getTime(); if (!ssEnabled) { return } if (!(t < 1 || Q - t > 1000 * 60)) { return } t = Q; var O = ""; $.ajax({ url: O + "&c=?", cache: false, type: "get", crossDomain: true, dataType: "json", timeout: 2000, success: function (R) { if (R.b) { if (Math.abs(P - R.b) > 250) { ssEnabled = false; showConsole("Bin Position Check Fail: ss=" + P + " != u.php=" + R.b) } else { showConsole("Bin Position SUCCESS: ss=" + P + " u.php=" + R.b) } } } }) }; var f = function (P, O) { var Q = parseInt(Math.random() * 10); var R = Q % 2; var S = Q > 5 ? 1 : -1; return (R * S) }; var C = 0; var N = function (Q, O) { try { var P = new Date(); C = P.getTime(); for (id in Q) { try { if (id == "a") { s(Q[id], O); continue } else { if (id == "bids_avail") { if (typeof (QBar) !== "undefined") { QBar.updateBidsAvailable(Q[id]); continue } } else { if (id == "bids_auction") { if (typeof (AuctionDetail) !== "undefined") { AuctionDetail.updateSavings(Q[id]); continue } } else { if (id == "officetotals") { $("#accountTotal").html(K(Q[id].accounts)); $("#auctionTotal").html(K(Q[id].auctions)); $("#bidTotal").html(K(Q[id].bids)) } else { if (id == "bido" && bidoActive) { if (!$("#bido_num_show")) { continue } if (Q[id].avail == "0") { resetBido(); continue } $("#bido_num_show").html(Q[id].avail + " / " + Q[id].num); continue } else { if (id == "bc") { if (typeof (AuctionDetail) !== "undefined") { AuctionDetail.updateBidderCount(Q[id]) } continue } else { if (id == "message") { G(Q[id]); continue } else { if (id == "globalaccept") { p(); continue } else { if (id == "b" && Q[id] > 0) { var S = parseInt(Q[id]); if (S > F) { F = S } continue } else { if (id == "bm" && Q[id] > 0) { var S = parseInt(Q[id]); if (S > J) { J = S; Auctions.checkSS(J) } continue } } } } } } } } } } } catch (R) { showConsole(R, "JS Error 5") } } u.push(P.getTime() - h); if (u.length > 5) { u.shift() } Auctions.updateConnectionSpeed() } catch (R) { showConsole(R, "JS Error 6") } }; var w = function (P) { try { for (id in P) { if (k["a_" + id] == undefined) { showConsole("Error: missing dataset " + id); continue } if (P[id].l) { var O = parseInt(P[id].l); if (k["a_" + id].lockType != O) { k["a_" + id].lockType = O } } } } catch (Q) { showConsole(Q, "JS Error process locked") } }; var A = false; var s = function (S, O) {
        try {
            var R = (new Date()).getTime(); var U = 0; if (O) { U = ((R - h) / 1000); if (U < 0) { U = 0 } else { if (U > 0.6) { U = 0.6 } } } for (id in S) {
                try {
                    var P = null; if (k["a_" + id] == undefined) { showConsole("Error: missing dataset " + id); continue } if (S[id].constructor.toString().indexOf("Number") > 0) { P = S[id] } else {
                        if (S[id].bidon) { k["a_" + id].bidon = S[id].bidon } if (S[id].p) { if (k["a_" + id].bidon > 0) { if (S[id].p >= k["a_" + id].bidon) { k["a_" + id].bidon = -1; k["a_" + id].price = S[id].p } } else { k["a_" + id].price = S[id].p } if (id == auctionDetailedID) { AuctionDetail.updateSavings(); if (H && typeof (QBar) !== "undefined") { setTimeout("QBar.triggerPoller()", 250) } } } if (S[id].v) { if (k["a_" + id].version < S[id].v) { k["a_" + id].version = S[id].v } } if (k["a_" + id] && k["a_" + id].bidon <= k["a_" + id].price && S[id]) { if (S[id].lb) { k["a_" + id].bidder = S[id].lb } if (S[id].av) { k["a_" + id].avatar = S[id].av } } if (S[id].l) { var Q = parseInt(S[id].l); if (k["a_" + id].lockType != Q) { k["a_" + id].lockType = Q } } if (S[id].s) {
                            if (k["a_" + id].bidon != -1 && k["a_" + id].bidon < k["a_" + id].price) { k["a_" + id].bidon = -1 } if (k["a_" + id].state == "Active" && S[id].s == "Completed") {
                                if (pageID == 2 && k["a_" + id].id == auctionDetailedID) { setTimeout("window.location.reload();", 500) } else {
                                    if (accountUsername.length && (pageID != 9 && k["a_" + id].bidder == accountUsername)) {
                                        if (A && (thisSessionsBids[k["a_" + id].id] == 1)) {
                                            setTimeout("location.href='/auction-" + k["a_" + id].id + "-winner';", 1000)
                                        }
                                    }
                                }
                            } if (k["a_" + id].state != "Active" || (k["a_" + id].state == "Active" && k["a_" + id].bidon == -1)) { k["a_" + id].state = S[id].s } if (b != k["a_" + id].state) { b = k["a_" + id].state }
                        } if (S[id].sl != undefined) { P = S[id].sl } if (S[id].bh && S[id].bh.length > 0) { try { AuctionDetail.processBidHistory(S[id].bh, k["a_" + id].price) } catch (T) { showConsole(T, "JS Error") } }
                    } if (P != undefined) { P -= U; k["a_" + id].timeleft = P; k["a_" + id].timeleft2 = P; k["a_" + id].lasttimeupdate = R } if (id == auctionDetailedID && typeof (AuctionDetail) !== "undefined") { AuctionDetail.updateSavings() } k["a_" + id].lastupdate = R
                } catch (T) { showConsole(T, "JS Error 7") }
            }
        } catch (T) { showConsole(T, "JS Error 8") } if (o) { Auctions.displayInterval() } A = true
    }; var d = {}; this.getCacheStyle = function (O) { if (d[O] != undefined) { return d[O] } d[O] = $(O); return d[O] }; this.clearCacheStyles = function () { d = {} }; this.displayInterval = function () { if (I == true) { showConsole("Updates paused...", null, 2); return } var ad = (new Date()).getTime(); var U = false; if (C > 0) { if (ad - C > 1000 * 3) { U = true } } for (i in k) { try { var X = k[i]; tName = "." + X.id; var T = (d[tName] != undefined) ? d[tName] : d[tName] = $(tName); if (typeof (X) === "undefined" || X.id < 1) { continue } if (X.timeleft == -1 && (X.status == "Completed" && X.laststate != "")) { continue } if (X.state == "Completed" && X.laststate == "Completed") { continue } var Z = X.timeleft; var W = (ad - X.lasttimeupdate) / 1000; if (X.lasttimeupdate > 100000 && Z > -1) { X.timeleft2 = Z - W; if (Z > 19.8 && Z < 20.1) { } else { if (Z > 14.8 && Z < 15.1) { } else { if (Z > 9.8 && Z < 10.1) { } else { Z -= W } } } } if (Z == -1 && X.laststate == "" && X.state == "Active") { continue } if (X.state == "Active" && Z < 1) { Z = 1 } var P = X.state; if (b == "Paused" && X.state != "Completed") { P = "Paused" } if (b != "Paused" && X.state == "Paused") { if (!reloadPauseTimeout) { reloadPauseTimeout = setTimeout("location.reload(true);", 1000 * Math.floor(Math.random() * 6)) } X.state = "Active" } if (Z > -1 || P == "Completed" || P == "Paused") { var ac = (d[tName + ".time"] != undefined) ? d[tName + ".time"] : d[tName + ".time"] = T.find(".time"); if (Z > 0 && P == "Active") { var S = ""; Z = Math.round(Z); if (Z < 1) { Z = 1 } if (Z < 10 && U) { ac.toggleClass("loading", true); ac.toggleClass("light-grey", true); ac.toggleClass("red", false) } else { ac.toggleClass("light-grey", false); ac.toggleClass("loading", false); ac.toggleClass("red", (Z <= 10)) } var V = 0; V = parseInt(Z / 3600); S += ((V > 9) ? "" : "0") + V + ":"; V = parseInt((Z / 60) % 60); S += ((V > 9) ? "" : "0") + V + ":"; V = Math.round(Z % 60); S += ((V > 9) ? "" : "0") + V; ac.html(S) } else { if ((P == "Paused" || b == "Paused")) { ac.toggleClass("loading", false); ac.toggleClass("orange", false); ac.toggleClass("grey", true); ac.html("--:--:--") } else { if (P == "Completed") { ac.toggleClass("loading", false); ac.toggleClass("grey", true); ac.html(_t.display("ended", "Ended")) } } } } if (X.laststate != P || X.lastLockType != X.lockType) { tName = "." + X.id + " .bid"; var Q = (d[tName] != undefined) ? d[tName] : d[tName] = $(tName); if (Q.hasClass("red")) { } else { if (P == "Completed") { $(document).trigger("AUCTION_COMPLETED", X); $("." + X.id + " .sold").show(); Q.html(_t.display("sold", "Sold")); Z = 0; Q.toggleClass("orange", false); Q.toggleClass("grey", true); var af = (d[tName + ".price"] != undefined) ? d[tName + ".price"] : d[tName + ".price"] = T.find(".price"); af.toggleClass("grey", true) } else { if (P == "Paused") { Q.html(_t.display("paused", "Paused")); Q.toggleClass("orange", false); Q.toggleClass("light-grey", true) } else { if (P == "Cancelled") { $(document).trigger("AUCTION_COMPLETED", X); Q.html(_t.display("canceled", "Canceled")); Q.toggleClass("orange", false); Q.toggleClass("light-grey", true) } else { if (P == "Active") { if (X.lockType == 1) { $("." + X.id + " .locked").show(); Q.html(_t.display("locked", "Locked")); Q.toggleClass("orange", false); Q.toggleClass("grey", true) } else { if (X.lockType == 2) { $("." + X.id + " .unlocked").show() } Q.html(X.bidNowPhrase); if (P != X.laststate) { Q.toggleClass("light-grey", false) } Q.toggleClass("orange", true) } } } } } } } if (X.lastprice < X.price) { var af = (d[tName + ".price"] != undefined) ? d[tName + ".price"] : d[tName + ".price"] = T.find(".price"); var ab = parseCurrency(X.price); var R = formatPrice(ab); if (af.html() != R) { af.html(R); if (!o && X.price > 0 && X.lastprice != -1) { var ag = "#fadb7d"; var Y = 900; if (X.lasttime < 2) { ag = "#e26969"; Y = 1200 } else { if (X.lasttime < 10) { ag = "#f1a43f"; Y = 1100 } } ag = "#f1a43f"; var O = true; if (jQuery.browser.mobile && pageID != 2) { O = false } else { if ($.browser.msie && ($.browser.version < 9)) { } } if (O) { af.clearQueue().css("background-color", "").effect("highlight", { color: ag }, Y) } } } } if (X.price == 0 && X.laststate != X.state) { var aa = (d[tName + ".winning"] != undefined) ? d[tName + ".winning"] : d[tName + ".winning"] = T.find(".winning"); if (X.state == "Completed" || X.state == "Cancelled") { aa.html(_t.display("no_winner", "No Winner")) } } else { if (X.lastbidder != X.bidder) { var aa = (d[tName + ".winning"] != undefined) ? d[tName + ".winning"] : d[tName + ".winning"] = T.find(".winning"); aa.html(X.bidder); $(document).trigger("AUCTION_" + X.id + "_UPDATED", X) } } if (X.id == auctionDetailedID) { detailedClockChange = (Z - detailedClockLast); if (detailedClockChange < 0) { detailedClockChange = 0 } detailedClockLast = Z } X.lastbidder = X.bidder; X.lastprice = X.price; X.lasttime = Z; X.laststate = P; X.lastLockType = X.lockType } catch (ae) { showConsole(ae, "JS Error 9") } } o = false }; var e = 0; var l = 0; this.userHasBid = function () { return (e > 0) }; var y = function (T) {
        var S = k["a_" + T]; if (S && S.state == "Completed") {
            errorMsg(_t.display("error_auctioncompleted", "Sorry, the auction ended before your bid could be submitted!"));
            return
        } if (!loggedIn) { if (isNewUser) { document.location = "/" + myLanguage + "/landing/?l=1" } else { document.location = "/" + myLanguage + "/account/login.php?e=" + escape(_t.display("please_login", "Not logged in. Please login to continue.")) + "&return=" + escape(document.location) } return } if (isSimulation) { errorMsg(_t.display("error_simulation", "These auctions are just for simulation.")); return } var R = (new Date()).getTime(); if (l == T && e + 250 > R) { return } e = R; l = T; Auctions.triggerActivity(); tName = "." + T; var Q = (d[tName] != undefined) ? d[tName] : d[tName] = $(tName); var O = (d[tName + ".bid"] != undefined) ? d[tName + ".bid"] : d[tName + ".bid"] = Q.find(".bid"); O.clearQueue().css("background-color", "").effect("highlight", { color: "yellow" }, 800); thisSessionsBids[T] = 1; var P = "/ajax/bid.php?id=" + T + "&p=" + pageID; if (auctionDetailedID == T) { P += "&detail=1" } if (S.pp_cid) { P += "&cid=" + S.pp_cid } else { if ($("#" + T).attr("pp") != undefined) { P += "&cid=" + $("#" + T).attr("pp").split(",")[0] } } $.ajax({ url: P, cache: false, dataType: "json", success: function (U) { N(U); Auctions.displayInterval() } })
    }; var K = function (P) { P += ""; x = P.split("."); x1 = x[0]; x2 = x.length > 1 ? "." + x[1] : ""; var O = /(\d+)(\d{3})/; while (O.test(x1)) { x1 = x1.replace(O, "$1,$2") } return x1 + x2 }; this.getPauseState = function () { return I }; this.updateConnectionSpeed = function () { if (Math.floor(Math.random() * 5) != 1) { return } var O = 0; if (u.length > 0) { $.each(u, function (Q, R) { O += R }); O = O / u.length; O.toFixed(0) } try { if (qBarEnabled) { QBar.updateConnectionSpeed(O) } } catch (P) { } }; this.triggerActivity = function () { v = (new Date()).getTime() }; var z = function (T, P) { Auctions.triggerActivity(); var S = false; try { var O = "/ajax/watch.php?action=toggle&auction_id=" + T; if (P) { O = O + "&addonly=1" } var R = k["a_" + T]; if (R && R.pp_cid) { O += "&cid=" + R.pp_cid } else { if (R && $("#" + T).attr("pp") != undefined) { O += "&cid=" + $("#" + T).attr("pp").split(",")[0] } else { if (catalogDetailedID != undefined) { O += "&cid=" + catalogDetailedID } } } $.ajax({ url: O, cache: false, async: false, success: function (W) { var V = false; if (readCookie("watchNotificationDisabled")) { V = true } tName = ".watchlist_" + T; var X = (d[tName] != undefined) ? d[tName] : d[tName] = $(tName); if (!W.action || W.action == "error") { if (W.message) { errorMsg(W.message) } } else { if (W.action == "added") { X.html(_t.display("watchlist_remove", "- Watchlist")); X.addClass("remove"); S = true; if (!V) { var U = _t.display("watchlist_addmessage", "Auction added to watchlist!") + "<br><div><a href='/auctions-watch-list/'>" + _t.display("watchlist_viewwatchlist", "View Watchlist") + "</a></div><div><small><p><input type='checkbox' onchange='Auctions.changeWatchNotification();' id='notificationCheckbox'>&nbsp;" + _t.display("watchlist_hidenotification", "Hide this popup notification?") + "</small></p></div>"; errorMsg(U, _t.display("watchlist", "Auction Watchlist")) } } else { if (W.action == "removed") { X.html(_t.display("watchlist_add", "+ Watchlist")); X.removeClass("remove"); S = true } } } } }) } catch (Q) { showConsole(Q, "JS Error 10") } return S }; this.addWatched = function (O) { return z(O, true) }; this.removeWatched = function (P) { var O = "/ajax/watch.php?action=toggle&removeonly=1&auction_id=" + P; $.ajax({ url: O, cache: false, success: function (R) { if (!R.action || R.action == "error") { if (R.message) { errorMsg(R.message) } } else { if (R.action == "removed") { $("tr#watchList_" + P).remove(); var Q = $("#watchlist >tbody >tr").length; if (Q == 1) { $("#nowatchlist-info").fadeIn() } } } } }); return true }; this.changeWatchNotification = function () { var O = false; if ($("#notificationCheckbox:checked").val() == "on") { O = true } createCookie("watchNotificationDisabled", O, 180) }; var M; var a = function (O) { if (!M) { L() } while (true) { var P = M[Math.floor(Math.random() * M.length)]; if (!O || P != O) { return P } } }; var L = function () { M = new Array("MrKerryon", "johnny1965", "ilywamhala", "kristenlc", "mobethea", "overlordbast", "vanmom11763", "Tcotton1127", "tdkurtz", "ricky6921", "sbruck", "Renegade20", "dogman081", "highball49", "cwright4u", "hpwitoyo", "cluttrull", "ambernicole6", "tjjom", "pfscorpio", "billbridges", "rltipton", "blmtsi", "RSKI50", "borr04", "dmolina22703", "wrx1949", "richjanet", "auntien", "Mayu0218", "rocadoodle", "wildcats1997", "RNbid1", "Juzo4k", "scotlcky13", "SkidMarc", "suedoe", "rodapi", "photopro", "mabboys", "xecor27", "Lottodude", "Makkbiz", "musicmattps", "dochog2", "anani1106", "joeylig", "hfidler", "riteshn3", "aramiscal", "psymons919", "LevIra", "ceh06", "okenid02", "jbpen", "debbietg", "jdub3033", "bobbym1232", "cgudeman", "fgoldberg", "patotong", "Uziekalla", "arlotobin", "rodh7829", "Roarn", "Ginny2010", "vince62", "JGman09", "flh1979", "fkostoff", "blainoman", "Papos5", "dzuels", "eluedt", "SimonV", "mjf1386", "hherrera", "bevbu913", "Tjackson1979", "kceee", "jj007g", "siroreo417", "h2ofile", "Coinshot", "vonRogue", "dudethtgamez", "leesputer", "vinh928", "chasD90", "aeichman101", "COSMO22", "KIMIM891", "ghost33", "roadknight", "paulsnc", "randyjuve", "IREFLYCS", "Karpediem", "blueartk", "kmbar") }; this.registerSimulationSpots = function (R, Q, T) {
        isSimulation = 1; try {
            Auctions.clearCacheStyles(); if (!R) { return } if (R.constructor.toString().indexOf("Array") == -1) { if (isDetailed) { auctionDetailedID = R } var O = R; R = new Array(); R.push(O) } var P = 0; $.each(R, function (V, X) {
                X = parseInt(X); if (X < 1000000) { return } if (k["a_" + X] == undefined) {
                    P += 1; k["a_" + X] = new j(X, "");
                    Auctions.resetBIN()
                } if (Q) { k["a_" + X].price = Q[V]; k["a_" + X].bidder = a() } if (T) { k["a_" + X].timeleft = T[V] } tName = "." + X; var W = (d[tName] != undefined) ? d[tName] : d[tName] = $(tName); var U = (d[tName + ".bid"] != undefined) ? d[tName + ".bid"] : d[tName + ".bid"] = W.find(".bid"); if (U.data("events") && U.data("events").click) { return } U.click(function () { $("html,body").animate({ scrollTop: $("#login-form").offset().top }, "slow"); return false }).hover(function (Y) { if ($(this).hasClass("orange")) { $(this).data("old", $(this).html()); $(this).html(_t.display("register", "Register") + "!") } }, function (Y) { if ($(this).hasClass("orange")) { $(this).html($(this).data("old")) } })
            }); if (P > 0) { showConsole(R, "Registering landing"); Auctions.displayInterval() }
        } catch (S) { showConsole(S, "JS Error 2") }
    };
    this.simulationInterval = function () {
        try { for (i in k) { var O = k[i]; O.timeleft -= 1; if (O.timeleft < 10 && (O.timeleft < 1 || 1 == Math.floor(Math.random() * (O.timeleft + 3)))) { O.timeleft = 15; O.price = O.price + 0.01; O.bidder = a(O.bidder) } } } catch (P) { showConsole(P, "JS Error 11") } setTimeout("Auctions.simulationInterval()", 1000)
    };
    var B = new Array(); var G = function (R) {
        var Q = (R.once == "1" ? true : false);
        if (R.type == "Error") { if (!B["" + R.id + ""]) { errorMsg(R.msg, R.id, R.width) } if (Q) { B["" + R.id + ""] = true } } else { if (R.type == "Info") { if (!B["" + R.id + ""]) { errorMsg(R.msg, R.id, R.width) } if (Q) { B["" + R.id + ""] = true } } else { if (R.type == "Image") { var S = R.url; var P = R.width; var O = R.height; if (!B["" + S + ""]) { popupImage(S, P, O) } if (Q) { B["" + S + ""] = true } } } }
    };
    var p = function () {
        popupShowURL("/ajax/globalinfo.php", "Participate in Global Auctions", 550, false, "", true)
    }; var H = false; this.toggleBidomatic = function (O) { H = O }; this.pm = function (R, O) { try { if (!k["a_" + R]) { return false } var T = k["a_" + R]; if (!T.pp) { var V = $("." + R).attr("pp"); var Q = "/ajax/pp.php?action=getStream&id=" + R; if (V) { Q += "&top=" + V } $.ajax({ url: Q, cache: false, success: function (Y) { T.pp = new Array(); var X = 0; for (var W in Y) { if (!Y[W].title) { continue } T.pp.push(Y[W]); X++ } if (X > 0) { Auctions.pm(R, O) } } }); return false } if (!T.pp) { return false } if (!T.pp.length) { return false } while (true) { if (O > 0) { T.pp_last++ } else { T.pp_last-- } if (T.pp_last > T.pp.length) { T.pp_last = 0 } if (T.pp_last < 0) { T.pp_last = T.pp.length - 1 } if (!T.pp_cid && T.pp.length > 1) { if ($("." + R + " .auction-item-title").html().toLowerCase().indexOf((T.pp[T.pp_last].title + "").toLowerCase()) > -1) { continue } } if (!T.pp[T.pp_last]) { T.pp_last = 0 } break } if (T.pp[T.pp_last]) { var Q = T.pp[T.pp_last].url; var P = T.pp[T.pp_last].img; var U = T.pp[T.pp_last].title; $("." + R).find(".auction-item-title").html('<a href="' + Q + '">' + U + "</a>"); $("." + R).find("#pimage").html('<a href="' + Q + '"><img src="' + P + '"></a>'); $("." + R).find("#shortdesc").html(T.pp[T.pp_last].shortdesc); $("." + R).find("#valueprice").html(T.pp[T.pp_last].value); T.pp_cid = T.pp[T.pp_last].cid } } catch (S) { showConsole(S, "JS Error 10") } return false }
}
String.prototype.ReplaceAll = function (c, d)
{
    var a = this; var b = a.indexOf(c); while (b != -1) { a = a.replace(c, d); b = a.indexOf(c) } return a
}
    ;
Auctions = new Class_Auctions();

$(document).ready
    (
    function ()
{
    Auctions.init()
})
    ;
auctionsEnabled = true;