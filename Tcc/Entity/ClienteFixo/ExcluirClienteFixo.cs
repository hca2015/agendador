using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class ExcluirClienteFixo : TaskActivity
    {
        public ExcluirClienteFixo()
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

        public bool excluir(ClienteFixo clientefixo)
        {           

            execute();

            return withoutError();
        }
    }
}