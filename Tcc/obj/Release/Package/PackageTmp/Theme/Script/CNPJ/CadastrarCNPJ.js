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
        server({            
            url: "/CNPJ/cnpjReceita",
            jsonObject: { cnpj: obj.CNPJ() },
            onSuccess: obj.retorno,
        }).request();
    }
        
    this.retorno = function (dado) {      
        updateDTO(obj, dado);
    }

    this.cadastrarEmpresa = function () {
        server({
            url: "/CNPJ/cadastrarEmpresa",
            jsonObject: getJsonObject(obj),
            onSuccess: function (dado) { window.location = '/Manage/Index'; },
        }).request();
    }
}