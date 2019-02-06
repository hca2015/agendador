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