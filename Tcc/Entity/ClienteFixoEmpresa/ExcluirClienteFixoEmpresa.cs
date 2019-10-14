using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class ExcluirClienteFixoEmpresa : TaskActivity
    {
        public ExcluirClienteFixoEmpresa()
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

        public bool excluir(ClienteFixoEmpresa ClienteFixoEmpresa)
        {           

            execute();

            return withoutError();
        }
    }
}