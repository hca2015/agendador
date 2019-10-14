using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirClienteFixoEmpresa : TaskActivity
    {
        public IncluirClienteFixoEmpresa()
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

        public bool incluir(ClienteFixoEmpresa ClienteFixoEmpresa)
        {           

            execute();

            return withoutError();
        }
    }
}