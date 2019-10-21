using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class ExcluirClienteFixoServico : TaskActivity
    {
        public ExcluirClienteFixoServico()
        {
           
        }

        ClienteFixoServico aClienteFixoServico;
        ClienteFixoServicoRepository aClienteFixoServicoRepository = new ClienteFixoServicoRepository();
        protected override bool PreCondicional()
        {

            if (aClienteFixoServico == null)
                addErro("Houve um erro com as informações digitadas");

            return withoutError();
        }

        protected override bool Semantic()
        {
            aClienteFixoServicoRepository.delete(aClienteFixoServico);

            return withoutError();
        }

        public bool excluir(ClienteFixoServico ClienteFixoServico)
        {
            aClienteFixoServico = ClienteFixoServico;


            execute();

            return withoutError();
        }
    }
}