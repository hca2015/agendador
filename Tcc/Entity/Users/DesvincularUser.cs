using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class DesvincularUser : TaskActivity
    {
        private User aUser;
        protected override bool PreCondicional()
        {
            if (aUser == null)
                addErro("Nenhum usuário encontrado para desvincular");

            return withoutError();
        }

        protected override bool Semantic()
        {
            aUser.ativo = 0;
            aUser.usermasterid = null;

            EditarUsers lEditarUsuario = new EditarUsers();
            if(!lEditarUsuario.editar(aUser))
                addErro("Houve um erro inesperado");

            return withoutError();
        }

        public bool desvincular(User user)
        {
            aUser = user;
            execute();

            return withoutError();
        }
    }
}