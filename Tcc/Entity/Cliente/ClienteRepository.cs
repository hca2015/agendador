using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Tcc.Entity
{
    public class ClienteRepository : Contexto, IContext
    {
        public ClienteRepository()
            : base()
        {

        }
        public DbSet<Cliente> Clientes { get; set; }
        
        public bool add(Modelo prEntity)
        {
            try
            {
                Cliente Entity = (Cliente)prEntity;

                Clientes.Add(Entity);

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
                Cliente Entity = (Cliente)prEntity;

                Clientes.Attach(Entity);

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
                Cliente Entity = (Cliente)prEntity;

                Clientes.Attach(Entity);

                if (Entry(Entity).State == EntityState.Detached)
                    Clientes.Attach(Entity);

                Clientes.Remove(Entity);

                return SaveChanges() > 0;
            }
            catch (Exception e)
            {                
                return false;
            }
        }

        public Cliente getDocumento(string d)
        {
            var linq = from c in Clientes where c.documento == d select c;

            return linq.FirstOrDefault();
        }
    }
}