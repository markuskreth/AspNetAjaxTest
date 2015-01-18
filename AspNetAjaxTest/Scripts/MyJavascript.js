/* File Created: January 15, 2015 */

var service = null;
var list = null;
var maxHstId = 0;
var textInputId = "";
var textOutputId = "";
var valuesMap = {};

function showMessage(message) {
    alert(message);
}

function listItemClicked(listId) {
    if (service == null)
        service = new AspNetAjaxTest.AjaxService();
    if (list == null)
        list = document.getElementById(listId);
    var listLength = list.options.length;
    for (var i = 0; i < listLength; i++) {
        if (list.options[i].selected) {
            if (valuesMap[list.options[i].value] == null) {
                service.getHstByPs(list.options[i].value, listItemClickedSuccess, processAjaxFailure, null);
            } else {
                showNominatimObject(valuesMap[list.options[i].value]);
            }
        }
    }
}

function showNominatimObject(obj) {
    var adress = obj.address;
    var lat = obj.lat;
    var lon = obj.lon;
    var txt = obj.class + "=" + obj.type + " (" + lat + "," + lon + ")\n";
    for (var property in adress) {
        if (adress.hasOwnProperty(property)) {
            txt += property + ": " + adress[property] + "\n";
        }
    }
    txt += "\n";
    alert(txt);
}
function listItemClickedSuccess(result) {
    var output = document.getElementById(textOutputId);
    output.value = result;
}

function getHst() {
    if (service == null)
        service = new AspNetAjaxTest.AjaxService();
    service.getHst(maxHstId, 15, getHstSuccess, processAjaxFailure, null);
}

function getHstSuccess(result) {
    if (list == null)
        alert("List Objekt nicht initialisiert!");
    else {
        var resultLength = result.length;
        var txt = "";
        try {
            for (var i = 0; i < resultLength; i++) {
                var hstPs = result[i].HstPs;
                var hstBez = result[i].HstBez;

                if (hstPs > maxHstId)
                    maxHstId = hstPs;
                var neuerEintrag = new Option;
                neuerEintrag.text = hstBez,
                neuerEintrag.value = hstPs;
                list.options[list.options.length] = neuerEintrag;
            }
        } catch (e) {
            alert(e.name + ": " + e.Message);
        }
    }
}

function QueryNominatim() {
    if (service == null)
        service = new AspNetAjaxTest.AjaxService();
    var inputControl = document.getElementById(textInputId);
    var text = inputControl.value;
    service.queryNominatim(text, processJsonNominatim, processAjaxFailure, null);
}

function processJsonNominatim(response) {
    var output = document.getElementById(textOutputId);
    var arr;

    try {
        arr = JSON.parse(response);
        processJsonArrayNominatim(arr);
    } catch (e) {
        alert(e);
    }
    output.value = response;
}
function processJsonArrayNominatim(arr) {
    var length = arr.length;
    for (var i = 0; i < length; i++) {
        var obj = arr[i];

        var neuerEintrag = new Option;
        neuerEintrag.value = obj.place_id;
        neuerEintrag.text = obj.display_name;
        list.options[list.options.length] = neuerEintrag;
        valuesMap[obj.place_id] = obj;
    }
}

function processAjaxFailure(response) {
    var output = document.getElementById(textOutputId);
    if (response.hasOwnProperty("_errorObject")) {
        var errorObject = response._errorObject;
        output.value = "Status Code: " + response._statusCode;
        output.value += "\nMessage: " + errorObject.Message;
        output.value += "\nStacktrace: " + errorObject.StackTrace;
    } else {
        output.value = response;
    }
}

function ajax(link, onSuccess, onFailure) {
    if (service == null)
        service = new AspNetAjaxTest.AjaxService();
    service.ajax(link, onSuccess, processAjaxFailure, null);
}