using System;
using Tcc.Apoio;

namespace Tcc.Entity
{
    public class IncluirClienteFixo : TaskActivity
    {
        public IncluirClienteFixo()
        {
           
        }

        private ClienteFixoDTO aClienteFixoDTO;
        private ClienteFixoRepository aClienteFixoRepository = new ClienteFixoRepository();
        private ClienteRepository aClienteRepository = new ClienteRepository();
        private ClienteFixoEmpresaRepository aClienteFixoEmpresaRepository = new ClienteFixoEmpresaRepository();
        private IncluirClienteFixoEmpresa aIncluirClienteFixoEmpresa = new IncluirClienteFixoEmpresa();
        private IncluirCliente aIncluirCliente = new IncluirCliente();
        private ServicoRepository aServicoRepository = new ServicoRepository();
        private ParametrizacaoAgenda aParametrizacaoAgenda;
        private ParametrizacaoAgendaRepository aParametrizacaoAgendaRepository = new ParametrizacaoAgendaRepository();

        protected override bool PreCondicional()
        {
            if (aClienteFixoDTO == null)
                return withoutError(newError("Houve um erro com as informações digitadas."));

            if(aClienteFixoRepository.getDia(aClienteFixoDTO.empresaid, aClienteFixoDTO.tipofrequencia, aClienteFixoDTO.horario) != null)
                return withoutError(newError("Já existe um cliente fixo neste horário."));

            aParametrizacaoAgenda = aParametrizacaoAgendaRepository.getEmpresa(aClienteFixoDTO.empresaid);

            if(aParametrizacaoAgenda == null && (aClienteFixoDTO.horario < aParametrizacaoAgenda.HORAINI || aClienteFixoDTO.horario > aParametrizacaoAgenda.HORAFIM))
                return withoutError(newError("Parametrizacao de agenda não encontrada ou horário fora do intervalo de trabalho."));

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
                dataultimoservico = aClienteFixoDTO.dataultimoservico,
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