using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class EditarTA : TaskActivity
    {
        public EditarTA()
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

        public bool editar()
        {           

            execute();

            return withoutError();
        }
    }
}