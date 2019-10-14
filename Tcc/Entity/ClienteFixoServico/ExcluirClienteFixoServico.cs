using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class ExcluirClienteFixoServico : TaskActivity
    {
        public ExcluirClienteFixoServico()
        {
           
        }
               
        protected override bool PreCondicional()
        {

            return withoutError();
        }

        protected override bool Semantic()
        {
            

            return withoutError();
        }

        public bool excluir(ClienteFixoServico ClienteFixoServico)
        {           

            execute();

            return withoutError();
        }
    }
}