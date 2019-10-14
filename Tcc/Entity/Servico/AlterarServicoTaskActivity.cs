using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class AlterarServicoTaskActivity : TaskActivity
    {
        public AlterarServicoTaskActivity()
        {
           
        }

        public Servico aServico;

        protected override bool PreCondicional()
        {
            if (aServico == null)
                addErro("Nenhum parâmetro informado");

            return withoutError();
        }

        protected override bool Semantic()
        {
            ServicoRepository lServicoRepository = new ServicoRepository();

            if (!lServicoRepository.update(aServico))
                addErro("Erro ao realizar a operação");

            return withoutError();
        }

        public bool alterar(Servico prServico)
        {
            aServico = prServico;

            execute();

            return withoutError();
        }
    }
}