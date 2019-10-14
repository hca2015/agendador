using System;
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

                empresas.Attach(Entity);

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
    }
}