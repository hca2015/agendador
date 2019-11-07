using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirUsers : TaskActivity
    {
        public IncluirUsers()
        {
           
        }

        User aUser;

        protected override bool PreCondicional()
        {
            if (aUser == null || string.IsNullOrWhiteSpace(aUser.membershipid) || string.IsNullOrWhiteSpace(aUser.cpf))
                addErro("Usuário inválido para inclusão.");

            return withoutError();
        }

        protected override bool Semantic()
        {
            aUser.telefone = aUser.telefone.Trim().removerCaracteresEspeciais();

            UsersRepository lUsersRepository = new UsersRepository();
            if(!lUsersRepository.add(aUser))
                addErro("Houve um erro ao incluir o usuário");

            //Incluir a associacao com a empresa
            if (aUser.usermasterid.HasValue)
            {
                EmpresaRepository lEmpresaRepository = new EmpresaRepository();
                Empresa lEmpresa = lEmpresaRepository.getUser(aUser.usermasterid.Value);
                UserEmpresaRepository lEmpresaUserRepository = new UserEmpresaRepository();

                if (lEmpresa != null)
                {
                    if (lEmpresaUserRepository.add(new UserEmpresa { empresaid = lEmpresa.empresaid, userid = aUser.userid }))
                    {
                        addMessage("Associação concluída com sucesso!", Message.kdType.Success);
                    }
                    else
                    {
                        addErro("Associação com o perfil não concluída!");
                    }
                }
            }


            return withoutError();
        }

        public bool incluir(User prUser)
        {
            aUser = prUser;

            execute();

            return withoutError();
        }
    }
}