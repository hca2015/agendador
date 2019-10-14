using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirClienteFixo : TaskActivity
    {
        public IncluirClienteFixo()
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

        public bool incluir(ClienteFixo clientefixo)
        {           

            execute();

            return withoutError();
        }
    }
}