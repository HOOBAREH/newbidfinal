function startTimer(duration, display) {

    var minutes, seconds, hours;
    var timer = duration;
    //var closeTime = $(display).siblings(".closeTime").val();
    //var status = $(display).siblings(".statusClick");
    //var Startstatus = $(display).siblings(".Startstatus");
    //var isClose = $(display).siblings(".isClose");
    //var id = $(display).siblings(".auctionId").val();
    //if (Startstatus.attr('data-Start') == "True") {
    //    $(display).parent().find("h2").removeClass('black').addClass('red');

    //}
    var intervalId = setInterval(function () {

        //if ((Startstatus.attr('data-Start') == "True") && (timer == 0 && status.val() == 0))
        ////time reset shode va kasi mojadaddarmozaede sherkat nakard.
        //{
        //    var img = $(display).parent().children(".p").children(".btntest");
        //    $(img).attr("src", "/Content/img/Sold.png");
        //    $(img).css({ "pointer-events": "none" });
        //    isClose.attr('data-Close', 'True');
        //    updateIsclose(id);
        //    window.clearInterval(intervalId);
        //}
        //if (status.val() == "1") {
        //    timer = closeTime;
        //    status.val("0");
        //}
        hours = parseInt((timer / 3600) % 24, 10);
        minutes = parseInt((timer / 60) % 60, 10);
        seconds = parseInt(timer % 60, 10);
        hours = hours < 10 ? "0" + hours : hours;
        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display[0].innerText = hours + ":" + minutes + ":" + seconds;

        if (--timer == -1) {
            timer = closeTime;
            //Startstatus.attr('data-Start', 'True');
            //$(display).parent().find("h2").removeClass('black').addClass('red');
            //cc.removeClass('black').addClass('red');

            //update startStatus aya inja bashe ya dar saveTimer;
        }
        //saveTimer(hours + ":" + minutes + ":" + seconds, id, Startstatus.attr('data-Start'));
    }, 1000);
}
window.onload = function () {
    var everyChild = document.getElementsByClassName("time");
    //var s = everyChild.getElementsByClassName(".time");
    //for (var i = 0; i < everyChild.length; i++) {
    const time = everyChild[0].innerText;
    var a = time.split(':'); // split it at the colons
    // minutes are worth 60 seconds. Hours are worth 60 minutes.
    var seconds = (+a[0]) * 60 * 60 + (+a[1]) * 60 + (+a[2]);

    //var fiveMinutes = 60 * 5,
    var display = everyChild;
    var isClose = $(display).siblings(".isClose");
    if (isClose.attr('data-Close') == "True") {
        $(display).parent().find("h2").removeClass('black').addClass('red');
    }
    else {
        startTimer(seconds, display);
    }
    //}
};