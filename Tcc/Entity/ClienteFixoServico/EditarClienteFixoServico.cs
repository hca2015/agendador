using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class EditarClienteFixoServico : TaskActivity
    {
        public EditarClienteFixoServico()
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

        public bool editar(ClienteFixoServico ClienteFixoServico)
        {           

            execute();

            return withoutError();
        }
    }
}