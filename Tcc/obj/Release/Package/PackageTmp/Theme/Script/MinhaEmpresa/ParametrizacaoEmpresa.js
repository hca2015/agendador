function ParametrizacaoEmpresa() {
    var obj = this;

    this.PARAMETRIZACAOAGENDAID = ko.observable(0);
    this.empresaid = ko.observable(0);
    this.HORAINI = ko.observable(null);
    this.HORAFIM = ko.observable(null);
}