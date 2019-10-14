function CadastrarCNPJ() {
    var obj = this;

    this.CNPJ = ko.observable("");
    this.nome = ko.observable("");
    this.situacao = ko.observable("");
    this.fantasia = ko.observable("");
    this.logradouro = ko.observable("");
    this.numero = ko.observable("");
    this.bairro = ko.observable("");
    this.cep = ko.observable("");
    this.municipio = ko.observable("");
    this.uf = ko.observable("");

    this.inicializar = function (prForm) {
        ko.applyBindings(obj, document.getElementById(prForm));
    }

    this.buscarReceita = function () {
        $.ajax({
            type: "POST",
            url: "/CNPJ/cnpjReceita",
            data: { cnpj: obj.CNPJ() },
            success: obj.retorno,
            error: handleNotifications,
            dataType: "text"
        });
    }
        
    this.retorno = function (dado) {      
        updateDTO(obj, ko.mapping.fromJSON(dado));
    }

    this.cadastrarEmpresa = function () {
        $.ajax({
            type: "POST",
            url: "/CNPJ/cadastrarEmpresa",
            data: getJsonObject(obj),
            success: function (dado) { toastr.success("Cadastrado com Sucesso.") },
            error: handleNotifications
            //dataType: "text"
        });
    }
}