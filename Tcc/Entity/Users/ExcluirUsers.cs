using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class ExcluirUsers : TaskActivity
    {
        public ExcluirUsers()
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

        public bool excluir(User Users)
        {           

            execute();

            return withoutError();
        }
    }
}