using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class EditarAgendaCompleta : TaskActivity
    {
        public EditarAgendaCompleta(GenClass prContextoExecucao) 
            : base(prContextoExecucao)
        {
        }

        public AgendaDTO aAgendaDTO;
        protected override bool PreCondicional()
        {
            if (aAgendaDTO == null)
                return aContextoExecucao.withoutError(newError("Houve um problema com a requisição"));

            if (aAgendaDTO.cliente == null)
                return aContextoExecucao.withoutError(newError("Necessário colocar um cliente"));

            if (aAgendaDTO.cliente.clienteid < 1 && (string.IsNullOrWhiteSpace(aAgendaDTO.cliente.documento) || !aAgendaDTO.cliente.datanascimento.HasValue))
                return aContextoExecucao.withoutError(newError("Necessário preencher todas as informações do cliente"));

            aAgendaDTO.cliente.documento = aAgendaDTO.cliente.documento.removerCaracteresEspeciais();

            if (!ValidaCPF.IsCpf(aAgendaDTO.cliente.documento))
                return aContextoExecucao.withoutError(newError("CPF inválido"));

            if (aAgendaDTO.servico == null)
                return aContextoExecucao.withoutError(newError("Necessário colocar um serviço"));

            if (aAgendaDTO.servico.servicoid < 1)
                return aContextoExecucao.withoutError(newError("Necessário colocar um serviço"));

            return aContextoExecucao.withoutError();
        }

        protected override bool Semantic()
        {
            Cliente lCliente = null;
            if (aAgendaDTO.cliente.clienteid < 1 || aAgendaDTO.cliente.clienteid == null)
            {
                lCliente = new Cliente()
                {
                    datanascimento = aAgendaDTO.cliente.datanascimento,
                    documento = aAgendaDTO.cliente.documento,
                    nome = aAgendaDTO.cliente.nomecliente
                };

                new IncluirCliente().incluir(lCliente);
            }

            aAgendaDTO.agenda.clienteid = lCliente == null ? aAgendaDTO.cliente.clienteid : lCliente.clienteid;
            aAgendaDTO.agenda.servicoid = aAgendaDTO.servico.servicoid;

            new EditarAgenda().editar(aAgendaDTO.agenda);

            return aContextoExecucao.withoutError();
        }

        public bool editar(AgendaDTO prAgenda)
        {
            aAgendaDTO = prAgenda;

            execute();

            return aContextoExecucao.withoutError();
        }
    }
}