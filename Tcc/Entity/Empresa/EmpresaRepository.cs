using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Tcc.Entity
{
    public class EmpresaRepository : Contexto, IContext
    {
        public EmpresaRepository()
            : base()
        {

        }
        public DbSet<Empresa> empresas { get; set; }
        public DbSet<UserEmpresa> EmpresaUsers { get; set; }

        public bool add(Modelo prEntity)
        {
            try
            {
                Empresa Entity = (Empresa)prEntity;

                empresas.Add(Entity);
                                
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
                Empresa Entity = (Empresa)prEntity;

                Empresa lEmpresa = empresas.Find(Entity.empresaid);

                if(lEmpresa != null && lEmpresa != Entity)
                {
                    lEmpresa.Update(Entity);
                }

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
                Empresa Entity = (Empresa)prEntity;

                empresas.Attach(Entity);

                if (Entry(Entity).State == EntityState.Detached)
                    empresas.Attach(Entity);

                empresas.Remove(Entity);

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {                
                return false;
            }
        }

        public Empresa getId(int empresaid)
        {
            return (from e in empresas where e.empresaid == empresaid select e).FirstOrDefault();
        }

        public Empresa getUser(int prUserId)
        {
            IQueryable<Empresa> linq = from e in empresas
                                       join eu in EmpresaUsers on e.empresaid equals eu.empresaid
                                       where eu.userid == prUserId
                                       select e;

            return linq.FirstOrDefault();
        }

        public Empresa getCNPJ(string CNPJ)
        {
            IQueryable<Empresa> linq = from e in empresas                                       
                                       where e.cnpj == CNPJ
                                       select e;

            return linq.FirstOrDefault();
        }

        public void apagarEmpresa(int empresaid)
        {
            string delete = $@"
            delete [dbo].[userempresa] where empresaid = { empresaid } 
            delete[dbo].[agenda] where empresaid = { empresaid } 
            delete[dbo].[PARAMETRIZACAOAGENDA] where empresaid = { empresaid } 
            delete[dbo].[clientefixoempresa] where empresaid = { empresaid } 
            delete[dbo].[SERVICO] where empresaid = { empresaid } 
            delete[dbo].[empresa] where empresaid = { empresaid } 
            ";

            Database.ExecuteSqlCommand(delete, empresaid);
        }
    }
}