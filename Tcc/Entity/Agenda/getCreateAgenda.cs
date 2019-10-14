using System;
using System.Collections.Generic;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class getCreateAgenda : TaskActivity
    {
        public getCreateAgenda()
        {
            aAgendaRepository = new Lazy<AgendaRepository>(() => new AgendaRepository());
        }

        private Lazy<AgendaRepository> aAgendaRepository;
        private DateTime aData;
        public List<Agenda> acoAgendas = new List<Agenda>();
        protected override bool PreCondicional()
        {
            List<Agenda> agendas = new List<Agenda>();//aAgendaRepository.Value.getData(aData);

            if (agendas != null && agendas.Count > 0)
                acoAgendas = agendas;

            return withoutError();
        }

        protected override bool Semantic()
        {
            if (acoAgendas.Count == 0)
            {
                for (int i = 0; i < 18; i++)
                {

                }
            }

            return withoutError();
        }

        public bool incluir(DateTime prData)
        {
            aData = prData;

            execute();

            return withoutError();
        }
    }
}