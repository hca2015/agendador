using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirServicoTaskActivity : TaskActivity
    {
        public IncluirServicoTaskActivity()
        {
           
        }

        public Servico aServico;

        protected override bool PreCondicional()
        {
            Empresa lEMPRESA = new EmpresaRepository().getUser(currentUser.userid);
                        
            if (aServico == null)
                addErro("Nenhum parâmetro informado");

            if (lEMPRESA == null)
                addErro("Sem empresa associada");
            else
                aServico.empresaid = lEMPRESA.empresaid;

            return withoutError();
        }

        protected override bool Semantic()
        {
            ServicoRepository lServicoRepository = new ServicoRepository();

            if (!lServicoRepository.add(aServico))
                addErro("Erro ao realizar a operação");

            return withoutError();
        }

        public bool incluir(Servico prServico)
        {
            aServico = prServico;

            execute();

            return withoutError();
        }
    }
}