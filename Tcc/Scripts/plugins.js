(function ($) {

    $.widget("ui.autocomplete", $.ui.autocomplete, {

        // extend default options
        options: {
            clearButton: false,
            cleanse: [],
            clearButtonHtml: '&times;',
            clearButtonPosition: {
                my: "right center",
                at: "right center"
            }
        },

        _create: function () {

            var self = this;

            // Invoke the parent widget's method.
            self._super();

            if (self.options.clearButton) {
                self._createClearButton();
            }

        },

        _createClearButton: function () {

            var self = this;

            self.clearElement = $("<span>")
                .attr("tabindex", "-1")
                .addClass("ui-autocomplete-clear")
                .html(self.options.clearButtonHtml)
                .appendTo(self.element.parent());

            if (self.options.clearButtonPosition !== false && typeof self.options.clearButtonPosition === 'object') {
                if (typeof self.options.clearButtonPosition.of === 'undefined') {
                    self.options.clearButtonPosition.of = self.element;
                }
                self.clearElement.position(self.options.clearButtonPosition);
            }

            self._on(self.clearElement, {
                click: function () {
                    self.element.val('').focus();
                    self._hideClearButton();
                    if (self.options.cleanse) {
                        for (var i = 0; i < self.options.cleanse.length; i++) {
                            document.getElementById(self.options.cleanse[i]).value = null;
                        }
                    }
                }
            });

            self.element.addClass('ui-autocomplete-input-has-clear');

            self._on(self.element, {
                input: function () {
                    if (self.element.val() !== "") {
                        self._showClearButton();
                    } else {
                        self._hideClearButton();
                    }
                }
            });

            self._on(self.menu.element, {
                menuselect: function () {
                    self._showClearButton();
                }
            });

            // show clearElement if input has some content on initialization
            if (self.element.val() !== "") {
                self._showClearButton();
            } else {
                self._hideClearButton();
            }

        },

        _showClearButton: function () {
            this.clearElement.css({ 'display': 'inline-block' });
        },

        _hideClearButton: function () {
            this.clearElement.css({ 'display': 'none' });
        }

    });

})(window.jQuery);

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