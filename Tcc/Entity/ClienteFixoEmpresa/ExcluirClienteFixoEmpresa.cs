using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class ExcluirClienteFixoEmpresa : TaskActivity
    {
        public ExcluirClienteFixoEmpresa()
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
            aClienteFixoEmpresaRepository.delete(aClienteFixoEmpresa);

            return withoutError();
        }

        public bool excluir(ClienteFixoEmpresa ClienteFixoEmpresa)
        {
            aClienteFixoEmpresa = ClienteFixoEmpresa;

            execute();

            return withoutError();
        }
    }
}