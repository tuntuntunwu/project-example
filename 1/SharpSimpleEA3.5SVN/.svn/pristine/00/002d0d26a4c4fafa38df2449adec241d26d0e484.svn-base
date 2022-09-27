function record_session(lastTime,period) {
    var mydate = new Date();
    currentTime = mydate.getMinutes();

    if (lastTime < 55 && currentTime > eval(parseInt(lastTime) + parseInt(period))) {
        return "timeout";
    }
    if (lastTime > 54) {
        if (eval(parseInt(currentTime) - parseInt(lastTime)) > parseInt(period)) {
            return "timeout";
        }
        if (currentTime < 5 && (eval(parseInt(currentTime) - parseInt(lastTime) + parseInt(60)) > parseInt(period))) {
            return "timeout";
        }
    }

    return "active";
}