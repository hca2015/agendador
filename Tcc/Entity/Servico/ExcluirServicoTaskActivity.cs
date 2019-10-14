using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class ExcluirServicoTaskActivity : TaskActivity
    {
        public ExcluirServicoTaskActivity()
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

            if (!lServicoRepository.delete(aServico))
                addErro("Erro ao realizar a operação");

            return withoutError();
        }

        public bool excluir(Servico prServico)
        {
            aServico = prServico;

            execute();

            return withoutError();
        }
    }
}