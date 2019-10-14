using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Tcc.Entity
{
    public class ServicoRepository : Contexto, IContext
    {
        public ServicoRepository()
            : base()
        {

        }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<UserEmpresa> EmpresaUsers { get; set; }

        public bool add(Modelo prEntity)
        {
            try
            {
                Servico Entity = (Servico)prEntity;

                Servicos.Add(Entity);

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
                Servico Entity = (Servico)prEntity;

                Servicos.Attach(Entity);

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
                Servico Entity = (Servico)prEntity;

                Servicos.Attach(Entity);

                if (Entry(Entity).State == EntityState.Detached)
                    Servicos.Attach(Entity);

                Servicos.Remove(Entity);

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {                
                return false;
            }
        }

        public Servico getId(int id)
        {
            IQueryable<Servico> linq = from serv in Servicos where serv.servicoid == id select serv;

            return linq.FirstOrDefault();
        }

        public List<Servico> getUser(int id)
        {
            IQueryable<Servico> linq = from serv in Servicos
                       join ep in Empresas on serv.empresaid equals ep.empresaid
                       join epu in EmpresaUsers on ep.empresaid equals epu.empresaid
                       where epu.userid == id
                       select serv;

            return linq.ToList();
        }
    }
}