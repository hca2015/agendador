using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class EditarCliente : TaskActivity
    {
        public EditarCliente()
        {
           
        }

        private Cliente aCliente;
        private ClienteRepository aClienteRepository = new ClienteRepository();

        protected override bool PreCondicional()
        {
            if (aCliente == null)
            {
                addErro("Houve um erro ao obter as informações digitadas.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(aCliente.documento))
                    addErro("Documento não pode estar em branco");
            }

            return withoutError();
        }

        protected override bool Semantic()
        {
            aClienteRepository.update(aCliente);

            return withoutError();
        }

        public bool editar(Cliente cliente)
        {
            aCliente = cliente;

            execute();

            return withoutError();
        }
    }
}