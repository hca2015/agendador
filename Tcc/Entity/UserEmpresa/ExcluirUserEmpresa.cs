using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class ExcluirUserEmpresa : TaskActivity
    {
        public ExcluirUserEmpresa()
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

        public bool excluir(UserEmpresa UserEmpresa)
        {           

            execute();

            return withoutError();
        }
    }
}