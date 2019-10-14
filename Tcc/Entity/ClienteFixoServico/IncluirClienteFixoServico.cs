using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirClienteFixoServico : TaskActivity
    {
        public IncluirClienteFixoServico()
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

        public bool incluir(ClienteFixoServico ClienteFixoServico)
        {           

            execute();

            return withoutError();
        }
    }
}