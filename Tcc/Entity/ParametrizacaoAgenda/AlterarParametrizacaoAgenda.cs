using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class AlterarParametrizacaoAgenda : TaskActivity
    {
        public AlterarParametrizacaoAgenda()
        {
           
        }

        public ParametrizacaoAgenda aParametrizacaoAgenda;
        private ParametrizacaoAgendaRepository aParametrizacaoAgendaRepository;

        protected override bool PreCondicional()
        {
            if (aParametrizacaoAgenda == null)
                addErro("erro");

            if (aParametrizacaoAgenda.HORAFIM < aParametrizacaoAgenda.HORAINI)
                addErro("Hora fim não pode ser menor que hora início");

            return withoutError();
        }

        protected override bool Semantic()
        {
            aParametrizacaoAgendaRepository = new ParametrizacaoAgendaRepository();

            if(!aParametrizacaoAgendaRepository.update(aParametrizacaoAgenda))
                addErro("erro ao alterar as entradas");

            return withoutError();
        }

        public bool alterar(ParametrizacaoAgenda prParametrizacaoAgenda)
        {
            aParametrizacaoAgenda = prParametrizacaoAgenda;

            execute();

            return withoutError();
        }
    }
}