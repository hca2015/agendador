using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class ExcluirClienteFixo : TaskActivity
    {
        public ExcluirClienteFixo()
        {
           
        }
        ClienteFixo aClienteFixo;
        protected override bool PreCondicional()
        {
            if (aClienteFixo == null)
                addErro("Houve um erro na requisição");

            return withoutError();
        }

        protected override bool Semantic()
        {
            ClienteFixoEmpresa cemp = new ClienteFixoEmpresaRepository().getClienteFixo(aClienteFixo.clientefixoid);

            if(cemp != null)
            {
                new ClienteFixoEmpresaRepository().delete(cemp);
            }

            new ClienteFixoRepository().delete(aClienteFixo);

            return withoutError();
        }

        public bool excluir(ClienteFixo clientefixo)
        {
            aClienteFixo = clientefixo;

            execute();

            return withoutError();
        }
    }
}