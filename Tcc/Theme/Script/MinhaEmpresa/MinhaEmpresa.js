function MinhaEmpresa() {
    var obj = this;

    var acao = {
        alterar: 0,
        limpar: 1
    };

    let aForm = "";

    this.HORARIOAGENDA = ko.observableArray([]);
    this.aAcao = ko.observable(0);
    this.aAgendaSelecionada = ko.observable(new AgendaDTO().agendaDTO);
    this.disableCliente = ko.observable(false);
    //ParametrizacaoEmpresa
    this.aParametrizacaoEmpresa = new ParametrizacaoEmpresa();

    this.data = ko.observable("");
    this.dataInput = ko.observable("");

    this.acoAgenda = ko.observableArray([]);

    this.getDados = function () {
        var lData = obj.data().split('/');
        window.location = '/MinhaEmpresa/MinhaEmpresa?prData=' + lData[2] + '-' + lData[1] + '-' + lData[0];
    }

    this.retorno = function (dado) {
        updateDTO(obj, ko.mapping.fromJSON(dado));
    }

    this.alterarData = function () {

        if (obj.dataInput() === "" || obj.dataInput() === null || obj.dataInput() == undefined) {
            toastr.error("Coloque uma data valida!");
            return;
        }

        var lData = obj.dataInput().split('-');

        obj.data(lData[2] + "/" + lData[1] + "/" + lData[0]);
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
                obj.getDados();
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

    this.selecionarAgenda = function (prAgenda) {

        if (prAgenda.cliente == null) {
            prAgenda.cliente = new AgendaDTO().agendaDTO.cliente;
            prAgenda.servico = new AgendaDTO().agendaDTO.servico;
        }

        obj.aAgendaSelecionada(prAgenda);
        obj.aAcao(0);
        obj.disableCliente(obj.aAgendaSelecionada().cliente().clienteid() > 0);


        var inpSRV = document.getElementById('nomeservico');
        if (inpSRV.value.length > 0)
            inpSRV.setAttribute('disabled', 'true');

        var inpCLI = document.getElementById('documento');
        if (inpCLI.value.length > 0)
            inpCLI.setAttribute('disabled', 'true');

        document.getElementById('nomecliente').disabled = obj.disableCliente();
        document.getElementById('datanascimento').disabled = obj.disableCliente();
        let dataEditar = new Date(prAgenda.cliente().datanascimento());
        obj.aAgendaSelecionada().cliente().datanascimento(`${dataEditar.getFullYear()}-${dataEditar.getMonth() + 1 >= 10 ? dataEditar.getMonth() + 1 : '' + dataEditar.getMonth() + 1}-${dataEditar.getDate()}`);
        document.getElementById('datanascimento').value = `${dataEditar.getFullYear()}-${dataEditar.getMonth() + 1 >= 10 ? dataEditar.getMonth() + 1 : '' + dataEditar.getMonth() + 1}-${dataEditar.getDate()}`;
    }

    this.selecionarAgendaLimpar = function (prAgenda) {

        if (prAgenda.cliente == null || (typeof (prAgenda.cliente().clienteid) == 'function' && prAgenda.cliente().clienteid() < 1)) {
            toastr.error('Não há dados para limpar.');
            document.getElementById('closeModalEditarAgenda').click();
            return;
        }

        obj.aAgendaSelecionada(prAgenda);
        obj.aAcao(1);
    }

    this.clearAgenda = function (prAgenda) {
        obj.aAgendaSelecionada(new AgendaDTO().agendaDTO);
        obj.aAcao(0);
        obj.disableCliente(false);
        document.getElementById('nomecliente').disabled = obj.disableCliente();
        document.getElementById('datanascimento').disabled = obj.disableCliente();

        document.getElementById('nomecliente').removeAttribute('disabled');
        document.getElementById('datanascimento').removeAttribute('disabled');
        document.getElementById('nomeservico').removeAttribute('disabled');
        document.getElementById('documento').removeAttribute('disabled');
    }

    this.salvarAgenda = function () {

        if (!obj.aAgendaSelecionada().servico().servicoid() || obj.aAgendaSelecionada().servico().servicoid() < 1) {
            toastr.error('Selecione um serviço')
            return;
        }

        let lAgenda = {
            agenda: ko.observable(obj.aAgendaSelecionada().agenda),
            empresa: ko.observable(obj.aAgendaSelecionada().empresa),
            cliente: ko.observable(obj.aAgendaSelecionada().cliente),
            servico: ko.observable(obj.aAgendaSelecionada().servico),
        }

        server({
            url: "/MinhaEmpresa/salvarAgenda",
            jsonObject: new JsonObjectRequestBean(lAgenda),
            onSuccess: function (dado) {
                tratarRetorno(dado, (dado) => {

                    obj.getDados();

                    document.getElementById('closeModalEditarAgenda').click();
                });
            },
            splashscreen: true
        }).request();
    }

    this.limparAgenda = function () {
        server({
            url: "/MinhaEmpresa/limparAgenda?pragendaid=" + obj.aAgendaSelecionada().agenda().agendaid(),
            onSuccess: function (dado) {
                tratarRetorno(dado, (dado) => {

                    obj.getDados();

                    document.getElementById('closeModalEditarAgenda').click();
                });
            },
            splashscreen: true
        }).request();
    }

    this.inicializar = function (prForm, prData, prParametrizacao, prAgenda) {
        obj.data(prData);
        aForm = prForm;
        var dataQuebrada = prData.split('/');

        tratarRetorno(prParametrizacao, obj.validarParametrizacao);
        //obj.acoAgenda(prAgenda);

        for (var i = 0; i < prAgenda.length; i++) {
            obj.acoAgenda().push(new AgendaDTO().agendaDTO);
        }

        updateDTO(obj.acoAgenda(), prAgenda);

        ko.applyBindings(obj, document.getElementById(prForm));
    }
}