function Cliente() {
    var obj = this;

    this.inicializar = function (prForm, prData) {
        ko.applyBindings(obj, document.getElementById(prForm));
    }
}