var kdType = 
{    
    Success: 0, 
    Error: 1, 
    Info: 2
}

var IsJsonString = function (str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

var getJsonObject = function (prDado) {
    return ko.mapping.fromJSON(JSON.stringify(ko.mapping.toJS(prDado)));
}

var updateDTO = function (prTo, prFrom) {
    var lpropTo = Object.keys(prTo);
    var lpropFrom = Object.keys(prFrom);

    for (var i = 0; i < lpropTo.length; i++) {
        if (prFrom.hasOwnProperty(lpropTo[i])) {

            var lpropName = lpropTo[i];
            var lvalueFrom = prFrom[lpropName];

            if (typeof lvalueFrom === 'function')
                lvalueFrom = lvalueFrom() === "" || lvalueFrom == null | lvalueFrom === "null" ? "" : lvalueFrom();

            if (typeof prTo[lpropName] === 'function')
                prTo[lpropName](lvalueFrom === "" || lvalueFrom == null | lvalueFrom === "null" ? "" : lvalueFrom);
            else
                prTo[lpropName] = lvalueFrom == "null" || lvalueFrom == null ? "" : lvalueFrom
        }
    }
}

var handleNotifications = function (prMessage) {

    //console.clear();

    var lmsg = IsJsonString(prMessage.responseText) ? JSON.parse(prMessage.responseText) : prMessage.responseText == undefined || prMessage.responseText == null ? prMessage : prMessage.responseText;
    var lErro = false;

    for (var i = 0; i < lmsg.length; i++) {

        if (lmsg[i].messageType === kdType.Success) {
            toastr.success(lmsg[i].message);
        }

        if (lmsg[i].messageType === kdType.Error) {
            toastr.error(lmsg[i].message);
            lErro = true;
            return lErro;
        }

        if (lmsg[i].messageType === kdType.Info) {
            toastr.info(lmsg[i].message);
        }
    }    

    return lErro;
}   

var tratarRetorno = function (prRetorno, f) {

    var lErro = false;

    if (Array.isArray(prRetorno))
    {
        if (prRetorno.length > 0) {
            var notificacao = prRetorno[0].hasOwnProperty('messageType');
            if (notificacao) {
                handleNotifications(prRetorno);
                prRetorno = null;                
            }
        }

        if(typeof(f) === 'function')
            f(prRetorno);
        return true;
        
    }

    if (prRetorno != undefined && prRetorno != null)
    {
        if (prRetorno.hasOwnProperty("Messages") || (f == null || f == undefined)) {
            lErro = handleNotifications(prRetorno);            

            if (!lErro && (f != null || f != undefined)) {
                if (typeof (f) === 'function')
                    f(prRetorno);
            }
        }
        else {
            if (typeof (f) === 'function')
                f(prRetorno);
            return true;
        }
    }
}