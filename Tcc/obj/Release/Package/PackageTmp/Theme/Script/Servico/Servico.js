function Servico() {
    var obj = this;

    this.HORARIOAGENDA = ko.observableArray([]);
    this.data = ko.observable("");

    this.inicializar = function (prForm, prData) {
        this.data = prData;
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

    this.alterarData = function () {
        $.ajax({
            type: "POST",
            url: "/MinhaEmpresa/alterarData",
            data: getJsonObject(obj),
            success: function (dado) { toastr.success("Cadastrado com Sucesso.") },
            error: handleNotifications
            //dataType: "text"
        });
    }
}