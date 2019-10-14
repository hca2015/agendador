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
                    addErro("Não há empresa associada");
                else
                    aParametrizacaoAgenda.empresaid = aEmpresa.empresaid;

                if (aParametrizacaoAgenda.HORAFIM < aParametrizacaoAgenda.HORAINI)
                    addErro("Hora fim não pode ser menor que hora início");
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