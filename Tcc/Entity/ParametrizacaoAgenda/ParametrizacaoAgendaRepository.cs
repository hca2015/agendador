using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Tcc.Entity
{
    public class ParametrizacaoAgendaRepository : Contexto, IContext
    {
        public ParametrizacaoAgendaRepository()
            : base()
        {

        }
        public DbSet<ParametrizacaoAgenda> ParametrizacaoAgendas { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<UserEmpresa> EmpresaUsers { get; set; }

        public bool add(Modelo prEntity)
        {
            try
            {
                ParametrizacaoAgenda Entity = (ParametrizacaoAgenda)prEntity;

                ParametrizacaoAgendas.Add(Entity);

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
                ParametrizacaoAgenda Entity = (ParametrizacaoAgenda)prEntity;

                ParametrizacaoAgendas.Attach(Entity);

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
                ParametrizacaoAgenda Entity = (ParametrizacaoAgenda)prEntity;

                ParametrizacaoAgendas.Attach(Entity);

                if (Entry(Entity).State == EntityState.Detached)
                    ParametrizacaoAgendas.Attach(Entity);

                ParametrizacaoAgendas.Remove(Entity);

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {                
                return false;
            }
        }

        public ParametrizacaoAgenda getUser(int id)
        {
            IQueryable<ParametrizacaoAgenda> linq = from pa in ParametrizacaoAgendas
                                                    join ep in Empresas on pa.empresaid equals ep.empresaid
                                                    join epu in EmpresaUsers on ep.empresaid equals epu.empresaid
                                                    where epu.userid == id
                                                    select pa;

            return linq.FirstOrDefault();
        }

        public ParametrizacaoAgenda getEmpresa(int id)
        {
            IQueryable<ParametrizacaoAgenda> linq = from pa in ParametrizacaoAgendas
                                                    join ep in Empresas on pa.empresaid equals ep.empresaid
                                                    join epu in EmpresaUsers on ep.empresaid equals epu.empresaid
                                                    where ep.empresaid == id
                                                    select pa;

            return linq.FirstOrDefault();
        }
    }
}