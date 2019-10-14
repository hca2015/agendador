using System;
using System.Collections.Generic;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirEmpresa : TaskActivity
    {
        public IncluirEmpresa()
        {
            aEmpresaRepository = new Lazy<EmpresaRepository>(() => new EmpresaRepository());
            aEmpresaUserRepository = new Lazy<UserEmpresaRepository>(() => new UserEmpresaRepository());
        }

        private Lazy<EmpresaRepository> aEmpresaRepository;
        private Lazy<UserEmpresaRepository> aEmpresaUserRepository;
        private Empresa aEmpresa;
        private int aUserId;

        protected override bool PreCondicional()
        {
            aEmpresa.cnpj = Utility.removerCaracteresEspeciais(aEmpresa.cnpj);
            aEmpresa.cep = Utility.removerCaracteresEspeciais(aEmpresa.cep);

            if (aEmpresa.cnpj.Length == 11)
                if (!ValidaCPF.IsCpf(aEmpresa.cnpj))
                {
                    addErro("CPF Inválido");
                    return withoutError();
                }

            if (aEmpresa.cnpj.Length == 14)
                if (!ValidaCNPJ.IsCnpj(aEmpresa.cnpj))
                {
                    addErro("CNPJ Inválido");
                    return withoutError();
                }

            UserEmpresa associacao = aEmpresaUserRepository.Value.getUser(aUserId);

            if (associacao != null)
                addErro("Já associado a uma empresa");

            Empresa lEmpresaDup =aEmpresaRepository.Value.getCNPJ(aEmpresa.cnpj);

            if (lEmpresaDup != null)
                addErro("Já existe uma empresa cadastrada com este documento");

            User lUser = new UsersRepository().getId(aUserId);

            if (lUser != null && lUser.usermasterid.HasValue)
                addErro("Usuário associado não pode criar empresa.");

            return withoutError();
        }

        protected override bool Semantic()
        {
            aEmpresa.useridowner = aUserId;

            if (aEmpresaRepository.Value.add(aEmpresa))
            {
                if (aEmpresaUserRepository.Value.add(new UserEmpresa { empresaid = aEmpresa.empresaid, userid = aUserId }))
                {
                    addMessage("Associação concluída com sucesso!", Message.kdType.Success);

                    List<User> lcoUsers = new UsersRepository().getSlaves(aUserId);

                    foreach (var item in lcoUsers)
                    {
                        if (!aEmpresaUserRepository.Value.add(new UserEmpresa { empresaid = aEmpresa.empresaid, userid = item.userid }))
                            addErro("Houve um erro ao associar um usuário slave");
                    }
                }
                else
                {
                    addErro("Associação com o perfil não concluída!");
                }
            }
            else
            {
                addErro("Criação da empresa não concluída!");
            }

            return withoutError();
        }

        public bool incluir(Empresa prEmpresa, int prUserId)
        {
            aEmpresa = prEmpresa;
            aUserId = prUserId;

            execute();

            return withoutError();
        }
    }
}