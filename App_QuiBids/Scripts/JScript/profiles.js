(function (b)
{
    profiles = {};
    profiles.options = {};
    profiles.defaultOptions = {
        width: 300, height: 800
    };
    profiles.profileStore = new Array();
    profiles.init = function (d, c) {
        if (typeof (d) == "string") {
            this.add(d)
        } else {
            if ((typeof (d) == "object") || (typeof (d) == "array")) {
                for (var e in d) { this.add(d[e], c) }
            }
        } profiles.options = b.extend({}, profiles.defaultOptions, c); return this
    };
    profiles.add = function (d, c) {
        b("*[id*=" + d + "]")
            .each(function () {
                b(this)
                    .css("cursor", "pointer"); b(this).hover(function () {
                        if (!/^\s*$/.test(b(this).text()) && b(this).attr("id").indexOf("qbar") === -1) { clearTimeout(profiles.popupTimout); profiles.showProfile(b(this), c) }
                },
                    function () {
                        clearTimeout(profiles.popupTimout); profiles.popupTimout = setTimeout(function () { profiles.hideProfile() }, 50)
                    });
                b(profiles.getProfilePopup()).hover(function ()
                {
                    clearTimeout(profiles.popupTimout)
                }, function ()
                    {
                        clearTimeout(profiles.popupTimout); profiles.popupTimout = setTimeout(function () { profiles.hideProfile() }, 50)
                    })
            }); return this
    }; profiles.getAmount = function (c) {
        var d = />.([\d]+\.[\d]+)</g.exec(c); if (d && d.length > 1) { return d[1] } return null
    }; profiles.showProfile = function (d, c)
    {
        var h = b(d).html(); try {
            if
            (!profiles.profileStore[h]) { b("#profile-left").html(""); b("#profile-right").html(""); var f = "/ajax/profiles.php?username=" + h; if (typeof auctionDetailedID != "undefined") { f += "&auctionid=" + auctionDetailedID } showConsole("Profile popup url: " + f); b.ajax({ url: f, cache: true, success: function (e) { if (e && e.profile) { profiles.currentUsername = h; profiles.currentAmount = profiles.getAmount(b(d).parent().html()); profiles.profileStore[h] = e.profile; profiles.updateProfile(profiles.profileStore[h], d) } b(profiles.getProfilePopup()).clearQueue(); b(profiles.getProfilePopup()).show() } }) } else { profiles.currentUsername = h; profiles.currentAmount = profiles.getAmount(b(d).parent().html()); profiles.updateProfile(profiles.profileStore[h], d); b(profiles.getProfilePopup()).clearQueue(); b(profiles.getProfilePopup()).show() }
        } catch (g) { }
    }; profiles.reloadProfile = function (d) {
        var c = 50; var e; if (profiles.updateTimeout) { clearTimeout(profiles.updateTimeout) } profiles.updateTimeout = setTimeout(function () { if (b(d).html() != profiles.currentUsername) { var f = b(d).parent(); if (typeof (f.attr("id")) == "undefined") { f = b(f).parent() } b(f).children().each(function () { var h = this; var g = profiles.getAmount(b(h).html()); if (g == profiles.currentAmount) { profiles.positionPopupY(b(h)) } }) } profiles.reloadProfile(d) }, c)
    }; var a = null; profiles.getProfilePopup = function () {
        if (a == null) { html = '<div id="profile-popup-intl" style="display:none">'; html += '<div id="profile-popup-intl-content" class="profile-popup-intl-content">'; html += '<div id="profile-top" class="profile-top"></div>'; html += '<div id="profile-left" class="profile-left"></div>'; html += '<div id="profile-right" class="profile-right"></div>'; html += '<div id="profile-bottom" class="profile-bottom"></div>'; html += "</div>"; html += '<div class="prof-popup-intl-bottom"></div>'; html += "</div>"; b("body").append(html); a = b("#profile-popup-intl") } return a
    }; profiles.updateProfile = function (h, g) {
        if (!h.username) { return } var f = ""; f += "<p>" + h.username; if (h.joined) { f += "<span>" + _t.display("member_since", " Member Since "); f += h.joined + "</span>" } b("#profile-top").html(f); var e = ""; e += '<img src="https://static.quibids.com/site/images/avatards/' + h.avatar + '.png" />'; b("#profile-left").html(e); var c = ""; if (h.biddingOnImage) { c += '<div class="profileinner-right-left"><p><strong>' + _t.display("bidding_on", "Bidding On") + "</strong>"; c += '<img src="' + h.biddingOnImage + '" width="70" height="56"></img></p></div>' } if (h.win_image) { c += '<div class="profileinner-right-right"><p><strong>' + _t.display("latest_win", "Latest Win") + "</strong></p>"; c += h.win_image + "</p></div>" } b("#profile-right").html(c); bottomContent = ""; if (h.badges && h.badges.length > 0) { var i = 0; bottomContent += "<p><strong>" + _t.display("latest_achievements", "Latest Achievements") + "</strong></p>"; bottomContent += "<p>"; for (id in h.badges) { if (i++ >= 4) { continue } var d = h.badges[id]; if (d.image && d.title) { bottomContent += ' <img src="' + d.image + '" title="' + d.title + '" width="60" />' } } c += "</p>" } b("#profile-bottom").html(bottomContent); profiles.positionPopup(g); b(profiles.getProfilePopup()).show(); profiles.reloadProfile(g)
    }; profiles.positionPopupY = function (d) {
        var c = b(d).offset().top - b(profiles.getProfilePopup()).height(); b(profiles.getProfilePopup()).css({ top: c })
    }; profiles.positionPopup = function (d) {
        var c = b(d).offset().top - b(profiles.getProfilePopup()).height(); var e = (b("body").width() - b("#container").width()) / 2; var f = b(d).offset().left - 20; b(profiles.getProfilePopup()).css({ top: c, left: f })
    }; profiles.hideProfile = function () {
        if (profiles.updateTimeout) { clearTimeout(profiles.updateTimeout) } profiles.currentUsername = null; profiles.currentAmount = null; b(profiles.getProfilePopup()).clearQueue(); b(profiles.getProfilePopup()).hide()
    }; profiles.initError = function () {
        if (b("div.error").length > 0) { b("section#content").css("marginTop", "50px") }
    }
})(jQuery);