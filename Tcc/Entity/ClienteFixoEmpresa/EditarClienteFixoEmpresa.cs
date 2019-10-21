using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class EditarClienteFixoEmpresa : TaskActivity
    {
        public EditarClienteFixoEmpresa()
        {
           
        }

        ClienteFixoEmpresa aClienteFixoEmpresa;
        ClienteFixoEmpresaRepository aClienteFixoEmpresaRepository = new ClienteFixoEmpresaRepository();
        protected override bool PreCondicional()
        {

            if (aClienteFixoEmpresa == null)
                addErro("Houve um erro com as informações digitadas");

            return withoutError();
        }

        protected override bool Semantic()
        {
            aClienteFixoEmpresaRepository.update(aClienteFixoEmpresa);

            return withoutError();
        }

        public bool editar(ClienteFixoEmpresa ClienteFixoEmpresa)
        {
            aClienteFixoEmpresa = ClienteFixoEmpresa;

            execute();

            return withoutError();
        }
    }
}