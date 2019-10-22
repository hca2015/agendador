using System.Linq;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirAgenda : TaskActivity
    {
        public IncluirAgenda()
        {
            aAgendaRepository = new AgendaRepository();
        }

        Agenda aAgenda;
        AgendaRepository aAgendaRepository;

        protected override bool PreCondicional()
        {
            if(aAgenda == null)
                return withoutError(newError("Houve um problema com a requisição!"));

            aAgenda.Validate();

            return withoutError();
        }

        protected override bool Semantic()
        {            
            if (!aAgendaRepository.add(aAgenda))
                addErro("Erro ao inserir as entradas!");

            return withoutError();
        }

        public bool incluir(Agenda prAgenda)
        {
            aAgenda = prAgenda;

            execute();

            return withoutError();
        }
    }
}