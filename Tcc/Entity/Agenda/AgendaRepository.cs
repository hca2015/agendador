using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Tcc.Entity
{
    public class AgendaRepository : Contexto, IContext
    {
        public AgendaRepository()
            : base()
        {

        }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<ClienteFixo> ClienteFixos { get; set; }
        public DbSet<ClienteFixoEmpresa> ClienteFixoEmpresas { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public bool add(Modelo prEntity)
        {
            try
            {
                Agenda Entity = (Agenda)prEntity;

                Agendas.Add(Entity);

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool update(Modelo prEntity)
        {
            try
            {
                Agenda Entity = (Agenda)prEntity;

                Agendas.Attach(Entity);

                Entry(Entity).State = EntityState.Modified;

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool delete(Modelo prEntity)
        {
            try
            {
                Agenda Entity = (Agenda)prEntity;

                Agendas.Attach(Entity);

                if (Entry(Entity).State == EntityState.Detached)
                    Agendas.Attach(Entity);

                Agendas.Remove(Entity);

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Agenda getId(int id)
        {
            return (from a in Agendas where a.agendaid == id select a).FirstOrDefault();
        }

        public List<AgendaDTO> getAgendaDTO(DateTime prData)
        {
            var linq = from ag in Agendas
                       join emp in Empresas on ag.empresaid equals emp.empresaid

                       join cli in (

                       from cfixo in ClienteFixos
                       join svc in Servicos on cfixo.servicoid equals svc.servicoid
                       join cli in Clientes on cfixo.clienteid equals cli.clienteid
                       join cfixoemp in ClienteFixoEmpresas on cfixo.clientefixoid equals cfixoemp.clientefixoid
                       select new ClienteFixoDTO()
                       {
                           clienteid = cli.clienteid,
                           datanascimento = cli.datanascimento,
                           dataultimoservico = cfixo.dataultimoservico,
                           diasemana = (DayOfWeek)cfixo.diasemana,
                           documento = cli.documento,
                           empresaid = cfixoemp.empresaid,
                           horario = cfixo.horario,
                           nomecliente = cli.nome,
                           nomeservico = svc.descricao,
                           servicoid = svc.servicoid,
                           tipofrequencia = (ClienteFixo.TipoFrequencia)cfixo.tipofrequencia,
                           clientefixoid = cfixo.clientefixoid
                       }

                       ) on ag.clienteid equals cli.clientefixoid into ClienteLeft
                       from cliEmpty in ClienteLeft.DefaultIfEmpty()

                       join srv in Servicos on ag.servicoid equals srv.servicoid into ServicoLeft
                       from srvEmpty in ServicoLeft.DefaultIfEmpty()

                       where ag.dia == prData.Date
                       select new AgendaDTO
                       {
                           agenda = ag,
                           empresa = emp,
                           cliente = cliEmpty,
                           servico = srvEmpty
                       };

            List<AgendaDTO> lRetorno = linq.ToList();

            Cliente cliente;
            foreach (var item in lRetorno)
            {
                cliente = null;
                if (item.cliente == null && item.agenda.clienteid.HasValue)
                {
                    cliente = new ClienteRepository().getId(item.agenda.clienteid.Value);
                    if(cliente != null)
                        item.cliente = new ClienteFixoDTO() { clienteid = cliente.clienteid, documento = cliente.documento, nomecliente = cliente.nome, datanascimento = cliente.datanascimento };
                }
            }

            return lRetorno;
        }
    }
}