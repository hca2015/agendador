using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class EditarUsers : TaskActivity
    {
        public EditarUsers()
        {
           
        }

        User aUser;

        protected override bool PreCondicional()
        {

            if (aUser == null)
                addErro("Entidade nula");

            return withoutError();
        }

        protected override bool Semantic()
        {
            UsersRepository usersRepository = new UsersRepository();
            usersRepository.update(aUser);

            return withoutError();
        }

        public bool editar(User user)
        {
            aUser = user;

            execute();

            return withoutError();
        }
    }
}