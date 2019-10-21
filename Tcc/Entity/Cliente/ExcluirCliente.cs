using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class ExcluirCliente : TaskActivity
    {
        public ExcluirCliente()
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

            return withoutError();
        }

        protected override bool Semantic()
        {
            aClienteRepository.delete(aCliente);

            return withoutError();
        }

        public bool excluir(Cliente cliente)
        {
            aCliente = cliente;

            execute();

            return withoutError();
        }
    }
}