function MinhaEmpresa() {
    var obj = this;

    this.HORARIOAGENDA = ko.observableArray([]);

    //ParametrizacaoEmpresa
    this.aParametrizacaoEmpresa = new ParametrizacaoEmpresa();

    this.data = ko.observable("");
    this.dataInput = ko.observable("");
        
    this.getDados = function () {
        $.ajax({
            type: "POST",
            url: "/MinhaEmpresa/getCreateDados?prData="+obj.data(),
            //data: { cnpj: obj.CNPJ() },
            success: obj.retorno,
            error: handleNotifications,
            dataType: "text"
        });
    }
        
    this.retorno = function (dado) {      
        updateDTO(obj, ko.mapping.fromJSON(dado));
    }

   
    this.alterarData = function () {

        var lData = obj.dataInput().split('-');

        obj.data(lData[2]+"/"+lData[1]+"/"+lData[0]);
        obj.dataInput("");

        obj.getDados();
    }

    this.salvarParametrizacao = function () {
        $.ajax({
            type: "POST",
            url: "/MinhaEmpresa/incluirAlterarParametrizacao",
            data: getJsonObject(obj.aParametrizacaoEmpresa),
            success: function (dado) {
                tratarRetorno(dado);
                $("#closeModal").click();
                //getorCreateAgendas();
            },
            error: handleNotifications
            //dataType: "text"
        });
    }

    this.verificarParametrizacao = function () {
        /*$.ajax({
            type: "POST",
            url: "/MinhaEmpresa/getParametrizacao",
            //data: getJsonObject(obj),
            success: function (dado) { obj.validarParametrizacao(dado); },
            //error: handleNotifications
            //dataType: "text"
        });*/

        $.ajax({
            dataType: "json",
            type: "POST",
            url: "/MinhaEmpresa/getParametrizacao",
            success: function (dado) { tratarRetorno(dado, obj.validarParametrizacao); },
            error: handleNotifications
        });
    }

    this.validarParametrizacao = function (dado) {
        if (dado == null || dado == undefined) {
            $("#mostrarModal").click();
        }
        else {
            updateDTO(obj.aParametrizacaoEmpresa, dado);
        }
    }

    this.inicializar = function (prForm, prData) {
        obj.data(prData);
        //verificar se tem parametrizacao
        obj.verificarParametrizacao();
        //carregar os horários


        ko.applyBindings(obj, document.getElementById(prForm));
    }
}