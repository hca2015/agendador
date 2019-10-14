function Servico() {
    var obj = this;

    this.servicoid = ko.observable(0);
    this.empresaid = ko.observable(0);
    this.descricao = ko.observable();
    this.valorservico = ko.observable();


    this.inserirServico = function () {
        server({
            url: "/Servico/Inserir",
            jsonObject: getJsonObject(obj),
            //beforeSend: function () {
            //    document.getElementById('closeModalApagarEmpresa').click();
            //},
            onSuccess: function (dado) {
                toastr.success('Servico incluido com sucesso');
                window.location = '/Servico/Servico';
            },
        }).request();
    }

    this.atribuirServicoid = function () {

    }

    this.ApagarServico = function () {

    }
       
    this.inicializar = function (prForm, prAction) {

        if (prAction === "SEARCH") {

        }

        ko.applyBindings(obj, document.getElementById(prForm));
    }
}

