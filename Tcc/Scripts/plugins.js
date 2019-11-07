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

function autocomplete(inp, url, fSelect, fClear) {
    /*the autocomplete function takes two arguments,
    the text field element and an array of possible autocompleted values:*/
    var currentFocus;
    let arr = [];

    var clearBtn = document.createElement('span');
    clearBtn.setAttribute('class', 'autocomplete-remove');
    clearBtn.addEventListener('click', function (e) {
        e.preventDefault();
        inp.removeAttribute('disabled');
        fClear();
    });
    inp.parentNode.appendChild(clearBtn);
    

    /*execute a function when someone writes in the text field:*/
    inp.addEventListener("input", function (e) {
        var a, divElement, i, val = this.value;
        /*close any already open lists of autocompleted values*/
        closeAllLists();
        if (!val) { return false; }
        currentFocus = -1;
        /*create a DIV element that will contain the items (values):*/
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        /*append the DIV element as a child of the autocomplete container:*/
        this.parentNode.appendChild(a);

        //tem q fazer a busca aqui...
        fetch(`${url}?term=${inp.value}`).then((body) => body.json())
            .then((data) => {

                arr = [];

                for (var p = 0; p < data.length; p++) {
                    arr.push(data[p]);
                }

                /*for each item in the array...*/
                for (i = 0; i < arr.length; i++) {
                    /*check if the item starts with the same letters as the text field value:*/
                    if (arr[i].label.substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                        /*create a DIV element for each matching element:*/
                        divElement = document.createElement("DIV");
                        /*make the matching letters bold:*/
                        divElement.innerHTML = "<strong>" + arr[i].label.substr(0, val.length) + "</strong>";
                        divElement.innerHTML += arr[i].label.substr(val.length);
                        /*insert a input field that will hold the current array item's value:*/
                        divElement.innerHTML += "<input type='hidden' value='" + arr[i].label + "'>";
                        /*execute a function when someone clicks on the item value (DIV element):*/


                        //tem q fazer a funcao aqui...

                        divElement.addEventListener("click", function (e) {
                            /*insert the value for the autocomplete text field:*/
                            inp.value = this.getElementsByTagName("input")[0].value;
                            /*close the list of autocompleted values,
                            (or any other open lists of autocompleted values:*/
                            closeAllLists();
                                                        
                            var selected = arr.filter((item) => item.label == this.getElementsByTagName("input")[0].value)[0];
                            if(selected != null)
                                fSelect(selected);

                            inp.setAttribute('disabled', 'true');
                        });

                        a.appendChild(divElement);
                    }
                }
            })
    });
    /*execute a function presses a key on the keyboard:*/
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            if (currentFocus > -1) {
                /*and simulate a click on the "active" item:*/
                if (x) x[currentFocus].click();
            }
        }
    });
    function addActive(x) {
        /*a function to classify an item as "active":*/
        if (!x) return false;
        /*start by removing the "active" class on all items:*/
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("autocomplete-active");
    }
    function removeActive(x) {
        /*a function to remove the "active" class from all autocomplete items:*/
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }
    function closeAllLists(elmnt) {
        /*close all autocomplete lists in the document,
        except the one passed as an argument:*/
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
    /*execute a function when someone clicks in the document:*/
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
}