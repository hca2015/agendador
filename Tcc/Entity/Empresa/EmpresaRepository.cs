using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tcc.Models;

namespace Tcc.Entity
{
    public class EmpresaRepository : Contexto, IContext
    {
        public EmpresaRepository()
            : base()
        {

        }
        public DbSet<empresa> empresas { get; set; }
        
        public bool add(Modelo prEntity)
        {
            try
            {
                var Entity = (empresa)prEntity;

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
                var Entity = (empresa)prEntity;

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
                var Entity = (empresa)prEntity;

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
    }
}