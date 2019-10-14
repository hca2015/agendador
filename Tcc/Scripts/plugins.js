var server = function (options) {

    server.defaults = {
        type: "POST",
        url: "",
        datatype: "json",
        onSuccess: null,
        onError: handleNotifications,
        beforeSend: null,
        splashscreen: true,
        jsonObject: null,
    }

    var settings = $.extend({}, server.defaults, options);

    this.request = function () {

        if (settings.beforeSend)
            settings.beforeSend();

        if (settings.splashscreen == true) {
            document.getElementById('_loader').hidden = false;
            $(document.body).append('<div id="overlay"></div>');
        }

        $.ajax({
            type: settings.type,
            url: settings.url,
            data: settings.jsonObject ? settings.jsonObject : null,
            dataType: "json",
            traditional: true,
            success: function (dado) {

                let hasError = false;

                if (settings.splashscreen == true) {
                    document.getElementById('_loader').hidden = true;
                    document.getElementById("overlay").remove();
                }

                if (Array.isArray(dado) && dado.length > 0 && dado[0].hasOwnProperty('messageType')) {
                    for (var i = 0; i < dado.length; i++) {
                                                
                        if (dado[i].messageType === kdType.Error) {
                            toastr.error(dado[i].message);                            
                            hasError = true;
                        }
                    } 

                    if (hasError)
                        return;
                }

                if (settings.onSuccess)
                    settings.onSuccess(dado);

            },
            error: function (dado) {

                if (settings.splashscreen == true) {
                    document.getElementById('_loader').hidden = true;
                    document.getElementById("overlay").remove();
                }

                handleNotifications(dado);
            },
        });
    }

    return this;
}
