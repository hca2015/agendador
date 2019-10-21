using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirClienteFixo : TaskActivity
    {
        public IncluirClienteFixo()
        {
           
        }

        ClienteFixoDTO aClienteFixoDTO;
        ClienteFixoRepository aClienteFixoRepository = new ClienteFixoRepository();
        ClienteFixoServicoRepository aClienteFixoServicoRepository = new ClienteFixoServicoRepository();
        ClienteRepository aClienteRepository = new ClienteRepository();
        ClienteFixoEmpresaRepository aClienteFixoEmpresaRepository = new ClienteFixoEmpresaRepository();
        IncluirClienteFixoEmpresa aIncluirClienteFixoEmpresa = new IncluirClienteFixoEmpresa();
        IncluirClienteFixoServico aIncluirClienteFixoServico = new IncluirClienteFixoServico();
        IncluirCliente aIncluirCliente = new IncluirCliente();
        ServicoRepository aServicoRepository = new ServicoRepository();

        protected override bool PreCondicional()
        {
            if (aClienteFixoDTO == null)
                addErro("Houve um erro com as informações digitadas.");

            return withoutError();
        }

        protected override bool Semantic()
        {
            Cliente cli;
            ClienteFixo cfixo;

            cli = aClienteRepository.getDocumento(aClienteFixoDTO.documento);

            if(cli == null)
            {
                cli = new Cliente()
                {
                    nome = aClienteFixoDTO.nomecliente,
                    documento = aClienteFixoDTO.documento,
                    datanascimento = aClienteFixoDTO.datanascimento,
                };

                aIncluirCliente.incluir(cli);
            }            

            cfixo = new ClienteFixo()
            {
                clienteid = cli.clienteid,
                servicoid = aClienteFixoDTO.servicoid,
                dataultimopagamento = aClienteFixoDTO.dataultimopagamento,
                diasemana = (int)aClienteFixoDTO.diasemana,
                horario = aClienteFixoDTO.horario,
                tipofrequencia = (int)aClienteFixoDTO.tipofrequencia
            };

            aClienteFixoRepository.add(cfixo);

            aIncluirClienteFixoEmpresa.incluir(new ClienteFixoEmpresa() { clientefixoid = cfixo.clientefixoid, empresaid = aClienteFixoDTO.empresaid});

            return withoutError();
        }

        public bool incluir(ClienteFixoDTO clientefixo)
        {
            aClienteFixoDTO = clientefixo;

            execute();

            return withoutError();
        }
    }
}