using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class EditarUserEmpresa : TaskActivity
    {
        public EditarUserEmpresa()
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

        public bool editar(UserEmpresa UserEmpresa)
        {           

            execute();

            return withoutError();
        }
    }
}