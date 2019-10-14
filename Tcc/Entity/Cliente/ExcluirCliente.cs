using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class ExcluirCliente : TaskActivity
    {
        public ExcluirCliente()
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

        public bool excluir(Cliente cliente)
        {           

            execute();

            return withoutError();
        }
    }
}