using System;
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
    }
}