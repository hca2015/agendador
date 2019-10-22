using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Tcc.Entity
{
    public class ClienteFixoRepository : Contexto, IContext
    {
        public ClienteFixoRepository()
            : base()
        {

        }
        public DbSet<ClienteFixo> ClienteFixos { get; set; }
        public DbSet<ClienteFixoEmpresa> ClienteFixoEmpresas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servico> Servicos { get; set; }

        public ClienteFixo getId(int id)
        {
            return (from c in ClienteFixos where c.clientefixoid == id select c).FirstOrDefault();
        }
        public bool add(Modelo prEntity)
        {
            try
            {
                ClienteFixo Entity = (ClienteFixo)prEntity;

                ClienteFixos.Add(Entity);

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
                ClienteFixo Entity = (ClienteFixo)prEntity;

                ClienteFixos.Attach(Entity);

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
                ClienteFixo Entity = (ClienteFixo)prEntity;

                ClienteFixos.Attach(Entity);

                if (Entry(Entity).State == EntityState.Detached)
                {
                    ClienteFixos.Attach(Entity);
                }

                ClienteFixos.Remove(Entity);

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public List<ClienteFixoDTO> getEmpresa(int empresaid)
        {
            var linq = from cfixo in ClienteFixos
                       join emp in ClienteFixoEmpresas on cfixo.clientefixoid equals emp.clientefixoid
                       join svc in Servicos on cfixo.servicoid equals svc.servicoid
                       join cli in Clientes on cfixo.clienteid equals cli.clienteid
                       where emp.empresaid == empresaid
                       select new ClienteFixoDTO()
                       {
                           clienteid = cli.clienteid,
                           datanascimento = cli.datanascimento,
                           dataultimoservico = cfixo.dataultimoservico,
                           diasemana = (DayOfWeek)cfixo.diasemana,
                           documento = cli.documento,
                           empresaid = empresaid,
                           horario = cfixo.horario,
                           nomecliente = cli.nome,
                           nomeservico = svc.descricao,
                           servicoid = svc.servicoid,
                           tipofrequencia = (ClienteFixo.TipoFrequencia)cfixo.tipofrequencia,
                           clientefixoid = cfixo.clientefixoid
                       };

            return linq.ToList();
        }

        public ClienteFixoDTO getDia(int empresaid, DateTime prDateTime, int horaini)
        {
            var linq = from cfixo in ClienteFixos
                       join emp in ClienteFixoEmpresas on cfixo.clientefixoid equals emp.clientefixoid
                       join svc in Servicos on cfixo.servicoid equals svc.servicoid
                       join cli in Clientes on cfixo.clienteid equals cli.clienteid
                       where emp.empresaid == empresaid
                       && cfixo.diasemana == (int)prDateTime.DayOfWeek
                       && cfixo.horario == horaini
                       select new ClienteFixoDTO()
                       {
                           clienteid = cli.clienteid,
                           datanascimento = cli.datanascimento,
                           dataultimoservico = cfixo.dataultimoservico,
                           diasemana = (DayOfWeek)cfixo.diasemana,
                           documento = cli.documento,
                           empresaid = empresaid,
                           horario = cfixo.horario,
                           nomecliente = cli.nome,
                           nomeservico = svc.descricao,
                           servicoid = svc.servicoid,
                           tipofrequencia = (ClienteFixo.TipoFrequencia)cfixo.tipofrequencia,
                           clientefixoid = cfixo.clientefixoid
                       };


            linq = linq.Where(x => x.ValidarUltimoServico());

            return linq.FirstOrDefault();
        }

        public ClienteFixoDTO getDia(int empresaid, ClienteFixo.TipoFrequencia prFrequencia, int horaini)
        {
            var linq = from cfixo in ClienteFixos
                       join emp in ClienteFixoEmpresas on cfixo.clientefixoid equals emp.clientefixoid
                       join svc in Servicos on cfixo.servicoid equals svc.servicoid
                       join cli in Clientes on cfixo.clienteid equals cli.clienteid
                       where emp.empresaid == empresaid
                       && cfixo.diasemana == (int)prFrequencia
                       && cfixo.horario == horaini
                       select new ClienteFixoDTO()
                       {
                           clienteid = cli.clienteid,
                           datanascimento = cli.datanascimento,
                           dataultimoservico = cfixo.dataultimoservico,
                           diasemana = (DayOfWeek)cfixo.diasemana,
                           documento = cli.documento,
                           empresaid = empresaid,
                           horario = cfixo.horario,
                           nomecliente = cli.nome,
                           nomeservico = svc.descricao,
                           servicoid = svc.servicoid,
                           tipofrequencia = (ClienteFixo.TipoFrequencia)cfixo.tipofrequencia,
                           clientefixoid = cfixo.clientefixoid
                       };


            //linq = linq.Where(x => x.ValidarUltimoServico());

            return linq.FirstOrDefault();
        }
    }
}