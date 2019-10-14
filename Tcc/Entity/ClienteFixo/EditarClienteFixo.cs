using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class EditarClienteFixo : TaskActivity
    {
        public EditarClienteFixo()
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

        public bool editar(ClienteFixo clientefixo)
        {           

            execute();

            return withoutError();
        }
    }
}