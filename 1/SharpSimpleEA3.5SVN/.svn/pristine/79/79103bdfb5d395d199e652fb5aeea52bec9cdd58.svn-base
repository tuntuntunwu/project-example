
/*==============================================================================
//	Copyright (c) 2010-2014 SHARP CORPORATION. All rights reserved.
//
//	Extended Sharp OSA SDK
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER ``AS IS'' AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
*/

var before = null;

function SelectedDiv(selectId) {
    var select = document.getElementById(selectId).style;
    if (null != before) {
        before.display = "none";
    }
    before = select;
    select.display = "block";
}


function CheckIsNumber(value) {
    // Available character is a numerical character
    return (value.match(/[0-9]+/g) == value);
}

function CreateXmlHttpRequest(parameters, url, errPage, timeout, func) {

    // create XML based HTTP request
    var xmlhttp = null;
    var timerID = null;

    xmlhttp = new XMLHttpRequest();

    httpAbort = function() {
        if (0 == xmlhttp.readyState || 4 == xmlhttp.readyState) {
            return;
        }
        xmlhttp.abort();
        xmlhttp = null;
        clearTimeout(timerID);
        document.location = errPage + "?code=AJAX";
    };

    timerID = setTimeout(httpAbort, parseInt(timeout));

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

    xmlhttp.onreadystatechange = function() {
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

function CheckJsonpResponseError(_retParam, getResourcesURL, errPage, timeout, event) {
    if (null != _retParam.error) {
        switch (_retParam.error.code) {
            case "OriginalNotDetected":
            case "ActionNotSupported":
                msgShow = function(res) { alert(res); }
                CreateXmlHttpRequest({ "code": _retParam.error.code }, getResourcesURL, errPage, timeout, msgShow);
                break;
            case "ResourceNotFound":
                var code = _retParam.error.code;
                if ("cancel" == event) {
                    code = "RESOURCENOTFOUND_CANCEL";
                }
                document.location = errPage + "?code=" + code;
                break;
            case "PhysicalError":
                break;
            case "AuthDisabled":
                if (event != "getloginuser") {
                    document.location = errPage + "?code=" + errCode;
                }
                break;
            default:
                document.location = errPage + "?code=" + _retParam.error.code;
                break;
        }
        return false;
    }
    return true;
}

function IsParameters(parameters) {
    var count = 0;
    for (var key in parameters) {
        count++;
        break;
    }
    if (count > 0) {
        return true;
    }
    else {
        return false;
    }
}