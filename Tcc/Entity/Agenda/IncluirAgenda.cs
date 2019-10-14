using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirAgenda : TaskActivity
    {
        public IncluirAgenda()
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

        public bool incluir()
        {           

            execute();

            return withoutError();
        }
    }
}