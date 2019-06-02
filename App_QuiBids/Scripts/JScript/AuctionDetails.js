function BiddingHistory() {
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
window.onload = function () {

    btn = document.getElementById('btnBid');

    var everyChild = document.getElementsByClassName("time");
    //var s = everyChild.getElementsByClassName(".time");
    //for (var i = 0; i < everyChild.length; i++) {
    const time = everyChild[0].innerText;
    var a = time.split(':'); // split it at the colons
    // minutes are worth 60 seconds. Hours are worth 60 minutes.
    var seconds = (+a[0]) * 60 * 60 + (+a[1]) * 60 + (+a[2]);

    //var fiveMinutes = 60 * 5,
    var display = everyChild;
    var Startstatus = $(".Startstatus");
    var isClose = $(display).siblings('.isClose');
    if (Startstatus.data('start') == 'True') {
        $(display).parent().find(".time").removeClass('black').addClass('red');
    }
    if (isClose.attr('data-Close') == 'True') {
        $(display).parent().find(".time").removeClass('black').addClass('red');
    }
    else {
        startTimer(seconds, display);
    }
    //}
}
$(".btntest").click(function () {
    //var currentUser = btn.parent().siblings(".Current_User");
    //var Startstatus = $(this).parents('.spots');
    //var currentUser = document.getElementsByClassName("Current_User");
    var Startstatus = $(".Startstatus");
    var userID = $('.userId').data('userid');
    if (Startstatus.data('start') == 'True') {
        var currentBids = $('#template_bidsavailable').text();
        if (currentBids == 0) {
            alert("You do not have enough bids left in your account to bid.");
            return false;
        }
        var revPrice = $('.Reserve-Price');
        var currency = revPrice.html().replace('$', '').trim();
        var sumCurrency = parseInt(currency) + 1;
        revPrice.html('$' + sumCurrency);
        var res = sumCurrency;
        var s ='@(Model.Id)';
        $(".statusClick").val("1");
        $.ajax({
            type: "POST",
            url: "/home/LowerBids/",
            data: {
                Current_UserId: userID,
                id: $(".auctionId").val(),
                Reserve_Price: res
            },
            success: function (list) {
                //currentUser.text(currentUser.attr('data-currentUser'));
                //$('#template_bidsavailable').text(result.currentbids);
                ///////////////addtoLog----------------------

                if (list == "fail") {
                    alert('error');
                }
                else {
                    //if (list.length) {
                    //    $("#titreAppend").append("<h3 style='margin-top:55px'>Object Appel d'Offre  : [ <u style='color:#f43030'><b> " + list[0].Objet + "</b></u> ] </h3>");
                    //}
                    $.each(list, function (i) {
                        $("#tab tbody").append("<tr>" +
                            "<td>" + list[i].Ref_Lot + "</td>" +
                            "<td>" + list[i].Titre + "</td>" +
                            "<td>" + list[i].TotalLotTTC + "</td>" +
                            "<td>" + list[i].NombreConcurrent + "</td>" +
                            "<td>" + list[i].NombreArticle + "</td>" +
                            "</tr>");
                    })

                }//Fermeture Else

                ///////////////Log----------------------
            },
            error: function (list) {
                alert('error');
            }
        });
    }
});
function startTimer(duration, display) {

    var minutes, seconds, hours;
    var timer = duration;
    var closeTime = $(display).siblings(".closeTime").val();
    var status = $(display).siblings(".statusClick");
    var Startstatus = $(display).siblings(".Startstatus");
    var isClose = $(display).siblings(".isClose");
    var id = $(display).siblings(".auctionId").val();
    if (Startstatus.attr('data-Start') == 'True') {
        $(display).parent().find(".time").removeClass('black').addClass('red');


    }
    var intervalId = setInterval(function () {

        if ((Startstatus.attr('data-Start') == 'True') && (timer == 0 && status.val() == 0))
        ////time reset shode va kasi mojadaddarmozaede sherkat nakard.
        {

            var img = $(display).parent().children("#138034043").children(".p").children()[0];
            ////var img = $(display).parent().children(".p").children(".btnBid");
            $(img).attr("src", "/Content/img/Sold.png");
            $(img).css({ "pointer-events": "none" });
            isClose.attr('data-Close', 'True');
            updateIsclose(id);
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

        display[0].innerText = hours + ":" + minutes + ":" + seconds;

        if (--timer == -1) {
            timer = closeTime;
            Startstatus.attr('data-Start', 'True');
            $(display).parent().find(".time").removeClass('black').addClass('red');

            //update startStatus aya inja bashe ya dar saveTimer;
        }
        saveTimer(hours + ":" + minutes + ":" + seconds, id, Startstatus.attr('data-Start'));
    }, 1000);
}
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
