/* File Created: January 15, 2015 */
function showMessage(message) {
    alert(message);
}

var service = null;
var list = null;
var maxHstId = 0;

function listItemClicked(listId) {
    if (service == null)
        service = new AspNetAjaxTest.AjaxService();
    if (list == null)
        list = document.getElementById(listId);
    var listLength = list.options.length;
    for (var i = 0; i < listLength; i++) {
        if (list.options[i].selected) {
            service.getHstByPs(list.options[i].value, listItemClickedSuccess, null, null);
        }
    }
}

function listItemClickedSuccess(result) {
    alert(result);
}

function getHst() {
    if (service == null)
        service = new AspNetAjaxTest.AjaxService();
    service.getHst(maxHstId, 15, getHstSuccess, null, null);
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