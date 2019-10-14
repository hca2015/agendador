function MinhaEmpresa() {
    var obj = this;

    this.HORARIOAGENDA = ko.observableArray([]);

    //ParametrizacaoEmpresa
    this.aParametrizacaoEmpresa = new ParametrizacaoEmpresa();
    
    this.data = ko.observable("");
    this.dataInput = ko.observable("");
        
    this.getDados = function () {
        server({            
            url: "/MinhaEmpresa/getCreateDados?prData="+obj.data(),            
            onSuccess: obj.retorno,
        }).request();
    }
        
    this.retorno = function (dado) {      
        updateDTO(obj, ko.mapping.fromJSON(dado));
    }
   
    this.alterarData = function () {

        debugger;

        if (obj.dataInput() === "" || obj.dataInput() === null || obj.dataInput() == undefined) {
            toastr.error("Coloque uma data valida!");
            return;
        }

        var lData = obj.dataInput().split('-');

        obj.data(lData[2]+"/"+lData[1]+"/"+lData[0]);
        obj.dataInput("");

        obj.getDados();
    }

    this.salvarParametrizacao = function () {
        server({
            type: "POST",
            url: "/MinhaEmpresa/incluirAlterarParametrizacao",
            jsonObject: getJsonObject(obj.aParametrizacaoEmpresa),
            onSuccess: function (dado) {
                tratarRetorno(dado, obj.validarParametrizacao);
                $("#closeModal").click();                
            },
        }).request();
    }

    this.verificarParametrizacao = function () {                
        server({
            url: "/MinhaEmpresa/getParametrizacao",
            onSuccess: function (dado) { tratarRetorno(dado, obj.validarParametrizacao); },      
            splashscreen: false
        }).request();           
    }

    this.validarParametrizacao = function (dado) {

        if (dado == null || dado == undefined) {
            $("#mostrarModal").click();
        }
        else {
            updateDTO(obj.aParametrizacaoEmpresa, dado);
        }
    }

    this.inicializar = function (prForm, prData, prParametrizacao) {
        obj.data(prData);

        var dataQuebrada = prData.split('/');

        //obj.dataInput(dataQuebrada[2] + '-' + dataQuebrada[1] + '-' + dataQuebrada[0])


        //verificar se tem parametrizacao
        ///vai vir via ViewBag
        //obj.verificarParametrizacao();
        //entao vai ser chamado assim...
        tratarRetorno(prParametrizacao, obj.validarParametrizacao);
        
        ko.applyBindings(obj, document.getElementById(prForm));
    }
}