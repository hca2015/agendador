using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirClienteFixoEmpresa : TaskActivity
    {
        public IncluirClienteFixoEmpresa()
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
            aClienteFixoEmpresaRepository.add(aClienteFixoEmpresa);

            return withoutError();
        }

        public bool incluir(ClienteFixoEmpresa ClienteFixoEmpresa)
        {
            aClienteFixoEmpresa = ClienteFixoEmpresa;

            execute();

            return withoutError();
        }
    }
}