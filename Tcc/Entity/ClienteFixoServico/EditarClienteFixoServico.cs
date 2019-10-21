using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class EditarClienteFixoServico : TaskActivity
    {
        public EditarClienteFixoServico()
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
            aClienteFixoServicoRepository.update(aClienteFixoServico);

            return withoutError();
        }

        public bool editar(ClienteFixoServico ClienteFixoServico)
        {
            aClienteFixoServico = ClienteFixoServico;

            execute();

            return withoutError();
        }
    }
}