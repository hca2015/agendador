using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class CriarAgendaAutomatica : TaskActivity
    {
        public CriarAgendaAutomatica(GenClass prContextoExecucao) 
            : base(prContextoExecucao)
        {
            acoAgendaDTO = new List<AgendaDTO>();
            aParametrizacaoAgendaRepository = new ParametrizacaoAgendaRepository();
            aIncluirAgenda = new IncluirAgenda();
            aClienteFixoRepository = new ClienteFixoRepository();
        }

        public List<AgendaDTO> acoAgendaDTO;
        private Empresa aEmpresa;
        private ParametrizacaoAgenda aParametrizacaoAgenda;
        private ParametrizacaoAgendaRepository aParametrizacaoAgendaRepository;
        private User aUser;
        private IncluirAgenda aIncluirAgenda;
        private DateTime aData;
        private ClienteFixoRepository aClienteFixoRepository;
        protected override bool PreCondicional()
        {
            aEmpresa = getEmpresa();

            if (aEmpresa == null)
                return withoutError(newError("Não existe empresa associada."));

            aUser = getUser();

            if (aUser == null)
                return withoutError(newError("Não foi possível capturar o usuário conectado, tente novamente."));

            aParametrizacaoAgenda = aParametrizacaoAgendaRepository.getUser(aUser.userid);

            if(aParametrizacaoAgenda == null)
                return withoutError(newError("Não foi encontrada parametrização de horário, definir uma nova."));

            return withoutError();
        }

        protected override bool Semantic()
        {
            Agenda lAgenda;
            ClienteFixoDTO cfixo;

            for (int i = aParametrizacaoAgenda.HORAINI; i <= aParametrizacaoAgenda.HORAFIM; i++)
            {
                cfixo = aClienteFixoRepository.getDia(aEmpresa.empresaid, aData, i);

                lAgenda = new Agenda()
                {
                    empresaid = aEmpresa.empresaid,
                    dia = aData,
                    horaini = i,
                    horafim = i + 1,
                    clienteid = cfixo == null ? null : cfixo.clienteid,
                    servicoid = cfixo == null ? null : (int?)cfixo.servicoid,
                };

                if(!aIncluirAgenda.incluir(lAgenda))
                {
                    addErro("Houve um erro ao criar as agendas");
                    break;
                }
            }   

            return withoutError();
        }

        public bool criar(DateTime prData)
        {
            aData = prData;

            execute();

            return withoutError();
        }
    }
}