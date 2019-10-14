using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class EditarClienteFixoEmpresa : TaskActivity
    {
        public EditarClienteFixoEmpresa()
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

        public bool editar(ClienteFixoEmpresa ClienteFixoEmpresa)
        {           

            execute();

            return withoutError();
        }
    }
}