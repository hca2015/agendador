using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirClienteFixoServico : TaskActivity
    {
        public IncluirClienteFixoServico()
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
            aClienteFixoServicoRepository.add(aClienteFixoServico);

            return withoutError();
        }

        public bool incluir(ClienteFixoServico ClienteFixoServico)
        {
            aClienteFixoServico = ClienteFixoServico;

            execute();

            return withoutError();
        }
    }
}