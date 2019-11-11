function MinhaConta() {
    var obj = this;

    this.isMaster = ko.observable();
    this.UsersAssociados = ko.observableArray([]);
    this.Empresa = ko.observable();

    this.userObject = {
        OldPassword: ko.observable("tantofaz"),
        userid: ko.observable(0),
        membershipid: ko.observable(""),
        NewPassword: ko.observable(""),
        ConfirmPassword: ko.observable("")
    };

    this.newUserObject = function () {

        obj.userObject.OldPassword("tantofaz");
        obj.userObject.membershipid("");
        obj.userObject.userid(0);
        obj.userObject.NewPassword("");
        obj.userObject.ConfirmPassword("");
    }

    this.atribuirUserid = function (user) {
        obj.userObject.membershipid(user.membershipid);
        obj.userObject.userid(user.userid);
    }

    this.trocarSenha = function () {

        server({
            url: "/Manage/ChangePasswordSlave",
            jsonObject: getJsonObject(obj.userObject),
            beforeSend: function () {
                document.getElementById('closeModal').click();
            },
            onSuccess: function (dado) {
                toastr.success('Senha alterada com sucesso');
            },
        }).request();

        obj.newUserObject();
    }

    this.retorno = function (dado) {
        updateDTO(obj, ko.mapping.fromJSON(dado));
    }

    this.replace = function (user) {
        var entity = ko.utils.arrayFirst(obj.UsersAssociados(), function (item) { return user.membershipid === item.membershipid });
        obj.UsersAssociados.replace(entity, user);
    }

    this.ativar = function (user) {
        obj.alterarEstadoUser(user, "ATIVAR");
    }

    this.inativar = function (user) {
        obj.alterarEstadoUser(user, "INATIVAR");
    }

    this.alterarEstadoUser = function (user, acao) {
        server({
            url: "/Manage/alterarEstadoUser?membershipid=" + user.membershipid + "&acao=" + acao,
            onSuccess: function (dado) { obj.replace(dado); },
        }).request();
    }

    this.desvincular = function () {

        server({
            url: "/Manage/desvincularUser?userid=" + obj.userObject.userid(),
            onSuccess: function (dado) {
                obj.newUserObject();
                obj.UsersAssociados(dado);
                document.getElementById('closeModalDesvincular').click();
            },
        }).request();
    }

    this.alterarEmpresa = function () {

        if (obj.Empresa().hasOwnProperty("empresaid") && obj.Empresa().hasOwnProperty("empresaid") > 0) {
            server({
                url: "/Manage/AlterarEmpresa",
                jsonObject: getJsonObject(obj.Empresa),
                beforeSend: function () {
                    document.getElementById('closeModalApagarEmpresa').click();
                },
                onSuccess: function (dado) {
                    updateDTO(obj.Empresa, dado)
                    toastr.success('Empresa alterada com sucesso');
                },
            }).request();
        }
        else {
            toastr.error('Nenhuma empresa associada.');
        }
    }

    this.apagarEmpresa = function () {
        if (obj.Empresa().hasOwnProperty("empresaid") && obj.Empresa().hasOwnProperty("empresaid") > 0) {
            server({
                url: "/Manage/ApagarEmpresa",
                jsonObject: getJsonObject(obj.Empresa),
                beforeSend: function () {
                    document.getElementById('closeModalApagarEmpresa').click();
                },
                onSuccess: function (dado) {
                    toastr.success('Empresa apagada com sucesso');
                    windows.location = '/Manage/Index';
                },
            }).request();
        }
        else {
            toastr.error('Nenhuma empresa associada.');
        }
    }

    this.inicializar = function (prForm, prMaster, prAssociados, empresa) {

        obj.isMaster(prMaster);
        obj.UsersAssociados(prAssociados);
        obj.Empresa(empresa);

        if (!obj.Empresa())
            obj.Empresa = ko.observable({});

        ko.applyBindings(obj, document.getElementById(prForm));
    }
}