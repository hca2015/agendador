using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class ExcluirTA : TaskActivity
    {
        public ExcluirTA()
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

        public bool excluir()
        {           

            execute();

            return withoutError();
        }
    }
}