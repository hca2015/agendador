using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class EditarAgenda : TaskActivity
    {
        public EditarAgenda()
        {
            aAgendaRepository = new AgendaRepository();
        }

        Agenda aAgenda;
        AgendaRepository aAgendaRepository;

        protected override bool PreCondicional()
        {
            if (aAgenda == null)
                return withoutError(newError("Houve um problema com a requisição!"));

            aAgenda.Validate();

            return withoutError();
        }

        protected override bool Semantic()
        {
            if (!aAgendaRepository.update(aAgenda))
                addErro("Erro ao atualziar as entradas!");

            return withoutError();
        }

        public bool editar(Agenda prAgenda)
        {
            aAgenda = prAgenda;

            execute();

            return withoutError();
        }
    }
}