using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirCliente : TaskActivity
    {
        public IncluirCliente()
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

        public bool incluir(Cliente cliente)
        {           

            execute();

            return withoutError();
        }
    }
}