using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirUserEmpresa : TaskActivity
    {
        public IncluirUserEmpresa()
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

        public bool incluir(UserEmpresa UserEmpresa)
        {           

            execute();

            return withoutError();
        }
    }
}