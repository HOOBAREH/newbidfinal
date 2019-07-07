$(".btnAuction").click(function () {
    var currentUser = $(this).parent().siblings(".Current_User");
    var Startstatus = $(this).parent().siblings(".Startstatus");
    var userID = $('.userId').attr('data-userId');

    if (Startstatus.attr('data-Start') == "True") {
        var currentBids = $('#template_bidsavailable').text();
        if (currentBids == 0) {
            alert("You do not have enough bids left in your account to bid.");
            return false;
        }
        var revPrice = $(this).parent().siblings('.Reserve-Price');
        var currency = revPrice.html().replace('$', '').trim();
        var sumCurrency = parseInt(currency) + 1;
        revPrice.html('$' + sumCurrency);
        var res = sumCurrency;
        $(this).parent().siblings(".statusClick").val("1");
        $.ajax({
            type: "POST",
            url: "/home/LowerBids/",
            data: {
                Current_UserId: userID,
                id: $(this).parent().siblings(".auctionId").val(),
                Reserve_Price: res
            },
            success: function (result) {
                currentUser.text(currentUser.attr('data-currentUser'));
                $('#template_bidsavailable').text(result.currentbids);

            },
            error: function (result) {
                alert('error');
            }
        });
    }
});
function saveTimer(timer, id, startStatus) {
    $.ajax({
        type: "POST",
        url: "/home/UpdateTimer/",
        data: {
            timer: timer,
            id: id,
            Startstatus: startStatus
            //startStatus=startstatus,
            //isClose=isclose
        },
        success: function (result) {

            //alert('success');
        },
        error: function (result) {
            alert('error');
            return false;
        }
    });
}
function updateIsclose(id) {
    $.ajax({
        type: "POST",
        url: "/home/UpdateIsclose/",
        data: {
            id: id

        },
        success: function (result) {

            //alert('success');
        },
        error: function (result) {
            alert('error');
            return false;
        }
    });
}
function startTimer(duration, display) {

    var minutes, seconds, hours;
    var timer = duration;
    var closeTime = $(display).siblings(".closeTime").val();
    var status = $(display).siblings(".statusClick");
    var Startstatus = $(display).siblings(".Startstatus");
    var isClose = $(display).siblings(".isClose");
    var id = $(display).siblings(".auctionId").val();
    if (Startstatus.attr('data-Start')=="True") {
        $(display).parent().find("h2").removeClass('black').addClass('red');

    } 
    var intervalId = setInterval(function () {
        

        if ((Startstatus.attr('data-Start') == "True") && (timer == 0 && status.val() == 0))
        //time reset shode va kasi mojadaddarmozaede sherkat nakard.
        {
            var img = $(display).parent().children(".p").children(".btntest");
            $(img).attr("src", "/Content/img/Sold.png");
            $(img).css({ "pointer-events": "none" });
            isClose.attr('data-Close', 'True');
            updateIsclose(id);
            $(display).offsetParent("div.auction-item-wrapper").remove();
            window.clearInterval(intervalId);

        }
        if (status.val() == "1") {
            timer = closeTime;
            status.val("0");
        }
        hours = parseInt((timer / 3600) % 24, 10);
        minutes = parseInt((timer / 60) % 60, 10);
        seconds = parseInt(timer % 60, 10);
        hours = hours < 10 ? "0" + hours : hours;
        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = hours + ":" + minutes + ":" + seconds;

        if (--timer == -1) {
            timer = closeTime;
            Startstatus.attr('data-Start', 'True');
            $(display).parent().find("h2").removeClass('black').addClass('red');
            //cc.removeClass('black').addClass('red');
                
            //update startStatus aya inja bashe ya dar saveTimer;
        }
        saveTimer(hours + ":" + minutes + ":" + seconds, id, Startstatus.attr('data-Start'));
    }, 1000);
}
window.onload = function () {
    var everyChild = document.getElementById("spots").querySelectorAll("#time");

    for (var i = 0; i < everyChild.length; i++) {
        const time = everyChild[i].innerText;
        var a = time.split(':'); // split it at the colons
        // minutes are worth 60 seconds. Hours are worth 60 minutes.
        var seconds = (+a[0]) * 60 * 60 + (+a[1]) * 60 + (+a[2]);

        //var fiveMinutes = 60 * 5,
        var display = everyChild[i];
        var isClose = $(display).siblings(".isClose");
        if (isClose.attr('data-Close') == "True") {
            $(display).parent().find("h2").removeClass('black').addClass('red');
            continue;
        }
        startTimer(seconds, display);
    }
};