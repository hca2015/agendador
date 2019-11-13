using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirParametrizacaoAgenda : TaskActivity
    {
        public IncluirParametrizacaoAgenda()
        {
            aEmpresaRepository = new EmpresaRepository();
        }

        public ParametrizacaoAgenda aParametrizacaoAgenda;
        private ParametrizacaoAgendaRepository aParametrizacaoAgendaRepository;
        private EmpresaRepository aEmpresaRepository;
        private Empresa aEmpresa;

        protected override bool PreCondicional()
        {
            if (aParametrizacaoAgenda == null)
                addErro("Erro");
            else
            {
                aEmpresa = aEmpresaRepository.getUser(currentUser.userid);

                if (aEmpresa == null)
                    return withoutError(newError("Não há empresa associada"));
                else
                    aParametrizacaoAgenda.empresaid = aEmpresa.empresaid;

                if (aParametrizacaoAgenda.HORAFIM < aParametrizacaoAgenda.HORAINI)
                    return withoutError(newError("Hora fim não pode ser menor que hora início"));

                if (aParametrizacaoAgenda.HORAINI < 0 || aParametrizacaoAgenda.HORAINI > 23)
                    return withoutError(newError("Hora início não pode ser menor que 0 e maior que 23"));

                if (aParametrizacaoAgenda.HORAFIM < 0 || aParametrizacaoAgenda.HORAFIM > 23)
                    return withoutError(newError("Hora início não pode ser menor que 0 e maior que 23"));
            }            

            return withoutError();
        }

        protected override bool Semantic()
        {
            aParametrizacaoAgendaRepository = new ParametrizacaoAgendaRepository();

            if (!aParametrizacaoAgendaRepository.add(aParametrizacaoAgenda))
                addErro("Erro ao incluir");

            return withoutError();
        }

        public bool incluir(ParametrizacaoAgenda prParametrizacaoAgenda)
        {
            aParametrizacaoAgenda = prParametrizacaoAgenda;

            execute();

            return withoutError();
        }
    }
}