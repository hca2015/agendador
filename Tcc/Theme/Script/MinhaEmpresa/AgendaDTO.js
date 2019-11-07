class AgendaDTO {

    constructor() {
        this._agendaDTO = {
            servico: ko.observable({
                servicoid: ko.observable(""),
                empresaid: ko.observable(""),
                descricao: ko.observable(""),
                valorservico: ko.observable(""),
            }),
            empresa: ko.observable({
                empresaid: ko.observable(""),
                cnpj: ko.observable(""),
                nome: ko.observable(""),
                situacao: ko.observable(""),
                fantasia: ko.observable(""),
                logradouro: ko.observable(""),
                numero: ko.observable(""),
                bairro: ko.observable(""),
                cep: ko.observable(""),
                municipio: ko.observable(""),
                uf: ko.observable(""),
                useridowner: ko.observable(""),
            }),
            agenda: ko.observable({
                agendaid: ko.observable(""),
                empresaid: ko.observable(""),
                servicoid: ko.observable(""),
                clienteid: ko.observable(""),
                horaini: ko.observable(""),
                horafim: ko.observable(""),
                dia: ko.observable(""),
            }),
            cliente: ko.observable({
                clientefixoid: ko.observable(""),
                clienteid: ko.observable(""),
                dataultimoservico: ko.observable(""),
                diasemana: ko.observable(""),
                tipofrequencia: ko.observable(""),
                horario: ko.observable(""),
                nomecliente: ko.observable(""),
                datanascimento: ko.observable(""),
                documento: ko.observable(""),
                servicoid: ko.observable(""),
                nomeservico: ko.observable(""),
                empresaid: ko.observable(""),
            })
        }
    }

    get agendaDTO() { return this._agendaDTO; };    
}

/*
class Agenda {
    agendaid: ko.observable(""),
    empresaid: ko.observable(""),
    servicoid: ko.observable(""),
    clienteid: ko.observable(""),
    horaini: ko.observable(""),
    horafim: ko.observable(""),
    dia: ko.observable(""),
}

class Cliente {
    clientefixoid: ko.observable(""),
    clienteid: ko.observable(""),
    dataultimoservico: ko.observable(""),
    diasemana: ko.observable(""),
    tipofrequencia: ko.observable(""),
    horario: ko.observable(""),
    nomecliente: ko.observable(""),
    datanascimento: ko.observable(""),
    documento: ko.observable(""),
    servicoid: ko.observable(""),
    nomeservico: ko.observable(""),
    empresaid: ko.observable(""),
}

class Empresa {
    empresaid: ko.observable(""),
    cnpj: ko.observable(""),
    nome: ko.observable(""),
    situacao: ko.observable(""),
    fantasia: ko.observable(""),
    logradouro: ko.observable(""),
    numero: ko.observable(""),
    bairro: ko.observable(""),
    cep: ko.observable(""),
    municipio: ko.observable(""),
    uf: ko.observable(""),
    useridowner: ko.observable(""),
}

class Servico {
    servicoid: ko.observable(""),
    empresaid: ko.observable(""),
    descricao: ko.observable(""),
    valorservico: ko.observable(""),
}*/