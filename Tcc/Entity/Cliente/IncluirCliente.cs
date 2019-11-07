using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirCliente : TaskActivity
    {
        public IncluirCliente()
        {
           
        }

        private Cliente aCliente;
        private ClienteRepository aClienteRepository = new ClienteRepository();

        protected override bool PreCondicional()
        {
            if(aCliente == null)
            {
                addErro("Houve um erro ao obter as informações digitadas.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(aCliente.documento))
                    addErro("Documento não pode estar em branco");
                else
                    if (!ValidaCPF.IsCpf(aCliente.documento))
                    addErro("CPF inválido");
            }

            return withoutError();
        }

        protected override bool Semantic()
        {
            aClienteRepository.add(aCliente);

            return withoutError();
        }

        public bool incluir(Cliente cliente)
        {
            aCliente = cliente;

            execute();

            return withoutError();
        }
    }
}