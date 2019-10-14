using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class EditarCliente : TaskActivity
    {
        public EditarCliente()
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

        public bool editar(Cliente cliente)
        {           

            execute();

            return withoutError();
        }
    }
}