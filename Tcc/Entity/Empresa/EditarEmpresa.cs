using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class EditarEmpresa : TaskActivity
    {
        public EditarEmpresa()
        {
            aEmpresaRepository = new EmpresaRepository();
        }

        public Empresa aEmpresa;
        private EmpresaRepository aEmpresaRepository;

        protected override bool PreCondicional()
        {
            if (aEmpresa == null)
            {
                addErro("Houve um erro na requisição");
                return false;
            }

            Empresa lOldEntity = aEmpresaRepository.getId(aEmpresa.empresaid);

            if (aEmpresa.cnpj != lOldEntity.cnpj)
                addErro("Não pode mudar o CNPJ");

            if (aEmpresa.nome != lOldEntity.nome)
                addErro("Não pode mudar a razão social.");

            return withoutError();
        }

        protected override bool Semantic()
        {
            if (!aEmpresaRepository.update(aEmpresa))
                addErro("Houve um erro ao atualizar a entrada.");

            return withoutError();
        }

        public bool editar(Empresa emp)
        {
            aEmpresa = emp;

            execute();

            return withoutError();
        }
    }
}