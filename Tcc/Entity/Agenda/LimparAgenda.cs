using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class LimparAgenda : TaskActivity
    {
        public LimparAgenda(GenClass prContextoExecucao)
            : base(prContextoExecucao)
        {
            aAgendaRepository = new AgendaRepository();
        }

        private int aAgendaid;
        private AgendaRepository aAgendaRepository;
        public Agenda aAgenda;
        protected override bool PreCondicional()
        {
            aAgenda = aAgendaRepository.getId(aAgendaid);

            if (aAgenda == null)
                addErro("Houve um problema com a requisição");

            return withoutError();
        }

        protected override bool Semantic()
        {
            aAgenda.clienteid = null;
            aAgenda.servicoid = null;

            EditarAgenda lEditarAgenda = new EditarAgenda();

            if (!lEditarAgenda.editar(aAgenda))
                aContextoExecucao.add(lEditarAgenda.Messages);

            return withoutError();
        }

        public bool Limpar(int agendaid)
        {
            aAgendaid = agendaid;

            execute();

            return withoutError();
        }
    }
}