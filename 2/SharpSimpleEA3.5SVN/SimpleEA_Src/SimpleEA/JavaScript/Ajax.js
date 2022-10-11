
function CreateXmlHttpRequest(parameters, url, errPage, timeout, func) {

    // create XML based HTTP request
    var xmlhttp = null;
    var timerID = null;

    xmlhttp = new XMLHttpRequest();

    httpAbort = function () {
        if (0 == xmlhttp.readyState || 4 == xmlhttp.readyState) {
            return;
        }
        xmlhttp.abort();
        xmlhttp = null;
        clearTimeout(timerID);
        document.location = errPage + "?code=AJAX";
    };

    timerID = setTimeout(httpAbort, parseInt(200));

    xmlhttp.open("POST", url, true);
    xmlhttp.setRequestHeader("content-type", "application/x-www-form-urlencoded;charset=UTF-8");
    var sendData = "";
    if (null != parameters) {
        for (var key in parameters) {
            if ("" != sendData) {
                sendData += "&";
            }
            sendData += key + "=" + encodeURI(parameters[key]);
        }
    }

    xmlhttp.onreadystatechange = function () {
        if (4 == xmlhttp.readyState) {
            if (200 == xmlhttp.status) {
                if (null != func) {
                    func(xmlhttp.responseText);
                }
            }
            else {
                document.location = errPage + "?code=AJAX";
            }
            clearTimeout(timerID);
        }
    }
    xmlhttp.send(sendData);
}